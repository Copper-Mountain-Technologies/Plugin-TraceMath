// Copyright ©2016 Copper Mountain Technologies
//
// Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"),
// to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense,
// and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
// MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR
// ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH
// THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

using CopperMountainTech;
using System;
using System.Drawing;
using System.Numerics;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace TraceMath
{
    public partial class FormMain : Form
    {
        // ::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::

        private enum ComConnectionStateEnum
        {
            INITIALIZED,
            NOT_CONNECTED,
            CONNECTED_VNA_NOT_READY,
            CONNECTED_VNA_READY
        }

        private ComConnectionStateEnum previousComConnectionState = ComConnectionStateEnum.INITIALIZED;
        private ComConnectionStateEnum comConnectionState = ComConnectionStateEnum.NOT_CONNECTED;

        private int selectedChannel = -1;

        private int selectedTraceA = -1;
        private int selectedTraceB = -1;
        private int selectedTraceResults = 0;

        private Color colorTraceA = SystemColors.Control;
        private Color colorTraceB = SystemColors.Control;
        private Color colorTraceResults = SystemColors.Control;

        // ------------------------------------------------------------------------------------------------------------

        private enum TraceMathOperationTypeEnum
        {
            ADD,
            SUBTRACT,
            MULTIPLY,
            DIVIDE
        }

        private TraceMathOperationTypeEnum traceMathOperationType = TraceMathOperationTypeEnum.ADD;

        // ------------------------------------------------------------------------------------------------------------

        private enum TraceMathOperationStateEnum
        {
            ON_TRIGGER_NOT_READY,
            ON_TRIGGER_READY,
            OFF
        }

        private TraceMathOperationStateEnum previousTraceMathOperationState = TraceMathOperationStateEnum.OFF;
        private TraceMathOperationStateEnum traceMathOperationState = TraceMathOperationStateEnum.OFF;

        // ------------------------------------------------------------------------------------------------------------

        // previous trigger continuous mode
        private string previousTriggerContinuousMode = "";

        // previous trigger source
        private string previousTriggerSource = "";

        // ::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::

        public FormMain()
        {
            InitializeComponent();

            // --------------------------------------------------------------------------------------------------------

            // set form icon
            Icon = Properties.Resources.app_icon;

            // set form title
            Text = Program.programName;

            // disable resizing the window
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            MinimizeBox = true;

            // position the plug-in in the lower right corner of the screen
            Rectangle workingArea = Screen.GetWorkingArea(this);
            Location = new Point(workingArea.Right - Size.Width - 130,
                                 workingArea.Bottom - Size.Height - 50);

            // always display on top
            TopMost = true;

            // --------------------------------------------------------------------------------------------------------

            // disable ui
            panelMain.Enabled = false;

            // set version label text
            toolStripStatusLabelVersion.Text = "v" + Assembly.GetExecutingAssembly().GetName().Version.ToString(3);

            // --------------------------------------------------------------------------------------------------------

            // init trace math operation
            if (string.IsNullOrEmpty(Properties.Settings.Default.traceMathOperationSetting))
            {
                Properties.Settings.Default.traceMathOperationSetting = TraceMathOperationTypeEnum.ADD.ToString();
                Properties.Settings.Default.Save();
            }

            // update trace math operation
            string value = Properties.Settings.Default.traceMathOperationSetting;
            traceMathOperationType = (TraceMathOperationTypeEnum)Enum.Parse(typeof(TraceMathOperationTypeEnum), value);

            // init trace math operation radio buttons
            initTraceMathOperationRadioButtons(traceMathOperationType);

            // --------------------------------------------------------------------------------------------------------

            // update the channel selection combo box
            updateChanComboBox();

            // --------------------------------------------------------------------------------------------------------

            // init color button enabled state
            buttonColorTraceA.Enabled = false;
            buttonColorTraceB.Enabled = false;
            buttonColorTraceResults.Enabled = false;

            // init color button background color
            buttonColorTraceA.BackColor = colorTraceA;
            buttonColorTraceB.BackColor = colorTraceB;
            buttonColorTraceResults.BackColor = colorTraceResults;

            // --------------------------------------------------------------------------------------------------------

            // start the ready timer
            readyTimer.Interval = 250; // 250 ms interval
            readyTimer.Enabled = true;
            readyTimer.Start();

            // start the update timer
            updateTimer.Interval = 250; // 250 ms interval
            updateTimer.Enabled = true;
            updateTimer.Start();

            // start the refresh timer
            refreshTimer.Interval = 100; // 100 ms interval
            refreshTimer.Enabled = true;
            refreshTimer.Start();

            // --------------------------------------------------------------------------------------------------------
        }

        // ::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
        //
        // Timers
        //
        // ::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::

        private void readyTimer_Tick(object sender, EventArgs e)
        {
            try
            {
                // is vna ready?
                if (Program.vna.app.Ready)
                {
                    // yes... vna is ready
                    comConnectionState = ComConnectionStateEnum.CONNECTED_VNA_READY;
                }
                else
                {
                    // no... vna is not ready
                    comConnectionState = ComConnectionStateEnum.CONNECTED_VNA_NOT_READY;
                }
            }
            catch (COMException)
            {
                // com connection has been lost
                comConnectionState = ComConnectionStateEnum.NOT_CONNECTED;
                Application.Exit();
                return;
            }

            if (comConnectionState != previousComConnectionState)
            {
                previousComConnectionState = comConnectionState;

                switch (comConnectionState)
                {
                    default:
                    case ComConnectionStateEnum.NOT_CONNECTED:

                        // update vna info text box
                        toolStripStatusLabelVnaInfo.ForeColor = Color.White;
                        toolStripStatusLabelVnaInfo.BackColor = Color.Red;
                        toolStripStatusLabelSpacer.BackColor = toolStripStatusLabelVnaInfo.BackColor;
                        toolStripStatusLabelVnaInfo.Text = "VNA NOT CONNECTED";

                        // disable ui
                        panelMain.Enabled = false;

                        break;

                    case ComConnectionStateEnum.CONNECTED_VNA_NOT_READY:

                        // update vna info text box
                        toolStripStatusLabelVnaInfo.ForeColor = Color.White;
                        toolStripStatusLabelVnaInfo.BackColor = Color.Red;
                        toolStripStatusLabelSpacer.BackColor = toolStripStatusLabelVnaInfo.BackColor;
                        toolStripStatusLabelVnaInfo.Text = "VNA NOT READY";

                        // disable ui
                        panelMain.Enabled = false;

                        break;

                    case ComConnectionStateEnum.CONNECTED_VNA_READY:

                        // get vna info
                        Program.vna.PopulateInfo(Program.vna.app.NAME);

                        // update vna info text box
                        toolStripStatusLabelVnaInfo.ForeColor = SystemColors.ControlText;
                        toolStripStatusLabelVnaInfo.BackColor = SystemColors.Control;
                        toolStripStatusLabelSpacer.BackColor = toolStripStatusLabelVnaInfo.BackColor;
                        toolStripStatusLabelVnaInfo.Text = Program.vna.modelString + "   " + "SN:" + Program.vna.serialNumberString + "   " + Program.vna.versionString;

                        // enable ui
                        panelMain.Enabled = true;

                        break;
                }
            }
        }

        // ------------------------------------------------------------------------------------------------------------

        private void updateTimer_Tick(object sender, EventArgs e)
        {
            if (comConnectionState == ComConnectionStateEnum.CONNECTED_VNA_READY)
            {
                // update the channel combo box
                if ((comboBoxChannel.DroppedDown == false) &&
                    (comboBoxTraceA.DroppedDown == false) &&
                    (comboBoxTraceB.DroppedDown == false) &&
                    (comboBoxResultsTrace.DroppedDown == false))
                {
                    updateChanComboBox();
                }

                // get trace's color and update it's color selection button
                if (selectedTraceA != -1)
                {
                    colorTraceA = getTraceColorAndUpdateButton(buttonColorTraceA, selectedTraceA);
                }

                // get trace's color and update it's color selection button
                if (selectedTraceB != -1)
                {
                    colorTraceB = getTraceColorAndUpdateButton(buttonColorTraceB, selectedTraceB);
                }

                // get trace's color and update it's color selection button
                if (selectedTraceResults > 0)
                {
                    colorTraceResults = getTraceColorAndUpdateButton(buttonColorTraceResults, selectedTraceResults);
                }
            }
        }

        // ------------------------------------------------------------------------------------------------------------

        private void refreshTimer_Tick(object sender, EventArgs e)
        {
            bool continuousTriggerMode = false;
            string triggerSource = "";

            try
            {
                // get number of traces for the selected channel
                long numOfTraces = Program.vna.app.SCPI.CALCulate[selectedChannel].PARameter.COUNt;

                // validate all trace indexes
                if (selectedTraceA > numOfTraces)
                {
                    selectedTraceA = -1;
                }
                if (selectedTraceB > numOfTraces)
                {
                    selectedTraceB = -1;
                }
                if (selectedTraceResults > numOfTraces)
                {
                    selectedTraceResults = 0;
                }

                // should the math operation be performed?
                if ((selectedChannel != -1) &&
                    (selectedTraceA != -1) &&
                    (selectedTraceB != -1) &&
                    (selectedTraceResults != 0))
                {
                    // yes...

                    // is it already on?
                    if (traceMathOperationState != TraceMathOperationStateEnum.ON_TRIGGER_READY)
                    {
                        // no... turn it on
                        traceMathOperationState = TraceMathOperationStateEnum.ON_TRIGGER_NOT_READY;
                    }
                }
                else
                {
                    // no...

                    // turn off the math operation
                    traceMathOperationState = TraceMathOperationStateEnum.OFF;
                }

                // has the math operation state changed?
                if (traceMathOperationState != previousTraceMathOperationState)
                {
                    // yes...

                    previousTraceMathOperationState = traceMathOperationState;

                    switch (traceMathOperationState)
                    {
                        default:
                        case TraceMathOperationStateEnum.ON_TRIGGER_READY:

                            break;

                        case TraceMathOperationStateEnum.ON_TRIGGER_NOT_READY:

                            // save the trigger continuous mode
                            bool isContinuous = Program.vna.app.SCPI.INITiate[selectedChannel].CONTinuous;
                            previousTriggerContinuousMode = isContinuous.ToString();

                            // save the trigger source
                            previousTriggerSource = Program.vna.app.SCPI.TRIGger.SEQuence.SOURce;

                            // turn off continuous trigger mode
                            Program.vna.app.SCPI.INITiate[selectedChannel].CONTinuous = false;

                            // set trigger source to bus
                            Program.vna.app.SCPI.TRIGger.SEQuence.SOURce = "BUS";

                            // trigger ready
                            traceMathOperationState = TraceMathOperationStateEnum.ON_TRIGGER_READY;

                            break;

                        case TraceMathOperationStateEnum.OFF:

                            // restore the previous trigger settings
                            restoreTrigger(selectedChannel);

                            break;
                    }
                }

                // get current trigger settings
                if (traceMathOperationState == TraceMathOperationStateEnum.ON_TRIGGER_READY)
                {
                    continuousTriggerMode = Program.vna.app.SCPI.INITiate[selectedChannel].CONTinuous;
                    triggerSource = Program.vna.app.SCPI.TRIGger.SEQuence.SOURce;
                }
            }
            catch
            {
            }

            // should the math operation be performed?
            if (traceMathOperationState == TraceMathOperationStateEnum.ON_TRIGGER_READY)
            {
                // yes...

                // is the background worker in process?
                if (operationBackgroundWorker.IsBusy == false)
                {
                    // no...

                    // verify current trigger settings
                    if ((continuousTriggerMode == false) &&
                        (triggerSource == "BUS"))
                    {
                        // run the background task to perform the math operation
                        operationBackgroundWorker.RunWorkerAsync();
                    }
                    else
                    {
                        // trigger settings were changed manually...

                        // init results trace selection
                        initResultsTraceSelection();
                    }
                }
            }
        }

        // ------------------------------------------------------------------------------------------------------------

        private void restoreTrigger(int channel)
        {
            try
            {
                // restore the previous trigger source
                if (string.IsNullOrEmpty(previousTriggerSource) != true)
                {
                    Program.vna.app.SCPI.TRIGger.SEQuence.SOURce = previousTriggerSource;
                }

                // restore the trigger continuous mode
                if (string.IsNullOrEmpty(previousTriggerContinuousMode) != true)
                {
                    Program.vna.app.SCPI.INITiate[channel].CONTinuous = Convert.ToBoolean(previousTriggerContinuousMode);
                }
            }
            catch
            {
            }
        }

        // ::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
        //
        // Channel
        //
        // ::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::

        private void updateChanComboBox()
        {
            // save previously selected channel index
            int selectedChannelIndex = comboBoxChannel.SelectedIndex;

            // prevent combo box from flickering when update occurs
            comboBoxChannel.BeginUpdate();

            // clear channel selection combo box
            comboBoxChannel.Items.Clear();

            long splitIndex = 0;
            long activeChannel = 0;
            try
            {
                // get the split index (needed to determine number of channels)
                splitIndex = Program.vna.app.SCPI.DISPlay.SPLit;

                // determine the active channel
                activeChannel = Program.vna.app.SCPI.SERVice.CHANnel.ACTive;
            }
            catch
            {
            }

            // determine number of channels from the split index
            int numOfChannels = Program.vna.DetermineNumberOfChannels(splitIndex);

            // populate the channel number combo box
            for (int ch = 1; ch < numOfChannels + 1; ch++)
            {
                comboBoxChannel.Items.Add(ch.ToString());
            }

            if ((selectedChannelIndex == -1) ||
                (selectedChannelIndex >= comboBoxChannel.Items.Count))
            {
                // init channel selection to the active channel
                comboBoxChannel.Text = activeChannel.ToString();
            }
            else
            {
                // restore previous channel selection
                comboBoxChannel.SelectedIndex = selectedChannelIndex;
            }

            // prevent combo box from flickering when update occurs
            comboBoxChannel.EndUpdate();
        }

        private void comboBoxChannel_SelectedIndexChanged(object sender, EventArgs e)
        {
            // save previously selected trace indices
            int selectedTraceAIndex = comboBoxTraceA.SelectedIndex;
            int selectedTraceBIndex = comboBoxTraceB.SelectedIndex;
            int selectedResultsTraceIndex = comboBoxResultsTrace.SelectedIndex;

            // has channel selection changed?
            bool channelHasChanged = false;
            if (selectedChannel != comboBoxChannel.SelectedIndex + 1)
            {
                // yes...

                // set flag to indicate channel has changed
                channelHasChanged = true;

                // restore the previous trigger settings for previous channel
                restoreTrigger(selectedChannel);
                previousTriggerSource = "";
                previousTriggerContinuousMode = "";

                // restore previous results trace configuration for previous channel
                restoreResultsTrace(selectedChannel, selectedTraceResults);

                // update selected channel
                selectedChannel = comboBoxChannel.SelectedIndex + 1;
            }

            long numOfTraces = 1;

            try
            {
                // get number of traces for this channel
                numOfTraces = Program.vna.app.SCPI.CALCulate[selectedChannel].PARameter.COUNt;
            }
            catch
            {
            }

            // prevent combo boxes from flickering when update occurs
            comboBoxTraceA.BeginUpdate();
            comboBoxTraceB.BeginUpdate();
            comboBoxResultsTrace.BeginUpdate();

            // clear trace selection combo boxes
            comboBoxTraceA.Items.Clear();
            comboBoxTraceB.Items.Clear();
            comboBoxResultsTrace.Items.Clear();

            // init results trace combo box
            comboBoxResultsTrace.Items.Add("OFF");

            // loop thru all traces on the selected channel
            for (int trace = 1; trace < numOfTraces + 1; trace++)
            {
                string traceMeasParameter = "";
                try
                {
                    // get this trace's measurement parameter
                    traceMeasParameter = Program.vna.app.SCPI.CALCulate[selectedChannel].PARameter[trace].DEFine;
                }
                catch
                {
                }

                // populate trace selection combo boxes
                comboBoxTraceA.Items.Add("Tr " + trace.ToString() + " " + traceMeasParameter);
                comboBoxTraceB.Items.Add("Tr " + trace.ToString() + " " + traceMeasParameter);
                comboBoxResultsTrace.Items.Add("Tr " + trace.ToString() + " " + traceMeasParameter);
            }

            // prevent combo boxes from flickering when update occurs
            comboBoxTraceA.EndUpdate();
            comboBoxTraceB.EndUpdate();
            comboBoxResultsTrace.EndUpdate();

            // --------------------------------------------------------------------------------------------------------

            if (selectedTraceAIndex >= comboBoxTraceA.Items.Count)
            {
                // init trace selection
                comboBoxTraceA.SelectedIndex = -1;
                selectedTraceA = -1;

                // init trace color selection
                colorTraceA = SystemColors.Control;
                buttonColorTraceA.BackColor = colorTraceA;
                buttonColorTraceA.Enabled = false;
            }
            else
            {
                // restore previous trace selection
                comboBoxTraceA.SelectedIndex = selectedTraceAIndex;
            }

            // --------------------------------------------------------------------------------------------------------

            if (selectedTraceBIndex >= comboBoxTraceB.Items.Count)
            {
                // init trace selection
                comboBoxTraceB.SelectedIndex = -1;
                selectedTraceB = -1;

                // init trace color selection
                colorTraceB = SystemColors.Control;
                buttonColorTraceB.BackColor = colorTraceB;
                buttonColorTraceB.Enabled = false;
            }
            else
            {
                // restore previous trace selection
                comboBoxTraceB.SelectedIndex = selectedTraceBIndex;
            }

            // --------------------------------------------------------------------------------------------------------

            if ((channelHasChanged == true) ||
                (selectedResultsTraceIndex == -1) ||
                (selectedResultsTraceIndex >= comboBoxResultsTrace.Items.Count))
            {
                // init results trace selection
                initResultsTraceSelection();
            }
            else
            {
                // restore previous trace selection
                comboBoxResultsTrace.SelectedIndex = selectedResultsTraceIndex;
            }

            // --------------------------------------------------------------------------------------------------------
        }

        // ------------------------------------------------------------------------------------------------------------

        private void initResultsTraceSelection()
        {
            // restore previous results trace configuration
            restoreResultsTrace(selectedChannel, selectedTraceResults);

            // init trace selection to 'off'
            comboBoxResultsTrace.SelectedIndex = 0;
            selectedTraceResults = 0;

            // init trace color selection
            colorTraceResults = SystemColors.Control;
            buttonColorTraceResults.BackColor = colorTraceResults;
            buttonColorTraceResults.Enabled = false;
        }

        // ::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::

        private void comboBoxTraceA_SelectedIndexChanged(object sender, EventArgs args)
        {
            // has trace selection changed?
            if (selectedTraceA != comboBoxTraceA.SelectedIndex + 1)
            {
                // update selected trace
                selectedTraceA = comboBoxTraceA.SelectedIndex + 1;

                // get trace's color and update it's color selection button
                colorTraceA = getTraceColorAndUpdateButton(buttonColorTraceA, selectedTraceA);

                // enable trace's color selection button
                buttonColorTraceA.Enabled = true;
            }
        }

        // ------------------------------------------------------------------------------------------------------------

        private void comboBoxTraceB_SelectedIndexChanged(object sender, EventArgs args)
        {
            // has trace selection changed?
            if (selectedTraceB != comboBoxTraceB.SelectedIndex + 1)
            {
                // update selected trace
                selectedTraceB = comboBoxTraceB.SelectedIndex + 1;

                // get trace's color and update it's color selection button
                colorTraceB = getTraceColorAndUpdateButton(buttonColorTraceB, selectedTraceB);

                // enable trace color selection
                buttonColorTraceB.Enabled = true;
            }
        }

        // ------------------------------------------------------------------------------------------------------------

        private void comboBoxResultsTrace_SelectedIndexChanged(object sender, EventArgs args)
        {
            // has trace selection changed?
            if (selectedTraceResults != comboBoxResultsTrace.SelectedIndex)
            {
                // restore previous results trace configuration
                restoreResultsTrace(selectedChannel, selectedTraceResults);

                // update selected trace
                selectedTraceResults = comboBoxResultsTrace.SelectedIndex;

                // is results trace 'off'?
                if (selectedTraceResults > 0)
                {
                    // no...

                    // get trace's color and update it's color selection button
                    colorTraceResults = getTraceColorAndUpdateButton(buttonColorTraceResults, selectedTraceResults);

                    // enable trace color selection
                    buttonColorTraceResults.Enabled = true;
                }
                else
                {
                    // init trace color selection
                    colorTraceResults = SystemColors.Control;
                    buttonColorTraceResults.BackColor = colorTraceResults;
                    buttonColorTraceResults.Enabled = false;
                }
            }
        }

        // ::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::

        private void buttonTrigger_Click(object sender, EventArgs args)
        {
            try
            {
                object err;

                // set the trigger state to hold
                Program.vna.app.SCPI.INITiate[selectedChannel].CONTinuous = false;

                // make sure selected channel is active
                err = Program.vna.app.SCPI.DISPlay.WINDow[selectedChannel].ACTivate;

                // perform a single trigger
                err = Program.vna.app.SCPI.INITiate[selectedChannel].IMMediate;
            }
            catch (COMException e)
            {
                // display error message
                showMessageBoxForComException(e);
            }
        }

        // ::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::

        private void initTraceMathOperationRadioButtons(TraceMathOperationTypeEnum operation)
        {
            switch (operation)
            {
                default:
                case TraceMathOperationTypeEnum.ADD:
                    radioButtonAdd.Checked = true;
                    break;

                case TraceMathOperationTypeEnum.SUBTRACT:
                    radioButtonSubtract.Checked = true;
                    break;

                case TraceMathOperationTypeEnum.MULTIPLY:
                    radioButtonMultiply.Checked = true;
                    break;

                case TraceMathOperationTypeEnum.DIVIDE:
                    radioButtonDivide.Checked = true;
                    break;
            }
        }

        private void radioButtonAdd_CheckedChanged(object sender, EventArgs e)
        {
            if (sender is RadioButton)
            {
                if ((sender as RadioButton).Checked)
                {
                    // update trace math operation
                    traceMathOperationType = TraceMathOperationTypeEnum.ADD;
                    Properties.Settings.Default.traceMathOperationSetting = traceMathOperationType.ToString();
                    Properties.Settings.Default.Save();
                }
            }
        }

        private void radioButtonSubtract_CheckedChanged(object sender, EventArgs e)
        {
            if (sender is RadioButton)
            {
                if ((sender as RadioButton).Checked)
                {
                    // update trace math operation
                    traceMathOperationType = TraceMathOperationTypeEnum.SUBTRACT;
                    Properties.Settings.Default.traceMathOperationSetting = traceMathOperationType.ToString();
                    Properties.Settings.Default.Save();
                }
            }
        }

        private void radioButtonMultiply_CheckedChanged(object sender, EventArgs e)
        {
            if (sender is RadioButton)
            {
                if ((sender as RadioButton).Checked)
                {
                    // update trace math operation
                    traceMathOperationType = TraceMathOperationTypeEnum.MULTIPLY;
                    Properties.Settings.Default.traceMathOperationSetting = traceMathOperationType.ToString();
                    Properties.Settings.Default.Save();
                }
            }
        }

        private void radioButtonDivide_CheckedChanged(object sender, EventArgs e)
        {
            if (sender is RadioButton)
            {
                if ((sender as RadioButton).Checked)
                {
                    // update trace math operation
                    traceMathOperationType = TraceMathOperationTypeEnum.DIVIDE;
                    Properties.Settings.Default.traceMathOperationSetting = traceMathOperationType.ToString();
                    Properties.Settings.Default.Save();
                }
            }
        }

        // ::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::

        private void operationBackgroundWorker_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            // should the math operation be performed?
            if (traceMathOperationState == TraceMathOperationStateEnum.ON_TRIGGER_READY)
            {
                // yes...

                // perform selected trace math operation
                try
                {
                    object err;

                    // ------------------------------------------------------------------------------------------------

                    // generate a single trigger and wait for completion
                    err = Program.vna.app.SCPI.INITiate[selectedChannel].IMMediate;
                    err = Program.vna.app.SCPI.TRIGger.SEQuence.SINGle;

                    // retrieve unformatted data for trace A
                    double[] dataTraceA;
                    if (Program.vna.app.SCPI.DISPlay.WINDow[selectedChannel].TRACe[selectedTraceA].MEMory.STATe == true)
                    {
                        dataTraceA = Program.vna.app.SCPI.CALCulate(selectedChannel).Trace(selectedTraceA).DATA.FMEMory;
                    }
                    else
                    {
                        dataTraceA = Program.vna.app.SCPI.CALCulate[selectedChannel].Trace[selectedTraceA].DATA.FDATa;
                    }

                    // retrieve unformatted data for trace B
                    double[] dataTraceB;
                    if (Program.vna.app.SCPI.DISPlay.WINDow[selectedChannel].TRACe[selectedTraceB].MEMory.STATe == true)
                    {
                        dataTraceB = Program.vna.app.SCPI.CALCulate(selectedChannel).Trace(selectedTraceB).DATA.FMEMory;
                    }
                    else
                    {
                        dataTraceB = Program.vna.app.SCPI.CALCulate[selectedChannel].Trace[selectedTraceB].DATA.FDATa;
                    }

                    // ------------------------------------------------------------------------------------------------

                    double[] dataTraceResult = new double[dataTraceA.Length];

                    // iterate through trace data
                    for (int i = 0; i < dataTraceA.Length / 2; i = i + 1)
                    {
                        Complex complexResult = new Complex();

                        // create complex number values from traces A and B
                        Complex complexA = new Complex(dataTraceA[i * 2], dataTraceA[i * 2 + 1]);
                        Complex complexB = new Complex(dataTraceB[i * 2], dataTraceB[i * 2 + 1]);

                        // perform complex math depending on selected operation
                        if (radioButtonAdd.Checked == true)
                        {
                            // add
                            complexResult = complexA + complexB;
                        }
                        else if (radioButtonSubtract.Checked == true)
                        {
                            // subtract
                            complexResult = complexA - complexB;
                        }
                        else if (radioButtonMultiply.Checked == true)
                        {
                            // multiply
                            complexResult = complexA * complexB;
                        }
                        else
                        {
                            // divide
                            complexResult = complexA / complexB;
                        }

                        // put math operation results into trace data array
                        dataTraceResult[i * 2] = complexResult.Real;
                        dataTraceResult[i * 2 + 1] = complexResult.Imaginary;
                    }

                    // ------------------------------------------------------------------------------------------------

                    // turn off display of trace live data
                    Program.vna.app.SCPI.DISPlay.WINDow[selectedChannel].TRACe[selectedTraceResults].STATe = false;

                    // turn on display of trace memory
                    Program.vna.app.SCPI.DISPlay.WINDow[selectedChannel].TRACe[selectedTraceResults].MEMory.STATe = true;

                    // turn on display of marker memory value (in case markers are used)
                    if (Program.vna.family == VnaFamilyEnum.S2)
                    {
                        // note: s4 com command to turn on marker memory value does not exist -- s4 users will need to do this manually
                        Program.vna.app.SCPI.DISPlay.WINDow[selectedChannel].TRACe[selectedTraceResults].ANNotation.MARKer.MEMory = true;
                    }

                    // ------------------------------------------------------------------------------------------------

                    // write unformatted results data to results trace
                    Program.vna.app.SCPI.CALCulate[selectedChannel].Trace[selectedTraceResults].DATA.FMEMory = dataTraceResult;

                    // refresh display
                    if (Program.vna.family == VnaFamilyEnum.S2)
                    {
                        err = Program.vna.app.SCPI.DISPlay.REFResh.IMMediate;
                    }
                    else if (Program.vna.family == VnaFamilyEnum.S4)
                    {
                        err = Program.vna.app.SCPI.DISPlay.UPDate.IMMediate;
                    }

                    // ################################################################################################
                    // FOR TESTING
                    /*
                    bool caught = false;
                    foreach (double d in dataTraceResult)
                    {
                        if (d != 0)
                        {
                            caught = true;
                        }
                    }

                    if (caught)
                    {
                        System.Diagnostics.Debug.WriteLine("TRACE A =====================================================");
                        foreach (double d in dataTraceA)
                        {
                            System.Diagnostics.Debug.WriteLine(d.ToString());
                        }
                        System.Diagnostics.Debug.WriteLine("");
                        System.Diagnostics.Debug.WriteLine("TRACE B =====================================================");
                        foreach (double d in dataTraceB)
                        {
                            System.Diagnostics.Debug.WriteLine(d.ToString());
                        }
                        System.Diagnostics.Debug.WriteLine("");
                        System.Diagnostics.Debug.WriteLine("TRACE RESULTS ===============================================");
                        foreach (double d in dataTraceResult)
                        {
                            System.Diagnostics.Debug.WriteLine(d.ToString());
                        }
                        System.Diagnostics.Debug.WriteLine("");
                        System.Diagnostics.Debug.WriteLine("");
                        System.Diagnostics.Debug.WriteLine("!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
                    }
                    */
                    // ################################################################################################

                    // ------------------------------------------------------------------------------------------------
                }
                catch
                {
                }
            }
        }

        // ::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::

        private void restoreResultsTrace(int channel, int resultsTrace)
        {
            // call this function to restore the current results trace before a new results trace is selected

            long numOfTraces = 0;
            try
            {
                // get number of traces for the selected channel
                numOfTraces = Program.vna.app.SCPI.CALCulate[channel].PARameter.COUNt;

                // loop thru all traces
                for (int trace = 1; trace < numOfTraces + 1; trace++)
                {
                    // results trace found?
                    if (trace == resultsTrace)
                    {
                        // yes...

                        // turn off display of marker memory value
                        if (Program.vna.family == VnaFamilyEnum.S2)
                        {
                            Program.vna.app.SCPI.DISPlay.WINDow[channel].TRACe[trace].ANNotation.MARKer.MEMory = false;
                        }

                        // turn off display of trace memory
                        Program.vna.app.SCPI.DISPlay.WINDow[channel].TRACe[trace].MEMory.STATe = false;

                        // turn on display of trace live data
                        Program.vna.app.SCPI.DISPlay.WINDow[channel].TRACe[trace].STATe = true;
                    }
                }
            }
            catch
            {
            }
        }

        // ::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::

        private void buttonColorTraceA_Click(object sender, EventArgs args)
        {
            if (selectedTraceA != -1)
            {
                colorTraceA = setTraceColor(buttonColorTraceA, selectedTraceA, colorTraceA);
            }
        }

        // ------------------------------------------------------------------------------------------------------------

        private void buttonColorTraceB_Click(object sender, EventArgs e)
        {
            if (selectedTraceB != -1)
            {
                colorTraceB = setTraceColor(buttonColorTraceB, selectedTraceB, colorTraceB);
            }
        }

        // ------------------------------------------------------------------------------------------------------------

        private void buttonColorTraceResults_Click(object sender, EventArgs e)
        {
            if (selectedTraceResults > 0)
            {
                colorTraceResults = setTraceColor(buttonColorTraceResults, selectedTraceResults, colorTraceResults);
            }
        }

        // ------------------------------------------------------------------------------------------------------------

        private Color setTraceColor(Button button, int trace, Color currentColor)
        {
            Color color = currentColor;

            // init color dialog
            colorDialog.Color = color;

            // check for results trace
            bool isResultsTrace = false;
            if (button == buttonColorTraceResults)
            {
                isResultsTrace = true;
            }

            // show color dialog
            if (colorDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    // set trace's color
                    double[] colorArray = new double[] { colorDialog.Color.R, colorDialog.Color.G, colorDialog.Color.B };
                    if (isResultsTrace == true)
                    {
                        Program.vna.app.SCPI.DISPlay.COLor.TRACe[trace].MEMory = colorArray;
                    }
                    else
                    {
                        Program.vna.app.SCPI.DISPlay.COLor.TRACe[trace].DATA = colorArray;
                    }
                }
                catch (COMException e)
                {
                    // display error message
                    showMessageBoxForComException(e);
                    return color;
                }

                // update return color
                color = colorDialog.Color;

                // update button color
                button.BackColor = color;
            }

            // return trace's color
            return color;
        }

        // ------------------------------------------------------------------------------------------------------------

        private Color getTraceColorAndUpdateButton(Button button, int trace)
        {
            Color color = SystemColors.Control;

            // check for results trace
            bool isResultsTrace = false;
            if (button == buttonColorTraceResults)
            {
                isResultsTrace = true;
            }

            try
            {
                // get trace's color
                if (isResultsTrace == true)
                {
                    double[] colorArray = Program.vna.app.SCPI.DISPlay.COLor.TRACe[trace].MEMory;
                    color = Color.FromArgb((int)colorArray[0], (int)colorArray[1], (int)colorArray[2]);
                }
                else
                {
                    double[] colorArray = Program.vna.app.SCPI.DISPlay.COLor.TRACe[trace].DATA;
                    color = Color.FromArgb((int)colorArray[0], (int)colorArray[1], (int)colorArray[2]);
                }
            }
            catch
            {
            }

            // update button color
            button.BackColor = color;

            // return trace's color
            return color;
        }

        // ::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::

        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            // restore the previous trigger settings
            restoreTrigger(selectedChannel);
        }

        // ::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::

        private void showMessageBoxForComException(COMException e)
        {
            MessageBox.Show(Program.vna.GetUserMessageForComException(e),
                Program.programName,
                MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        // ::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
    }
}