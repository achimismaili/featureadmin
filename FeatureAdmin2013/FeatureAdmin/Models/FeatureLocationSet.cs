using System;
using System.Collections.Generic;
using System.Text;

namespace FeatureAdmin.Models
{
    public class FeatureLocationSet : Dictionary<Feature, List<FeatureParent>>
    {
        private int _LocationCount = -1;
        public int GetTotalLocationCount()
        {
            if (_LocationCount == -1)
            {
                _LocationCount = 0;
                foreach (List<FeatureParent> list in this.Values)
                {
                    _LocationCount += list.Count;
                }
            }
            return _LocationCount;
        }
    }
}
