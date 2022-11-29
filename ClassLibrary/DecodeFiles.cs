using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using ClassLibrary;

namespace ClassLibrary
{
    public class DecodeFiles
    {
        public int numFiles = 0;

        public int numMsgs = 0;
        public int numCAT10Msgs = 0;
        public int numCAT10SMRMsgs = 0;
        public int numCAT10MLATMsgs = 0;
        public int numCAT21Msgs = 0;
       
        public string process = "Select a file to decode";

        List<CAT10> listCAT10 = new List<CAT10>();
        List<CAT21> listCAT21 = new List<CAT21>();
        List<CATALL> listCATALL = new List<CATALL>();

        DataTable tableCAT10 = new DataTable();
        DataTable tableCAT21 = new DataTable();

        List<Trajectories> SMRTraj = new List<Trajectories>(); 
        List<Trajectories> MLATTraj = new List<Trajectories>(); 
        List<Trajectories> ADSBTraj = new List<Trajectories>(); 


        public List<string> nameFiles = new List<string>();


        public List<CAT10> GetListCAT10() { return listCAT10; }
        public List<CAT21> GetListCAT21() { return listCAT21; }
       // public List<CATALL> GetListCATALL() { return listCATALL; }
        public DataTable GetTableCAT10() { return tableCAT10; }
        public DataTable GetTableCAT21() { return tableCAT21; }

        public DecodeFiles() 
        {
            this.CreateTableCAT10();
            this.CreateTableCAT21();
        }

        public void ClearStoredData()
        {
            numFiles = 0;
            numMsgs = 0;
            numCAT10Msgs = 0;
            numCAT10SMRMsgs = 0;
            numCAT10MLATMsgs = 0;
            numCAT21Msgs = 0;
            nameFiles.Clear();
            listCAT10.Clear();
            listCAT21.Clear();
            listCATALL.Clear();
            tableCAT10.Clear();
            tableCAT21.Clear();
        }

        public List<CAT10> ReadCAT10()
        {
            byte[] fileBytes = File.ReadAllBytes("C:\\Users\\Eduard\\Desktop\\Asterix_PGTA\\AsterixDecoder\\201002-lebl-080001_smr.ast");
            List<byte[]> listBytes = new List<byte[]>();

            int cont = fileBytes[1] * 256 + fileBytes[2];
            int i = 0;

            while (i < fileBytes.Length)
            {
                byte[] array = new byte[cont];

                for (int j = 0; j < array.Length; j++)
                {
                    array[j] = fileBytes[i];
                    i++;
                }

                listBytes.Add(array);

                if (i + 2 < fileBytes.Length) cont = fileBytes[i + 1] * 256 + fileBytes[i + 2];
            }

            List<string[]> listHex = new List<string[]>();

            for (i = 0; i < listBytes.Count; i++)
            {
                byte[] byteSelect = listBytes[i];
                string[] arrayHex = new string[byteSelect.Length];

                for (int j = 0; j < byteSelect.Length; j++)
                {
                    arrayHex[j] = byteSelect[j].ToString("X");

                    if (arrayHex[j].Length <= 1) arrayHex[j] = string.Concat('0', arrayHex[j]);
                }

                listHex.Add(arrayHex);
            }

            for (i = 0; i < listHex.Count; i++)
            {
                process = "Loading message " + Convert.ToString(i) + " of " + Convert.ToString(listHex.Count) + " messages...";

                string[] arrayMsg = listHex[i];
                int CAT = int.Parse(arrayMsg[0], System.Globalization.NumberStyles.HexNumber);
                int lenght = int.Parse(arrayMsg[1], System.Globalization.NumberStyles.HexNumber) * 256 + int.Parse(arrayMsg[2], System.Globalization.NumberStyles.HexNumber);

                if (CAT == 10)
                {
                    CAT10 cat10 = new CAT10(arrayMsg, 0);

                    cat10.msgNum = numMsgs;
                    cat10.msgCAT10Num = numCAT10Msgs;

                    numMsgs++;
                    numCAT10Msgs++;

                    //Console.WriteLine("CAT" + CAT + ", lenght: " + lenght);

                    listCAT10.Add(cat10);
                }
            }

            //process = "DONE";
            //print(process);

            return listCAT10;
        }

