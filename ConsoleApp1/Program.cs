using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using ClassLibrary;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            byte[] fileBytes = File.ReadAllBytes("C:\\Users\\Eduard\\Desktop\\Asterix_PGTA\\AsterixDecoder\\201002-lebl-080001_smr.ast");
            List<byte[]> listBytes = new List<byte[]>();

            int cont = fileBytes[1] * 256 + fileBytes[2];
            int i = 0;

            while(i < fileBytes.Length)
            {
                byte[] array = new byte[cont];

                for(int j = 0; j < array.Length; j++)
                {
                    array[j] = fileBytes[i];
                    i++;
                }

                listBytes.Add(array);
                
                if(i + 2 < fileBytes.Length) cont = fileBytes[i + 1] * 256 + fileBytes[i + 2];
            }

            List<string[]> listHex = new List<string[]>();

            for (i = 0; i < listBytes.Count; i++)
            {
                byte[] byteSelect = listBytes[i];
                string[] arrayHex = new string[byteSelect.Length];

                for(int j = 0; j < byteSelect.Length; j++)
                {
                    arrayHex[j] = byteSelect[j].ToString("X");

                    if (arrayHex[j].Length <= 1) arrayHex[j] = string.Concat('0', arrayHex[j]);
                }

                listHex.Add(arrayHex);
            }

            //only for the first message, let's see if it works

            string[] arrayMsg = listHex[0];
            int CAT = int.Parse(arrayMsg[0], System.Globalization.NumberStyles.HexNumber);
            int lenght = int.Parse(arrayMsg[1], System.Globalization.NumberStyles.HexNumber) * 256 + int.Parse(arrayMsg[2], System.Globalization.NumberStyles.HexNumber);

            CAT10 cat10 = new CAT10();

            Console.WriteLine("CAT" + CAT + ", lenght: " + lenght);

            cat10.DecodeCAT10(arrayMsg, 0);
        }

        

        static void print(string texto)
        {
            Console.WriteLine(texto);
        }
    }
}
