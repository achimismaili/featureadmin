# Recommended Permissions for running FeatureAdmin

The FeatureAdmin ONLY USES SharePoint API, it DOES NOT access the farm in any unsupported way.
If you know, what you are doing and you have sufficient rights for that, this should be enough.

In general, If you get the SharePoint farm returned, when entering 'Get-SPFarm' in the SharePoint (PowerShell) Management Shell, this is already a good sign that you have all the permissions you need and that your farm is up and running.

Nevertheless, if you are running into issues, or if you have custom hardening and many custom permissions defined in your farm setup, you might want to check out these topics, if they help you on your way to achieve what you want:

## Server Role Web Front End (WFE)

FeatureAdmin has to run locally on a SharePoint server with the corresponding SharePoint version, so for SP 2013, you should download the FeatureAdmin for SP 2013. (Often, a lower FeatureAdmin version also still runs on higher versions.)

The SharePoint server you are running FeatureAdmin on, should have the role web front end (wfe), as some feature definitions are not installed on servers without this role. 

For demo purposes, there is also a _[demo version](https://github.com/achimismaili/featureadmin/tree/master/Releases/demo/stable/featureadmin.exe)_ availabe that does not require a SharePoint installation, it even runs on Windows Desktop operating systems.

## Local Administrator (Windows built in group)

It is recommended to be a local administrator, so that you also can start feature admin 'as administrator' (UAC)
Actually, running as administrator seems to be the only way, it is possible to access the farm and its features starting with SP 2016. (FeatureAdmin will automatically start as administrator, if you have sufficient rights.)

## SPShellAdmin

In SharePoint PowerShell, when entering 'Get-SPShellAdmin', make sure, your name appears under the list of names

## Farm Administrator

As FeatureAdmin parses the farm on start, it is also required to have Farm Administrator rights.
Make sure, you are a member of the farm administrators in central administration.

## Locked (Readlonly) Site Collections

If you have locked or read only site collections in your farm, you obviously cannot manage features in there.
FeatureAdmin works best, if no site collections are locked or set to readonly in a farm.

## Web Application User Policy (is not required)

It should not be required that you put your account with full control into each Web application, as long as you check the 'Elevated Privileges' setting in FeatureAdmin (this is checked by default). In case you even then continue to have issues, you still might try out the user policy, if it brings you forward.

## Database access

If you have problems accessing locations or feature definitions or if you fail to modify features, maybe you are missing database rights. SharePoint requires different rights, depending on the scope of a Feature, you would like to activate, deactivate, upgrade or uninstall.

These are databases, you might want to check out, if you have enough privileges:

* SharePoint Config Database (for access to farm and web applications and its activated features)
* SharePoint Content Databases (for access to Site Collections (SPSite) or Webs (SPWeb) and its activated features)

## File System access

The Farm Feature Definitions are read from the file system from the following path:
C:\Program Files\Common Files\microsoft shared\Web Server Extensions\[SP version]\TEMPLATE\FEATURES\
You might want to check if you or rather the farm admin account has access to this folder, in case you are running into issues.