        public List<CAT21> ReadCAT21()
        {
            byte[] fileBytes = File.ReadAllBytes("C:\\Users\\Eduard\\Desktop\\Asterix_PGTA\\AsterixDecoder\\201002-lebl-080001_adsb.ast");
            List<byte[]> listBytes = new List<byte[]>();

            int cont = fileBytes[1] * 256 + fileBytes[2];
            int i = 0;

            while (i < fileBytes.Length)
            {
                byte[] array = new byte[cont];

                for (int j = 0; j < array.Length; j++)
                {
                    array[j] = fileBytes[i];
                    i++;
                }

                listBytes.Add(array);

                if (i + 2 < fileBytes.Length) cont = fileBytes[i + 1] * 256 + fileBytes[i + 2];
            }

            List<string[]> listHex = new List<string[]>();

            for (i = 0; i < listBytes.Count; i++)
            {
                byte[] byteSelect = listBytes[i];
                string[] arrayHex = new string[byteSelect.Length];

                for (int j = 0; j < byteSelect.Length; j++)
                {
                    arrayHex[j] = byteSelect[j].ToString("X");

                    if (arrayHex[j].Length <= 1) arrayHex[j] = string.Concat('0', arrayHex[j]);
                }

                listHex.Add(arrayHex);
            }

            //only for the first message, let's see if it works
            for (i = 0; i < listHex.Count; i++)
            {
                //process = "Loading message " + Convert.ToString(i) + " of " + Convert.ToString(listHex.Count) + " messages...";
                //print(process);

                string[] arrayMsg = listHex[i];
                int CAT = int.Parse(arrayMsg[0], System.Globalization.NumberStyles.HexNumber);
                int lenght = int.Parse(arrayMsg[1], System.Globalization.NumberStyles.HexNumber) * 256 + int.Parse(arrayMsg[2], System.Globalization.NumberStyles.HexNumber);

                if (CAT == 21)
                {
                    CAT21 cat21 = new CAT21(arrayMsg, 0);

                    cat21.msgNum = numMsgs;
                    cat21.msgCAT21Num = numCAT21Msgs;

                    numMsgs++;
                    numCAT21Msgs++;

                    //Console.WriteLine("CAT" + CAT + ", lenght: " + lenght);

                    listCAT21.Add(cat21);
                }
            }

            //process = "DONE";
            //print(process);

            return listCAT21;
        }

        public int Read(string path)
        {
            try
            {
                process = "File is being decoded...";
                bool first = true;
                int first_time_file = 0;

                byte[] fileBytes = File.ReadAllBytes(path);
                List<byte[]> listBytes = new List<byte[]>();

                int cont = fileBytes[1] * 256 + fileBytes[2];
                int i = 0;

                while (i < fileBytes.Length)
                {
                    byte[] array = new byte[cont];

                    for (int j = 0; j < array.Length; j++)
                    {
                        array[j] = fileBytes[i];
                        i++;
                    }

                    listBytes.Add(array);

                    if (i + 2 < fileBytes.Length) cont = fileBytes[i + 1] * 256 + fileBytes[i + 2];
                }

                List<string[]> listHex = new List<string[]>();

                for (i = 0; i < listBytes.Count; i++)
                {
                    byte[] byteSelect = listBytes[i];
                    string[] arrayHex = new string[byteSelect.Length];

                    for (int j = 0; j < byteSelect.Length; j++)
                    {
                        arrayHex[j] = byteSelect[j].ToString("X");

                        if (arrayHex[j].Length <= 1) arrayHex[j] = string.Concat('0', arrayHex[j]);
                    }

                    listHex.Add(arrayHex);
                }

                for (i = 0; i < listHex.Count; i++)
                {
                    process = "Loading message " + Convert.ToString(i) + " of " + Convert.ToString(listHex.Count) + " messages...";

                    string[] arrayMsg = listHex[i];
                    int CAT = int.Parse(arrayMsg[0], System.Globalization.NumberStyles.HexNumber);
                    int lenght = int.Parse(arrayMsg[1], System.Globalization.NumberStyles.HexNumber) * 256 + int.Parse(arrayMsg[2], System.Globalization.NumberStyles.HexNumber);

                    if (CAT == 10)
                    {
                        CAT10 cat10 = new CAT10(arrayMsg, 0);

                        cat10.msgNum = numMsgs;
                        cat10.msgCAT10Num = numCAT10Msgs;

                        numMsgs++;
                        numCAT10Msgs++;

                        if (cat10.SIC == "7") numCAT10SMRMsgs++;
                        else if (cat10.SIC == "107") numCAT10MLATMsgs++;
                        if (first == true)
                        {
                            first_time_file = cat10.TimeOfDaySec;
                            first = false;
                        }
                        listCAT10.Add(cat10);

                        CATALL catall = new CATALL(cat10, 0, first_time_file); 

                        listCATALL.Add(catall);
                        FillTableCAT10(cat10);
                    }
                    else if (CAT == 21)
                    {
                        CAT21 cat21 = new CAT21(arrayMsg, 0);

                        cat21.msgNum = numMsgs;
                        cat21.msgCAT21Num = numCAT10Msgs;

                        numMsgs++;
                        numCAT21Msgs++;
                        if (first == true)
                        {
                            first_time_file = cat21.timeOfDaySeconds;
                            first = false;
                        }
                        listCAT21.Add(cat21);
                        CATALL catall = new CATALL(cat21, 0, first_time_file);
                        listCATALL.Add(catall);
                        FillTableCAT21(cat21);
                    }
                }
                listCATALL = ComputeTimeOfDay(listCATALL);
                ComputeTraj(listCATALL);
                ComputeDirecction(listCATALL);
                ComputeDetectionRatio(listCATALL);
                return 1;
            }
            catch
            {
                return 0;
            }
        }

