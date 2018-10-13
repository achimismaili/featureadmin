namespace FeatureAdmin.Core.Models
{
    public class ProgressModule
    {
        public bool Completed
        {
            get
            {
                if (Total == 0)
                {
                    return Processed > 0;
                }
                else
                {
                    return Processed / Total >= 1; 
                }
            }
        }

        public double Quota;

        public double MaxCumulatedQuota;

        public int Total;

        public int Processed;

        public double OuotaPercentage
        {
            get
            {
                var totalNormalized = Processed > Total ? Processed : Total;

                if (totalNormalized == 0)
                {
                    return 0d;
                }
                else
                {
                    return (double)Processed / totalNormalized * Quota;
                }
            }
        }

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
