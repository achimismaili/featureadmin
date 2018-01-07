using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeatureAdmin.Core.Models
{
    public class ProgressModule
    {
        public double Quota;

        public double MaxCumulatedQuota;

        public int Total;

        public int Processed;

        public double OuotaPercentage { get {
                return Processed / Total * Quota;
            } }

        /// <summary>
        /// Tracks the progress for one part of all progress parts
        /// </summary>
        /// <param name="quota">the part of this module of all parts</param>
        /// <param name="previousMaxCumulatedQuota">maximum progress of all previous parts excluding this part</param>
        /// <param name="total">number of all processed items of this module, if known at creation time of this module</param>
        public ProgressModule(double quota, double previousMaxCumulatedQuota, int total = 0)
        {
            Quota = quota;
            MaxCumulatedQuota = previousMaxCumulatedQuota + quota;
            Total = total;
        }
    }
}
