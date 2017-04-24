If you want to use FeatureAdmin, you need to have rights!

SharePoint requires different rights, depending on the scope of a Feature, you would like to cleanup, activate, deactivate or uninstall.


If you are e.g. using the [Find Faulty Feature](Find-Faulty-Feature.md) functionality the complete farm is parsed and all the following rights are required:

_+Rights required for Farm scoped Features:+_
- Full control on SQL database Level for SharePoint Config Database
- _(?) member of farm Administrators (? not 100% sure)_

_+Rights required for Web Application scoped Features:+_
- Full control on SQL database Level for SharePoint Config Database
- _(?) member of farm administrators (? not 100% sure)_

_+Rights required for SiteCollection (SPSite) scoped Features:+_
- SiteCollection Administrator rights or
- Full control via Web Application User Policy (recommended, because with one Setting, all content for a web application is configured correctly)
- _(?) Full control on SQL database Level for all SharePoint Content Databases (? not 100% sure)_

_+Rights required for Web (SPWeb) scoped Features:+_
- SiteCollection Administrator rights or
- Full control via Web Application User Policy (recommended, because with one Setting, all content for a web application is configured correctly)
- _(?) Full control on SQL database Level for all SharePoint Content Databases (? not 100% sure)_


-+Recommended account configuration:+_
You should add a login account to the Web Application User Policy with Full Control for each of the content web apps in the farm and it should be a member of the farm administrators group.
Also, grant this account dbowner rights to the SharePoint Config Database and to all Content Databases

If not in use, you could also disable the login account.
