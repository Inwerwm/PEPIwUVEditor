
namespace IwUVEditor.Subform
{
    partial class FormColorSettings
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormColorSettings));
            this.radioButtonBackground = new System.Windows.Forms.RadioButton();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panelBackgroundB = new System.Windows.Forms.Panel();
            this.panelBackground = new System.Windows.Forms.Panel();
            this.radioButtonVtxMesh = new System.Windows.Forms.RadioButton();
            this.radioButtonSelectedVtx = new System.Windows.Forms.RadioButton();
            this.panelVtxMeshB = new System.Windows.Forms.Panel();
            this.panelVtxMesh = new System.Windows.Forms.Panel();
            this.panelSelectedVtxB = new System.Windows.Forms.Panel();
            this.panelSelectedVtx = new System.Windows.Forms.Panel();
            this.radioButtonSelectionRect = new System.Windows.Forms.RadioButton();
            this.panelSelectionRectB = new System.Windows.Forms.Panel();
            this.panelSelectionRect = new System.Windows.Forms.Panel();
            this.colorSelector1 = new ColorSelector.ColorSelector();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabelForefront = new System.Windows.Forms.ToolStripStatusLabel();
            this.tableLayoutPanel1.SuspendLayout();
            this.panelBackgroundB.SuspendLayout();
            this.panelVtxMeshB.SuspendLayout();
            this.panelSelectedVtxB.SuspendLayout();
            this.panelSelectionRectB.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // radioButtonBackground
            // 
            this.radioButtonBackground.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.radioButtonBackground.AutoSize = true;
            this.radioButtonBackground.Font = new System.Drawing.Font("游ゴシック", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.radioButtonBackground.Location = new System.Drawing.Point(3, 228);
            this.radioButtonBackground.Name = "radioButtonBackground";
            this.radioButtonBackground.Size = new System.Drawing.Size(60, 69);
            this.radioButtonBackground.TabIndex = 0;
            this.radioButtonBackground.Text = "背景";
            this.radioButtonBackground.UseVisualStyleBackColor = true;
            this.radioButtonBackground.CheckedChanged += new System.EventHandler(this.RadioButton_CheckedChanged);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.radioButtonBackground, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.panelBackgroundB, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.radioButtonVtxMesh, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.radioButtonSelectedVtx, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.panelVtxMeshB, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.panelSelectedVtxB, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.radioButtonSelectionRect, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.panelSelectionRectB, 1, 2);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(12, 13);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(250, 300);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // panelBackgroundB
            // 
            this.panelBackgroundB.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelBackgroundB.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panelBackgroundB.BackgroundImage")));
            this.panelBackgroundB.Controls.Add(this.panelBackground);
            this.panelBackgroundB.Location = new System.Drawing.Point(128, 228);
            this.panelBackgroundB.Name = "panelBackgroundB";
            this.panelBackgroundB.Size = new System.Drawing.Size(119, 69);
            this.panelBackgroundB.TabIndex = 1;
            // 
            // panelBackground
            // 
            this.panelBackground.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelBackground.BackColor = System.Drawing.Color.White;
            this.panelBackground.Location = new System.Drawing.Point(0, 0);
            this.panelBackground.Name = "panelBackground";
            this.panelBackground.Size = new System.Drawing.Size(119, 69);
            this.panelBackground.TabIndex = 2;
            this.panelBackground.Click += new System.EventHandler(this.Panel_Click);
            // 
            // radioButtonVtxMesh
            // 
            this.radioButtonVtxMesh.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.radioButtonVtxMesh.AutoSize = true;
            this.radioButtonVtxMesh.Checked = true;
            this.radioButtonVtxMesh.Font = new System.Drawing.Font("游ゴシック", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.radioButtonVtxMesh.Location = new System.Drawing.Point(3, 3);
            this.radioButtonVtxMesh.Name = "radioButtonVtxMesh";
            this.radioButtonVtxMesh.Size = new System.Drawing.Size(60, 69);
            this.radioButtonVtxMesh.TabIndex = 0;
            this.radioButtonVtxMesh.TabStop = true;
            this.radioButtonVtxMesh.Text = "頂点";
            this.radioButtonVtxMesh.UseVisualStyleBackColor = true;
            this.radioButtonVtxMesh.CheckedChanged += new System.EventHandler(this.RadioButton_CheckedChanged);
            // 
            // radioButtonSelectedVtx
            // 
            this.radioButtonSelectedVtx.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.radioButtonSelectedVtx.AutoSize = true;
            this.radioButtonSelectedVtx.Font = new System.Drawing.Font("游ゴシック", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.radioButtonSelectedVtx.Location = new System.Drawing.Point(3, 78);
            this.radioButtonSelectedVtx.Name = "radioButtonSelectedVtx";
            this.radioButtonSelectedVtx.Size = new System.Drawing.Size(92, 69);
            this.radioButtonSelectedVtx.TabIndex = 0;
            this.radioButtonSelectedVtx.Text = "選択頂点";
            this.radioButtonSelectedVtx.UseVisualStyleBackColor = true;
            this.radioButtonSelectedVtx.CheckedChanged += new System.EventHandler(this.RadioButton_CheckedChanged);
            // 
            // panelVtxMeshB
            // 
            this.panelVtxMeshB.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelVtxMeshB.BackColor = System.Drawing.SystemColors.Control;
            this.panelVtxMeshB.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panelVtxMeshB.BackgroundImage")));
            this.panelVtxMeshB.Controls.Add(this.panelVtxMesh);
            this.panelVtxMeshB.Location = new System.Drawing.Point(128, 3);
            this.panelVtxMeshB.Name = "panelVtxMeshB";
            this.panelVtxMeshB.Size = new System.Drawing.Size(119, 69);
            this.panelVtxMeshB.TabIndex = 1;
            // 
            // panelVtxMesh
            // 
            this.panelVtxMesh.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelVtxMesh.BackColor = System.Drawing.Color.White;
            this.panelVtxMesh.Location = new System.Drawing.Point(0, 0);
            this.panelVtxMesh.Name = "panelVtxMesh";
            this.panelVtxMesh.Size = new System.Drawing.Size(119, 69);
            this.panelVtxMesh.TabIndex = 2;
            this.panelVtxMesh.Click += new System.EventHandler(this.Panel_Click);
            // 
            // panelSelectedVtxB
            // 
            this.panelSelectedVtxB.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelSelectedVtxB.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panelSelectedVtxB.BackgroundImage")));
            this.panelSelectedVtxB.Controls.Add(this.panelSelectedVtx);
            this.panelSelectedVtxB.Location = new System.Drawing.Point(128, 78);
            this.panelSelectedVtxB.Name = "panelSelectedVtxB";
            this.panelSelectedVtxB.Size = new System.Drawing.Size(119, 69);
            this.panelSelectedVtxB.TabIndex = 1;
            // 
            // panelSelectedVtx
            // 
            this.panelSelectedVtx.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelSelectedVtx.BackColor = System.Drawing.Color.White;
            this.panelSelectedVtx.Location = new System.Drawing.Point(0, 0);
            this.panelSelectedVtx.Name = "panelSelectedVtx";
            this.panelSelectedVtx.Size = new System.Drawing.Size(119, 69);
            this.panelSelectedVtx.TabIndex = 2;
            this.panelSelectedVtx.Click += new System.EventHandler(this.Panel_Click);
            // 
            // radioButtonSelectionRect
            // 
            this.radioButtonSelectionRect.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.radioButtonSelectionRect.AutoSize = true;
            this.radioButtonSelectionRect.Font = new System.Drawing.Font("游ゴシック", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.radioButtonSelectionRect.Location = new System.Drawing.Point(3, 153);
            this.radioButtonSelectionRect.Name = "radioButtonSelectionRect";
            this.radioButtonSelectionRect.Size = new System.Drawing.Size(92, 69);
            this.radioButtonSelectionRect.TabIndex = 0;
            this.radioButtonSelectionRect.Text = "範囲選択";
            this.radioButtonSelectionRect.UseVisualStyleBackColor = true;
            this.radioButtonSelectionRect.CheckedChanged += new System.EventHandler(this.RadioButton_CheckedChanged);
            // 
            // panelSelectionRectB
            // 
            this.panelSelectionRectB.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelSelectionRectB.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panelSelectionRectB.BackgroundImage")));
            this.panelSelectionRectB.Controls.Add(this.panelSelectionRect);
            this.panelSelectionRectB.Location = new System.Drawing.Point(128, 153);
            this.panelSelectionRectB.Name = "panelSelectionRectB";
            this.panelSelectionRectB.Size = new System.Drawing.Size(119, 69);
            this.panelSelectionRectB.TabIndex = 1;
            // 
            // panelSelectionRect
            // 
            this.panelSelectionRect.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelSelectionRect.BackColor = System.Drawing.Color.White;
            this.panelSelectionRect.Location = new System.Drawing.Point(0, 0);
            this.panelSelectionRect.Name = "panelSelectionRect";
            this.panelSelectionRect.Size = new System.Drawing.Size(119, 69);
            this.panelSelectionRect.TabIndex = 2;
            this.panelSelectionRect.Click += new System.EventHandler(this.Panel_Click);
            // 
            // colorSelector1
            // 
            this.colorSelector1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.colorSelector1.Color = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.colorSelector1.Font = new System.Drawing.Font("游ゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.colorSelector1.Location = new System.Drawing.Point(268, 13);
            this.colorSelector1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.colorSelector1.Name = "colorSelector1";
            this.colorSelector1.Size = new System.Drawing.Size(300, 300);
            this.colorSelector1.TabIndex = 2;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabelForefront});
            this.statusStrip1.Location = new System.Drawing.Point(0, 323);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(580, 24);
            this.statusStrip1.TabIndex = 3;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabelForefront
            // 
            this.toolStripStatusLabelForefront.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.toolStripStatusLabelForefront.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.toolStripStatusLabelForefront.Name = "toolStripStatusLabelForefront";
            this.toolStripStatusLabelForefront.Size = new System.Drawing.Size(71, 19);
            this.toolStripStatusLabelForefront.Text = "最前面表示";
            this.toolStripStatusLabelForefront.Click += new System.EventHandler(this.toolStripStatusLabelForefront_Click);
            // 
            // FormColorSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(580, 347);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.colorSelector1);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Font = new System.Drawing.Font("游ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "FormColorSettings";
            this.Text = "色設定";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormColorSettings_FormClosing);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.panelBackgroundB.ResumeLayout(false);
            this.panelVtxMeshB.ResumeLayout(false);
            this.panelSelectedVtxB.ResumeLayout(false);
            this.panelSelectionRectB.ResumeLayout(false);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RadioButton radioButtonBackground;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panelBackgroundB;
        private System.Windows.Forms.RadioButton radioButtonVtxMesh;
        private System.Windows.Forms.RadioButton radioButtonSelectedVtx;
        private System.Windows.Forms.Panel panelVtxMeshB;
        private System.Windows.Forms.Panel panelSelectedVtxB;
        private System.Windows.Forms.RadioButton radioButtonSelectionRect;
        private System.Windows.Forms.Panel panelSelectionRectB;
        private System.Windows.Forms.Panel panelBackground;
        private System.Windows.Forms.Panel panelVtxMesh;
        private System.Windows.Forms.Panel panelSelectedVtx;
        private System.Windows.Forms.Panel panelSelectionRect;
        private ColorSelector.ColorSelector colorSelector1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelForefront;
    }
}