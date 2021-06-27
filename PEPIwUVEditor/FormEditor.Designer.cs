
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
            this.splitContainerToolAction = new System.Windows.Forms.SplitContainer();
            this.flowEditTools = new System.Windows.Forms.FlowLayoutPanel();
            this.radioButtonRectangleSelection = new System.Windows.Forms.RadioButton();
            this.radioButtonMove = new System.Windows.Forms.RadioButton();
            this.radioButtonRotate = new System.Windows.Forms.RadioButton();
            this.radioButtonScale = new System.Windows.Forms.RadioButton();
            this.tableLayoutPanelActions = new System.Windows.Forms.TableLayoutPanel();
            this.numericScaleRatioY = new System.Windows.Forms.NumericUpDown();
            this.numericScaleRatioX = new System.Windows.Forms.NumericUpDown();
            this.labelScaleRatio = new System.Windows.Forms.Label();
            this.numericScaleCenterY = new System.Windows.Forms.NumericUpDown();
            this.numericScaleCenterX = new System.Windows.Forms.NumericUpDown();
            this.labelScaleCenter = new System.Windows.Forms.Label();
            this.numericRotAngle = new System.Windows.Forms.NumericUpDown();
            this.numericRotCenterY = new System.Windows.Forms.NumericUpDown();
            this.numericRotCenterX = new System.Windows.Forms.NumericUpDown();
            this.numericMoveY = new System.Windows.Forms.NumericUpDown();
            this.labelRotAngle = new System.Windows.Forms.Label();
            this.labelRotCenter = new System.Windows.Forms.Label();
            this.labelY = new System.Windows.Forms.Label();
            this.flowLayoutPanelActions = new System.Windows.Forms.FlowLayoutPanel();
            this.buttonReverseV = new System.Windows.Forms.Button();
            this.buttonReverseH = new System.Windows.Forms.Button();
            this.buttonResetCamera = new System.Windows.Forms.Button();
            this.buttonSelectContinuousVertices = new System.Windows.Forms.Button();
            this.LabelMove = new System.Windows.Forms.Label();
            this.labelX = new System.Windows.Forms.Label();
            this.numericMoveX = new System.Windows.Forms.NumericUpDown();
            this.buttonApplyNumericEdit = new System.Windows.Forms.Button();
            this.splitUVMat = new System.Windows.Forms.SplitContainer();
            this.tableLayoutDrawSettings = new System.Windows.Forms.TableLayoutPanel();
            this.labelRadiusOfPosSq = new System.Windows.Forms.Label();
            this.listBoxMaterial = new System.Windows.Forms.ListBox();
            this.numericRadiusOfPosSq = new System.Windows.Forms.NumericUpDown();
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
            this.デバッグログToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.timerEvery = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.splitCtrlView)).BeginInit();
            this.splitCtrlView.Panel1.SuspendLayout();
            this.splitCtrlView.Panel2.SuspendLayout();
            this.splitCtrlView.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerToolAction)).BeginInit();
            this.splitContainerToolAction.Panel1.SuspendLayout();
            this.splitContainerToolAction.Panel2.SuspendLayout();
            this.splitContainerToolAction.SuspendLayout();
            this.flowEditTools.SuspendLayout();
            this.tableLayoutPanelActions.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericScaleRatioY)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericScaleRatioX)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericScaleCenterY)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericScaleCenterX)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericRotAngle)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericRotCenterY)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericRotCenterX)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericMoveY)).BeginInit();
            this.flowLayoutPanelActions.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericMoveX)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitUVMat)).BeginInit();
            this.splitUVMat.Panel2.SuspendLayout();
            this.splitUVMat.SuspendLayout();
            this.tableLayoutDrawSettings.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericRadiusOfPosSq)).BeginInit();
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
            this.splitCtrlView.Panel1.Controls.Add(this.splitContainerToolAction);
            this.splitCtrlView.Panel1MinSize = 90;
            // 
            // splitCtrlView.Panel2
            // 
            this.splitCtrlView.Panel2.Controls.Add(this.splitUVMat);
            this.splitCtrlView.Panel2MinSize = 500;
            this.splitCtrlView.Size = new System.Drawing.Size(1556, 911);
            this.splitCtrlView.SplitterDistance = 174;
            this.splitCtrlView.SplitterWidth = 6;
            this.splitCtrlView.TabIndex = 0;
            // 
            // splitContainerToolAction
            // 
            this.splitContainerToolAction.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitContainerToolAction.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerToolAction.Location = new System.Drawing.Point(0, 0);
            this.splitContainerToolAction.Margin = new System.Windows.Forms.Padding(0);
            this.splitContainerToolAction.Name = "splitContainerToolAction";
            this.splitContainerToolAction.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainerToolAction.Panel1
            // 
            this.splitContainerToolAction.Panel1.Controls.Add(this.flowEditTools);
            // 
            // splitContainerToolAction.Panel2
            // 
            this.splitContainerToolAction.Panel2.Controls.Add(this.tableLayoutPanelActions);
            this.splitContainerToolAction.Size = new System.Drawing.Size(174, 911);
            this.splitContainerToolAction.SplitterDistance = 403;
            this.splitContainerToolAction.TabIndex = 1;
            // 
            // flowEditTools
            // 
            this.flowEditTools.Controls.Add(this.radioButtonRectangleSelection);
            this.flowEditTools.Controls.Add(this.radioButtonMove);
            this.flowEditTools.Controls.Add(this.radioButtonRotate);
            this.flowEditTools.Controls.Add(this.radioButtonScale);
            this.flowEditTools.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowEditTools.Location = new System.Drawing.Point(0, 0);
            this.flowEditTools.Margin = new System.Windows.Forms.Padding(0);
            this.flowEditTools.Name = "flowEditTools";
            this.flowEditTools.Size = new System.Drawing.Size(172, 401);
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
            // radioButtonMove
            // 
            this.radioButtonMove.Appearance = System.Windows.Forms.Appearance.Button;
            this.radioButtonMove.Location = new System.Drawing.Point(89, 3);
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
            this.radioButtonRotate.Location = new System.Drawing.Point(3, 69);
            this.radioButtonRotate.Name = "radioButtonRotate";
            this.radioButtonRotate.Size = new System.Drawing.Size(80, 60);
            this.radioButtonRotate.TabIndex = 2;
            this.radioButtonRotate.Text = "回転";
            this.radioButtonRotate.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.radioButtonRotate.UseVisualStyleBackColor = true;
            this.radioButtonRotate.CheckedChanged += new System.EventHandler(this.radioButtonRotate_CheckedChanged);
            // 
            // radioButtonScale
            // 
            this.radioButtonScale.Appearance = System.Windows.Forms.Appearance.Button;
            this.radioButtonScale.Location = new System.Drawing.Point(89, 69);
            this.radioButtonScale.Name = "radioButtonScale";
            this.radioButtonScale.Size = new System.Drawing.Size(80, 60);
            this.radioButtonScale.TabIndex = 2;
            this.radioButtonScale.Text = "拡縮";
            this.radioButtonScale.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.radioButtonScale.UseVisualStyleBackColor = true;
            this.radioButtonScale.CheckedChanged += new System.EventHandler(this.radioButtonScale_CheckedChanged);
            // 
            // tableLayoutPanelActions
            // 
            this.tableLayoutPanelActions.ColumnCount = 2;
            this.tableLayoutPanelActions.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanelActions.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanelActions.Controls.Add(this.numericScaleRatioY, 1, 10);
            this.tableLayoutPanelActions.Controls.Add(this.numericScaleRatioX, 0, 10);
            this.tableLayoutPanelActions.Controls.Add(this.labelScaleRatio, 0, 9);
            this.tableLayoutPanelActions.Controls.Add(this.numericScaleCenterY, 1, 8);
            this.tableLayoutPanelActions.Controls.Add(this.numericScaleCenterX, 0, 8);
            this.tableLayoutPanelActions.Controls.Add(this.labelScaleCenter, 0, 7);
            this.tableLayoutPanelActions.Controls.Add(this.numericRotAngle, 1, 6);
            this.tableLayoutPanelActions.Controls.Add(this.numericRotCenterY, 1, 5);
            this.tableLayoutPanelActions.Controls.Add(this.numericRotCenterX, 0, 5);
            this.tableLayoutPanelActions.Controls.Add(this.numericMoveY, 1, 3);
            this.tableLayoutPanelActions.Controls.Add(this.labelRotAngle, 0, 6);
            this.tableLayoutPanelActions.Controls.Add(this.labelRotCenter, 0, 4);
            this.tableLayoutPanelActions.Controls.Add(this.labelY, 1, 1);
            this.tableLayoutPanelActions.Controls.Add(this.flowLayoutPanelActions, 0, 0);
            this.tableLayoutPanelActions.Controls.Add(this.LabelMove, 0, 2);
            this.tableLayoutPanelActions.Controls.Add(this.labelX, 0, 1);
            this.tableLayoutPanelActions.Controls.Add(this.numericMoveX, 0, 3);
            this.tableLayoutPanelActions.Controls.Add(this.buttonApplyNumericEdit, 0, 11);
            this.tableLayoutPanelActions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanelActions.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanelActions.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanelActions.Name = "tableLayoutPanelActions";
            this.tableLayoutPanelActions.RowCount = 12;
            this.tableLayoutPanelActions.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelActions.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanelActions.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanelActions.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
            this.tableLayoutPanelActions.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanelActions.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
            this.tableLayoutPanelActions.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
            this.tableLayoutPanelActions.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanelActions.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
            this.tableLayoutPanelActions.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanelActions.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
            this.tableLayoutPanelActions.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
            this.tableLayoutPanelActions.Size = new System.Drawing.Size(172, 502);
            this.tableLayoutPanelActions.TabIndex = 0;
            // 
            // numericScaleRatioY
            // 
            this.numericScaleRatioY.DecimalPlaces = 5;
            this.numericScaleRatioY.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.numericScaleRatioY.Location = new System.Drawing.Point(86, 438);
            this.numericScaleRatioY.Margin = new System.Windows.Forms.Padding(0);
            this.numericScaleRatioY.Name = "numericScaleRatioY";
            this.numericScaleRatioY.Size = new System.Drawing.Size(86, 32);
            this.numericScaleRatioY.TabIndex = 21;
            this.numericScaleRatioY.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // numericScaleRatioX
            // 
            this.numericScaleRatioX.DecimalPlaces = 5;
            this.numericScaleRatioX.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.numericScaleRatioX.Location = new System.Drawing.Point(0, 438);
            this.numericScaleRatioX.Margin = new System.Windows.Forms.Padding(0);
            this.numericScaleRatioX.Name = "numericScaleRatioX";
            this.numericScaleRatioX.Size = new System.Drawing.Size(86, 32);
            this.numericScaleRatioX.TabIndex = 20;
            this.numericScaleRatioX.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // labelScaleRatio
            // 
            this.labelScaleRatio.AutoSize = true;
            this.tableLayoutPanelActions.SetColumnSpan(this.labelScaleRatio, 2);
            this.labelScaleRatio.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelScaleRatio.Location = new System.Drawing.Point(3, 418);
            this.labelScaleRatio.Name = "labelScaleRatio";
            this.labelScaleRatio.Size = new System.Drawing.Size(166, 20);
            this.labelScaleRatio.TabIndex = 19;
            this.labelScaleRatio.Text = "拡縮率";
            this.labelScaleRatio.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // numericScaleCenterY
            // 
            this.numericScaleCenterY.DecimalPlaces = 5;
            this.numericScaleCenterY.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.numericScaleCenterY.Location = new System.Drawing.Point(86, 386);
            this.numericScaleCenterY.Margin = new System.Windows.Forms.Padding(0);
            this.numericScaleCenterY.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            -2147483648});
            this.numericScaleCenterY.Name = "numericScaleCenterY";
            this.numericScaleCenterY.Size = new System.Drawing.Size(86, 32);
            this.numericScaleCenterY.TabIndex = 18;
            this.numericScaleCenterY.ValueChanged += new System.EventHandler(this.numericScaleCenterY_ValueChanged);
            // 
            // numericScaleCenterX
            // 
            this.numericScaleCenterX.DecimalPlaces = 5;
            this.numericScaleCenterX.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.numericScaleCenterX.Location = new System.Drawing.Point(0, 386);
            this.numericScaleCenterX.Margin = new System.Windows.Forms.Padding(0);
            this.numericScaleCenterX.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            -2147483648});
            this.numericScaleCenterX.Name = "numericScaleCenterX";
            this.numericScaleCenterX.Size = new System.Drawing.Size(86, 32);
            this.numericScaleCenterX.TabIndex = 17;
            this.numericScaleCenterX.ValueChanged += new System.EventHandler(this.numericScaleCenterX_ValueChanged);
            // 
            // labelScaleCenter
            // 
            this.labelScaleCenter.AutoSize = true;
            this.tableLayoutPanelActions.SetColumnSpan(this.labelScaleCenter, 2);
            this.labelScaleCenter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelScaleCenter.Location = new System.Drawing.Point(3, 366);
            this.labelScaleCenter.Name = "labelScaleCenter";
            this.labelScaleCenter.Size = new System.Drawing.Size(166, 20);
            this.labelScaleCenter.TabIndex = 16;
            this.labelScaleCenter.Text = "拡縮中心";
            this.labelScaleCenter.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // numericRotAngle
            // 
            this.numericRotAngle.DecimalPlaces = 3;
            this.numericRotAngle.Location = new System.Drawing.Point(86, 334);
            this.numericRotAngle.Margin = new System.Windows.Forms.Padding(0);
            this.numericRotAngle.Maximum = new decimal(new int[] {
            360,
            0,
            0,
            0});
            this.numericRotAngle.Minimum = new decimal(new int[] {
            360,
            0,
            0,
            -2147483648});
            this.numericRotAngle.Name = "numericRotAngle";
            this.numericRotAngle.Size = new System.Drawing.Size(86, 32);
            this.numericRotAngle.TabIndex = 15;
            // 
            // numericRotCenterY
            // 
            this.numericRotCenterY.DecimalPlaces = 5;
            this.numericRotCenterY.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.numericRotCenterY.Location = new System.Drawing.Point(86, 302);
            this.numericRotCenterY.Margin = new System.Windows.Forms.Padding(0);
            this.numericRotCenterY.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            -2147483648});
            this.numericRotCenterY.Name = "numericRotCenterY";
            this.numericRotCenterY.Size = new System.Drawing.Size(86, 32);
            this.numericRotCenterY.TabIndex = 14;
            this.numericRotCenterY.ValueChanged += new System.EventHandler(this.numericRotCenterY_ValueChanged);
            // 
            // numericRotCenterX
            // 
            this.numericRotCenterX.DecimalPlaces = 5;
            this.numericRotCenterX.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.numericRotCenterX.Location = new System.Drawing.Point(0, 302);
            this.numericRotCenterX.Margin = new System.Windows.Forms.Padding(0);
            this.numericRotCenterX.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            -2147483648});
            this.numericRotCenterX.Name = "numericRotCenterX";
            this.numericRotCenterX.Size = new System.Drawing.Size(86, 32);
            this.numericRotCenterX.TabIndex = 13;
            this.numericRotCenterX.ValueChanged += new System.EventHandler(this.numericRotCenterX_ValueChanged);
            // 
            // numericMoveY
            // 
            this.numericMoveY.DecimalPlaces = 5;
            this.numericMoveY.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.numericMoveY.Location = new System.Drawing.Point(86, 250);
            this.numericMoveY.Margin = new System.Windows.Forms.Padding(0);
            this.numericMoveY.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            -2147483648});
            this.numericMoveY.Name = "numericMoveY";
            this.numericMoveY.Size = new System.Drawing.Size(86, 32);
            this.numericMoveY.TabIndex = 12;
            // 
            // labelRotAngle
            // 
            this.labelRotAngle.AutoSize = true;
            this.labelRotAngle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelRotAngle.Location = new System.Drawing.Point(0, 334);
            this.labelRotAngle.Margin = new System.Windows.Forms.Padding(0);
            this.labelRotAngle.Name = "labelRotAngle";
            this.labelRotAngle.Size = new System.Drawing.Size(86, 32);
            this.labelRotAngle.TabIndex = 10;
            this.labelRotAngle.Text = "回転角度";
            this.labelRotAngle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelRotCenter
            // 
            this.labelRotCenter.AutoSize = true;
            this.tableLayoutPanelActions.SetColumnSpan(this.labelRotCenter, 2);
            this.labelRotCenter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelRotCenter.Location = new System.Drawing.Point(3, 282);
            this.labelRotCenter.Name = "labelRotCenter";
            this.labelRotCenter.Size = new System.Drawing.Size(166, 20);
            this.labelRotCenter.TabIndex = 7;
            this.labelRotCenter.Text = "回転中心";
            this.labelRotCenter.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelY
            // 
            this.labelY.AutoSize = true;
            this.labelY.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelY.Location = new System.Drawing.Point(86, 210);
            this.labelY.Margin = new System.Windows.Forms.Padding(0);
            this.labelY.Name = "labelY";
            this.labelY.Size = new System.Drawing.Size(86, 20);
            this.labelY.TabIndex = 4;
            this.labelY.Text = "Y";
            this.labelY.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // flowLayoutPanelActions
            // 
            this.tableLayoutPanelActions.SetColumnSpan(this.flowLayoutPanelActions, 2);
            this.flowLayoutPanelActions.Controls.Add(this.buttonReverseV);
            this.flowLayoutPanelActions.Controls.Add(this.buttonReverseH);
            this.flowLayoutPanelActions.Controls.Add(this.buttonResetCamera);
            this.flowLayoutPanelActions.Controls.Add(this.buttonSelectContinuousVertices);
            this.flowLayoutPanelActions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanelActions.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanelActions.Margin = new System.Windows.Forms.Padding(0);
            this.flowLayoutPanelActions.Name = "flowLayoutPanelActions";
            this.flowLayoutPanelActions.Size = new System.Drawing.Size(172, 210);
            this.flowLayoutPanelActions.TabIndex = 0;
            // 
            // buttonReverseV
            // 
            this.buttonReverseV.Location = new System.Drawing.Point(3, 3);
            this.buttonReverseV.Name = "buttonReverseV";
            this.buttonReverseV.Size = new System.Drawing.Size(80, 60);
            this.buttonReverseV.TabIndex = 3;
            this.buttonReverseV.Text = "上下反転";
            this.buttonReverseV.UseVisualStyleBackColor = true;
            this.buttonReverseV.Click += new System.EventHandler(this.buttonReverseV_Click);
            // 
            // buttonReverseH
            // 
            this.buttonReverseH.Location = new System.Drawing.Point(89, 3);
            this.buttonReverseH.Name = "buttonReverseH";
            this.buttonReverseH.Size = new System.Drawing.Size(80, 60);
            this.buttonReverseH.TabIndex = 3;
            this.buttonReverseH.Text = "左右反転";
            this.buttonReverseH.UseVisualStyleBackColor = true;
            this.buttonReverseH.Click += new System.EventHandler(this.buttonReverseH_Click);
            // 
            // buttonResetCamera
            // 
            this.buttonResetCamera.Location = new System.Drawing.Point(3, 69);
            this.buttonResetCamera.Name = "buttonResetCamera";
            this.buttonResetCamera.Size = new System.Drawing.Size(80, 60);
            this.buttonResetCamera.TabIndex = 3;
            this.buttonResetCamera.Text = "カメラ\r\n初期化";
            this.buttonResetCamera.UseVisualStyleBackColor = true;
            this.buttonResetCamera.Click += new System.EventHandler(this.buttonResetCamera_Click);
            // 
            // buttonSelectContinuousVertices
            // 
            this.buttonSelectContinuousVertices.Location = new System.Drawing.Point(89, 69);
            this.buttonSelectContinuousVertices.Name = "buttonSelectContinuousVertices";
            this.buttonSelectContinuousVertices.Size = new System.Drawing.Size(80, 60);
            this.buttonSelectContinuousVertices.TabIndex = 4;
            this.buttonSelectContinuousVertices.Text = "連続頂点を選択";
            this.buttonSelectContinuousVertices.UseVisualStyleBackColor = true;
            this.buttonSelectContinuousVertices.Click += new System.EventHandler(this.buttonSelectContinuousVertices_Click);
            // 
            // LabelMove
            // 
            this.LabelMove.AutoSize = true;
            this.tableLayoutPanelActions.SetColumnSpan(this.LabelMove, 2);
            this.LabelMove.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LabelMove.Location = new System.Drawing.Point(3, 230);
            this.LabelMove.Name = "LabelMove";
            this.LabelMove.Size = new System.Drawing.Size(166, 20);
            this.LabelMove.TabIndex = 1;
            this.LabelMove.Text = "移動";
            this.LabelMove.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelX
            // 
            this.labelX.AutoSize = true;
            this.labelX.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelX.Location = new System.Drawing.Point(0, 210);
            this.labelX.Margin = new System.Windows.Forms.Padding(0);
            this.labelX.Name = "labelX";
            this.labelX.Size = new System.Drawing.Size(86, 20);
            this.labelX.TabIndex = 3;
            this.labelX.Text = "X";
            this.labelX.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // numericMoveX
            // 
            this.numericMoveX.DecimalPlaces = 5;
            this.numericMoveX.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.numericMoveX.Location = new System.Drawing.Point(0, 250);
            this.numericMoveX.Margin = new System.Windows.Forms.Padding(0);
            this.numericMoveX.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            -2147483648});
            this.numericMoveX.Name = "numericMoveX";
            this.numericMoveX.Size = new System.Drawing.Size(86, 32);
            this.numericMoveX.TabIndex = 11;
            // 
            // buttonApplyNumericEdit
            // 
            this.tableLayoutPanelActions.SetColumnSpan(this.buttonApplyNumericEdit, 2);
            this.buttonApplyNumericEdit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonApplyNumericEdit.Location = new System.Drawing.Point(3, 473);
            this.buttonApplyNumericEdit.Name = "buttonApplyNumericEdit";
            this.buttonApplyNumericEdit.Size = new System.Drawing.Size(166, 26);
            this.buttonApplyNumericEdit.TabIndex = 22;
            this.buttonApplyNumericEdit.Text = "適用";
            this.buttonApplyNumericEdit.UseVisualStyleBackColor = true;
            this.buttonApplyNumericEdit.Click += new System.EventHandler(this.buttonApplyNumericEdit_Click);
            // 
            // splitUVMat
            // 
            this.splitUVMat.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
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
            this.splitUVMat.Panel2.Controls.Add(this.tableLayoutDrawSettings);
            this.splitUVMat.Size = new System.Drawing.Size(1376, 911);
            this.splitUVMat.SplitterDistance = 1102;
            this.splitUVMat.SplitterWidth = 6;
            this.splitUVMat.TabIndex = 0;
            // 
            // tableLayoutDrawSettings
            // 
            this.tableLayoutDrawSettings.ColumnCount = 1;
            this.tableLayoutDrawSettings.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutDrawSettings.Controls.Add(this.labelRadiusOfPosSq, 0, 1);
            this.tableLayoutDrawSettings.Controls.Add(this.listBoxMaterial, 0, 0);
            this.tableLayoutDrawSettings.Controls.Add(this.numericRadiusOfPosSq, 0, 2);
            this.tableLayoutDrawSettings.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutDrawSettings.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutDrawSettings.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutDrawSettings.Name = "tableLayoutDrawSettings";
            this.tableLayoutDrawSettings.RowCount = 3;
            this.tableLayoutDrawSettings.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutDrawSettings.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutDrawSettings.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
            this.tableLayoutDrawSettings.Size = new System.Drawing.Size(266, 909);
            this.tableLayoutDrawSettings.TabIndex = 1;
            // 
            // labelRadiusOfPosSq
            // 
            this.labelRadiusOfPosSq.AutoSize = true;
            this.labelRadiusOfPosSq.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelRadiusOfPosSq.Location = new System.Drawing.Point(0, 857);
            this.labelRadiusOfPosSq.Margin = new System.Windows.Forms.Padding(0);
            this.labelRadiusOfPosSq.Name = "labelRadiusOfPosSq";
            this.labelRadiusOfPosSq.Size = new System.Drawing.Size(266, 20);
            this.labelRadiusOfPosSq.TabIndex = 4;
            this.labelRadiusOfPosSq.Text = "頂点描画の大きさ";
            this.labelRadiusOfPosSq.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // listBoxMaterial
            // 
            this.listBoxMaterial.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBoxMaterial.FormattingEnabled = true;
            this.listBoxMaterial.ItemHeight = 20;
            this.listBoxMaterial.Location = new System.Drawing.Point(0, 0);
            this.listBoxMaterial.Margin = new System.Windows.Forms.Padding(0);
            this.listBoxMaterial.Name = "listBoxMaterial";
            this.listBoxMaterial.Size = new System.Drawing.Size(266, 857);
            this.listBoxMaterial.TabIndex = 0;
            this.listBoxMaterial.SelectedIndexChanged += new System.EventHandler(this.listBoxMaterial_SelectedIndexChanged);
            // 
            // numericRadiusOfPosSq
            // 
            this.numericRadiusOfPosSq.DecimalPlaces = 4;
            this.numericRadiusOfPosSq.Dock = System.Windows.Forms.DockStyle.Fill;
            this.numericRadiusOfPosSq.Increment = new decimal(new int[] {
            5,
            0,
            0,
            262144});
            this.numericRadiusOfPosSq.Location = new System.Drawing.Point(0, 877);
            this.numericRadiusOfPosSq.Margin = new System.Windows.Forms.Padding(0);
            this.numericRadiusOfPosSq.Maximum = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.numericRadiusOfPosSq.Name = "numericRadiusOfPosSq";
            this.numericRadiusOfPosSq.Size = new System.Drawing.Size(266, 32);
            this.numericRadiusOfPosSq.TabIndex = 4;
            this.numericRadiusOfPosSq.Value = new decimal(new int[] {
            5,
            0,
            0,
            196608});
            this.numericRadiusOfPosSq.ValueChanged += new System.EventHandler(this.numericRadiusOfPosSq_ValueChanged);
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
            this.テクスチャToolStripMenuItem,
            this.デバッグログToolStripMenuItem});
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
            this.再読込ToolStripMenuItem1.ShortcutKeyDisplayString = "";
            this.再読込ToolStripMenuItem1.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.R)));
            this.再読込ToolStripMenuItem1.Size = new System.Drawing.Size(280, 24);
            this.再読込ToolStripMenuItem1.Text = "再読込";
            this.再読込ToolStripMenuItem1.Click += new System.EventHandler(this.再読込ToolStripMenuItem1_Click);
            // 
            // uVを反映ToolStripMenuItem
            // 
            this.uVを反映ToolStripMenuItem.Name = "uVを反映ToolStripMenuItem";
            this.uVを反映ToolStripMenuItem.ShortcutKeyDisplayString = "";
            this.uVを反映ToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.uVを反映ToolStripMenuItem.Size = new System.Drawing.Size(280, 24);
            this.uVを反映ToolStripMenuItem.Text = "反映";
            this.uVを反映ToolStripMenuItem.Click += new System.EventHandler(this.UVを反映ToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(277, 6);
            // 
            // 選択頂点を受信ToolStripMenuItem
            // 
            this.選択頂点を受信ToolStripMenuItem.Name = "選択頂点を受信ToolStripMenuItem";
            this.選択頂点を受信ToolStripMenuItem.ShortcutKeyDisplayString = "";
            this.選択頂点を受信ToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.X)));
            this.選択頂点を受信ToolStripMenuItem.Size = new System.Drawing.Size(280, 24);
            this.選択頂点を受信ToolStripMenuItem.Text = "選択頂点を送信";
            this.選択頂点を受信ToolStripMenuItem.Click += new System.EventHandler(this.選択頂点を受信ToolStripMenuItem_Click);
            // 
            // 選択頂点を送信ToolStripMenuItem
            // 
            this.選択頂点を送信ToolStripMenuItem.Name = "選択頂点を送信ToolStripMenuItem";
            this.選択頂点を送信ToolStripMenuItem.ShortcutKeyDisplayString = "";
            this.選択頂点を送信ToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.X)));
            this.選択頂点を送信ToolStripMenuItem.Size = new System.Drawing.Size(280, 24);
            this.選択頂点を送信ToolStripMenuItem.Text = "選択頂点を受信";
            this.選択頂点を送信ToolStripMenuItem.Click += new System.EventHandler(this.選択頂点を送信ToolStripMenuItem_Click);
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
            this.元に戻すToolStripMenuItem.ShortcutKeyDisplayString = "";
            this.元に戻すToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Z)));
            this.元に戻すToolStripMenuItem.Size = new System.Drawing.Size(237, 24);
            this.元に戻すToolStripMenuItem.Text = "元に戻す";
            this.元に戻すToolStripMenuItem.Click += new System.EventHandler(this.元に戻すToolStripMenuItem_Click);
            // 
            // やり直しToolStripMenuItem
            // 
            this.やり直しToolStripMenuItem.Name = "やり直しToolStripMenuItem";
            this.やり直しToolStripMenuItem.ShortcutKeyDisplayString = "";
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
            // デバッグログToolStripMenuItem
            // 
            this.デバッグログToolStripMenuItem.Name = "デバッグログToolStripMenuItem";
            this.デバッグログToolStripMenuItem.Size = new System.Drawing.Size(111, 24);
            this.デバッグログToolStripMenuItem.Text = "デバッグログ";
            this.デバッグログToolStripMenuItem.Click += new System.EventHandler(this.デバッグログToolStripMenuItem_Click);
            // 
            // timerEvery
            // 
            this.timerEvery.Interval = 5;
            this.timerEvery.Tick += new System.EventHandler(this.timerEvery_Tick);
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
            this.splitContainerToolAction.Panel1.ResumeLayout(false);
            this.splitContainerToolAction.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerToolAction)).EndInit();
            this.splitContainerToolAction.ResumeLayout(false);
            this.flowEditTools.ResumeLayout(false);
            this.tableLayoutPanelActions.ResumeLayout(false);
            this.tableLayoutPanelActions.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericScaleRatioY)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericScaleRatioX)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericScaleCenterY)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericScaleCenterX)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericRotAngle)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericRotCenterY)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericRotCenterX)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericMoveY)).EndInit();
            this.flowLayoutPanelActions.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numericMoveX)).EndInit();
            this.splitUVMat.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitUVMat)).EndInit();
            this.splitUVMat.ResumeLayout(false);
            this.tableLayoutDrawSettings.ResumeLayout(false);
            this.tableLayoutDrawSettings.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericRadiusOfPosSq)).EndInit();
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
        private System.Windows.Forms.ToolStripMenuItem デバッグログToolStripMenuItem;
        private System.Windows.Forms.SplitContainer splitContainerToolAction;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelActions;
        private System.Windows.Forms.Label labelY;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelActions;
        private System.Windows.Forms.Label LabelMove;
        private System.Windows.Forms.Label labelX;
        private System.Windows.Forms.NumericUpDown numericScaleRatioY;
        private System.Windows.Forms.NumericUpDown numericScaleRatioX;
        private System.Windows.Forms.Label labelScaleRatio;
        private System.Windows.Forms.NumericUpDown numericScaleCenterY;
        private System.Windows.Forms.NumericUpDown numericScaleCenterX;
        private System.Windows.Forms.Label labelScaleCenter;
        private System.Windows.Forms.NumericUpDown numericRotAngle;
        private System.Windows.Forms.NumericUpDown numericRotCenterY;
        private System.Windows.Forms.NumericUpDown numericRotCenterX;
        private System.Windows.Forms.NumericUpDown numericMoveY;
        private System.Windows.Forms.Label labelRotAngle;
        private System.Windows.Forms.Label labelRotCenter;
        private System.Windows.Forms.NumericUpDown numericMoveX;
        private System.Windows.Forms.Button buttonApplyNumericEdit;
        private System.Windows.Forms.TableLayoutPanel tableLayoutDrawSettings;
        private System.Windows.Forms.Label labelRadiusOfPosSq;
        private System.Windows.Forms.Button buttonSelectContinuousVertices;
    }
}