        public void CreateTableCAT10()
        {
            tableCAT10.Columns.Add("Nº", typeof(int)); 
            tableCAT10.Columns.Add("CAT Nº"); 
            tableCAT10.Columns.Add("Category"); 
            tableCAT10.Columns.Add("SAC"); 
            tableCAT10.Columns.Add("SIC");
            tableCAT10.Columns.Add("Target Identification"); 
            tableCAT10.Columns.Add("Track Number"); 
            tableCAT10.Columns.Add("Target Report"); 
            tableCAT10.Columns.Add("Message Type"); 
            tableCAT10.Columns.Add("Flight Level"); 
            tableCAT10.Columns.Add("Time of Day"); 
            tableCAT10.Columns.Add("Track Status"); 
            tableCAT10.Columns.Add("Position in WGS-84"); 
            tableCAT10.Columns.Add("Position in Cartesian"); 
            tableCAT10.Columns.Add("Position in Polar"); 
            tableCAT10.Columns.Add("Track Velocity in Polar"); 
            tableCAT10.Columns.Add("Track Velocity in Cartesian"); 
            tableCAT10.Columns.Add("Target Size and Orientation"); 
            tableCAT10.Columns.Add("Target Address"); 
            tableCAT10.Columns.Add("System Status");
            tableCAT10.Columns.Add("Vehicle Fleet Identification");
            tableCAT10.Columns.Add("Pre-programmed Message");
            tableCAT10.Columns.Add("Measured Height");
            tableCAT10.Columns.Add("Mode-3A Code");
            tableCAT10.Columns.Add("Mode S MB Data");
            tableCAT10.Columns.Add("Standard Deviation of Position");
            tableCAT10.Columns.Add("Presence");
            tableCAT10.Columns.Add("Amplitude of Primary Plot");
            tableCAT10.Columns.Add("Calculated Acceleration");
        }

        private void CreateTableCAT21()
        { 
            tableCAT21.Columns.Add("Nº", typeof(int));  
            tableCAT21.Columns.Add("CAT Nº");  
            tableCAT21.Columns.Add("Category");  
            tableCAT21.Columns.Add("SAC"); 
            tableCAT21.Columns.Add("SIC"); 
            tableCAT21.Columns.Add("Target Identification"); 
            tableCAT21.Columns.Add("Track Number"); 
            tableCAT21.Columns.Add("Target Report"); 
            tableCAT21.Columns.Add("Service Identification"); 
            tableCAT21.Columns.Add("Time of Report Transmission"); 
            tableCAT21.Columns.Add("Position in WGS-84"); 
            tableCAT21.Columns.Add("Position in WGS-84 HR"); 
            tableCAT21.Columns.Add("Air Speed"); 
            tableCAT21.Columns.Add("True Air Speed"); 
            tableCAT21.Columns.Add("Target Address"); 
            tableCAT21.Columns.Add("Time of Applicability for Position"); 
            tableCAT21.Columns.Add("Time of Message Reception of Position"); 
            tableCAT21.Columns.Add("Time of Applicability for Velocity"); 
            tableCAT21.Columns.Add("Time of Message Reception of Velocity"); 
            tableCAT21.Columns.Add("Geometric Height");  
            tableCAT21.Columns.Add("Quality Indicators");  
            tableCAT21.Columns.Add("MOPS Version");
            tableCAT21.Columns.Add("Mode-3A Code");
            tableCAT21.Columns.Add("Roll Angle");
            tableCAT21.Columns.Add("Flight Level");
            tableCAT21.Columns.Add("Magnetic Heading");
            tableCAT21.Columns.Add("Target Status");
            tableCAT21.Columns.Add("Barometric Vertical Rate");
            tableCAT21.Columns.Add("Geometric Vertical Rate");
            tableCAT21.Columns.Add("Airborne Ground Vector");
            tableCAT21.Columns.Add("Track Angle Rate");    
            tableCAT21.Columns.Add("Emitter Category");
            tableCAT21.Columns.Add("Met Information");
            tableCAT21.Columns.Add("Selected Altitude");
            tableCAT21.Columns.Add("Final State Selected Altitude");
            tableCAT21.Columns.Add("Trajectory Intent");
            tableCAT21.Columns.Add("Service Management");
            tableCAT21.Columns.Add("Aircraft Operational Status");
            tableCAT21.Columns.Add("Surface Capabilities and Characteristics");
            tableCAT21.Columns.Add("Message Amplitude");
            tableCAT21.Columns.Add("Mode S MB Data");
            tableCAT21.Columns.Add("ACAS Resolution Advisory Report");
            tableCAT21.Columns.Add("Receiver ID");
            tableCAT21.Columns.Add("Data Ages");
        }

