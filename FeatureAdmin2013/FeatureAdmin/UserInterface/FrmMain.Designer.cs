namespace FeatureAdmin.UserInterface
{
    partial class FrmMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>Clean up any resources being used.</summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.txtResult = new System.Windows.Forms.TextBox();
            this.lblSPSiteFeatures = new System.Windows.Forms.Label();
            this.clbSPSiteFeatures = new System.Windows.Forms.CheckedListBox();
            this.lblSPWebFeatures = new System.Windows.Forms.Label();
            this.clbSPWebFeatures = new System.Windows.Forms.CheckedListBox();
            this.btnRemoveFromWeb = new System.Windows.Forms.Button();
            this.lblLog = new System.Windows.Forms.Label();
            this.btnRefreshAllContent = new System.Windows.Forms.Button();
            this.lblSiteCollections = new System.Windows.Forms.Label();
            this.lblWebApps = new System.Windows.Forms.Label();
            this.lblWebs = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lblFeatureDefinitions = new System.Windows.Forms.Label();
            this.btnRemoveFromSiteCollection = new System.Windows.Forms.Button();
            this.btnRemoveFromWebApp = new System.Windows.Forms.Button();
            this.btnRemoveFromFarm = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.FarmFeatures = new System.Windows.Forms.TabPage();
            this.useForce = new System.Windows.Forms.CheckBox();
            this.FarmActionPanel = new System.Windows.Forms.Panel();
            this.btnUninstFDef = new System.Windows.Forms.Button();
            this.btnDeactivateSPFarm = new System.Windows.Forms.Button();
            this.ActionFarmCaption = new System.Windows.Forms.Label();
            this.btnActivateSPFarm = new System.Windows.Forms.Button();
            this.WebAppActionPanel = new System.Windows.Forms.Panel();
            this.ActionWebAppCaption = new System.Windows.Forms.Label();
            this.btnDeactivateSPWebApp = new System.Windows.Forms.Button();
            this.btnActivateSPWebApp = new System.Windows.Forms.Button();
            this.SiteCollectionActionPanel = new System.Windows.Forms.Panel();
            this.btnDeactivateSPSite = new System.Windows.Forms.Button();
            this.ActionSiteCaption = new System.Windows.Forms.Label();
            this.btnActivateSPSite = new System.Windows.Forms.Button();
            this.WebActionPanel = new System.Windows.Forms.Panel();
            this.btnDeactivateSPWeb = new System.Windows.Forms.Button();
            this.ActionWebCaption = new System.Windows.Forms.Label();
            this.btnActivateSPWeb = new System.Windows.Forms.Button();
            this.btnViewActivations = new System.Windows.Forms.Button();
            this.gridFeatureDefinitions = new System.Windows.Forms.DataGridView();
            this.RemoveFeatures = new System.Windows.Forms.TabPage();
            this.splitContainerRightSiteCollFeaturesWebFeatures = new System.Windows.Forms.SplitContainer();
            this.splitContainerCompleteMainframe = new System.Windows.Forms.SplitContainer();
            this.splitContainerLeftWindow = new System.Windows.Forms.SplitContainer();
            this.splitContainerLeftUpperWebAppAndSiteColl = new System.Windows.Forms.SplitContainer();
            this.gridWebApplications = new System.Windows.Forms.DataGridView();
            this.gridSiteCollections = new System.Windows.Forms.DataGridView();
            this.splitContainerLeftDownWebsAndLogs = new System.Windows.Forms.SplitContainer();
            this.gridWebs = new System.Windows.Forms.DataGridView();
            this.btnClearLog = new System.Windows.Forms.Button();
            this.all = new System.Windows.Forms.RadioButton();
            this.featureFilterBox = new System.Windows.Forms.GroupBox();
            this.farm = new System.Windows.Forms.RadioButton();
            this.webApp = new System.Windows.Forms.RadioButton();
            this.siCo = new System.Windows.Forms.RadioButton();
            this.web = new System.Windows.Forms.RadioButton();
            this.faulty = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.btnUpgrade = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.tabControl1.SuspendLayout();
            this.FarmFeatures.SuspendLayout();
            this.FarmActionPanel.SuspendLayout();
            this.WebAppActionPanel.SuspendLayout();
            this.SiteCollectionActionPanel.SuspendLayout();
            this.WebActionPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridFeatureDefinitions)).BeginInit();
            this.RemoveFeatures.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerRightSiteCollFeaturesWebFeatures)).BeginInit();
            this.splitContainerRightSiteCollFeaturesWebFeatures.Panel1.SuspendLayout();
            this.splitContainerRightSiteCollFeaturesWebFeatures.Panel2.SuspendLayout();
            this.splitContainerRightSiteCollFeaturesWebFeatures.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerCompleteMainframe)).BeginInit();
            this.splitContainerCompleteMainframe.Panel1.SuspendLayout();
            this.splitContainerCompleteMainframe.Panel2.SuspendLayout();
            this.splitContainerCompleteMainframe.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerLeftWindow)).BeginInit();
            this.splitContainerLeftWindow.Panel1.SuspendLayout();
            this.splitContainerLeftWindow.Panel2.SuspendLayout();
            this.splitContainerLeftWindow.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerLeftUpperWebAppAndSiteColl)).BeginInit();
            this.splitContainerLeftUpperWebAppAndSiteColl.Panel1.SuspendLayout();
            this.splitContainerLeftUpperWebAppAndSiteColl.Panel2.SuspendLayout();
            this.splitContainerLeftUpperWebAppAndSiteColl.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridWebApplications)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridSiteCollections)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerLeftDownWebsAndLogs)).BeginInit();
            this.splitContainerLeftDownWebsAndLogs.Panel1.SuspendLayout();
            this.splitContainerLeftDownWebsAndLogs.Panel2.SuspendLayout();
            this.splitContainerLeftDownWebsAndLogs.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridWebs)).BeginInit();
            this.featureFilterBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtResult
            // 
            this.txtResult.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtResult.Location = new System.Drawing.Point(0, 31);
            this.txtResult.Multiline = true;
            this.txtResult.Name = "txtResult";
            this.txtResult.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtResult.Size = new System.Drawing.Size(423, 313);
            this.txtResult.TabIndex = 2;
            this.txtResult.WordWrap = false;
            // 
            // lblSPSiteFeatures
            // 
            this.lblSPSiteFeatures.AutoSize = true;
            this.lblSPSiteFeatures.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lblSPSiteFeatures.Location = new System.Drawing.Point(7, 0);
            this.lblSPSiteFeatures.Name = "lblSPSiteFeatures";
            this.lblSPSiteFeatures.Size = new System.Drawing.Size(159, 13);
            this.lblSPSiteFeatures.TabIndex = 9;
            this.lblSPSiteFeatures.Text = "Site Collection Features (SPSite)";
            // 
            // clbSPSiteFeatures
            // 
            this.clbSPSiteFeatures.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.clbSPSiteFeatures.BackColor = System.Drawing.Color.Moccasin;
            this.clbSPSiteFeatures.CheckOnClick = true;
            this.clbSPSiteFeatures.FormattingEnabled = true;
            this.clbSPSiteFeatures.Location = new System.Drawing.Point(10, 16);
            this.clbSPSiteFeatures.Name = "clbSPSiteFeatures";
            this.clbSPSiteFeatures.Size = new System.Drawing.Size(436, 214);
            this.clbSPSiteFeatures.TabIndex = 8;
            this.clbSPSiteFeatures.SelectedIndexChanged += new System.EventHandler(this.clbSPSiteFeatures_SelectedIndexChanged);
            // 
            // lblSPWebFeatures
            // 
            this.lblSPWebFeatures.AutoSize = true;
            this.lblSPWebFeatures.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lblSPWebFeatures.Location = new System.Drawing.Point(7, -1);
            this.lblSPWebFeatures.Name = "lblSPWebFeatures";
            this.lblSPWebFeatures.Size = new System.Drawing.Size(115, 13);
            this.lblSPWebFeatures.TabIndex = 8;
            this.lblSPWebFeatures.Text = "Site Features (SPWeb)";
            // 
            // clbSPWebFeatures
            // 
            this.clbSPWebFeatures.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.clbSPWebFeatures.BackColor = System.Drawing.Color.Moccasin;
            this.clbSPWebFeatures.CheckOnClick = true;
            this.clbSPWebFeatures.FormattingEnabled = true;
            this.clbSPWebFeatures.Location = new System.Drawing.Point(10, 15);
            this.clbSPWebFeatures.Name = "clbSPWebFeatures";
            this.clbSPWebFeatures.Size = new System.Drawing.Size(436, 214);
            this.clbSPWebFeatures.TabIndex = 7;
            this.clbSPWebFeatures.SelectedIndexChanged += new System.EventHandler(this.clbSPWebFeatures_SelectedIndexChanged);
            // 
            // btnRemoveFromWeb
            // 
            this.btnRemoveFromWeb.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnRemoveFromWeb.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btnRemoveFromWeb.Location = new System.Drawing.Point(6, 490);
            this.btnRemoveFromWeb.Name = "btnRemoveFromWeb";
            this.btnRemoveFromWeb.Size = new System.Drawing.Size(200, 23);
            this.btnRemoveFromWeb.TabIndex = 8;
            this.btnRemoveFromWeb.Text = "Remove from selected Site (SPweb)";
            this.btnRemoveFromWeb.UseVisualStyleBackColor = true;
            this.btnRemoveFromWeb.Click += new System.EventHandler(this.btnRemoveFromWeb_Click);
            // 
            // lblLog
            // 
            this.lblLog.AutoSize = true;
            this.lblLog.Location = new System.Drawing.Point(3, 0);
            this.lblLog.Name = "lblLog";
            this.lblLog.Size = new System.Drawing.Size(25, 13);
            this.lblLog.TabIndex = 10;
            this.lblLog.Text = "Log";
            // 
            // btnRefreshAllContent
            // 
            this.btnRefreshAllContent.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRefreshAllContent.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRefreshAllContent.Location = new System.Drawing.Point(135, 3);
            this.btnRefreshAllContent.Name = "btnRefreshAllContent";
            this.btnRefreshAllContent.Size = new System.Drawing.Size(95, 41);
            this.btnRefreshAllContent.TabIndex = 12;
            this.btnRefreshAllContent.Text = "Refresh \r\nall content";
            this.btnRefreshAllContent.UseVisualStyleBackColor = true;
            this.btnRefreshAllContent.Click += new System.EventHandler(this.btnRefreshAllContent_Click);
            // 
            // lblSiteCollections
            // 
            this.lblSiteCollections.AutoSize = true;
            this.lblSiteCollections.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblSiteCollections.Location = new System.Drawing.Point(0, 0);
            this.lblSiteCollections.Name = "lblSiteCollections";
            this.lblSiteCollections.Size = new System.Drawing.Size(233, 13);
            this.lblSiteCollections.TabIndex = 8;
            this.lblSiteCollections.Text = "Site Collections within selected Web Application";
            // 
            // lblWebApps
            // 
            this.lblWebApps.AutoSize = true;
            this.lblWebApps.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblWebApps.Location = new System.Drawing.Point(3, 4);
            this.lblWebApps.Name = "lblWebApps";
            this.lblWebApps.Size = new System.Drawing.Size(90, 13);
            this.lblWebApps.TabIndex = 9;
            this.lblWebApps.Text = "Web Applications";
            // 
            // lblWebs
            // 
            this.lblWebs.AutoSize = true;
            this.lblWebs.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblWebs.Location = new System.Drawing.Point(3, 0);
            this.lblWebs.Name = "lblWebs";
            this.lblWebs.Size = new System.Drawing.Size(196, 13);
            this.lblWebs.TabIndex = 8;
            this.lblWebs.Text = "Web Sites within selected SiteCollection";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(290, 6);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(156, 13);
            this.label4.TabIndex = 17;
            this.label4.Text = "(Re) Load the list of all Features";
            // 
            // lblFeatureDefinitions
            // 
            this.lblFeatureDefinitions.AutoSize = true;
            this.lblFeatureDefinitions.ForeColor = System.Drawing.Color.Black;
            this.lblFeatureDefinitions.Location = new System.Drawing.Point(6, 16);
            this.lblFeatureDefinitions.Name = "lblFeatureDefinitions";
            this.lblFeatureDefinitions.Size = new System.Drawing.Size(158, 13);
            this.lblFeatureDefinitions.TabIndex = 9;
            this.lblFeatureDefinitions.Text = "All Features installed in the Farm";
            // 
            // btnRemoveFromSiteCollection
            // 
            this.btnRemoveFromSiteCollection.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnRemoveFromSiteCollection.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btnRemoveFromSiteCollection.Location = new System.Drawing.Point(6, 519);
            this.btnRemoveFromSiteCollection.Name = "btnRemoveFromSiteCollection";
            this.btnRemoveFromSiteCollection.Size = new System.Drawing.Size(200, 23);
            this.btnRemoveFromSiteCollection.TabIndex = 8;
            this.btnRemoveFromSiteCollection.Text = "Remove from selected SiteCollection";
            this.btnRemoveFromSiteCollection.UseVisualStyleBackColor = true;
            this.btnRemoveFromSiteCollection.Click += new System.EventHandler(this.btnRemoveFromSiteCollection_Click);
            // 
            // btnRemoveFromWebApp
            // 
            this.btnRemoveFromWebApp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnRemoveFromWebApp.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btnRemoveFromWebApp.Location = new System.Drawing.Point(6, 548);
            this.btnRemoveFromWebApp.Name = "btnRemoveFromWebApp";
            this.btnRemoveFromWebApp.Size = new System.Drawing.Size(200, 23);
            this.btnRemoveFromWebApp.TabIndex = 8;
            this.btnRemoveFromWebApp.Text = "Remove from selected Web App";
            this.btnRemoveFromWebApp.UseVisualStyleBackColor = true;
            this.btnRemoveFromWebApp.Click += new System.EventHandler(this.btnRemoveFromWebApp_Click);
            // 
            // btnRemoveFromFarm
            // 
            this.btnRemoveFromFarm.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnRemoveFromFarm.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btnRemoveFromFarm.Location = new System.Drawing.Point(6, 577);
            this.btnRemoveFromFarm.Name = "btnRemoveFromFarm";
            this.btnRemoveFromFarm.Size = new System.Drawing.Size(200, 23);
            this.btnRemoveFromFarm.TabIndex = 8;
            this.btnRemoveFromFarm.Text = "Remove Feature from Farm";
            this.btnRemoveFromFarm.UseVisualStyleBackColor = true;
            this.btnRemoveFromFarm.Click += new System.EventHandler(this.btnRemoveFromFarm_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.FarmFeatures);
            this.tabControl1.Controls.Add(this.RemoveFeatures);
            this.tabControl1.Location = new System.Drawing.Point(-4, 50);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(768, 672);
            this.tabControl1.TabIndex = 17;
            // 
            // FarmFeatures
            // 
            this.FarmFeatures.Controls.Add(this.btnUpgrade);
            this.FarmFeatures.Controls.Add(this.label1);
            this.FarmFeatures.Controls.Add(this.WebActionPanel);
            this.FarmFeatures.Controls.Add(this.btnUninstFDef);
            this.FarmFeatures.Controls.Add(this.gridFeatureDefinitions);
            this.FarmFeatures.Controls.Add(this.label4);
            this.FarmFeatures.Controls.Add(this.btnViewActivations);
            this.FarmFeatures.Controls.Add(this.lblFeatureDefinitions);
            this.FarmFeatures.Controls.Add(this.SiteCollectionActionPanel);
            this.FarmFeatures.Controls.Add(this.FarmActionPanel);
            this.FarmFeatures.Controls.Add(this.WebAppActionPanel);
            this.FarmFeatures.Controls.Add(this.panel1);
            this.FarmFeatures.Location = new System.Drawing.Point(4, 22);
            this.FarmFeatures.Name = "FarmFeatures";
            this.FarmFeatures.Padding = new System.Windows.Forms.Padding(3);
            this.FarmFeatures.Size = new System.Drawing.Size(760, 646);
            this.FarmFeatures.TabIndex = 1;
            this.FarmFeatures.Text = "Farm Feature Administration";
            this.FarmFeatures.UseVisualStyleBackColor = true;
            // 
            // useForce
            // 
            this.useForce.AutoSize = true;
            this.useForce.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.useForce.Location = new System.Drawing.Point(236, 7);
            this.useForce.Name = "useForce";
            this.useForce.Size = new System.Drawing.Size(134, 30);
            this.useForce.TabIndex = 30;
            this.useForce.Text = "Always use FORCE\r\non changes";
            this.useForce.UseVisualStyleBackColor = true;
            this.useForce.CheckedChanged += new System.EventHandler(this.useForce_CheckedChanged);
            // 
            // FarmActionPanel
            // 
            this.FarmActionPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.FarmActionPanel.Controls.Add(this.btnDeactivateSPFarm);
            this.FarmActionPanel.Controls.Add(this.ActionFarmCaption);
            this.FarmActionPanel.Controls.Add(this.btnActivateSPFarm);
            this.FarmActionPanel.Location = new System.Drawing.Point(-1, 549);
            this.FarmActionPanel.Name = "FarmActionPanel";
            this.FarmActionPanel.Size = new System.Drawing.Size(92, 97);
            this.FarmActionPanel.TabIndex = 29;
            // 
            // btnUninstFDef
            // 
            this.btnUninstFDef.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnUninstFDef.Location = new System.Drawing.Point(6, 520);
            this.btnUninstFDef.Name = "btnUninstFDef";
            this.btnUninstFDef.Size = new System.Drawing.Size(75, 20);
            this.btnUninstFDef.TabIndex = 3;
            this.btnUninstFDef.Text = "Uninstall";
            this.btnUninstFDef.UseVisualStyleBackColor = true;
            this.btnUninstFDef.Click += new System.EventHandler(this.btnUninstFDef_Click);
            // 
            // btnDeactivateSPFarm
            // 
            this.btnDeactivateSPFarm.Location = new System.Drawing.Point(6, 34);
            this.btnDeactivateSPFarm.Name = "btnDeactivateSPFarm";
            this.btnDeactivateSPFarm.Size = new System.Drawing.Size(76, 20);
            this.btnDeactivateSPFarm.TabIndex = 2;
            this.btnDeactivateSPFarm.Text = "Deactivate";
            this.btnDeactivateSPFarm.UseVisualStyleBackColor = true;
            this.btnDeactivateSPFarm.Click += new System.EventHandler(this.btnDeactivateSPFarm_Click);
            // 
            // ActionFarmCaption
            // 
            this.ActionFarmCaption.AutoSize = true;
            this.ActionFarmCaption.Location = new System.Drawing.Point(13, 57);
            this.ActionFarmCaption.Name = "ActionFarmCaption";
            this.ActionFarmCaption.Size = new System.Drawing.Size(60, 26);
            this.ActionFarmCaption.TabIndex = 1;
            this.ActionFarmCaption.Text = "Across\r\nEntire Farm";
            this.ActionFarmCaption.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnActivateSPFarm
            // 
            this.btnActivateSPFarm.Location = new System.Drawing.Point(6, 8);
            this.btnActivateSPFarm.Name = "btnActivateSPFarm";
            this.btnActivateSPFarm.Size = new System.Drawing.Size(76, 20);
            this.btnActivateSPFarm.TabIndex = 0;
            this.btnActivateSPFarm.Text = "Activate";
            this.btnActivateSPFarm.UseVisualStyleBackColor = true;
            this.btnActivateSPFarm.Click += new System.EventHandler(this.btnActivateSPFarm_Click);
            // 
            // WebAppActionPanel
            // 
            this.WebAppActionPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.WebAppActionPanel.Controls.Add(this.ActionWebAppCaption);
            this.WebAppActionPanel.Controls.Add(this.btnDeactivateSPWebApp);
            this.WebAppActionPanel.Controls.Add(this.btnActivateSPWebApp);
            this.WebAppActionPanel.Location = new System.Drawing.Point(99, 549);
            this.WebAppActionPanel.Name = "WebAppActionPanel";
            this.WebAppActionPanel.Size = new System.Drawing.Size(93, 97);
            this.WebAppActionPanel.TabIndex = 28;
            // 
            // ActionWebAppCaption
            // 
            this.ActionWebAppCaption.AutoSize = true;
            this.ActionWebAppCaption.Location = new System.Drawing.Point(3, 57);
            this.ActionWebAppCaption.Name = "ActionWebAppCaption";
            this.ActionWebAppCaption.Size = new System.Drawing.Size(85, 26);
            this.ActionWebAppCaption.TabIndex = 3;
            this.ActionWebAppCaption.Text = "Across Selected\r\nWeb Application";
            this.ActionWebAppCaption.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnDeactivateSPWebApp
            // 
            this.btnDeactivateSPWebApp.Location = new System.Drawing.Point(6, 34);
            this.btnDeactivateSPWebApp.Name = "btnDeactivateSPWebApp";
            this.btnDeactivateSPWebApp.Size = new System.Drawing.Size(76, 20);
            this.btnDeactivateSPWebApp.TabIndex = 1;
            this.btnDeactivateSPWebApp.Text = "Deactivate";
            this.btnDeactivateSPWebApp.UseVisualStyleBackColor = true;
            this.btnDeactivateSPWebApp.Click += new System.EventHandler(this.btnDeactivateSPWebApp_Click);
            // 
            // btnActivateSPWebApp
            // 
            this.btnActivateSPWebApp.Location = new System.Drawing.Point(6, 8);
            this.btnActivateSPWebApp.Name = "btnActivateSPWebApp";
            this.btnActivateSPWebApp.Size = new System.Drawing.Size(76, 20);
            this.btnActivateSPWebApp.TabIndex = 0;
            this.btnActivateSPWebApp.Text = "Activate";
            this.btnActivateSPWebApp.UseVisualStyleBackColor = true;
            this.btnActivateSPWebApp.Click += new System.EventHandler(this.btnActivateSPWebApp_Click);
            // 
            // SiteCollectionActionPanel
            // 
            this.SiteCollectionActionPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.SiteCollectionActionPanel.Controls.Add(this.btnDeactivateSPSite);
            this.SiteCollectionActionPanel.Controls.Add(this.ActionSiteCaption);
            this.SiteCollectionActionPanel.Controls.Add(this.btnActivateSPSite);
            this.SiteCollectionActionPanel.Location = new System.Drawing.Point(198, 549);
            this.SiteCollectionActionPanel.Name = "SiteCollectionActionPanel";
            this.SiteCollectionActionPanel.Size = new System.Drawing.Size(97, 104);
            this.SiteCollectionActionPanel.TabIndex = 27;
            // 
            // btnDeactivateSPSite
            // 
            this.btnDeactivateSPSite.Location = new System.Drawing.Point(6, 34);
            this.btnDeactivateSPSite.Name = "btnDeactivateSPSite";
            this.btnDeactivateSPSite.Size = new System.Drawing.Size(76, 20);
            this.btnDeactivateSPSite.TabIndex = 2;
            this.btnDeactivateSPSite.Text = "Deactivate";
            this.btnDeactivateSPSite.UseVisualStyleBackColor = true;
            this.btnDeactivateSPSite.Click += new System.EventHandler(this.btnDeactivateSPSite_Click);
            // 
            // ActionSiteCaption
            // 
            this.ActionSiteCaption.AutoSize = true;
            this.ActionSiteCaption.Location = new System.Drawing.Point(3, 57);
            this.ActionSiteCaption.Name = "ActionSiteCaption";
            this.ActionSiteCaption.Size = new System.Drawing.Size(84, 26);
            this.ActionSiteCaption.TabIndex = 1;
            this.ActionSiteCaption.Text = "Across Selected\r\nSite Collection";
            this.ActionSiteCaption.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnActivateSPSite
            // 
            this.btnActivateSPSite.Location = new System.Drawing.Point(6, 8);
            this.btnActivateSPSite.Name = "btnActivateSPSite";
            this.btnActivateSPSite.Size = new System.Drawing.Size(76, 20);
            this.btnActivateSPSite.TabIndex = 0;
            this.btnActivateSPSite.Text = "Activate";
            this.btnActivateSPSite.UseVisualStyleBackColor = true;
            this.btnActivateSPSite.Click += new System.EventHandler(this.btnActivateSPSite_Click);
            // 
            // WebActionPanel
            // 
            this.WebActionPanel.Controls.Add(this.btnDeactivateSPWeb);
            this.WebActionPanel.Controls.Add(this.ActionWebCaption);
            this.WebActionPanel.Controls.Add(this.btnActivateSPWeb);
            this.WebActionPanel.Location = new System.Drawing.Point(301, 549);
            this.WebActionPanel.Name = "WebActionPanel";
            this.WebActionPanel.Size = new System.Drawing.Size(93, 101);
            this.WebActionPanel.TabIndex = 26;
            // 
            // btnDeactivateSPWeb
            // 
            this.btnDeactivateSPWeb.Location = new System.Drawing.Point(6, 34);
            this.btnDeactivateSPWeb.Name = "btnDeactivateSPWeb";
            this.btnDeactivateSPWeb.Size = new System.Drawing.Size(76, 20);
            this.btnDeactivateSPWeb.TabIndex = 2;
            this.btnDeactivateSPWeb.Text = "Deactivate";
            this.btnDeactivateSPWeb.UseVisualStyleBackColor = true;
            this.btnDeactivateSPWeb.Click += new System.EventHandler(this.btnDeactivateSPWeb_Click);
            // 
            // ActionWebCaption
            // 
            this.ActionWebCaption.AutoSize = true;
            this.ActionWebCaption.Location = new System.Drawing.Point(12, 57);
            this.ActionWebCaption.Name = "ActionWebCaption";
            this.ActionWebCaption.Size = new System.Drawing.Size(61, 26);
            this.ActionWebCaption.TabIndex = 1;
            this.ActionWebCaption.Text = "In Selected\r\nWeb";
            this.ActionWebCaption.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.ActionWebCaption.Click += new System.EventHandler(this.ActionWebCaption_Click);
            // 
            // btnActivateSPWeb
            // 
            this.btnActivateSPWeb.Location = new System.Drawing.Point(6, 8);
            this.btnActivateSPWeb.Name = "btnActivateSPWeb";
            this.btnActivateSPWeb.Size = new System.Drawing.Size(76, 20);
            this.btnActivateSPWeb.TabIndex = 0;
            this.btnActivateSPWeb.Text = "Activate";
            this.btnActivateSPWeb.UseVisualStyleBackColor = true;
            this.btnActivateSPWeb.Click += new System.EventHandler(this.btnActivateSPWeb_Click);
            // 
            // btnViewActivations
            // 
            this.btnViewActivations.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnViewActivations.Location = new System.Drawing.Point(204, 520);
            this.btnViewActivations.Name = "btnViewActivations";
            this.btnViewActivations.Size = new System.Drawing.Size(179, 23);
            this.btnViewActivations.TabIndex = 25;
            this.btnViewActivations.Text = "Activation details";
            this.btnViewActivations.UseVisualStyleBackColor = true;
            this.btnViewActivations.Click += new System.EventHandler(this.btnViewActivations_Click);
            // 
            // gridFeatureDefinitions
            // 
            this.gridFeatureDefinitions.AllowUserToAddRows = false;
            this.gridFeatureDefinitions.AllowUserToDeleteRows = false;
            this.gridFeatureDefinitions.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gridFeatureDefinitions.BackgroundColor = System.Drawing.Color.LightSteelBlue;
            this.gridFeatureDefinitions.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridFeatureDefinitions.Location = new System.Drawing.Point(6, 31);
            this.gridFeatureDefinitions.Name = "gridFeatureDefinitions";
            this.gridFeatureDefinitions.ReadOnly = true;
            this.gridFeatureDefinitions.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gridFeatureDefinitions.Size = new System.Drawing.Size(754, 467);
            this.gridFeatureDefinitions.TabIndex = 24;
            this.gridFeatureDefinitions.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.gridFeatureDefinitions_CellMouseDown);
            this.gridFeatureDefinitions.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.gridFeatureDefinitions_DataBindingComplete);
            this.gridFeatureDefinitions.SelectionChanged += new System.EventHandler(this.gridFeatureDefinitions_SelectionChanged);
            // 
            // RemoveFeatures
            // 
            this.RemoveFeatures.Controls.Add(this.btnRemoveFromWeb);
            this.RemoveFeatures.Controls.Add(this.btnRemoveFromSiteCollection);
            this.RemoveFeatures.Controls.Add(this.btnRemoveFromWebApp);
            this.RemoveFeatures.Controls.Add(this.btnRemoveFromFarm);
            this.RemoveFeatures.Controls.Add(this.splitContainerRightSiteCollFeaturesWebFeatures);
            this.RemoveFeatures.ForeColor = System.Drawing.Color.DarkGoldenrod;
            this.RemoveFeatures.Location = new System.Drawing.Point(4, 22);
            this.RemoveFeatures.Name = "RemoveFeatures";
            this.RemoveFeatures.Padding = new System.Windows.Forms.Padding(3);
            this.RemoveFeatures.Size = new System.Drawing.Size(539, 693);
            this.RemoveFeatures.TabIndex = 0;
            this.RemoveFeatures.Text = "Remove / deactivate features in selected sites";
            this.RemoveFeatures.UseVisualStyleBackColor = true;
            // 
            // splitContainerRightSiteCollFeaturesWebFeatures
            // 
            this.splitContainerRightSiteCollFeaturesWebFeatures.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainerRightSiteCollFeaturesWebFeatures.Location = new System.Drawing.Point(-4, 0);
            this.splitContainerRightSiteCollFeaturesWebFeatures.Name = "splitContainerRightSiteCollFeaturesWebFeatures";
            this.splitContainerRightSiteCollFeaturesWebFeatures.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainerRightSiteCollFeaturesWebFeatures.Panel1
            // 
            this.splitContainerRightSiteCollFeaturesWebFeatures.Panel1.Controls.Add(this.clbSPSiteFeatures);
            this.splitContainerRightSiteCollFeaturesWebFeatures.Panel1.Controls.Add(this.lblSPSiteFeatures);
            // 
            // splitContainerRightSiteCollFeaturesWebFeatures.Panel2
            // 
            this.splitContainerRightSiteCollFeaturesWebFeatures.Panel2.Controls.Add(this.lblSPWebFeatures);
            this.splitContainerRightSiteCollFeaturesWebFeatures.Panel2.Controls.Add(this.clbSPWebFeatures);
            this.splitContainerRightSiteCollFeaturesWebFeatures.Size = new System.Drawing.Size(458, 484);
            this.splitContainerRightSiteCollFeaturesWebFeatures.SplitterDistance = 236;
            this.splitContainerRightSiteCollFeaturesWebFeatures.TabIndex = 19;
            // 
            // splitContainerCompleteMainframe
            // 
            this.splitContainerCompleteMainframe.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainerCompleteMainframe.Location = new System.Drawing.Point(2, 2);
            this.splitContainerCompleteMainframe.Name = "splitContainerCompleteMainframe";
            // 
            // splitContainerCompleteMainframe.Panel1
            // 
            this.splitContainerCompleteMainframe.Panel1.Controls.Add(this.splitContainerLeftWindow);
            // 
            // splitContainerCompleteMainframe.Panel2
            // 
            this.splitContainerCompleteMainframe.Panel2.Controls.Add(this.featureFilterBox);
            this.splitContainerCompleteMainframe.Panel2.Controls.Add(this.useForce);
            this.splitContainerCompleteMainframe.Panel2.Controls.Add(this.tabControl1);
            this.splitContainerCompleteMainframe.Panel2.Controls.Add(this.btnRefreshAllContent);
            this.splitContainerCompleteMainframe.Size = new System.Drawing.Size(1204, 728);
            this.splitContainerCompleteMainframe.SplitterDistance = 433;
            this.splitContainerCompleteMainframe.TabIndex = 19;
            // 
            // splitContainerLeftWindow
            // 
            this.splitContainerLeftWindow.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainerLeftWindow.Location = new System.Drawing.Point(0, 0);
            this.splitContainerLeftWindow.Name = "splitContainerLeftWindow";
            this.splitContainerLeftWindow.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainerLeftWindow.Panel1
            // 
            this.splitContainerLeftWindow.Panel1.Controls.Add(this.splitContainerLeftUpperWebAppAndSiteColl);
            // 
            // splitContainerLeftWindow.Panel2
            // 
            this.splitContainerLeftWindow.Panel2.Controls.Add(this.splitContainerLeftDownWebsAndLogs);
            this.splitContainerLeftWindow.Size = new System.Drawing.Size(430, 725);
            this.splitContainerLeftWindow.SplitterDistance = 220;
            this.splitContainerLeftWindow.TabIndex = 13;
            // 
            // splitContainerLeftUpperWebAppAndSiteColl
            // 
            this.splitContainerLeftUpperWebAppAndSiteColl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainerLeftUpperWebAppAndSiteColl.Location = new System.Drawing.Point(4, 3);
            this.splitContainerLeftUpperWebAppAndSiteColl.Name = "splitContainerLeftUpperWebAppAndSiteColl";
            this.splitContainerLeftUpperWebAppAndSiteColl.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainerLeftUpperWebAppAndSiteColl.Panel1
            // 
            this.splitContainerLeftUpperWebAppAndSiteColl.Panel1.Controls.Add(this.gridWebApplications);
            this.splitContainerLeftUpperWebAppAndSiteColl.Panel1.Controls.Add(this.lblWebApps);
            // 
            // splitContainerLeftUpperWebAppAndSiteColl.Panel2
            // 
            this.splitContainerLeftUpperWebAppAndSiteColl.Panel2.Controls.Add(this.gridSiteCollections);
            this.splitContainerLeftUpperWebAppAndSiteColl.Panel2.Controls.Add(this.lblSiteCollections);
            this.splitContainerLeftUpperWebAppAndSiteColl.Size = new System.Drawing.Size(423, 214);
            this.splitContainerLeftUpperWebAppAndSiteColl.SplitterDistance = 105;
            this.splitContainerLeftUpperWebAppAndSiteColl.TabIndex = 0;
            // 
            // gridWebApplications
            // 
            this.gridWebApplications.AllowUserToAddRows = false;
            this.gridWebApplications.AllowUserToDeleteRows = false;
            this.gridWebApplications.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gridWebApplications.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridWebApplications.Location = new System.Drawing.Point(0, 20);
            this.gridWebApplications.MultiSelect = false;
            this.gridWebApplications.Name = "gridWebApplications";
            this.gridWebApplications.ReadOnly = true;
            this.gridWebApplications.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gridWebApplications.Size = new System.Drawing.Size(423, 81);
            this.gridWebApplications.TabIndex = 13;
            this.gridWebApplications.SelectionChanged += new System.EventHandler(this.gridWebApplications_SelectionChanged);
            // 
            // gridSiteCollections
            // 
            this.gridSiteCollections.AllowUserToAddRows = false;
            this.gridSiteCollections.AllowUserToDeleteRows = false;
            this.gridSiteCollections.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gridSiteCollections.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridSiteCollections.Location = new System.Drawing.Point(0, 15);
            this.gridSiteCollections.Name = "gridSiteCollections";
            this.gridSiteCollections.ReadOnly = true;
            this.gridSiteCollections.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gridSiteCollections.Size = new System.Drawing.Size(423, 82);
            this.gridSiteCollections.TabIndex = 9;
            this.gridSiteCollections.SelectionChanged += new System.EventHandler(this.gridSiteCollections_SelectionChanged);
            // 
            // splitContainerLeftDownWebsAndLogs
            // 
            this.splitContainerLeftDownWebsAndLogs.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainerLeftDownWebsAndLogs.Location = new System.Drawing.Point(4, 4);
            this.splitContainerLeftDownWebsAndLogs.Name = "splitContainerLeftDownWebsAndLogs";
            this.splitContainerLeftDownWebsAndLogs.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainerLeftDownWebsAndLogs.Panel1
            // 
            this.splitContainerLeftDownWebsAndLogs.Panel1.Controls.Add(this.gridWebs);
            this.splitContainerLeftDownWebsAndLogs.Panel1.Controls.Add(this.lblWebs);
            // 
            // splitContainerLeftDownWebsAndLogs.Panel2
            // 
            this.splitContainerLeftDownWebsAndLogs.Panel2.Controls.Add(this.lblLog);
            this.splitContainerLeftDownWebsAndLogs.Panel2.Controls.Add(this.txtResult);
            this.splitContainerLeftDownWebsAndLogs.Panel2.Controls.Add(this.btnClearLog);
            this.splitContainerLeftDownWebsAndLogs.Size = new System.Drawing.Size(423, 494);
            this.splitContainerLeftDownWebsAndLogs.SplitterDistance = 146;
            this.splitContainerLeftDownWebsAndLogs.TabIndex = 0;
            // 
            // gridWebs
            // 
            this.gridWebs.AllowUserToAddRows = false;
            this.gridWebs.AllowUserToDeleteRows = false;
            this.gridWebs.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gridWebs.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridWebs.Location = new System.Drawing.Point(0, 15);
            this.gridWebs.Name = "gridWebs";
            this.gridWebs.ReadOnly = true;
            this.gridWebs.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gridWebs.Size = new System.Drawing.Size(423, 212);
            this.gridWebs.TabIndex = 9;
            this.gridWebs.SelectionChanged += new System.EventHandler(this.gridWebs_SelectionChanged);
            // 
            // btnClearLog
            // 
            this.btnClearLog.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClearLog.Location = new System.Drawing.Point(294, 3);
            this.btnClearLog.Name = "btnClearLog";
            this.btnClearLog.Size = new System.Drawing.Size(126, 23);
            this.btnClearLog.TabIndex = 19;
            this.btnClearLog.Text = "Clear Log";
            this.btnClearLog.UseVisualStyleBackColor = true;
            this.btnClearLog.Click += new System.EventHandler(this.btnClearLog_Click);
            // 
            // all
            // 
            this.all.AutoSize = true;
            this.all.Checked = true;
            this.all.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.all.Location = new System.Drawing.Point(17, 14);
            this.all.Name = "all";
            this.all.Size = new System.Drawing.Size(64, 17);
            this.all.TabIndex = 31;
            this.all.Text = "No Filter";
            this.all.UseVisualStyleBackColor = true;
            // 
            // featureFilterBox
            // 
            this.featureFilterBox.Controls.Add(this.faulty);
            this.featureFilterBox.Controls.Add(this.web);
            this.featureFilterBox.Controls.Add(this.siCo);
            this.featureFilterBox.Controls.Add(this.webApp);
            this.featureFilterBox.Controls.Add(this.farm);
            this.featureFilterBox.Controls.Add(this.all);
            this.featureFilterBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.featureFilterBox.Location = new System.Drawing.Point(369, 7);
            this.featureFilterBox.Name = "featureFilterBox";
            this.featureFilterBox.Size = new System.Drawing.Size(386, 37);
            this.featureFilterBox.TabIndex = 25;
            this.featureFilterBox.TabStop = false;
            this.featureFilterBox.Text = "Feature Filter";
            // 
            // farm
            // 
            this.farm.AutoSize = true;
            this.farm.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.farm.Location = new System.Drawing.Point(87, 14);
            this.farm.Name = "farm";
            this.farm.Size = new System.Drawing.Size(48, 17);
            this.farm.TabIndex = 32;
            this.farm.Text = "Farm";
            this.farm.UseVisualStyleBackColor = true;
            // 
            // webApp
            // 
            this.webApp.AutoSize = true;
            this.webApp.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.webApp.Location = new System.Drawing.Point(141, 14);
            this.webApp.Name = "webApp";
            this.webApp.Size = new System.Drawing.Size(70, 17);
            this.webApp.TabIndex = 33;
            this.webApp.Text = "Web App";
            this.webApp.UseVisualStyleBackColor = true;
            // 
            // siCo
            // 
            this.siCo.AutoSize = true;
            this.siCo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.siCo.Location = new System.Drawing.Point(217, 14);
            this.siCo.Name = "siCo";
            this.siCo.Size = new System.Drawing.Size(47, 17);
            this.siCo.TabIndex = 34;
            this.siCo.Text = "SiCo";
            this.siCo.UseVisualStyleBackColor = true;
            // 
            // web
            // 
            this.web.AutoSize = true;
            this.web.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.web.Location = new System.Drawing.Point(270, 14);
            this.web.Name = "web";
            this.web.Size = new System.Drawing.Size(48, 17);
            this.web.TabIndex = 35;
            this.web.Text = "Web";
            this.web.UseVisualStyleBackColor = true;
            // 
            // faulty
            // 
            this.faulty.AutoSize = true;
            this.faulty.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.faulty.Location = new System.Drawing.Point(324, 14);
            this.faulty.Name = "faulty";
            this.faulty.Size = new System.Drawing.Size(53, 17);
            this.faulty.TabIndex = 36;
            this.faulty.Text = "Faulty";
            this.faulty.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(9, 501);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(374, 13);
            this.label1.TabIndex = 30;
            this.label1.Text = "Perform the following actions on the selected Feature Definitions";
            // 
            // btnUpgrade
            // 
            this.btnUpgrade.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnUpgrade.Location = new System.Drawing.Point(105, 521);
            this.btnUpgrade.Name = "btnUpgrade";
            this.btnUpgrade.Size = new System.Drawing.Size(76, 20);
            this.btnUpgrade.TabIndex = 31;
            this.btnUpgrade.Text = "Upgrade";
            this.btnUpgrade.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.Location = new System.Drawing.Point(-4, 549);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(768, 104);
            this.panel1.TabIndex = 32;
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1206, 730);
            this.Controls.Add(this.splitContainerCompleteMainframe);
            this.Name = "FrmMain";
            this.Text = "FeatureAdmin for SharePoint";
            this.Load += new System.EventHandler(this.FrmMain_Load);
            this.tabControl1.ResumeLayout(false);
            this.FarmFeatures.ResumeLayout(false);
            this.FarmFeatures.PerformLayout();
            this.FarmActionPanel.ResumeLayout(false);
            this.FarmActionPanel.PerformLayout();
            this.WebAppActionPanel.ResumeLayout(false);
            this.WebAppActionPanel.PerformLayout();
            this.SiteCollectionActionPanel.ResumeLayout(false);
            this.SiteCollectionActionPanel.PerformLayout();
            this.WebActionPanel.ResumeLayout(false);
            this.WebActionPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridFeatureDefinitions)).EndInit();
            this.RemoveFeatures.ResumeLayout(false);
            this.splitContainerRightSiteCollFeaturesWebFeatures.Panel1.ResumeLayout(false);
            this.splitContainerRightSiteCollFeaturesWebFeatures.Panel1.PerformLayout();
            this.splitContainerRightSiteCollFeaturesWebFeatures.Panel2.ResumeLayout(false);
            this.splitContainerRightSiteCollFeaturesWebFeatures.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerRightSiteCollFeaturesWebFeatures)).EndInit();
            this.splitContainerRightSiteCollFeaturesWebFeatures.ResumeLayout(false);
            this.splitContainerCompleteMainframe.Panel1.ResumeLayout(false);
            this.splitContainerCompleteMainframe.Panel2.ResumeLayout(false);
            this.splitContainerCompleteMainframe.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerCompleteMainframe)).EndInit();
            this.splitContainerCompleteMainframe.ResumeLayout(false);
            this.splitContainerLeftWindow.Panel1.ResumeLayout(false);
            this.splitContainerLeftWindow.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerLeftWindow)).EndInit();
            this.splitContainerLeftWindow.ResumeLayout(false);
            this.splitContainerLeftUpperWebAppAndSiteColl.Panel1.ResumeLayout(false);
            this.splitContainerLeftUpperWebAppAndSiteColl.Panel1.PerformLayout();
            this.splitContainerLeftUpperWebAppAndSiteColl.Panel2.ResumeLayout(false);
            this.splitContainerLeftUpperWebAppAndSiteColl.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerLeftUpperWebAppAndSiteColl)).EndInit();
            this.splitContainerLeftUpperWebAppAndSiteColl.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridWebApplications)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridSiteCollections)).EndInit();
            this.splitContainerLeftDownWebsAndLogs.Panel1.ResumeLayout(false);
            this.splitContainerLeftDownWebsAndLogs.Panel1.PerformLayout();
            this.splitContainerLeftDownWebsAndLogs.Panel2.ResumeLayout(false);
            this.splitContainerLeftDownWebsAndLogs.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerLeftDownWebsAndLogs)).EndInit();
            this.splitContainerLeftDownWebsAndLogs.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridWebs)).EndInit();
            this.featureFilterBox.ResumeLayout(false);
            this.featureFilterBox.PerformLayout();
            this.ResumeLayout(false);

        }



        private System.Windows.Forms.TextBox txtResult;
        private System.Windows.Forms.Label lblSPSiteFeatures;
        private System.Windows.Forms.CheckedListBox clbSPSiteFeatures;
        private System.Windows.Forms.Label lblSPWebFeatures;
        private System.Windows.Forms.CheckedListBox clbSPWebFeatures;
        private System.Windows.Forms.Button btnRemoveFromWeb;
        private System.Windows.Forms.Label lblLog;
        private System.Windows.Forms.Button btnRefreshAllContent;
        private System.Windows.Forms.Label lblWebApps;
        private System.Windows.Forms.Label lblSiteCollections;
        private System.Windows.Forms.Label lblWebs;
        private System.Windows.Forms.Label lblFeatureDefinitions;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnRemoveFromSiteCollection;
        private System.Windows.Forms.Button btnRemoveFromWebApp;
        private System.Windows.Forms.Button btnRemoveFromFarm;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage RemoveFeatures;
        private System.Windows.Forms.TabPage FarmFeatures;
    
        #endregion    
        private System.Windows.Forms.SplitContainer splitContainerRightSiteCollFeaturesWebFeatures;
        private System.Windows.Forms.SplitContainer splitContainerCompleteMainframe;
        private System.Windows.Forms.SplitContainer splitContainerLeftWindow;
        private System.Windows.Forms.SplitContainer splitContainerLeftUpperWebAppAndSiteColl;
        private System.Windows.Forms.SplitContainer splitContainerLeftDownWebsAndLogs;
        private System.Windows.Forms.DataGridView gridFeatureDefinitions;
        private System.Windows.Forms.Button btnClearLog;
        private System.Windows.Forms.Panel WebActionPanel;
        private System.Windows.Forms.Button btnActivateSPWeb;
        private System.Windows.Forms.Button btnViewActivations;
        private System.Windows.Forms.Panel SiteCollectionActionPanel;
        private System.Windows.Forms.Button btnDeactivateSPSite;
        private System.Windows.Forms.Label ActionSiteCaption;
        private System.Windows.Forms.Button btnActivateSPSite;
        private System.Windows.Forms.Button btnDeactivateSPWeb;
        private System.Windows.Forms.Label ActionWebCaption;
        private System.Windows.Forms.Panel WebAppActionPanel;
        private System.Windows.Forms.Label ActionWebAppCaption;
        private System.Windows.Forms.Button btnDeactivateSPWebApp;
        private System.Windows.Forms.Button btnActivateSPWebApp;
        private System.Windows.Forms.Panel FarmActionPanel;
        private System.Windows.Forms.Button btnUninstFDef;
        private System.Windows.Forms.Button btnDeactivateSPFarm;
        private System.Windows.Forms.Label ActionFarmCaption;
        private System.Windows.Forms.Button btnActivateSPFarm;
        private System.Windows.Forms.DataGridView gridWebApplications;
        private System.Windows.Forms.DataGridView gridSiteCollections;
        private System.Windows.Forms.DataGridView gridWebs;
        private System.Windows.Forms.CheckBox useForce;
        private System.Windows.Forms.Button btnUpgrade;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.GroupBox featureFilterBox;
        private System.Windows.Forms.RadioButton faulty;
        private System.Windows.Forms.RadioButton web;
        private System.Windows.Forms.RadioButton siCo;
        private System.Windows.Forms.RadioButton webApp;
        private System.Windows.Forms.RadioButton farm;
        private System.Windows.Forms.RadioButton all;
    }
}

