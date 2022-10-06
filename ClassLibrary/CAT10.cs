using System;
using System.Collections.Generic;
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

        
       
       public static string[] BinTwosComplementToSignedDecimal(string[] msgBinTwos) { 
            string[] msgDecimal = new string[msgBinTwos.Length];
            for (int i = 0; i < msgBinTwos.Length; i++)
            {
                String buffer = "";
                int result = 0;
                Console.WriteLine(msgBinTwos[i][0]);
                if (msgBinTwos[i][0] == '1')
                {
                    for (int o = 0; o < 8; o++)
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
                DataSourceIdentifier(data, position);
                position = position + 2;
            } 
            if (FSPEC[1] == "1") 
            { 
                MessageType(data, position);
                position = position + 1;
            }
            if (FSPEC[2] == "1")
            {
                position = TargetReportDescriptor(data, position);
            }
            if (FSPEC[3] == "1")
            {
                TimeofDay(data, positon);
                position = position + 3;
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
            

        }

        //DATA ITEM: I010/010
        public string SAC;
        public string SIC;

        private void DataSourceIdentifier(string[] data, int position)
        {
            this.SAC = Convert.ToString(Convert.ToInt32(data[position], 2));
            this.SIC = Convert.ToString(Convert.ToInt32(data[position + 1], 2));
        }


        //DATA ITEM: I010/000
        public string messageType;

        private void MessageType(string[] data, int position)
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
        public string PositioninPolarCoordinates

        private void MeasuredPositioninPolarCoordinates(string[] data, int position) 
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
            double latitude = Convert.ToDouble(BinTwosComplementToSignedDecimal(data[position]) + BinTwosComplementToSignedDecimal(data[position + 1]) + BinTwosComplementToSignedDecimal(data[position + 2]) + BinTwosComplementToSignedDecimal(data[position + 3])) * (180 / (Math.Pow(2, 31)));
            double longitude = Convert.ToDouble(BinTwosComplementToSignedDecimal(data[newposition]) + BinTwosComplementToSignedDecimal(data[newposition + 1]) + BinTwosComplementToSignedDecimal(data[newposition + 2]) + BinTwosComplementToSignedDecimal(data[newposition + 3])) * (180 / (Math.Pow(2, 31)));

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
            this.XMap = Convert.ToDouble(BinTwosComplementToSignedDecimal(data[position]) + BinTwosComplementToSignedDecimal(data[position + 1]));
            this.YMap = Convert.ToDouble(BinTwosComplementToSignedDecimal(data[position + 2]) + BinTwosComplementToSignedDecimal(data[position + 3]));

            string X = Convert.ToString(this.XMap);
            string Y = Convert.ToString(this.YMap);

            PositioninCartesianCoordinates = "X Component = " + XMap + ", Y Component = " + YMap;

            Point MapPoint = new Point(XMap, YMap);

            /*
                WSG84 FROM CARTESIAN

                PointLatLng position = lib.ComputeWGS_84_from_Cartesian(p, this.SIC); //Compute WGS84 position from cartesian position
                Set_WGS84_Coordinates(position); //Apply computed WGS84 position to this message

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

            return newposition;
        }

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

            if (V == "0") VMode3A = "V: Code validated";
            else VMode3A = "V: Code not validated";

            string G = octetNum1.Substring(1, 1);

            if (G == "0") GMode3A = "G: Default";
            else GMode3A = "G: Garbled code";

            string L = octetNum1.Substring(2, 1);

            if (L == "0") LMode3A = "L: Mode-3/A code derived from the reply of the transponder";
            else LMode3A = "L: Mode-3/A code not extracted during the last scan";


        }

        public void UTM2WGS84()
        {
        }

        public void LLA2UTM()
        {
        }
    }
}
