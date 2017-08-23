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

        // sort the features: first Farm, Web App, Site then Web, after this, alphabetically after the name
        public int CompareTo(object obj)
        {
            if (obj is Feature)
            {
                int iVal = 0;
                int oVal = 0;

                switch (this.Scope)
                {
                    case SPFeatureScope.Farm:
                        iVal += 100; break;
                    case SPFeatureScope.WebApplication:
                        iVal += 200; break;
                    case SPFeatureScope.Site:
                        iVal += 300; break;
                    case SPFeatureScope.Web:
                        iVal += 400; break;
                    default:
                        iVal += 500; break;
                }

                switch (((Feature)obj).Scope)
                {
                    case SPFeatureScope.Farm:
                        oVal += 100; break;
                    case SPFeatureScope.WebApplication:
                        oVal += 200; break;
                    case SPFeatureScope.Site:
                        oVal += 300; break;
                    case SPFeatureScope.Web:
                        oVal += 400; break;
                    default:
                        oVal += 500; break;

                }

                if (this.Name != null)
                {
                    iVal += this.Name.CompareTo(((Feature)obj).Name);
                }

                // if iVal is higher than oVal in value, it will be far down in the list ...
                return (iVal - oVal);
            }
            else
                throw (new System.ArgumentException("Object is not a Feature like the instance"));

        }
    }
}
