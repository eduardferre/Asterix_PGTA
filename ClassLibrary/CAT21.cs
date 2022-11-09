using System;
using System.Collections.Generic;
using System.Text;

namespace CLassLibrary
{
    public class CAT21
    {
        #region Principal Parameters of CAT21

        readonly string[] FSPEC;
        public int msgNum;
        public int msgCAT21Num;
        public int airportCode;
        public string CAT = "21";

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
                msgBin[i] = Convert.ToString(Convert.ToInt32(msgHexa,16),2); 
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
                if (i == 0) 
                { 
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
                    if (msgBinTwos[o] == '1') buffer += "0";
                    else if (msgBinTwos[o] == '0') buffer += "1";
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

        #region CAT21 Decoding Procedure 

        public void DecodECAT21(string[] dataMessage, int position)
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
            if (FSPEC[2] == '1') position = TrackNumber(data, position);
            if (FSPEC[3] == '1') position = ServiceIdentification(data, position);
            if (FSPEC[4] == '1') position = TimeOfApplicabilityForPosition(data, position);
            if (FSPEC[5] == '1') position = PositionInWGS84Coordinates(data, position);
            if (FSPEC[6] == '1') position = PositionInWGS84CoordinatesHighResolution(data, position);
            
            if (FSPEC.Length > 8)
            {
               if (FSPEC[7] == '1') position = TimeOfApplicabilityForVelocity(data, position); 
               if (FSPEC[8] == '1') position = AirSpeed(data, position); 
               if (FSPEC[9] == '1') position = TrueAirSpeed(data, position); 
               if (FSPEC[10] == '1') position = TargetAddress(data, position);
               if (FSPEC[11] == '1') position = TimeOfMessageReceptionOfPosition(data, position); 
               if (FSPEC[12] == '1') position = TimeOfMessageReceptionOfPositionHighPrecision(data, position); 
               if (FSPEC[13] == '1') position = TimeOfMessageReceptionOfVelocity(data, position); 
            }

            if (FSPEC.Length > 15)
            {
               if (FSPEC[14] == '1') position = TimeOfMessageReceptionOfVelocityHighPrecision(data, position); 
               if (FSPEC[15] == '1') position = GeometricHeight(data, position); 
               if (FSPEC[16] == '1') position = QualityIndicators(data, position);
               if (FSPEC[17] == '1') position = MOPSVersion(data, position);
               if (FSPEC[18] == '1') position = Mode3ACodeinOctalRepresentation(data, position);
               if (FSPEC[19] == '1') position = RollAngle(data, position);
               if (FSPEC[20] == '1') position = FlightLevel(data, position);
            }

            if (FSPEC.Length > 22)
            {
               if (FSPEC[21] == '1') position = MagneticHeading(data, position);
               if (FSPEC[22] == '1') position = TargetStatus(data, position);
               if (FSPEC[23] == '1') position = BarometricVerticalRate(data, position);
               if (FSPEC[24] == '1') position = GeometricVerticalRate(data, position);
               if (FSPEC[25] == '1') position = AirborneGroundVector(data, position);
               if (FSPEC[26] == '1') position = TrackAngleRate(data, position);
               if (FSPEC[27] == '1') position = TimeOfReportTransmission(data, position);
            }

            if (FSPEC.Length > 29)
            {
               if (FSPEC[28] == '1') position = TargetIdentification(data, position);
               if (FSPEC[29] == '1') position = EmitterCategory(data, position);
               if (FSPEC[30] == '1') position = MetInformation(data, position);
               if (FSPEC[31] == '1') position = SelectedAltitude(data, position);
               if (FSPEC[32] == '1') position = FinalStateSelectedAltitude(data, position);
               if (FSPEC[33] == '1') position = TrajectoryIntent(data, position);
               if (FSPEC[34] == '1') position = ServiceManagement(data, position);
            }

            if (FSPEC.Length > 36)
            {
               if (FSPEC[35] == '1') position = AircraftOperationalStatus(data, position);
               if (FSPEC[36] == '1') position = SurfaceCapabilitiesAndCharacteristics(data, position);
               if (FSPEC[37] == '1') position = MessageAmplitude(data, position);
               if (FSPEC[38] == '1') position = ModeSMBData(data, position);
               if (FSPEC[39] == '1') position = ACASResolutionAdvisoryReport(data, position);
               if (FSPEC[40] == '1') position = ReceiverID(data, position);
               if (FSPEC[41] == '1') position = DataAges(data, position);
            }

            if (FSPEC.Length > 43)
            {
               if (FSPEC[47] == '1') position = ReservedExpansionField(data, position);
               if (FSPEC[48] == '1') position = SpecialPurposeField(data, position);
            }

        }

        #endregion

        #region DATA ITEMS DEFINITION & PARAMETER CALCULATION

        //DATA ITEM: I021/008
        public string RA;
        public string TC;
        public string TS;
        public string ARV;
        public string CDTIA;
        public string NotTCAS;
        public string SA;

