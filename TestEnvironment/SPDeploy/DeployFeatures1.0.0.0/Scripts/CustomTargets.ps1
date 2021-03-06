###############################################################################
# SharePoint Solution Deployer (SPSD)
# Version          : 5.0.4.6440
# Url              : http://spsd.codeplex.com
# Creator          : Matthias Einig, RENCORE AB, http://twitter.com/mattein
# License          : MS-PL
###############################################################################

# EDIT THIS FILE to perform custom actions at certain events during the deployment process
# The available events are (in order how they are called): 
#    - RunCustomPrerequisites
#    - CheckCustomPreconditions
#    - Initialize
#    - BeforeDeploy/BeforeRetract/BeforeUpdate
#    - AfterDeploy/AfterRetract/AfterUpdate
#    - ProcessSolution
#    - Finalize


#region CustomPrerequisites
# Desc: use this target to run commands after the environment XML is loaded and before the preconditions are checked. 
#       this target allows you to add your custom addins or other prerequisites to prepare the environment e.g. setting up SCVMM.
#       on failure throw and exception to abort the rest of the script.
function RunCustomPrerequisites($vars) {
    #    Log "Run prerequisite (Custom Event)" -Type $SPSD.LogTypes.Normal -Indent
    #    Log "Run my custom prerequisite..." -Type $SPSD.LogTypes.Normal -NoNewline
    #    if(MyCustomPrecondition -eq $true){
    #    	Log "Ok" -Type $SPSD.LogTypes.Success -NoIndent
    #    }
    #    else{
    #     	Log "Failed" -Type $SPSD.LogTypes.Error -NoIndent
    #  		Throw "My custom prerequisite failed"
    #    }
    #    LogOutdent
}
#endregion

#region CheckCustomPreconditions
# Desc: use this target to run commands after all out-of-the-box precondition checks have been passed successfully.
#       if your precondition is not met, throw and exception to abort the rest of the script.
function CheckCustomPreconditions($vars) {
    #    Log "Checking precondition (Custom Event)" -Type $SPSD.LogTypes.Information -Indent
    #    Log "Checking my custom precondition 1..." -Type $SPSD.LogTypes.Normal -NoNewline
    #    if(MyCustomPrecondition1 -eq $true){
    #      Log "Ok" -Type $SPSD.LogTypes.Success -NoIndent
    #    }
    #    else{
    #      Log "Failed" -Type $SPSD.LogTypes.Error -NoIndent
    # 	   Throw "My custom precondition failed"
    #    }
    #    LogOutdent
}
#endregion

#region Initialize
# Desc: use this target to perform commands after all precondition checks were successful and before any deploy/update/retract action is started. 
#       This will allow you to do anything to the running SharePoint environment.
function Initialize($vars) {

    Log "Initializing (Custom Event)" -Type $SPSD.LogTypes.Information -Indent

    $siteUrl15 = "{0}{1}" -f $($vars['SiteBaseUrl']), "15"
    Log "Remove SiteCollection '$siteUrl15'" -Type $SPSD.LogTypes.Information 
    Remove-SPSite -Identity "$siteUrl15" -GradualDelete -Confirm:$False -ErrorAction SilentlyContinue
    
    $siteUrl14 = "{0}{1}" -f $($vars['SiteBaseUrl']), "14"
    Log "Remove SiteCollection '$siteUrl14'" -Type $SPSD.LogTypes.Information 
    Remove-SPSite -Identity "$siteUrl14" -GradualDelete -Confirm:$False -ErrorAction SilentlyContinue

    LogOutdent
}
#endregion

#region ProcessSolution
# Desc: use this target to skip a solution based on your own conditions or perform any custom command before the deployment step takes place
#       it will be run once for each solution for each deployment command
# 		if you return $true then the deployment command is skipped for that solution
function ProcessSolution ($vars, [string]$solutionName, [int]$cmd, [bool]$isSandboxedSolution) {
    #   Log "Skip solution '$solutionName' (Custom Event)..." -Type $SPSD.LogTypes.Information -Indent -NoNewline
    #	$ProcessSolution = $false

    #    	add commands here
    #	switch($cmd){
    #	   $SPSD.Commands.Deploy  {  }
    #	   $SPSD.Commands.Retract {  }
    #	   $SPSD.Commands.Update  {  }
    #	}

    #    if($ProcessSolution){
    #        Log "Skipped" -Type $SPSD.LogTypes.Warning -Outdent -NoIndent
    #    }
    #    else{
    #        Log "Not skipped" -Type $SPSD.LogTypes.Success -Outdent -NoIndent
    #    }
    #	return $ProcessSolution
}
#endregion


