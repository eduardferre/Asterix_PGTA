using System;
using ClassLibrary;

namespace ConsoleApp1
{
    class Program
    {
      
        static void Main(string[] args)
        {
            string[] hola1 = { "11111110", "10010001", "00011000", "01010101" };
            string[] hola;
            hola =  ClassLibrary.CAT10.BinTwosComplementToSignedDecimal(hola1);
            Console.WriteLine(hola[0]);
            Console.WriteLine(hola[1]);
            Console.WriteLine(hola[2]);
            Console.WriteLine(hola[3]);


        }
    }
}