        private int AircraftOperationalStatus(string[] data, int position)
        {
            string octet = data[position];

            string RA = octet.Substring(0, 1);
            if (RA == "1") this.RA = "TCAS RA active";
            else this.RA = "TCAS II or ACAS RA not active";

            string TC = octet.Substring(1, 2);
            if (TC == "00") this.TC = "No capability for Trajectory Change Reports";
            else if (TC == "01") this.TC = "Support for TC+0 reports only";
            else if (TC == "10") this.TC = "Support for multiple TC reports";
            else this.TC = "Reserved";

            string TS = octet.Substring(3, 1);
            if (TS == "0") this.TS = "No capability to support Target State Reports";
            else this.TS = "Capable of supporting target State Reports";

            string ARV = octet.Substring(4, 1);
            if (ARV == "0") this.ARV = "No capability to generate ARV-Reports";
            else this.ARV = "Capable of generate ARV-Reports";

            string CDTIA = octet.Substring(5, 1);
            if (CDTIA == "0") this.CDTIA = "CDTI not operational";
            else this.CDTIA = "CDTI operational";

            string NotTCAS = octet.Substring(6, 1);
            if (NotTCAS == "0") this.NotTCAS = "TCAS operational";
            else this.NotTCAS = "TCAS not operational";

            string SA = octet.Substring(7, 1);
            if (SA == "0") this.SA = "Antenna Diversity";
            else this.SA = "Single Antenna only";

            position = position + 1;
            return position;
        }

        //DATA ITEM: I021/010
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

        //DATA ITEM: I021/015
        public string ServiceId;

        private int ServiceIdentification(string[] data, int position) 
        {
            this.ServiceId = Convert.ToString(Convert.ToInt32(data[position], 2));
            
            position = position + 1;
            return position;
        }

        //DATA ITEM: I021/016
        public string RP;

        private int ServiceManagement(string[] data, int position) 
        {
            this.RP = Convert.ToString(Convert.ToDouble(Convert.ToInt32(data[position], 2)) * 0.5) + " s";
            
            position = position + 1;
            return position;
        }


        //DATA ITEM: I021/020
        public string ECAT;

        private int EmitterCategory(string[] data, int position)
        {
            int ECAT = Convert.ToInt32(data[position], 2);

            if (ECAT == 0) this.ECAT = "No ADS-B Emitter Category Information";
            if (ECAT == 1) this.ECAT = "Light aircraft (<=15500 lbs)";
            if (ECAT == 2) this.ECAT = "Small aircraft (>15500 lbs || <75000 lbs)";
            if (ECAT == 3) this.ECAT = "Medium aircraft(>75000 lbs || <300000 lbs";
            if (ECAT == 4) this.ECAT = "High Vortex Large";
            if (ECAT == 5) this.ECAT = "Heavy aircraft (>=300000 lbs";
            if (ECAT == 6) this.ECAT = "Highly manoeuvrable (5g acceleration capability) and high speed(>400 kts cruise)";
            if (ECAT == 7 || ECAT == 8 || ECAT == 9) this.ECAT = "Reserved";
            if (ECAT == 10) this.ECAT = "Rotocraft";
            if (ECAT == 11) this.ECAT = "Glider / Sailplane";
            if (ECAT == 12) this.ECAT = "Lighter than air";
            if (ECAT == 13) this.ECAT = "Unmanned Aerial VehiCLe";
            if (ECAT == 14) this.ECAT = "Space / Transatmospheric VehiCLe";
            if (ECAT == 15) this.ECAT = "Ultralight / Handglider / Paraglider";
            if (ECAT == 16) this.ECAT = "Parachutist / Skydiver";
            if (ECAT == 17 || ECAT == 18 || ECAT == 19) this.ECAT = "Reserved";
            if (ECAT == 20) this.ECAT = "Surface emergency vehiCLe";
            if (ECAT == 21) this.ECAT = "Surface service vehiCLe";
            if (ECAT == 22) this.ECAT = "Fixed ground or tethered obstruction";
            if (ECAT == 23) this.ECAT = "CLuster obstaCLe";
            if (ECAT == 24) this.ECAT = "Line obstaCLe";

            position = position + 1;
            return position;
        }

        //DATA ITEM: I021/040
        public string ATP;
        public string ARC;
        public string RC;
        public string RAB;
        public string DCR;
        public string GBS;
        public string SIM;
        public string TST;
        public string SAA;
        public string CL;
        public string IPC;
        public string NOGO;
        public string CPR;
        public string LDPJ;
        public string RCF;
        public string FX;
        