        public void FillTableCAT10(CAT10 message)
        { 
            var row = tableCAT10.NewRow();

            row["Nº"] = message.msgNum;
            row["CAT Nº"] = message.msgCAT10Num;
            if (message.CAT != null) { row["Category"] = message.CAT; }
            else { row["Category"] = "N/A"; }
            if (message.SAC != null) { row["SAC"] = message.SAC; }
            else { row["SAC"] = "N/A"; }
            if (message.SIC != null) { row["SIC"] = message.SIC; }
            else { row["SIC"] = "N/A"; }
            if (message.TargetId != null)
            {
                if (message.TargetId.Replace(" ","") != "" ) { row["Target Identification"] = message.TargetId; }
                else { row["Target Identification"] = "N/A"; }
            }
            else { row["Target Identification"] = "N/A"; }
            if (message.TYP != null) { row["Target Report"] = "Click for more data"; }
            else { row["Target Report"] = "N/A"; }
            if (message.messageType != null) { row["Message Type"] = message.messageType; }
            else { row["Message Type"] = "N/A"; }
            if (message.FlightLevelInfo != null) { row["Flight Level"] = "Click for more data"; }
            else { row["Flight Level"] = "N/A"; }
            if (message.TrackNum != null) { row["Track Number"] = message.TrackNum; }
            else { row["Track Number"] = "N/A"; }
            if (message.TimeOfDay != null) { row["Time of Day"] = message.TimeOfDay; }
            else { row["Time of Day"] = "N/A"; }
            if (message.CNF != null) { row["Track Status"] = "Click for more data"; }
            else { row["Track Status"] = "N/A"; }
            if (message.LatitudeinWGS84 != null && message.LongitudeinWGS84 != null) { row["Position in WGS-84"] =  message.LatitudeinWGS84 + ", " + message.LongitudeinWGS84; }
            else { row["Position in WGS-84"] = "N/A"; }
            if (message.positioninCartesianCoordinates != null) { row["Position in Cartesian"] = message.positioninCartesianCoordinates; }
            else { row["Position in Cartesian"] = "N/A"; }
            if (message.positioninPolarCoordinates != null) { row["Position in Polar"] = message.positioninPolarCoordinates; }
            else { row["Position in Polar"] = "N/A"; }
            if (message.TrackVelocityPolarCoordinates != null) { row["Track Velocity in Polar"] = message.TrackVelocityPolarCoordinates; }
            else { row["Track Velocity in Polar"] = "N/A"; }
            if (message.TrackVelocityCartesianCoordinates != null) { row["Track Velocity in Cartesian"] = message.TrackVelocityCartesianCoordinates; }
            else { row["Track Velocity in Cartesian"] = "N/A"; }
            if (message.targetSizeOrientation != null) { row["Target Size and Orientation"] = message.targetSizeOrientation; }
            else { row["Target Size and Orientation"] = "N/A"; }
            if (message.TargetAdd != null) { row["Target Address"] = message.TargetAdd; }
            else { row["Target Address"] = "N/A"; }
            if (message.NOGO != null) { row["System Status"] = "Click for more data"; }
            else { row["System Status"] = "N/A"; }
            if (message.VFI != null) { row["Vehicle Fleet Identification"] = message.VFI; }
            else { row["Vehicle Fleet Identification"] = "N/A"; }
            if (message.preProgrammedMessage != null) { row["Pre-programmed Message"] = message.preProgrammedMessage; }
            else { row["Pre-programmed Message"] = "N/A"; }
            if (message.measuredHeight != null) { row["Measured Height"] = message.measuredHeight; }
            else { row["Measured Height"] = "N/A"; }
            if (message.Mode3A != null) { row["Mode-3A Code"] = message.Mode3A; }
            else { row["Mode-3A Code"] = "N/A"; }
            if (message.MBData != null) { row["Mode S MB Data"] = "Click for more data"; }
            else { row["Mode S MB Data"] = "N/A"; }
            if (message.DeviationX != null) { row["Standard Deviation of Position"] = "Click for more data"; }
            else { row["Standard Deviation of Position"] = "N/A"; }
            if (message.REPPresence != 0) { row["Presence"] = "Click for more data"; }
            else { row["Presence"] = "N/A"; }
            if (message.PAM != null) { row["Amplitude of Primary Plot"] = message.PAM; }
            else { row["Amplitude of Primary Plot"] = "N/A"; }
            if (message.calculatedAcceleration != null) { row["Calculated Acceleration"] = message.calculatedAcceleration; }
            else { row["Calculated Acceleration"] = "N/A"; }

            tableCAT10.Rows.Add(row);
        }

