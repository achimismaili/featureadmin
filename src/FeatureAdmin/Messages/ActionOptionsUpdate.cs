namespace FeatureAdmin.Messages
{
    public class ActionOptionsUpdate
    {
        public ActionOptionsUpdate(bool canActivate, bool canDeactivate)
        {
            CanActivate = canActivate;
            CanDeactivate = canDeactivate;
        }

        public bool CanActivate { get; private set; }
        public bool CanDeactivate { get; private set; }
    }
}
