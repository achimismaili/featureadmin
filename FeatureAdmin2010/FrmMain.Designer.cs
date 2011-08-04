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
            this.btnRemoveFromList = new System.Windows.Forms.Button();
            this.lblLog = new System.Windows.Forms.Label();
            this.listWebApplications = new System.Windows.Forms.ListBox();
            this.btnListWebApplications = new System.Windows.Forms.Button();
            this.lblSiteCollections = new System.Windows.Forms.Label();
            this.listSiteCollections = new System.Windows.Forms.ListBox();
            this.lblWebApps = new System.Windows.Forms.Label();
            this.listSites = new System.Windows.Forms.ListBox();
            this.lblWebs = new System.Windows.Forms.Label();
            this.clbFeatureDefinitions = new System.Windows.Forms.CheckedListBox();
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
            this.btnActivateSPWeb = new System.Windows.Forms.Button();
            this.btnActivateSPSite = new System.Windows.Forms.Button();
            this.btnActivateSPWebApp = new System.Windows.Forms.Button();
            this.btnFindActivatedFeature = new System.Windows.Forms.Button();
            this.btnActivateSPFarm = new System.Windows.Forms.Button();
            this.RemoveFeatures = new System.Windows.Forms.TabPage();
            this.btnClearLog = new System.Windows.Forms.Button();
            this.btnFindFaultyFeature = new System.Windows.Forms.Button();
            this.tabControl1.SuspendLayout();
            this.FarmFeatures.SuspendLayout();
            this.RemoveFeatures.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtResult
            // 
            this.txtResult.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtResult.Location = new System.Drawing.Point(12, 484);
            this.txtResult.Multiline = true;
            this.txtResult.Name = "txtResult";
            this.txtResult.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtResult.Size = new System.Drawing.Size(403, 177);
            this.txtResult.TabIndex = 2;
            this.txtResult.WordWrap = false;
            // 
            // lblSPSiteFeatures
            // 
            this.lblSPSiteFeatures.AutoSize = true;
            this.lblSPSiteFeatures.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lblSPSiteFeatures.Location = new System.Drawing.Point(3, 0);
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
            this.clbSPSiteFeatures.Location = new System.Drawing.Point(6, 16);
            this.clbSPSiteFeatures.Name = "clbSPSiteFeatures";
            this.clbSPSiteFeatures.Size = new System.Drawing.Size(449, 199);
            this.clbSPSiteFeatures.TabIndex = 8;
            this.clbSPSiteFeatures.SelectedIndexChanged += new System.EventHandler(this.clbSPSiteFeatures_SelectedIndexChanged);
            // 
            // lblSPWebFeatures
            // 
            this.lblSPWebFeatures.AutoSize = true;
            this.lblSPWebFeatures.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lblSPWebFeatures.Location = new System.Drawing.Point(3, 226);
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
            this.clbSPWebFeatures.Location = new System.Drawing.Point(6, 242);
            this.clbSPWebFeatures.Name = "clbSPWebFeatures";
            this.clbSPWebFeatures.Size = new System.Drawing.Size(449, 199);
            this.clbSPWebFeatures.TabIndex = 7;
            this.clbSPWebFeatures.SelectedIndexChanged += new System.EventHandler(this.clbSPWebFeatures_SelectedIndexChanged);
            // 
            // btnRemoveFromList
            // 
            this.btnRemoveFromList.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRemoveFromList.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btnRemoveFromList.Location = new System.Drawing.Point(6, 447);
            this.btnRemoveFromList.Name = "btnRemoveFromList";
            this.btnRemoveFromList.Size = new System.Drawing.Size(200, 23);
            this.btnRemoveFromList.TabIndex = 8;
            this.btnRemoveFromList.Text = "Remove from selected Site (SPweb)";
            this.btnRemoveFromList.UseVisualStyleBackColor = true;
            this.btnRemoveFromList.Click += new System.EventHandler(this.btnRemoveFromList_Click);
            // 
            // lblLog
            // 
            this.lblLog.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblLog.AutoSize = true;
            this.lblLog.Location = new System.Drawing.Point(12, 468);
            this.lblLog.Name = "lblLog";
            this.lblLog.Size = new System.Drawing.Size(25, 13);
            this.lblLog.TabIndex = 10;
            this.lblLog.Text = "Log";
            // 
            // listWebApplications
            // 
            this.listWebApplications.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.listWebApplications.BackColor = System.Drawing.SystemColors.Window;
            this.listWebApplications.FormattingEnabled = true;
            this.listWebApplications.Location = new System.Drawing.Point(12, 40);
            this.listWebApplications.Name = "listWebApplications";
            this.listWebApplications.Size = new System.Drawing.Size(403, 82);
            this.listWebApplications.TabIndex = 11;
            this.listWebApplications.SelectedIndexChanged += new System.EventHandler(this.listWebApplications_SelectedIndexChanged);
            // 
            // btnListWebApplications
            // 
            this.btnListWebApplications.Location = new System.Drawing.Point(310, 10);
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
            this.lblSiteCollections.Location = new System.Drawing.Point(12, 125);
            this.lblSiteCollections.Name = "lblSiteCollections";
            this.lblSiteCollections.Size = new System.Drawing.Size(233, 13);
            this.lblSiteCollections.TabIndex = 8;
            this.lblSiteCollections.Text = "Site Collections within selected Web Application";
            // 
            // listSiteCollections
            // 
            this.listSiteCollections.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.listSiteCollections.BackColor = System.Drawing.SystemColors.Window;
            this.listSiteCollections.Enabled = false;
            this.listSiteCollections.FormattingEnabled = true;
            this.listSiteCollections.Location = new System.Drawing.Point(12, 141);
            this.listSiteCollections.Name = "listSiteCollections";
            this.listSiteCollections.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.listSiteCollections.Size = new System.Drawing.Size(403, 95);
            this.listSiteCollections.TabIndex = 9;
            this.listSiteCollections.SelectedIndexChanged += new System.EventHandler(this.listSiteCollections_SelectedIndexChanged);
            // 
            // lblWebApps
            // 
            this.lblWebApps.AutoSize = true;
            this.lblWebApps.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblWebApps.Location = new System.Drawing.Point(12, 24);
            this.lblWebApps.Name = "lblWebApps";
            this.lblWebApps.Size = new System.Drawing.Size(90, 13);
            this.lblWebApps.TabIndex = 9;
            this.lblWebApps.Text = "Web Applications";
            // 
            // listSites
            // 
            this.listSites.BackColor = System.Drawing.SystemColors.Window;
            this.listSites.FormattingEnabled = true;
            this.listSites.Location = new System.Drawing.Point(12, 266);
            this.listSites.Name = "listSites";
            this.listSites.Size = new System.Drawing.Size(403, 199);
            this.listSites.TabIndex = 9;
            this.listSites.SelectedIndexChanged += new System.EventHandler(this.listSites_SelectedIndexChanged);
            // 
            // lblWebs
            // 
            this.lblWebs.AutoSize = true;
            this.lblWebs.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblWebs.Location = new System.Drawing.Point(12, 250);
            this.lblWebs.Name = "lblWebs";
            this.lblWebs.Size = new System.Drawing.Size(196, 13);
            this.lblWebs.TabIndex = 8;
            this.lblWebs.Text = "Web Sites within selected SiteCollection";
            // 
            // clbFeatureDefinitions
            // 
            this.clbFeatureDefinitions.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.clbFeatureDefinitions.BackColor = System.Drawing.Color.LightSteelBlue;
            this.clbFeatureDefinitions.CheckOnClick = true;
            this.clbFeatureDefinitions.ForeColor = System.Drawing.Color.MidnightBlue;
            this.clbFeatureDefinitions.FormattingEnabled = true;
            this.clbFeatureDefinitions.Location = new System.Drawing.Point(6, 31);
            this.clbFeatureDefinitions.Name = "clbFeatureDefinitions";
            this.clbFeatureDefinitions.Size = new System.Drawing.Size(449, 379);
            this.clbFeatureDefinitions.TabIndex = 8;
            this.clbFeatureDefinitions.SelectedIndexChanged += new System.EventHandler(this.clbFeatureDefinitions_SelectedIndexChanged);
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
            this.btnReloadFDefs.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
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
            this.btnUninstFDef.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.btnUninstFDef.ForeColor = System.Drawing.Color.MidnightBlue;
            this.btnUninstFDef.Location = new System.Drawing.Point(9, 423);
            this.btnUninstFDef.Name = "btnUninstFDef";
            this.btnUninstFDef.Size = new System.Drawing.Size(62, 23);
            this.btnUninstFDef.TabIndex = 8;
            this.btnUninstFDef.Text = "Uninstall";
            this.btnUninstFDef.UseVisualStyleBackColor = true;
            this.btnUninstFDef.Click += new System.EventHandler(this.btnUninstFeatureDef_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(77, 428);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(230, 13);
            this.label1.TabIndex = 16;
            this.label1.Text = "Uninstall selected Feature Definitions from Farm";
            // 
            // btnRemoveFromSiteCollection
            // 
            this.btnRemoveFromSiteCollection.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRemoveFromSiteCollection.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btnRemoveFromSiteCollection.Location = new System.Drawing.Point(6, 476);
            this.btnRemoveFromSiteCollection.Name = "btnRemoveFromSiteCollection";
            this.btnRemoveFromSiteCollection.Size = new System.Drawing.Size(200, 23);
            this.btnRemoveFromSiteCollection.TabIndex = 8;
            this.btnRemoveFromSiteCollection.Text = "Remove from selected SiteCollection";
            this.btnRemoveFromSiteCollection.UseVisualStyleBackColor = true;
            this.btnRemoveFromSiteCollection.Click += new System.EventHandler(this.btnRemoveFromSiteCollection_Click);
            // 
            // btnRemoveFromWebApp
            // 
            this.btnRemoveFromWebApp.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRemoveFromWebApp.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btnRemoveFromWebApp.Location = new System.Drawing.Point(6, 505);
            this.btnRemoveFromWebApp.Name = "btnRemoveFromWebApp";
            this.btnRemoveFromWebApp.Size = new System.Drawing.Size(200, 23);
            this.btnRemoveFromWebApp.TabIndex = 8;
            this.btnRemoveFromWebApp.Text = "Remove from selected Web App";
            this.btnRemoveFromWebApp.UseVisualStyleBackColor = true;
            this.btnRemoveFromWebApp.Click += new System.EventHandler(this.btnRemoveFromWebApp_Click);
            // 
            // btnRemoveFromFarm
            // 
            this.btnRemoveFromFarm.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRemoveFromFarm.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btnRemoveFromFarm.Location = new System.Drawing.Point(6, 534);
            this.btnRemoveFromFarm.Name = "btnRemoveFromFarm";
            this.btnRemoveFromFarm.Size = new System.Drawing.Size(200, 23);
            this.btnRemoveFromFarm.TabIndex = 8;
            this.btnRemoveFromFarm.Text = "Remove Feature from Farm";
            this.btnRemoveFromFarm.UseVisualStyleBackColor = true;
            this.btnRemoveFromFarm.Click += new System.EventHandler(this.btnRemoveFromFarm_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.FarmFeatures);
            this.tabControl1.Controls.Add(this.RemoveFeatures);
            this.tabControl1.Location = new System.Drawing.Point(421, 2);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(471, 594);
            this.tabControl1.TabIndex = 17;
            // 
            // FarmFeatures
            // 
            this.FarmFeatures.Controls.Add(this.btnActivateSPWeb);
            this.FarmFeatures.Controls.Add(this.btnActivateSPSite);
            this.FarmFeatures.Controls.Add(this.btnActivateSPWebApp);
            this.FarmFeatures.Controls.Add(this.btnFindActivatedFeature);
            this.FarmFeatures.Controls.Add(this.btnActivateSPFarm);
            this.FarmFeatures.Controls.Add(this.clbFeatureDefinitions);
            this.FarmFeatures.Controls.Add(this.btnUninstFDef);
            this.FarmFeatures.Controls.Add(this.label4);
            this.FarmFeatures.Controls.Add(this.label1);
            this.FarmFeatures.Controls.Add(this.lblFeatureDefinitions);
            this.FarmFeatures.Controls.Add(this.btnReloadFDefs);
            this.FarmFeatures.Location = new System.Drawing.Point(4, 22);
            this.FarmFeatures.Name = "FarmFeatures";
            this.FarmFeatures.Padding = new System.Windows.Forms.Padding(3);
            this.FarmFeatures.Size = new System.Drawing.Size(463, 568);
            this.FarmFeatures.TabIndex = 1;
            this.FarmFeatures.Text = "Farm Feature Administration";
            this.FarmFeatures.UseVisualStyleBackColor = true;
            // 
            // btnActivateSPWeb
            // 
            this.btnActivateSPWeb.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.btnActivateSPWeb.ForeColor = System.Drawing.Color.MidnightBlue;
            this.btnActivateSPWeb.Location = new System.Drawing.Point(9, 452);
            this.btnActivateSPWeb.Name = "btnActivateSPWeb";
            this.btnActivateSPWeb.Size = new System.Drawing.Size(200, 23);
            this.btnActivateSPWeb.TabIndex = 20;
            this.btnActivateSPWeb.Text = "Activate in selected Site (SPweb)";
            this.btnActivateSPWeb.UseVisualStyleBackColor = true;
            this.btnActivateSPWeb.Click += new System.EventHandler(this.btnActivateSPWeb_Click);
            // 
            // btnActivateSPSite
            // 
            this.btnActivateSPSite.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.btnActivateSPSite.ForeColor = System.Drawing.Color.MidnightBlue;
            this.btnActivateSPSite.Location = new System.Drawing.Point(9, 481);
            this.btnActivateSPSite.Name = "btnActivateSPSite";
            this.btnActivateSPSite.Size = new System.Drawing.Size(200, 23);
            this.btnActivateSPSite.TabIndex = 21;
            this.btnActivateSPSite.Text = "Activate in selected SiteCollection";
            this.btnActivateSPSite.UseVisualStyleBackColor = true;
            this.btnActivateSPSite.Click += new System.EventHandler(this.btnActivateSPSite_Click);
            // 
            // btnActivateSPWebApp
            // 
            this.btnActivateSPWebApp.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.btnActivateSPWebApp.ForeColor = System.Drawing.Color.MidnightBlue;
            this.btnActivateSPWebApp.Location = new System.Drawing.Point(9, 510);
            this.btnActivateSPWebApp.Name = "btnActivateSPWebApp";
            this.btnActivateSPWebApp.Size = new System.Drawing.Size(200, 23);
            this.btnActivateSPWebApp.TabIndex = 18;
            this.btnActivateSPWebApp.Text = "Activate in selected Web App";
            this.btnActivateSPWebApp.UseVisualStyleBackColor = true;
            this.btnActivateSPWebApp.Click += new System.EventHandler(this.btnActivateSPWebApp_Click);
            // 
            // btnFindActivatedFeature
            // 
            this.btnFindActivatedFeature.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.btnFindActivatedFeature.ForeColor = System.Drawing.Color.MidnightBlue;
            this.btnFindActivatedFeature.Location = new System.Drawing.Point(255, 452);
            this.btnFindActivatedFeature.Name = "btnFindActivatedFeature";
            this.btnFindActivatedFeature.Size = new System.Drawing.Size(200, 23);
            this.btnFindActivatedFeature.TabIndex = 19;
            this.btnFindActivatedFeature.Text = "Find where activated in Farm";
            this.btnFindActivatedFeature.UseVisualStyleBackColor = true;
            this.btnFindActivatedFeature.Click += new System.EventHandler(this.btnFindActivatedFeature_Click);
            // 
            // btnActivateSPFarm
            // 
            this.btnActivateSPFarm.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.btnActivateSPFarm.ForeColor = System.Drawing.Color.MidnightBlue;
            this.btnActivateSPFarm.Location = new System.Drawing.Point(9, 539);
            this.btnActivateSPFarm.Name = "btnActivateSPFarm";
            this.btnActivateSPFarm.Size = new System.Drawing.Size(200, 23);
            this.btnActivateSPFarm.TabIndex = 19;
            this.btnActivateSPFarm.Text = "Activate in whole Farm";
            this.btnActivateSPFarm.UseVisualStyleBackColor = true;
            this.btnActivateSPFarm.Click += new System.EventHandler(this.btnActivateSPFarm_Click);
            // 
            // RemoveFeatures
            // 
            this.RemoveFeatures.Controls.Add(this.lblSPWebFeatures);
            this.RemoveFeatures.Controls.Add(this.clbSPWebFeatures);
            this.RemoveFeatures.Controls.Add(this.clbSPSiteFeatures);
            this.RemoveFeatures.Controls.Add(this.btnRemoveFromList);
            this.RemoveFeatures.Controls.Add(this.lblSPSiteFeatures);
            this.RemoveFeatures.Controls.Add(this.btnRemoveFromSiteCollection);
            this.RemoveFeatures.Controls.Add(this.btnRemoveFromWebApp);
            this.RemoveFeatures.Controls.Add(this.btnRemoveFromFarm);
            this.RemoveFeatures.ForeColor = System.Drawing.Color.DarkGoldenrod;
            this.RemoveFeatures.Location = new System.Drawing.Point(4, 22);
            this.RemoveFeatures.Name = "RemoveFeatures";
            this.RemoveFeatures.Padding = new System.Windows.Forms.Padding(3);
            this.RemoveFeatures.Size = new System.Drawing.Size(463, 568);
            this.RemoveFeatures.TabIndex = 0;
            this.RemoveFeatures.Text = "Remove / deactivate features in selected sites";
            this.RemoveFeatures.UseVisualStyleBackColor = true;
            // 
            // btnClearLog
            // 
            this.btnClearLog.Location = new System.Drawing.Point(431, 602);
            this.btnClearLog.Name = "btnClearLog";
            this.btnClearLog.Size = new System.Drawing.Size(90, 23);
            this.btnClearLog.TabIndex = 18;
            this.btnClearLog.Text = "Clear Log";
            this.btnClearLog.UseVisualStyleBackColor = true;
            this.btnClearLog.Click += new System.EventHandler(this.btnClearLog_Click);
            // 
            // btnFindFaultyFeature
            // 
            this.btnFindFaultyFeature.Location = new System.Drawing.Point(431, 631);
            this.btnFindFaultyFeature.Name = "btnFindFaultyFeature";
            this.btnFindFaultyFeature.Size = new System.Drawing.Size(200, 23);
            this.btnFindFaultyFeature.TabIndex = 18;
            this.btnFindFaultyFeature.Text = "Find Faulty Feature in Farm";
            this.btnFindFaultyFeature.UseVisualStyleBackColor = true;
            this.btnFindFaultyFeature.Click += new System.EventHandler(this.btnFindFaultyFeature_Click);
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(892, 673);
            this.Controls.Add(this.btnFindFaultyFeature);
            this.Controls.Add(this.btnClearLog);
            this.Controls.Add(this.btnListWebApplications);
            this.Controls.Add(this.listSiteCollections);
            this.Controls.Add(this.listSites);
            this.Controls.Add(this.lblSiteCollections);
            this.Controls.Add(this.lblWebs);
            this.Controls.Add(this.lblWebApps);
            this.Controls.Add(this.listWebApplications);
            this.Controls.Add(this.lblLog);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.txtResult);
            this.Name = "FrmMain";
            this.Text = "Feature Admin Tool v2010";
            this.Load += new System.EventHandler(this.FrmMain_Load);
            this.tabControl1.ResumeLayout(false);
            this.FarmFeatures.ResumeLayout(false);
            this.FarmFeatures.PerformLayout();
            this.RemoveFeatures.ResumeLayout(false);
            this.RemoveFeatures.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }



        private System.Windows.Forms.TextBox txtResult;
        private System.Windows.Forms.Label lblSPSiteFeatures;
        private System.Windows.Forms.CheckedListBox clbSPSiteFeatures;
        private System.Windows.Forms.Label lblSPWebFeatures;
        private System.Windows.Forms.CheckedListBox clbSPWebFeatures;
        private System.Windows.Forms.Button btnRemoveFromList;
        private System.Windows.Forms.Label lblLog;
        private System.Windows.Forms.ListBox listWebApplications;
        private System.Windows.Forms.Button btnListWebApplications;
        private System.Windows.Forms.Label lblWebApps;
        private System.Windows.Forms.Label lblSiteCollections;
        private System.Windows.Forms.ListBox listSiteCollections;
        private System.Windows.Forms.ListBox listSites;
        private System.Windows.Forms.Label lblWebs;
        private System.Windows.Forms.CheckedListBox clbFeatureDefinitions;
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
    
    }
}

