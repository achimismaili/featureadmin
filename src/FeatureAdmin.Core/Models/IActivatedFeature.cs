//using FeatureAdmin.Core.Models.Enums;
//using System;
//using System.Collections.Generic;

//namespace FeatureAdmin.Core.Models
//{
//    /// <summary>
//    /// Feature Model
//    /// </summary>
//    /// <remarks>
//    /// see also https://msdn.microsoft.com/en-us/library/microsoft.sharepoint.spfeature_members.aspx
//    /// </remarks>
//    public interface IActivatedFeature 
//    {
//        // identifier based on parent, feature-definition and compatibility level
//        Guid FeatureId { get; }
//        Guid LocationId { get; }
//        DateTime TimeActivated { get; }
//        Version Version { get; }
//        Dictionary<string,string> Properties { get; }
//        bool Faulty { get; }
//    }
//}