
using System;

namespace IwUVEditor
{
    partial class FormEditor
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
                ColorSettings.Dispose();
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
            this.splitCtrlView = new System.Windows.Forms.SplitContainer();
            this.flowEditTools = new System.Windows.Forms.FlowLayoutPanel();
            this.radioButtonRectangleSelection = new System.Windows.Forms.RadioButton();
            this.radioButtonLassoSelection = new System.Windows.Forms.RadioButton();
            this.radioButtonMove = new System.Windows.Forms.RadioButton();
            this.radioButtonRotate = new System.Windows.Forms.RadioButton();
            this.buttonReverseV = new System.Windows.Forms.Button();
            this.buttonReverseH = new System.Windows.Forms.Button();
            this.buttonResetCamera = new System.Windows.Forms.Button();
            this.numericRadiusOfPosSq = new System.Windows.Forms.NumericUpDown();
            this.splitUVMat = new System.Windows.Forms.SplitContainer();
            this.listBoxMaterial = new System.Windows.Forms.ListBox();
            this.statusStripEditor = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabelFPS = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabelState = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripProgressBarState = new System.Windows.Forms.ToolStripProgressBar();
            this.menuStripEditor = new System.Windows.Forms.MenuStrip();
            this.再読込ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.再読込ToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.uVを反映ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.選択頂点を受信ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.選択頂点を送信ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.編集ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.元に戻すToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.やり直しToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.色を変更ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.頂点選択保存ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.描画リミッター解除ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.座標のコピーToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.座標のペーストToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.テクスチャToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.選択材質のテクスチャを変更ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.uV情報を合成して保存ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.timerEvery = new System.Windows.Forms.Timer(this.components);
            this.radioButtonScale = new System.Windows.Forms.RadioButton();
            ((System.ComponentModel.ISupportInitialize)(this.splitCtrlView)).BeginInit();
            this.splitCtrlView.Panel1.SuspendLayout();
            this.splitCtrlView.Panel2.SuspendLayout();
            this.splitCtrlView.SuspendLayout();
            this.flowEditTools.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericRadiusOfPosSq)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitUVMat)).BeginInit();
            this.splitUVMat.Panel2.SuspendLayout();
            this.splitUVMat.SuspendLayout();
            this.statusStripEditor.SuspendLayout();
            this.menuStripEditor.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitCtrlView
            // 
            this.splitCtrlView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitCtrlView.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitCtrlView.Location = new System.Drawing.Point(0, 30);
            this.splitCtrlView.Margin = new System.Windows.Forms.Padding(0);
            this.splitCtrlView.Name = "splitCtrlView";
            // 
            // splitCtrlView.Panel1
            // 
            this.splitCtrlView.Panel1.Controls.Add(this.flowEditTools);
            this.splitCtrlView.Panel1MinSize = 90;
            // 
            // splitCtrlView.Panel2
            // 
            this.splitCtrlView.Panel2.Controls.Add(this.splitUVMat);
            this.splitCtrlView.Panel2MinSize = 500;
            this.splitCtrlView.Size = new System.Drawing.Size(1556, 911);
            this.splitCtrlView.SplitterDistance = 175;
            this.splitCtrlView.SplitterWidth = 6;
            this.splitCtrlView.TabIndex = 0;
            // 
            // flowEditTools
            // 
            this.flowEditTools.Controls.Add(this.radioButtonRectangleSelection);
            this.flowEditTools.Controls.Add(this.radioButtonLassoSelection);
            this.flowEditTools.Controls.Add(this.radioButtonMove);
            this.flowEditTools.Controls.Add(this.radioButtonRotate);
            this.flowEditTools.Controls.Add(this.radioButtonScale);
            this.flowEditTools.Controls.Add(this.buttonReverseV);
            this.flowEditTools.Controls.Add(this.buttonReverseH);
            this.flowEditTools.Controls.Add(this.buttonResetCamera);
            this.flowEditTools.Controls.Add(this.numericRadiusOfPosSq);
            this.flowEditTools.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowEditTools.Location = new System.Drawing.Point(0, 0);
            this.flowEditTools.Name = "flowEditTools";
            this.flowEditTools.Size = new System.Drawing.Size(175, 911);
            this.flowEditTools.TabIndex = 0;
            // 
            // radioButtonRectangleSelection
            // 
            this.radioButtonRectangleSelection.Appearance = System.Windows.Forms.Appearance.Button;
            this.radioButtonRectangleSelection.Checked = true;
            this.radioButtonRectangleSelection.Location = new System.Drawing.Point(3, 3);
            this.radioButtonRectangleSelection.Name = "radioButtonRectangleSelection";
            this.radioButtonRectangleSelection.Size = new System.Drawing.Size(80, 60);
            this.radioButtonRectangleSelection.TabIndex = 0;
            this.radioButtonRectangleSelection.TabStop = true;
            this.radioButtonRectangleSelection.Text = "範囲選択";
            this.radioButtonRectangleSelection.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.radioButtonRectangleSelection.UseVisualStyleBackColor = true;
            this.radioButtonRectangleSelection.CheckedChanged += new System.EventHandler(this.radioButtonRectangleSelection_CheckedChanged);
            // 
            // radioButtonLassoSelection
            // 
            this.radioButtonLassoSelection.Appearance = System.Windows.Forms.Appearance.Button;
            this.radioButtonLassoSelection.Location = new System.Drawing.Point(89, 3);
            this.radioButtonLassoSelection.Name = "radioButtonLassoSelection";
            this.radioButtonLassoSelection.Size = new System.Drawing.Size(80, 60);
            this.radioButtonLassoSelection.TabIndex = 0;
            this.radioButtonLassoSelection.Text = "投縄選択";
            this.radioButtonLassoSelection.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.radioButtonLassoSelection.UseVisualStyleBackColor = true;
            // 
            // radioButtonMove
            // 
            this.radioButtonMove.Appearance = System.Windows.Forms.Appearance.Button;
            this.radioButtonMove.Location = new System.Drawing.Point(3, 69);
            this.radioButtonMove.Name = "radioButtonMove";
            this.radioButtonMove.Size = new System.Drawing.Size(80, 60);
            this.radioButtonMove.TabIndex = 1;
            this.radioButtonMove.Text = "移動";
            this.radioButtonMove.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.radioButtonMove.UseVisualStyleBackColor = true;
            this.radioButtonMove.CheckedChanged += new System.EventHandler(this.radioButtonMove_CheckedChanged);
            // 
            // radioButtonRotate
            // 
            this.radioButtonRotate.Appearance = System.Windows.Forms.Appearance.Button;
            this.radioButtonRotate.Location = new System.Drawing.Point(89, 69);
            this.radioButtonRotate.Name = "radioButtonRotate";
            this.radioButtonRotate.Size = new System.Drawing.Size(80, 60);
            this.radioButtonRotate.TabIndex = 2;
            this.radioButtonRotate.Text = "回転";
            this.radioButtonRotate.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.radioButtonRotate.UseVisualStyleBackColor = true;
            this.radioButtonRotate.CheckedChanged += new System.EventHandler(this.radioButtonRotate_CheckedChanged);
            // 
            // buttonReverseV
            // 
            this.buttonReverseV.Location = new System.Drawing.Point(89, 135);
            this.buttonReverseV.Name = "buttonReverseV";
            this.buttonReverseV.Size = new System.Drawing.Size(80, 60);
            this.buttonReverseV.TabIndex = 3;
            this.buttonReverseV.Text = "垂直反転";
            this.buttonReverseV.UseVisualStyleBackColor = true;
            this.buttonReverseV.Click += new System.EventHandler(this.buttonReverseV_Click);
            // 
            // buttonReverseH
            // 
            this.buttonReverseH.Location = new System.Drawing.Point(3, 201);
            this.buttonReverseH.Name = "buttonReverseH";
            this.buttonReverseH.Size = new System.Drawing.Size(80, 60);
            this.buttonReverseH.TabIndex = 3;
            this.buttonReverseH.Text = "鏡像反転";
            this.buttonReverseH.UseVisualStyleBackColor = true;
            // 
            // buttonResetCamera
            // 
            this.buttonResetCamera.Location = new System.Drawing.Point(89, 201);
            this.buttonResetCamera.Name = "buttonResetCamera";
            this.buttonResetCamera.Size = new System.Drawing.Size(80, 60);
            this.buttonResetCamera.TabIndex = 3;
            this.buttonResetCamera.Text = "カメラ\r\n初期化";
            this.buttonResetCamera.UseVisualStyleBackColor = true;
            this.buttonResetCamera.Click += new System.EventHandler(this.buttonResetCamera_Click);
            // 
            // numericRadiusOfPosSq
            // 
            this.numericRadiusOfPosSq.DecimalPlaces = 4;
            this.numericRadiusOfPosSq.Increment = new decimal(new int[] {
            5,
            0,
            0,
            262144});
            this.numericRadiusOfPosSq.Location = new System.Drawing.Point(3, 267);
            this.numericRadiusOfPosSq.Maximum = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.numericRadiusOfPosSq.Name = "numericRadiusOfPosSq";
            this.numericRadiusOfPosSq.Size = new System.Drawing.Size(120, 32);
            this.numericRadiusOfPosSq.TabIndex = 4;
            this.numericRadiusOfPosSq.Value = new decimal(new int[] {
            5,
            0,
            0,
            196608});
            this.numericRadiusOfPosSq.ValueChanged += new System.EventHandler(this.numericRadiusOfPosSq_ValueChanged);
            // 
            // splitUVMat
            // 
            this.splitUVMat.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitUVMat.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitUVMat.Location = new System.Drawing.Point(0, 0);
            this.splitUVMat.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.splitUVMat.Name = "splitUVMat";
            // 
            // splitUVMat.Panel1
            // 
            this.splitUVMat.Panel1.ClientSizeChanged += new System.EventHandler(this.splitUVMat_Panel1_ClientSizeChanged);
            this.splitUVMat.Panel1.MouseEnter += new System.EventHandler(this.splitUVMat_Panel1_MouseEnter);
            this.splitUVMat.Panel1.MouseLeave += new System.EventHandler(this.splitUVMat_Panel1_MouseLeave);
            // 
            // splitUVMat.Panel2
            // 
            this.splitUVMat.Panel2.Controls.Add(this.listBoxMaterial);
            this.splitUVMat.Size = new System.Drawing.Size(1375, 911);
            this.splitUVMat.SplitterDistance = 989;
            this.splitUVMat.SplitterWidth = 6;
            this.splitUVMat.TabIndex = 0;
            // 
            // listBoxMaterial
            // 
            this.listBoxMaterial.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBoxMaterial.FormattingEnabled = true;
            this.listBoxMaterial.ItemHeight = 20;
            this.listBoxMaterial.Location = new System.Drawing.Point(0, 0);
            this.listBoxMaterial.Margin = new System.Windows.Forms.Padding(0);
            this.listBoxMaterial.Name = "listBoxMaterial";
            this.listBoxMaterial.Size = new System.Drawing.Size(380, 911);
            this.listBoxMaterial.TabIndex = 0;
            this.listBoxMaterial.SelectedIndexChanged += new System.EventHandler(this.listBoxMaterial_SelectedIndexChanged);
            // 
            // statusStripEditor
            // 
            this.statusStripEditor.Font = new System.Drawing.Font("游ゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.statusStripEditor.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabelFPS,
            this.toolStripStatusLabelState,
            this.toolStripProgressBarState});
            this.statusStripEditor.Location = new System.Drawing.Point(0, 941);
            this.statusStripEditor.Name = "statusStripEditor";
            this.statusStripEditor.Padding = new System.Windows.Forms.Padding(2, 0, 21, 0);
            this.statusStripEditor.Size = new System.Drawing.Size(1556, 22);
            this.statusStripEditor.TabIndex = 1;
            this.statusStripEditor.Text = "statusStrip1";
            // 
            // toolStripStatusLabelFPS
            // 
            this.toolStripStatusLabelFPS.Name = "toolStripStatusLabelFPS";
            this.toolStripStatusLabelFPS.Size = new System.Drawing.Size(46, 17);
            this.toolStripStatusLabelFPS.Text = "000fps";
            // 
            // toolStripStatusLabelState
            // 
            this.toolStripStatusLabelState.Name = "toolStripStatusLabelState";
            this.toolStripStatusLabelState.Size = new System.Drawing.Size(68, 17);
            this.toolStripStatusLabelState.Text = "現在の状態";
            // 
            // toolStripProgressBarState
            // 
            this.toolStripProgressBarState.Name = "toolStripProgressBarState";
            this.toolStripProgressBarState.Size = new System.Drawing.Size(100, 16);
            this.toolStripProgressBarState.Visible = false;
            // 
            // menuStripEditor
            // 
            this.menuStripEditor.Font = new System.Drawing.Font("游ゴシック", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.menuStripEditor.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.再読込ToolStripMenuItem,
            this.編集ToolStripMenuItem,
            this.テクスチャToolStripMenuItem});
            this.menuStripEditor.Location = new System.Drawing.Point(0, 0);
            this.menuStripEditor.Name = "menuStripEditor";
            this.menuStripEditor.Padding = new System.Windows.Forms.Padding(9, 3, 0, 3);
            this.menuStripEditor.Size = new System.Drawing.Size(1556, 30);
            this.menuStripEditor.TabIndex = 2;
            this.menuStripEditor.Text = "menuStrip1";
            // 
            // 再読込ToolStripMenuItem
            // 
            this.再読込ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.再読込ToolStripMenuItem1,
            this.uVを反映ToolStripMenuItem,
            this.toolStripSeparator1,
            this.選択頂点を受信ToolStripMenuItem,
            this.選択頂点を送信ToolStripMenuItem});
            this.再読込ToolStripMenuItem.Name = "再読込ToolStripMenuItem";
            this.再読込ToolStripMenuItem.Size = new System.Drawing.Size(66, 24);
            this.再読込ToolStripMenuItem.Text = "モデル";
            // 
            // 再読込ToolStripMenuItem1
            // 
            this.再読込ToolStripMenuItem1.Name = "再読込ToolStripMenuItem1";
            this.再読込ToolStripMenuItem1.ShortcutKeyDisplayString = "Ctrl+R";
            this.再読込ToolStripMenuItem1.Size = new System.Drawing.Size(284, 24);
            this.再読込ToolStripMenuItem1.Text = "再読込";
            // 
            // uVを反映ToolStripMenuItem
            // 
            this.uVを反映ToolStripMenuItem.Name = "uVを反映ToolStripMenuItem";
            this.uVを反映ToolStripMenuItem.ShortcutKeyDisplayString = "Ctrl+S";
            this.uVを反映ToolStripMenuItem.Size = new System.Drawing.Size(284, 24);
            this.uVを反映ToolStripMenuItem.Text = "反映";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(281, 6);
            // 
            // 選択頂点を受信ToolStripMenuItem
            // 
            this.選択頂点を受信ToolStripMenuItem.Name = "選択頂点を受信ToolStripMenuItem";
            this.選択頂点を受信ToolStripMenuItem.ShortcutKeyDisplayString = "Ctrl+X";
            this.選択頂点を受信ToolStripMenuItem.Size = new System.Drawing.Size(284, 24);
            this.選択頂点を受信ToolStripMenuItem.Text = "選択頂点を送信";
            // 
            // 選択頂点を送信ToolStripMenuItem
            // 
            this.選択頂点を送信ToolStripMenuItem.Name = "選択頂点を送信ToolStripMenuItem";
            this.選択頂点を送信ToolStripMenuItem.ShortcutKeyDisplayString = "Ctra+Shift+X";
            this.選択頂点を送信ToolStripMenuItem.Size = new System.Drawing.Size(284, 24);
            this.選択頂点を送信ToolStripMenuItem.Text = "選択頂点を受信";
            // 
            // 編集ToolStripMenuItem
            // 
            this.編集ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.元に戻すToolStripMenuItem,
            this.やり直しToolStripMenuItem,
            this.toolStripSeparator2,
            this.色を変更ToolStripMenuItem,
            this.頂点選択保存ToolStripMenuItem,
            this.toolStripSeparator3,
            this.描画リミッター解除ToolStripMenuItem,
            this.座標のコピーToolStripMenuItem,
            this.座標のペーストToolStripMenuItem});
            this.編集ToolStripMenuItem.Name = "編集ToolStripMenuItem";
            this.編集ToolStripMenuItem.Size = new System.Drawing.Size(51, 24);
            this.編集ToolStripMenuItem.Text = "編集";
            // 
            // 元に戻すToolStripMenuItem
            // 
            this.元に戻すToolStripMenuItem.Name = "元に戻すToolStripMenuItem";
            this.元に戻すToolStripMenuItem.ShortcutKeyDisplayString = "Ctrl+Z";
            this.元に戻すToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Z)));
            this.元に戻すToolStripMenuItem.Size = new System.Drawing.Size(237, 24);
            this.元に戻すToolStripMenuItem.Text = "元に戻す";
            this.元に戻すToolStripMenuItem.Click += new System.EventHandler(this.元に戻すToolStripMenuItem_Click);
            // 
            // やり直しToolStripMenuItem
            // 
            this.やり直しToolStripMenuItem.Name = "やり直しToolStripMenuItem";
            this.やり直しToolStripMenuItem.ShortcutKeyDisplayString = "Ctrl+Shift+Z";
            this.やり直しToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.Z)));
            this.やり直しToolStripMenuItem.Size = new System.Drawing.Size(237, 24);
            this.やり直しToolStripMenuItem.Text = "やり直し";
            this.やり直しToolStripMenuItem.Click += new System.EventHandler(this.やり直しToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(234, 6);
            // 
            // 色を変更ToolStripMenuItem
            // 
            this.色を変更ToolStripMenuItem.Name = "色を変更ToolStripMenuItem";
            this.色を変更ToolStripMenuItem.Size = new System.Drawing.Size(237, 24);
            this.色を変更ToolStripMenuItem.Text = "色を変更";
            this.色を変更ToolStripMenuItem.Click += new System.EventHandler(this.色を変更ToolStripMenuItem_Click);
            // 
            // 頂点選択保存ToolStripMenuItem
            // 
            this.頂点選択保存ToolStripMenuItem.Name = "頂点選択保存ToolStripMenuItem";
            this.頂点選択保存ToolStripMenuItem.Size = new System.Drawing.Size(237, 24);
            this.頂点選択保存ToolStripMenuItem.Text = "頂点選択保存";
            this.頂点選択保存ToolStripMenuItem.Click += new System.EventHandler(this.頂点選択保存ToolStripMenuItem_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(234, 6);
            // 
            // 描画リミッター解除ToolStripMenuItem
            // 
            this.描画リミッター解除ToolStripMenuItem.CheckOnClick = true;
            this.描画リミッター解除ToolStripMenuItem.Name = "描画リミッター解除ToolStripMenuItem";
            this.描画リミッター解除ToolStripMenuItem.Size = new System.Drawing.Size(237, 24);
            this.描画リミッター解除ToolStripMenuItem.Text = "描画リミッター解除";
            this.描画リミッター解除ToolStripMenuItem.Click += new System.EventHandler(this.描画リミッター解除ToolStripMenuItem_Click);
            // 
            // 座標のコピーToolStripMenuItem
            // 
            this.座標のコピーToolStripMenuItem.Name = "座標のコピーToolStripMenuItem";
            this.座標のコピーToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)));
            this.座標のコピーToolStripMenuItem.Size = new System.Drawing.Size(237, 24);
            this.座標のコピーToolStripMenuItem.Text = "座標のコピー";
            this.座標のコピーToolStripMenuItem.Click += new System.EventHandler(this.座標のコピーToolStripMenuItem_Click);
            // 
            // 座標のペーストToolStripMenuItem
            // 
            this.座標のペーストToolStripMenuItem.Name = "座標のペーストToolStripMenuItem";
            this.座標のペーストToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.V)));
            this.座標のペーストToolStripMenuItem.Size = new System.Drawing.Size(237, 24);
            this.座標のペーストToolStripMenuItem.Text = "座標のペースト";
            this.座標のペーストToolStripMenuItem.Click += new System.EventHandler(this.座標のペーストToolStripMenuItem_Click);
            // 
            // テクスチャToolStripMenuItem
            // 
            this.テクスチャToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.選択材質のテクスチャを変更ToolStripMenuItem,
            this.uV情報を合成して保存ToolStripMenuItem});
            this.テクスチャToolStripMenuItem.Name = "テクスチャToolStripMenuItem";
            this.テクスチャToolStripMenuItem.Size = new System.Drawing.Size(96, 24);
            this.テクスチャToolStripMenuItem.Text = "テクスチャ";
            // 
            // 選択材質のテクスチャを変更ToolStripMenuItem
            // 
            this.選択材質のテクスチャを変更ToolStripMenuItem.Name = "選択材質のテクスチャを変更ToolStripMenuItem";
            this.選択材質のテクスチャを変更ToolStripMenuItem.Size = new System.Drawing.Size(273, 24);
            this.選択材質のテクスチャを変更ToolStripMenuItem.Text = "選択材質のテクスチャを変更";
            // 
            // uV情報を合成して保存ToolStripMenuItem
            // 
            this.uV情報を合成して保存ToolStripMenuItem.Name = "uV情報を合成して保存ToolStripMenuItem";
            this.uV情報を合成して保存ToolStripMenuItem.Size = new System.Drawing.Size(273, 24);
            this.uV情報を合成して保存ToolStripMenuItem.Text = "UV情報を合成して保存";
            // 
            // timerEvery
            // 
            this.timerEvery.Interval = 5;
            this.timerEvery.Tick += new System.EventHandler(this.timerEvery_Tick);
            // 
            // radioButtonScale
            // 
            this.radioButtonScale.Appearance = System.Windows.Forms.Appearance.Button;
            this.radioButtonScale.Location = new System.Drawing.Point(3, 135);
            this.radioButtonScale.Name = "radioButtonScale";
            this.radioButtonScale.Size = new System.Drawing.Size(80, 60);
            this.radioButtonScale.TabIndex = 2;
            this.radioButtonScale.Text = "拡縮";
            this.radioButtonScale.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.radioButtonScale.UseVisualStyleBackColor = true;
            this.radioButtonScale.CheckedChanged += new System.EventHandler(this.radioButtonScale_CheckedChanged);
            // 
            // FormEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1556, 963);
            this.Controls.Add(this.statusStripEditor);
            this.Controls.Add(this.menuStripEditor);
            this.Controls.Add(this.splitCtrlView);
            this.Font = new System.Drawing.Font("游ゴシック", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "FormEditor";
            this.Text = "UV編集";
            this.splitCtrlView.Panel1.ResumeLayout(false);
            this.splitCtrlView.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitCtrlView)).EndInit();
            this.splitCtrlView.ResumeLayout(false);
            this.flowEditTools.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numericRadiusOfPosSq)).EndInit();
            this.splitUVMat.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitUVMat)).EndInit();
            this.splitUVMat.ResumeLayout(false);
            this.statusStripEditor.ResumeLayout(false);
            this.statusStripEditor.PerformLayout();
            this.menuStripEditor.ResumeLayout(false);
            this.menuStripEditor.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitCtrlView;
        private System.Windows.Forms.SplitContainer splitUVMat;
        private System.Windows.Forms.ListBox listBoxMaterial;
        private System.Windows.Forms.StatusStrip statusStripEditor;
        private System.Windows.Forms.MenuStrip menuStripEditor;
        private System.Windows.Forms.ToolStripMenuItem 再読込ToolStripMenuItem;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelState;
        private System.Windows.Forms.ToolStripMenuItem 編集ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 元に戻すToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem やり直しToolStripMenuItem;
        private System.Windows.Forms.FlowLayoutPanel flowEditTools;
        private System.Windows.Forms.RadioButton radioButtonRectangleSelection;
        private System.Windows.Forms.RadioButton radioButtonLassoSelection;
        private System.Windows.Forms.RadioButton radioButtonMove;
        private System.Windows.Forms.RadioButton radioButtonRotate;
        private System.Windows.Forms.Button buttonReverseV;
        private System.Windows.Forms.Button buttonReverseH;
        private System.Windows.Forms.ToolStripMenuItem 再読込ToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem uVを反映ToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem 選択頂点を受信ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 選択頂点を送信ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem テクスチャToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 選択材質のテクスチャを変更ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem uV情報を合成して保存ToolStripMenuItem;
        private System.Windows.Forms.ToolStripProgressBar toolStripProgressBarState;
        private System.Windows.Forms.Button buttonResetCamera;
        private System.Windows.Forms.Timer timerEvery;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelFPS;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem 描画リミッター解除ToolStripMenuItem;
        private System.Windows.Forms.NumericUpDown numericRadiusOfPosSq;
        private System.Windows.Forms.ToolStripMenuItem 色を変更ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 頂点選択保存ToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem 座標のコピーToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 座標のペーストToolStripMenuItem;
        private System.Windows.Forms.RadioButton radioButtonScale;
    }
}