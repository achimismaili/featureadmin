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

        Guid _id = Guid.Empty;
        public Guid Id
        {
            get { return _id; }
        }

        String _name = string.Empty;
        public String Name
        {
            get { return _name; }
            set { _name = value; }
        }

        int _compatibilityLevel;
        public int CompatibilityLevel
        {
            get { return _compatibilityLevel; }
            set { _compatibilityLevel = value; }
        }

        SPFeatureScope _scope = SPFeatureScope.ScopeInvalid;
        public SPFeatureScope Scope
        {
            get { return _scope; }
            set { _scope = value; }
        }

        String _exceptionMsg = "";
        public String ExceptionMsg
        {
            get { return _exceptionMsg; }
            set { _exceptionMsg = value; }
        }


        public Feature(Guid id)
        {
            this._id = id;
        }

        public Feature(Guid id, SPFeatureScope scope)
        {
            this._id = id;
            this._scope = scope;
        }

#endregion

        /// <summary>overwrite method for ToString - defines, what is shown of a Feature Class as string</summary>
        /// <returns>Feature Information string with scope, name and Guid</returns>
        public override string ToString()
        {
            String result = string.Empty;

            string idstr = String.Format("{1}/{0}", this._id, this._compatibilityLevel);
            if (this._compatibilityLevel == FeatureManager.COMPATINAPPLICABLE)
            {
                idstr = String.Format("{0}", this._id);
            }
            if (String.IsNullOrEmpty(_name))
            {
                result = String.Format("ERROR READING FEATURE [{0}], Scope: {1}", idstr, this._scope.ToString());
            }
            else
            {
                result = String.Format("{2}: '{1}' [{0}]", idstr, this._name, this._scope.ToString());
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