        private int TargetReportDescriptor(string[] data, int position)
        {
            int newposition = position;

            string octetNum1 = data[newposition];
            
            int ATP = Convert.ToInt32(octetNum1.Substring(0, 3), 2);
            if (ATP == 0) this.ATP = "24-Bit ICAO address";
            else if (ATP == 1) this.ATP = "Duplicate address";
            else if (ATP == 2) this.ATP = "Surface vehiCLe address";
            else if (ATP == 3) this.ATP = "Anonymous address";
            else this.ATP = "Reserved for future use";

            int ARC = Convert.ToInt32(octetNum1.Substring(4, 2), 2);
            if (ARC == 0) this.ARC = "25 ft ";
            else if (ARC == 1) this.ARC = "100 ft";
            else if (ARC == 2) this.ARC = "Unknown";
            else this.ARC = "Invalid";

            int RC = Convert.ToInt32(octetNum1.Substring(5, 1), 2);
            if (RC == 0) this.RC = "Default";
            else this.RC = "Range Check passed, CPR Validation pending";

            int RAB = Convert.ToInt32(octetNum1.Substring(6, 1), 2);
            if (RAB == 0) this.RAB = "Report from target transponder";
            else this.RAB = "Report from field monitor (fixed transponder)";

            int FX1 = Convert.ToInt32(octetNum1.Substring(7, 1), 2);

            if (FX1 == 1)
            {
                newposition = newposition + 1;

                string octetNum1_1 = data[newposition];
                
                int DCR = Convert.ToInt32(octetNum1_1.Substring(0, 1), 2);
                if (DCR == 0) this.DCR = "No differential correction (ADS-B)";
                else this.DCR = "Differential correction (ADS-B)";

                int GBS = Convert.ToInt32(octetNum1_1.Substring(1, 1), 2);
                if (GBS == 0) this.GBS = "Ground Bit not set";
                else this.GBS = "Ground Bit set";

                int SIM = Convert.ToInt32(octetNum1_1.Substring(2, 1), 2);
                if (SIM == 0) this.SIM = "Actual target report";
                else this.SIM = "Simulated target report";

                int TST = Convert.ToInt32(octetNum1_1.Substring(3, 1), 2);
                if (TST == '0') this.TST = "Default";
                else this.TST = "Test Target";

                int SAA = Convert.ToInt32(octetNum1_1.Substring(4, 1), 2);
                if (SAA == '0') this.SAA = "Equipment capable to provide Selected Altitude";
                else this.SAA = "Equipment not capable to provide Selected Altitude";

                int CL = Convert.ToInt32(octetNum1_1.Substring(5, 2), 2);
                if (CL == 0) this.CL = "Report valid";
                else if (CL == 1) this.CL = "Report suspect";
                else if (CL == 2) this.CL = "No information";
                else this.CL = "Reserved for future use";
                
                int FX2 = Convert.ToInt32(octetNum1_1.Substring(7, 1), 2);

                if (FX2 == 1)
                {
                    newposition = newposition + 1;

                    string octetNum1_2 = data[newposition];

                    string spareBits = octetNum1_2.Substring(0, 2);

                    int IPC = Convert.ToInt32(octetNum1_2.Substring(2, 1), 2);
                    if (IPC == 0) this.IPC = "Default";
                    else this.IPC = "Independent Position Check failed";

                    int NOGO = Convert.ToInt32(octetNum1_2.Substring(3, 1), 2);
                    if (NOGO == 0) this.NOGO = "NOGO-bit not set";
                    else this.NOGO = "NOGO-bit set";

                    int CPR = Convert.ToInt32(octetNum1_2.Substring(4, 1), 2);
                    if (CPR == 0) this.CPR = "CPR Validation correct ";
                    else this.CPR = "CPR Validation failed";
                    
                    int LDPJ = Convert.ToInt32(octetNum1_2.Substring(5, 1), 2);
                    if (LDPJ == 0) this.LDPJ = "LDPJ not detected";
                    else this.LDPJ = "LDPJ detected";

                    int RCF = Convert.ToInt32(octetNum1_2.Substring(6, 1), 2);
                    if (RCF == 0) this.RCF = "Default";
                    else this.RCF = "Range Check failed ";

                    int FX3 = Convert.ToInt32(octetNum1_2.Substring(7, 1), 2);
                }
            }

            return newposition + 1;
        }

        //DATA ITEM: I021/070
        public string Mode3A;

        private int Mode3ACodeinOctalRepresentation(string[] data, int position) 
        {
            this.Mode3A = Convert.ToString(DecimalToOctal(Convert.ToInt32(string.Concat(data[position], data[position + 1]).Substring(4, 12), 2))).PadLeft(4,'0');
            
            position = position + 2;
            return position; 
        }

        //DATA ITEM: I021/071
        public string timeOfApplicabilityForPosition;

        private int TimeOfApplicabilityForPosition(string[] data, int position)
        {
            double seconds = Convert.ToDouble(Convert.ToInt32(string.Concat(data[position], data[position + 1], data[position + 2]), 2)) / 128;
            TimeSpan time = TimeSpan.FromSeconds(seconds);

            this.timeOfApplicabilityForPosition = time.ToString(@"hh\:mm\:ss\:fff");

            position = position + 3;
            return position;
        }

        //DATA ITEM: I021/072
        public string timeOfApplicabilityForVelocity;

        private int TimeOfApplicabilityForVelocity(string[] data, int position)
        {
            double seconds = Convert.ToDouble(Convert.ToInt32(string.Concat(data[position], data[position + 1], data[position + 2]), 2)) / 128;
            TimeSpan time = TimeSpan.FromSeconds(seconds);
            
            this.timeOfApplicabilityForVelocity = time.ToString(@"hh\:mm\:ss\:fff");

            position = position + 3;
            return position;
        }

        //DATA ITEM: I021/073
        public string timeOfMessageReceptionOfPosition;

        private int TimeOfMessageReceptionOfPosition(string[] data, int position)
        {
            double seconds = Convert.ToDouble(Convert.ToInt32(string.Concat(data[position], data[position + 1], data[position + 2]), 2)) / 128;
            TimeSpan time = TimeSpan.FromSeconds(seconds);
            
            this.timeOfMessageReceptionOfPosition = time.ToString(@"hh\:mm\:ss\:fff");
            
            position = position + 3;
            return position;
        }

        //DATA ITEM: I021/074
        public string timeOfMessageReceptionOfPositionHighPrecision;
        public string FSI_Position;

