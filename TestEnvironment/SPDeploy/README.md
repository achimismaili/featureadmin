# Solution / Feature deployment for integration tests

This script uses the SharePoint Solution Deployment framework from
https://archive.codeplex.com/?p=spsd

to prepare a reference environment for tests.
It has two steps

## summary

* the features healthy14 are deployed in step1 with version 1.0.0.0, activated and then solution is updated in step2 to version3.0.0.0
  --> all activated features healthy14 are found in feature admin tab "Upgrade"
* the features healthy15 are deployed in step1 with version 1.0.0.0, activated and then solution is updated in step2 to version3.0.0.0
  --> all activated features healthy14 are found in feature admin tab "Upgrade"
* the features faulty15 are deployed in step1 with version 1.0.0.0, activated and then solution is updated in step2 to contain no more features
  --> all activated features faulty15 are orphaned/faulty
* the features faulty14 are deployed in step1 with version 1.0.0.0, activated and then solution is retracted again
  --> web app and farm features get retracted, site and web features faulty14 are orphaned/faulty

## Step 1 - Deploy dummy solutions and activate features

(Re)Deploy the SPSD Deploy Features 1.0.0.0

### Steps to execute:

1. go to folder /DeployFeatures1.0.0.0
1. Run Redeploy.bat

#### This will

* install following dummy-solutions:
    - DummyFeaturesHealthy14.wsp
        Solution in version 14 with 1 (healthy) dummy feature per scope (web, site, webapp, farm)
    - DummyFeaturesHealthy15.wsp
        Solution in version 15 with 1 (healthy) dummy feature per scope (web, site, webapp, farm)
    - DummyFeaturesFaulty14.wsp
        Solution in version 14 with 1 (faulty) dummy feature per scope (web, site, webapp, farm)
        (at installation time, the feature is not faulty, just when removing the feature definition without deactivating the feature before)
    - DummyFeaturesFaulty15.wsp
        Solution in version 15 with 1 (faulty) dummy feature per scope (web, site, webapp, farm)
        (at installation time, the feature is not faulty, just when removing the feature definition without deactivating the feature before)
* After adding and deploying, it will activate all featues in following URL https://featuretest.dbi.local(/sites/activated14(and /activated15)(/sub/subsub))
* It will then Remove the DummyFeaturesFaulty14.wsp - this will cause the 14 faulty web and site features to become orphaned (faulty)

## Step 2 - Deploy updated solutions and solutions wit removed features

As second step, run "Update" in SPSD "Deploy Features 3.0.0.0"

### Steps to execute:

1. go to folder /DeployFeatures3.0.0.0
1. Run Update.bat

#### This will

* update the following solutions with new WSPs:
    - DummyFeaturesHealthy14.wsp
        This modified solution has each feature version set to 3.0.0.0, so that the activated features in all 4 scopes can be upgraded.
    - DummyFeaturesHealthy15.wsp
        This modified solution has each feature version set to 3.0.0.0, so that the activated features in all 4 scopes can be upgraded.
    - DummyFeaturesFaulty15.wsp
        This modified solution has _no features(!)_ (all features are removed), so that all faulty15 features are missing in the solution and activated features become orphaned (faulty)

## Test

Now you should be able to run the feature admin and find some test features in the tab "Upgrade" and "Cleanup"

