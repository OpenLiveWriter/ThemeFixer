namespace ThemeFixer
{
    partial class MainWindow
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
            this.comboBoxBlogs = new System.Windows.Forms.ComboBox();
            this.buttonSave = new System.Windows.Forms.Button();
            this.textBoxUrl = new System.Windows.Forms.TextBox();
            this.buttonNavigate = new System.Windows.Forms.Button();
            this.webBrowser = new System.Windows.Forms.WebBrowser();
            this.buttonSelectBody = new System.Windows.Forms.Button();
            this.buttonSelectTitle = new System.Windows.Forms.Button();
            this.buttonOpenFolder = new System.Windows.Forms.Button();
            this.labelStatus = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // comboBoxBlogs
            // 
            this.comboBoxBlogs.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxBlogs.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBoxBlogs.FormattingEnabled = true;
            this.comboBoxBlogs.Location = new System.Drawing.Point(12, 12);
            this.comboBoxBlogs.Name = "comboBoxBlogs";
            this.comboBoxBlogs.Size = new System.Drawing.Size(216, 23);
            this.comboBoxBlogs.TabIndex = 0;
            // 
            // buttonSave
            // 
            this.buttonSave.Enabled = false;
            this.buttonSave.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonSave.Location = new System.Drawing.Point(234, 10);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(75, 23);
            this.buttonSave.TabIndex = 1;
            this.buttonSave.Text = "Save";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // textBoxUrl
            // 
            this.textBoxUrl.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxUrl.Location = new System.Drawing.Point(12, 39);
            this.textBoxUrl.Name = "textBoxUrl";
            this.textBoxUrl.Size = new System.Drawing.Size(216, 23);
            this.textBoxUrl.TabIndex = 2;
            this.textBoxUrl.Enter += new System.EventHandler(this.textBoxUrl_Enter);
            // 
            // buttonNavigate
            // 
            this.buttonNavigate.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonNavigate.Location = new System.Drawing.Point(234, 36);
            this.buttonNavigate.Name = "buttonNavigate";
            this.buttonNavigate.Size = new System.Drawing.Size(75, 23);
            this.buttonNavigate.TabIndex = 3;
            this.buttonNavigate.Text = "Navigate";
            this.buttonNavigate.UseVisualStyleBackColor = true;
            this.buttonNavigate.Click += new System.EventHandler(this.buttonNavigate_Click);
            // 
            // webBrowser
            // 
            this.webBrowser.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.webBrowser.Location = new System.Drawing.Point(-1, 76);
            this.webBrowser.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowser.Name = "webBrowser";
            this.webBrowser.ScriptErrorsSuppressed = true;
            this.webBrowser.Size = new System.Drawing.Size(764, 295);
            this.webBrowser.TabIndex = 4;
            // 
            // buttonSelectBody
            // 
            this.buttonSelectBody.Enabled = false;
            this.buttonSelectBody.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonSelectBody.Location = new System.Drawing.Point(422, 10);
            this.buttonSelectBody.Name = "buttonSelectBody";
            this.buttonSelectBody.Size = new System.Drawing.Size(75, 23);
            this.buttonSelectBody.TabIndex = 5;
            this.buttonSelectBody.Text = "Pick Body";
            this.buttonSelectBody.UseVisualStyleBackColor = true;
            this.buttonSelectBody.Click += new System.EventHandler(this.buttonSelectBody_Click);
            // 
            // buttonSelectTitle
            // 
            this.buttonSelectTitle.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonSelectTitle.Location = new System.Drawing.Point(341, 10);
            this.buttonSelectTitle.Name = "buttonSelectTitle";
            this.buttonSelectTitle.Size = new System.Drawing.Size(75, 23);
            this.buttonSelectTitle.TabIndex = 6;
            this.buttonSelectTitle.Text = "Pick Title";
            this.buttonSelectTitle.UseVisualStyleBackColor = true;
            this.buttonSelectTitle.Click += new System.EventHandler(this.buttonSelectTitle_Click);
            // 
            // buttonOpenFolder
            // 
            this.buttonOpenFolder.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonOpenFolder.Location = new System.Drawing.Point(341, 39);
            this.buttonOpenFolder.Name = "buttonOpenFolder";
            this.buttonOpenFolder.Size = new System.Drawing.Size(156, 23);
            this.buttonOpenFolder.TabIndex = 7;
            this.buttonOpenFolder.Text = "Open Folder to HTML Files";
            this.buttonOpenFolder.UseVisualStyleBackColor = true;
            this.buttonOpenFolder.Click += new System.EventHandler(this.buttonOpenFolder_Click);
            // 
            // labelStatus
            // 
            this.labelStatus.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelStatus.Location = new System.Drawing.Point(519, 13);
            this.labelStatus.Name = "labelStatus";
            this.labelStatus.Size = new System.Drawing.Size(230, 49);
            this.labelStatus.TabIndex = 8;
            this.labelStatus.Text = "Navigate to a blog post to create your template from, then click \'Pick Tite\'";
            // 
            // MainWindow
            // 
            this.AcceptButton = this.buttonNavigate;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(761, 371);
            this.Controls.Add(this.labelStatus);
            this.Controls.Add(this.buttonOpenFolder);
            this.Controls.Add(this.buttonSelectTitle);
            this.Controls.Add(this.buttonSelectBody);
            this.Controls.Add(this.webBrowser);
            this.Controls.Add(this.buttonNavigate);
            this.Controls.Add(this.textBoxUrl);
            this.Controls.Add(this.buttonSave);
            this.Controls.Add(this.comboBoxBlogs);
            this.Name = "MainWindow";
            this.Text = "Windows Live Writer Theme Creator";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox comboBoxBlogs;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.TextBox textBoxUrl;
        private System.Windows.Forms.Button buttonNavigate;
        private System.Windows.Forms.WebBrowser webBrowser;
        private System.Windows.Forms.Button buttonSelectBody;
        private System.Windows.Forms.Button buttonSelectTitle;
        private System.Windows.Forms.Button buttonOpenFolder;
        private System.Windows.Forms.Label labelStatus;
    }
}