        private int TimeOfMessageReceptionOfPositionHighPrecision(string[] data, int position)
        {
            string octet = string.Concat(data[position], data[position + 1], data[position + 2], data[position + 3]);
            
            FSI_Position = octet.Substring(0, 2);
            double time = Convert.ToDouble(Convert.ToInt32(octet.Substring(2, 30), 2)) * Math.Pow(2, -30);

            if (FSI_Position == "00") this.FSI_Position = "Same Time";
            if (FSI_Position == "01") 
            { 
                time =  time + 1;
                this.FSI_Position = "Time +1 second";
            }
            if (FSI_Position == "10") 
            { 
                time =  time - 1;
                this.FSI_Position = "Time -1 second";
            }
            if (FSI_Position == "01") this.FSI_Position = "Reserved";
            
            this.timeOfMessageReceptionOfPositionHighPrecision = Convert.ToString(time) + " s";
            
            position = position + 4;
            return position;
        }

        //DATA ITEM: I021/075
        public string timeOfMessageReceptionOfVelocity;

        private int TimeOfMessageReceptionOfVelocity(string[] data, int position)
        {
            double seconds = Convert.ToDouble(Convert.ToInt32(string.Concat(data[position], data[position + 1], data[position + 2]), 2)) / 128;
            TimeSpan time = TimeSpan.FromSeconds(seconds);
            
            this.timeOfMessageReceptionOfVelocity = time.ToString(@"hh\:mm\:ss\:fff");
            
            position = position + 3;
            return position;
        }

        //DATA ITEM: I021/076
        public string timeOfMessageReceptionOfVelocityHighPrecision;
        public string FSI_Velocity;

        private int TimeOfMessageReceptionOfVelocityHighPrecision(string[] data, int position)
        {
            string octet = string.Concat(data[position], data[position + 1], data[position + 2], data[position + 3]);
            
            FSI_Velocity = octet.Substring(0, 2);
            double time = Convert.ToDouble(Convert.ToInt32(octet.Substring(2, 30), 2)) * Math.Pow(2, -30);

            if (FSI_Velocity == "00") this.FSI_Velocity = "Same Time";
            if (FSI_Velocity == "01") 
            { 
                time =  time + 1;
                this.FSI_Velocity = "Time +1 second";
            }
            if (FSI_Velocity == "10") 
            { 
                time =  time - 1;
                this.FSI_Velocity = "Time -1 second";
            }
            if (FSI_Velocity == "01") this.FSI_Velocity = "Reserved";
            
            this.timeOfMessageReceptionOfVelocityHighPrecision = Convert.ToString(time) + " s";
            
            position = position + 4;
            return position;
        }

        //DATA ITEM: I021/077
        public string timeOfASTERIXReportTransmission;
        public int timeOfDaySeconds;

        private int TimeOfASTERIXReportTransmission(string[] data, int position)
        {
            double seconds = Convert.ToDouble(Convert.ToInt32(string.Concat(data[position], data[position + 1], data[position + 2]), 2)) / 128;
            TimeSpan time = TimeSpan.FromSeconds(seconds);
            
            this.timeOfDaySeconds = Convert.ToInt32(Math.Truncate(seconds));
            this.timeOfASTERIXReportTransmission = time.ToString(@"hh\:mm\:ss\:fff");
            
            position = position + 3;
            return position;
        }

        //DATA ITEM: I021/080
        public string TargetAdd; //In Hexadecimal 

        private int TargetAddress(string[] data, int position)
        {
            this.TargetAdd = string.Concat(BinarytoHexadecimal(data[position]), BinarytoHexadecimal(data[position + 1]), BinarytoHexadecimal(data[position + 2]));

            position = position + 3;
            return position;
        }

        //DATA ITEM: I021/090
        //public string qualityIndicators;
        public string NUCr_NACv;
        public string NUCp_NIC;
        public string NICbaro;
        public string SIL;
        public string NACp;
        public string SILS;
        public string SDA;
        public string GVA;
        public int PIC;
        public string ICB;
        public string NUCp;
        public string NIC;

