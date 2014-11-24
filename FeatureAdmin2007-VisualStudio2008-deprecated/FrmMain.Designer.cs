namespace FeatureAdmin
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
            this.listWebApplications = new System.Windows.Forms.ListBox();
            this.btnListWebApplications = new System.Windows.Forms.Button();
            this.lblSiteCollections = new System.Windows.Forms.Label();
            this.listSiteCollections = new System.Windows.Forms.ListBox();
            this.lblWebApps = new System.Windows.Forms.Label();
            this.listSites = new System.Windows.Forms.ListBox();
            this.lblWebs = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lblFeatureDefinitions = new System.Windows.Forms.Label();
            this.btnReloadFDefs = new System.Windows.Forms.Button();
            this.btnUninstFDef = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.btnRemoveFromSiteCollection = new System.Windows.Forms.Button();
            this.btnRemoveFromWebApp = new System.Windows.Forms.Button();
            this.btnRemoveFromFarm = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.FarmFeatures = new System.Windows.Forms.TabPage();
            this.gridFeatureDefinitions = new System.Windows.Forms.DataGridView();
            this.btnLoadAllFeatureActivations = new System.Windows.Forms.Button();
            this.btnFindAllActivationsFeature = new System.Windows.Forms.Button();
            this.btnActivateSPWeb = new System.Windows.Forms.Button();
            this.btnActivateSPSite = new System.Windows.Forms.Button();
            this.btnActivateSPWebApp = new System.Windows.Forms.Button();
            this.btnFindActivatedFeature = new System.Windows.Forms.Button();
            this.btnActivateSPFarm = new System.Windows.Forms.Button();
            this.RemoveFeatures = new System.Windows.Forms.TabPage();
            this.splitContainerRightSiteCollFeaturesWebFeatures = new System.Windows.Forms.SplitContainer();
            this.btnClearLog = new System.Windows.Forms.Button();
            this.btnFindFaultyFeature = new System.Windows.Forms.Button();
            this.splitContainerCompleteMainframe = new System.Windows.Forms.SplitContainer();
            this.splitContainerLeftWindow = new System.Windows.Forms.SplitContainer();
            this.splitContainerLeftUpperWebAppAndSiteColl = new System.Windows.Forms.SplitContainer();
            this.splitContainerLeftDownWebsAndLogs = new System.Windows.Forms.SplitContainer();
            this.tabControl1.SuspendLayout();
            this.FarmFeatures.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridFeatureDefinitions)).BeginInit();
            this.RemoveFeatures.SuspendLayout();
            this.splitContainerRightSiteCollFeaturesWebFeatures.Panel1.SuspendLayout();
            this.splitContainerRightSiteCollFeaturesWebFeatures.Panel2.SuspendLayout();
            this.splitContainerRightSiteCollFeaturesWebFeatures.SuspendLayout();
            this.splitContainerCompleteMainframe.Panel1.SuspendLayout();
            this.splitContainerCompleteMainframe.Panel2.SuspendLayout();
            this.splitContainerCompleteMainframe.SuspendLayout();
            this.splitContainerLeftWindow.Panel1.SuspendLayout();
            this.splitContainerLeftWindow.Panel2.SuspendLayout();
            this.splitContainerLeftWindow.SuspendLayout();
            this.splitContainerLeftUpperWebAppAndSiteColl.Panel1.SuspendLayout();
            this.splitContainerLeftUpperWebAppAndSiteColl.Panel2.SuspendLayout();
            this.splitContainerLeftUpperWebAppAndSiteColl.SuspendLayout();
            this.splitContainerLeftDownWebsAndLogs.Panel1.SuspendLayout();
            this.splitContainerLeftDownWebsAndLogs.Panel2.SuspendLayout();
            this.splitContainerLeftDownWebsAndLogs.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtResult
            // 
            this.txtResult.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtResult.Location = new System.Drawing.Point(0, 16);
            this.txtResult.Multiline = true;
            this.txtResult.Name = "txtResult";
            this.txtResult.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtResult.Size = new System.Drawing.Size(412, 328);
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
            // listWebApplications
            // 
            this.listWebApplications.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.listWebApplications.BackColor = System.Drawing.SystemColors.Window;
            this.listWebApplications.FormattingEnabled = true;
            this.listWebApplications.Location = new System.Drawing.Point(0, 32);
            this.listWebApplications.Name = "listWebApplications";
            this.listWebApplications.Size = new System.Drawing.Size(412, 69);
            this.listWebApplications.TabIndex = 11;
            this.listWebApplications.SelectedIndexChanged += new System.EventHandler(this.listWebApplications_SelectedIndexChanged);
            // 
            // btnListWebApplications
            // 
            this.btnListWebApplications.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnListWebApplications.Location = new System.Drawing.Point(304, 3);
            this.btnListWebApplications.Name = "btnListWebApplications";
            this.btnListWebApplications.Size = new System.Drawing.Size(105, 24);
            this.btnListWebApplications.TabIndex = 12;
            this.btnListWebApplications.Text = "Reload Web Apps";
            this.btnListWebApplications.UseVisualStyleBackColor = true;
            this.btnListWebApplications.Click += new System.EventHandler(this.btnListWebApplications_Click);
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
            // listSiteCollections
            // 
            this.listSiteCollections.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.listSiteCollections.BackColor = System.Drawing.SystemColors.Window;
            this.listSiteCollections.Enabled = false;
            this.listSiteCollections.FormattingEnabled = true;
            this.listSiteCollections.Location = new System.Drawing.Point(0, 15);
            this.listSiteCollections.Name = "listSiteCollections";
            this.listSiteCollections.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.listSiteCollections.Size = new System.Drawing.Size(412, 82);
            this.listSiteCollections.TabIndex = 9;
            this.listSiteCollections.SelectedIndexChanged += new System.EventHandler(this.listSiteCollections_SelectedIndexChanged);
            // 
            // lblWebApps
            // 
            this.lblWebApps.AutoSize = true;
            this.lblWebApps.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblWebApps.Location = new System.Drawing.Point(12, 9);
            this.lblWebApps.Name = "lblWebApps";
            this.lblWebApps.Size = new System.Drawing.Size(90, 13);
            this.lblWebApps.TabIndex = 9;
            this.lblWebApps.Text = "Web Applications";
            // 
            // listSites
            // 
            this.listSites.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.listSites.BackColor = System.Drawing.SystemColors.Window;
            this.listSites.FormattingEnabled = true;
            this.listSites.Location = new System.Drawing.Point(0, 16);
            this.listSites.Name = "listSites";
            this.listSites.Size = new System.Drawing.Size(412, 121);
            this.listSites.TabIndex = 9;
            this.listSites.SelectedIndexChanged += new System.EventHandler(this.listSites_SelectedIndexChanged);
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
            // btnReloadFDefs
            // 
            this.btnReloadFDefs.ForeColor = System.Drawing.Color.MidnightBlue;
            this.btnReloadFDefs.Location = new System.Drawing.Point(222, 6);
            this.btnReloadFDefs.Name = "btnReloadFDefs";
            this.btnReloadFDefs.Size = new System.Drawing.Size(62, 21);
            this.btnReloadFDefs.TabIndex = 8;
            this.btnReloadFDefs.Text = "Load";
            this.btnReloadFDefs.UseVisualStyleBackColor = true;
            this.btnReloadFDefs.Click += new System.EventHandler(this.btnLoadFDefs_Click);
            // 
            // btnUninstFDef
            // 
            this.btnUninstFDef.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnUninstFDef.ForeColor = System.Drawing.Color.MidnightBlue;
            this.btnUninstFDef.Location = new System.Drawing.Point(9, 466);
            this.btnUninstFDef.Name = "btnUninstFDef";
            this.btnUninstFDef.Size = new System.Drawing.Size(62, 23);
            this.btnUninstFDef.TabIndex = 8;
            this.btnUninstFDef.Text = "Uninstall";
            this.btnUninstFDef.UseVisualStyleBackColor = true;
            this.btnUninstFDef.Click += new System.EventHandler(this.btnUninstFeatureDef_Click);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(77, 471);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(230, 13);
            this.label1.TabIndex = 16;
            this.label1.Text = "Uninstall selected Feature Definitions from Farm";
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
            this.tabControl1.Location = new System.Drawing.Point(3, 3);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(458, 637);
            this.tabControl1.TabIndex = 17;
            // 
            // FarmFeatures
            // 
            this.FarmFeatures.Controls.Add(this.gridFeatureDefinitions);
            this.FarmFeatures.Controls.Add(this.btnLoadAllFeatureActivations);
            this.FarmFeatures.Controls.Add(this.btnFindAllActivationsFeature);
            this.FarmFeatures.Controls.Add(this.btnActivateSPWeb);
            this.FarmFeatures.Controls.Add(this.btnActivateSPSite);
            this.FarmFeatures.Controls.Add(this.btnActivateSPWebApp);
            this.FarmFeatures.Controls.Add(this.btnFindActivatedFeature);
            this.FarmFeatures.Controls.Add(this.btnActivateSPFarm);
            this.FarmFeatures.Controls.Add(this.btnUninstFDef);
            this.FarmFeatures.Controls.Add(this.label4);
            this.FarmFeatures.Controls.Add(this.label1);
            this.FarmFeatures.Controls.Add(this.lblFeatureDefinitions);
            this.FarmFeatures.Controls.Add(this.btnReloadFDefs);
            this.FarmFeatures.Location = new System.Drawing.Point(4, 22);
            this.FarmFeatures.Name = "FarmFeatures";
            this.FarmFeatures.Padding = new System.Windows.Forms.Padding(3);
            this.FarmFeatures.Size = new System.Drawing.Size(450, 611);
            this.FarmFeatures.TabIndex = 1;
            this.FarmFeatures.Text = "Farm Feature Administration";
            this.FarmFeatures.UseVisualStyleBackColor = true;
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
            this.gridFeatureDefinitions.Size = new System.Drawing.Size(436, 409);
            this.gridFeatureDefinitions.TabIndex = 24;
            this.gridFeatureDefinitions.SelectionChanged += new System.EventHandler(this.gridFeatureDefinitions_SelectionChanged);
            // 
            // btnLoadAllFeatureActivations
            // 
            this.btnLoadAllFeatureActivations.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnLoadAllFeatureActivations.ForeColor = System.Drawing.Color.MidnightBlue;
            this.btnLoadAllFeatureActivations.Location = new System.Drawing.Point(247, 553);
            this.btnLoadAllFeatureActivations.Name = "btnLoadAllFeatureActivations";
            this.btnLoadAllFeatureActivations.Size = new System.Drawing.Size(195, 23);
            this.btnLoadAllFeatureActivations.TabIndex = 23;
            this.btnLoadAllFeatureActivations.Text = "Load all activation data";
            this.btnLoadAllFeatureActivations.UseVisualStyleBackColor = true;
            this.btnLoadAllFeatureActivations.Click += new System.EventHandler(this.btnLoadAllFeatureActivations_Click);
            // 
            // btnFindAllActivationsFeature
            // 
            this.btnFindAllActivationsFeature.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnFindAllActivationsFeature.ForeColor = System.Drawing.Color.MidnightBlue;
            this.btnFindAllActivationsFeature.Location = new System.Drawing.Point(247, 524);
            this.btnFindAllActivationsFeature.Name = "btnFindAllActivationsFeature";
            this.btnFindAllActivationsFeature.Size = new System.Drawing.Size(195, 23);
            this.btnFindAllActivationsFeature.TabIndex = 22;
            this.btnFindAllActivationsFeature.Text = "Find all activations";
            this.btnFindAllActivationsFeature.UseVisualStyleBackColor = true;
            this.btnFindAllActivationsFeature.Click += new System.EventHandler(this.btnFindAllActivationsFeature_Click);
            // 
            // btnActivateSPWeb
            // 
            this.btnActivateSPWeb.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnActivateSPWeb.ForeColor = System.Drawing.Color.MidnightBlue;
            this.btnActivateSPWeb.Location = new System.Drawing.Point(9, 495);
            this.btnActivateSPWeb.Name = "btnActivateSPWeb";
            this.btnActivateSPWeb.Size = new System.Drawing.Size(200, 23);
            this.btnActivateSPWeb.TabIndex = 20;
            this.btnActivateSPWeb.Text = "Activate in selected Site (SPweb)";
            this.btnActivateSPWeb.UseVisualStyleBackColor = true;
            this.btnActivateSPWeb.Click += new System.EventHandler(this.btnActivateSPWeb_Click);
            // 
            // btnActivateSPSite
            // 
            this.btnActivateSPSite.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnActivateSPSite.ForeColor = System.Drawing.Color.MidnightBlue;
            this.btnActivateSPSite.Location = new System.Drawing.Point(9, 524);
            this.btnActivateSPSite.Name = "btnActivateSPSite";
            this.btnActivateSPSite.Size = new System.Drawing.Size(200, 23);
            this.btnActivateSPSite.TabIndex = 21;
            this.btnActivateSPSite.Text = "Activate in selected SiteCollection";
            this.btnActivateSPSite.UseVisualStyleBackColor = true;
            this.btnActivateSPSite.Click += new System.EventHandler(this.btnActivateSPSite_Click);
            // 
            // btnActivateSPWebApp
            // 
            this.btnActivateSPWebApp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnActivateSPWebApp.ForeColor = System.Drawing.Color.MidnightBlue;
            this.btnActivateSPWebApp.Location = new System.Drawing.Point(9, 553);
            this.btnActivateSPWebApp.Name = "btnActivateSPWebApp";
            this.btnActivateSPWebApp.Size = new System.Drawing.Size(200, 23);
            this.btnActivateSPWebApp.TabIndex = 18;
            this.btnActivateSPWebApp.Text = "Activate in selected Web App";
            this.btnActivateSPWebApp.UseVisualStyleBackColor = true;
            this.btnActivateSPWebApp.Click += new System.EventHandler(this.btnActivateSPWebApp_Click);
            // 
            // btnFindActivatedFeature
            // 
            this.btnFindActivatedFeature.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnFindActivatedFeature.ForeColor = System.Drawing.Color.MidnightBlue;
            this.btnFindActivatedFeature.Location = new System.Drawing.Point(247, 495);
            this.btnFindActivatedFeature.Name = "btnFindActivatedFeature";
            this.btnFindActivatedFeature.Size = new System.Drawing.Size(195, 23);
            this.btnFindActivatedFeature.TabIndex = 19;
            this.btnFindActivatedFeature.Text = "Find one activation";
            this.btnFindActivatedFeature.UseVisualStyleBackColor = true;
            this.btnFindActivatedFeature.Click += new System.EventHandler(this.btnFindActivatedFeature_Click);
            // 
            // btnActivateSPFarm
            // 
            this.btnActivateSPFarm.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnActivateSPFarm.ForeColor = System.Drawing.Color.MidnightBlue;
            this.btnActivateSPFarm.Location = new System.Drawing.Point(9, 582);
            this.btnActivateSPFarm.Name = "btnActivateSPFarm";
            this.btnActivateSPFarm.Size = new System.Drawing.Size(200, 23);
            this.btnActivateSPFarm.TabIndex = 19;
            this.btnActivateSPFarm.Text = "Activate in whole Farm";
            this.btnActivateSPFarm.UseVisualStyleBackColor = true;
            this.btnActivateSPFarm.Click += new System.EventHandler(this.btnActivateSPFarm_Click);
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
            this.RemoveFeatures.Size = new System.Drawing.Size(450, 611);
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
            // btnClearLog
            // 
            this.btnClearLog.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnClearLog.Location = new System.Drawing.Point(7, 646);
            this.btnClearLog.Name = "btnClearLog";
            this.btnClearLog.Size = new System.Drawing.Size(90, 23);
            this.btnClearLog.TabIndex = 18;
            this.btnClearLog.Text = "Clear Log";
            this.btnClearLog.UseVisualStyleBackColor = true;
            this.btnClearLog.Click += new System.EventHandler(this.btnClearLog_Click);
            // 
            // btnFindFaultyFeature
            // 
            this.btnFindFaultyFeature.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnFindFaultyFeature.Location = new System.Drawing.Point(7, 675);
            this.btnFindFaultyFeature.Name = "btnFindFaultyFeature";
            this.btnFindFaultyFeature.Size = new System.Drawing.Size(200, 23);
            this.btnFindFaultyFeature.TabIndex = 18;
            this.btnFindFaultyFeature.Text = "Find Faulty Feature in Farm";
            this.btnFindFaultyFeature.UseVisualStyleBackColor = true;
            this.btnFindFaultyFeature.Click += new System.EventHandler(this.btnFindFaultyFeature_Click);
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
            this.splitContainerCompleteMainframe.Panel2.Controls.Add(this.btnClearLog);
            this.splitContainerCompleteMainframe.Panel2.Controls.Add(this.btnFindFaultyFeature);
            this.splitContainerCompleteMainframe.Panel2.Controls.Add(this.tabControl1);
            this.splitContainerCompleteMainframe.Size = new System.Drawing.Size(890, 728);
            this.splitContainerCompleteMainframe.SplitterDistance = 422;
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
            this.splitContainerLeftWindow.Size = new System.Drawing.Size(419, 725);
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
            this.splitContainerLeftUpperWebAppAndSiteColl.Panel1.Controls.Add(this.lblWebApps);
            this.splitContainerLeftUpperWebAppAndSiteColl.Panel1.Controls.Add(this.btnListWebApplications);
            this.splitContainerLeftUpperWebAppAndSiteColl.Panel1.Controls.Add(this.listWebApplications);
            // 
            // splitContainerLeftUpperWebAppAndSiteColl.Panel2
            // 
            this.splitContainerLeftUpperWebAppAndSiteColl.Panel2.Controls.Add(this.lblSiteCollections);
            this.splitContainerLeftUpperWebAppAndSiteColl.Panel2.Controls.Add(this.listSiteCollections);
            this.splitContainerLeftUpperWebAppAndSiteColl.Size = new System.Drawing.Size(412, 214);
            this.splitContainerLeftUpperWebAppAndSiteColl.SplitterDistance = 105;
            this.splitContainerLeftUpperWebAppAndSiteColl.TabIndex = 0;
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
            this.splitContainerLeftDownWebsAndLogs.Panel1.Controls.Add(this.lblWebs);
            this.splitContainerLeftDownWebsAndLogs.Panel1.Controls.Add(this.listSites);
            // 
            // splitContainerLeftDownWebsAndLogs.Panel2
            // 
            this.splitContainerLeftDownWebsAndLogs.Panel2.Controls.Add(this.lblLog);
            this.splitContainerLeftDownWebsAndLogs.Panel2.Controls.Add(this.txtResult);
            this.splitContainerLeftDownWebsAndLogs.Size = new System.Drawing.Size(412, 494);
            this.splitContainerLeftDownWebsAndLogs.SplitterDistance = 146;
            this.splitContainerLeftDownWebsAndLogs.TabIndex = 0;
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(892, 730);
            this.Controls.Add(this.splitContainerCompleteMainframe);
            this.Name = "FrmMain";
            this.Text = "FeatureAdmin for SharePoint 2010 - v2.3";
            this.Load += new System.EventHandler(this.FrmMain_Load);
            this.tabControl1.ResumeLayout(false);
            this.FarmFeatures.ResumeLayout(false);
            this.FarmFeatures.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridFeatureDefinitions)).EndInit();
            this.RemoveFeatures.ResumeLayout(false);
            this.splitContainerRightSiteCollFeaturesWebFeatures.Panel1.ResumeLayout(false);
            this.splitContainerRightSiteCollFeaturesWebFeatures.Panel1.PerformLayout();
            this.splitContainerRightSiteCollFeaturesWebFeatures.Panel2.ResumeLayout(false);
            this.splitContainerRightSiteCollFeaturesWebFeatures.Panel2.PerformLayout();
            this.splitContainerRightSiteCollFeaturesWebFeatures.ResumeLayout(false);
            this.splitContainerCompleteMainframe.Panel1.ResumeLayout(false);
            this.splitContainerCompleteMainframe.Panel2.ResumeLayout(false);
            this.splitContainerCompleteMainframe.ResumeLayout(false);
            this.splitContainerLeftWindow.Panel1.ResumeLayout(false);
            this.splitContainerLeftWindow.Panel2.ResumeLayout(false);
            this.splitContainerLeftWindow.ResumeLayout(false);
            this.splitContainerLeftUpperWebAppAndSiteColl.Panel1.ResumeLayout(false);
            this.splitContainerLeftUpperWebAppAndSiteColl.Panel1.PerformLayout();
            this.splitContainerLeftUpperWebAppAndSiteColl.Panel2.ResumeLayout(false);
            this.splitContainerLeftUpperWebAppAndSiteColl.Panel2.PerformLayout();
            this.splitContainerLeftUpperWebAppAndSiteColl.ResumeLayout(false);
            this.splitContainerLeftDownWebsAndLogs.Panel1.ResumeLayout(false);
            this.splitContainerLeftDownWebsAndLogs.Panel1.PerformLayout();
            this.splitContainerLeftDownWebsAndLogs.Panel2.ResumeLayout(false);
            this.splitContainerLeftDownWebsAndLogs.Panel2.PerformLayout();
            this.splitContainerLeftDownWebsAndLogs.ResumeLayout(false);
            this.ResumeLayout(false);

        }



        private System.Windows.Forms.TextBox txtResult;
        private System.Windows.Forms.Label lblSPSiteFeatures;
        private System.Windows.Forms.CheckedListBox clbSPSiteFeatures;
        private System.Windows.Forms.Label lblSPWebFeatures;
        private System.Windows.Forms.CheckedListBox clbSPWebFeatures;
        private System.Windows.Forms.Button btnRemoveFromWeb;
        private System.Windows.Forms.Label lblLog;
        private System.Windows.Forms.ListBox listWebApplications;
        private System.Windows.Forms.Button btnListWebApplications;
        private System.Windows.Forms.Label lblWebApps;
        private System.Windows.Forms.Label lblSiteCollections;
        private System.Windows.Forms.ListBox listSiteCollections;
        private System.Windows.Forms.ListBox listSites;
        private System.Windows.Forms.Label lblWebs;
        private System.Windows.Forms.Label lblFeatureDefinitions;

        private System.Windows.Forms.Button btnReloadFDefs;
        private System.Windows.Forms.Button btnUninstFDef;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnRemoveFromSiteCollection;
        private System.Windows.Forms.Button btnRemoveFromWebApp;
        private System.Windows.Forms.Button btnRemoveFromFarm;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage RemoveFeatures;
        private System.Windows.Forms.TabPage FarmFeatures;
        private System.Windows.Forms.Button btnClearLog;
        private System.Windows.Forms.Button btnActivateSPWeb;
        private System.Windows.Forms.Button btnActivateSPSite;
        private System.Windows.Forms.Button btnActivateSPWebApp;
        private System.Windows.Forms.Button btnActivateSPFarm;
        private System.Windows.Forms.Button btnFindFaultyFeature;
        private System.Windows.Forms.Button btnFindActivatedFeature;
    
        #endregion    
        private System.Windows.Forms.SplitContainer splitContainerRightSiteCollFeaturesWebFeatures;
        private System.Windows.Forms.SplitContainer splitContainerCompleteMainframe;
        private System.Windows.Forms.SplitContainer splitContainerLeftWindow;
        private System.Windows.Forms.SplitContainer splitContainerLeftUpperWebAppAndSiteColl;
        private System.Windows.Forms.SplitContainer splitContainerLeftDownWebsAndLogs;
        private System.Windows.Forms.Button btnLoadAllFeatureActivations;
        private System.Windows.Forms.Button btnFindAllActivationsFeature;
        private System.Windows.Forms.DataGridView gridFeatureDefinitions;
    
    }
}

