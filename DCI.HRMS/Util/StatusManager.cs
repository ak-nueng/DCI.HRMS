using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace DCI.HRMS.Util
{
   
    public class StatusManager
    {
        private static Form MDIPa = null ;
        private static StatusStrip MDIStBar = null;
        private static ToolStripStatusLabel stsLbl = null;
        private static ToolStripProgressBar pgBar = null;

        public StatusManager(Form _mdipa)
        {
            MDIPa = _mdipa;
            MDIStBar = (StatusStrip)MDIPa.Controls["sbar"];
            stsLbl = (ToolStripStatusLabel)MDIStBar.Items["stsLbl"];
            pgBar = (ToolStripProgressBar)MDIStBar.Items["pgBar"];


        }
        public StatusManager()
        {
  
        }
        
        public string Status
        {
            set
            {
   
                stsLbl.Text = value;
                MDIPa.Update();
            }
            get
            {  
                return stsLbl.Text ;

            }


        }
        public int MaxProgress
        {
            set
            {
                Progress = 0;
                pgBar.Maximum = value;
            }
        }
    
        public int Progress
        {
            set
            {
                if (value != 0)
                {
                    pgBar.Visible = true;
                }
                else
                {
                    pgBar.Visible = false;
                }
                pgBar.Value = value; ;
            }
            get{
            return pgBar.Value;
            }
        }
     

    }
}
