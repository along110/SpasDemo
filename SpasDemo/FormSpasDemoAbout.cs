using System;
using System.Drawing;
using System.Configuration;
using System.ComponentModel;
using System.Windows.Forms;
using System.Runtime.InteropServices;
namespace SpasDemo
{
    /// <summary>
    /// About-Form
    /// </summary>
    public class FormSpasDemoAbout : System.Windows.Forms.Form
    {
        #region -- Private --

        private System.Windows.Forms.Button buttonClose;
        private System.ComponentModel.Container components = null;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.LinkLabel linkLabelNdd;
        private TextBox textBoxAddress;
        private System.Windows.Forms.Label labelSwVersionText;
        private Label labelSwVersion;
        #endregion
            
        #region -- Public --

        #endregion

        #region -- Constructor --
        /// <summary>
        /// Constructor for the About-Class
        /// </summary>
        public FormSpasDemoAbout()
        {
            InitializeComponent();

            //File version (assembly settings)
            labelSwVersion.Text = Application.ProductVersion.ToString();
        }
        #endregion

        #region -- Dispose --
        /// <summary>
        /// Clean Ressources
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
        }
        #endregion

        #region Vom Windows Form-Designer generierter Code
        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung. 
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormSpasDemoAbout));
            this.buttonClose = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.linkLabelNdd = new System.Windows.Forms.LinkLabel();
            this.labelSwVersion = new System.Windows.Forms.Label();
            this.labelSwVersionText = new System.Windows.Forms.Label();
            this.textBoxAddress = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonClose
            // 
            this.buttonClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            resources.ApplyResources(this.buttonClose, "buttonClose");
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
            // 
            // pictureBox1
            // 
            resources.ApplyResources(this.pictureBox1, "pictureBox1");
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.TabStop = false;
            // 
            // linkLabelNdd
            // 
            this.linkLabelNdd.FlatStyle = System.Windows.Forms.FlatStyle.System;
            resources.ApplyResources(this.linkLabelNdd, "linkLabelNdd");
            this.linkLabelNdd.Name = "linkLabelNdd";
            this.linkLabelNdd.TabStop = true;
            this.linkLabelNdd.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelNdd_LinkClicked);
            // 
            // labelSwVersion
            // 
            resources.ApplyResources(this.labelSwVersion, "labelSwVersion");
            this.labelSwVersion.Name = "labelSwVersion";
            // 
            // labelSwVersionText
            // 
            resources.ApplyResources(this.labelSwVersionText, "labelSwVersionText");
            this.labelSwVersionText.Name = "labelSwVersionText";
            // 
            // textBoxAddress
            // 
            this.textBoxAddress.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.textBoxAddress.BorderStyle = System.Windows.Forms.BorderStyle.None;
            resources.ApplyResources(this.textBoxAddress, "textBoxAddress");
            this.textBoxAddress.Name = "textBoxAddress";
            // 
            // FormSpasDemoAbout
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.CancelButton = this.buttonClose;
            this.Controls.Add(this.textBoxAddress);
            this.Controls.Add(this.labelSwVersion);
            this.Controls.Add(this.labelSwVersionText);
            this.Controls.Add(this.linkLabelNdd);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.buttonClose);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormSpasDemoAbout";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion
      
        /// <summary>
        /// Click function www.ndd.ch --> open browser
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void linkLabelNdd_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                VisitLink();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// Function to open the browser with the required link.
        /// </summary>
        private void VisitLink()
        {
            // Change the color of the link text by setting LinkVisited 
            // to true.
            linkLabelNdd.LinkVisited = true;
            //Call the Process.Start method to open the default browser 
            //with a URL:
            //System.Diagnostics.Process proc = System.Diagnostics.Process.Start("IExplore.exe", "http://www.ndd.ch");
            
            try
            {

                // alternative A: new window but allways microsoft internet explorer
                System.Diagnostics.Process myProcess = new System.Diagnostics.Process();
                myProcess.StartInfo.FileName = "iexplore";
                myProcess.StartInfo.Arguments = "http://www.ndd.ch";
                myProcess.Start();

                // alternative B: default browser but not new windows
                //System.Diagnostics.Process.Start("http://www.ndd.ch");
            }
            catch(System.ComponentModel.Win32Exception noBrowser)
            {
                if (noBrowser.ErrorCode == -2147467259)
                    MessageBox.Show(noBrowser.Message);
            }
            catch (System.Exception other)
            {
                MessageBox.Show(other.Message);
            }
        }

        /// <summary>
        /// Click function close button. Form is closed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }


    }
}