        private int QualityIndicators(string[] data, int position)
        {
            int newposition = position;

            string octetNum1 = data[newposition];

            this.NUCr_NACv = Convert.ToString(Convert.ToInt32(octetNum1.Substring(0, 3), 2));
            this.NUCp_NIC = Convert.ToString(Convert.ToInt32(octetNum1.Substring(3, 4), 2));

            string FX1 = octetNum1.Substring(7, 1);

            if (FX1 == "1")
            {
                newposition = newposition + 1;

                string octetNum1_1 = data[newposition];

                this.NICbaro = Convert.ToString(Convert.ToInt32(octetNum1_1.Substring(0, 1), 2));
                this.SIL = Convert.ToString(Convert.ToInt32(octetNum1_1.Substring(1, 2), 2));
                this.NACp = Convert.ToString(Convert.ToInt32(octetNum1_1.Substring(3, 4), 2));
                
                string FX2 = octetNum1_1.Substring(7, 1);

                if (FX2 == "1")
                {
                    newposition = newposition + 1;

                    string octetNum1_2 = data[newposition];

                    string SILS = octetNum1_2.Substring(2, 1);
                    if (SILS == "0") this.SILS = "Measured per flight-Hour";
                    else this.SILS = "Measured per sample";

                    this.SDA = Convert.ToString(Convert.ToInt32(data[position].Substring(3, 2), 2));
                    this.GVA = Convert.ToString(Convert.ToInt32(data[position].Substring(5, 2), 2));

                    string FX3 = octetNum1_2.Substring(7, 1);

                    if (FX3 == "1")
                    {
                        newposition = newposition + 1;

                        string octetNum1_3 = data[newposition];

                        this.PIC = Convert.ToInt32(octetNum1_3.Substring(0, 4), 2);
                        if (this.PIC == 0) this.ICB = "No integrity(or > 20.0 NM)"; this.NUCp = "0"; this.NIC = "0";
                        if (this.PIC == 1) this.ICB = "< 20.0 NM"; this.NUCp = "1"; this.NIC = "1";
                        if (this.PIC == 2) this.ICB = "< 10.0 NM"; this.NUCp = "2"; this.NIC = "-";
                        if (this.PIC == 3) this.ICB = "< 8.0 NM"; this.NUCp = "-"; this.NIC = "2";
                        if (this.PIC == 4) this.ICB = "< 4.0 NM"; this.NUCp = "-"; this.NIC = "3";
                        if (this.PIC == 5) this.ICB = "< 2.0 NM"; this.NUCp = "3"; this.NIC = "4";
                        if (this.PIC == 6) this.ICB = "< 1.0 NM"; this.NUCp = "4"; this.NIC = "5";
                        if (this.PIC == 7) this.ICB = "< 0.6 NM"; this.NUCp = "-"; this.NIC = "6 (+ 1/1)";
                        if (this.PIC == 8) this.ICB = "< 0.5 NM"; this.NUCp = "5"; this.NIC = "6 (+ 0/0)";
                        if (this.PIC == 9) this.ICB = "< 0.3 NM"; this.NUCp = "-"; this.NIC = "6 (+ 0/1)";
                        if (this.PIC == 10) this.ICB = "< 0.2 NM"; this.NUCp = "6"; this.NIC = "7";
                        if (this.PIC == 11) this.ICB = "< 0.1 NM"; this.NUCp = "7"; this.NIC = "8";
                        if (this.PIC == 12) this.ICB = "< 0.04 NM"; this.NUCp = ""; this.NIC = "9";
                        if (this.PIC == 13) this.ICB = "< 0.013 NM"; this.NUCp = "8"; this.NIC = "10";
                        if (this.PIC == 14) this.ICB = "< 0.004 NM"; this.NUCp = "9"; this.NIC = "11";
                        
                        string spareBits = octetNum1_3.Substring(4, 3);

                        string FX4 = Convert.ToString(octetNum1_3[7]);
                    }
                }
            }

            return newposition + 1;
        }

        //DATA ITEM: I021/110
        public bool TIS;
        public bool TID;
        public string NAV;
        public string NVB;
        public int REP;
        public string[] TCA;
        public string[] NC;
        public int[] TCP;
        public string[] Altitude;
        public string[] Latitude;
        public string[] Longitude;
        public string[] PointType;
        public string[] TD;
        public string[] TRA;
        public string[] TOA;
        public string[] TOV;
        public string[] TTR;

