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
        
        public void DecodeCAT10(string[] msgCAT10)
        {
        }

        public void UTM2WGS84()
        {
        }

        public void LLA2UTM()
        {
        }
    }
}
