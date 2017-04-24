**'Features' of the FeatureAdmin Tool**


* List features and feature definitions
  orange background: Activated Features in SiteCollections and Webs 
  blue Background: Installed Features in the farm

* Search for faulty features in the farm and remove them cleanly if wanted
  Use the Button [Find Faulty Features](Find-Faulty-Features.md)
  
* Uninstall feature Definitions from the farm, even if faulty

* When uninstalling a feature, FeatureAdmin provides an option to deactivate this feature in the entire farm before uninstalling it!
  The correct way, to uninstall a Feature, so that no orphaned Information is left in the farm is, by first deactivating the Feature in the complete farm and then uninstalling it. When clicking "uninstall", the featureadmin offers to first deactivate this Feature in the complete farm, before uninstalling it.

* can also bulk deactivate Features (with scope of web or SiteCollection) and bulk activate features (with any scope) in the whole farm, in only one Web Application, only within a SiteCollection (or in just a single web (SPWeb)

* All Web Apps of a farm are retrieved and listed
* After selecting a Web App, all Site Collections in this Web App are shown

* When selecting a SiteCollection, all sites (webs) of this SiteCollection are listed automatically.

* logging information is shown (e.g. change of selections, features added or removed)
   This Information can e.g. be selected and copied to the clipboard.

* Features and Feature Definitions are sorted (first after scope and then) after name

* The FeatureAdmin window and all internal windows are resizeable (starting with version 2.1)
