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

        public void ClearStoredData()
        {
            numFiles = 0;
            numMsgs = 0;
            numCAT10Msgs = 0;
            numCAT21Msgs = 0;
            nameFiles.Clear();
            listCAT10.Clear();
            listCAT21.Clear();
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
                    }
                    else if (CAT == 21)
                    {
                        CAT21 cat21 = new CAT21(arrayMsg, 0);

                        cat21.msgNum = numMsgs;
                        cat21.msgCAT21Num = numCAT10Msgs;

                        numMsgs++;
                        numCAT21Msgs++;

                        listCAT21.Add(cat21);
                    }
                }


                return 1;
            }
            catch
            {
                return 0;
            }
        }

        public void print(string texto)
        {
            Console.WriteLine(texto);
        }
    }
}
