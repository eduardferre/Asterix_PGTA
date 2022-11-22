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

        public void DecodeFiles() 
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
            tableCAT10.Columns.Add("Standard\ Deviation of Position");
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
            var row = tablaCAT10.NewRow();
            row["Number"] = message.num;
            row["CAT number"] = message.cat10num;
            if (message.CAT != null) { row["Category"] = message.CAT; }
            else { row["Category"] = "N/A"; }
            if (message.SAC != null) { row["SAC"] = message.SAC; }
            else { row["SAC"] = "N/A"; }
            if (message.SIC != null) { row["SIC"] = message.SIC; }
            else { row["SIC"] = "N/A"; }
            if (message.TAR != null)
            {
                if (message.TAR.Replace(" ","")!="" ) { row["Target Identification"] = message.TAR; }
                else { row["Target Identification"] = "N/A"; }
            }
            else { row["Target Identification"] = "N/A"; }
            if (message.TYP != null) { row["Target Report Descriptor"] = "Click for more data"; }
            else { row["Target Report Descriptor"] = "N/A"; }
            if (message.MESSAGE_TYPE != null) { row["Message Type"] = message.MESSAGE_TYPE; }
            else { row["Message Type"] = "N/A"; }
            if (message.Flight_Level != null) { row["Flight Level"] = message.Flight_Level; }
            else { row["Flight Level"] = "N/A"; }
            if (message.Track_Number != null) { row["Track Number"] = message.Track_Number; }
            else { row["Track Number"] = "N/A"; }
            if (message.Time_Of_Day != null) { row["Time of Day"] = message.Time_Of_Day; }
            else { row["Time of Day"] = "N/A"; }
            if (message.CNF != null) { row["Track Status"] = "Click for more data"; }
            else { row["Track Status"] = "N/A"; }
            if (message.Latitude_in_WGS_84 != null && message.Longitude_in_WGS_84 != null) { row["Position in WGS-84 Co-ordinates"] =  message.Latitude_in_WGS_84 + ", " + message.Longitude_in_WGS_84; }
            else { row["Position in WGS-84 Co-ordinates"] = "N/A"; }
            if (message.Position_Cartesian_Coordinates != null) { row["Position in Cartesian Co-ordinates"] = message.Position_Cartesian_Coordinates; }
            else { row["Position in Cartesian Co-ordinates"] = "N/A"; }
            if (message.Position_In_Polar != null) { row["Position in Polar Co-ordinates"] = message.Position_In_Polar; }
            else { row["Position in Polar Co-ordinates"] = "N/A"; }
            if (message.Track_Velocity_Polar_Coordinates != null) { row["Track Velocity in Polar Coordinates"] = message.Track_Velocity_Polar_Coordinates; }
            else { row["Track Velocity in Polar Coordinates"] = "N/A"; }
            if (message.Track_Velocity_in_Cartesian_Coordinates != null) { row["Track Velocity in Cartesian Coordinates"] = message.Track_Velocity_in_Cartesian_Coordinates; }
            else { row["Track Velocity in Cartesian Coordinates"] = "N/A"; }
            if (message.Target_size_and_orientation != null) { row["Target Size and Orientation"] = message.Target_size_and_orientation; }
            else { row["Target Size and Orientation"] = "N/A"; }
            if (message.Target_Address != null) { row["Target Address"] = message.Target_Address; }
            else { row["Target Address"] = "N/A"; }
            if (message.NOGO != null) { row["System Status"] = "Click for more data"; }
            else { row["System Status"] = "N/A"; }
            if (message.VFI != null) { row["Vehicle Fleet Identification"] = message.VFI; }
            else { row["Vehicle Fleet Identification"] = "N/A"; }
            if (message.Pre_programmed_message != null) { row["Pre-programmed Message"] = message.Pre_programmed_message; }
            else { row["Pre-programmed Message"] = "N/A"; }
            if (message.Measured_Height != null) { row["Measured Height"] = message.Measured_Height; }
            else { row["Measured Height"] = "N/A"; }
            if (message.Mode_3A != null) { row["Mode-3A Code"] = message.Mode_3A; }
            else { row["Mode-3A Code"] = "N/A"; }
            if (message.MB_Data != null) { row["Mode S MB Data"] = "Click for more data"; }
            else { row["Mode S MB Data"] = "N/A"; }
            if (message.Deviation_X != null) { row["Standard Deviation of Position"] = "Click for more data"; }
            else { row["Standard Deviation of Position"] = "N/A"; }
            if (message.REP_Presence != 0) { row["Presence"] = "Click for more data"; }
            else { row["Presence"] = "N/A"; }
            if (message.PAM != null) { row["Amplitude of Primary Plot"] = message.PAM; }
            else { row["Amplitude of Primary Plot"] = "N/A"; }
            if (message.Calculated_Acceleration != null) { row["Calculated Acceleration"] = message.Calculated_Acceleration; }
            else { row["Calculated Acceleration"] = "N/A"; }
            tablaCAT10.Rows.Add(row);
        }

        private void FillTableCAT21(CAT21vs21 message)
        {
            var row = tablaCAT21v21.NewRow();
            row["Number"] = Message.num;
            row["CAT number"] = Message.cat21v21num;
            if (Message.CAT != null) { row["Category"] = Message.CAT; }
            else { row["Category"] = "No Data"; }
            if (Message.SAC != null) { row["SAC"] = Message.SAC; }
            else { row["SAC"] = "No Data"; }
            if (Message.SIC != null) { row["SIC"] = Message.SIC; }
            else { row["SIC"] = "No Data"; }
            if (Message.Target_Identification != null) { row["Target\nIdentification"] = Message.Target_Identification; }
            else { row["Target\nIdentification"] = "No Data"; }
            if (Message.ATP != null) { row["Target\nReport\nDescriptor"] = "Click to expand"; }
            else { row["Target\nReport\nDescriptor"] = "No Data"; }
            if (Message.Track_Number != null) { row["Track\nNumber"] = Message.Track_Number; }
            else { row["Track\nNumber"] = "No Data"; }
            if (Message.Service_Identification != null) { row["Service\nIdentification"] = Message.Service_Identification; }
            else { row["Service\nIdentification"] = "No Data"; }
            if (Message.Time_of_Applicability_Position != null) { row["Time of\nApplicability\nfor Position"] = Message.Time_of_Applicability_Position; }
            else { row["Time of\nApplicability\nfor Position"] = "No Data"; }
            if (Message.LatitudeWGS_84 != null && Message.LongitudeWGS_84 != null) { row["Position in WGS-84 co-ordinates"] =  Message.LatitudeWGS_84 + ", " + Message.LongitudeWGS_84; }
            else { row["Position in WGS-84 co-ordinates"] = "No Data"; }
            if (Message.High_Resolution_LatitudeWGS_84 != null && Message.High_Resolution_LongitudeWGS_84 != null) { row["Position in WGS-84 co-ordinates high res"] =  Message.High_Resolution_LatitudeWGS_84 + ", " + Message.High_Resolution_LongitudeWGS_84; }
            else { row["Position in WGS-84 co-ordinates high res"] = "No Data"; }
            if (Message.Time_of_Applicability_Velocity != null) { row["Time of\nApplicability\nfor Velocity"] = Message.Time_of_Applicability_Velocity; }
            else { row["Time of\nApplicability\nfor Velocity"] = "No Data"; }
            if (Message.Air_Speed != null) { row["Air\nSpeed"] = Message.Air_Speed; }
            else { row["Air\nSpeed"] = "No Data"; }
            if (Message.True_Air_Speed != null) { row["True Air\nSpeed"] = Message.True_Air_Speed; }
            else { row["True Air\nSpeed"] = "No Data"; }
            if (Message.Target_address != null) { row["Target Address"] = Message.Target_address; }
            else { row["Target Address"] = "No Data"; }
            if (Message.Time_of_Message_Reception_Position != null) { row["Time of\nMessage\nReception\nof Position"] = Message.Time_of_Message_Reception_Position; }
            else { row["Time of\nMessage\nReception\nof Position"] = "No Data"; }
            if (Message.Time_of_Message_Reception_Velocity != null) { row["Time of\nMessage\nReception\nof Velocity"] = Message.Time_of_Message_Reception_Velocity; }
            else { row["Time of\nMessage\nReception\nof Velocity"] = "No Data"; }
            if (Message.Geometric_Height != null) { row["Geometric\nHeight"] = Message.Geometric_Height; }
            else { row["Geometric\nHeight"] = "No Data"; }
            if (Message.NUCr_NACv != null) { row["Quality\nIndicators"] = "Click to expand"; }
            else { row["Quality\nIndicators"] = "No Data"; }
            if (Message.MOPS != null) { row["MOPS Version"] = Message.MOPS; }
            else { row["MOPS Version"] = "No Data"; }
            if (Message.ModeA3 != null) { row["Mode-3A\nCode"] = Message.ModeA3; }
            else { row["Mode-3A\nCode"] = "No Data"; }
            if (Message.Roll_Angle != null) { row["Roll\nAngle"] = Message.Roll_Angle; }
            else { row["Roll\nAngle"] = "No Data"; }
            if (Message.Flight_Level != null) { row["Flight\nLevel"] = Message.Flight_Level; }
            else { row["Flight\nLevel"] = "No Data"; }
            if (Message.Magnetic_Heading != null) { row["Magnetic\nHeading"] = Message.Magnetic_Heading; }
            else { row["Magnetic\nHeading"] = "No Data"; }
            if (Message.ICF != null) { row["Target\nStatus"] = "Click to expand"; }
            else { row["Target\nStatus"] = "No Data"; }
            if (Message.Barometric_Vertical_Rate != null) { row["Barometric\nVertical Rate"] = Message.Barometric_Vertical_Rate; }
            else { row["Barometric\nVertical Rate"] = "No Data"; }
            if (Message.Geometric_Vertical_Rate != null) { row["Geometric\nVertical Rate"] = Message.Geometric_Vertical_Rate; }
            else { row["Geometric\nVertical Rate"] = "No Data"; }
            if (Message.Ground_vector != null) { row["Airborne Ground Vector"] = Message.Ground_vector; }
            else { row["Airborne Ground Vector"] = "No Data"; }
            if (Message.Track_Angle_Rate != null) { row["Track\nAngle\nRate"] = Message.Track_Angle_Rate; }
            else { row["Track\nAngle\nRate"] = "No Data"; }
            if (Message.Time_of_Asterix_Report_Transmission != null) { row["Time of\nReport\nTransmission"] = Message.Time_of_Asterix_Report_Transmission; }
            else { row["Time of\nReport\nTransmission"] = "No Data"; }
            if (Message.ECAT != null) { row["Emitter Category"] = Message.ECAT; }
            else { row["Emitter Category"] = "No Data"; }
            if (Message.MET_present != 0) { row["Met\nInformation"] = "Click to expand"; }
            else { row["Met\nInformation"] = "No Data"; }
            if (Message.Selected_Altitude != null) { row["Selected Altitude"] = Message.Selected_Altitude; }
            else { row["Selected Altitude"] = "No Data"; }
            if (Message.MV != null) { row["Final\nState\nSelected\nAltitude"] = "Click to expand"; }
            else { row["Final\nState\nSelected\nAltitude"] = "No Data"; }
            if (Message.Trajectory_present != 0) { row["Trajectory\nIntent"] = "Click to expand"; }
            else { row["Trajectory\nIntent"] = "No Data"; }
            if (Message.RP != null) { row["Service\nManagement"] = Message.RP; }
            else { row["Service\nManagement"] = "No Data"; }
            if (Message.RA != null) { row["Aircraft\nOperational\nStatus"] = "Click to expand"; }
            else { row["Aircraft\nOperational\nStatus"] = "No Data"; }
            if (Message.POA != null)
            {
                if (Message.POA.Length > 25) { row["Surface\nCapabilities\nand\nCharacteristics"] = "Click to expand"; }
                else { row["Surface\nCapabilities\nand\nCharacteristics"] = "No Data"; }
            }
            else { row["Surface\nCapabilities\nand\nCharacteristics"] = "No Data"; }
            if (Message.Message_Amplitude != null) { row["Message\nAmplitude"] = Message.Message_Amplitude; }
            else { row["Message\nAmplitude"] = "No Data"; }
            if (Message.MB_Data != null) { row["Mode S MB Data"] = "Click to expand"; }
            else { row["Mode S MB Data"] = "No Data"; }
            if (Message.TYP != null) { row["ACAS\nResolution\nAdvisory\nReport"] = "Click to expand"; }
            else { row["ACAS\nResolution\nAdvisory\nReport"] = "No Data"; }
            if (Message.Receiver_ID != null) { row["Receiver\nID"] = Message.Receiver_ID; }
            else { row["Receiver\nID"] = "No Data"; }
            if (Message.Data_Ages_present != 0) { row["Data Ages"] = "Click to expand"; }
            else { row["Data Ages"] = "No Data"; }
            tablaCAT21v21.Rows.Add(row);
        }

        public void print(string texto)
        {
            Console.WriteLine(texto);
        }
    }
}
