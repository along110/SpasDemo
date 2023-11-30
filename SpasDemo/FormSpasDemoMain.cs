using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
[assembly: CLSCompliant(true)]

namespace SpasDemo
{
    /// <summary>
    /// Demo class to show the basic functionality of a Spiroson-AS Flow Sensor.
    /// This demo samples flow data with 200Hz and displays the values in a textbox.
    /// The displayed flow is not BTPS (body temperature, pressure and saturation)
    /// corrected.
    /// The communication protocol of the Spiroson-AS requires a CRC calculation. This is
    /// implemented in the class CCrc and called in the SendData function.
    /// </summary>
    /// <remarks>
    /// Copyright © 2006 ndd Medizintechnik AG
    /// 
    /// This software is provided 'as-is', without any express or implied warranty.
    /// In no event will the authors be held liable for any damages arising from the
    /// use of this software.
    /// 
    /// ToDo:
    /// Checksum calcualation and check for the received sensor data.
    /// </remarks>
    public partial class FormSpasDemoMain : Form
    {
        #region -- Private Declarations --
        /// <summary>
        /// Structure of some commands, you could use to communicate with Spiroson-AS.
        /// </summary>
        private enum SPIROSON_CMD
        {
            /// <summary> switch Spiroson sensor off </summary>
            Standby_On = 17,

            /// <summary> switch Spiroson sensor on </summary>
            Standby_Off = 18,

            /// <summary> start sampling with inhibit bit set </summary>
            Start_Peak_Sampling = 23,

            /// <summary> stops peak sampling, resets inhibit </summary>
            Stop_Peak_Sampling = 24,

            /// <summary> request status information </summary>
            Get_Status = 203,
        }

        private int iPacketCount = 0;           // Packet0: Channel-Number+Data, Packet1: Data
        private int iData = 0;           // Flow data in 0.01 l/s
        private int iNewData = 0;           // new read data
        private bool bAux = false;       // indicate if the sent data is the checksum or an error
        private double totals = 0;
        private double seconds = 0;
        private double nettotal = 0;
        private double target = 0;
        private double ampmax = 0;
        private double ampmin = 0;
        private double scales = 0;
        private int count_up = 0;
        private int count_down = 0;
        private double period = 1;
        private double t1 = 0;
        private double t2 = 0;
        private int t1_t2 = 1;
        private float timer = 0.0f;
        private List<double> flowmax = new List<double>();
        #endregion

        /// <summary>
        /// Constructor: Initialize the Form-Dialog
        /// </summary>
        public FormSpasDemoMain()
        {
            //automatic generated initialization
            InitializeComponent();

            //Initialize more spezific
            comboBoxComPort.Items.Clear();
            comboBoxComPort.Items.AddRange(System.IO.Ports.SerialPort.GetPortNames());
        }

        /// <summary>
        /// On Form Closing: Stop the sensor if running, stop all running threads
        /// </summary>
        private void FormSpasDemoMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            //Stop the Sensor if running
            StopSensor();

            //Close the port
            serialPort1.Close();
            serialPort1.Dispose();

            //Cancel the backgroundworker thread
            if (backgroundWorker1.IsBusy)
            {
                backgroundWorker1.CancelAsync();
            }
        }

        /// <summary>
        /// Click Function Start-Button: Start the sensor and control the data handling:
        /// - Get data from the serial port
        /// - Display flow data
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonStartSensor_Click(object sender, EventArgs e)
        {
            buttonStartSensor.Enabled = false;
            buttonStopSensor.Enabled = true;
            comboBoxComPort.Enabled = false;
            try
            {
                //Init serial port with the selected COM-Port number
                if (!serialPort1.IsOpen)
                {
                    //Port settings are defined in the Form-Designer. Set only Port-Name (number) here.
                    serialPort1.PortName = comboBoxComPort.GetItemText(comboBoxComPort.SelectedItem);
                    serialPort1.Open();
                }
                if (serialPort1.IsOpen)
                {
                    //Reiinitialize all variables needed in DoDataHandling on Thread-Start
                    iPacketCount = 0; //Packet0: Channel-Number+Data, Packet1: Data
                    iData = 0;        //Flow data in 0.01 l/s
                    iNewData = 0;     //new read data
                    bAux = false;     //indicate if the sent data is the checksum or an error

                    serialPort1.DiscardInBuffer();  //Clear receive buffer
                    serialPort1.ReadTimeout = 2000; //Set timeout to detect if no sensor is connected

                    //Start the background worker thread to handle the data acquisition (polling the COM-Port)
                    if (!backgroundWorker1.IsBusy)//check if worker thread is already running
                        backgroundWorker1.RunWorkerAsync();

                    //Start the Sensor: Send Start_Peak_Sampling command to the sensor
                    SendSpirosonCommand(SPIROSON_CMD.Start_Peak_Sampling);

                }
            }
            catch (Exception ex)
            {
                StopSensor();
                MessageBox.Show("Start-Error: " + ex.Message, "Error");

                buttonStartSensor.Enabled = true;
                buttonStopSensor.Enabled = false;
                comboBoxComPort.Enabled = false;
            }
        }

