namespace PolygonDrawer
{
    partial class HelpForm
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
            mainTextBox = new RichTextBox();
            SuspendLayout();
            // 
            // mainTextBox
            // 
            mainTextBox.Location = new Point(20, 26);
            mainTextBox.Name = "mainTextBox";
            mainTextBox.ReadOnly = true;
            mainTextBox.Size = new Size(420, 320);
            mainTextBox.TabIndex = 0;
            mainTextBox.Text = "";
            // 
            // HelpForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(466, 384);
            Controls.Add(mainTextBox);
            MaximizeBox = false;
            Name = "HelpForm";
            Text = "HelpForm";
            ResumeLayout(false);
        }

        #endregion

        private RichTextBox mainTextBox;
    }
}