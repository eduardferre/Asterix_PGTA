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

            List<CAT10> listCAT10 = decode.ReadCAT10();
            List<CAT21> listCAT21 = decode.ReadCAT21();

            //foreach (CAT10 msg in listCAT10)
            //{
            //    AddRowT(msg);
            //}

            for (int i = 0; i < 5; i++)
            {
                CreateTable_CAT10(listCAT10[i]);
            }

            for (int i = 0; i < 5; i++)
            {
                CreateTable_CAT21(listCAT21[i]);
            }

        }

        public void CreateTable_CAT10(CAT10 Message)
        {
            int rowId = tableCAT10.Rows.Add();

            DataGridViewRow row = tableCAT10.Rows[rowId];
            if (Message.CAT != null) row.Cells["Category"].Value = Message.CAT;

            if (Message.SAC != null) row.Cells["SAC"].Value = Message.SAC;
            else row.Cells["SAC"].Value = "N/A";

            if (Message.SIC != null) row.Cells["SIC"].Value = Message.SIC;
            else row.Cells["SIC"].Value = "N/A";

            if (Message.TargetId != null)
            {
                if (Message.TargetId.Replace(" ", "") != "") { row.Cells["TargetIdentification"].Value = Message.TargetId; }
                else { row.Cells["TargetIdentification"].Value = "N/A"; }
            }
            else row.Cells["TargetIdentification"].Value = "N/A";

            if (Message.TYP != null) row.Cells["TargetReport"].Value = Message.TYP;
            else row.Cells["TargetReport"].Value = "N/A";

            if (Message.messageType != null) row.Cells["MessageType"].Value = Message.messageType;
            else row.Cells["MessageType"].Value = "N/A";

            if (Message.FlightLevelInfo != null) row.Cells["FlightLevel"].Value = Message.FlightLevelInfo;
            else row.Cells["FlightLevel"].Value = "N/A";

            if (Message.TrackNum != null) row.Cells["TrackNumber"].Value = Message.TrackNum;
            else row.Cells["TrackNumber"].Value = "N/A";

            if (Message.TimeOfDay != null) row.Cells["TimeofDay"].Value = Message.TimeOfDay;
            else row.Cells["TimeofDay"].Value = "N/A";

            if (Message.CNF != null) row.Cells["TrackStatus"].Value = Message.CNF;
            else row.Cells["TrackStatus"].Value = "N/A";

            if (Message.LatitudeinWGS84 != null && Message.LongitudeinWGS84 != null) row.Cells["PositioninWGS84Coordinates"].Value = Message.LatitudeinWGS84 + ", " + Message.LongitudeinWGS84;
            else row.Cells["PositioninWGS84Coordinates"].Value = "N/A";

            if (Message.positioninCartesianCoordinates != null) row.Cells["PositioninCartesianCoordinates"].Value = Message.positioninCartesianCoordinates;
            else row.Cells["PositioninCartesianCoordinates"].Value = "N/A";

            if (Message.positioninPolarCoordinates != null) row.Cells["PositioninPolarCoordinates"].Value = Message.positioninPolarCoordinates;
            else row.Cells["PositioninPolarCoordinates"].Value = "N/A";

            if (Message.TrackVelocityPolarCoordinates != null) row.Cells["TrackVelocityinPolarCoordinates"].Value = Message.TrackVelocityPolarCoordinates;
            else row.Cells["TrackVelocityinPolarCoordinates"].Value = "N/A";

            if (Message.TrackVelocityCartesianCoordinates != null) { row.Cells["TrackVelocityinCartesianCoordinates"].Value = Message.TrackVelocityCartesianCoordinates; }
            else { row.Cells["TrackVelocityinCartesianCoordinates"].Value = "N/A"; }

            if (Message.targetSizeOrientation != null) row.Cells["TargetSizeandOrientation"].Value = Message.targetSizeOrientation;
            else row.Cells["TargetSizeandOrientation"].Value = "N/A";

            if (Message.TargetAdd != null) row.Cells["TargetAddress"].Value = Message.TargetAdd;
            else row.Cells["TargetAddress"].Value = "N/A";

            if (Message.NOGO != null) row.Cells["SystemStatus"].Value = Message.NOGO;
            else row.Cells["SystemStatus"].Value = "N/A";

            if (Message.VFI != null) row.Cells["VehicleFleetIdentification"].Value = Message.VFI;
            else row.Cells["VehicleFleetIdentification"].Value = "N/A";

            if (Message.preProgrammedMessage != null) row.Cells["PreprogrammedMessage"].Value = Message.preProgrammedMessage;
            else row.Cells["PreprogrammedMessage"].Value = "N/A";

            if (Message.measuredHeight != null) row.Cells["MeasuredHeight"].Value = Message.measuredHeight;
            else row.Cells["MeasuredHeight"].Value = "N/A";

            if (Message.Mode3A != null) row.Cells["Mode3ACode"].Value = Message.Mode3A;
            else row.Cells["Mode3ACode"].Value = "N/A";

            if (Message.MBData != null) row.Cells["ModeSMBData"].Value = Message.MBData;
            else row.Cells["ModeSMBData"].Value = "N/A";

            if (Message.DeviationX != null) row.Cells["StandardDeviationofPosition"].Value = Message.DeviationX;
            else row.Cells["StandardDeviationofPosition"].Value = "N/A";

            if (Message.REPPresence != 0) row.Cells["Presence"].Value = Message.REPPresence;
            else row.Cells["Presence"].Value = "N/A";

            if (Message.PAM != null) row.Cells["AmplitudeofPrimaryPlot"].Value = Message.PAM;
            else row.Cells["AmplitudeofPrimaryPlot"].Value = "N/A";

            if (Message.Calculated_Acceleration != null) row.Cells["CalculatedAcceleration"].Value = Message.Calculated_Acceleration;
            else row.Cells["CalculatedAcceleration"].Value = "N/A";
        }

        public void CreateTable_CAT21(CAT21 Message)
        {
            int rowId = tableCAT21.Rows.Add();

            DataGridViewRow row = tableCAT21.Rows[rowId];
            if (Message.CAT != null) row.Cells["CategoryCAT21"].Value = Message.CAT;

            if (Message.SAC != null) row.Cells["SACCAT21"].Value = Message.SAC;
            else row.Cells["SACCAT21"].Value = "N/A";

            if (Message.SIC != null) row.Cells["SICCAT21"].Value = Message.SIC;
            else row.Cells["SICCAT21"].Value = "N/A";

            if (Message.TargetId != null)
            {
                if (Message.TargetId.Replace(" ", "") != "") { row.Cells["TargetIdentificationCAT21"].Value = Message.TargetId; }
                else { row.Cells["TargetIdentificationCAT21"].Value = "N/A"; }
            }
            else row.Cells["TargetIdentificationCAT21"].Value = "N/A";

            if (Message.trackNumber != null) row.Cells["TrackNumberCAT21"].Value = Message.trackNumber;
            else row.Cells["TrackNumberCAT21"].Value = "N/A";

            if (Message.ServiceId != null) row.Cells["ServiceIdentificationCAT21"].Value = Message.ServiceId;
            else row.Cells["ServiceIdentificationCAT21"].Value = "N/A";

            //if (Message.FlightLevelInfo != null) row.Cells["FlightLevel"].Value = Message.FlightLevelInfo;
            //else row.Cells["FlightLevel"].Value = "N/A";

            //if (Message.TrackNum != null) row.Cells["TrackNumber"].Value = Message.TrackNum;
            //else row.Cells["TrackNumber"].Value = "N/A";

            //if (Message.TimeOfDay != null) row.Cells["TimeofDay"].Value = Message.TimeOfDay;
            //else row.Cells["TimeofDay"].Value = "N/A";

            //if (Message.CNF != null) row.Cells["TrackStatus"].Value = Message.CNF;
            //else row.Cells["TrackStatus"].Value = "N/A";

            //if (Message.LatitudeinWGS84 != null && Message.LongitudeinWGS84 != null) row.Cells["PositioninWGS84Coordinates"].Value = Message.LatitudeinWGS84 + ", " + Message.LongitudeinWGS84;
            //else row.Cells["PositioninWGS84Coordinates"].Value = "N/A";

            //if (Message.positioninCartesianCoordinates != null) row.Cells["PositioninCartesianCoordinates"].Value = Message.positioninCartesianCoordinates;
            //else row.Cells["PositioninCartesianCoordinates"].Value = "N/A";

            //if (Message.positioninPolarCoordinates != null) row.Cells["PositioninPolarCoordinates"].Value = Message.positioninPolarCoordinates;
            //else row.Cells["PositioninPolarCoordinates"].Value = "N/A";

            //if (Message.TrackVelocityPolarCoordinates != null) row.Cells["TrackVelocityinPolarCoordinates"].Value = Message.TrackVelocityPolarCoordinates;
            //else row.Cells["TrackVelocityinPolarCoordinates"].Value = "N/A";

            //if (Message.TrackVelocityCartesianCoordinates != null) { row.Cells["TrackVelocityinCartesianCoordinates"].Value = Message.TrackVelocityCartesianCoordinates; }
            //else { row.Cells["TrackVelocityinCartesianCoordinates"].Value = "N/A"; }

            //if (Message.targetSizeOrientation != null) row.Cells["TargetSizeandOrientation"].Value = Message.targetSizeOrientation;
            //else row.Cells["TargetSizeandOrientation"].Value = "N/A";

            //if (Message.TargetAdd != null) row.Cells["TargetAddress"].Value = Message.TargetAdd;
            //else row.Cells["TargetAddress"].Value = "N/A";

            //if (Message.NOGO != null) row.Cells["SystemStatus"].Value = Message.NOGO;
            //else row.Cells["SystemStatus"].Value = "N/A";

            //if (Message.VFI != null) row.Cells["VehicleFleetIdentification"].Value = Message.VFI;
            //else row.Cells["VehicleFleetIdentification"].Value = "N/A";

            //if (Message.preProgrammedMessage != null) row.Cells["PreprogrammedMessage"].Value = Message.preProgrammedMessage;
            //else row.Cells["PreprogrammedMessage"].Value = "N/A";

            //if (Message.measuredHeight != null) row.Cells["MeasuredHeight"].Value = Message.measuredHeight;
            //else row.Cells["MeasuredHeight"].Value = "N/A";

            //if (Message.Mode3A != null) row.Cells["Mode3ACode"].Value = Message.Mode3A;
            //else row.Cells["Mode3ACode"].Value = "N/A";

            //if (Message.MBData != null) row.Cells["ModeSMBData"].Value = Message.MBData;
            //else row.Cells["ModeSMBData"].Value = "N/A";

            //if (Message.DeviationX != null) row.Cells["StandardDeviationofPosition"].Value = Message.DeviationX;
            //else row.Cells["StandardDeviationofPosition"].Value = "N/A";

            //if (Message.REPPresence != 0) row.Cells["Presence"].Value = Message.REPPresence;
            //else row.Cells["Presence"].Value = "N/A";

            //if (Message.PAM != null) row.Cells["AmplitudeofPrimaryPlot"].Value = Message.PAM;
            //else row.Cells["AmplitudeofPrimaryPlot"].Value = "N/A";

            //if (Message.Calculated_Acceleration != null) row.Cells["CalculatedAcceleration"].Value = Message.Calculated_Acceleration;
            //else row.Cells["CalculatedAcceleration"].Value = "N/A";
        }
    }
}