        private int TrajectoryIntent(string[] data, int position)
        {   
            if (data[position].Substring(0, 1) == "1") this.TIS = true;
            else this.TIS = false;
            
            if (data[position].Substring(1, 1) == "1") this.TID = true;
            else this.TID = false;

            if (this.TIS == true)
            {
                position = position + 1;

                if (data[position].Substring(0, 1) == "0") this.NAV = "Trajectory Intent Data is available for this aircraft";
                else this.NAV = "Trajectory Intent Data is not available for this aircraft ";

                if (data[position].Substring(1, 1) == "0") this.NVB = "Trajectory Intent Data is valid";
                else this.NVB = "Trajectory Intent Data is not valid";
            }

            if (this.TID == true)
            {
                position = position + 1;

                this.REP = Convert.ToInt32(data[position], 2);
                this.TCA = new string[this.REP];
                this.NC = new string[this.REP];
                this.TCP = new int[this.REP];
                this.Altitude = new string[this.REP];
                this.Latitude = new string[this.REP];
                this.Longitude = new string[this.REP];
                this.PointType = new string[this.REP];
                this.TD = new string[this.REP];
                this.TRA = new string[this.REP];
                this.TOA = new string[this.REP];
                this.TOV = new string[this.REP];
                this.TTR = new string[this.REP];
                
                position = position + 1;

                for (int i = 0; i < this.REP; i++)
                {
                    if (data[position].Substring(0, 1) == "0") this.TCA[i] = "TCP number available";
                    else this.TCA[i] = "TCP number not available";

                    if (data[position].Substring(1, 1) == "0") this.NC[i] = "TCP compliance";
                    else this.NC[i] = "TCP non-compliance";

                    this.TCP[i] = Convert.ToInt32(data[position].Substring(2, 6));
                    
                    position = position + 1;

                    this.Altitude[i] = Convert.ToString(Convert.ToDouble(BinTwosComplementToSignedDecimal(string.Concat(data[position], data[position + 1]))) * 10) + " ft";

                    position = position + 2;

                    this.Latitude[i] = Convert.ToString(Convert.ToDouble(BinTwosComplementToSignedDecimal(string.Concat(data[position], data[position + 1]))) * (180 / (Math.Pow(2, 23)))) + " º";

                    position = position + 2;
                    
                    this.Longitude[i] = Convert.ToString(Convert.ToDouble(BinTwosComplementToSignedDecimal(string.Concat(data[position], data[position + 1]))) * (180 / (Math.Pow(2, 23)))) + " º";
                    
                    position = position + 2;

                    int PT = Convert.ToInt32(data[position].Substring(0, 4), 2);
                    if (PT == 0) this.PointType[i] = "Unknown";
                    else if (PT == 1) this.PointType[i] = "Fly by waypoint (LT) ";
                    else if (PT == 2) this.PointType[i] = "Fly over waypoint (LT)";
                    else if (PT == 3) this.PointType[i] = "Hold pattern (LT)";
                    else if (PT == 4) this.PointType[i] = "Procedure hold (LT)";
                    else if (PT == 5) this.PointType[i] = "Procedure turn (LT)";
                    else if (PT == 6) this.PointType[i] = "RF leg (LT)";
                    else if (PT == 7) this.PointType[i] = "Top of climb (VT)";
                    else if (PT == 8) this.PointType[i] = "Top of descent (VT)";
                    else if (PT == 9) this.PointType[i] = "Start of level (VT)";
                    else if (PT == 10) this.PointType[i] = "Cross-over altitude (VT)";
                    else this.PointType[i] = "Transition altitude (VT)";

                    string TD = data[position].Substring(4, 2);
                    if (TD == "00") this.TD[i] = "N/A";
                    else if (TD == "01") this.TD[i] = "Turn right";
                    else if (TD == "10") this.TD[i] = "Turn left";
                    else this.TD[i] = "No turn";

                    if (data[position].Substring(6, 1) == "0") this.TRA[i] = "TTR not available";
                    else this.TRA[i] = "TTR available";

                    if (data[position].Substring(7, 1) == "0") this.TOA[i] = "TOV available";
                    else this.TOA[i] = "TOV not available";

                    position = position + 1;

                    this.TOV[i] = Convert.ToString(Convert.ToInt32(string.Concat(data[position], data[position + 1], data[position + 2]), 2)) + " s";

                    position = position + 3;

                    this.TTR[i] = Convert.ToString(Convert.ToInt32(string.Concat(data[position], data[position + 1]), 2) * 0.01) + " NM";
                    
                    position = position + 2;
                }
            }

            return position;
        }

        //DATA ITEM: I021/130
        public string LatitudeinWGS84;
        public string LongitudeinWGS84;
        public double LatitudeMapWGS84 = -200; //For the map
        public double LongitudeMapWGS84 = -200; //For the map

