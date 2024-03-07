using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Diagnostics;
using DCI.HRMS.Model.Organize;

namespace DCI.HRMS.Model.Attendance
{
    [Serializable]
    public class ManpowerInfo : DivisionInfo
    {
        private PositionInfo []positions;
        private int totalEmpMale;
        private int totalEmpFemale;

        public ManpowerInfo()
        {
            positions = new PositionInfo[21]; ;
        }
        public ManpowerInfo(int totalPositions)
        {
            positions = new PositionInfo[totalPositions];
        }

        public PositionInfo[] Positions
        {
            get { return positions; }
            set { positions = value; }
        }
        public int TotalMale
        {
            get { return totalEmpMale; }
            set { totalEmpMale = value; }
        }
        public int TotalFemale
        {
            get { return totalEmpFemale; }
            set { totalEmpFemale = value; }
        }
        public int TotalEmployee
        {
            get
            {
                int total = 0;

                foreach (PositionInfo p in positions)
                {
                    total += p.TotalEmployee;
                }
                return total;
            }
        }

        public void SetPositions()
        {
            try
            {
                if (PositionInfo.CurrentPositions != null && positions.Length != PositionInfo.CurrentPositions.Count)
                {
                    positions = new PositionInfo[PositionInfo.CurrentPositions.Count];
                    for (int i = 0; i < positions.Length; i++)
                    {
                        positions[i] = (PositionInfo)PositionInfo.CurrentPositions[i];
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error: " + ex.Message);
            }
        }
    }
}