        /// <summary>
        /// Click function Stop-Button: Stop the Sensor and the background worker thread
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonStopSensor_Click(object sender, EventArgs e)
        {
            StopSensor();
        }

        /// <summary>
        /// Stop the background worker thread and stop the sensor
        /// </summary>
        private void StopSensor()
        {
            buttonStartSensor.Enabled = true;
            buttonStopSensor.Enabled = false;
            comboBoxComPort.Enabled = true;

            try
            {
                //Set receive timeout to -1 that serialPort.Read returns imediately
                serialPort1.ReadTimeout = -1;

                //Cancel the backgroundworker thread
                if (backgroundWorker1.IsBusy)
                {
                    backgroundWorker1.CancelAsync();
                    while (backgroundWorker1.IsBusy)
                    {
                        Application.DoEvents();
                    }
                }

                if (serialPort1.IsOpen)
                {
                    //Stop the Sensor: Send Standby_On command to the sensor          
                    SendSpirosonCommand(SPIROSON_CMD.Standby_On);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Stop-Error: " + ex.ToString(), "Error");
            }
        }

        /// <summary>
        /// Send a command to the device, command only
        /// </summary>
        /// <param name="newCommand"></param>
        private void SendSpirosonCommand(SPIROSON_CMD newCommand)
        {
            const byte length = 1;//only command, no data
            byte[] data = new byte[length];
            data[0] = (byte)newCommand;
            SendData(data);
        }

        /// <summary>
        /// Send data in the Spiroson-Protocol over a serialPort connection to the Device.
        /// Spiroson-Protocol: Start (0x01)--> Length (Command+Data) --> Command --> n*Data --> CRC-High --> CRC-Low
        /// All package parts are in byte-array byNewData
        /// </summary>
        /// <param name="byNewData">data in Spiroson-Protocol format</param>
        private void SendData(byte[] byNewData)
        {
            int iLength = byNewData.Length;
            byte[] byData = new byte[iLength + 4];//add start,length,2*crc
            int iCrc = 0;

            byData[0] = 0x01;//Start byte
            byData[1] = (byte)(iLength);//length = command + data
            byData[2] = byNewData[0];//Command

            //Add data if available
            for (int i = 2; i <= iLength; i++)
            {
                byData[3 + (i - 2)] = byNewData[i - 1];
            }

            //Calculate CRC of the send data
            iCrc = Tools.CCrc.CalculateCrc(byData, iLength + 2);//iLength is the lenght without start-byte and length-byte --> +2
                                                                //Add calculated CRC
            byData[iLength + 2] = (byte)((iCrc & 0xFF00) >> 8);
            byData[iLength + 3] = (byte)(iCrc & 0x00FF);

            //Check if null befor sending the data: If user changes serialPort1-Port, the serialPort1 is null for
            //a certain time!
            if (serialPort1 != null) serialPort1.Write(byData, 0, byData.Length);
        }

        /// <summary>
        /// Background worker thread to handle the Stop-Button during data acquisition
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            while (!backgroundWorker1.CancellationPending)
            {
                //Handle the data received over the serial interface
                DoDataHandling();
            }
        }

        /// <summary>
        /// Read data from the Serial-Port, fill the data stream into flow values (double)
        /// and display the data.
        /// </summary>
        private void DoDataHandling()
        {
            //  double dDatatotal = 0;
            try
            {
                //Read one byte from the serial interface
                iNewData = serialPort1.ReadByte();
            }
            //catch error
            catch (TimeoutException et)
            {
                MessageBox.Show("Timeout-Error: " + et.Message, "Error");
                return;
            }
            catch
            {
                return;
            }
            if (iPacketCount == 0)
            {
                //Detect auxiliary channel (e.g. Checksum)
                if ((iNewData & 0xF0) != 0xF0)
                {
                    //Mask high byte data
                    iData = ((iNewData & 0x0F) << 8);
                    bAux = false;
                }
                else
                {
                    bAux = true;
                }
                iPacketCount++;
            }
            else if (iPacketCount == 1)
            {
                if (!bAux)
                {
                    //Add Packet2 data to Packet1 data
                    iData = iNewData + iData;
                    //Check if 12-bit data is minus
                    if ((iData & 0x0800) == 0x0800)
                    {
                        iData = iData - 0x1000;//set minus integer value
                    }
                    //Convert into double flow value in l/s
                    double dData = Convert.ToDouble(iData) * 0.01d;
                    //dDatatotal = dDatatotal + dData;

                    // noise filter
                      if (-0.1 < dData && dData < 0.1)
                      {
                          dData = 0;
                      }
                    //Display flow value
                    try
                    {
                        SetFlowValue(dData);
                    }
                    // catch error
                    catch (SystemException ex)
                    {
                        MessageBox.Show("Data-Error: " + ex.ToString(), "Error");
                    }
                }
                else
                {
                    //ToDo: Checksum Test
                }
                iPacketCount = 0;
            }

        }

