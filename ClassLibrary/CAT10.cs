using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace ClassLibrary
{
    public class CAT10
    {
        #region Principal Parameters of CAT10

        readonly string[] FSPEC;
        public int msgNum;
        public int msgCAT10Num;
        public int airportCode;
        public string CAT = "10";

        #endregion

        #region Extra Functions for Decoding Parameters

        public static string HexToBinary(string msgHexa)
        {
            string[] msgBin = new string[msgHexa.Length];
            string msgBinReal = "";
            for (int i = 0; i < msgHexa.Length; i++) {
               // if (msghexa[i].Length == 1){
               //     msghexa[i] = string.Concat('0', msghexa[i]);
               // }
                msgBin[i] = Convert.ToString(Convert.ToInt32(msgHexa,16), 2); 
                while (msgBin[i].Length < 8)
                {
                    msgBin[i] = string.Concat('0', msgBin[i]);
                }

                msgBinReal = string.Concat(msgBinReal, msgBin[i]);
            }
            return msgBinReal;
        }

        public string HexToBinary1(string octeto)
        {
            StringBuilder Octeto1 = new StringBuilder();
            StringBuilder Octeto2 = new StringBuilder();
            int i = 0;

            foreach (char a in octeto)
            {
                if (i == 0) { 
                    int len = Octeto1.Append(Convert.ToString(Convert.ToInt32(Convert.ToString(a), 16), 2)).ToString().Length;

                    while (len < 4)
                    {
                        Octeto1.Insert(0, '0');
                        len = Octeto1.Length;
                    }
                    i++;
                }
                else
                {
                    int len = Octeto2.Append(Convert.ToString(Convert.ToInt32(Convert.ToString(a), 16), 2)).ToString().Length;

                    while (len < 4)
                    {
                        Octeto2.Insert(0, '0');
                        len = Octeto2.Length;
                    }
                }

            }

            string oct = Octeto1.Append(Octeto2).ToString();

            return oct;
        }

        public int DecimalToOctal(int decimalNum) 
        {
            int octal = 0;
            int i = 1;

            while (decimalNum != 0)
            {
                octal = octal + (decimalNum % 8) * i;
                decimalNum = decimalNum / 8;
                i = i*10;
            }

            return octal;
        }

        public string BinarytoHexadecimal(string binaryNumber)
        {
            string hexadecimalNumber = string.Format("{0:x}", Convert.ToInt32(binaryNumber, 2));
            return hexadecimalNumber;
        }

        public string ComputeCharacter(string BinaryNumber)
        {
            int character = Convert.ToInt32(BinaryNumber, 2);
            List<string> codelist = new List<string>() { "", "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "", "", "", "", "", "" };
            return codelist[character];
        }
        
        public static string BinTwosComplementToSignedDecimal(string msgBinTwos) {
            string msgDecimal;
            String buffer = "";
            int result = 0;
            if (msgBinTwos[0] == '1')
            {
                for (int o = 0; o < msgBinTwos.Length; o++)
                {
                    if (msgBinTwos[o] == '1') { buffer += "0"; }
                    else if (msgBinTwos[o] == '0') { buffer += "1"; }
                }
                result = Convert.ToInt32(buffer, 2);
                result += 1;
                result = result * (-1);
            }
            else
            {
                result = Convert.ToInt32(msgBinTwos, 2);
            }
            msgDecimal = Convert.ToString(result);
            return msgDecimal;
        }

        public string FSPECnum (string[] message)
        {
            int position = 3;
            string FSPECNum = "";
            bool end = true;
            while (end == true)
            {
                string newOctet = Convert.ToString(Convert.ToInt32(message[position], 16), 2).PadLeft(8, '0');
                FSPECNum = FSPECNum + newOctet.Substring(0, 7);
                if (newOctet.Substring(7, 1) == "1")
                    position = position + 1;
                else
                    end = false;
            }
            return FSPECNum;
        }

        public void UTM2WGS84()
        {
        }

        public void LLA2UTM()
        {
        }

        #endregion

        #region CAT10 Decoding Procedure 
        public CAT10()
        {

        }
        public CAT10(string[] dataMessage, int position)
        {
            string FSPECNum = FSPECnum(dataMessage);
            position = 3 + FSPECNum.Length / 7;
            char[] FSPEC = FSPECNum.ToCharArray(0, FSPECNum.Length);

            string[] data = new string[dataMessage.Length];

            for (int i = 0; i < dataMessage.Length; i++)
            {
                data[i] = HexToBinary1(dataMessage[i]);
            }

            if (FSPEC[0] == '1') position = DataSourceIdentifier(data, position);
            if (FSPEC[1] == '1') position = MessageType(data, position);
            if (FSPEC[2] == '1') position = TargetReportDescriptor(data, position);
            if (FSPEC[3] == '1') position = TimeofDay(data, position);
            if (FSPEC[4] == '1') position = PositioninWGS84Coordinates(data, position);
            if (FSPEC[5] == '1') position = PositioninPolarCoordinates(data, position);
            if (FSPEC[6] == '1') position = PositioninCartesianCoordinates(data, position);
            
            if (FSPEC.Length > 8)
            {
                if (FSPEC[7] == '1') position = TrackVelocityInPolarCoordinates(data, position); 
                if (FSPEC[8] == '1') position = TrackVelocityInCartesianCoordinates(data, position); 
                if (FSPEC[9] == '1') position = TrackNumber(data, position); 
                if (FSPEC[10] == '1') position = TrackStatus(data, position);
                if (FSPEC[11] == '1') position = Mode3ACodeinOctalRepresentation(data, position); 
                if (FSPEC[12] == '1') position = TargetAddress(data, position); 
                if (FSPEC[13] == '1') position = TargetIdentification(data, position); 
            }

            if (FSPEC.Length > 15)
            {
                if (FSPEC[14] == '1') position = ModeSMBData(data, position); 
                if (FSPEC[15] == '1') position = VehicleFleetIdentificatior(data, position); 
                if (FSPEC[16] == '1') position = FlightlevelinBinaryRepresentation(data, position);
                if (FSPEC[17] == '1') position = MeasuredHeight(data, position);
                if (FSPEC[18] == '1') position = TargetSizeOrientation(data, position);
                if (FSPEC[19] == '1') position = SystemStatus(data, position);
                if (FSPEC[20] == '1') position = PreProgrammedMessage(data, position);
            }

            if (FSPEC.Length > 22)
            {
                if (FSPEC[21] == '1') position = StandardDeviationOfPosition(data, position);
                if (FSPEC[22] == '1') position = Presence(data, position);
                if (FSPEC[23] == '1') position = AmplitudeofPrimaryPlot(data, position);
                if (FSPEC[24] == '1') position = CalculatedAcceleration(data, position);
            }
        }

        #endregion

        #region DATA ITEMS DEFINITION & PARAMETER CALCULATION

        //DATA ITEM: I010/000
        public string messageType;

        private int MessageType(string[] data, int position)
        {
            int messageType = Convert.ToInt32(data[position], 2);
            if (messageType == 1) this.messageType = "Target Report";
            if (messageType == 2) this.messageType = "Start of Update Cycle";
            if (messageType == 3) this.messageType = "Periodic Status Message";
            if (messageType == 4) this.messageType = "Event-triggered Status Message"; 

            //Console.WriteLine("MessageType: " + this.messageType);

            position = position + 1;
            return position;
        }

        //DATA ITEM: I010/010
        public string SAC;
        public string SIC;
        
        private int DataSourceIdentifier(string[] data, int position)
        {
            this.SAC = Convert.ToString(Convert.ToInt32(data[position], 2));
            this.SIC = Convert.ToString(Convert.ToInt32(data[position + 1], 2));

            //Console.WriteLine("SAC: " + this.SAC);
            //Console.WriteLine("SIC: " + this.SIC);

            position = position + 2;
            return position;
        }

        //DATA ITEM: I010/020
        public string TYP;
        public string DCR;
        public string CHN;
        public string GBS;
        public string CRT;
        public string SIM;
        public string TST;
        public string RAB;
        public string LOP;
        public string TOT;
        public string SPI;

        private int TargetReportDescriptor(string[] data, int position)
        {
            int newposition = position;

            string octetNum1 = data[newposition];
            string TYP = octetNum1.Substring(0, 3); //The left first 3 bits (8, 7 & 6) represent TYP

            if (TYP == "000") this.TYP = "SSR MLAT";
            if (TYP == "001") this.TYP = "Mode S MLAT";
            if (TYP == "010") this.TYP = "ADS-B";
            if (TYP == "011") this.TYP = "PSR";
            if (TYP == "100") this.TYP = "Magnetic Loop System";
            if (TYP == "101") this.TYP = "HF MLAT";
            if (TYP == "110") this.TYP = "Not defined";
            if (TYP == "111") this.TYP = "Other types";

            string DCR = octetNum1.Substring(3, 1); //Next bit (5) represents DCR

            if (DCR == "0") this.DCR = "No differential correction";
            if (DCR == "1") this.DCR = "Differential correction";

            string CHN = octetNum1.Substring(4, 1); //Next bit (4) represents CHN

            if (CHN == "0") this.CHN = "Chain 1";
            if (CHN == "1") this.CHN = "Chain 2";

            string GBS = octetNum1.Substring(5, 1); //Next bit (3) represents GBS

            if (GBS == "0") this.GBS = "Transponder Ground bit not set";
            if (GBS == "1") this.GBS = "Transponder Ground bit set";

            string CRT = octetNum1.Substring(6, 1); //Next bit (2) represents CRT

            if (CRT == "0") this.CRT = "No Corrupted reply in MLAT";
            if (CRT == "1") this.CRT = "Corrupted replies in MLAT";

            string FX1 = Convert.ToString(octetNum1[7]); //Next bit (1) represents FX

            if (FX1 == "1")
            {
                newposition = newposition + 1;

                string octetNum1_1 = data[newposition];

                string SIM = octetNum1_1.Substring(0, 1);

                if (SIM == "0") this.SIM = "Actual target report";
                if (SIM == "1") this.SIM = "Simulated target report";

                string TST = octetNum1_1.Substring(1, 1);

                if (TST == "0") this.TST = "Default";
                if (TST == "1") this.TST = "Test Target";

                string RAB = octetNum1_1.Substring(2, 1);

                if (RAB == "0") this.RAB = "Report from target transponder";
                if (RAB == "1") this.RAB = "Report from field monitor (fixed transponder)";

                string LOP = octetNum1_1.Substring(3, 2);

                if (LOP == "00") this.LOP = "Undetermined";
                if (LOP == "01") this.LOP = "Loop start";
                if (LOP == "10") this.LOP = "Loop finish";

                string TOT = octetNum1_1.Substring(5, 2);

                if (TOT == "00") this.TOT = "Undetermined";
                if (TOT == "01") this.TOT = "Aircraft";
                if (TOT == "10") this.TOT = "Ground vehicle";
                if (TOT == "11") this.TOT = "Helicopter";

                string FX2 = Convert.ToString(octetNum1_1[7]);

                if (FX2 == "1")
                {
                    newposition = newposition + 1;

                    string octetNum1_2 = data[newposition];

                    string SPI = octetNum1_2.Substring(0, 1);

                    if (SPI == "0") this.SPI = "Absence of SPI";
                    if (SPI == "1") this.SPI = "Special Position Identification";

                    string spareBits = octetNum1_2.Substring(1, 6);

                    string FX3 = Convert.ToString(octetNum1_2[7]);
                }
            }

            //Console.WriteLine("TYP: " + this.TYP);
            //Console.WriteLine("DCR: " + this.DCR);
            //Console.WriteLine("CHN: " + this.CHN);
            //Console.WriteLine("GBS: " + this.GBS);
            //Console.WriteLine("CRT: " + this.CRT);
            //Console.WriteLine("SIM: " + this.SIM);
            //Console.WriteLine("TST: " + this.TST);
            //Console.WriteLine("RAB: " + this.RAB);
            //Console.WriteLine("LOP: " + this.LOP);
            //Console.WriteLine("TOT: " + this.TOT);
            //Console.WriteLine("SPI: " + this.SPI);

            return newposition + 1;
        }


        //DATA ITEM: I010/040
        public string RHO;
        public string THETA;
        public string positioninPolarCoordinates;

        private int PositioninPolarCoordinates(string[] data, int position) 
        {
            double range = Convert.ToInt32(string.Concat(data[position], data[position + 1]), 2); // RHO 2 octets
            
            if (range >= 65536) this.RHO = "RHO is equal or exceeds maximum range (65536m ≈ 35.4NM)";
            else this.RHO = "RHO = " + Convert.ToString(range) + "m";
            
            this.THETA = "THETA = " + String.Format("{0:0.00}", Convert.ToDouble(Convert.ToInt32(string.Concat(data[position + 2], data[position + 3]), 2)) * (360 / (Math.Pow(2, 16)))) + "º"; // THETA 2 octets
            
            this.positioninPolarCoordinates = this.RHO + ", " + this.THETA;

            //Console.WriteLine("RHO: " + this.RHO);
            //Console.WriteLine("THETA: " + this.THETA);
            //Console.WriteLine("Pos. in PolarCoords: " + this.positioninPolarCoordinates);

            return position + 4;
        }

        //DATA ITEM: I010/041
        public string LatitudeinWGS84;
        public string LongitudeinWGS84;
        public double LatitudeMapWGS84 = -200; //For the map
        public double LongitudeMapWGS84 = -200; //For the map

        private int PositioninWGS84Coordinates(string[] data, int position) 
        {
            int newposition = position + 4;
            double latitude = Convert.ToDouble(BinTwosComplementToSignedDecimal(string.Concat(data[position], data[position + 1], data[position + 2], data[position + 3]))) * (180 / (Math.Pow(2, 31)));
            double longitude = Convert.ToDouble(BinTwosComplementToSignedDecimal(string.Concat(data[newposition], data[newposition + 1], data[newposition + 2], data[newposition + 3]))) * (180 / (Math.Pow(2, 31)));

            int latitudeDeg = Convert.ToInt32(Math.Truncate(latitude));
            int latitudeMin = Convert.ToInt32(Math.Truncate((latitude - latitudeDeg) * 60));
            double latitudeSec = Math.Round(((latitude - (latitudeDeg +  (Convert.ToDouble(latitudeMin) / 60))) * 3600), 5);
            int longitudeDeg = Convert.ToInt32(Math.Truncate(longitude));
            int longitudeMin = Convert.ToInt32(Math.Truncate((longitude - longitudeDeg) * 60));
            double longitudeSec = Math.Round(((longitude - (longitudeDeg +  (Convert.ToDouble(longitudeMin) / 60))) * 3600), 5);
            
            this.LatitudeinWGS84 = Convert.ToString(latitudeDeg) + "º " + Convert.ToString(latitudeMin) + "' " + Convert.ToString(latitudeSec) + "''";
            this.LongitudeinWGS84 = Convert.ToString(longitudeDeg) + "º " + Convert.ToString(longitudeMin) + "' " + Convert.ToString(longitudeSec) + "''";

            //Console.WriteLine("LatWGS84: " + this.LatitudeinWGS84);
            //Console.WriteLine("LongWGS84: " + this.LongitudeinWGS84);

            return newposition;
        }

        //DATA ITEM: I010/42
        public double XMap = -99999; //For the map
        public double YMap = -99999; //For the map
        public string positioninCartesianCoordinates;

        private int PositioninCartesianCoordinates(string[] data, int position)
        {
            int newposition = position + 4;
            this.XMap = Convert.ToDouble(BinTwosComplementToSignedDecimal(string.Concat(data[position], data[position + 1])));
            this.YMap = Convert.ToDouble(BinTwosComplementToSignedDecimal(string.Concat(data[position + 2], data[position + 3])));

            string X = Convert.ToString(this.XMap);
            string Y = Convert.ToString(this.YMap);

            positioninCartesianCoordinates = "X Component = " + XMap + ", Y Component = " + YMap;

            //Point MapPoint = new Point(this.XMap, this.YMap);

            //PointLatLng position = lib.ComputeWGS_84_from_Cartesian(MapPoint, this.SIC); //Compute WGS84 position from cartesian position
            //Set_WGS84_Coordinates(position); //Apply computed WGS84 position to this message

            //Console.WriteLine("Pos. in CartesianCoords: " + this.positioninCartesianCoordinates);

            return newposition;
        }

        /*
        WSG84 FROM CARTESIAN
        public PointLatLng ComputeWGS_84_from_Cartesian(Point point, string SIC)
        {
            PointLatLng position = new PointLatLng();
            
            double X_pos = position.X;
            double Y_pos = position.Y;
        }

        public void Set_WGS84_Coordinates(PointLatLng pos)
        {
            LatitudeWGS_84_map=pos.Lat;
            LongitudeWGS_84_map=pos.Lng;
            int Latdegres = Convert.ToInt32(Math.Truncate(LatitudeWGS_84_map));
            int Latmin = Convert.ToInt32(Math.Truncate((LatitudeWGS_84_map - Latdegres) * 60));
            double Latsec = Math.Round(((LatitudeWGS_84_map - (Latdegres + (Convert.ToDouble(Latmin) / 60))) * 3600), 5);
            int Londegres = Convert.ToInt32(Math.Truncate(LongitudeWGS_84_map));
            int Lonmin = Convert.ToInt32(Math.Truncate((LongitudeWGS_84_map - Londegres) * 60));
            double Lonsec = Math.Round(((LongitudeWGS_84_map - (Londegres + (Convert.ToDouble(Lonmin) / 60))) * 3600), 5);
            Latitude_in_WGS_84 = Convert.ToString(Latdegres) + "º " + Convert.ToString(Latmin) + "' " + Convert.ToString(Latsec) + "''";
            Longitude_in_WGS_84 = Convert.ToString(Londegres) + "º" + Convert.ToString(Lonmin) + "' " + Convert.ToString(Lonsec) + "''";
        }
        */

        //DATA ITEM I010/060
        public string VMode3A;
        public string GMode3A;
        public string LMode3A;
        public string Mode3A;

        private int Mode3ACodeinOctalRepresentation(string[] data, int position)
        {
            string octetNum1 = data[position];
            string octetNum2 = data[position + 1];
            string V = octetNum1.Substring(0, 1);

            if (V == "0") this.VMode3A = "V: Code validated";
            else this.VMode3A = "V: Code not validated";

            string G = octetNum1.Substring(1, 1);

            if (G == "0") this.GMode3A = "G: Default";
            else this.GMode3A = "G: Garbled code";

            string L = octetNum1.Substring(2, 1);

            if (L == "0") this.LMode3A = "L: Mode-3/A code derived from the reply of the transponder";
            else this.LMode3A = "L: Mode-3/A code not extracted during the last scan";

            this.Mode3A = Convert.ToString(DecimalToOctal(Convert.ToInt32(string.Concat(data[position], data[position + 1]).Substring(4, 12), 2))).PadLeft(4,'0');

            //Console.WriteLine("VMode3A: " + this.VMode3A);
            //Console.WriteLine("GMode3A: " + this.GMode3A);
            //Console.WriteLine("LMode3A: " + this.LMode3A);
            //Console.WriteLine("Mode3A: " + this.Mode3A);

            position = position + 2;
            return position;
        }

        //DATA ITEM: I010/090
        public string VFlightLevel;
        public string GFlightLevel;
        public string FlightLevel;
        public string FlightLevelInfo;
        public string FlightLevelFT;

        private int FlightlevelinBinaryRepresentation(string[] data, int position) 
        {
            char[] octet = data[position].ToCharArray(0, 8);

            if (octet[0] == '0') this.VFlightLevel = "Code validated";
            else this.VFlightLevel = "Code not validated";

            if(octet[1] == 0) this.GFlightLevel = "Default";
            else this.GFlightLevel = "Garbled code";

            this.FlightLevel = Convert.ToString(Convert.ToDouble(BinTwosComplementToSignedDecimal(string.Concat(data[position],  data[position + 1]).Substring(2, 14))) * 0.25);

            this.FlightLevelFT =  Convert.ToString(Convert.ToDouble(FlightLevel) * 100) + " ft";

            this.FlightLevelInfo = this.VFlightLevel + ", " + this.GFlightLevel + ", FL" + this.FlightLevel;

            //Console.WriteLine("VFlight: " + this.VFlightLevel);
            //Console.WriteLine("GFlight: " + this.GFlightLevel);
            //Console.WriteLine("FlightLevel: " + this.FlightLevel);
            //Console.WriteLine("FlightLevelInfo: " + this.FlightLevelInfo);
            //Console.WriteLine("FlightLevelFT: " + this.FlightLevelFT);

            position += 2;
            return position;
        }

        //DATA ITEM: I010/091
        public string measuredHeight;

        private int MeasuredHeight(string[] data, int position)
        {
            this.measuredHeight = Convert.ToString(Convert.ToDouble(BinTwosComplementToSignedDecimal(string.Concat(data[position], data[position + 1]))) * 6.25) + " ft";

            //Console.WriteLine("MeasuredHeight: " + this.measuredHeight);

            position += 2;
            return position;
        }

        //DATA ITEM: I010/131
        public string PAM;

        private int AmplitudeofPrimaryPlot(string[] data, int position)
        {
            double PAM = Convert.ToInt32(data[position], 2);

            if (PAM == 0) this.PAM = "PAM: 0";
            else this.PAM = "PAM: " + Convert.ToString(Convert.ToInt32(data[position], 2));

            //Console.WriteLine("PAM: " + this.PAM);

            position += 1;
            return position;
        }

        //DATA ITEM: I010/140
        public string TimeOfDay;
        public int TimeOfDaySec;

        private int TimeofDay(string[] data, int position)
        {
            int number = Convert.ToInt32(string.Concat(data[position], data[position + 1], data[position + 2]), 2);
            double seconds = Convert.ToDouble(number) / 128;

            this.TimeOfDaySec = Convert.ToInt32(Math.Truncate(seconds));
            this.TimeOfDay = TimeSpan.FromSeconds(seconds).ToString(@"hh\:mm\:ss\:fff");

            //Console.WriteLine("TimeOfDay: " + this.TimeOfDay);

            position = position + 3;
            return position;
        }

        //DATA ITEM: I010/161
        public string TrackNum;

        private int TrackNumber(string[] data, int position)
        {
            this.TrackNum = Convert.ToString(Convert.ToInt32(string.Concat(data[position], data[position + 1]), 2));

            //Console.WriteLine("TrackNum: " + this.TrackNum);

            position += 2;
            return position;
        }

        //DATA ITEM: I010/170
        public string CNF;
        public string TRE;
        public string CST;
        public string MAH;
        public string TCC;
        public string STH;
        public string TOM;
        public string DOU;
        public string MRS;
        public string GHO;

        private int TrackStatus(string[] data, int position)
        {
            char[] octet = data[position].ToCharArray(0, 8);
            if (octet[0] == '0') this.CNF = "Confirmed track";
            else this.CNF = "Track in initialisation phase";
            
            if (octet[1] == '0') this.TRE = "Default";
            else this.TRE = "Last report for a track";

            string crt = string.Concat(octet[2], octet[3]);
            if (crt == "00") this.CST = "No extrapolation";
            else if (crt == "01") this.CST = "Predictable extrapolation due to sensor refresh period"; 
            else if (crt == "10") this.CST = "Predictable extrapolation in masked area"; 
            else if (crt == "11") this.CST = "Extrapolation due to unpredictable absence of detection"; 
            
            if (octet[4] == '0') this.MAH = "Default";
            else this.MAH = "Horizontal manoeuvre";

            if (octet[5] == '0') this.TCC = "Tracking performed in 'Sensor Plane', i.e. neither slant range correction nor projection was applied."; 
            else this.TCC = "Slant range correction and a suitable projection technique are used to track in a 2D.reference plane, tangential to the earth model at the Sensor Site co-ordinates."; 
            
            if (octet[6] == '0') this.STH = "Measured position"; 
            else this.STH = "Smoothed position"; 
            
            position = position + 1;

            if (octet[7] == '1')
            {
                octet = data[position].ToCharArray(0, 8);
                string tom = string.Concat(octet[0], octet[1]);
                if (tom == "00") this.TOM = "Unknown type of movement";
                else if (tom == "01") this.TOM = "Taking-off"; 
                else if (tom == "10") this.TOM = "Landing"; 
                else if (tom == "11") this.TOM = "Other types of movement"; 

                string dou = string.Concat(octet[2], octet[3], octet[4]);
                if (dou == "000") this.DOU = "No doubt"; 
                else if (dou == "001") this.DOU = "Doubtful correlation (undetermined reason)"; 
                else if (dou == "010") this.DOU = "Doubtful correlation in clutter"; 
                else if (dou == "011") this.DOU = "Loss of accuracy"; 
                else if (dou == "100") this.DOU = "Loss of accuracy in clutter"; 
                else if (dou == "101") this.DOU = "Unstable track"; 
                else if (dou == "110") this.DOU = "Previously coasted";

                string mrs = string.Concat(octet[5], octet[6]);
                if (mrs == "00") this.MRS = "Merge or split indication undetermined"; 
                else if (mrs == "01") this.MRS = "Track merged by association to plot"; 
                else if (mrs == "10") this.MRS = "Track merged by non-association to plot"; 
                else if (mrs == "11") this.MRS = "Split track"; 

                position = position + 1;

                if (octet[7] == '1')
                {
                    octet = data[position].ToCharArray(0, 8);
                    if (octet[0] == '0') this.GHO = "GHO: Default";
                    else this.GHO = "Ghost track";

                    position = position + 1;
                }
            }

            //Console.WriteLine("CNF: " + this.CNF);
            //Console.WriteLine("TRE: " + this.TRE);
            //Console.WriteLine("CST: " + this.CST);
            //Console.WriteLine("MAH: " + this.MAH);
            //Console.WriteLine("TCC: " + this.TCC);
            //Console.WriteLine("STH: " + this.STH);
            //Console.WriteLine("TOM: " + this.TOM);
            //Console.WriteLine("DOU: " + this.DOU);
            //Console.WriteLine("MRS: " + this.MRS);
            //Console.WriteLine("GHO: " + this.GHO);

            return position;
        }

        //DATA ITEM: I010/200
        public string GroundSpeed;
        public string TrackAngle;
        public string TrackVelocityPolarCoordinates;

        private int TrackVelocityInPolarCoordinates(string[] data, int position)
        {
            double groundSpeed = Convert.ToDouble(Convert.ToInt32(string.Concat(data[position], data[position + 1]), 2)) * Math.Pow(2, -14);
            double groundSpeed_meters = groundSpeed * 1852;

            if (groundSpeed >= 2) this.GroundSpeed = "Ground Speed is equal or higher than the maximum available value (2NM/s), ";
            else this.GroundSpeed = "GroundSpeed: " + String.Format("{0:0.00}", groundSpeed_meters) + "m/s, ";

            this.TrackAngle = "TrackAngle: " + String.Format("{0:0.00}", (Convert.ToInt32(string.Concat(data[position + 2], data[position + 3]),2)) * (360 / (Math.Pow(2, 16)))) + "°";
            this.TrackVelocityPolarCoordinates = this.GroundSpeed + this.TrackAngle;

            //Console.WriteLine("GroundSpeed: " + this.GroundSpeed);
            //Console.WriteLine("TrackAngle: " + this.TrackAngle);
            //Console.WriteLine("TrackVelPolar: " + this.TrackVelocityPolarCoordinates);

            position = position + 4;
            return position;
        }

        //DATA ITEM: I010/202
        public string Vx;
        public string Vy;
        public string TrackVelocityCartesianCoordinates;

        private int TrackVelocityInCartesianCoordinates(string[] data, int position)
        {
            double vx = Convert.ToInt32(BinTwosComplementToSignedDecimal(string.Concat(data[position], data[position + 1])))*0.25;
            this.Vx = "Vx: " + Convert.ToString(vx) + "m/s, ";
            
            double vy = Convert.ToInt32(BinTwosComplementToSignedDecimal(string.Concat(data[position + 2], data[position + 3]))) * 0.25;
            this.Vy = "Vy: " + Convert.ToString(vy) + "m/s";
            
            this.TrackVelocityCartesianCoordinates = this.Vx + this.Vy;

            //Console.WriteLine("Vx: " + this.Vx);
            //Console.WriteLine("Vy: " + this.Vy);
            //Console.WriteLine("TrackVelCartesian: " + this.TrackVelocityCartesianCoordinates);

            position = position + 4;
            return position;
        }

        //DATA ITEM: I010/210
        public string Ax;
        public string Ay;
        public string calculatedAcceleration;

        private int CalculatedAcceleration(string[] data, int position)
        {
            double ax = Convert.ToInt32(BinTwosComplementToSignedDecimal(data[position])) * 0.25;
            double ay = Convert.ToInt32(BinTwosComplementToSignedDecimal(data[position + 1])) * 0.25;
            
            if (ax >= 31 || ax <= -31) { this.Ax = "Ax exceed the max value"; }
            else { this.Ax = "Ax: " + Convert.ToString(ax) + "m/s²"; }
            
            if (ay >= 31 || ax <= -31) { this.Ay = "Ay exceed the max value"; }
            else { this.Ay = "Ay: " + Convert.ToString(ay) + "m/s²"; }
            
            this.calculatedAcceleration = this.Ax + " " + this.Ay;

            //Console.WriteLine("Ax: " + this.Ax);
            //Console.WriteLine("Ay: " + this.Ay);
            //Console.WriteLine("CalcAcc: " + this.Calculated_Acceleration);

            position = position + 2;
            return position;
        }

        //DATA ITEM: I010/220
        public string TargetAdd; //In Hexadecimal 

        private int TargetAddress(string[] data, int position)
        {
            this.TargetAdd = string.Concat(BinarytoHexadecimal(data[position]), BinarytoHexadecimal(data[position + 1]), BinarytoHexadecimal(data[position + 2]));

            //Console.WriteLine("TargetAdd: " + this.TargetAdd);

            position = position + 3;
            return position;
        }

        //DATA ITEM: I010/245
        public string STI;
        public string TargetId;

        private int TargetIdentification(string[] data, int position)
        {
            string sti = data[position];
            if (sti == "00000000") this.STI = "Callsign or registration downlinked from transponder";
            else if (sti == "01000000") this.STI = "Callsign not downlinked from transponder"; 
            else if (sti == "10000000") this.STI = "Registration not downlinked from transponder"; 

            string characters = string.Concat(data[position + 1], data[position + 2], data[position + 3], data[position + 4], data[position + 5], data[position + 6]);
            for (int i = 0; i < 8; i++) this.TargetId = Convert.ToString(ComputeCharacter(characters.Substring(i * 6, 6)));

            //Console.WriteLine("STI: " + this.STI);
            //Console.WriteLine("TargetId: " + this.TargetId);

            position = position + 7;
            return position;
        }

        //DATA ITEM: I010/250 
        public string[] MBData;
        public string[] BDS1;
        public string[] BDS2;
        public int modeSrep;

        private int ModeSMBData(string[] data, int position)
        {
            this.modeSrep = Convert.ToInt32(data[position], 2);
            if (this.modeSrep < 0) { this.MBData = new string[this.modeSrep]; this.BDS1 = new string[this.modeSrep]; this.BDS2 = new string[this.modeSrep]; }
            
            position =  position + 1;

            for (int i = 0; i < modeSrep; i++)
            {
                this.MBData[i] = String.Concat(data[position], data[position + 1], data[position + 2], data[position + 3], data[position + 4], data[position + 5], data[position + 6]);
                this.BDS1[1] = data[position + 7].Substring(0, 4);
                this.BDS2[1] = data[position + 7].Substring(4, 4);

                position = position + 8;
            }

            //Console.WriteLine("MBData: " + this.MBData);
            //Console.WriteLine("BSD1: " + this.BDS1);
            //Console.WriteLine("BSD2: " + this.BDS2);
            //Console.WriteLine("modeSrep: " + this.modeSrep);

            return position;
        }

        //DATA ITEM: I010/270 
        public string targetLength;
        public string targetOrientation;
        public string targetWidth;
        public string targetSizeOrientation;

        public int TargetSizeOrientation(string[] data, int position)
        {
            //this.targetLength = Convert.ToInt32(data[position][0]) * 64 + Convert.ToInt32(data[position][1]) * 32 + Convert.ToInt32(data[position][2]) * 16 + Convert.ToInt32(data[position][3]) * 8 + Convert.ToInt32(data[position][4]) * 4 + Convert.ToInt32(data[position][5]) * 2 + Convert.ToInt32(data[position][6]);
            this.targetLength = "Target Length: " + Convert.ToString(Convert.ToInt32(data[position].Substring(0, 7), 2)) + "m";
            if (data[position][7] == '1') position = position + 1;
            else return position;
            
            this.targetOrientation = "Target Orientation: " + Convert.ToString(Convert.ToDouble(Convert.ToInt32(data[position].Substring(0, 7) ,2)) * 360 / 128) + "º";
            if (data[position][7] == '1') position = position + 1;
            else return position;

            this.targetWidth = "Target Width: " + Convert.ToString(Convert.ToInt32(data[position].Substring(0, 7), 2)) + "m";

            this.targetSizeOrientation = this.targetLength + ", " + this.targetOrientation + ", " + this.targetWidth;

            //Console.WriteLine("targetLength: " + this.targetLength);
            //Console.WriteLine("targetOrientation: " + this.targetOrientation);
            //Console.WriteLine("targetWidth: " + this.targetWidth);
            //Console.WriteLine("targetSieOrientation: " + this.targetSizeOrientation);

            position = position + 1;
            return position;
        }

        //DATA ITEM: I010/280 
        public int REPPresence = 0;
        public string[] DRHO;
        public string[] DTHETA;

        private int Presence(string[] data, int position)
        {
            this.REPPresence = Convert.ToInt32(string.Concat(data[position]), 2);
            position = position + 1;

            for (int i = 0; i < REPPresence; i++)
            {
                this.DRHO[i] = Convert.ToString(Convert.ToInt32(data[position], 2)) + "m";
                this.DTHETA[i] = Convert.ToString(Convert.ToDouble(Convert.ToInt32(data[position + 1], 2)) * 0.15) + "º";
                
                position = position + 2;
            }

            //Console.WriteLine("REPPresence: " + this.REPPresence);
            //Console.WriteLine("DRHO: " + this.DRHO);
            //Console.WriteLine("THETA: " + this.DTHETA);

            return position;
        }

        //DATA ITEM: I010/300
        public string VFI;

        private int VehicleFleetIdentificatior(string[] data, int position)
        {
            int vfi = Convert.ToInt32(data[position], 2);
            
            if (vfi == 0) { this.VFI = "Unknown"; }
            else if (vfi == 1) this.VFI = "ATC equipment maintenance"; 
            else if (vfi == 2) this.VFI = "Airport maintenance"; 
            else if (vfi == 3) this.VFI = "Fire";
            else if (vfi == 4) this.VFI = "Bird scarer";
            else if (vfi == 5) this.VFI = "Snow plough"; 
            else if (vfi == 6) this.VFI = "Runway sweeper";
            else if (vfi == 7) this.VFI = "Emergency";
            else if (vfi == 8) this.VFI = "Police";
            else if (vfi == 9) this.VFI = "Bus";
            else if (vfi == 10) this.VFI = "Tug (push/tow)";
            else if (vfi == 11) this.VFI = "Grass cutter";
            else if (vfi == 12) this.VFI = "Fuel";
            else if (vfi == 13) this.VFI = "Baggage";
            else if (vfi == 14) this.VFI = "Catering";
            else if (vfi == 15) this.VFI = "Aircraft maintenance";
            else if (vfi == 16) this.VFI = "Flyco (follow me)";

            //Console.WriteLine("VFI: " + this.VFI);

            position = position + 1;
            return position;
        }

        //DATA ITEM: I010/310
        public string TRB;
        public string MSG;
        public string preProgrammedMessage;

        private int PreProgrammedMessage(string[] data, int position)
        {
            char[] OctetoChar = data[position].ToCharArray(0, 8);
            
            if (OctetoChar[0] == '0') this.TRB = "Trouble: Default"; 
            else if (OctetoChar[0] == '1') this.TRB = "Trouble: In Trouble"; 
            
            int msg = Convert.ToInt32(data[position].Substring(1, 7), 2);
            
            if (msg == 1) this.MSG = "Message: Towing aircraft"; 
            else if (msg == 2) this.MSG = "Message: “Follow me” operation"; 
            else if (msg == 3) this.MSG = "Message: Runway check"; 
            else if (msg == 4) this.MSG = "Message: Emergency operation (fire, medical…)"; 
            else if (msg == 5) this.MSG = "Message: Work in progress (maintenance, birds scarer, sweepers…)";

            this.preProgrammedMessage = TRB + " " + MSG;

            //Console.WriteLine("TRB: " + this.TRB);
            //Console.WriteLine("MSG: " + this.MSG);
            //Console.WriteLine("preProgramMsg: " + this.preProgrammedMessage);

            position = position + 1;
            return position;
        }

        //DATA ITEM: I010/500
        public string DeviationX;
        public string DeviationY;
        public string CovarianceXY;

        private int StandardDeviationOfPosition(string[] data, int position)
        {
            DeviationX = "Standard Deviation of X component (σx):" + Convert.ToString(Convert.ToDouble(Convert.ToInt32(data[position], 2)) * 0.25) + "m";
            DeviationY = "Standard Deviation of Y component (σy): " + Convert.ToString(Convert.ToDouble(Convert.ToInt32(data[position + 1], 2)) * 0.25) + "m";
            CovarianceXY = "Covariance (σxy): " + Convert.ToString(Convert.ToInt32(BinTwosComplementToSignedDecimal(string.Concat(data[position + 2], data[position + 3]))) * 0.25) + "m^2";

            //Console.WriteLine("DevX: " + this.DeviationX);
            //Console.WriteLine("DevY: " + this.DeviationY);
            //Console.WriteLine("CovXY: " + this.CovarianceXY);

            position = position + 4;
            return position;
        }

        //DATA ITEM: I010/550
        public string NOGO;
        public string OVL;
        public string TSV;
        public string DIV;
        public string TIF;

        private int SystemStatus(string[] data, int position)
        {
            char[] OctetoChar = data[position].ToCharArray(0, 8);
            int nogo = Convert.ToInt32(string.Concat(OctetoChar[0], OctetoChar[1]), 2);
            
            if (nogo == 0) this.NOGO = "Operational Release Status of the System (NOGO): Operational"; 
            else if (nogo == 1) this.NOGO = "Operational Release Status of the System (NOGO): Degraded"; 
            else if (nogo == 2) this.NOGO = "Operational Release Status of the System (NOGO): NOGO"; 
            
            if (OctetoChar[2] == '0') this.OVL = "Overload indicator: No overload"; 
            else if (OctetoChar[2] == '1') this.OVL = "Overload indicator: Overload"; 
            
            if (OctetoChar[3] == '0') this.TSV = "Time Source Validity: Valid"; 
            else if (OctetoChar[3] == '1') this.TSV = "Time Source Validity: Invalid"; 
            
            if (OctetoChar[4] == '0') this.DIV = "DIV: Normal Operation"; 
            else if (OctetoChar[4] == '1') this.DIV = "DIV: Diversity degraded"; 
            
            if (OctetoChar[5] == '0') this.TIF = "TIF: Test Target Operative";
            else if (OctetoChar[5] == '1') this.TIF = "TIF: Test Target Failure";

            //Console.WriteLine("NOGO: " + this.NOGO);
            //Console.WriteLine("OVL: " + this.OVL);
            //Console.WriteLine("TSV: " + this.TSV);
            //Console.WriteLine("DIV: " + this.DIV);
            //Console.WriteLine("TIF: " + this.TIF);

            position = position + 1;
            return position;
        }

        #endregion
    }
}