        private void FillTableCAT21(CAT21 message)
        {
            var row = tableCAT21.NewRow();

            row["Nº"] = message.msgNum;
            row["CAT Nº"] = message.msgCAT21Num;
            if (message.CAT != null) { row["Category"] = message.CAT; }
            else { row["Category"] = "N/A"; }
            if (message.SAC != null) { row["SAC"] = message.SAC; }
            else { row["SAC"] = "N/A"; }
            if (message.SIC != null) { row["SIC"] = message.SIC; }
            else { row["SIC"] = "N/A"; }
            if (message.TargetId != null) { row["Target Identification"] = message.TargetId; }
            else { row["Target Identification"] = "N/A"; }
            if (message.ATP != null) { row["Target Report"] = "Click for more data"; }
            else { row["Target Report"] = "N/A"; }
            if (message.trackNumber != null) { row["Track Number"] = message.trackNumber; }
            else { row["Track Number"] = "N/A"; }
            if (message.ServiceId != null) { row["Service Identification"] = message.ServiceId; }
            else { row["Service Identification"] = "N/A"; }
            if (message.timeOfApplicabilityForPosition != null) { row["Time of Applicability for Position"] = message.timeOfApplicabilityForPosition; }
            else { row["Time of Applicability for Position"] = "N/A"; }
            if (message.LatitudeinWGS84 != null && message.LongitudeinWGS84 != null) { row["Position in WGS-84"] =  message.LatitudeinWGS84 + ", " + message.LongitudeinWGS84; }
            else { row["Position in WGS-84"] = "N/A"; }
            if (message.LatitudeinWGS84HighResolution != null && message.LongitudeinWGS84HighResolution != null) { row["Position in WGS-84 HR"] =  message.LatitudeinWGS84HighResolution + ", " + message.LatitudeinWGS84HighResolution; }
            else { row["Position in WGS-84 HR"] = "N/A"; }
            if (message.timeOfApplicabilityForVelocity != null) { row["Time of Applicability for Velocity"] = message.timeOfApplicabilityForVelocity; }
            else { row["Time of Applicability for Velocity"] = "N/A"; }
            if (message.airSpeed != null) { row["Air Speed"] = message.airSpeed; }
            else { row["Air Speed"] = "N/A"; }
            if (message.trueAirSpeed != null) { row["True Air Speed"] = message.trueAirSpeed; }
            else { row["True Air Speed"] = "N/A"; }
            if (message.TargetAdd != null) { row["Target Address"] = message.TargetAdd; }
            else { row["Target Address"] = "N/A"; }
            if (message.timeOfMessageReceptionOfPosition != null) { row["Time of Message Reception of Position"] = message.timeOfMessageReceptionOfPosition; }
            else { row["Time of Message Reception of Position"] = "N/A"; }
            if (message.timeOfMessageReceptionOfVelocity != null) { row["Time of Message Reception of Velocity"] = message.timeOfMessageReceptionOfVelocity; }
            else { row["Time of Message Reception of Velocity"] = "N/A"; }
            if (message.geometricHeight != null) { row["Geometric Height"] = message.geometricHeight; }
            else { row["Geometric Height"] = "N/A"; }
            if (message.NUCr_NACv != null) { row["Quality Indicators"] = "Click for more data"; }
            else { row["Quality Indicators"] = "N/A"; }
            if (message.MOPS != null) { row["MOPS Version"] = message.MOPS; }
            else { row["MOPS Version"] = "N/A"; }
            if (message.Mode3A != null) { row["Mode-3A Code"] = message.Mode3A; }
            else { row["Mode-3A Code"] = "N/A"; }
            if (message.rollAngle != null) { row["Roll Angle"] = message.rollAngle; }
            else { row["Roll Angle"] = "N/A"; }
            if (message.fligthLevel != null) { row["Flight Level"] = message.fligthLevel; }
            else { row["Flight Level"] = "N/A"; }
            if (message.magneticHeading != null) { row["Magnetic Heading"] = message.magneticHeading; }
            else { row["Magnetic Heading"] = "N/A"; }
            if (message.ICF != null) { row["Target Status"] = "Click for more data"; }
            else { row["Target Status"] = "N/A"; }
            if (message.barometricVerticalRate != null) { row["Barometric Vertical Rate"] = message.barometricVerticalRate; }
            else { row["Barometric Vertical Rate"] = "N/A"; }
            if (message.geometricVerticalRate != null) { row["Geometric Vertical Rate"] = message.geometricVerticalRate; }
            else { row["Geometric Vertical Rate"] = "N/A"; }
            if (message.GroundVector != null) { row["Airborne Ground Vector"] = message.GroundVector; }
            else { row["Airborne Ground Vector"] = "N/A"; }
            if (message.trackAngleRate != null) { row["Track Angle Rate"] = message.trackAngleRate; }
            else { row["Track Angle Rate"] = "N/A"; }
            if (message.timeOfASTERIXReportTransmission != null) { row["Time of Report Transmission"] = message.timeOfASTERIXReportTransmission; }
            else { row["Time of Report Transmission"] = "N/A"; }
            if (message.ECAT != null) { row["Emitter Category"] = message.ECAT; }
            else { row["Emitter Category"] = "N/A"; }
            if (message.METInfo != 0) { row["Met Information"] = "Click for more data"; }
            else { row["Met Information"] = "N/A"; }
            if (message.selectedAltitude != null) { row["Selected Altitude"] = "Click for more data"; }
            else { row["Selected Altitude"] = "N/A"; }
            if (message.MV != null) { row["Final State Selected Altitude"] = "Click for more data"; }
            else { row["Final State Selected Altitude"] = "N/A"; }
            if (message.TRAJInfo != 0) { row["Trajectory Intent"] = "Click for more data"; }
            else { row["Trajectory Intent"] = "N/A"; }
            if (message.RP != null) { row["Service Management"] = message.RP; }
            else { row["Service Management"] = "N/A"; }
            if (message.RA != null) { row["Aircraft Operational Status"] = "Click for more data"; }
            else { row["Aircraft Operational Status"] = "N/A"; }
            if (message.POA != null)
            {
                if (message.POA.Length > 25) { row["Surface Capabilities and Characteristics"] = "Click for more data"; }
                else { row["Surface Capabilities and Characteristics"] = "N/A"; }
            }
            else { row["Surface Capabilities and Characteristics"] = "N/A"; }
            if (message.messageAmplitude != null) { row["Message Amplitude"] = message.messageAmplitude; }
            else { row["Message Amplitude"] = "N/A"; }
            if (message.MBData != null) { row["Mode S MB Data"] = "Click for more data"; }
            else { row["Mode S MB Data"] = "N/A"; }
            if (message.TYP != null) { row["ACAS Resolution Advisory Report"] = "Click for more data"; }
            else { row["ACAS Resolution Advisory Report"] = "N/A"; }
            if (message.receiverID != null) { row["Receiver ID"] = message.receiverID; }
            else { row["Receiver ID"] = "N/A"; }
            if (message.DAInfo != 0) { row["Data Ages"] = "Click for more data"; }
            else { row["Data Ages"] = "N/A"; }
            
            tableCAT21.Rows.Add(row);
        }

