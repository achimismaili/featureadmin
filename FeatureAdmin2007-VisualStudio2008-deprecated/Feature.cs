using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.SharePoint;


using System.Windows.Forms;


namespace FeatureAdmin
{
    public class Feature : IComparable
    {
        public const int COMPATINAPPLICABLE = -8;
        public const int COMPATUNKNOWN = -6;

        #region class variables

        public Guid Id { get; private set; }
        public SPFeatureScope Scope { get; set; }
        public int CompatibilityLevel { get; set; }
        public String Name { get; set; }
        public bool Faulty { get; set; }
        public String ExceptionMsg { get; set; }
        public int? Activations { get; set; }

        #endregion

        public Feature(Guid id)
        {
            this.Id = id;
            this.Scope = SPFeatureScope.ScopeInvalid;
            this.CompatibilityLevel = COMPATUNKNOWN;
        }

        public Feature(Guid id, SPFeatureScope scope)
        {
            this.Id = id;
            this.Scope = scope;
            this.CompatibilityLevel = COMPATUNKNOWN;
        }


        /// <summary>overwrite method for ToString - defines, what is shown of a Feature Class as string</summary>
        /// <returns>Feature Information string with scope, name and Guid</returns>
        public override string ToString()
        {
            string idstr = "";
            // ID (with compatibility level if available for this SharePoint version)
            if (this.CompatibilityLevel == COMPATINAPPLICABLE)
            {
                idstr = String.Format("{0}", this.Id);
            }
            else
            {
                idstr = String.Format("{1}/{0}", this.Id, this.CompatibilityLevel);
            }
            // Combine name & Id
            String result = string.Empty;
            if (String.IsNullOrEmpty(this.Name))
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
            Faulty = true;
        }

        // sort the features: first Farm, Web App, Site then Web, after this, alphabetically after the name
        public int CompareTo(object obj)
        {
            if (obj is Feature)
            {
                Feature other = (Feature)obj;

                int iVal = 0;
                int oVal = 0;

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
            else
            {
                throw (new System.ArgumentException("Object is not a Feature like the instance"));
            }

        }
    }
}
