namespace FeatureAdmin.Core.Models.Enums
{
    /// <summary>
    /// Lock state for site collections
    /// </summary>
    /// <remarks>
    /// see also https://docs.microsoft.com/en-us/sharepoint/sites/manage-the-lock-status-for-site-collections
    /// </remarks>
    public enum LockState
    {

        // Unlocks the site collection and makes it available to users. 
        // PowerShell Command: 'Unlock' 
        NotLocked = 10,

        // Prevents users from adding new content to the site collection.Updates and deletions are still allowed.
        // PowerShell Command: 'NoAdditions'
        AddingContentPrevented = 20,

        // blocks additions, updates, and deletions
        // PowerShell Command: 'ReadOnly' 
        ReadOnly = 30,

        // Prevents users from accessing the site collection and its content. 
        // Users who attempt to access the site receive an error page that informs the user that the website declined to show the webpage. 
        // PowerShell Command: 'NoAccess' 
        NoAccess = 40
    }
}
