using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DCI.HRMS.Controls
{
    public partial class Ucl_DateInterval : UserControl
    {
        public delegate void DateChange_Handle();
        [Category("Action")]
        [Description("Fires when the MonthComboBox change.")]
        public event DateChange_Handle DateChange;
        protected virtual void On_DateChange()
        {
            if (DateChange != null)
            {
                DateChange();

            }

        }
        public Ucl_DateInterval()
        {
            InitializeComponent();
        }
        public DateTime DateFrom
        {
            set
            {
                DateText_Control1.Value = value;
            }

            get
            {
                return DateText_Control1.Value;
            }
        }
        public DateTime DateTo
        {
            set
            {
                dateText_Control2.Value = value;
            }
            get
            {
                return dateText_Control2.Value;
            }
        }
        public string DateLabel
        {
            set
            {
                kryptonLabel2.Text = value;

            }
            get
            {
                return kryptonLabel2.Text;
            }
        }

        private void DateText_Control1_ValueChanged(object sender, EventArgs e)
        {
            if (DateText_Control1.Value != DateTime.MinValue || DateText_Control1.Value != DateTime.Parse("01/01/1900").Date)
            {
                if (dateText_Control2.Value != DateTime.MinValue || dateText_Control2.Value != DateTime.Parse("01/01/1900").Date)
                {
                    if (DateText_Control1.Value > dateText_Control2.Value)
                    {
                        dateText_Control2.Value = DateText_Control1.Value;
                    }
                }
            }
            On_DateChange();
        }

        private void dateText_Control2_ValueChanged(object sender, EventArgs e)
        {
            if (dateText_Control2.Value != DateTime.MinValue || dateText_Control2.Value != DateTime.Parse("01/01/1900").Date)
            {
                if (DateText_Control1.Value != DateTime.MinValue || DateText_Control1.Value != DateTime.Parse("01/01/1900").Date)
                {
                    if (DateText_Control1.Value > dateText_Control2.Value)
                    {
                        DateText_Control1.Value = dateText_Control2.Value;
                    }
                }
            }
            On_DateChange();
        }




    }
}
