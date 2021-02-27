
namespace IwUVEditor.Subform
{
    partial class FormSaveSelection
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
            this.components = new System.ComponentModel.Container();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.buttonApply = new System.Windows.Forms.Button();
            this.buttonSave = new System.Windows.Forms.Button();
            this.contextMenuStripSelection = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.削除ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dataGridViewSelections = new System.Windows.Forms.DataGridView();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabelForefront = new System.Windows.Forms.ToolStripStatusLabel();
            this.SelectionName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SelectionCount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SelectionMaterial = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SelectionCommand = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tableLayoutPanel1.SuspendLayout();
            this.contextMenuStripSelection.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewSelections)).BeginInit();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.buttonApply, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.buttonSave, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.dataGridViewSelections, 0, 1);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 48F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(484, 437);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // buttonApply
            // 
            this.buttonApply.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonApply.Location = new System.Drawing.Point(3, 3);
            this.buttonApply.Name = "buttonApply";
            this.buttonApply.Size = new System.Drawing.Size(236, 42);
            this.buttonApply.TabIndex = 1;
            this.buttonApply.Text = "適用";
            this.buttonApply.UseVisualStyleBackColor = true;
            this.buttonApply.Click += new System.EventHandler(this.buttonApply_Click);
            // 
            // buttonSave
            // 
            this.buttonSave.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonSave.Location = new System.Drawing.Point(245, 3);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(236, 42);
            this.buttonSave.TabIndex = 2;
            this.buttonSave.Text = "保存";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // contextMenuStripSelection
            // 
            this.contextMenuStripSelection.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.削除ToolStripMenuItem});
            this.contextMenuStripSelection.Name = "contextMenuStripSelection";
            this.contextMenuStripSelection.Size = new System.Drawing.Size(181, 48);
            // 
            // 削除ToolStripMenuItem
            // 
            this.削除ToolStripMenuItem.Name = "削除ToolStripMenuItem";
            this.削除ToolStripMenuItem.ShortcutKeyDisplayString = "Delete";
            this.削除ToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.削除ToolStripMenuItem.Text = "削除(&D)";
            this.削除ToolStripMenuItem.Click += new System.EventHandler(this.削除ToolStripMenuItem_Click);
            // 
            // dataGridViewSelections
            // 
            this.dataGridViewSelections.AllowUserToAddRows = false;
            this.dataGridViewSelections.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewSelections.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.SelectionName,
            this.SelectionCount,
            this.SelectionMaterial,
            this.SelectionCommand});
            this.tableLayoutPanel1.SetColumnSpan(this.dataGridViewSelections, 2);
            this.dataGridViewSelections.ContextMenuStrip = this.contextMenuStripSelection;
            this.dataGridViewSelections.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridViewSelections.Location = new System.Drawing.Point(3, 51);
            this.dataGridViewSelections.MultiSelect = false;
            this.dataGridViewSelections.Name = "dataGridViewSelections";
            this.dataGridViewSelections.RowTemplate.Height = 21;
            this.dataGridViewSelections.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewSelections.Size = new System.Drawing.Size(478, 383);
            this.dataGridViewSelections.TabIndex = 4;
            this.dataGridViewSelections.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridViewSelections_CellMouseDown);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabelForefront});
            this.statusStrip1.Location = new System.Drawing.Point(0, 437);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(484, 24);
            this.statusStrip1.TabIndex = 2;
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
            // SelectionName
            // 
            this.SelectionName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.SelectionName.HeaderText = "名前";
            this.SelectionName.Name = "SelectionName";
            this.SelectionName.Width = 64;
            // 
            // SelectionCount
            // 
            this.SelectionCount.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.SelectionCount.HeaderText = "選択頂点数";
            this.SelectionCount.Name = "SelectionCount";
            this.SelectionCount.ReadOnly = true;
            this.SelectionCount.Width = 109;
            // 
            // SelectionMaterial
            // 
            this.SelectionMaterial.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.SelectionMaterial.HeaderText = "材質";
            this.SelectionMaterial.Name = "SelectionMaterial";
            this.SelectionMaterial.ReadOnly = true;
            this.SelectionMaterial.Width = 64;
            // 
            // SelectionCommand
            // 
            this.SelectionCommand.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.SelectionCommand.HeaderText = "コマンド";
            this.SelectionCommand.Name = "SelectionCommand";
            this.SelectionCommand.ReadOnly = true;
            this.SelectionCommand.Visible = false;
            // 
            // FormSaveSelection
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(484, 461);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Font = new System.Drawing.Font("游ゴシック", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MinimumSize = new System.Drawing.Size(250, 250);
            this.Name = "FormSaveSelection";
            this.Text = "選択保存";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.contextMenuStripSelection.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewSelections)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Button buttonApply;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelForefront;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripSelection;
        private System.Windows.Forms.ToolStripMenuItem 削除ToolStripMenuItem;
        private System.Windows.Forms.DataGridView dataGridViewSelections;
        private System.Windows.Forms.DataGridViewTextBoxColumn SelectionName;
        private System.Windows.Forms.DataGridViewTextBoxColumn SelectionCount;
        private System.Windows.Forms.DataGridViewTextBoxColumn SelectionMaterial;
        private System.Windows.Forms.DataGridViewTextBoxColumn SelectionCommand;
    }
}