namespace FeatureAdmin
{
    partial class LocationForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
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
            this.MainSplitContainer = new System.Windows.Forms.SplitContainer();
            this.FeatureTitlePanel = new System.Windows.Forms.Panel();
            this.FeaturePanelCaption = new System.Windows.Forms.Label();
            this.LocationGrid = new System.Windows.Forms.DataGridView();
            this.LocationDetailsView = new System.Windows.Forms.ListView();
            this.DeactivateButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.MainSplitContainer)).BeginInit();
            this.MainSplitContainer.Panel1.SuspendLayout();
            this.MainSplitContainer.Panel2.SuspendLayout();
            this.MainSplitContainer.SuspendLayout();
            this.FeatureTitlePanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.LocationGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // MainSplitContainer
            // 
            this.MainSplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MainSplitContainer.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.MainSplitContainer.Location = new System.Drawing.Point(0, 0);
            this.MainSplitContainer.Name = "MainSplitContainer";
            this.MainSplitContainer.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // MainSplitContainer.Panel1
            // 
            this.MainSplitContainer.Panel1.Controls.Add(this.FeatureTitlePanel);
            this.MainSplitContainer.Panel1.Controls.Add(this.LocationGrid);
            // 
            // MainSplitContainer.Panel2
            // 
            this.MainSplitContainer.Panel2.Controls.Add(this.LocationDetailsView);
            this.MainSplitContainer.Panel2.Controls.Add(this.DeactivateButton);
            this.MainSplitContainer.Size = new System.Drawing.Size(539, 503);
            this.MainSplitContainer.SplitterDistance = 321;
            this.MainSplitContainer.TabIndex = 0;
            // 
            // FeatureTitlePanel
            // 
            this.FeatureTitlePanel.Controls.Add(this.FeaturePanelCaption);
            this.FeatureTitlePanel.Location = new System.Drawing.Point(3, 3);
            this.FeatureTitlePanel.Name = "FeatureTitlePanel";
            this.FeatureTitlePanel.Size = new System.Drawing.Size(524, 30);
            this.FeatureTitlePanel.TabIndex = 3;
            // 
            // FeaturePanelCaption
            // 
            this.FeaturePanelCaption.AutoSize = true;
            this.FeaturePanelCaption.Location = new System.Drawing.Point(9, 6);
            this.FeaturePanelCaption.Name = "FeaturePanelCaption";
            this.FeaturePanelCaption.Size = new System.Drawing.Size(92, 13);
            this.FeaturePanelCaption.TabIndex = 0;
            this.FeaturePanelCaption.Text = "Feature Locations";
            // 
            // LocationGrid
            // 
            this.LocationGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.LocationGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.LocationGrid.Location = new System.Drawing.Point(0, 39);
            this.LocationGrid.Name = "LocationGrid";
            this.LocationGrid.ReadOnly = true;
            this.LocationGrid.Size = new System.Drawing.Size(536, 279);
            this.LocationGrid.TabIndex = 2;
            this.LocationGrid.SelectionChanged += new System.EventHandler(this.LocationGrid_SelectionChanged);
            // 
            // LocationDetailsView
            // 
            this.LocationDetailsView.Location = new System.Drawing.Point(4, 32);
            this.LocationDetailsView.Name = "LocationDetailsView";
            this.LocationDetailsView.Size = new System.Drawing.Size(532, 143);
            this.LocationDetailsView.TabIndex = 1;
            this.LocationDetailsView.UseCompatibleStateImageBehavior = false;
            this.LocationDetailsView.View = System.Windows.Forms.View.Details;
            // 
            // DeactivateButton
            // 
            this.DeactivateButton.Location = new System.Drawing.Point(184, 3);
            this.DeactivateButton.Name = "DeactivateButton";
            this.DeactivateButton.Size = new System.Drawing.Size(200, 23);
            this.DeactivateButton.TabIndex = 0;
            this.DeactivateButton.Text = "Deactivate Selected Activations";
            this.DeactivateButton.UseVisualStyleBackColor = true;
            // 
            // LocationForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(539, 503);
            this.Controls.Add(this.MainSplitContainer);
            this.Name = "LocationForm";
            this.Text = "LocationForm";
            this.MainSplitContainer.Panel1.ResumeLayout(false);
            this.MainSplitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.MainSplitContainer)).EndInit();
            this.MainSplitContainer.ResumeLayout(false);
            this.FeatureTitlePanel.ResumeLayout(false);
            this.FeatureTitlePanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.LocationGrid)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer MainSplitContainer;
        private System.Windows.Forms.DataGridView LocationGrid;
        private System.Windows.Forms.Panel FeatureTitlePanel;
        private System.Windows.Forms.Label FeaturePanelCaption;
        private System.Windows.Forms.ListView LocationDetailsView;
        private System.Windows.Forms.Button DeactivateButton;

    }
}