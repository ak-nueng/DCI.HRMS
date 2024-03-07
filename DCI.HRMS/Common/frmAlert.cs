using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Media;

namespace DCI.HRMS.Common
{
    public partial class frmAlert : ComponentFactory.Krypton.Toolkit.KryptonForm
    {
        public enum MsgType {Error,Warning,Information};
        private SoundPlayer sndPlayer;
        public frmAlert()
        {
            InitializeComponent();
        }

        private void frmAlert_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        public void ShowMessage(IWin32Window ower, string message, string caption)
        {


            try
            {
                sndPlayer = new SoundPlayer("MessageAllert.wav");
                sndPlayer.LoadAsync();
                sndPlayer.Play();
            }
            catch 
            {
                
         
            }
            pictureBox1.Image = Properties.Resources.discussion;
            this.textBox1.Text = message;
            this.Text = caption;
            this.ShowDialog(ower);
        }
        public void ShowMessage(IWin32Window ower, string message,  MsgType t)
        {


            try
            {
                sndPlayer = new SoundPlayer("MessageAllert.wav");
                sndPlayer.LoadAsync();
                sndPlayer.Play();
            }
            catch
            {


            }

            switch (t)
            {
                case MsgType.Error:
                    pictureBox1.Image =  Properties.Resources.error;
                    break;
                case MsgType.Warning:
                    pictureBox1.Image = Properties.Resources.discussion;
                    break;
                case MsgType.Information:
                    pictureBox1.Image = Properties.Resources.checkmark;
                    break;
                default:
                    pictureBox1.Image = Properties.Resources.discussion;
                    break;
            }

            this.textBox1.Text = message;
            this.Text = t.ToString() ;
            this.ShowDialog(ower);
        }
    }
}