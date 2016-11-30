namespace TraceMath
{
    partial class FormMain
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
            this.readyTimer = new System.Windows.Forms.Timer(this.components);
            this.updateTimer = new System.Windows.Forms.Timer(this.components);
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabelVnaInfo = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabelSpacer = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabelVersion = new System.Windows.Forms.ToolStripStatusLabel();
            this.comboBoxChannel = new System.Windows.Forms.ComboBox();
            this.comboBoxTraceA = new System.Windows.Forms.ComboBox();
            this.labelTraceA = new System.Windows.Forms.Label();
            this.comboBoxTraceB = new System.Windows.Forms.ComboBox();
            this.labelTraceB = new System.Windows.Forms.Label();
            this.panelMain = new System.Windows.Forms.Panel();
            this.groupBoxChannel = new System.Windows.Forms.GroupBox();
            this.groupBoxTraces = new System.Windows.Forms.GroupBox();
            this.buttonColorTraceB = new System.Windows.Forms.Button();
            this.buttonColorTraceA = new System.Windows.Forms.Button();
            this.groupBoxResultsTrace = new System.Windows.Forms.GroupBox();
            this.buttonColorTraceResults = new System.Windows.Forms.Button();
            this.radioButtonDivide = new System.Windows.Forms.RadioButton();
            this.radioButtonMultiply = new System.Windows.Forms.RadioButton();
            this.radioButtonSubtract = new System.Windows.Forms.RadioButton();
            this.radioButtonAdd = new System.Windows.Forms.RadioButton();
            this.comboBoxResultsTrace = new System.Windows.Forms.ComboBox();
            this.refreshTimer = new System.Windows.Forms.Timer(this.components);
            this.colorDialog = new System.Windows.Forms.ColorDialog();
            this.operationBackgroundWorker = new System.ComponentModel.BackgroundWorker();
            this.statusStrip.SuspendLayout();
            this.panelMain.SuspendLayout();
            this.groupBoxChannel.SuspendLayout();
            this.groupBoxTraces.SuspendLayout();
            this.groupBoxResultsTrace.SuspendLayout();
            this.SuspendLayout();
            // 
            // readyTimer
            // 
            this.readyTimer.Interval = 1000;
            this.readyTimer.Tick += new System.EventHandler(this.readyTimer_Tick);
            // 
            // updateTimer
            // 
            this.updateTimer.Interval = 1000;
            this.updateTimer.Tick += new System.EventHandler(this.updateTimer_Tick);
            // 
            // statusStrip
            // 
            this.statusStrip.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabelVnaInfo,
            this.toolStripStatusLabelSpacer,
            this.toolStripStatusLabelVersion});
            this.statusStrip.Location = new System.Drawing.Point(0, 273);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(284, 22);
            this.statusStrip.SizingGrip = false;
            this.statusStrip.TabIndex = 29;
            // 
            // toolStripStatusLabelVnaInfo
            // 
            this.toolStripStatusLabelVnaInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStripStatusLabelVnaInfo.Margin = new System.Windows.Forms.Padding(5, 3, 0, 2);
            this.toolStripStatusLabelVnaInfo.Name = "toolStripStatusLabelVnaInfo";
            this.toolStripStatusLabelVnaInfo.Padding = new System.Windows.Forms.Padding(5, 0, 0, 0);
            this.toolStripStatusLabelVnaInfo.Size = new System.Drawing.Size(27, 17);
            this.toolStripStatusLabelVnaInfo.Text = "     ";
            // 
            // toolStripStatusLabelSpacer
            // 
            this.toolStripStatusLabelSpacer.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStripStatusLabelSpacer.Name = "toolStripStatusLabelSpacer";
            this.toolStripStatusLabelSpacer.Size = new System.Drawing.Size(206, 17);
            this.toolStripStatusLabelSpacer.Spring = true;
            // 
            // toolStripStatusLabelVersion
            // 
            this.toolStripStatusLabelVersion.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Left;
            this.toolStripStatusLabelVersion.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStripStatusLabelVersion.ForeColor = System.Drawing.SystemColors.GrayText;
            this.toolStripStatusLabelVersion.Margin = new System.Windows.Forms.Padding(5, 3, 0, 2);
            this.toolStripStatusLabelVersion.Name = "toolStripStatusLabelVersion";
            this.toolStripStatusLabelVersion.Size = new System.Drawing.Size(26, 17);
            this.toolStripStatusLabelVersion.Text = "v ---";
            this.toolStripStatusLabelVersion.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // comboBoxChannel
            // 
            this.comboBoxChannel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxChannel.FormattingEnabled = true;
            this.comboBoxChannel.Location = new System.Drawing.Point(9, 21);
            this.comboBoxChannel.Name = "comboBoxChannel";
            this.comboBoxChannel.Size = new System.Drawing.Size(244, 21);
            this.comboBoxChannel.TabIndex = 30;
            this.comboBoxChannel.SelectedIndexChanged += new System.EventHandler(this.comboBoxChannel_SelectedIndexChanged);
            // 
            // comboBoxTraceA
            // 
            this.comboBoxTraceA.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxTraceA.FormattingEnabled = true;
            this.comboBoxTraceA.Location = new System.Drawing.Point(9, 28);
            this.comboBoxTraceA.Name = "comboBoxTraceA";
            this.comboBoxTraceA.Size = new System.Drawing.Size(215, 21);
            this.comboBoxTraceA.TabIndex = 33;
            this.comboBoxTraceA.SelectedIndexChanged += new System.EventHandler(this.comboBoxTraceA_SelectedIndexChanged);
            // 
            // labelTraceA
            // 
            this.labelTraceA.AutoSize = true;
            this.labelTraceA.Location = new System.Drawing.Point(8, 12);
            this.labelTraceA.Name = "labelTraceA";
            this.labelTraceA.Size = new System.Drawing.Size(48, 13);
            this.labelTraceA.TabIndex = 34;
            this.labelTraceA.Text = "Trace &A:";
            // 
            // comboBoxTraceB
            // 
            this.comboBoxTraceB.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxTraceB.FormattingEnabled = true;
            this.comboBoxTraceB.Location = new System.Drawing.Point(9, 71);
            this.comboBoxTraceB.Name = "comboBoxTraceB";
            this.comboBoxTraceB.Size = new System.Drawing.Size(215, 21);
            this.comboBoxTraceB.TabIndex = 35;
            this.comboBoxTraceB.SelectedIndexChanged += new System.EventHandler(this.comboBoxTraceB_SelectedIndexChanged);
            // 
            // labelTraceB
            // 
            this.labelTraceB.AutoSize = true;
            this.labelTraceB.Location = new System.Drawing.Point(8, 55);
            this.labelTraceB.Name = "labelTraceB";
            this.labelTraceB.Size = new System.Drawing.Size(48, 13);
            this.labelTraceB.TabIndex = 36;
            this.labelTraceB.Text = "Trace &B:";
            // 
            // panelMain
            // 
            this.panelMain.Controls.Add(this.groupBoxChannel);
            this.panelMain.Controls.Add(this.groupBoxTraces);
            this.panelMain.Controls.Add(this.groupBoxResultsTrace);
            this.panelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelMain.Location = new System.Drawing.Point(0, 0);
            this.panelMain.Name = "panelMain";
            this.panelMain.Size = new System.Drawing.Size(284, 295);
            this.panelMain.TabIndex = 37;
            // 
            // groupBoxChannel
            // 
            this.groupBoxChannel.Controls.Add(this.comboBoxChannel);
            this.groupBoxChannel.Location = new System.Drawing.Point(12, 12);
            this.groupBoxChannel.Name = "groupBoxChannel";
            this.groupBoxChannel.Size = new System.Drawing.Size(260, 54);
            this.groupBoxChannel.TabIndex = 43;
            this.groupBoxChannel.TabStop = false;
            this.groupBoxChannel.Text = "&Channel";
            // 
            // groupBoxTraces
            // 
            this.groupBoxTraces.Controls.Add(this.comboBoxTraceB);
            this.groupBoxTraces.Controls.Add(this.buttonColorTraceB);
            this.groupBoxTraces.Controls.Add(this.comboBoxTraceA);
            this.groupBoxTraces.Controls.Add(this.buttonColorTraceA);
            this.groupBoxTraces.Controls.Add(this.labelTraceA);
            this.groupBoxTraces.Controls.Add(this.labelTraceB);
            this.groupBoxTraces.Location = new System.Drawing.Point(12, 72);
            this.groupBoxTraces.Name = "groupBoxTraces";
            this.groupBoxTraces.Size = new System.Drawing.Size(260, 105);
            this.groupBoxTraces.TabIndex = 42;
            this.groupBoxTraces.TabStop = false;
            // 
            // buttonColorTraceB
            // 
            this.buttonColorTraceB.Location = new System.Drawing.Point(230, 70);
            this.buttonColorTraceB.Name = "buttonColorTraceB";
            this.buttonColorTraceB.Size = new System.Drawing.Size(23, 23);
            this.buttonColorTraceB.TabIndex = 41;
            this.buttonColorTraceB.UseVisualStyleBackColor = true;
            this.buttonColorTraceB.Click += new System.EventHandler(this.buttonColorTraceB_Click);
            // 
            // buttonColorTraceA
            // 
            this.buttonColorTraceA.Location = new System.Drawing.Point(230, 27);
            this.buttonColorTraceA.Name = "buttonColorTraceA";
            this.buttonColorTraceA.Size = new System.Drawing.Size(23, 23);
            this.buttonColorTraceA.TabIndex = 40;
            this.buttonColorTraceA.UseVisualStyleBackColor = true;
            this.buttonColorTraceA.Click += new System.EventHandler(this.buttonColorTraceA_Click);
            // 
            // groupBoxResultsTrace
            // 
            this.groupBoxResultsTrace.Controls.Add(this.buttonColorTraceResults);
            this.groupBoxResultsTrace.Controls.Add(this.radioButtonDivide);
            this.groupBoxResultsTrace.Controls.Add(this.radioButtonMultiply);
            this.groupBoxResultsTrace.Controls.Add(this.radioButtonSubtract);
            this.groupBoxResultsTrace.Controls.Add(this.radioButtonAdd);
            this.groupBoxResultsTrace.Controls.Add(this.comboBoxResultsTrace);
            this.groupBoxResultsTrace.Location = new System.Drawing.Point(12, 183);
            this.groupBoxResultsTrace.Name = "groupBoxResultsTrace";
            this.groupBoxResultsTrace.Size = new System.Drawing.Size(260, 79);
            this.groupBoxResultsTrace.TabIndex = 0;
            this.groupBoxResultsTrace.TabStop = false;
            this.groupBoxResultsTrace.Text = "&Results Trace";
            // 
            // buttonColorTraceResults
            // 
            this.buttonColorTraceResults.Location = new System.Drawing.Point(230, 18);
            this.buttonColorTraceResults.Name = "buttonColorTraceResults";
            this.buttonColorTraceResults.Size = new System.Drawing.Size(23, 23);
            this.buttonColorTraceResults.TabIndex = 42;
            this.buttonColorTraceResults.UseVisualStyleBackColor = true;
            this.buttonColorTraceResults.Click += new System.EventHandler(this.buttonColorTraceResults_Click);
            // 
            // radioButtonDivide
            // 
            this.radioButtonDivide.AutoSize = true;
            this.radioButtonDivide.Location = new System.Drawing.Point(206, 52);
            this.radioButtonDivide.Name = "radioButtonDivide";
            this.radioButtonDivide.Size = new System.Drawing.Size(50, 17);
            this.radioButtonDivide.TabIndex = 3;
            this.radioButtonDivide.TabStop = true;
            this.radioButtonDivide.Text = "A / B";
            this.radioButtonDivide.UseVisualStyleBackColor = true;
            this.radioButtonDivide.CheckedChanged += new System.EventHandler(this.radioButtonDivide_CheckedChanged);
            // 
            // radioButtonMultiply
            // 
            this.radioButtonMultiply.AutoSize = true;
            this.radioButtonMultiply.Location = new System.Drawing.Point(140, 52);
            this.radioButtonMultiply.Name = "radioButtonMultiply";
            this.radioButtonMultiply.Size = new System.Drawing.Size(50, 17);
            this.radioButtonMultiply.TabIndex = 2;
            this.radioButtonMultiply.TabStop = true;
            this.radioButtonMultiply.Text = "A x B";
            this.radioButtonMultiply.UseVisualStyleBackColor = true;
            this.radioButtonMultiply.CheckedChanged += new System.EventHandler(this.radioButtonMultiply_CheckedChanged);
            // 
            // radioButtonSubtract
            // 
            this.radioButtonSubtract.AutoSize = true;
            this.radioButtonSubtract.Location = new System.Drawing.Point(76, 52);
            this.radioButtonSubtract.Name = "radioButtonSubtract";
            this.radioButtonSubtract.Size = new System.Drawing.Size(48, 17);
            this.radioButtonSubtract.TabIndex = 1;
            this.radioButtonSubtract.TabStop = true;
            this.radioButtonSubtract.Text = "A - B";
            this.radioButtonSubtract.UseVisualStyleBackColor = true;
            this.radioButtonSubtract.CheckedChanged += new System.EventHandler(this.radioButtonSubtract_CheckedChanged);
            // 
            // radioButtonAdd
            // 
            this.radioButtonAdd.AutoSize = true;
            this.radioButtonAdd.Location = new System.Drawing.Point(9, 52);
            this.radioButtonAdd.Name = "radioButtonAdd";
            this.radioButtonAdd.Size = new System.Drawing.Size(51, 17);
            this.radioButtonAdd.TabIndex = 0;
            this.radioButtonAdd.TabStop = true;
            this.radioButtonAdd.Text = "A + B";
            this.radioButtonAdd.UseVisualStyleBackColor = true;
            this.radioButtonAdd.CheckedChanged += new System.EventHandler(this.radioButtonAdd_CheckedChanged);
            // 
            // comboBoxResultsTrace
            // 
            this.comboBoxResultsTrace.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxResultsTrace.FormattingEnabled = true;
            this.comboBoxResultsTrace.Location = new System.Drawing.Point(9, 19);
            this.comboBoxResultsTrace.Name = "comboBoxResultsTrace";
            this.comboBoxResultsTrace.Size = new System.Drawing.Size(215, 21);
            this.comboBoxResultsTrace.TabIndex = 38;
            this.comboBoxResultsTrace.SelectedIndexChanged += new System.EventHandler(this.comboBoxResultsTrace_SelectedIndexChanged);
            // 
            // refreshTimer
            // 
            this.refreshTimer.Tick += new System.EventHandler(this.refreshTimer_Tick);
            // 
            // colorDialog
            // 
            this.colorDialog.AnyColor = true;
            this.colorDialog.FullOpen = true;
            // 
            // operationBackgroundWorker
            // 
            this.operationBackgroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.operationBackgroundWorker_DoWork);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 295);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.panelMain);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "FormMain";
            this.Text = "< application title goes here >";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormMain_FormClosing);
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.panelMain.ResumeLayout(false);
            this.groupBoxChannel.ResumeLayout(false);
            this.groupBoxTraces.ResumeLayout(false);
            this.groupBoxTraces.PerformLayout();
            this.groupBoxResultsTrace.ResumeLayout(false);
            this.groupBoxResultsTrace.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Timer readyTimer;
        private System.Windows.Forms.Timer updateTimer;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelVnaInfo;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelSpacer;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelVersion;
        private System.Windows.Forms.ComboBox comboBoxChannel;
        private System.Windows.Forms.ComboBox comboBoxTraceA;
        private System.Windows.Forms.Label labelTraceA;
        private System.Windows.Forms.ComboBox comboBoxTraceB;
        private System.Windows.Forms.Label labelTraceB;
        private System.Windows.Forms.Panel panelMain;
        private System.Windows.Forms.GroupBox groupBoxResultsTrace;
        private System.Windows.Forms.RadioButton radioButtonDivide;
        private System.Windows.Forms.RadioButton radioButtonMultiply;
        private System.Windows.Forms.RadioButton radioButtonSubtract;
        private System.Windows.Forms.RadioButton radioButtonAdd;
        private System.Windows.Forms.ComboBox comboBoxResultsTrace;
        private System.Windows.Forms.Timer refreshTimer;
        private System.Windows.Forms.Button buttonColorTraceA;
        private System.Windows.Forms.ColorDialog colorDialog;
        private System.Windows.Forms.Button buttonColorTraceResults;
        private System.Windows.Forms.Button buttonColorTraceB;
        private System.Windows.Forms.GroupBox groupBoxTraces;
        private System.Windows.Forms.GroupBox groupBoxChannel;
        private System.ComponentModel.BackgroundWorker operationBackgroundWorker;
    }
}

