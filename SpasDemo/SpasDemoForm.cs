using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SpasDemo
{
  /// <summary>
  /// Demo class to show the basic functionality of a Spiroson-AS Flow Sensor.
  /// This demo samples flow data with 200Hz and displays the values in a textbox.
  /// The displayed flow is not BTPS corrected.
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
  /// Checksum calcualation and check.
  /// </remarks>
  public partial class SpasDemoForm : Form
  {
    #region -- Private Declarations --
    private enum SPIROSON_CMD
    {
      // Definition of Spiroson Interface Commands 
      Standby_On                = 17,          // switch Spiroson sensor off 
      Standby_Off               = 18,          // switch Spiroson sensor on                                             
      Start_Peak_Sampling       = 23,          // start sampling with inhibit bit set 
      Stop_Peak_Sampling        = 24,          // stops peak sampling, resets inhibit
      Get_Status                =203,          // request status information
    }
        
    private bool bWorkerThreadStoped = false; //Defines if the background worker thread is running

    private int iPacketCount = 0; //Packet0: Channel-Number+Data, Packet1: Data
    private int iData = 0;        //Flow data in 0.01 l/s
    private int iNewData = 0;     // new read data
    private bool bAux = false;    // indicate if the sent data is the checksum or an error
    #endregion
    
    /// <summary>
    /// Constructor: Initialize the Form-Dialog
    /// </summary>
    public SpasDemoForm()
    {
      InitializeComponent();
    }

    /// <summary>
    /// On Form Closing: Stop the sensor if running, stop all running threads
    /// </summary>
    private void SpasDemoForm_FormClosing(object sender, FormClosingEventArgs e)
    {
      //Stop the Sensor if running
      StopSensor();

      //Close the port
      serialPort1.Close();
      serialPort1.Dispose();
      
      //Cancel the backgroudnworker thread
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
      try
      {        
        //Init serial port with the selected COM-Port number
        if (!serialPort1.IsOpen)
        {
          //Port setting are defined in the Form-Designer. Set only Port-Name (number) here.
          serialPort1.PortName = comboBoxComPort.GetItemText(comboBoxComPort.SelectedItem);
          serialPort1.Open();
        }
        if (serialPort1.IsOpen)
        {
          //Reiinitialize all variables needed in DoDataHandling on Thread-Start
          iPacketCount = 0; //Packet0: Channel-Number+Data, Packet1: Data
          iData = 0;        //Flow data in 0.01 l/s
          iNewData = 0;     // new read data
          bAux = false;    // indicate if the sent data is the checksum or an error

          serialPort1.DiscardInBuffer();//Clear receive buffer
          serialPort1.ReadTimeout = 2000;//Set timeout to detect if no sensor is connected

          //Start the background worker thred to handle the data acquisition (polling the COM-Port)
          bWorkerThreadStoped = false;
          if(!backgroundWorker1.IsBusy)//check if worker thread is already running
            backgroundWorker1.RunWorkerAsync();

          //Start the Sensor: Send Start_Peak_Sampling command to the sensor
          SendSpiroson(SPIROSON_CMD.Start_Peak_Sampling);

        }
      }
      catch(System.Exception ex)
      {
        StopSensor();
        MessageBox.Show("Start-Error: "+ex.Message, "Error");

        buttonStartSensor.Enabled = true;
        buttonStopSensor.Enabled = false;
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
    /// Stop the background worker theard and stop the sensor
    /// </summary>
    private void StopSensor()
    {
      buttonStartSensor.Enabled = true;
      buttonStopSensor.Enabled = false;

      try
      {
        //Set receive timeout to -1 that serialPort.Read returns imediately
        serialPort1.ReadTimeout = -1;

        //Stop the worker thread
        bWorkerThreadStoped = true;
        //Cancel the backgroudnworker thread
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
          SendSpiroson(SPIROSON_CMD.Standby_On);
        }
      }
      catch (System.Exception ex)
      {
        MessageBox.Show("Stop-Error: " + ex.ToString(), "Error");
      }
    }

    /// <summary>
    /// Send data in the Spiroson-Protocol over a serialPort connection to the Device.
    /// Spiroson-Protocol: Start (0x01)--> Length (Command+Data) --> Command --> n*Data --> CRC-High --> CRC-Low
    /// All package parts are in byte-array byNewData
    /// </summary>
    /// <param name="byNewData">data in Spiroson-Protocol format</param>
    protected void SendData(byte[] byNewData)
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
      iCrc = Tools.CCrc.CalculateCRC(byData, iLength + 2);//iLength is the lenght without start-byte and length-byte --> +2
      //Add calculated CRC
      byData[iLength + 2] = (byte)((iCrc & 0xFF00) >> 8);
      byData[iLength + 3] = (byte)(iCrc & 0x00FF);

      //Check if null befor sending the data: If user changes serialPort1-Port, the serialPort1 is null for
      //a certain time!
      if (serialPort1 != null) serialPort1.Write(byData,0,byData.Length);
    }

    /// <summary>
    /// Send a command to the device, command only
    /// </summary>
    /// <param name="newCommand"></param>
    private void SendSpiroson(SPIROSON_CMD newCommand)
    {
      const byte length = 1;//only command, no data
      byte[] data = new byte[length];
      data[0] = (byte)newCommand;
      SendData(data);
    }

    /// <summary>
    /// Background worker thread to handle the Stop-Button during data acquisition
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
    { 
      while (!bWorkerThreadStoped)
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
      try
      {
        //Read one byte from the serial interface
        iNewData = serialPort1.ReadByte();
      }
      //catch(System.TimeoutException
      catch (System.TimeoutException et)
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
          bAux = true;
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

          //Display flow value
          try
          {
            SetFlowValue(dData);
          }
          catch (System.SystemException exx)
          {
            MessageBox.Show("Data-Error: " + exx.ToString(), "Error");
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
    /// Delegate definition to set the flow value asynchronous
    /// </summary>
    /// <param name="dVal"></param>
    private delegate void SetFlowValueCallback(double dVal);

    /// <summary>
    /// Set the flow value in the Form element out of another thread.
    /// This has to be solved over BeginInvoke because the form was 
    /// created in another thread than the call will be.
    /// </summary>
    /// <param name="dVal"></param>
    private void SetFlowValue(double dVal)
    {
      // InvokeRequired required compares the thread ID of the
      // calling thread to the thread ID of the creating thread.
      // If these threads are different, it returns true.
      //if (this.textBoxFlow.InvokeRequired)
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
          this.textBoxFlow.Text = Convert.ToString(dVal);

          //Set flow value to progressbar
          //Positive = Inspiration
          if (dVal > Convert.ToDouble(progressBarPlus.Maximum/10d))
            dVal = Convert.ToDouble(progressBarPlus.Maximum/10d);
          //Negative = Expiration
          if (dVal < -Convert.ToDouble(progressBarMinus.Maximum /10d))
            dVal = -Convert.ToDouble(progressBarMinus.Maximum/10d);
          if (dVal > 0d)
          {
            progressBarPlus.Value = Convert.ToInt32(Math.Abs(dVal) * 10);
            progressBarMinus.Value = 0;
          }
          else
          {
            progressBarMinus.Value = Convert.ToInt32(Math.Abs(dVal) * 10);
            progressBarPlus.Value = 0;
          }
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
            serialPort1.Close();

          //Port setting are defined in the Form-Designer. Set only Port-Name (number) here.
          serialPort1.PortName = comboBoxComPort.GetItemText(comboBoxComPort.SelectedItem);
          serialPort1.Open();
        }
      }
      catch
      {
      }
    }    
  }//end class

}//end namespace