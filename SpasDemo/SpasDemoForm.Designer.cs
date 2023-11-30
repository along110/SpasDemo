namespace SpasDemo
{
  partial class SpasDemoForm
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
      this.serialPort1 = new System.IO.Ports.SerialPort(this.components);
      this.comboBoxComPort = new System.Windows.Forms.ComboBox();
      this.labelComPort = new System.Windows.Forms.Label();
      this.buttonStartSensor = new System.Windows.Forms.Button();
      this.buttonStopSensor = new System.Windows.Forms.Button();
      this.textBoxFlow = new System.Windows.Forms.TextBox();
      this.progressBarPlus = new System.Windows.Forms.ProgressBar();
      this.progressBarMinus = new System.Windows.Forms.ProgressBar();
      this.label1 = new System.Windows.Forms.Label();
      this.label2 = new System.Windows.Forms.Label();
      this.label3 = new System.Windows.Forms.Label();
      this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
      this.SuspendLayout();
      // 
      // serialPort1
      // 
      this.serialPort1.BaudRate = 57600;
      this.serialPort1.ReadTimeout = 2000;
      // 
      // comboBoxComPort
      // 
      this.comboBoxComPort.FormattingEnabled = true;
      this.comboBoxComPort.Items.AddRange(new object[] {
            "COM1",
            "COM2",
            "COM3",
            "COM4",
            "COM5",
            "COM6",
            ""});
      this.comboBoxComPort.Location = new System.Drawing.Point(125, 24);
      this.comboBoxComPort.Name = "comboBoxComPort";
      this.comboBoxComPort.Size = new System.Drawing.Size(121, 21);
      this.comboBoxComPort.TabIndex = 0;
      this.comboBoxComPort.SelectedIndexChanged += new System.EventHandler(this.comboBoxComPort_SelectedIndexChanged);
      // 
      // labelComPort
      // 
      this.labelComPort.AutoSize = true;
      this.labelComPort.Location = new System.Drawing.Point(50, 27);
      this.labelComPort.Name = "labelComPort";
      this.labelComPort.Size = new System.Drawing.Size(56, 13);
      this.labelComPort.TabIndex = 1;
      this.labelComPort.Text = "COM-Port:";
      // 
      // buttonStartSensor
      // 
      this.buttonStartSensor.Location = new System.Drawing.Point(53, 80);
      this.buttonStartSensor.Name = "buttonStartSensor";
      this.buttonStartSensor.Size = new System.Drawing.Size(75, 23);
      this.buttonStartSensor.TabIndex = 2;
      this.buttonStartSensor.Text = "Start";
      this.buttonStartSensor.UseVisualStyleBackColor = true;
      this.buttonStartSensor.Click += new System.EventHandler(this.buttonStartSensor_Click);
      // 
      // buttonStopSensor
      // 
      this.buttonStopSensor.Enabled = false;
      this.buttonStopSensor.Location = new System.Drawing.Point(53, 118);
      this.buttonStopSensor.Name = "buttonStopSensor";
      this.buttonStopSensor.Size = new System.Drawing.Size(75, 23);
      this.buttonStopSensor.TabIndex = 3;
      this.buttonStopSensor.Text = "Stop";
      this.buttonStopSensor.UseVisualStyleBackColor = true;
      this.buttonStopSensor.Click += new System.EventHandler(this.buttonStopSensor_Click);
      // 
      // textBoxFlow
      // 
      this.textBoxFlow.Location = new System.Drawing.Point(236, 82);
      this.textBoxFlow.Name = "textBoxFlow";
      this.textBoxFlow.Size = new System.Drawing.Size(100, 20);
      this.textBoxFlow.TabIndex = 4;
      // 
      // progressBarPlus
      // 
      this.progressBarPlus.Location = new System.Drawing.Point(332, 118);
      this.progressBarPlus.Name = "progressBarPlus";
      this.progressBarPlus.Size = new System.Drawing.Size(100, 23);
      this.progressBarPlus.Step = 1;
      this.progressBarPlus.TabIndex = 5;
      // 
      // progressBarMinus
      // 
      this.progressBarMinus.Location = new System.Drawing.Point(236, 118);
      this.progressBarMinus.Name = "progressBarMinus";
      this.progressBarMinus.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
      this.progressBarMinus.RightToLeftLayout = true;
      this.progressBarMinus.Size = new System.Drawing.Size(100, 23);
      this.progressBarMinus.Step = 1;
      this.progressBarMinus.TabIndex = 6;
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(178, 85);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(53, 13);
      this.label1.TabIndex = 7;
      this.label1.Text = "Flow [l/s]:";
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Location = new System.Drawing.Point(222, 144);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(59, 26);
      this.label2.TabIndex = 8;
      this.label2.Text = "-10l/s\r\n(Expiration)";
      // 
      // label3
      // 
      this.label3.AutoSize = true;
      this.label3.Location = new System.Drawing.Point(414, 144);
      this.label3.Name = "label3";
      this.label3.Size = new System.Drawing.Size(61, 26);
      this.label3.TabIndex = 9;
      this.label3.Text = "10l/s\r\n(Inspiration)";
      // 
      // backgroundWorker1
      // 
      this.backgroundWorker1.WorkerSupportsCancellation = true;
      this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
      // 
      // SpasDemoForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
      this.ClientSize = new System.Drawing.Size(507, 184);
      this.Controls.Add(this.label3);
      this.Controls.Add(this.label2);
      this.Controls.Add(this.label1);
      this.Controls.Add(this.progressBarMinus);
      this.Controls.Add(this.progressBarPlus);
      this.Controls.Add(this.textBoxFlow);
      this.Controls.Add(this.buttonStopSensor);
      this.Controls.Add(this.buttonStartSensor);
      this.Controls.Add(this.labelComPort);
      this.Controls.Add(this.comboBoxComPort);
      this.Name = "SpasDemoForm";
      this.Text = "Spiroson-AS Demo-Tool";
      this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SpasDemoForm_FormClosing);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.IO.Ports.SerialPort serialPort1;
    private System.Windows.Forms.ComboBox comboBoxComPort;
    private System.Windows.Forms.Label labelComPort;
    private System.Windows.Forms.Button buttonStartSensor;
    private System.Windows.Forms.Button buttonStopSensor;
    private System.Windows.Forms.TextBox textBoxFlow;
    private System.Windows.Forms.ProgressBar progressBarPlus;
    private System.Windows.Forms.ProgressBar progressBarMinus;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.Label label3;
    private System.ComponentModel.BackgroundWorker backgroundWorker1;
  }
}

