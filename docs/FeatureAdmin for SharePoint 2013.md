The FeatureAdmin for SP2013 also runs on SP 2013 Foundation.
To use it, run the FarmAdmin directly on a SharePoint Server and make sure, your account has the appropriate [Rights](Rights)

Please refer to the [General Feature Overview](General-Feature-Overview) regarding functionality for all versions

_Differences especially for SharePoint 2013:_

In SharePoint 2013, Features can be installed for Compatibility Level 14 and 15. A lot of the Standard SharePoint Features exist for both Compatibility Levels. Therefore, a new prefix was introduced to the FeatureAdmin, that shows the Compatibility Level in front of the Guid. 
    +Example:+
     WebApplication: 'SharePoint Server Standard Web application features' (**_15/_**4f56f9fa-51a0-420c-b707-63ecbb494db1)
     WebApplication: 'SharePoint Server Standard Web application features' (**_14/_**4f56f9fa-51a0-420c-b707-63ecbb494db1)
