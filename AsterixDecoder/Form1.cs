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
            DecodeFiles decode = new DecodeFiles();

            List<CAT10> listCAT10 = decode.Read();

            foreach (CAT10 msg in listCAT10)
            {
                AddRowT(msg);
            }
        }
        
        public void AddRowT(CAT10 Message)
        {
            int rowId = tableCAT10.Rows.Add();

            DataGridViewRow row = tableCAT10.Rows[rowId];
            if (Message.CAT != null) row.Cells["Category"].Value = Message.CAT;

            if (Message.SAC != null) row.Cells["SAC"].Value = Message.SAC; 
            else row.Cells["SAC"].Value = "No Data"; 

            if (Message.SIC != null) row.Cells["SIC"].Value = Message.SIC; 
            else  row.Cells["SIC"].Value = "No Data"; 

            if (Message.TargetId != null)
            {
                if (Message.TargetId.Replace(" ", "") != "") { row.Cells["TargetIdentification"].Value = Message.TargetId; }
                else { row.Cells["TargetIdentification"].Value = "No Data"; }
            }
            else row.Cells["TargetIdentification"].Value = "No Data";

            if (Message.TYP != null) row.Cells["TargetReport"].Value = "Click to expand";
            else  row.Cells["TargetReport"].Value = "No Data"; 

            if (Message.messageType != null) row.Cells["MessageType"].Value = Message.messageType; 
            else row.Cells["MessageType"].Value = "No Data";

            if (Message.FlightLevel != null) row.Cells["FlightLevel"].Value = Message.FlightLevel; 
            else row.Cells["FlightLevel"].Value = "No Data";

            if (Message.TrackNum != null) row.Cells["TrackNumber"].Value = Message.TrackNum; 
            else row.Cells["TrackNumber"].Value = "No Data";

            if (Message.TimeOfDay != null) row.Cells["TimeofDay"].Value = Message.TimeOfDay; 
            else row.Cells["TimeofDay"].Value = "No Data"; 

            if (Message.CNF != null) row.Cells["TrackStatus"].Value = "Click to expand";
            else  row.Cells["TrackStatus"].Value = "No Data"; 

            if (Message.LatitudeinWGS84 != null && Message.LongitudeinWGS84 != null) { row.Cells["PositioninWGS84Coordinates"].Value = Message.LatitudeinWGS84 + ", " + Message.LongitudeinWGS84; }
            else { row.Cells["PositioninWGS84Coordinates"].Value = "No Data"; }

            if (Message.positioninCartesianCoordinates != null) row.Cells["PositioninCartesianCoordinates"].Value = Message.positioninCartesianCoordinates; 
            else row.Cells["PositioninCartesianCoordinates"].Value = "No Data"; 

            if (Message.positioninPolarCoordinates != null) row.Cells["PositioninPolarCoordinates"].Value = Message.positioninPolarCoordinates;
            else row.Cells["PositioninPolarCoordinates"].Value = "No Data";

            if (Message.TrackVelocityPolarCoordinates != null) row.Cells["TrackVelocityinPolarCoordinates"].Value = Message.TrackVelocityPolarCoordinates;
            else row.Cells["TrackVelocityinPolarCoordinates"].Value = "No Data";

            if (Message.TrackVelocityCartesianCoordinates != null) { row.Cells["TrackVelocityinCartesianCoordinates"].Value = Message.TrackVelocityCartesianCoordinates; }
            else { row.Cells["TrackVelocityinCartesianCoordinates"].Value = "No Data"; }

            if (Message.targetOrientation != null) row.Cells["TargetSizeandOrientation"].Value = Message.targetOrientation;
            else row.Cells["TargetSizeandOrientation"].Value = "No Data";

            if (Message.TargetAdd != null) row.Cells["TargetAddress"].Value = Message.TargetAdd;
            else row.Cells["TargetAddress"].Value = "No Data";

            if (Message.NOGO != null) row.Cells["SystemStatus"].Value = "Click to expand";
            else row.Cells["SystemStatus"].Value = "No Data";

            if (Message.VFI != null) row.Cells["VehicleFleetIdentification"].Value = Message.VFI;
            else row.Cells["VehicleFleetIdentification"].Value = "No Data";

            if (Message.preProgrammedMessage != null) row.Cells["PreprogrammedMessage"].Value = Message.preProgrammedMessage;
            else row.Cells["PreprogrammedMessage"].Value = "No Data"; 

            if (Message.measuredHeight != null) row.Cells["MeasuredHeight"].Value = Message.measuredHeight;
            else row.Cells["MeasuredHeight"].Value = "No Data";

            if (Message.Mode3A != null) row.Cells["Mode3ACode"].Value = Message.Mode3A;
            else row.Cells["Mode3ACode"].Value = "No Data";

            if (Message.MBData != null) row.Cells["ModeSMBData"].Value = "Click to expand";
            else row.Cells["ModeSMBData"].Value = "No Data";

            if (Message.DeviationX != null) row.Cells["StandardDeviationofPosition"].Value = "Click to expand";
            else row.Cells["StandardDeviationofPosition"].Value = "No Data";

            if (Message.REPPresence != 0) row.Cells["Presence"].Value = "Click to expand";
            else row.Cells["Presence"].Value = "No Data";

            if (Message.PAM != null) row.Cells["AmplitudeofPrimaryPlot"].Value = Message.PAM;
            else row.Cells["AmplitudeofPrimaryPlot"].Value = "No Data";

            if (Message.Calculated_Acceleration != null) row.Cells["CalculatedAcceleration"].Value = Message.Calculated_Acceleration;
            else row.Cells["CalculatedAcceleration"].Value = "No Data";

            tableCAT10.Rows.Add(row);
        }
    }
}
