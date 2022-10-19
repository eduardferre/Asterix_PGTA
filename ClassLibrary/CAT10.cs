using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace ClassLibrary
{
    public class CAT10
    {

        public static string[] HexToBinary(string[] msgHexa)
        {
            string[] msgBin = new string[msgHexa.Length];
            for (int i = 0; i < msgHexa.Length; i++) {
               // if (msghexa[i].Length == 1){
               //     msghexa[i] = string.Concat('0', msghexa[i]);
               // }
                msgBin[i] = Convert.ToString(Convert.ToInt32(msgHexa[i],16),2); 
                while (msgBin[i].Length < 8)
                {
                    msgBin[i] = string.Concat('0', msgBin[i]);
                }
            }
            return msgBin;
        }

        public string DecimaltoOctal() 
        {

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
        
        public static string[] BinTwosComplementToSignedDecimal(string[] msgBinTwos) { 
            string[] msgDecimal = new string[msgBinTwos.Length];
            for (int i = 0; i < msgBinTwos.Length; i++)
            {
                String buffer = "";
                int result = 0;
                Console.WriteLine(msgBinTwos[i][0]);
                if (msgBinTwos[i][0] == '1')
                {
                    for (int o = 0; o < msgBinTwos[i].Length; o++)
                    {
                        if(msgBinTwos[i][o] == '1') { buffer += "0"; }
                        else if (msgBinTwos[i][o] == '0') { buffer += "1"; }
                    }
                    result = Convert.ToInt32(buffer, 2);
                    result += 1;
                    result = result * (-1);
                }
                else
                {
                    result = Convert.ToInt32(msgBinTwos[i], 2);
                }
                msgDecimal[i] = Convert.ToString(result);
            }
            return msgDecimal;
        }

        readonly string[] FSPEC; 
        string [] data; //octets
        int position; //one octet

        public void DecodeCAT10(string[] fspec, int position)
        {
            if (FSPEC[0] == "1") 
            {
                position = DataSourceIdentifier(data, position);
            } 
            if (FSPEC[1] == "1") 
            { 
                position = MessageType(data, position);
            }
            if (FSPEC[2] == "1")
            {
                position = TargetReportDescriptor(data, position);
            }
            if (FSPEC[3] == "1")
            {
                position = TimeofDay(data, position);
            }
            if (FSPEC[4] == "1")
            {
                position = PositioninWGS84Coordinates(data, position);
            }
            if (FSPEC[5] == "1")
            {
                position = MeasuredPositioninPolarCoordinates(data, position);
            }
            if (FSPEC[6] == "1")
            {
                position = PositioninCartesianCoordinates(data, position);
            }
            if (FSPEC.Length > 8)
            {
                if (FSPEC[7] == "1") {
                    //position = TrackVelocityInPolarCoordinates(data, position); 
                } 
                if (FSPEC[8] == "1") {
                    //position = TrackVelocityInCartesianCoordinates(data, position); 
                } 
                if (FSPEC[9] == "1") {
                    position = TrackNumber(data, position); 
                } 
                if (FSPEC[10] == "1") {
                    position = TrackStatus(data, position); 
                }
                if (FSPEC[11] == "1") {
                    position = Mode3ACodeinOctalRepresentation(data, position); 
                } 
                if (FSPEC[12] == "1") {
                    position = TargetAddress(data, position); 
                } 
                if (FSPEC[13] == "1") {
                    position = TargetIdentification(data, position); 
                } 
            }

        }

        //DATA ITEM: I010/010
        public string SAC;
        public string SIC;
        private int DataSourceIdentifier(string[] data, int position)
        {
            this.SAC = Convert.ToString(Convert.ToInt32(data[position], 2));
            this.SIC = Convert.ToString(Convert.ToInt32(data[position + 1], 2));
            position = position + 2;
            return position;
        }

        //DATA ITEM: I010/000
        public string messageType;
        private int MessageType(string[] data, int position)
        {
            string messageType = data[position];
            if (messageType == "00") {
                this.messageType = "Target Report"; 
            }
            if (messageType == "01") {
                this.messageType = "Start of Update Cycle"; 
            }
            if (messageType == "10") {
                this.messageType = "Periodic Status Message"; 
            }
            if (messageType == "11") {
                this.messageType = "Event-triggered Status Message"; 
            }
            position = position + 1;
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
            if (TYP == "001") this.TYP = "MOde S MLAT";
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

            return newposition + 1;
        }


        //DATA ITEM: I010/040
        public string RHO;
        public string THETA;
        public string PositioninPolarCoordinates;

        private int MeasuredPositioninPolarCoordinates(string[] data, int position) 
        {
            double range = Convert.ToInt32(string.Concat(data[position], data[position + 1]), 2); // RHO 2 octets
            
            if (range >= 65536) this.RHO = "RHO is equal or exceeds maximum range (65536m ≈ 35.4NM)";
            else this.RHO = "ρ = " + Convert.ToString(range) + "m";
            
            this.THETA = "θ = " + String.Format("{0:0.00}", Convert.ToDouble(Convert.ToInt32(string.Concat(data[position + 2], data[position + 3]), 2)) * (360 / (Math.Pow(2, 16)))) + "º"; // THETA 2 octets
            
            this.PositioninPolarCoordinates = this.RHO + ", " + this.THETA;

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

            return newposition;
        }

        //DATA ITEM: I010/42
        public double XMap = -99999; //For the map
        public double YMap = -99999; //For the map
        public string PositioninCartesianCoordinates;
        private int PositioninCartesianCoordinates(string[] data, int position)
        {
            int newposition = position + 4;
            this.XMap = Convert.ToDouble(BinTwosComplementToSignedDecimal(string.Concat(data[position], data[position + 1])));
            this.YMap = Convert.ToDouble(BinTwosComplementToSignedDecimal(string.Concat(data[position + 2], data[position + 3])));

            string X = Convert.ToString(this.XMap);
            string Y = Convert.ToString(this.YMap);

            this.PositioninCartesianCoordinates = "X Component = " + XMap + ", Y Component = " + YMap;

            //Point MapPoint = new Point(this.XMap, this.YMap);

            //PointLatLng position = lib.ComputeWGS_84_from_Cartesian(MapPoint, this.SIC); //Compute WGS84 position from cartesian position
            //Set_WGS84_Coordinates(position); //Apply computed WGS84 position to this message

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

            //!this.Mode3A = Convert.ToString(lib.ConvertDecimalToOctal(Convert.ToInt32(string.Concat(message[pos], message[pos + 1]).Substring(4, 12), 2))).PadLeft(4,'0');
            
            position += 2;
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

            if (octet[0] == "0") this.VFlightLevel = "Code validated";
            else this.VFlightLevel = "Code not validated";

            if(octet[1] == 0) this.GFlightLevel = "Default";
            else this.GFlightLevel = "Garbled code";

            this.FlightLevel = Convert.ToString(Convert.ToDouble(BinTwosComplementToSignedDecimal(string.Concat(data[position],  data[position + 1]).Substring(2, 14))) * 0.25);

            this.FlightLevelFT =  Convert.ToString(Convert.ToDouble(FlightLevel) * 100) + " ft";

            this.FlightLevelInfo = this.VFlightLevel + ", " + this.GFlightLevel + ", FL" + this.FlightLevel;

            position += 2;
            return position;
        }

        //DATA ITEM: I010/091
        public string MeasuredHeight;
        private int MeasuredHeight(string[] data, int position)
        {
            this.MeasuredHeight = Convert.ToString(Convert.ToDouble(BinTwosComplementToSignedDecimal(string.Concat(data[position], data[position + 1]))) * 6.25) + " ft";
            
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
            
            position += 1;
            return position;
        }

        //DATA ITEM: I010/140
        public string TimeOfDay;
        private int TimeofDay(string[] data, int position)
        {
            int number = Convert.ToInt32(string.Concat(data[position], data[position + 1], data[position + 2]), 2);
            double seconds = Convert.ToDouble(number) / 128;
            TimeOfDay = TimeSpan.FromSeconds(seconds).ToString(@"hh\:mm\:ss\:fff");
            position = position + 3;
            return position;
        }

        //DATA ITEM: I010/161
        public string TrackNum;
        private int TrackNumber(string[] data, int position)
        {
            this.TrackNum = Convert.ToString(Convert.ToInt32(string.Concat(data[position], data[position + 1]), 2));
            
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
            
            position = position + 2;

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

                position = position + 2;

                if (octet[7] == '1')
                {
                    octet = data[position].ToCharArray(0, 8);
                    if (octet[0] == '0') this.GHO = "GHO: Default";
                    else this.GHO = "Ghost track";

                    position = position + 2;
                }
            }
            return position;
        }

        //DATA ITEM: I010/200
        public string GroundSpeed;
        public string TrackAngle;
        public string TrackVelocityPolarCoordinates;
        private int TrackVelocityInPolarCoordinates(string[] data, int position)
        {
            double groundSpeed = Convert.ToDouble(Convert.ToInt32(string.Concat(data[position], data[position + 1]), 2)) * Math.Pow(2, -14);
            
            //!FALTA

        }

        //DATA ITEM: I010/202

        //DATA ITEM: I010/210


        //DATA ITEM: I010/220
        public string TargetAdd; //In Hexadecimal 
        private int TargetAddress(string[] data, int position)
        {
            this.TargetAdd = string.Concat(BinarytoHexadecimal(data[position]), BinarytoHexadecimal(data[position + 1]), BinarytoHexadecimal(data[position + 2]));
            position += 3;
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
            
            position += 7;
            return position;
        }

        //DATA ITEM: I010/270 
        public int targetLength;
        public double targetOrientation;
        public int targetLenght2;
        public int TargetSizeOrientation(string[] data, int position)
        {
            this.targetLength = Convert.ToInt32(data[position][0]) * 64 + Convert.ToInt32(data[position][1]) * 32 + Convert.ToInt32(data[position][2]) * 16 + Convert.ToInt32(data[position][3]) * 8 + Convert.ToInt32(data[position][4]) * 4 + Convert.ToInt32(data[position][5]) * 2 + Convert.ToInt32(data[position][6]);
            if (data[position][7] == '1') position = +1;
            else return position;
            
            this.targetOrientation = Convert.ToInt32(data[position][0]) * 64 * 2.81 + Convert.ToInt32(data[position][1]) * 32 * 2.81 + Convert.ToInt32(data[position][2]) * 16 * 2.81 + Convert.ToInt32(data[position][3]) * 8 * 2.81 + Convert.ToInt32(data[position][4]) * 4 * 2.81 + Convert.ToInt32(data[position][5]) * 2 * 2.81 + Convert.ToInt32(data[position][6]) * 2.81;
            if (data[position][7] == '1') position = +1;
            else return position;
            
            this.targetLenght2 = Convert.ToInt32(data[position][0]) * 64 + Convert.ToInt32(data[position][1]) * 32 + Convert.ToInt32(data[position][2]) * 16 + Convert.ToInt32(data[position][3]) * 8 + Convert.ToInt32(data[position][4]) * 4 + Convert.ToInt32(data[position][5]) * 2 + Convert.ToInt32(data[position][6]);
            
            position += 1;
            return position;
        }

        public void UTM2WGS84()
        {
        }

        public void LLA2UTM()
        {
        }
    }
}
