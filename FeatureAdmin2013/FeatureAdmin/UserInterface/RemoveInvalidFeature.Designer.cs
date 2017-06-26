namespace FeatureAdmin
{
    partial class RemoveInvalidFeature
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
            this.radioScopeWeb = new System.Windows.Forms.RadioButton();
            this.radioScopeSite = new System.Windows.Forms.RadioButton();
            this.radioScopeWebApp = new System.Windows.Forms.RadioButton();
            this.radioScopeFarm = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.grpFeatureScope = new System.Windows.Forms.GroupBox();
            this.btnScopeUnknown = new System.Windows.Forms.Button();
            this.btnScopeSelected = new System.Windows.Forms.Button();
            this.btnScopeCancel = new System.Windows.Forms.Button();
            this.grpFeatureScope.SuspendLayout();
            this.SuspendLayout();
            // 
            // radioScopeWeb
            // 
            this.radioScopeWeb.AutoSize = true;
            this.radioScopeWeb.Checked = true;
            this.radioScopeWeb.Location = new System.Drawing.Point(16, 21);
            this.radioScopeWeb.Name = "radioScopeWeb";
            this.radioScopeWeb.Size = new System.Drawing.Size(48, 17);
            this.radioScopeWeb.TabIndex = 0;
            this.radioScopeWeb.TabStop = true;
            this.radioScopeWeb.Text = "Web";
            this.radioScopeWeb.UseVisualStyleBackColor = true;
            // 
            // radioScopeSite
            // 
            this.radioScopeSite.AutoSize = true;
            this.radioScopeSite.Location = new System.Drawing.Point(16, 44);
            this.radioScopeSite.Name = "radioScopeSite";
            this.radioScopeSite.Size = new System.Drawing.Size(43, 17);
            this.radioScopeSite.TabIndex = 0;
            this.radioScopeSite.Text = "Site";
            this.radioScopeSite.UseVisualStyleBackColor = true;
            // 
            // radioScopeWebApp
            // 
            this.radioScopeWebApp.AutoSize = true;
            this.radioScopeWebApp.Location = new System.Drawing.Point(16, 67);
            this.radioScopeWebApp.Name = "radioScopeWebApp";
            this.radioScopeWebApp.Size = new System.Drawing.Size(100, 17);
            this.radioScopeWebApp.TabIndex = 0;
            this.radioScopeWebApp.Text = "WebApplication";
            this.radioScopeWebApp.UseVisualStyleBackColor = true;
            // 
            // radioScopeFarm
            // 
            this.radioScopeFarm.AutoSize = true;
            this.radioScopeFarm.Location = new System.Drawing.Point(16, 90);
            this.radioScopeFarm.Name = "radioScopeFarm";
            this.radioScopeFarm.Size = new System.Drawing.Size(48, 17);
            this.radioScopeFarm.TabIndex = 0;
            this.radioScopeFarm.Text = "Farm";
            this.radioScopeFarm.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(147, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Scope of the feature is invalid";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 26);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(151, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Please select scope, if known.";
            // 
            // grpFeatureScope
            // 
            this.grpFeatureScope.Controls.Add(this.radioScopeSite);
            this.grpFeatureScope.Controls.Add(this.radioScopeWeb);
            this.grpFeatureScope.Controls.Add(this.radioScopeWebApp);
            this.grpFeatureScope.Controls.Add(this.radioScopeFarm);
            this.grpFeatureScope.Location = new System.Drawing.Point(16, 52);
            this.grpFeatureScope.Name = "grpFeatureScope";
            this.grpFeatureScope.Size = new System.Drawing.Size(133, 124);
            this.grpFeatureScope.TabIndex = 2;
            this.grpFeatureScope.TabStop = false;
            this.grpFeatureScope.Text = "Feature Scope";
            // 
            // btnScopeUnknown
            // 
            this.btnScopeUnknown.Location = new System.Drawing.Point(169, 56);
            this.btnScopeUnknown.Name = "btnScopeUnknown";
            this.btnScopeUnknown.Size = new System.Drawing.Size(191, 36);
            this.btnScopeUnknown.TabIndex = 3;
            this.btnScopeUnknown.Text = "Scope not known, please check all possibilities in whole farm";
            this.btnScopeUnknown.UseVisualStyleBackColor = true;
            this.btnScopeUnknown.Click += new System.EventHandler(this.btnScopeUnknown_Click);
            // 
            // btnScopeSelected
            // 
            this.btnScopeSelected.Location = new System.Drawing.Point(169, 98);
            this.btnScopeSelected.Name = "btnScopeSelected";
            this.btnScopeSelected.Size = new System.Drawing.Size(191, 36);
            this.btnScopeSelected.TabIndex = 3;
            this.btnScopeSelected.Text = "Please check the selected Scope in the whole farm";
            this.btnScopeSelected.UseVisualStyleBackColor = true;
            this.btnScopeSelected.Click += new System.EventHandler(this.btnScopeSelected_Click);
            // 
            // btnScopeCancel
            // 
            this.btnScopeCancel.Location = new System.Drawing.Point(169, 140);
            this.btnScopeCancel.Name = "btnScopeCancel";
            this.btnScopeCancel.Size = new System.Drawing.Size(191, 36);
            this.btnScopeCancel.TabIndex = 3;
            this.btnScopeCancel.Text = "Cancel";
            this.btnScopeCancel.UseVisualStyleBackColor = true;
            this.btnScopeCancel.Click += new System.EventHandler(this.btnScopeCancel_Click);
            // 
            // FeatureScope
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(374, 193);
            this.Controls.Add(this.btnScopeCancel);
            this.Controls.Add(this.btnScopeSelected);
            this.Controls.Add(this.btnScopeUnknown);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.grpFeatureScope);
            this.Name = "FeatureScope";
            this.Text = "FeatureScope";
            this.grpFeatureScope.ResumeLayout(false);
            this.grpFeatureScope.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RadioButton radioScopeWeb;
        private System.Windows.Forms.RadioButton radioScopeSite;
        private System.Windows.Forms.RadioButton radioScopeWebApp;
        private System.Windows.Forms.RadioButton radioScopeFarm;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox grpFeatureScope;
        private System.Windows.Forms.Button btnScopeUnknown;
        private System.Windows.Forms.Button btnScopeSelected;
        private System.Windows.Forms.Button btnScopeCancel;
    }
}