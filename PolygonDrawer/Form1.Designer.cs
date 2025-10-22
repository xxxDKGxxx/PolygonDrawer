namespace PolygonDrawer
{
    partial class PolygonDrawer
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
            tableLayoutPanel1 = new TableLayoutPanel();
            mainCanvas = new PictureBox();
            flowLayoutPanel1 = new FlowLayoutPanel();
            groupBox1 = new GroupBox();
            resetButton = new Button();
            groupBox2 = new GroupBox();
            selectedEdgeLabel = new Label();
            selectedVertexLabel = new Label();
            label2 = new Label();
            label1 = new Label();
            groupBox3 = new GroupBox();
            deleteVertexButton = new Button();
            groupBox4 = new GroupBox();
            circularRadioButton = new RadioButton();
            bezierRadioButton = new RadioButton();
            edgeLengthTextBox = new TextBox();
            label3 = new Label();
            fixedRadioButton = new RadioButton();
            obliqueRadioButton = new RadioButton();
            normalRadioButton = new RadioButton();
            verticalRadioButton = new RadioButton();
            splitEdgeButton = new Button();
            tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)mainCanvas).BeginInit();
            flowLayoutPanel1.SuspendLayout();
            groupBox1.SuspendLayout();
            groupBox2.SuspendLayout();
            groupBox3.SuspendLayout();
            groupBox4.SuspendLayout();
            SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 2;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 60F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 40F));
            tableLayoutPanel1.Controls.Add(mainCanvas, 0, 0);
            tableLayoutPanel1.Controls.Add(flowLayoutPanel1, 1, 0);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(0, 0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 1;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Size = new Size(984, 561);
            tableLayoutPanel1.TabIndex = 0;
            // 
            // mainCanvas
            // 
            mainCanvas.Dock = DockStyle.Fill;
            mainCanvas.Location = new Point(3, 3);
            mainCanvas.Name = "mainCanvas";
            mainCanvas.Size = new Size(584, 555);
            mainCanvas.TabIndex = 0;
            mainCanvas.TabStop = false;
            // 
            // flowLayoutPanel1
            // 
            flowLayoutPanel1.Controls.Add(groupBox1);
            flowLayoutPanel1.Controls.Add(groupBox2);
            flowLayoutPanel1.Controls.Add(groupBox3);
            flowLayoutPanel1.Controls.Add(groupBox4);
            flowLayoutPanel1.Dock = DockStyle.Fill;
            flowLayoutPanel1.Location = new Point(593, 3);
            flowLayoutPanel1.Name = "flowLayoutPanel1";
            flowLayoutPanel1.Size = new Size(388, 555);
            flowLayoutPanel1.TabIndex = 1;
            // 
            // groupBox1
            // 
            groupBox1.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            groupBox1.Controls.Add(resetButton);
            groupBox1.Dock = DockStyle.Top;
            groupBox1.Location = new Point(3, 3);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(385, 100);
            groupBox1.TabIndex = 0;
            groupBox1.TabStop = false;
            groupBox1.Text = "groupBox1";
            // 
            // resetButton
            // 
            resetButton.Location = new Point(6, 22);
            resetButton.Name = "resetButton";
            resetButton.Size = new Size(75, 23);
            resetButton.TabIndex = 0;
            resetButton.Text = "Reset";
            resetButton.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            groupBox2.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            groupBox2.Controls.Add(selectedEdgeLabel);
            groupBox2.Controls.Add(selectedVertexLabel);
            groupBox2.Controls.Add(label2);
            groupBox2.Controls.Add(label1);
            groupBox2.Dock = DockStyle.Top;
            groupBox2.Location = new Point(3, 109);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(385, 100);
            groupBox2.TabIndex = 1;
            groupBox2.TabStop = false;
            groupBox2.Text = "Info";
            // 
            // selectedEdgeLabel
            // 
            selectedEdgeLabel.AutoSize = true;
            selectedEdgeLabel.Location = new Point(100, 44);
            selectedEdgeLabel.Name = "selectedEdgeLabel";
            selectedEdgeLabel.Size = new Size(0, 15);
            selectedEdgeLabel.TabIndex = 3;
            // 
            // selectedVertexLabel
            // 
            selectedVertexLabel.AutoSize = true;
            selectedVertexLabel.Location = new Point(100, 19);
            selectedVertexLabel.Name = "selectedVertexLabel";
            selectedVertexLabel.Size = new Size(0, 15);
            selectedVertexLabel.TabIndex = 2;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(6, 44);
            label2.Name = "label2";
            label2.Size = new Size(83, 15);
            label2.TabIndex = 1;
            label2.Text = "Selected Edge:";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(6, 19);
            label1.Name = "label1";
            label1.Size = new Size(88, 15);
            label1.TabIndex = 0;
            label1.Text = "Selected Vertex:";
            // 
            // groupBox3
            // 
            groupBox3.Controls.Add(deleteVertexButton);
            groupBox3.Dock = DockStyle.Top;
            groupBox3.Location = new Point(3, 215);
            groupBox3.Name = "groupBox3";
            groupBox3.Size = new Size(385, 100);
            groupBox3.TabIndex = 2;
            groupBox3.TabStop = false;
            groupBox3.Text = "Vertex Controls";
            // 
            // deleteVertexButton
            // 
            deleteVertexButton.Location = new Point(6, 22);
            deleteVertexButton.Name = "deleteVertexButton";
            deleteVertexButton.Size = new Size(75, 23);
            deleteVertexButton.TabIndex = 0;
            deleteVertexButton.Text = "Delete";
            deleteVertexButton.UseVisualStyleBackColor = true;
            // 
            // groupBox4
            // 
            groupBox4.Controls.Add(circularRadioButton);
            groupBox4.Controls.Add(bezierRadioButton);
            groupBox4.Controls.Add(edgeLengthTextBox);
            groupBox4.Controls.Add(label3);
            groupBox4.Controls.Add(fixedRadioButton);
            groupBox4.Controls.Add(obliqueRadioButton);
            groupBox4.Controls.Add(normalRadioButton);
            groupBox4.Controls.Add(verticalRadioButton);
            groupBox4.Controls.Add(splitEdgeButton);
            groupBox4.Dock = DockStyle.Top;
            groupBox4.Location = new Point(3, 321);
            groupBox4.Name = "groupBox4";
            groupBox4.Size = new Size(385, 142);
            groupBox4.TabIndex = 3;
            groupBox4.TabStop = false;
            groupBox4.Text = "Edge Controls";
            // 
            // circularRadioButton
            // 
            circularRadioButton.AutoSize = true;
            circularRadioButton.Location = new Point(105, 83);
            circularRadioButton.Name = "circularRadioButton";
            circularRadioButton.Size = new Size(66, 19);
            circularRadioButton.TabIndex = 8;
            circularRadioButton.TabStop = true;
            circularRadioButton.Text = "Circular";
            circularRadioButton.UseVisualStyleBackColor = true;
            // 
            // bezierRadioButton
            // 
            bezierRadioButton.AutoSize = true;
            bezierRadioButton.Location = new Point(9, 83);
            bezierRadioButton.Name = "bezierRadioButton";
            bezierRadioButton.Size = new Size(90, 19);
            bezierRadioButton.TabIndex = 7;
            bezierRadioButton.TabStop = true;
            bezierRadioButton.Text = "Bezier Curve";
            bezierRadioButton.UseVisualStyleBackColor = true;
            // 
            // edgeLengthTextBox
            // 
            edgeLengthTextBox.Location = new Point(87, 51);
            edgeLengthTextBox.Name = "edgeLengthTextBox";
            edgeLengthTextBox.Size = new Size(100, 23);
            edgeLengthTextBox.TabIndex = 6;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(7, 54);
            label3.Name = "label3";
            label3.Size = new Size(74, 15);
            label3.TabIndex = 5;
            label3.Text = "FixedLength:";
            // 
            // fixedRadioButton
            // 
            fixedRadioButton.AutoSize = true;
            fixedRadioButton.Location = new Point(293, 22);
            fixedRadioButton.Name = "fixedRadioButton";
            fixedRadioButton.Size = new Size(52, 19);
            fixedRadioButton.TabIndex = 4;
            fixedRadioButton.TabStop = true;
            fixedRadioButton.Text = "Fixed";
            fixedRadioButton.UseVisualStyleBackColor = true;
            // 
            // obliqueRadioButton
            // 
            obliqueRadioButton.AutoSize = true;
            obliqueRadioButton.Location = new Point(158, 22);
            obliqueRadioButton.Name = "obliqueRadioButton";
            obliqueRadioButton.Size = new Size(60, 19);
            obliqueRadioButton.TabIndex = 3;
            obliqueRadioButton.TabStop = true;
            obliqueRadioButton.Text = "45 deg";
            obliqueRadioButton.UseVisualStyleBackColor = true;
            // 
            // normalRadioButton
            // 
            normalRadioButton.AutoSize = true;
            normalRadioButton.Location = new Point(222, 22);
            normalRadioButton.Name = "normalRadioButton";
            normalRadioButton.Size = new Size(65, 19);
            normalRadioButton.TabIndex = 2;
            normalRadioButton.TabStop = true;
            normalRadioButton.Text = "Normal";
            normalRadioButton.UseVisualStyleBackColor = true;
            // 
            // verticalRadioButton
            // 
            verticalRadioButton.AutoSize = true;
            verticalRadioButton.FlatStyle = FlatStyle.Flat;
            verticalRadioButton.Location = new Point(90, 22);
            verticalRadioButton.Name = "verticalRadioButton";
            verticalRadioButton.Size = new Size(62, 19);
            verticalRadioButton.TabIndex = 1;
            verticalRadioButton.Text = "Vertical";
            verticalRadioButton.UseVisualStyleBackColor = true;
            // 
            // splitEdgeButton
            // 
            splitEdgeButton.Location = new Point(1, 18);
            splitEdgeButton.Name = "splitEdgeButton";
            splitEdgeButton.Size = new Size(75, 23);
            splitEdgeButton.TabIndex = 0;
            splitEdgeButton.Text = "Split";
            splitEdgeButton.UseVisualStyleBackColor = true;
            // 
            // PolygonDrawer
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(984, 561);
            Controls.Add(tableLayoutPanel1);
            FormBorderStyle = FormBorderStyle.Fixed3D;
            Name = "PolygonDrawer";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Form1";
            tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)mainCanvas).EndInit();
            flowLayoutPanel1.ResumeLayout(false);
            groupBox1.ResumeLayout(false);
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            groupBox3.ResumeLayout(false);
            groupBox4.ResumeLayout(false);
            groupBox4.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private TableLayoutPanel tableLayoutPanel1;
        private PictureBox mainCanvas;
        private FlowLayoutPanel flowLayoutPanel1;
        private GroupBox groupBox1;
        private Button resetButton;
        private GroupBox groupBox2;
        private Label selectedVertexLabel;
        private Label label2;
        private Label label1;
        private Label selectedEdgeLabel;
        private GroupBox groupBox3;
        private Button deleteVertexButton;
        private GroupBox groupBox4;
        private Button splitEdgeButton;
        private RadioButton verticalRadioButton;
        private RadioButton normalRadioButton;
        private RadioButton obliqueRadioButton;
        private RadioButton fixedRadioButton;
        private TextBox edgeLengthTextBox;
        private Label label3;
        private RadioButton bezierRadioButton;
        private RadioButton circularRadioButton;
    }
}
