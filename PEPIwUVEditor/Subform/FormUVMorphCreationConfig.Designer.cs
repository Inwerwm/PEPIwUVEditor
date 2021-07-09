
namespace IwUVEditor.Subform
{
    partial class FormUVMorphCreationConfig
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.labelName = new System.Windows.Forms.Label();
            this.labelPanel = new System.Windows.Forms.Label();
            this.buttonOK = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.radioButtonBrow = new System.Windows.Forms.RadioButton();
            this.radioButtonEye = new System.Windows.Forms.RadioButton();
            this.radioButtonLip = new System.Windows.Forms.RadioButton();
            this.radioButtonOther = new System.Windows.Forms.RadioButton();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 4;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.Controls.Add(this.radioButtonOther, 3, 3);
            this.tableLayoutPanel1.Controls.Add(this.radioButtonLip, 2, 3);
            this.tableLayoutPanel1.Controls.Add(this.radioButtonEye, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.labelName, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.labelPanel, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.buttonOK, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.buttonCancel, 2, 4);
            this.tableLayoutPanel1.Controls.Add(this.textBox1, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.radioButtonBrow, 0, 3);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 5;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 11.76471F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 23.52941F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 11.76471F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 23.52941F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 29.41176F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(384, 211);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // labelName
            // 
            this.labelName.AutoSize = true;
            this.tableLayoutPanel1.SetColumnSpan(this.labelName, 4);
            this.labelName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelName.Location = new System.Drawing.Point(4, 0);
            this.labelName.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelName.Name = "labelName";
            this.labelName.Size = new System.Drawing.Size(376, 24);
            this.labelName.TabIndex = 0;
            this.labelName.Text = "モーフ名";
            this.labelName.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // labelPanel
            // 
            this.labelPanel.AutoSize = true;
            this.tableLayoutPanel1.SetColumnSpan(this.labelPanel, 4);
            this.labelPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelPanel.Location = new System.Drawing.Point(4, 73);
            this.labelPanel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelPanel.Name = "labelPanel";
            this.labelPanel.Size = new System.Drawing.Size(376, 24);
            this.labelPanel.TabIndex = 1;
            this.labelPanel.Text = "パネル";
            this.labelPanel.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // buttonOK
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.buttonOK, 2);
            this.buttonOK.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonOK.Location = new System.Drawing.Point(4, 166);
            this.buttonOK.Margin = new System.Windows.Forms.Padding(4, 20, 4, 5);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(184, 40);
            this.buttonOK.TabIndex = 2;
            this.buttonOK.Text = "OK";
            this.buttonOK.UseVisualStyleBackColor = true;
            // 
            // buttonCancel
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.buttonCancel, 2);
            this.buttonCancel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonCancel.Location = new System.Drawing.Point(196, 166);
            this.buttonCancel.Margin = new System.Windows.Forms.Padding(4, 20, 4, 5);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(184, 40);
            this.buttonCancel.TabIndex = 3;
            this.buttonCancel.Text = "キャンセル";
            this.buttonCancel.UseVisualStyleBackColor = true;
            // 
            // textBox1
            // 
            this.textBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.SetColumnSpan(this.textBox1, 4);
            this.textBox1.Location = new System.Drawing.Point(4, 32);
            this.textBox1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(376, 32);
            this.textBox1.TabIndex = 4;
            // 
            // radioButtonBrow
            // 
            this.radioButtonBrow.Appearance = System.Windows.Forms.Appearance.Button;
            this.radioButtonBrow.AutoSize = true;
            this.radioButtonBrow.Dock = System.Windows.Forms.DockStyle.Fill;
            this.radioButtonBrow.Location = new System.Drawing.Point(4, 102);
            this.radioButtonBrow.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.radioButtonBrow.Name = "radioButtonBrow";
            this.radioButtonBrow.Size = new System.Drawing.Size(88, 39);
            this.radioButtonBrow.TabIndex = 5;
            this.radioButtonBrow.Text = "眉";
            this.radioButtonBrow.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.radioButtonBrow.UseVisualStyleBackColor = true;
            // 
            // radioButtonEye
            // 
            this.radioButtonEye.Appearance = System.Windows.Forms.Appearance.Button;
            this.radioButtonEye.AutoSize = true;
            this.radioButtonEye.Dock = System.Windows.Forms.DockStyle.Fill;
            this.radioButtonEye.Location = new System.Drawing.Point(100, 102);
            this.radioButtonEye.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.radioButtonEye.Name = "radioButtonEye";
            this.radioButtonEye.Size = new System.Drawing.Size(88, 39);
            this.radioButtonEye.TabIndex = 6;
            this.radioButtonEye.Text = "目";
            this.radioButtonEye.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.radioButtonEye.UseVisualStyleBackColor = true;
            // 
            // radioButtonLip
            // 
            this.radioButtonLip.Appearance = System.Windows.Forms.Appearance.Button;
            this.radioButtonLip.AutoSize = true;
            this.radioButtonLip.Dock = System.Windows.Forms.DockStyle.Fill;
            this.radioButtonLip.Location = new System.Drawing.Point(196, 102);
            this.radioButtonLip.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.radioButtonLip.Name = "radioButtonLip";
            this.radioButtonLip.Size = new System.Drawing.Size(88, 39);
            this.radioButtonLip.TabIndex = 7;
            this.radioButtonLip.Text = "口";
            this.radioButtonLip.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.radioButtonLip.UseVisualStyleBackColor = true;
            // 
            // radioButtonOther
            // 
            this.radioButtonOther.Appearance = System.Windows.Forms.Appearance.Button;
            this.radioButtonOther.AutoSize = true;
            this.radioButtonOther.Checked = true;
            this.radioButtonOther.Dock = System.Windows.Forms.DockStyle.Fill;
            this.radioButtonOther.Location = new System.Drawing.Point(292, 102);
            this.radioButtonOther.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.radioButtonOther.Name = "radioButtonOther";
            this.radioButtonOther.Size = new System.Drawing.Size(88, 39);
            this.radioButtonOther.TabIndex = 8;
            this.radioButtonOther.TabStop = true;
            this.radioButtonOther.Text = "他";
            this.radioButtonOther.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.radioButtonOther.UseVisualStyleBackColor = true;
            // 
            // FormUVMorphCreationConfig
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(384, 211);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Font = new System.Drawing.Font("游ゴシック", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "FormUVMorphCreationConfig";
            this.Text = "UVモーフの作成";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label labelName;
        private System.Windows.Forms.Label labelPanel;
        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.RadioButton radioButtonBrow;
        private System.Windows.Forms.RadioButton radioButtonOther;
        private System.Windows.Forms.RadioButton radioButtonLip;
        private System.Windows.Forms.RadioButton radioButtonEye;
    }
}