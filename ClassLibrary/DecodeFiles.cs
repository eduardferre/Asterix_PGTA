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
        public int numCAT21Msgs = 0;
       
        public string process;

        List<CAT10> listCAT10 = new List<CAT10>();
        List<CAT21> listCAT21 = new List<CAT21>();

        DataTable tableCAT10 = new DataTable();
        DataTable tableCAT21 = new DataTable();

        public List<string> nameFiles = new List<string>();


        public List<CAT10> GetListCAT10() { return listCAT10; }
        public List<CAT21> GetListCAT21() { return listCAT21; }
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
            numCAT21Msgs = 0;
            nameFiles.Clear();
            listCAT10.Clear();
            listCAT21.Clear();
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

            //only for the first message, let's see if it works
            for (i = 0; i < listHex.Count; i++)
            {
                //process = "Loading message " + Convert.ToString(i) + " of " + Convert.ToString(listHex.Count) + " messages...";
                //print(process);

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
                print(process);

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
                    print(process);

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

                        listCAT10.Add(cat10);
                        FillTableCAT10(cat10);
                    }
                    else if (CAT == 21)
                    {
                        CAT21 cat21 = new CAT21(arrayMsg, 0);

                        cat21.msgNum = numMsgs;
                        cat21.msgCAT21Num = numCAT10Msgs;

                        numMsgs++;
                        numCAT21Msgs++;

                        listCAT21.Add(cat21);
                        FillTableCAT21(cat21);
                    }
                }


                return 1;
            }
            catch
            {
                return 0;
            }
        }

        public void CreateTableCAT10()
        {
            tableCAT10.Columns.Add("Number",typeof(int)); 
            tableCAT10.Columns.Add("CAT number"); 
            tableCAT10.Columns.Add("Category"); 
            tableCAT10.Columns.Add("SAC"); 
            tableCAT10.Columns.Add("SIC"); 
            tableCAT10.Columns.Add("Target Identification"); 
            tableCAT10.Columns.Add("Track Number"); 
            tableCAT10.Columns.Add("Target Report Descriptor"); 
            tableCAT10.Columns.Add("Message Type"); 
            tableCAT10.Columns.Add("Flight Level"); 
            tableCAT10.Columns.Add("Time of Day"); 
            tableCAT10.Columns.Add("Track Status"); 
            tableCAT10.Columns.Add("Position in WGS-84 Co-ordinates"); 
            tableCAT10.Columns.Add("Position in Cartesian Co-ordinates"); 
            tableCAT10.Columns.Add("Position in Polar Co-ordinates"); 
            tableCAT10.Columns.Add("Track Velocity in Polar Coordinates"); 
            tableCAT10.Columns.Add("Track Velocity in Cartesian Coordinates"); 
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
            tableCAT21.Columns.Add("Number", typeof(int));  
            tableCAT21.Columns.Add("CAT number");  
            tableCAT21.Columns.Add("Category");  
            tableCAT21.Columns.Add("SAC"); 
            tableCAT21.Columns.Add("SIC"); 
            tableCAT21.Columns.Add("Target Identification"); 
            tableCAT21.Columns.Add("Track Number"); 
            tableCAT21.Columns.Add("Target Report Descriptor"); 
            tableCAT21.Columns.Add("Service Identification"); 
            tableCAT21.Columns.Add("Time of Report Transmission"); 
            tableCAT21.Columns.Add("Position in WGS-84 co-ordinates"); 
            tableCAT21.Columns.Add("Position in WGS-84 co-ordinates high res"); 
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

            row["Number"] = message.msgNum;
            row["CAT number"] = message.msgCAT10Num;
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
            if (message.TYP != null) { row["Target Report Descriptor"] = "Click for more data"; }
            else { row["Target Report Descriptor"] = "N/A"; }
            if (message.messageType != null) { row["Message Type"] = message.messageType; }
            else { row["Message Type"] = "N/A"; }
            if (message.FlightLevelInfo != null) { row["Flight Level"] = message.FlightLevelInfo; }
            else { row["Flight Level"] = "N/A"; }
            if (message.TrackNum != null) { row["Track Number"] = message.TrackNum; }
            else { row["Track Number"] = "N/A"; }
            if (message.TimeOfDay != null) { row["Time of Day"] = message.TimeOfDay; }
            else { row["Time of Day"] = "N/A"; }
            if (message.CNF != null) { row["Track Status"] = "Click for more data"; }
            else { row["Track Status"] = "N/A"; }
            if (message.LatitudeinWGS84 != null && message.LongitudeinWGS84 != null) { row["Position in WGS-84 Co-ordinates"] =  message.LatitudeinWGS84 + ", " + message.LongitudeinWGS84; }
            else { row["Position in WGS-84 Co-ordinates"] = "N/A"; }
            if (message.positioninCartesianCoordinates != null) { row["Position in Cartesian Co-ordinates"] = message.positioninCartesianCoordinates; }
            else { row["Position in Cartesian Co-ordinates"] = "N/A"; }
            if (message.positioninPolarCoordinates != null) { row["Position in Polar Co-ordinates"] = message.positioninPolarCoordinates; }
            else { row["Position in Polar Co-ordinates"] = "N/A"; }
            if (message.TrackVelocityPolarCoordinates != null) { row["Track Velocity in Polar Coordinates"] = message.TrackVelocityPolarCoordinates; }
            else { row["Track Velocity in Polar Coordinates"] = "N/A"; }
            if (message.TrackVelocityCartesianCoordinates != null) { row["Track Velocity in Cartesian Coordinates"] = message.TrackVelocityCartesianCoordinates; }
            else { row["Track Velocity in Cartesian Coordinates"] = "N/A"; }
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

            row["Number"] = message.msgNum;
            row["CAT number"] = message.msgCAT21Num;
            if (message.CAT != null) { row["Category"] = message.CAT; }
            else { row["Category"] = "No Data"; }
            if (message.SAC != null) { row["SAC"] = message.SAC; }
            else { row["SAC"] = "No Data"; }
            if (message.SIC != null) { row["SIC"] = message.SIC; }
            else { row["SIC"] = "No Data"; }
            if (message.TargetId != null) { row["Target\nIdentification"] = message.TargetId; }
            else { row["Target\nIdentification"] = "No Data"; }
            if (message.ATP != null) { row["Target\nReport\nDescriptor"] = "Click to expand"; }
            else { row["Target\nReport\nDescriptor"] = "No Data"; }
            if (message.trackNumber != null) { row["Track\nNumber"] = message.trackNumber; }
            else { row["Track\nNumber"] = "No Data"; }
            if (message.ServiceId != null) { row["Service\nIdentification"] = message.ServiceId; }
            else { row["Service\nIdentification"] = "No Data"; }
            if (message.timeOfApplicabilityForPosition != null) { row["Time of\nApplicability\nfor Position"] = message.timeOfApplicabilityForPosition; }
            else { row["Time of\nApplicability\nfor Position"] = "No Data"; }
            if (message.LatitudeinWGS84 != null && message.LongitudeinWGS84 != null) { row["Position in WGS-84 co-ordinates"] =  message.LatitudeinWGS84 + ", " + message.LongitudeinWGS84; }
            else { row["Position in WGS-84 co-ordinates"] = "No Data"; }
            if (message.LatitudeinWGS84HighResolution != null && message.LongitudeinWGS84HighResolution != null) { row["Position in WGS-84 co-ordinates high res"] =  message.LatitudeinWGS84HighResolution + ", " + message.LatitudeinWGS84HighResolution; }
            else { row["Position in WGS-84 co-ordinates high res"] = "No Data"; }
            if (message.timeOfApplicabilityForVelocity != null) { row["Time of\nApplicability\nfor Velocity"] = message.timeOfApplicabilityForVelocity; }
            else { row["Time of\nApplicability\nfor Velocity"] = "No Data"; }
            if (message.airSpeed != null) { row["Air\nSpeed"] = message.airSpeed; }
            else { row["Air\nSpeed"] = "No Data"; }
            if (message.trueAirSpeed != null) { row["True Air\nSpeed"] = message.trueAirSpeed; }
            else { row["True Air\nSpeed"] = "No Data"; }
            if (message.TargetAdd != null) { row["Target Address"] = message.TargetAdd; }
            else { row["Target Address"] = "No Data"; }
            if (message.timeOfMessageReceptionOfPosition != null) { row["Time of\nMessage\nReception\nof Position"] = message.timeOfMessageReceptionOfPosition; }
            else { row["Time of\nMessage\nReception\nof Position"] = "No Data"; }
            if (message.timeOfMessageReceptionOfVelocity != null) { row["Time of\nMessage\nReception\nof Velocity"] = message.timeOfMessageReceptionOfVelocity; }
            else { row["Time of\nMessage\nReception\nof Velocity"] = "No Data"; }
            if (message.geometricHeight != null) { row["Geometric\nHeight"] = message.geometricHeight; }
            else { row["Geometric\nHeight"] = "No Data"; }
            if (message.NUCr_NACv != null) { row["Quality\nIndicators"] = "Click to expand"; }
            else { row["Quality\nIndicators"] = "No Data"; }
            if (message.MOPS != null) { row["MOPS Version"] = message.MOPS; }
            else { row["MOPS Version"] = "No Data"; }
            if (message.Mode3A != null) { row["Mode-3A\nCode"] = message.Mode3A; }
            else { row["Mode-3A\nCode"] = "No Data"; }
            if (message.rollAngle != null) { row["Roll\nAngle"] = message.rollAngle; }
            else { row["Roll\nAngle"] = "No Data"; }
            if (message.fligthLevel != null) { row["Flight\nLevel"] = message.fligthLevel; }
            else { row["Flight\nLevel"] = "No Data"; }
            if (message.magneticHeading != null) { row["Magnetic\nHeading"] = message.magneticHeading; }
            else { row["Magnetic\nHeading"] = "No Data"; }
            if (message.ICF != null) { row["Target\nStatus"] = "Click to expand"; }
            else { row["Target\nStatus"] = "No Data"; }
            if (message.barometricVerticalRate != null) { row["Barometric\nVertical Rate"] = message.barometricVerticalRate; }
            else { row["Barometric\nVertical Rate"] = "No Data"; }
            if (message.geometricVerticalRate != null) { row["Geometric\nVertical Rate"] = message.geometricVerticalRate; }
            else { row["Geometric\nVertical Rate"] = "No Data"; }
            if (message.GroundVector != null) { row["Airborne Ground Vector"] = message.GroundVector; }
            else { row["Airborne Ground Vector"] = "No Data"; }
            if (message.trackAngleRate != null) { row["Track\nAngle\nRate"] = message.trackAngleRate; }
            else { row["Track\nAngle\nRate"] = "No Data"; }
            if (message.timeOfASTERIXReportTransmission != null) { row["Time of\nReport\nTransmission"] = message.timeOfASTERIXReportTransmission; }
            else { row["Time of\nReport\nTransmission"] = "No Data"; }
            if (message.ECAT != null) { row["Emitter Category"] = message.ECAT; }
            else { row["Emitter Category"] = "No Data"; }
            if (message.METInfo != 0) { row["Met\nInformation"] = "Click to expand"; }
            else { row["Met\nInformation"] = "No Data"; }
            if (message.selectedAltitude != null) { row["Selected Altitude"] = message.selectedAltitude; }
            else { row["Selected Altitude"] = "No Data"; }
            if (message.MV != null) { row["Final\nState\nSelected\nAltitude"] = "Click to expand"; }
            else { row["Final\nState\nSelected\nAltitude"] = "No Data"; }
            if (message.TRAJInfo != 0) { row["Trajectory\nIntent"] = "Click to expand"; }
            else { row["Trajectory\nIntent"] = "No Data"; }
            if (message.RP != null) { row["Service\nManagement"] = message.RP; }
            else { row["Service\nManagement"] = "No Data"; }
            if (message.RA != null) { row["Aircraft\nOperational\nStatus"] = "Click to expand"; }
            else { row["Aircraft\nOperational\nStatus"] = "No Data"; }
            if (message.POA != null)
            {
                if (message.POA.Length > 25) { row["Surface\nCapabilities\nand\nCharacteristics"] = "Click to expand"; }
                else { row["Surface\nCapabilities\nand\nCharacteristics"] = "No Data"; }
            }
            else { row["Surface\nCapabilities\nand\nCharacteristics"] = "No Data"; }
            if (message.messageAmplitude != null) { row["Message\nAmplitude"] = message.messageAmplitude; }
            else { row["Message\nAmplitude"] = "No Data"; }
            if (message.MBData != null) { row["Mode S MB Data"] = "Click to expand"; }
            else { row["Mode S MB Data"] = "No Data"; }
            if (message.TYP != null) { row["ACAS\nResolution\nAdvisory\nReport"] = "Click to expand"; }
            else { row["ACAS\nResolution\nAdvisory\nReport"] = "No Data"; }
            if (message.receiverID != null) { row["Receiver\nID"] = message.receiverID; }
            else { row["Receiver\nID"] = "No Data"; }
            if (message.DAInfo != 0) { row["Data Ages"] = "Click to expand"; }
            else { row["Data Ages"] = "No Data"; }
            
            tableCAT21.Rows.Add(row);
        }

        public void print(string texto)
        {
            Console.WriteLine(texto);
        }
    }
}
