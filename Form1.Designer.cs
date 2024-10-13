namespace UMLClassDiagramTranslator
{
    partial class mainMenu
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            label1 = new Label();
            btnCreate = new Button();
            btnLoad = new Button();
            btnLitTrans = new Button();
            btnModTrans = new Button();
            btnClose = new Button();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 16.2F, FontStyle.Bold, GraphicsUnit.Point);
            label1.Location = new Point(419, 31);
            label1.Name = "label1";
            label1.Size = new Size(500, 38);
            label1.TabIndex = 0;
            label1.Text = "UML CLASS DIAGRAM TRANSLATOR";
            // 
            // btnCreate
            // 
            btnCreate.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point);
            btnCreate.Location = new Point(482, 133);
            btnCreate.Name = "btnCreate";
            btnCreate.Size = new Size(346, 71);
            btnCreate.TabIndex = 2;
            btnCreate.Text = "CREATE CLASS DIAGRAM";
            btnCreate.UseVisualStyleBackColor = true;
            btnCreate.Click += btnCreate_Click;
            // 
            // btnLoad
            // 
            btnLoad.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point);
            btnLoad.Location = new Point(482, 265);
            btnLoad.Name = "btnLoad";
            btnLoad.Size = new Size(346, 71);
            btnLoad.TabIndex = 3;
            btnLoad.Text = "LOAD DIAGRAM";
            btnLoad.UseVisualStyleBackColor = true;
            btnLoad.Click += btnLoad_Click;
            // 
            // btnLitTrans
            // 
            btnLitTrans.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point);
            btnLitTrans.Location = new Point(482, 395);
            btnLitTrans.Name = "btnLitTrans";
            btnLitTrans.Size = new Size(346, 71);
            btnLitTrans.TabIndex = 4;
            btnLitTrans.Text = "LITERAL TRANSLATION";
            btnLitTrans.UseVisualStyleBackColor = true;
            btnLitTrans.Click += btnLitTrans_Click;
            // 
            // btnModTrans
            // 
            btnModTrans.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point);
            btnModTrans.Location = new Point(482, 522);
            btnModTrans.Name = "btnModTrans";
            btnModTrans.Size = new Size(346, 71);
            btnModTrans.TabIndex = 5;
            btnModTrans.Text = "MODEL TRANSLATION";
            btnModTrans.UseVisualStyleBackColor = true;
            btnModTrans.Click += btnModTrans_Click;
            // 
            // btnClose
            // 
            btnClose.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point);
            btnClose.Location = new Point(482, 651);
            btnClose.Name = "btnClose";
            btnClose.Size = new Size(346, 71);
            btnClose.TabIndex = 6;
            btnClose.Text = "CLOSE";
            btnClose.UseVisualStyleBackColor = true;
            btnClose.Click += btnClose_Click;
            // 
            // mainMenu
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1318, 812);
            Controls.Add(btnClose);
            Controls.Add(btnModTrans);
            Controls.Add(btnLitTrans);
            Controls.Add(btnLoad);
            Controls.Add(btnCreate);
            Controls.Add(label1);
            Name = "mainMenu";
            Text = "Form1";
            Load += Form1_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Button btnCreate;
        private Button btnLoad;
        private Button btnLitTrans;
        private Button btnModTrans;
        private Button btnClose;
    }
}