        private void ComputeTraj(List<CATALL> list)
        {
            SMRTraj = new List<Trajectories>();
            MLATTraj = new List<Trajectories>();
            ADSBTraj = new List<Trajectories>();

            int i = 0;
            foreach (CATALL msg in list)
            {
                process = "Computing trajectory for message " + i + " of " + Convert.ToString(list.Count) + " messages...";
                i++;

                if (msg.latitudeInWGS84 != -200 && msg.longitudeInWGS84 != -200)
                {
                    if (msg.detectionMode == "SMR")
                    {
                        if (msg.targetIdentification != null)
                        {
                            if (SMRTraj.Exists(x => x.targetIdentification == msg.targetIdentification)) { SMRTraj.Find(x => x.targetIdentification == msg.targetIdentification).AddTimePoint(msg.latitudeInWGS84, msg.longitudeInWGS84, msg.timeOfDay); }
                            else
                            {
                                Trajectories traj = new Trajectories(msg.targetIdentification, msg.timeOfDay, msg.latitudeInWGS84, msg.longitudeInWGS84, msg.type, msg.targetAddress, msg.detectionMode, msg.CAT, msg.SAC, msg.SIC, msg.trackNumber);
                                SMRTraj.Add(traj);

                            }
                        }
                        else if (msg.targetAddress != null)
                        {
                            if (SMRTraj.Exists(x => x.targetAdd == msg.targetAddress)) { SMRTraj.Find(x => x.targetAdd == msg.targetAddress).AddTimePoint(msg.latitudeInWGS84, msg.longitudeInWGS84, msg.timeOfDay); }
                            else
                            {
                                Trajectories traj = new Trajectories(msg.targetIdentification, msg.timeOfDay, msg.latitudeInWGS84, msg.longitudeInWGS84, msg.type, msg.targetAddress, msg.detectionMode, msg.CAT, msg.SAC, msg.SIC, msg.trackNumber);
                                SMRTraj.Add(traj);

                            }
                        }
                        else if (msg.trackNumber != null)
                        {
                            if (SMRTraj.Exists(x => x.trackNum == msg.trackNumber)) { SMRTraj.Find(x => x.trackNum == msg.trackNumber).AddTimePoint(msg.latitudeInWGS84, msg.longitudeInWGS84, msg.timeOfDay); }
                            else
                            {
                                Trajectories traj = new Trajectories(msg.targetIdentification, msg.timeOfDay, msg.latitudeInWGS84, msg.longitudeInWGS84, msg.type, msg.targetAddress, msg.detectionMode, msg.CAT, msg.SAC, msg.SIC, msg.trackNumber);
                                SMRTraj.Add(traj);
                            }
                        }
                    }
                    else if (msg.detectionMode == "MLAT")
                    {
                        if (msg.targetIdentification != null)
                        {
                            if (MLATTraj.Exists(x => x.targetIdentification == msg.targetIdentification)) { MLATTraj.Find(x => x.targetIdentification == msg.targetIdentification).AddTimePoint(msg.latitudeInWGS84, msg.longitudeInWGS84, msg.timeOfDay); }
                            else
                            {
                                Trajectories traj = new Trajectories(msg.targetIdentification, msg.timeOfDay, msg.latitudeInWGS84, msg.longitudeInWGS84, msg.type, msg.targetAddress, msg.detectionMode, msg.CAT, msg.SAC, msg.SIC, msg.trackNumber);
                                MLATTraj.Add(traj);

                            }
                        }
                        else if (msg.targetAddress != null)
                        {
                            if (MLATTraj.Exists(x => x.targetAdd == msg.targetAddress)) { MLATTraj.Find(x => x.targetAdd == msg.targetAddress).AddTimePoint(msg.latitudeInWGS84, msg.longitudeInWGS84, msg.timeOfDay); }
                            else
                            {
                                Trajectories traj = new Trajectories(msg.targetIdentification, msg.timeOfDay, msg.latitudeInWGS84, msg.longitudeInWGS84, msg.type, msg.targetAddress, msg.detectionMode, msg.CAT, msg.SAC, msg.SIC, msg.trackNumber);
                                MLATTraj.Add(traj);

                            }
                        }
                        else if (msg.trackNumber != null)
                        {
                            if (MLATTraj.Exists(x => x.trackNum == msg.trackNumber)) { MLATTraj.Find(x => x.trackNum == msg.trackNumber).AddTimePoint(msg.latitudeInWGS84, msg.longitudeInWGS84, msg.timeOfDay); }
                            else
                            {
                                Trajectories traj = new Trajectories(msg.targetIdentification, msg.timeOfDay, msg.latitudeInWGS84, msg.longitudeInWGS84, msg.type, msg.targetAddress, msg.detectionMode, msg.CAT, msg.SAC, msg.SIC, msg.trackNumber);
                                MLATTraj.Add(traj);
                            }
                        }
                    }
                    else if (msg.detectionMode == "ADSB")
                    {
                        if (msg.targetIdentification != null)
                        {
                            if (ADSBTraj.Exists(x => x.targetIdentification == msg.targetIdentification)) { ADSBTraj.Find(x => x.targetIdentification == msg.targetIdentification).AddTimePoint(msg.latitudeInWGS84, msg.longitudeInWGS84, msg.timeOfDay); }
                            else
                            {
                                Trajectories traj = new Trajectories(msg.targetIdentification, msg.timeOfDay, msg.latitudeInWGS84, msg.longitudeInWGS84, msg.type, msg.targetAddress, msg.detectionMode, msg.CAT, msg.SAC, msg.SIC, msg.trackNumber);
                                ADSBTraj.Add(traj);
                            }
                        }
                        else if (msg.targetAddress != null)
                        {
                            if (ADSBTraj.Exists(x => x.targetAdd == msg.targetAddress)) { ADSBTraj.Find(x => x.targetAdd == msg.targetAddress).AddTimePoint(msg.latitudeInWGS84, msg.longitudeInWGS84, msg.timeOfDay); }
                            else
                            {
                                Trajectories traj = new Trajectories(msg.targetIdentification, msg.timeOfDay, msg.latitudeInWGS84, msg.longitudeInWGS84, msg.type, msg.targetAddress, msg.detectionMode, msg.CAT, msg.SAC, msg.SIC, msg.trackNumber);
                                ADSBTraj.Add(traj);
                            }
                        }
                        else if (msg.trackNumber != null)
                        {
                            if (ADSBTraj.Exists(x => x.trackNum == msg.trackNumber)) { ADSBTraj.Find(x => x.trackNum == msg.trackNumber).AddTimePoint(msg.latitudeInWGS84, msg.longitudeInWGS84, msg.timeOfDay); }
                            else
                            {
                                Trajectories traj = new Trajectories(msg.targetIdentification, msg.timeOfDay, msg.latitudeInWGS84, msg.longitudeInWGS84, msg.type, msg.targetAddress, msg.detectionMode, msg.CAT, msg.SAC, msg.SIC, msg.trackNumber);
                                ADSBTraj.Add(traj);
                            }
                        }
                    }
                }
            }
        }
        private void ClearTraj()
        {
            SMRTraj = new List<Trajectories>();
            MLATTraj = new List<Trajectories>();
            ADSBTraj = new List<Trajectories>();
        }

