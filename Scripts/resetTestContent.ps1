# resets test content for feature admin unit tests

# some of this script logic is based on the following powershell scripts:
# https://gallery.technet.microsoft.com/office/Get-collection-owners-for-13336f43
# https://gallery.technet.microsoft.com/scriptcenter/c1366cfb-f518-4595-98c7-3a2fc3454473

# How to Use / Getting started:
# Before the tests can be run, some preparation is required:
# 1. build the two dummy features projects and deploy them to the farm
# 2. run this script with a parameter of a test web application, e.g. resetTestContent.ps1 https://sp.local
# 3. The script will create a managed path fa and it will create 2 Sitecollection below it
# 4. the script will retract the dummyfeaturesfaulty solution, so that the activated features of this solution will become faulty
# 5. now you can run the reference end to end tests



$snapin = get-pssnapin | Where-Object { $_.Name -eq "Microsoft.SharePoint.PowerShell" }
if ($snapin -eq $null) {
    Add-PsSnapin Microsoft.SharePoint.PowerShell
}

#DECLARE VARIABLES
# The web app url is the web application url from the test web application, where test sites will be created 
[string]$webAppUrl = $args[0]

function GetMissingParameter {
    $script:webAppUrl = Read-Host "Enter Web Application URL"
}

#IF MISSING PARM FOR URL, ASK FOR INPUT TO FILL
if ($args.length -eq 0) {
    GetMissingParameter
}

# content hierarchy
$testSitesManagedPath = "fa" # fa = feature admin

$nameActivated = "activated"
$nameInactive = "inactive"

$dummySolutionWspName = "DummyFeaturesFaulty.wsp"

$featureIdHealthyWeb = "6a5615a2-4c44-40dd-ac9f-26cc45fb7e79"
$featureIdHealthySiCo = "bdd4c395-4c92-4bf8-8c61-9d12349bb853"
$featureIdHealthyWebApp = "cb53cddc-4335-4560-bf29-f1a0c47f8e6a"
$featureIdHealthyFarm = "d2cb3620-aacb-459e-842d-dc09aea28828"

$featureIdFaultyWeb = "be656c15-c3c9-45e4-af56-ffcf3ef0330a"
$featureIdFaultySiCo = "ff832e14-23ea-483e-80f8-5a22cb7297a0"
$featureIdFaultyWebApp = "6bf1d2d1-ea35-4bf7-817f-a65549c5ffe9"
$featureIdFaultyFarm = "c08299d9-65fd-4871-b2e4-8de19315f7e8"


# Defaults
$executionTime = Get-Date
$ownerAlias = "DBI\Max"
$compatibilityLevel = "15"
$language = 1031 # 1031 German, 1033 English
$template = "STS#1" # STS#0 = TeamSite , STS#1 = blank site
$titleSuffix = " Test site created '$executionTime'"

# $webApp = [Microsoft.SharePoint.Administration.SPAdministrationWebApplication]::Local
# $caWeb = $webApp.Sites[0]

$ownerAlias = whoami.exe


function CreateSiteCollection($url, $title, $featuresActivated) {
    
    Write-Output ""
    Write-Output "Working on site collection $($url)"    
     
    New-SPSite -Url $url -Name $title -owneralias $ownerAlias -template $template -CompatibilityLevel $compatibilityLevel -Language $language

    # in RootWeb, enable web features same as SiCo features
    if ($featuresActivated) {
        Enable-SPFeature -identity $featureIdHealthySiCo -Url $url  
        Enable-SPFeature -identity $featureIdFaultySiCo -Url $url  
        
        Enable-SPFeature -identity $featureIdHealthyWeb -Url $url 
        Enable-SPFeature -identity $featureIdFaultyWeb -Url $url  
    }

    # create sub webs
    CreateSpWeb "$url/$nameActivated" "Web Features $nameActivated, SiCo $title" $true
    CreateSpWeb "$url/$nameInActive" "Web Features $nameInActive, SiCo $title" $false
}

function CreateSpWeb($url, $title, $webFeaturesActivated) {
    
    Write-Output ""
    Write-Output "  - Working on sub web $($url)"    
     
    New-SPWeb -Url $url -Name $title -template $template -Language 1033

    if ($webFeaturesActivated) {
        Enable-SPFeature -identity $featureIdHealthyWeb -Url $url 
        Enable-SPFeature -identity $featureIdFaultyWeb -Url $url  
    }

}
        
try {
   
    # Install the solution, if it is currently retracted ... ( it needs to be added already)
    Install-SPSolution $dummySolutionWspName -GACDeployment -ErrorAction SilentlyContinue


    Write-Output ""
    Write-Output "Enabling farm features"

    # enable farm features
    Enable-SPFeature -identity $featureIdHealthyFarm -Force:$true -ErrorAction SilentlyContinue
    Enable-SPFeature -identity $featureIdFaultyFarm -Force:$true -ErrorAction SilentlyContinue 

    #Fetches webapplication
    $wa = Get-SPWebApplication $webAppUrl

    # now, webAppUrl always ends with "/"
    $webAppUrl = $wa.Url
    # this url does NOT end with "/"
    $sitesPrefix = "$webAppUrl$testSitesManagedPath"


    Write-Output ""
    Write-Output "Working on web application $($webAppUrl)"

    # enable web app features
    Enable-SPFeature -identity $featureIdHealthyWebApp -Url $webAppUrl -Force:$true -ErrorAction SilentlyContinue
    Enable-SPFeature -identity $featureIdFaultyWebApp -Url $webAppUrl -Force:$true -ErrorAction SilentlyContinue


    New-SPManagedPath $testSitesManagedPath -WebApplication $webAppUrl -ErrorAction SilentlyContinue

    #delete existing test site collections, if there are any
    $scToBeDeleted = (Get-SPSite -Limit ALL).Where( {$_.Url -like "$sitesPrefix/*" })

    if ($scToBeDeleted -ne $null  -and $scToBeDeleted.Count -gt 0 ) {
        foreach ($site in $scToBeDeleted) {
            write-host "Deleting Site " + $site.Url

            remove-Spsite -identity $site.Url -confirm:$false 
        }
    }

    # create test content
    Write-Output ""
    Write-Output "Creating Test Content in web app '$($webAppUrl)'"

    # healthy sico
    $scUrl = "$sitesPrefix/$nameActivated"
    $url = $scUrl
    $title = "Features $nameActivated $titleSuffix"

    CreateSiteCollection $url $title $true
    
    # faulty sico
    $scUrl = "$sitesPrefix/$nameInactive"
    $url = $scUrl
    $title = "Features $nameInactive $titleSuffix"

    CreateSiteCollection $url $title $false


    # retract dummy faulty feature solution
    Write-Output ""
    Write-Output "Retracting dummy faulty feature solution '$dummySolutionWspName'"
    Uninstall-SPSolution $dummySolutionWspName -Confirm:$false

}    

catch [Exception] {
    Write-Error $Error[0]
    $err = $_.Exception
    while ( $err.InnerException ) {
        $err = $err.InnerException
        Write-Output $err.Message
    }
}


Write-Output ""
Write-Output "Script Execution finished"