using System;
using System.Collections.Generic;
using System.Text;

namespace FeatureAdmin
{
    public class FeatureLocationSet : Dictionary<Feature, List<Location>>
    {
        private int _LocationCount = -1;
        public int GetTotalLocationCount()
        {
            if (_LocationCount == -1)
            {
                _LocationCount = 0;
                foreach (List<Location> list in this.Values)
                {
                    _LocationCount += list.Count;
                }
            }
            return _LocationCount;
        }
    }
}