        public void print(string texto)
        {
            Console.WriteLine(texto);
        }

        private void ComputeDirectionFromindex(Trajectories t, int index, CATALL message)
        {
            if (t.CountTimepoint() > 2)
            {
                if ((index + 2) < t.CountTimepoint())
                {

                    PointWithTime p = t.listTimePoints[index + 1];
                    if (t.listTimePoints[index + 1].time == message.timeOfDay)
                    {
                        bool dif = false;
                        int i = 2;
                        while (dif == false)
                        {
                            p = t.listTimePoints[index + i];
                            if (p.time != message.timeOfDay) { dif = true; }
                            if ((index + i + 1) >= t.CountTimepoint()) { dif = true; }
                            i++;
                        }
                    }
                    double X = p.point.Lng - message.longitudeInWGS84;
                    double Y = p.point.Lat - message.latitudeInWGS84;
                    int direction = 100;
                    double dir = 0;
                    dir = (Math.Atan2(Y, X) * (180 / Math.PI));
                    try
                    {
                        direction = Convert.ToInt32(dir);
                    }
                    catch { direction = 0; }
                    if (message.type == "car")
                    {
                        direction = -(direction - 180);
                    }
                    else if (message.type == "plane")
                    {
                        direction = -(direction - 45);
                    }
                    message.direction = direction;
                }
                else if ((index + 1) < t.CountTimepoint())
                {
                    double X;
                    double Y;
                    int direction;
                    double dir;
                    PointWithTime p = t.listTimePoints[index + 1];
                    if (t.listTimePoints[index + 1].time != message.timeOfDay || index == 0)
                    {
                        p = t.listTimePoints[index + 1];
                        X = p.point.Lng - message.longitudeInWGS84;
                        Y = p.point.Lat - message.latitudeInWGS84;

                        dir = (Math.Atan2(Y, X) * (180 / Math.PI));
                        try
                        {
                            direction = Convert.ToInt32(dir);
                        }
                        catch { direction = 0; }
                    }

                    else
                    {
                        p = t.listTimePoints[index - 1];
                        X = message.longitudeInWGS84 - p.point.Lng;
                        Y = message.latitudeInWGS84 - p.point.Lat;
                        dir = (Math.Atan2(Y, X) * (180 / Math.PI));
                        try
                        {
                            direction = Convert.ToInt32(dir);
                        }
                        catch { direction = 0; }

                    }

                    if (message.type == "car")
                    {
                        direction = -(direction - 180);
                    }
                    else if (message.type == "plane")
                    {
                        direction = -(direction - 45);
                    }
                    message.direction = direction;

                }
                else
                {
                    try
                    {
                        PointWithTime p = t.listTimePoints[index - 1];
                        double X = message.longitudeInWGS84 - p.point.Lng;
                        double Y = message.latitudeInWGS84 - p.point.Lat;
                        int direction = 100;
                        double dir = (Math.Atan2(Y, X) * (180 / Math.PI));
                        try
                        {
                            direction = Convert.ToInt32(dir);
                        }
                        catch { direction = 0; }
                        if (message.type == "car")
                        {
                            direction = -(direction - 180);
                        }
                        else if (message.type == "plane")
                        {
                            direction = -(direction - 45);
                        }
                        message.direction = direction;
                    }
                    catch { }
                }
            }
        }