#region BeforeDeploy
# Desc: use this target to perform commands before a deployment.
#       runs on commands: Deploy, Redeploy
function BeforeDeploy($vars) {
    # Log "BeforeDeploy (Custom Event)" -Type $SPSD.LogTypes.Information -Indent

    

    # LogOutdent  
}
#endregion

#region AfterDeploy
# Desc: use this target to perform commands after a successful deployment
#       runs on commands: Deploy, Redeploy
function AfterDeploy($vars) {
    # Log "AfterDeploy (Custom Event)" -Type $SPSD.LogTypes.Information -Indent
    

    # LogOutdent 
    # Enable-SPFeature -Identity '[feature name]' -Url '$vars["SiteUrl"]' -Force
    # Enable-SPFeature -Identity [feature guid] -Url '$vars["SiteUrl"]' -Force
}
#endregion

#region BeforeRetract
# Desc: use this target to perform commands before retraction
#       runs on commands: Retract, Redeploy
function BeforeRetract($vars) {
    # Log "BeforeRetract (Custom Event)" -Type $SPSD.LogTypes.Information -Indent

    
    # #   Disable-SPFeature -Identity '[feature name]' -Url '$vars["SiteUrl"]' -Confirm:$false -Force 
    # #     Uninstall-SPFeature -Identity '[feature name]' -Confirm:$false -Force 
    # #     Remove-SPSite -Identity '$vars["SiteUrl"]' -Confirm:$false 
    # LogOutdent  
}
#endregion

function CreateTestSite{
    
Param ([string] $siteBaseUrl, [int] $compatibilityLevel)

    $siteUrl = "{0}{1}" -f $siteBaseUrl, $compatibilityLevel

    $subWebUrl = "{0}/sub" -f $siteUrl
    $subSiblingWebUrl = "{0}/subsibling" -f $siteUrl
    $subSubWebUrl = "{0}/sub/subsub" -f $siteUrl

    Log "SiteUrl $siteUrl" -Type $SPSD.LogTypes.Information -Indent
    Log "SubWebUrl $subWebUrl" -Type $SPSD.LogTypes.Information -Indent

    # Defaults
    $executionTime = Get-Date
    $ownerAlias = "$env:USERDOMAIN\$env:USERNAME"
    $language = 1033 # 1031 German, 1033 English
    $template = "STS#0"
    $name = "$($nameSuffix)Test site created '$executionTime'"

    Log "Create SiteCollection '$($vars['SiteUrl'])'" -Type $SPSD.LogTypes.Information 
    New-SPSite -Url $siteUrl -OwnerAlias $ownerAlias -Name $name -Template $template -CompatibilityLevel $compatibilityLevel -Language $language

       
    New-SPWeb $subWebUrl -Name "SubWeb $name" -Template $template

    New-SPWeb $subSiblingWebUrl -Name "SubSibling $name" -Template $template

    New-SPWeb $subSubWebUrl -Name "SubSub $name" -Template $template

}

#region AfterRetract
# Desc: use this target to perform commands after a successful retraction
#       runs on commands: Retract, Redeploy
function AfterRetract($vars) {
    Log "AfterRetract (Custom Event)" -Type $SPSD.LogTypes.Information -Indent

    CreateTestSite $($vars['SiteBaseUrl']) 15

    CreateTestSite $($vars['SiteBaseUrl']) 14
    
    LogOutdent
}
#endregion

#region BeforeUpdate
# Desc: use this target to perform commands before update
#       runs on commands: Update
function BeforeUpdate($vars) {
    #   Log "BeforeUpdate (Custom Event)" -Type $SPSD.LogTypes.Information -Indent
    # 	   add commands here
    #   LogOutdent
}
#endregion

#region AfterUpdate
# Desc: use this target to perform commands after a successful update
#       runs on commands: Update
function AfterUpdate($vars) {
    #   Log "AfterUpdate (Custom Event)" -Type $SPSD.LogTypes.Information -Indent
    # 	    add commands here
    #   LogOutdent
}
#endregion

#region Finalize
# Desc: use this target to perform commands at the very end before the deployment summary is shown.
#       runs also in case of an exception
function Finalize($vars) {
    # $dummySolutionWspName15 = "DummyFeaturesFaulty15.wsp"
    $dummySolutionWspName14 = "DummyFeaturesFaulty14.wsp"
    Log "Finalizing (Custom Event)" -Type $SPSD.LogTypes.Information -Indent
    # Uninstall-SPSolution $dummySolutionWspName15 -Confirm:$false
    Uninstall-SPSolution $dummySolutionWspName14 -Confirm:$false
    Restart-WebAppPool -Name "SharePoint Content"

    LogOutdent
}
#endregion
