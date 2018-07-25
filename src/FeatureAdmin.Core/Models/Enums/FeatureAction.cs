namespace FeatureAdmin.Core.Models.Enums
{
    public enum FeatureAction
    {
        Activate = 10,
        Deactivate = 30,
        CleanUp = 35, // is basically just a deactivation with always force enabled, 
                         // only needed for confirmation and task setup
        Upgrade = 50
    }
}
