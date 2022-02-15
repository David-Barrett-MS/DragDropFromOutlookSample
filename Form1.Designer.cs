namespace DragDropFromOutlookSample
{
    partial class Form1
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
            this.groupBoxDropTarget = new System.Windows.Forms.GroupBox();
            this.buttonClose = new System.Windows.Forms.Button();
            this.groupBoxItemInfo = new System.Windows.Forms.GroupBox();
            this.textBoxItemInfo = new System.Windows.Forms.TextBox();
            this.checkBoxSaveItems = new System.Windows.Forms.CheckBox();
            this.groupBoxItemInfo.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBoxDropTarget
            // 
            this.groupBoxDropTarget.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxDropTarget.Location = new System.Drawing.Point(6, 6);
            this.groupBoxDropTarget.Name = "groupBoxDropTarget";
            this.groupBoxDropTarget.Size = new System.Drawing.Size(312, 100);
            this.groupBoxDropTarget.TabIndex = 0;
            this.groupBoxDropTarget.TabStop = false;
            this.groupBoxDropTarget.Text = "Drop Target";
            // 
            // buttonClose
            // 
            this.buttonClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonClose.Location = new System.Drawing.Point(324, 12);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(75, 23);
            this.buttonClose.TabIndex = 1;
            this.buttonClose.Text = "Close";
            this.buttonClose.UseVisualStyleBackColor = true;
            this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
            // 
            // groupBoxItemInfo
            // 
            this.groupBoxItemInfo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxItemInfo.Controls.Add(this.textBoxItemInfo);
            this.groupBoxItemInfo.Location = new System.Drawing.Point(6, 112);
            this.groupBoxItemInfo.Name = "groupBoxItemInfo";
            this.groupBoxItemInfo.Size = new System.Drawing.Size(393, 175);
            this.groupBoxItemInfo.TabIndex = 2;
            this.groupBoxItemInfo.TabStop = false;
            this.groupBoxItemInfo.Text = "Dropped Item(s) Information (shows available formats on drag)";
            // 
            // textBoxItemInfo
            // 
            this.textBoxItemInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxItemInfo.Location = new System.Drawing.Point(3, 16);
            this.textBoxItemInfo.Multiline = true;
            this.textBoxItemInfo.Name = "textBoxItemInfo";
            this.textBoxItemInfo.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBoxItemInfo.Size = new System.Drawing.Size(387, 156);
            this.textBoxItemInfo.TabIndex = 0;
            // 
            // checkBoxSaveItems
            // 
            this.checkBoxSaveItems.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBoxSaveItems.AutoSize = true;
            this.checkBoxSaveItems.Enabled = false;
            this.checkBoxSaveItems.Location = new System.Drawing.Point(324, 89);
            this.checkBoxSaveItems.Name = "checkBoxSaveItems";
            this.checkBoxSaveItems.Size = new System.Drawing.Size(78, 17);
            this.checkBoxSaveItems.TabIndex = 3;
            this.checkBoxSaveItems.Text = "Save items";
            this.checkBoxSaveItems.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(404, 291);
            this.Controls.Add(this.checkBoxSaveItems);
            this.Controls.Add(this.groupBoxItemInfo);
            this.Controls.Add(this.buttonClose);
            this.Controls.Add(this.groupBoxDropTarget);
            this.Name = "Form1";
            this.Text = "Drag/Drop Sample";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBoxItemInfo.ResumeLayout(false);
            this.groupBoxItemInfo.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBoxDropTarget;
        private System.Windows.Forms.Button buttonClose;
        private System.Windows.Forms.GroupBox groupBoxItemInfo;
        private System.Windows.Forms.TextBox textBoxItemInfo;
        private System.Windows.Forms.CheckBox checkBoxSaveItems;
    }
}