        private void FindDirection(List<Trajectories> listTraj, CATALL message)
        {
            if (message.targetIdentification != null && message.targetIdentification.Count() > 1) // && listtraj.Exists(x => x.Target_Identification == message.Target_Identification))
            {
                Trajectories t = listTraj.Find(x => x.targetIdentification == message.targetIdentification);
                int index = t.listTimePoints.FindIndex(x => x.time == message.timeOfDay);

                ComputeDirectionFromindex(t, index, message);
            }

            else if (message.targetAddress != null)// && listtraj.Exists(x => x.Target_Address == message.Target_Address))
            {
                Trajectories t = listTraj.Find(x => x.targetAdd == message.targetAddress);
                int index = t.listTimePoints.FindIndex(x => x.time == message.timeOfDay);
                ComputeDirectionFromindex(t, index, message);
            }

            else if (message.trackNumber != null)// && listtraj.Exists(x => x.Track_number == message.Track_number))
            {
                Trajectories t = listTraj.Find(x => x.trackNum == message.trackNumber);
                int index = t.listTimePoints.FindIndex(x => x.time == message.timeOfDay);
                ComputeDirectionFromindex(t, index, message);
            }
        }

        private void ComputeDirecction(List<CATALL> listCATALL)
        {
            process = "Computing trajectories...";

            process = "Applying trajectories...";

            int i = 0;

            /*Once trajectories are created, we will walk trhought the CATALL list and search the direction of each message*/
            foreach (CATALL message in listCATALL)
            {
                i++;
                process = "Applying trajectory for message " + i + " of " + Convert.ToString(listCATALL.Count) + " messages...";

                if (message.latitudeInWGS84 != -200 && message.longitudeInWGS84 != -200)
                {
                    if (message.detectionMode == "SMR")
                    {
                        FindDirection(SMRTraj, message);
                    }
                    if (message.detectionMode == "MLAT")
                    {
                        FindDirection(MLATTraj, message);
                    }
                    if (message.detectionMode == "ADSB")
                    {
                        FindDirection(ADSBTraj, message);
                    }
                }
            }
        }

        private void ComputeDetectionRatio(List<CATALL> listCATALL)
        {
            process = "Computing radars detection ratio...";
            double ADSBratio = 0;
            int ADSBRatio;
            int ADSBCount = 0;
            if (ADSBTraj.Count() > 0)
            {
                foreach (Trajectories t in ADSBTraj)
                {
                    if (t.CountTimepoint() > 3)
                    {
                        double r = (t.listTimePoints[t.listTimePoints.Count() - 1].time - t.listTimePoints[0].time) / (t.listTimePoints.Count() - 1);
                        if (r < 5)
                        {
                            ADSBCount++;
                            ADSBratio += r;
                        }
                    }
                }
                ADSBRatio = Convert.ToInt32(ADSBratio / ADSBCount);
                if (ADSBRatio < 1) { ADSBRatio = 1; }
            }
            else
            {
                ADSBRatio = 1;
            }
            process = "Applying radars detection ratio...";
            foreach (CATALL message in listCATALL)
            {
                if (message.CAT == "21 v. 2.1")
                {
                    message.refreshratio = ADSBRatio;
                }
                else
                {
                    message.refreshratio = 1;
                }
            }
        }

        private List<CATALL> ComputeTimeOfDay(List<CATALL> listCATALL)
        {
            List<CATALL> List = new List<CATALL>();
            List = listCATALL.OrderBy(CATAll => CATAll.listTimeOfDay).ToList();
            int firsttime = List[0].listTimeOfDay;
            int lasttime = List[List.Count - 1].listTimeOfDay;
            double da = (lasttime - firsttime) / 86400;
            int days = Convert.ToInt32(Math.Truncate(da)) + 1;
            if (List[0].listTimeOfDay < 0)
            {
                double fir = (firsttime / 86400);
                int firstday = Convert.ToInt32(Math.Truncate(fir)) - 1;
                foreach (CATALL mess in List)
                {
                    mess.timeOfDay = mess.listTimeOfDay + (-firstday * 86400);
                }
            }
            else
            {
                foreach (CATALL mess in List)
                {
                    mess.timeOfDay = mess.listTimeOfDay;
                }
            }
            return List;
        }
    }
}
