namespace UMLClassDiagramTranslator
{
    partial class LoadForm
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
            btnBrowse = new Button();
            lblFile = new Label();
            lblConfirm = new Label();
            btnClose = new Button();
            SuspendLayout();
            // 
            // btnBrowse
            // 
            btnBrowse.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
            btnBrowse.Location = new Point(542, 55);
            btnBrowse.Name = "btnBrowse";
            btnBrowse.Size = new Size(110, 45);
            btnBrowse.TabIndex = 0;
            btnBrowse.Text = "Browse File";
            btnBrowse.UseVisualStyleBackColor = true;
            btnBrowse.Click += btnBrowse_Click;
            // 
            // lblFile
            // 
            lblFile.AutoSize = true;
            lblFile.Location = new Point(447, 185);
            lblFile.Name = "lblFile";
            lblFile.Size = new Size(0, 20);
            lblFile.TabIndex = 1;
            // 
            // lblConfirm
            // 
            lblConfirm.AutoSize = true;
            lblConfirm.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            lblConfirm.Location = new Point(542, 221);
            lblConfirm.Name = "lblConfirm";
            lblConfirm.Size = new Size(0, 20);
            lblConfirm.TabIndex = 3;
            // 
            // btnClose
            // 
            btnClose.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
            btnClose.Location = new Point(542, 501);
            btnClose.Name = "btnClose";
            btnClose.Size = new Size(110, 45);
            btnClose.TabIndex = 4;
            btnClose.Text = "Close";
            btnClose.UseVisualStyleBackColor = true;
            btnClose.Click += btnClose_Click;
            // 
            // LoadForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1289, 717);
            Controls.Add(btnClose);
            Controls.Add(lblConfirm);
            Controls.Add(lblFile);
            Controls.Add(btnBrowse);
            Name = "LoadForm";
            Text = "LoadForm";
            Load += LoadForm_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnBrowse;
        private Label lblFile;
        private Label lblConfirm;
        private Button btnClose;
    }
}