        /// <summary>
        /// Delegate definition to set the flow value asynchronous.
        /// Could as well be handled through BackgroundWorker.ProgressChanged
        /// </summary>
        /// <param name="dVal">Flow value</param>
        private delegate void SetFlowValueCallback(double dVal);

        /// <summary>
        /// Set the flow value in the Form. Since this code runs in another 
        /// thread (the form was created in another thread than the call will be), 
        /// it has to be solved over BeginInvoke (or BackgroundWorker.ProcessChanged). 
        /// </summary>
        /// <param name="dVal">Flow value</param>
        private void SetFlowValue(double dVal)
        {
            // InvokeRequired required compares the thread ID of the
            // calling thread to the thread ID of the creating thread.
            // If these threads are different, it returns true.
            // if (this.textBoxFlow.InvokeRequired)
            if (this.InvokeRequired)
            {
                SetFlowValueCallback d = new SetFlowValueCallback(SetFlowValue);
                this.BeginInvoke(d, new object[] { dVal });
            }
            else
            {
                try
                {
                    //Set flow value in Textbox
                    this.textBoxFlow.Text = dVal.ToString();
                    // count up timer
                    //timer += Time.deltaTime;
                    //int tm = timer % 60;
                    //label2.Text = (TimeSpan.FromMinutes(5) - (DateTime.Now - startTime)).ToString("mm\:ss")
                    //Set flow value to progressbar
                    //Positive = Inspiration
                    if (dVal > Convert.ToDouble(progressBarPlus.Maximum / 10d))
                        dVal = Convert.ToDouble(progressBarPlus.Maximum / 10d);
                    //Negative = Expiration
                    if (dVal < -Convert.ToDouble(progressBarMinus.Maximum / 10d))
                        dVal = -Convert.ToDouble(progressBarMinus.Maximum / 10d);
                    if (dVal > 0d)
                    {
                        progressBarPlus.Value = Convert.ToInt32(Math.Abs(dVal) * 10);
                        progressBarMinus.Value = 0;
                        count_up = count_up + 1;
                        if (count_up == 10)
                        {
                            count_down = 0;
                        }

                        if (count_up == 500)
                        {
                            if (t1_t2 == 1)
                            {
                                t1 = seconds;
                            }
                            else
                            {
                                t2 = seconds;
                                t1_t2 = 1;
                            }

                        }

                    }
                    else
                    {
                        progressBarMinus.Value = Convert.ToInt32(Math.Abs(dVal) * 10);
                        progressBarPlus.Value = 0;
                        count_down = count_down + 1;
                        if (count_down == 10)
                        {
                            count_up = 0;
                        }
                        if (count_down == 500)
                        {
                            t1_t2 = 2;
                        }
                    }
                    ///////
                    ///
                    seconds = seconds + .005;
                    seconds = Math.Round(seconds, 3);
                    //double secs = Math.Round(seconds, 1);
                    //this.textBox2.Text = secs.ToString();

                    if (seconds < 10)
                    {
                        chart2.ChartAreas[0].AxisX.Maximum = seconds + 10;
                        chart2.ChartAreas[0].AxisX.Minimum = seconds - 10;
                        ///////
                        chart2.Annotations["blueline"].X = seconds;
                        ////////////// ADD-IN line chart /////////////////////////////////

                        //chart1.Invoke((MethodInvoker)(() => chart1.Series["Paitent"].Points.AddXY(DateTime.Now.ToLongTimeString(), dVal )));
                        totals = totals + Math.Abs(dVal) / 200;
                        nettotal = nettotal + dVal / 200;
                        flowmax.Add(nettotal);
                        target = Math.Sin(seconds);
                        this.textBox1.Text = totals.ToString();
                        chart2.Invoke((MethodInvoker)(() => chart2.Series["Paitent"].Points.AddXY(seconds, nettotal)));
                        chart2.Invoke((MethodInvoker)(() => chart2.Series["Target"].Points.AddXY(seconds, target)));
                        //chart2.Invoke((MethodInvoker)(() => chart2.Series["TargetBack"].Points.AddXY(seconds+6.2831, target)));
                    }
                    else if (seconds == 10)
                    {
                        MessageBox.Show("Calibration complete: Stop breathing, Testing will begin in 5 seconds");
                        chart2.Series["Paitent"].Points.Clear();
                        chart2.Series["Target"].Points.Clear();
                        chart2.Series["TargetBack"].Points.Clear();

                        //get aplitude max and min
                        ampmax = flowmax.Max();
                        ampmin = flowmax.Min();

                        scales = (ampmax - ampmin) / 2;
                        // reset nettotal to 0
                        nettotal = 0;
                        //set period coefficent
                        period = 6.28 / (t2 - t1);


                        /*   if ((ampmax + ampmin) < 1.5)
                           {

                               chart2.Series["Paitent"].Points.Clear();
                               chart2.Series["Target"].Points.Clear();
                               MessageBox.Show("calibration complete: testing will begin in 5 seconds");


                           }
                           else
                           {
                               chart2.Series["Paitent"].Points.Clear();
                               chart2.Series["Target"].Points.Clear();
                               MessageBox.Show("calibration error");
                               return;
                           }*/


                        //string message = "Do you want to close this window?";
                        //string title = "Close Window";
                        //MessageBoxButtons buttons = MessageBoxButtons.OK;
                        //DialogResult result = MessageBox.Show(message, title, buttons);
                        //while (pause == 1)
                        //{
                        //    Thread.Sleep(250);
                        //    if (result == DialogResult.OK)
                        //    {
                        //        pause = 0;
                        //    }
                        //}


                    }
                    else if (seconds > 15)
                    {
                        if (Math.Round(Math.IEEERemainder(seconds, 0.05), 3) == 0)
                        {
                            chart2.ChartAreas[0].AxisX.Maximum = seconds + 10;
                            chart2.ChartAreas[0].AxisX.Minimum = seconds - 10;
                            chart2.ChartAreas[0].AxisY.Maximum = 1;
                            chart2.ChartAreas[0].AxisY.Minimum = -1;
                            ///////
                            chart2.Annotations["blueline"].X = seconds;
                            ////////////// ADD-IN line chart /////////////////////////////////

                            //chart1.Invoke((MethodInvoker)(() => chart1.Series["Paitent"].Points.AddXY(DateTime.Now.ToLongTimeString(), dVal )));
                            totals = totals + Math.Abs(dVal) / 200;
                            nettotal = nettotal + dVal / 20;
                            double scal_tot = nettotal / scales;
                            target = Math.Sin(seconds * period);
                            this.textBox1.Text = totals.ToString();
                            chart2.Invoke((MethodInvoker)(() => chart2.Series["Paitent"].Points.AddXY(seconds, scal_tot)));
                            chart2.Invoke((MethodInvoker)(() => chart2.Series["Target"].Points.AddXY(seconds, target)));
                            //chart2.Invoke((MethodInvoker)(() => chart2.Series["TargetBack"].Points.AddXY(seconds , target)));
                        }
                    }
                    ///////////////////////////////////////////////
                }
                catch
                {
                    return;
                }
            }
        }




