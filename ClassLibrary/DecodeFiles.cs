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
        public int numberMsgs = 0;
        public int numberCAT10Msgs = 0;
        public string process;

        List<CAT10> listCAT10 = new List<CAT10>();

        public List<CAT10> Read()
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
                    CAT10 cat10 = new CAT10();

                    cat10.msgNum = numberMsgs;
                    cat10.msgCAT10Num = numberCAT10Msgs;

                    numberMsgs++;
                    numberCAT10Msgs++;

                    //Console.WriteLine("CAT" + CAT + ", lenght: " + lenght);

                    cat10.DecodeCAT10(arrayMsg, 0);

                    listCAT10.Add(cat10);
                }
            }

            //process = "DONE";
            //print(process);

            return listCAT10;
        }

        public void print(string texto)
        {
            Console.WriteLine(texto);
        }
    }
}
