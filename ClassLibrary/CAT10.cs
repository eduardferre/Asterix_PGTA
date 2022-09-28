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

        public string messageType;
        public string SAC;
        public string SIC;

        public void DecodeCAT10(string[] fspec, int position)
        {
            if (FSPEC[0] == "1") {
                DataSourceIdentifier(data, position);
                position = position + 2;
            } 
            if (FSPEC[1] == "2") { 
                MessageType(data, position);
                position = position + 1;
            } 

        }

        //DATA ITEM: I010/010
        private void DataSourceIdentifier(string[] data, int position)
        {
            SAC = Convert.ToString(Convert.ToInt32(data[position], 2));
            SIC = Convert.ToString(Convert.ToInt32(data[position + 1], 2));
        }

        //DATA ITEM: I010/000
        private void MessageType(string[] data, int position)
        {
            messageType = data[position];
            if (messageType == "00") {
                messageType = "Target Report"; 
            }
            if (messageType == "01") {
                messageType = "Start of Update Cycle"; 
            }
            if (messageType == "10") {
                messageType = "Periodic Status Message"; 
            }
            if (messageType == "11") {
                messageType = "Event-triggered Status Message"; 
            }
        }

        public void UTM2WGS84()
        {
        }

        public void LLA2UTM()
        {
        }
    }
}