        /// <summary>
        /// Called if a new Com port is selected.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboBoxComPort_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                //Check if port has changed
                if (serialPort1.PortName != comboBoxComPort.GetItemText(comboBoxComPort.SelectedItem))
                {
                    //Check if current port is open
                    if (serialPort1.IsOpen)
                    {
                        StopSensor();
                        serialPort1.Close();
                    }

                    //Port setting are defined in the Form-Designer. Set only Port-Name (number) here.
                    serialPort1.PortName = comboBoxComPort.GetItemText(comboBoxComPort.SelectedItem);
                    serialPort1.Open();
                }
            }
            catch (SystemException ex)
            {
                MessageBox.Show("Port changing error: " + ex.ToString(), "Error");
            }
        }

        /// <summary>
        /// Open a ndd About-Screen
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonOpenAbout_Click(object sender, EventArgs e)
        {
            FormSpasDemoAbout formAbout = new FormSpasDemoAbout();
            formAbout.ShowDialog(this);
        }

        /// <summary>
        /// Refresh the COM-Port list in the comboBox on activation.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboBoxComPort_Click(object sender, EventArgs e)
        {
            //Initialize more spezific
            comboBoxComPort.Items.Clear();
            comboBoxComPort.Items.AddRange(System.IO.Ports.SerialPort.GetPortNames());
        }

        private void chart2_Click(object sender, EventArgs e)
        {

        }

        private void chart1_Click(object sender, EventArgs e)
        {

        }
    }//end class

}//end namespace