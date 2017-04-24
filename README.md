## FeatureAdmin
SharePoint Feature Administration and Clean Up Tool
## FeatureAdmin
SharePoint Feature Administration and Clean Up Tool to find and delete broken features in a SharePoint farm

[Further Documentation](docs/Documentation.md)

[Release Downloads](Releases)

[License: Microsoft Public License](License.md)

## Tool Description
FeatureAdmin is a tool for SharePoint administrators and developers to manage SP features. It finds and cleans faulty FeatureDefinitions and orphaned reminders.

The Feature Admin Tool finds faulty FeatureDefinitions and cleanly uninstalls them.
It finds feature remainders in sites, SiteCollections, WebApps and in the Farm, caused e.g. by forcefully uninstalled Features from a farm without deactivating them before. These faulty features, never visible, cause errors.
The Feature Admin Tool is able to identify them and remove them.
Very useful for major version upgrades e.g. to 2010 or 2013, or for downgrades from Enterprise to Standard.

Also, uninstall, (bulk) deactivate or (bulk) activate features with any scope farm wide.
Ideal, for activating or deactivating features in large farms with many sites and/or SiteCollections.

## FeatureAdmin 2.4.4

2015-09 Perry implemented some bug fixes and stability improvements, so that some issues raised by the community can be closed.

Thanks to Project Team member _PerrySharePoint_, functionality from the SP2010 Version was migrated to the MOSS Version and for all 3 versions, Long expected exception handling was introduced. Thanks Perry!

The feature admin is available for SP 2013 (& SP 2013 Foundation), SP 2010 (& SP 2010 Foundation Services) and for MOSS 2007 (& WSS 3.x).

## What others are saying about this tool
- _**"It's been a very useful yet very powerful tool."**_, by **[(SharePoint-)Joel Oleson](http://www.sharepointjoel.com/Lists/Posts/Post.aspx?ID=469)**
- _"Swiss Army knife for managing features in SharePoint"_, by **[The SharePoint Nomad](http://sharepointnomad.wordpress.com/2010/05/22/262/)**
- _"Tool that can find faulty features and remove them from the system"_,
>{from the book: "[Windows Small Business Server 2011 - Das Handbuch](http://www.amazon.de/Microsoft-Windows-Business-Server-Standard/dp/3866451385/ref=sr_1_1?s=books&ie=UTF8&qid=1307103905&sr=1-1)", page 187, by **[Thomas Joos](http://thomasjoos.wordpress.com/books/)**}>
- _"... does a great job in listing Feature Definitions across Site Collections and Sub Webs and cleanly uninstalling them."_,
>{by  **[(SharePoint-)George Khalil](http://sharepointgeorge.com/2009/upgrading-content-db-sharepoint-2010-part-1-preupgradecheck/)**}>
- _"Tool to remove missing features"_, by **[Paul King, Microsoft](http://blogs.msdn.com/b/paulking/archive/2011/10/05/removing-missingfeature-database-amp-missingwebpart-webpart-class-errors-from-sharepoint-2010.aspx)**
- _**"Oh my God, your product saved my job!"**_, by jasonengberg
- _**"This tool saved me from pulling out the remaining hair that I have. Worked like a charm."**_, by rsreagan
- _"I loved it. This is the best tool to use when in jamm with orphaned features"_, by nee2ok
- _"Saved my day!!! Thanks..."_, by vinodkumarpvk
- _"Champion! Love it! Thank you SOOOOO much!"_, by drewberrylicious
- _"Excellent tool."_, by rarandas


... if the _Feature Admin Tool_ was able to help you, too, please rate it.

## Most useful are two buttons:
Button **'Find Faulty Feature in Farm'**
With this one click, you can check, if there are any errors in any Feature Collections (Farm, WebApplication, Site or Web) in your farm. If this runs through the farm and tells you "No errors found", all your farm's feature collections are clean. (If it finds a faulty feature of scope Web or Site, you can browse in the left window to it and see the errors in the Register of the Site and Web Features (yellow).
Once, it finds a faulty feature, you can let it be deactivated in / removed from the whole farm.

Button **'Uninstall'**
Functionality regarding Feature Definitions. Feature definitions can also cause errors in a farm.
Before you Uninstall feature definitions, you are asked, if you want to first deactivate the feature everywhere in the farm. If the Feature Definition is corrupt, and not even the Feature scope can be retrieved, logic was added that checks all FeatureCollections in the farm for this Feature, before it gets uninstalled.

## 'Features' of the FeatureAdmin Tool
* List features and feature definitions
* Search for faulty features in the farm and remove them cleanly if wanted
* Uninstall feature Definitions from the farm, even if faulty
* _When uninstalling a feature, FeatureAdmin provides an option to deactivate this feature in the entire farm before uninstalling it!_
* can also simply activate or deactivate _features with any scope_ in the whole farm, in only a Web Application, only within a SiteCollection or in a Site (SPWeb)
* Web Apps are parsed automatically, after selecting one, all Site Collections in the Web App are shown
* When selecting a SiteCollection, all sites (webs) are listed automatically.
* logging information is shown (e.g. change of selections, features added or removed)
* Features and Feature Definitions are sorted (first after scope and then) after name
* The FeatureAdmin window and all internal windows are resizeable (starting with version 2.1)

## Preconditions
**You need to have rights!**
 +dbOwner rights to the SharePoint Configuration database and to every content database, you would like to cleanup, activate or deactivate features on.+
+You should add your login account to the _Web Application User Policy_ with Full Control for each of the content web apps in the farm and it should be a member of the farm administrators group+.
Additionally, this tool needs to run locally on a SharePoint Instance.
If you use the "find faulty feature" functionality, it will parse through the whole farm.

## Screenshots
Feature View - remove Features
![](docs/FeatureAdmin-Remove-Features.png)

Feature Definition View - activate or uninstall Features
![](docs/FeatureAdmin-Installed-Features.png)

## The Problem, that triggered development of this tool:
After doing Solution Package based deployment for a while, I found out, that 'stsadm -o upgradesolution' is not supported, when features are added / removed. (see article about this [http://sharepointtipoftheday.blogspot.com/2009/06/solution-feature-upgrading-and.html](http://sharepointtipoftheday.blogspot.com/2009/06/solution-feature-upgrading-and.html))
Also, I had some cases, where Features had forcefully been uninstalled from a farm without deactivating them first in all Sites / SiteCollections / WebApps or in the Farm.

All this has caused errors and left overs in the Farm, e.g. it was not possible to open the WorkFlow Overview page in several SiteCollections, because it was complaining about missing Features. Also, when doing exports, you might run in the error: FatalError: Failed to compare two elements in the array.
## Roots of the Feature Admin Tool
I found the Faulty Feature Tool from "Steven Van de Craen", at
[http://www.moss2007.be/blogs/vandest/archive/2008/04/28/stsadm-o-export-fatalerror-failed-to-compare-two-elements-in-the-array.aspx](http://www.moss2007.be/blogs/vandest/archive/2008/04/28/stsadm-o-export-fatalerror-failed-to-compare-two-elements-in-the-array.aspx)
which solved some of my problems. It was very basic and I needed it for a lot more.

## Source Code
See https://github.com/achimismaili/featureadmin
fork https://github.com/SharePointPog/FeatureAdmin

formerly on github at: https://featureadmin.codeplex.com/