        private int PositionInWGS84Coordinates(string[] data, int position) 
        {
            int newposition = position + 3;
            double latitude = Convert.ToDouble(BinTwosComplementToSignedDecimal(string.Concat(data[position], data[position + 1], data[position + 2]))) * (180 / (Math.Pow(2, 23)));
            double longitude = Convert.ToDouble(BinTwosComplementToSignedDecimal(string.Concat(data[newposition], data[newposition + 1], data[newposition + 2]))) * (180 / (Math.Pow(2, 23)));

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

        //DATA ITEM: I021/131
        public string LatitudeinWGS84HighResolution;
        public string LongitudeinWGS84HighResolution;

        private int PositionInWGS84CoordinatesHighResolution(string[] data, int position)
        {
            int newposition = position + 4;
            double latitude = Convert.ToDouble(BinTwosComplementToSignedDecimal(string.Concat(data[position], data[position + 1], data[position + 2], data[position + 3]))) * (180 / (Math.Pow(2, 30)));
            double longitude = Convert.ToDouble(BinTwosComplementToSignedDecimal(string.Concat(data[newposition], data[newposition + 1], data[newposition + 2], data[newposition + 3]))) * (180 / (Math.Pow(2, 30)));

            int latitudeDeg = Convert.ToInt32(Math.Truncate(latitude));
            int latitudeMin = Convert.ToInt32(Math.Truncate((latitude - latitudeDeg) * 60));
            double latitudeSec = Math.Round(((latitude - (latitudeDeg +  (Convert.ToDouble(latitudeMin) / 60))) * 3600), 5);
            int longitudeDeg = Convert.ToInt32(Math.Truncate(longitude));
            int longitudeMin = Convert.ToInt32(Math.Truncate((longitude - longitudeDeg) * 60));
            double longitudeSec = Math.Round(((longitude - (longitudeDeg +  (Convert.ToDouble(longitudeMin) / 60))) * 3600), 5);
            
            this.LatitudeinWGS84HighResolution = Convert.ToString(latitudeDeg) + "º " + Convert.ToString(latitudeMin) + "' " + Convert.ToString(latitudeSec) + "''";
            this.LongitudeinWGS84HighResolution = Convert.ToString(longitudeDeg) + "º " + Convert.ToString(longitudeMin) + "' " + Convert.ToString(longitudeSec) + "''";

            //Console.WriteLine("LatWGS84: " + this.LatitudeinWGS84);
            //Console.WriteLine("LongWGS84: " + this.LongitudeinWGS84);

            return newposition;
        }

        //DATA ITEM: I021/132
        public string messageAmplitude;

        private int MessageAmplitude(string[] data, int position) 
        { 
            this.messageAmplitude = Convert.ToString(Convert.ToDouble(BinTwosComplementToSignedDecimal(string.Concat(data[position])))) + " dBm"; 
            
            position = position + 1;
            return position; 
        }

        //DATA ITEM: I021/140
        public string geometricHeight;

        private int GeometricHeight(string[] data, int position)
        {
            this.geometricHeight = Convert.ToString(Convert.ToDouble(BinTwosComplementToSignedDecimal(string.Concat(data[position], data[position + 1]))) * 6.25) + " ft";
            
            position = position + 2;
            return position; 
        }

        //DATA ITEM: I021/145
        public string fligthLevel;

        private int FlightLevel(string[] data, int position)
        {
            this.fligthLevel = Convert.ToString(Convert.ToDouble(BinTwosComplementToSignedDecimal(string.Concat(data[position], data[position + 1]))) * (0.25)) + " FL";
            
            position = position + 2;
            return position; 
        }

        //DATA ITEM: I021/146
        public string SAS;
        public string Source;
        public string selectedAltitude;

        private int SelectedAltitude(string[] data, int position)
        {          
            string Source = data[position].Substring(1, 2);
            if (Source == "00") this.Source = "Unknown";
            else if (Source == "01") this.Source = "Aircraft Altitude (Holding Altitude)";
            else if (Source == "10") this.Source = "MCP/FCU Selected Altitude";
            else this.Source = "FMS Selected Altitude";

            
            this.selectedAltitude = "SA: " + Convert.ToString(Convert.ToDouble(BinTwosComplementToSignedDecimal(string.Concat(data[position], data[position + 1]).Substring(3, 13))) * 25) + " ft";
            
            position = position + 2;
            return position; 
        }

        //DATA ITEM: I021/148
        public string MV;
        public string AH;
        public string AM;
        public string finalStateSelectedAltitude;

        private int FinalStateSelectedAltitude(string[] data, int position)
        {
            if (data[position].Substring(0, 1) == "0") this.MV = "Not active or unknown";
            else this.MV = "Active";
            if (data[position].Substring(1, 1) == "0") this.AH = "Not active or unknown";
            else this.AH = "Active";
            if (data[position].Substring(2, 1) == "0") this.AM = "Not active or unknown";
            else this.AM = "Active";
            
            this.finalStateSelectedAltitude = Convert.ToString(Convert.ToDouble(BinTwosComplementToSignedDecimal(string.Concat(data[position], data[position + 1]).Substring(3, 13))) * 25) + " ft";
            
            position = position + 2;
            return position;
        }

        //DATA ITEM: I021/150
        public string airSpeed;

        private int AirSpeed(string[] data, int position)
        {
            if (data[position].Substring(0, 1) == "0") this.airSpeed = Convert.ToString(Convert.ToInt32(string.Concat(data[position], data[position + 1]).Substring(1, 15),2) * Math.Pow(2, -14)) + " NM/s";
            else this.airSpeed = Convert.ToString(Convert.ToInt32(string.Concat(data[position], data[position + 1]).Substring(1, 15),2) * 0.001) + " Mach";
            
            position = position + 2;
            return position;
        }

        //DATA ITEM: I021/151
        public string trueAirSpeed;

        private int TrueAirSpeed(string[] data, int position)
        {
            if (data[position].Substring(0, 1) == "0") this.trueAirSpeed = Convert.ToString(Convert.ToInt32(string.Concat(data[position], data[position + 1]).Substring(1, 15),2)) + " kts";
            else this.trueAirSpeed = "Value exceeds defined rage";
            
            position = position + 2;
            return position;
        }

        //DATA ITEM: I021/152
        public string magneticHeading;

        private int MagneticHeading(string[] data, int position)
        {
            this.magneticHeading = Convert.ToString(Convert.ToInt32(string.Concat(data[position], data[position + 1]), 2) * (360 / (Math.Pow(2, 16)))) + "º"; 
            
            position = position + 2;
            return position;
        }

        //DATA ITEM: I021/155
        public string barometricVerticalRate;

        private int BarometricVerticalRate(string[] data, int position)
        {
            if (data[position].Substring(0, 1) == "0") this.barometricVerticalRate = Convert.ToString(Convert.ToDouble(BinTwosComplementToSignedDecimal(string.Concat(data[position], data[position + 1]).Substring(1, 15))) * 6.25) + " ft/min";
            else this.barometricVerticalRate = "Value exceeds defined rage";
            
            position = position + 2;
            return position;
        }

        //DATA ITEM: I021/157
        public string geometricVerticalRate;

        private int GeometricVerticalRate(string[] data, int position)
        {
            if (data[position].Substring(0, 1) == "0") this.geometricVerticalRate = Convert.ToString(Convert.ToDouble(BinTwosComplementToSignedDecimal(string.Concat(data[position], data[position + 1]).Substring(1, 15))) * 6.25) + " feet/minute";
            else this.geometricVerticalRate = "Value exceeds defined rage";
            
            position = position + 2;
            return position;
        }

        //DATA ITEM: I021/160
        public string GroundSpeed;
        public string TrackAngle;
        public string GroundVector;

        private int AirborneGroundVector(string[] data, int position)
        {
            if (data[position].Substring(0, 1) == "0")
            {
                this.GroundSpeed = String.Format("{0:0.00}", (Convert.ToInt32(string.Concat(data[position], data[postion + 1]).Substring(1, 15),2) * Math.Pow(2, -14)*3600)) +  " kts";
                this.TrackAngle = String.Format("{0:0.00}", Convert.ToInt32(string.Concat(data[position + 2], data[postion + 3]).Substring(0, 16),2) * (360 / (Math.Pow(2, 16)))) + " º";
                this.GroundVector = "GS: " + this.GroundSpeed + ", TA: " + String.Format("{0:0.00}", this.TrackAngle);
            }
            else this.GroundVector = "Value exceeds defined rage";

            position = position + 4;
            return position;
        }

        //DATA ITEM: I021/161
        public string trackNumber;

        private int TrackNumber(string[] data, int postion)
        {
            this.trackNumber = Convert.ToString(Convert.ToInt32(string.Concat(data[position], data[position + 1]).Substring(4,12), 2)); 
            
            position = position + 2;
            return position;  
        }

        //DATA ITEM: I021/165
        public string trackAngleRate;

        private int TrackAngleRate(string[] data, int position) 
        {
            this.trackAngleRate = Convert.ToString(Convert.ToInt32(string.Concat(data[position], data position[position + 1]).Substring(6, 10), 2)*(1/32)) + " º/s";
            
            position = position + 2;
            return position; 
        }

        //DATA ITEM: I021/170
        public string TargetId;

        private int TargetIdentification (string[] data, int position)
        {
            string characters = string.Concat(data[position + 1], data[position + 2], data[position + 3], data[position + 4], data[position + 5]);
            for (int i = 0; i < 8; i++) this.TargetId = Convert.ToString(ComputeCharacter(characters.Substring(i * 6, 6)));
            
            position = position + 6;
            return position;
        }

        //DATA ITEM: I021/200
        public string ICF;
        public string LNAV;
        public string PS;
        public string SS;

        private int TargetStatus(string[] data, int position)
        {
            if (data[position].Substring(0, 1) == "0") this.ICF = "No intent change active";
            else this.ICF= "Intent change flag raised";

            if (data[position].Substring(1, 1) == "0") this.LNAV = "LNAV Mode engaged";
            else this.LNAV = "LNAV Mode not engaged";

            int PS = Convert.ToInt32(data[position].Substring(3,3), 2);
            if (PS == 0) this.PS = "No emergency / not reported";
            else if (PS == 1) this.PS = "General emergency";
            else if (PS == 2) this.PS = "Lifeguard / medical emergency";
            else if (PS == 3) this.PS = "Minimum fuel";
            else if (PS == 4) this.PS = "No communications";
            else if (PS == 5) this.PS = "Unlawful interference";
            else this.PS = "'Downed' Aircraft ";
            
            int SS = Convert.ToInt32(data[position].Substring(6, 2), 2);
            if (SS == 0) this.SS = "No condition reported";
            else if (SS == 1) this.SS = "Permanent Alert (Emergency condition)";
            else if (SS == 2) this.SS = "Temporary Alert (change in Mode 3/A Code other than emergency)";
            else this.SS = "SPI set";
            
            position = position + 1;
            return position;
        }

        //DATA ITEM: I021/210
        public string VNS;
        public string LTT;
        public string MOPS;
        public string VN;

        private int MOPSVersion(string[] data, int position)
        {
            if (data[position].Substring(1, 1) == "0") this.VNS = "The MOPS Version is supported by the GS";
            else this.VNS = "The MOPS Version is not supported by the GS";

            int LTT = Convert.ToInt32( data[position].Substring(5,3),2);
            if (LTT == 0) this.LTT = "Other";
            else if (LTT == 1) this.LTT = "UAT";
            else if (LTT == 2) 
            {
                int VN = Convert.ToInt32(data[position].Substring(2, 3), 2);
                if (VN == 0) this.VN = "ED102/DO-260";
                if (VN == 1) this.VN = "DO-260A";
                if (VN == 2) this.VN = "ED102A/DO-260B";
                this.LTT = "1090 ES | Version Number: " + this.VN; 
            }
            else if (LTT == 3) this.LTT = "VDL 4";
            else this.LTT = "Not assigned";
            this.MOPS = this.LTT;
            
            position = position + 1;
            return position;
        }

        //DATA ITEM: I021/220
        public string WindSpeed;
        public string WindDirection;
        public string Temperature;
        public string Turbulence;

        private int MetInformation (string[] message, int pos)
        {
            int positionInitial = position;

            if (data[positionInitial].Substring(0, 1) == "1") { WindSpeed = Convert.ToString(Convert.ToInt32(string.Concat(data[position + 1], data[position + 2]), 2)) + " kts"; position = position + 2; }
            if (data[positionInitial].Substring(1, 1) == "1") { WindDirection = Convert.ToString(Convert.ToInt32(string.Concat(data[position + 1], data[position + 2]), 2)) + " º"; position = position + 2; }
            if (data[positionInitial].Substring(2, 1) == "1") { Temperature = Convert.ToString(Convert.ToInt32(string.Concat(data[position + 1], data[position + 2]), 2) * 0.25) + " ºC"; position = position + 2; }
            if (data[positionInitial].Substring(3, 1) == "1") { Turbulence = Convert.ToString(Convert.ToInt32(string.Concat(data[position + 1], data[position + 2]), 2)) + " Turbulence"; position = position + 2; }
            
            return posfin;
        }


        #endregion
    }
}
