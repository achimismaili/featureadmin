# FeatureAdmin Release Notes

## Relevant Links

* [Homepage FeatureAdmin](https://www.featureadmin.com)
* [Readme](https://github.com/achimismaili/featureadmin/blob/master/README.md)
* [Source Code](https://github.com/achimismaili/featureadmin)
* [Release Notes stable releases](https://github.com/achimismaili/featureadmin/blob/master/Releases/ReleaseNotes.md)
* [Release Notes development releases](https://github.com/achimismaili/featureadmin/blob/development/Releases/ReleaseNotes.md)

* [License](https://github.com/achimismaili/featureadmin/blob/master/license.md)

## Version 3.0.0

### Available SharePoint target versions

* FeatureAdmin for SP 2013
* FeatureAdmin Demo

### New Features

* Complete redesign (based on WPF, Mah Apps, Caliburn Micro, akka.net, OrigoDb)
* Search as you type for feature definitions and locations
* Initial farm load on app start with progress bar
* Shows all faulty / orphaned features within farm
* Supports feature upgrade
* Supports 'elevated privileges' mode (run as system) for handling in site collections and web sites
* Supports 'Force' mode for always leveraging force flag when applicable
* demo version available, runs without SharePoint installation

### Bugfixes

* Web Application Features were sometimes not handled correctly
* in 2.4.8, it was no longer possible to deactivate/remove faulty/orphaned features


## Version 2.4.8 (Mar 10, 2016)

### New Features

* Improved exception handling
* Ask user whether to use Force Flag
* Upgrade left-hand lists (webapps, sites, webs) to multicolumn

### Known issues
* Web Application Features are sometimes not handled correctly
* It is no longer possible to deactivate/remove faulty/orphaned features

## Version 2.4.6 (Feb 10, 2016)

### Bugfixes

* Fix erroneous exception during ForceUninstallFeatureDefinition

## Version 2.4.4 (Sep 23, 2015)

### New Features

* Exception wrapper when removing faulty site collection feature

## Version 2.4.2 (Aug 27, 2015)

### New Features

* Include CentralAdmin

## Version 2.4 (Aug 27, 2015)

### New Features

* Better exception handling when removing faulty features, and allow skipping individual
    faulty features

## Version 2.3 (Feb 24th 2013)

### New Features
- Show Feature Compatibility Level as prefix of the Guid

### BugFixes

- issue 8513* fixed SP2013 feature definition uninstall, that compatibility level is recognized
- Trapped & logged Access Denied exceptions in Find Faulty Feature (so it doesn't crash)
- Fix Find Activated Feature to correctly find web application scoped features
