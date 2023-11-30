namespace SpasDemo
{
    partial class FormSpasDemoMain
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
            System.Windows.Forms.DataVisualization.Charting.LineAnnotation lineAnnotation4 = new System.Windows.Forms.DataVisualization.Charting.LineAnnotation();
            System.Windows.Forms.DataVisualization.Charting.LineAnnotation lineAnnotation5 = new System.Windows.Forms.DataVisualization.Charting.LineAnnotation();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea3 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend3 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series5 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Title title3 = new System.Windows.Forms.DataVisualization.Charting.Title();
            System.Windows.Forms.DataVisualization.Charting.LineAnnotation lineAnnotation6 = new System.Windows.Forms.DataVisualization.Charting.LineAnnotation();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea4 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend4 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series6 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series7 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series8 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Title title4 = new System.Windows.Forms.DataVisualization.Charting.Title();
            this.serialPort1 = new System.IO.Ports.SerialPort(this.components);
            this.comboBoxComPort = new System.Windows.Forms.ComboBox();
            this.labelComPort = new System.Windows.Forms.Label();
            this.buttonStartSensor = new System.Windows.Forms.Button();
            this.buttonStopSensor = new System.Windows.Forms.Button();
            this.textBoxFlow = new System.Windows.Forms.TextBox();
            this.progressBarPlus = new System.Windows.Forms.ProgressBar();
            this.progressBarMinus = new System.Windows.Forms.ProgressBar();
            this.labelFlow = new System.Windows.Forms.Label();
            this.labelExpiration = new System.Windows.Forms.Label();
            this.labelInspiration = new System.Windows.Forms.Label();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.buttonOpenAbout = new System.Windows.Forms.Button();
            this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.chart2 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox2 = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart2)).BeginInit();
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
            this.comboBoxComPort.Location = new System.Drawing.Point(78, 30);
            this.comboBoxComPort.Name = "comboBoxComPort";
            this.comboBoxComPort.Size = new System.Drawing.Size(75, 21);
            this.comboBoxComPort.Sorted = true;
            this.comboBoxComPort.TabIndex = 0;
            this.comboBoxComPort.SelectedIndexChanged += new System.EventHandler(this.comboBoxComPort_SelectedIndexChanged);
            this.comboBoxComPort.Click += new System.EventHandler(this.comboBoxComPort_Click);
            // 
            // labelComPort
            // 
            this.labelComPort.AutoSize = true;
            this.labelComPort.Location = new System.Drawing.Point(16, 33);
            this.labelComPort.Name = "labelComPort";
            this.labelComPort.Size = new System.Drawing.Size(56, 13);
            this.labelComPort.TabIndex = 1;
            this.labelComPort.Text = "COM-Port:";
            // 
            // buttonStartSensor
            // 
            this.buttonStartSensor.Location = new System.Drawing.Point(78, 83);
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
            this.buttonStopSensor.Location = new System.Drawing.Point(78, 119);
            this.buttonStopSensor.Name = "buttonStopSensor";
            this.buttonStopSensor.Size = new System.Drawing.Size(75, 23);
            this.buttonStopSensor.TabIndex = 3;
            this.buttonStopSensor.Text = "Stop";
            this.buttonStopSensor.UseVisualStyleBackColor = true;
            this.buttonStopSensor.Click += new System.EventHandler(this.buttonStopSensor_Click);
            // 
            // textBoxFlow
            // 
            this.textBoxFlow.BackColor = System.Drawing.SystemColors.Window;
            this.textBoxFlow.Enabled = false;
            this.textBoxFlow.Location = new System.Drawing.Point(265, 86);
            this.textBoxFlow.Name = "textBoxFlow";
            this.textBoxFlow.ReadOnly = true;
            this.textBoxFlow.Size = new System.Drawing.Size(100, 20);
            this.textBoxFlow.TabIndex = 4;
            // 
            // progressBarPlus
            // 
            this.progressBarPlus.Location = new System.Drawing.Point(364, 119);
            this.progressBarPlus.Name = "progressBarPlus";
            this.progressBarPlus.Size = new System.Drawing.Size(100, 23);
            this.progressBarPlus.Step = 1;
            this.progressBarPlus.TabIndex = 5;
            // 
            // progressBarMinus
            // 
            this.progressBarMinus.Location = new System.Drawing.Point(265, 119);
            this.progressBarMinus.Name = "progressBarMinus";
            this.progressBarMinus.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.progressBarMinus.RightToLeftLayout = true;
            this.progressBarMinus.Size = new System.Drawing.Size(100, 23);
            this.progressBarMinus.Step = 1;
            this.progressBarMinus.TabIndex = 6;
            // 
            // labelFlow
            // 
            this.labelFlow.AutoSize = true;
            this.labelFlow.Location = new System.Drawing.Point(206, 88);
            this.labelFlow.Name = "labelFlow";
            this.labelFlow.Size = new System.Drawing.Size(53, 13);
            this.labelFlow.TabIndex = 7;
            this.labelFlow.Text = "Flow [l/s]:";
            // 
            // labelExpiration
            // 
            this.labelExpiration.AutoSize = true;
            this.labelExpiration.Location = new System.Drawing.Point(262, 150);
            this.labelExpiration.Name = "labelExpiration";
            this.labelExpiration.Size = new System.Drawing.Size(59, 26);
            this.labelExpiration.TabIndex = 8;
            this.labelExpiration.Text = "-10l/s\r\n(Expiration)";
            // 
            // labelInspiration
            // 
            this.labelInspiration.AutoSize = true;
            this.labelInspiration.Location = new System.Drawing.Point(403, 150);
            this.labelInspiration.Name = "labelInspiration";
            this.labelInspiration.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.labelInspiration.Size = new System.Drawing.Size(61, 26);
            this.labelInspiration.TabIndex = 9;
            this.labelInspiration.Text = "10l/s\r\n(Inspiration)";
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.WorkerSupportsCancellation = true;
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            // 
            // buttonOpenAbout
            // 
            this.buttonOpenAbout.Location = new System.Drawing.Point(389, 28);
            this.buttonOpenAbout.Name = "buttonOpenAbout";
            this.buttonOpenAbout.Size = new System.Drawing.Size(75, 23);
            this.buttonOpenAbout.TabIndex = 11;
            this.buttonOpenAbout.Text = "&About";
            this.buttonOpenAbout.UseVisualStyleBackColor = true;
            this.buttonOpenAbout.Click += new System.EventHandler(this.buttonOpenAbout_Click);
            // 
            // chart1
            // 
            lineAnnotation4.Height = 0D;
            lineAnnotation4.LineColor = System.Drawing.Color.Blue;
            lineAnnotation4.LineWidth = 5;
            lineAnnotation4.Name = "LineAnnotation1";
            lineAnnotation4.Width = 67D;
            lineAnnotation4.X = 10D;
            lineAnnotation4.Y = 1D;
            lineAnnotation4.YAxisName = "ChartArea1\\rY";
            lineAnnotation5.Height = 0D;
            lineAnnotation5.LineColor = System.Drawing.Color.Red;
            lineAnnotation5.LineWidth = 5;
            lineAnnotation5.Name = "LineAnnotation2";
            lineAnnotation5.Width = 67D;
            lineAnnotation5.X = 10D;
            lineAnnotation5.Y = 2D;
            lineAnnotation5.YAxisName = "ChartArea1\\rY";
            this.chart1.Annotations.Add(lineAnnotation4);
            this.chart1.Annotations.Add(lineAnnotation5);
            chartArea3.Name = "ChartArea1";
            this.chart1.ChartAreas.Add(chartArea3);
            legend3.Name = "Legend1";
            this.chart1.Legends.Add(legend3);
            this.chart1.Location = new System.Drawing.Point(10, 231);
            this.chart1.Margin = new System.Windows.Forms.Padding(1, 1, 1, 1);
            this.chart1.Name = "chart1";
            this.chart1.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.SeaGreen;
            series5.BorderWidth = 5;
            series5.ChartArea = "ChartArea1";
            series5.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            series5.Legend = "Legend1";
            series5.Name = "Paitent";
            series5.YValuesPerPoint = 2;
            this.chart1.Series.Add(series5);
            this.chart1.Size = new System.Drawing.Size(137, 324);
            this.chart1.TabIndex = 12;
            this.chart1.Text = "chart1";
            title3.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F);
            title3.Name = "Title1";
            title3.Text = "Flow Rate";
            this.chart1.Titles.Add(title3);
            this.chart1.Click += new System.EventHandler(this.chart1_Click);
            // 
            // chart2
            // 
            lineAnnotation6.AllowAnchorMoving = true;
            lineAnnotation6.AllowMoving = true;
            lineAnnotation6.AxisXName = "ChartArea1\\rX";
            lineAnnotation6.Height = 100D;
            lineAnnotation6.LineColor = System.Drawing.Color.Blue;
            lineAnnotation6.LineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Dash;
            lineAnnotation6.LineWidth = 3;
            lineAnnotation6.Name = "blueline";
            lineAnnotation6.Width = 0D;
            lineAnnotation6.X = 5D;
            lineAnnotation6.Y = 5D;
            this.chart2.Annotations.Add(lineAnnotation6);
            chartArea4.AxisX.Interval = 5D;
            chartArea4.AxisX.IsLabelAutoFit = false;
            chartArea4.AxisX.MajorGrid.Interval = 0D;
            chartArea4.AxisX.Maximum = 10D;
            chartArea4.AxisX.ScaleView.Zoomable = false;
            chartArea4.AxisX.Title = "Time (Seconds)";
            chartArea4.AxisY.Title = "Air Flow (Liters)";
            chartArea4.Name = "ChartArea1";
            this.chart2.ChartAreas.Add(chartArea4);
            legend4.Name = "Legend1";
            this.chart2.Legends.Add(legend4);
            this.chart2.Location = new System.Drawing.Point(247, 196);
            this.chart2.Margin = new System.Windows.Forms.Padding(1, 1, 1, 1);
            this.chart2.Name = "chart2";
            this.chart2.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.Berry;
            series6.BorderWidth = 5;
            series6.ChartArea = "ChartArea1";
            series6.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            series6.Color = System.Drawing.Color.Lime;
            series6.Legend = "Legend1";
            series6.Name = "Paitent";
            series7.BorderColor = System.Drawing.Color.Transparent;
            series7.BorderWidth = 3;
            series7.ChartArea = "ChartArea1";
            series7.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            series7.Color = System.Drawing.Color.Firebrick;
            series7.Legend = "Legend1";
            series7.Name = "Target";
            series8.BorderWidth = 2;
            series8.ChartArea = "ChartArea1";
            series8.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            series8.Color = System.Drawing.Color.Firebrick;
            series8.Legend = "Legend1";
            series8.Name = "TargetBack";
            this.chart2.Series.Add(series6);
            this.chart2.Series.Add(series7);
            this.chart2.Series.Add(series8);
            this.chart2.Size = new System.Drawing.Size(756, 357);
            this.chart2.TabIndex = 13;
            this.chart2.Text = "chart2";
            title4.Alignment = System.Drawing.ContentAlignment.TopCenter;
            title4.BorderWidth = 3;
            title4.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F);
            title4.Name = "Total Volume";
            title4.Text = "Flow Volume";
            this.chart2.Titles.Add(title4);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(460, 86);
            this.textBox1.Margin = new System.Windows.Forms.Padding(1, 1, 1, 1);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(64, 20);
            this.textBox1.TabIndex = 14;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(367, 86);
            this.label1.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(87, 13);
            this.label1.TabIndex = 15;
            this.label1.Text = "Total Volume (L):";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(928, 159);
            this.label2.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(33, 13);
            this.label2.TabIndex = 16;
            this.label2.Text = "Timer";
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(963, 156);
            this.textBox2.Margin = new System.Windows.Forms.Padding(1, 1, 1, 1);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(40, 20);
            this.textBox2.TabIndex = 17;
            // 
            // FormSpasDemoMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.ClientSize = new System.Drawing.Size(1124, 563);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.chart2);
            this.Controls.Add(this.chart1);
            this.Controls.Add(this.buttonOpenAbout);
            this.Controls.Add(this.labelInspiration);
            this.Controls.Add(this.labelExpiration);
            this.Controls.Add(this.labelFlow);
            this.Controls.Add(this.progressBarMinus);
            this.Controls.Add(this.progressBarPlus);
            this.Controls.Add(this.textBoxFlow);
            this.Controls.Add(this.buttonStopSensor);
            this.Controls.Add(this.buttonStartSensor);
            this.Controls.Add(this.labelComPort);
            this.Controls.Add(this.comboBoxComPort);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Name = "FormSpasDemoMain";
            this.Text = "Spiroson-AS Demo-Tool";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormSpasDemoMain_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart2)).EndInit();
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
        private System.Windows.Forms.Label labelFlow;
        private System.Windows.Forms.Label labelExpiration;
        private System.Windows.Forms.Label labelInspiration;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.Button buttonOpenAbout;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart2;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox2;
    }
}

