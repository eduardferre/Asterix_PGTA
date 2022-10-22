using ClassLibrary;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AsterixDecoder
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
        
        public void AddRowT(CAT10 Message)
        {
            DataGridViewRow row = new DataGridViewRow();
            if (Message.SAC != null) row.Cells["SAC"].Value = Message.SAC; 
            else row.Cells["SAC"].Value = "No Data"; 

            if (Message.SIC != null) row.Cells["SIC"].Value = Message.SIC; 
            else  row.Cells["SIC"].Value = "No Data"; 

            if (Message.TargetId != null)
            {
                if (Message.TargetId.Replace(" ", "") != "") { row.Cells["Target Identification"].Value = Message.TargetId; }
                else { row.Cells["Target Identification"].Value = "No Data"; }
            }
            else row.Cells["Target Identification"].Value = "No Data"; 

            if (Message.TYP != null) row.Cells["Target Report Descriptor"].Value = "Click to expand";
            else  row.Cells["Target Report Descriptor"].Value = "No Data"; 

            if (Message.messageType != null) row.Cells["Message Type"].Value = Message.messageType; 
            else row.Cells["Message Type"].Value = "No Data";

            if (Message.FlightLevel != null) row.Cells["Flight Level"].Value = Message.FlightLevel; 
            else row.Cells["Flight Level"].Value = "No Data";

            if (Message.TrackNum != null) row.Cells["Track Number"].Value = Message.TrackNum; 
            else row.Cells["Track Number"].Value = "No Data";

            if (Message.TimeOfDay != null) row.Cells["Time of Day"].Value = Message.TimeOfDay; 
            else row.Cells["Time of Day"].Value = "No Data"; 

            if (Message.CNF != null) row.Cells["Track Status"].Value = "Click to expand";
            else  row.Cells["Track Status"].Value = "No Data"; 

            if (Message.LatitudeinWGS84 != null && Message.LongitudeinWGS84 != null) { row.Cells["Position in WGS-84 Co-ordinates"].Value = Message.LatitudeinWGS84 + ", " + Message.LongitudeinWGS84; }
            else { row.Cells["Position in WGS-84 Co-ordinates"].Value = "No Data"; }

            if (Message.positioninCartesianCoordinates != null) row.Cells["Position in Cartesian Co-ordinates"].Value = Message.positioninCartesianCoordinates; 
            else row.Cells["Position in Cartesian Co-ordinates"].Value = "No Data"; 

            if (Message.positioninPolarCoordinates != null) row.Cells["Position in Polar Co-ordinates"].Value = Message.positioninPolarCoordinates;
            else row.Cells["Position in Polar Co-ordinates"].Value = "No Data";

            if (Message.TrackVelocityPolarCoordinates != null) row.Cells["Track Velocity in Polar Coordinates"].Value = Message.TrackVelocityPolarCoordinates;
            else row.Cells["Track Velocity in Polar Coordinates"].Value = "No Data";

            if (Message.TrackVelocityCartesianCoordinates != null) { row.Cells["Track Velocity in\nCartesian\nCoordinates"].Value = Message.TrackVelocityCartesianCoordinates; }
            else { row.Cells["Track Velocity in\nCartesian\nCoordinates"].Value = "No Data"; }

            if (Message.targetOrientation != null) row.Cells["Target Size and Orientation"].Value = Message.targetOrientation;
            else row.Cells["Target Size and Orientation"].Value = "No Data";

            if (Message.TargetAdd != null) row.Cells["Target Address"].Value = Message.TargetAdd;
            else row.Cells["Target Address"].Value = "No Data";

            if (Message.NOGO != null) row.Cells["System Status"].Value = "Click to expand";
            else row.Cells["System Status"].Value = "No Data";

            if (Message.VFI != null) row.Cells["Vehicle Fleet Identification"].Value = Message.VFI;
            else row.Cells["Vehicle Fleet Identification"].Value = "No Data";

            if (Message.preProgrammedMessage != null) row.Cells["Pre-programmed Message"].Value = Message.preProgrammedMessage;
            else row.Cells["Pre-programmed Message"].Value = "No Data"; 

            if (Message.measuredHeight != null) row.Cells["Measured Height"].Value = Message.measuredHeight;
            else row.Cells["Measured Height"].Value = "No Data";

            if (Message.Mode3A != null) row.Cells["Mode-3A Code"].Value = Message.Mode3A;
            else row.Cells["Mode-3A Code"].Value = "No Data";

            if (Message.MBData != null) row.Cells["Mode S MB Data"].Value = "Click to expand";
            else row.Cells["Mode S MB Data"].Value = "No Data";

            if (Message.DeviationX != null) row.Cells["Standard Deviation of Position"].Value = "Click to expand";
            else row.Cells["Standard Deviation of Position"].Value = "No Data";

            if (Message.REPPresence != 0) row.Cells["Presence"].Value = "Click to expand";
            else row.Cells["Presence"].Value = "No Data";

            if (Message.PAM != null) row.Cells["Amplitude of Primary Plot"].Value = Message.PAM;
            else row.Cells["Amplitude of Primary Plot"].Value = "No Data";

            if (Message.Calculated_Acceleration != null) row.Cells["Calculated Acceleration"].Value = Message.Calculated_Acceleration;
            else row.Cells["Calculated Acceleration"].Value = "No Data";

            tableCAT10.Rows.Add(row);
        }

    }
}
