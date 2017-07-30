This script uses the SharePoint Solution Deployment framework from

To reset the reference environment, follow these steps:

Deploy the Features 1.0.0.0
This will
- Install 2 features per scope (web, site, webapp, farm)
    and activate them
- uninstall 1 feature per scope
- This way, 1 feature per scope will be orphaned (faulty)

Then, Deploy Features 3.0.0.0
This will
- Install 1 features per scope (web, site, webapp, farm)
- This way, 1 feature per scope can be updated

