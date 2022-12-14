using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace ClassLibrary
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
            return hexadecimalNumber.ToUpper();
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

        public CAT21()
        {

        }
        public CAT21(string[] dataMessage, int position)
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
            if (FSPEC[1] == '1') position = TargetReportDescriptor(data, position);
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
               if (FSPEC[27] == '1') position = TimeOfASTERIXReportTransmission(data, position);
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
               //if (FSPEC[47] == '1') position = ReservedExpansionField(data, position);
               //if (FSPEC[48] == '1') position = SpecialPurposeField(data, position);
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
            if (RA == "1") this.RA = "RA: TCAS RA active";
            else this.RA = "RA: TCAS II or ACAS RA not active";

            string TC = octet.Substring(1, 2);
            if (TC == "00") this.TC = "TC: No capability for Trajectory Change Reports";
            else if (TC == "01") this.TC = "TC: Support for TC+0 reports only";
            else if (TC == "10") this.TC = "TC: Support for multiple TC reports";
            else this.TC = "TC: Reserved";

            string TS = octet.Substring(3, 1);
            if (TS == "0") this.TS = "TS: No capability to support Target State Reports";
            else this.TS = "TS: Capable of supporting target State Reports";

            string ARV = octet.Substring(4, 1);
            if (ARV == "0") this.ARV = "ARV: No capability to generate ARV-Reports";
            else this.ARV = "ARV: Capable of generate ARV-Reports";

            string CDTIA = octet.Substring(5, 1);
            if (CDTIA == "0") this.CDTIA = "CDTIA: CDTI not operational";
            else this.CDTIA = "CDTIA: CDTI operational";

            string NotTCAS = octet.Substring(6, 1);
            if (NotTCAS == "0") this.NotTCAS = "Not TCAS: TCAS operational";
            else this.NotTCAS = "Not TCAS: TCAS not operational";

            string SA = octet.Substring(7, 1);
            if (SA == "0") this.SA = "SA: Antenna Diversity";
            else this.SA = "SA: Single Antenna only";

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
            if (ATP == 0) this.ATP = "ATP: 24-Bit ICAO address";
            else if (ATP == 1) this.ATP = "ATP: Duplicate address";
            else if (ATP == 2) this.ATP = "ATP: Surface vehiCLe address";
            else if (ATP == 3) this.ATP = "ATP: Anonymous address";
            else this.ATP = "ATP: Reserved for future use";

            int ARC = Convert.ToInt32(octetNum1.Substring(4, 2), 2);
            if (ARC == 0) this.ARC = "ARC: 25 ft ";
            else if (ARC == 1) this.ARC = "ARC: 100 ft";
            else if (ARC == 2) this.ARC = "ARC: Unknown";
            else this.ARC = "ARC: Invalid";

            int RC = Convert.ToInt32(octetNum1.Substring(5, 1), 2);
            if (RC == 0) this.RC = "RC: Default";
            else this.RC = "RC: Range Check passed, CPR Validation pending";

            int RAB = Convert.ToInt32(octetNum1.Substring(6, 1), 2);
            if (RAB == 0) this.RAB = "RAB: Report from target transponder";
            else this.RAB = "RAB: Report from field monitor (fixed transponder)";

            int FX1 = Convert.ToInt32(octetNum1.Substring(7, 1), 2);

            if (FX1 == 1)
            {
                newposition = newposition + 1;

                string octetNum1_1 = data[newposition];
                
                int DCR = Convert.ToInt32(octetNum1_1.Substring(0, 1), 2);
                if (DCR == 0) this.DCR = "DCR: No differential correction (ADS-B)";
                else this.DCR = "DCR: Differential correction (ADS-B)";

                int GBS = Convert.ToInt32(octetNum1_1.Substring(1, 1), 2);
                if (GBS == 0) this.GBS = "GBS: Ground Bit not set";
                else this.GBS = "GBS: Ground Bit set";

                int SIM = Convert.ToInt32(octetNum1_1.Substring(2, 1), 2);
                if (SIM == 0) this.SIM = "SIM: Actual target report";
                else this.SIM = "SIM: Simulated target report";

                int TST = Convert.ToInt32(octetNum1_1.Substring(3, 1), 2);
                if (TST == '0') this.TST = "TST: Default";
                else this.TST = "TST: Test Target";

                int SAA = Convert.ToInt32(octetNum1_1.Substring(4, 1), 2);
                if (SAA == '0') this.SAA = "SAA: Equipment capable to provide Selected Altitude";
                else this.SAA = "SAA: Equipment not capable to provide Selected Altitude";

                int CL = Convert.ToInt32(octetNum1_1.Substring(5, 2), 2);
                if (CL == 0) this.CL = "CL: Report valid";
                else if (CL == 1) this.CL = "CL: Report suspect";
                else if (CL == 2) this.CL = "CL: No information";
                else this.CL = "CL: Reserved for future use";
                
                int FX2 = Convert.ToInt32(octetNum1_1.Substring(7, 1), 2);

                if (FX2 == 1)
                {
                    newposition = newposition + 1;

                    string octetNum1_2 = data[newposition];

                    string spareBits = octetNum1_2.Substring(0, 2);

                    int IPC = Convert.ToInt32(octetNum1_2.Substring(2, 1), 2);
                    if (IPC == 0) this.IPC = "IPC: Default";
                    else this.IPC = "IPC: Independent Position Check failed";

                    int NOGO = Convert.ToInt32(octetNum1_2.Substring(3, 1), 2);
                    if (NOGO == 0) this.NOGO = "NOGO: NOGO-bit not set";
                    else this.NOGO = "NOGO: NOGO-bit set";

                    int CPR = Convert.ToInt32(octetNum1_2.Substring(4, 1), 2);
                    if (CPR == 0) this.CPR = "CPR: CPR Validation correct ";
                    else this.CPR = "CPR: CPR Validation failed";
                    
                    int LDPJ = Convert.ToInt32(octetNum1_2.Substring(5, 1), 2);
                    if (LDPJ == 0) this.LDPJ = "LDPJ: LDPJ not detected";
                    else this.LDPJ = "LDPJ: LDPJ detected";

                    int RCF = Convert.ToInt32(octetNum1_2.Substring(6, 1), 2);
                    if (RCF == 0) this.RCF = "RCF: Default";
                    else this.RCF = "RCF: Range Check failed ";

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
        public string SILsupp;
        public string SDA;
        public string GVA;
        public int PIC;
        public string PICsupp;
        public string ICB;
        public string NUCp;
        public string NIC;

        private int QualityIndicators(string[] data, int position)
        {
            int newposition = position;

            string octetNum1 = data[newposition];

            this.NUCr_NACv = "NUCr or NACv: " + Convert.ToString(Convert.ToInt32(octetNum1.Substring(0, 3), 2));
            this.NUCp_NIC = "NUCp or NIC: " + Convert.ToString(Convert.ToInt32(octetNum1.Substring(3, 4), 2));

            string FX1 = octetNum1.Substring(7, 1);

            if (FX1 == "1") // First Extension
            {
                newposition = newposition + 1;

                string octetNum1_1 = data[newposition];

                this.NICbaro = "NICbaro: " + Convert.ToString(Convert.ToInt32(octetNum1_1.Substring(0, 1), 2));
                this.SIL = "SIL: " + Convert.ToString(Convert.ToInt32(octetNum1_1.Substring(1, 2), 2));
                this.NACp = "NACp: " + Convert.ToString(Convert.ToInt32(octetNum1_1.Substring(3, 4), 2));
                
                string FX2 = octetNum1_1.Substring(7, 1);

                if (FX2 == "1") // Second Extension
                {
                    newposition = newposition + 1;

                    string octetNum1_2 = data[newposition];

                    string spareBits = octetNum1_2.Substring(0, 2);

                    string SILsupp = octetNum1_2.Substring(2, 1);
                    if (SILsupp == "0") this.SILsupp = "SIL: Measured per hour";
                    else this.SILsupp = "SIL: Measured per sample";

                    this.SDA = "SDA: " + Convert.ToString(Convert.ToInt32(data[position].Substring(3, 2), 2));
                    this.GVA = "GVA: " + Convert.ToString(Convert.ToInt32(data[position].Substring(5, 2), 2));

                    string FX3 = octetNum1_2.Substring(7, 1);

                    if (FX3 == "1") // Third Extension
                    {
                        newposition = newposition + 1;

                        string octetNum1_3 = data[newposition];

                        this.PIC = Convert.ToInt32(octetNum1_3.Substring(0, 4), 2);
                        this.PICsupp = "PIC: " + this.PIC.ToString();

                        if (this.PIC == 0) this.ICB = "ICB: No integrity(or > 20.0 NM)"; this.NUCp = "NUCp: 0"; this.NIC = "NIC: 0";
                        if (this.PIC == 1) this.ICB = "ICB: < 20.0 NM"; this.NUCp = "NUCp: 1"; this.NIC = "NIC: 1";
                        if (this.PIC == 2) this.ICB = "ICB: < 10.0 NM"; this.NUCp = "NUCp: 2"; this.NIC = "NIC: -";
                        if (this.PIC == 3) this.ICB = "ICB: < 8.0 NM"; this.NUCp = "NUCp: -"; this.NIC = "NIC: 2";
                        if (this.PIC == 4) this.ICB = "ICB: < 4.0 NM"; this.NUCp = "NUCp: -"; this.NIC = "NIC: 3";
                        if (this.PIC == 5) this.ICB = "ICB: < 2.0 NM"; this.NUCp = "NUCp: 3"; this.NIC = "NIC: 4";
                        if (this.PIC == 6) this.ICB = "ICB: < 1.0 NM"; this.NUCp = "NUCp: 4"; this.NIC = "NIC: 5";
                        if (this.PIC == 7) this.ICB = "ICB: < 0.6 NM"; this.NUCp = "NUCp: -"; this.NIC = "NIC: 6 (+ 1/1)";
                        if (this.PIC == 8) this.ICB = "ICB: < 0.5 NM"; this.NUCp = "NUCp: 5"; this.NIC = "NIC: 6 (+ 0/0)";
                        if (this.PIC == 9) this.ICB = "ICB: < 0.3 NM"; this.NUCp = "NUCp: -"; this.NIC = "NIC: 6 (+ 0/1)";
                        if (this.PIC == 10) this.ICB = "ICB: < 0.2 NM"; this.NUCp = "NUCp: 6"; this.NIC = "NIC: 7";
                        if (this.PIC == 11) this.ICB = "ICB: < 0.1 NM"; this.NUCp = "NUCp: 7"; this.NIC = "NIC: 8";
                        if (this.PIC == 12) this.ICB = "ICB: < 0.04 NM"; this.NUCp = "NUCp: -"; this.NIC = "NIC: 9";
                        if (this.PIC == 13) this.ICB = "ICB: < 0.013 NM"; this.NUCp = "NUCp: 8"; this.NIC = "NIC: 10";
                        if (this.PIC == 14) this.ICB = "ICB: < 0.004 NM"; this.NUCp = "NUCp: 9"; this.NIC = "NIC: 11";
                        if (this.PIC == 15) this.ICB = "ICB: Not defined"; this.NUCp = "NUCp: Not defined"; this.NIC = "NIC: Not defined";
                        
                        string spareBits1 = octetNum1_3.Substring(4, 3);

                        string FX4 = Convert.ToString(octetNum1_3[7]);
                    }
                }
            }

            return newposition + 1;
        }

        //DATA ITEM: I021/110
        public int TRAJInfo;
        public bool TIS;
        public bool TID_TI;
        public string NAV;
        public string NVB;
        public int REP_TI;
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
            this.TRAJInfo = 1;
            
            if (data[position].Substring(0, 1) == "1") this.TIS = true;
            else this.TIS = false;
            
            if (data[position].Substring(1, 1) == "1") this.TID_TI = true;
            else this.TID_TI = false;

            if (this.TIS == true)
            {
                position = position + 1;

                if (data[position].Substring(0, 1) == "0") this.NAV = "NAV: Trajectory Intent Data is available for this aircraft";
                else this.NAV = "NAV: Trajectory Intent Data is not available for this aircraft ";

                if (data[position].Substring(1, 1) == "0") this.NVB = "NVB: Trajectory Intent Data is valid";
                else this.NVB = "NVB: Trajectory Intent Data is not valid";
            }

            if (this.TID_TI == true)
            {
                position = position + 1;

                this.REP_TI = Convert.ToInt32(data[position], 2);
                this.TCA = new string[this.REP_TI];
                this.NC = new string[this.REP_TI];
                this.TCP = new int[this.REP_TI];
                this.Altitude = new string[this.REP_TI];
                this.Latitude = new string[this.REP_TI];
                this.Longitude = new string[this.REP_TI];
                this.PointType = new string[this.REP_TI];
                this.TD = new string[this.REP_TI];
                this.TRA = new string[this.REP_TI];
                this.TOA = new string[this.REP_TI];
                this.TOV = new string[this.REP_TI];
                this.TTR = new string[this.REP_TI];
                
                position = position + 1;

                for (int i = 0; i < this.REP_TI; i++)
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
            double latitude = Convert.ToDouble(BinTwosComplementToSignedDecimal(string.Concat(data[position], data[position + 1], data[position + 2]))) * (180 / (Math.Pow(2, 23)));
            position = position + 3;
            double longitude = Convert.ToDouble(BinTwosComplementToSignedDecimal(string.Concat(data[position], data[position + 1], data[position + 2]))) * (180 / (Math.Pow(2, 23)));
            position = position + 3;

            int latitudeDeg = Convert.ToInt32(Math.Truncate(latitude));
            int latitudeMin = Convert.ToInt32(Math.Truncate((latitude - latitudeDeg) * 60));
            double latitudeSec = Math.Round(((latitude - (latitudeDeg +  (Convert.ToDouble(latitudeMin) / 60))) * 3600), 5);
            int longitudeDeg = Convert.ToInt32(Math.Truncate(longitude));
            int longitudeMin = Convert.ToInt32(Math.Truncate((longitude - longitudeDeg) * 60));
            double longitudeSec = Math.Round(((longitude - (longitudeDeg +  (Convert.ToDouble(longitudeMin) / 60))) * 3600), 5);
            
            this.LatitudeinWGS84 = Convert.ToString(latitudeDeg) + "º " + Convert.ToString(latitudeMin) + "' " + Convert.ToString(latitudeSec) + "''";
            this.LongitudeinWGS84 = Convert.ToString(longitudeDeg) + "º " + Convert.ToString(longitudeMin) + "' " + Convert.ToString(longitudeSec) + "''";
            LatitudeMapWGS84 = Convert.ToDouble(latitude);
            LongitudeMapWGS84 = Convert.ToDouble(longitude);
            //Console.WriteLine("LatWGS84: " + this.LatitudeinWGS84);
            //Console.WriteLine("LongWGS84: " + this.LongitudeinWGS84);

            return position;
        }

        //DATA ITEM: I021/131
        public string LatitudeinWGS84HighResolution;
        public string LongitudeinWGS84HighResolution;

        private int PositionInWGS84CoordinatesHighResolution(string[] data, int position)
        {
            double latitude = Convert.ToDouble(BinTwosComplementToSignedDecimal(string.Concat(data[position], data[position + 1], data[position + 2], data[position + 3]))) * (180 / (Math.Pow(2, 30))); 
            position = position + 4;
            double longitude = Convert.ToDouble(BinTwosComplementToSignedDecimal(string.Concat(data[position], data[position + 1], data[position + 2], data[position + 3]))) * (180 / (Math.Pow(2, 30))); 
            position = position + 4;

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

            return position;
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
            string sas = data[position].Substring(0, 1);
            if (sas == "0") this.SAS = "SAS: No source information provided";
            else this.SAS = "SAS: Source information provided";

            string Source = data[position].Substring(1, 2);
            if (Source == "00") this.Source = "Source: Unknown";
            else if (Source == "01") this.Source = "Source: Aircraft Altitude (Holding Altitude)";
            else if (Source == "10") this.Source = "Source: MCP/FCU Selected Altitude";
            else this.Source = "Source: FMS Selected Altitude";
            
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
            if (data[position].Substring(0, 1) == "0") this.MV = "MV: Not active or unknown";
            else this.MV = "MV: Active";
            if (data[position].Substring(1, 1) == "0") this.AH = "AH: Not active or unknown";
            else this.AH = "AH: Active";
            if (data[position].Substring(2, 1) == "0") this.AM = "AM: Not active or unknown";
            else this.AM = "AM: Active";
            
            this.finalStateSelectedAltitude = "Altitude: " + Convert.ToString(Convert.ToDouble(BinTwosComplementToSignedDecimal(string.Concat(data[position], data[position + 1]).Substring(3, 13))) * 25) + " ft";
            
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
                this.GroundSpeed = String.Format("{0:0.00}", (Convert.ToInt32(string.Concat(data[position], data[position + 1]).Substring(1, 15), 2) * Math.Pow(2, -14) * 3600)) +  " kts";
                this.TrackAngle = String.Format("{0:0.00}", Convert.ToInt32(string.Concat(data[position + 2], data[position + 3]).Substring(0, 16),2) * (360 / (Math.Pow(2, 16)))) + " º";
                this.GroundVector = "GS: " + this.GroundSpeed + ", TA: " + String.Format("{0:0.00}", this.TrackAngle);
            }
            else this.GroundVector = "Value exceeds defined rage";

            position = position + 4;
            return position;
        }

        //DATA ITEM: I021/161
        public string trackNumber;

        private int TrackNumber(string[] data, int position)
        {
            this.trackNumber = Convert.ToString(Convert.ToInt32(string.Concat(data[position], data[position + 1]).Substring(4,12), 2)); 
            
            position = position + 2;
            return position;  
        }

        //DATA ITEM: I021/165
        public string trackAngleRate;

        private int TrackAngleRate(string[] data, int position) 
        {
            this.trackAngleRate = Convert.ToString(Convert.ToInt32(string.Concat(data[position], data[position + 1]).Substring(6, 10), 2) * (1 / 32)) + " º/s";
            
            position = position + 2;
            return position;
        }

        //DATA ITEM: I021/170
        public string TargetId;

        private int TargetIdentification(string[] data, int position)
        {
            StringBuilder tarId = new StringBuilder();

            string characters = string.Concat(data[position], data[position + 1], data[position + 2], data[position + 3], data[position + 4], data[position + 5]);
            for (int i = 0; i < 8; i++) tarId.Append(ComputeCharacter(characters.Substring(i * 6, 6)));

            string id = tarId.ToString();

            if (id.Length > 1) { this.TargetId = id; }

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
            if (data[position].Substring(0, 1) == "0") this.ICF = "ICF: No intent change active";
            else this.ICF= "ICF: Intent change flag raised";

            if (data[position].Substring(1, 1) == "0") this.LNAV = "LNAV: LNAV Mode engaged";
            else this.LNAV = "LNAV: LNAV Mode not engaged";

            int PS = Convert.ToInt32(data[position].Substring(3,3), 2);
            if (PS == 0) this.PS = "PS: No emergency / not reported";
            else if (PS == 1) this.PS = "PS: General emergency";
            else if (PS == 2) this.PS = "PS: Lifeguard / medical emergency";
            else if (PS == 3) this.PS = "PS: Minimum fuel";
            else if (PS == 4) this.PS = "PS: No communications";
            else if (PS == 5) this.PS = "PS: UnLength_Widthful interference";
            else this.PS = "PS: 'Downed' Aircraft ";
            
            int SS = Convert.ToInt32(data[position].Substring(6, 2), 2);
            if (SS == 0) this.SS = "SS: No condition reported";
            else if (SS == 1) this.SS = "SS: Permanent Alert (Emergency condition)";
            else if (SS == 2) this.SS = "SS: Temporary Alert (change in Mode 3/A Code other than emergency)";
            else this.SS = "SS: SPI set";
            
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
        public int METInfo;
        public string WindSpeed;
        public string WindDirection;
        public string Temperature;
        public string Turbulence;

        private int MetInformation (string[] data, int position)
        {
            METInfo = 1;
            
            int positionInitial = position;

            if (data[positionInitial].Substring(0, 1) == "1") { this.WindSpeed = "Wind Seed:" + Convert.ToString(Convert.ToInt32(string.Concat(data[position + 1], data[position + 2]), 2)) + " kts"; position = position + 2; }
            if (data[positionInitial].Substring(1, 1) == "1") { this.WindDirection = "Wind Direction: " + Convert.ToString(Convert.ToInt32(string.Concat(data[position + 1], data[position + 2]), 2)) + " º"; position = position + 2; }
            if (data[positionInitial].Substring(2, 1) == "1") { this.Temperature = "Temperature: " + Convert.ToString(Convert.ToInt32(string.Concat(data[position + 1], data[position + 2]), 2) * 0.25) + " ºC"; position = position + 2; }
            if (data[positionInitial].Substring(3, 1) == "1") { this.Turbulence = "Turbulence: " + Convert.ToString(Convert.ToInt32(string.Concat(data[position + 1], data[position + 2]), 2)); position = position + 2; }
            
            return position;
        }

        //DATA ITEM: I021/230
        public string rollAngle;
        private int RollAngle(string[] data, int position)
        {
            this.rollAngle = Convert.ToString(Convert.ToDouble(BinTwosComplementToSignedDecimal(string.Concat(data[position], data[position + 1]))) * 0.01) + " º"; 
            
            position = position + 2;
            return position; 
        }

        //DATA ITEM: I021/250
        public string[] MBData;
        public string[] BDS1;
        public string[] BDS2;
        public int REP_SMB;

        private int ModeSMBData(string[] data, int position)
        {
            this.REP_SMB = Convert.ToInt32(data[position], 2);
            
            if (this.REP_SMB < 0) { this.MBData = new string[this.REP_SMB]; this.BDS1 = new string[this.REP_SMB]; this.BDS2 = new string[this.REP_SMB]; }

            position = position + 1;

            for (int i = 0 ; i < this.REP_SMB ; i++)
            {
                this.MBData[i] = String.Concat(data[position], data[position + 1], data[position + 2], data[position + 3], data[position + 4], data[position + 5], data[position + 6]);
                this.BDS1[1] = data[position + 7].Substring(0, 4);
                this.BDS2[1] = data[position + 7].Substring(4, 4);
                
                position = position + 8;
            }

            return position;
        }

        //DATA ITEM: I021/260
        public string TYP;
        public string STYP;
        public string ARA;
        public string RAC;
        public string RAT;
        public string MTE;
        public string TTI;
        public string TID;

        private int ACASResolutionAdvisoryReport(string[] data, int position)
        {
            string data_message = string.Concat(data[position], data[position + 1], data[position + 2], data[position + 3], data[position + 4], data[position + 5], data[position + 6]);
            
            this.TYP = "TYP: " +  data_message.Substring(0,5);
            this.STYP = "STYP: " + data_message.Substring(5, 3);
            this.ARA = "ARA: " + data_message.Substring(8, 14);
            this.RAC = "RAC: " + data_message.Substring(22, 4);
            this.RAT = "RAT: " + data_message.Substring(26, 1);
            this.MTE = "MTE: " + data_message.Substring(27, 1);
            this.TTI = "TTI: " + data_message.Substring(28, 2);
            this.TID = "TID: " + data_message.Substring(30, 26);
            
            position = position + 7;
            return position;
        }

        //DATA ITEM: I021/271
        public string POA;
        public string CDTIS;
        public string B2Low;
        public string RAS;
        public string IDENT;
        public string Length_Width;

        private int SurfaceCapabilitiesAndCharacteristics (string[] data, int position)
        {
            
            if (data[position].Substring(2, 1) == "0") this.POA = "POA: Position transmitted is not ADS-B position reference point";
            else this.POA = "POA: Position transmitted is the ADS-B position reference point";

            if (data[position].Substring(3, 1) == "0") this.CDTIS = "CDTIS: Cockpit Display of Traffic Information not operational";
            else this.CDTIS = "CDTIS: Cockpit Display of Traffic Information operational";

            if (data[position].Substring(4, 1) == "0") this.B2Low= "B2Low: Class B2 transmit power ≥ 70 Watts";
            else this.B2Low= "B2Low: Class B2 transmit power < 70 Watts";

            if (data[position].Substring(5, 1) == "0") this.RAS = "RAS: Aircraft not receiving ATC-services";
            else this.RAS = "RAS: Aircraft receiving ATC services";
            if (data[position].Substring(6, 1) == "0") this.IDENT = "IDENT: IDENT switch not active";
            else this.IDENT = "IDENT: IDENT switch active";

            if (data[position].Substring(7, 1) == "1") 
            {
                position = position + 1;

                int Length_Width = Convert.ToInt32(data[position].Substring(4, 4), 2) ;

                if (Length_Width == 0) this.Length_Width  = "Lenght < 15  and Width < 11.5";
                if (Length_Width == 1) this.Length_Width = "Lenght < 15  and Width < 23";
                if (Length_Width == 2) this.Length_Width = "Lenght < 25  and Width < 28.5";
                if (Length_Width == 3) this.Length_Width = "Lenght < 25  and Width < 34";
                if (Length_Width == 4) this.Length_Width = "Lenght < 35  and Width < 33";
                if (Length_Width == 5) this.Length_Width = "Lenght < 35  and Width < 38";
                if (Length_Width == 6) this.Length_Width = "Lenght < 45  and Width < 39.5";
                if (Length_Width == 7) this.Length_Width = "Lenght < 45  and Width < 45";
                if (Length_Width == 8) this.Length_Width = "Lenght < 55  and Width < 45";
                if (Length_Width == 9) this.Length_Width = "Lenght < 55  and Width < 52";
                if (Length_Width == 10) this.Length_Width = "Lenght < 65  and Width < 59.5";
                if (Length_Width == 11) this.Length_Width = "Lenght < 65  and Width < 67";
                if (Length_Width == 12) this.Length_Width = "Lenght < 75  and Width < 72.5";
                if (Length_Width == 13) this.Length_Width = "Lenght < 75  and Width < 80";
                if (Length_Width == 14) this.Length_Width = "Lenght < 85  and Width < 80";
                if (Length_Width == 15) this.Length_Width = "Lenght > 85  and Width > 80";
            }

            position = position + 1;
            return position;
        }

        //DATA ITEM: I021/295
        public int DAInfo;
        public string AOS;
        public string TRD;
        public string M3A;
        public string QI;
        public string TI;
        public string MAM;
        public string GH;
        public string FL;
        public string ISA;
        public string FSA;
        public string AS;
        public string TAS;
        public string MH;
        public string BVR;
        public string GVR;
        public string GV;
        public string TAR;
        public string TIDataAge;
        public string TSDataAge;
        public string MET;
        public string ROA;
        public string ARADataAge;
        public string SCC;

        private int DataAges(string[] data, int position)
        {
            this.DAInfo = 1;

            int positionInitial = position;

            if (data[position].Substring(7, 1) == "1")
            {
                position = position + 1;

                if (data[position].Substring(7, 1) == "1")
                {
                    position = position + 1;

                    if (data[position].Substring(7, 1) == "1")
                    {
                        position = position + 1;
                    }
                }
            }

            position = position + 1;

            if (data[positionInitial].Substring(0, 1) == "1") { this.AOS = "AOS: " + (Convert.ToInt32(data[position], 2) * 0.1).ToString("0.00") + " s"; position = position + 1; }
            if (data[positionInitial].Substring(1, 1) == "1") { this.TRD = "TRD: " + (Convert.ToInt32(data[position], 2) * 0.1).ToString("0.00") + " s"; position = position + 1; }
            if (data[positionInitial].Substring(2, 1) == "1") { this.M3A = "M3A: " + (Convert.ToInt32(data[position], 2) * 0.1).ToString("0.00") + " s"; position = position + 1; }
            if (data[positionInitial].Substring(3, 1) == "1") { this.QI = "QI: " + (Convert.ToInt32(data[position], 2) * 0.1).ToString("0.00") + " s"; position = position + 1; }
            if (data[positionInitial].Substring(4, 1) == "1") { this.TI = "TI: " + (Convert.ToInt32(data[position], 2) * 0.1).ToString("0.00") + " s"; position = position + 1; }
            if (data[positionInitial].Substring(5, 1) == "1") { this.MAM = "MAM: " + (Convert.ToInt32(data[position], 2) * 0.1).ToString("0.00") + " s"; position = position + 1; }
            if (data[positionInitial].Substring(6, 1) == "1") { this.GH = "GH: " + (Convert.ToInt32(data[position], 2) * 0.1).ToString("0.00") + " s"; position = position + 1; }

            if (data[positionInitial].Substring(7, 1) == "1")
            {
                if (data[positionInitial + 1].Substring(0, 1) == "1") { this.FL = "FL: " + (Convert.ToInt32(data[position], 2) * 0.1).ToString("0.00") + " s"; position = position + 1; }
                if (data[positionInitial + 1].Substring(1, 1) == "1") { this.ISA = "ISA: " + (Convert.ToInt32(data[position], 2) * 0.1).ToString("0.00") + " s"; position = position + 1; }
                if (data[positionInitial + 1].Substring(2, 1) == "1") { this.FSA = "FSA: " + (Convert.ToInt32(data[position], 2) * 0.1).ToString("0.00") + " s"; position = position + 1; }
                if (data[positionInitial + 1].Substring(3, 1) == "1") { this.AS = "AS: " + (Convert.ToInt32(data[position], 2) * 0.1).ToString("0.00") + " s"; position = position + 1; }
                if (data[positionInitial + 1].Substring(4, 1) == "1") { this.TAS = "TAS: " + (Convert.ToInt32(data[position], 2) * 0.1).ToString("0.00") + " s"; position = position + 1; }
                if (data[positionInitial + 1].Substring(5, 1) == "1") { this.MH = "MH: " + (Convert.ToInt32(data[position], 2) * 0.1).ToString("0.00") + " s"; position = position + 1; }
                if (data[positionInitial + 1].Substring(6, 1) == "1") { this.BVR = "BVR: " + (Convert.ToInt32(data[position], 2) * 0.1).ToString("0.00") + " s"; position = position + 1; }
                
                if (data[positionInitial + 1].Substring(7, 1) == "1")
                {
                    if (data[positionInitial + 2].Substring(0, 1) == "1") { this.GVR = "GVR: " + (Convert.ToInt32(data[position], 2) * 0.1).ToString("0.00") + " s"; position = position + 1; }
                    if (data[positionInitial + 2].Substring(1, 1) == "1") { this.GV = "GV: " + (Convert.ToInt32(data[position], 2) * 0.1).ToString("0.00") + " s"; position = position + 1; }
                    if (data[positionInitial + 2].Substring(2, 1) == "1") { this.TAR = "TAR: " + (Convert.ToInt32(data[position], 2) * 0.1).ToString("0.00") + " s"; position = position + 1; }
                    if (data[positionInitial + 2].Substring(3, 1) == "1") { this.TIDataAge = "TI: " + (Convert.ToInt32(data[position], 2) * 0.1).ToString("0.00") + " s"; position = position + 1; }
                    if (data[positionInitial + 2].Substring(4, 1) == "1") { this.TSDataAge = "TS: " + (Convert.ToInt32(data[position], 2) * 0.1).ToString("0.00") + " s"; position = position + 1; }
                    if (data[positionInitial + 2].Substring(5, 1) == "1") { this.MET = "MET: " + (Convert.ToInt32(data[position], 2) * 0.1).ToString("0.00") + " s"; position = position + 1; }
                    if (data[positionInitial + 2].Substring(6, 1) == "1") { this.ROA = "ROA: " + (Convert.ToInt32(data[position], 2) * 0.1).ToString("0.00") + " s"; position = position + 1; }
                    
                    if (data[positionInitial+2].Substring(7, 1) == "1")
                    {
                        if (data[positionInitial + 3].Substring(0, 1) == "1") { this.ARADataAge = "ARA: " + (Convert.ToInt32(data[position], 2) * 0.1).ToString("0.00") + " s"; position = position + 1; }
                        if (data[positionInitial + 3].Substring(1, 1) == "1") { this.SCC = "SCC: " + (Convert.ToInt32(data[position], 2) * 0.1).ToString("0.00") + " s"; position = position + 1; }
                    }
                }
            }
            
            return position; 
        }

        //DATA ITEM: I021/400
        public string receiverID;

        private int ReceiverID(string[] data, int position) 
        { 
            this.receiverID = Convert.ToString(Convert.ToInt32(data[position],2));
            
            position = position + 1;
            return position;
        }

        #endregion
    }
}
