namespace FeatureAdmin.Core.Messages.Completed
{
    public class SettingsChanged
    {
        public SettingsChanged(bool elevatedPrivileges, bool force)
        {
            ElevatedPrivileges = elevatedPrivileges;
            Force = force;
        }

        public bool ElevatedPrivileges { get; }

        public bool Force { get; }
    }
}
