using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.SharePoint;


using System.Windows.Forms;


namespace FeatureAdmin
{
    public class Feature : IComparable
    {
        #region class variables

        public Guid Id { get; private set; }
        private SPFeatureScope _Scope = SPFeatureScope.ScopeInvalid;
        public SPFeatureScope Scope
        {
            get { return _Scope; }
            set
            {
                _Scope = value;
                ScopeAbbrev = ScopeAbbrevConverter.ScopeToAbbrev(value);
            }
        }
        public string ScopeAbbrev { get; private set; } // more legible scope names -- see ScopeAbbrevConverter
        public int CompatibilityLevel { get; set; }
        public string Name { get; set; }

        String _exceptionMsg = "";
        public String ExceptionMsg
        {
            get { return _exceptionMsg; }
            set { _exceptionMsg = value; }
        }
        #endregion

        public Feature(Guid id)
        {
            this.Id = id;
            this.Scope = SPFeatureScope.ScopeInvalid;
        }

        public Feature(Guid id, SPFeatureScope scope)
        {
            this.Id = id;
            this.Scope = scope;
        }

        /// <summary>overwrite method for ToString - defines, what is shown of a Feature Class as string</summary>
        /// <returns>Feature Information string with scope, name and Guid</returns>
        public override string ToString()
        {
            string idstr = "";
            // ID (with compatibility level if available for this SharePoint version)
            if (this.CompatibilityLevel == FeatureManager.COMPATINAPPLICABLE)
            {
                idstr = String.Format("{0}", this.Id);
            }
            else
            {
                idstr = String.Format("{1}/{0}", this.Id, this.CompatibilityLevel);
            }
            // Combine name & Id
            String result = string.Empty;
            if (String.IsNullOrEmpty(Name))
            {
                result = String.Format("ERROR READING FEATURE [{0}], Scope: {1}", idstr, this.Scope.ToString());
            }
            else
            {
                result = String.Format("{2}: '{1}' [{0}]", idstr, this.Name, this.Scope.ToString());
            }
            return result;
        }

        // Record any exceptions experienced reading feature or feature definitions
        public void AppendExceptionMsg(Exception exc)
        {
            if (ExceptionMsg != "") ExceptionMsg += "; ";
            ExceptionMsg += ExceptionSerializer.ToString(exc);
        }

        // sort the features: 
        // First scope (Farm, Web App, Site then Web)
        // Then name alphabetically
        // Then Id
        // Then CompatibilityLevel
        // Note: Need to have a fixed stable sort, so compare everything including Ids & CompatLevel
        public int CompareTo(object obj)
        {
            if (!(obj is Feature))
            {
                throw (new System.ArgumentException("Object is not a Feature like the instance"));
            }
            Feature other = (Feature)obj;

            int cmp = this.Scope.CompareTo(other.Scope);
            if (cmp != 0)
            {
                return cmp;
            }

            cmp = string.Compare(this.Name, other.Name);
            if (cmp != 0)
            {
                return cmp;
            }
            cmp = this.Id.CompareTo(other.Id);
            if (cmp != 0)
            {
                return cmp;
            }
            cmp = this.CompatibilityLevel.CompareTo(other.CompatibilityLevel);
            return cmp;
        }
    }
}
