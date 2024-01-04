using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace ConsoleApplication1
{
    class Program
    {
        static void checkZadnjaCifra(int[] cifre, int p)//Ne prepoznaje kraj
        {
            int n = cifre.Length;
            if ((cifre[p] >= n) || (cifre[p] >= 3 && p != 0))//ZLATNO PRAVILO
            {
                if (p == 0) { cifre[p] = 0; return; }
                cifre[p] = 0;
                cifre[p - 1] += 1;
                checkZadnjaCifra(cifre, p - 1);
            }
        }
        static int add(int[] cifre, string sufix)//1 dobro, -1 napravio krug (00000...00)
        {
            int l = cifre.Length;
            cifre[l - 1] += 1;
            checkZadnjaCifra(cifre, l - 1);
            if (cifre[0] == 0) { return -1; }
            else
            {
                if (cifre.Sum() != l + sufix.Length) { return add(cifre, sufix); }//2 pravilo
                return 1;
            }
        }
        //
        //
        //
        static void BROJEVI(int a, int b, StreamWriter izlaz)//POCETAK
        {
            for (int n = a; n <= b; n++)//1 pravilo
            {
                Console.WriteLine(n + "-ti brojevi:");
                ntiBrojevi(n, izlaz);
            }
        }
        static void ntiBrojevi(int n, StreamWriter izlaz)
        {
            string sufix = "";
            int l = n;
            if (n >= 5) { sufix = "00"; l = n - 2; }//4 pravilo
            else if (n == 9 || n == 10) { sufix = "000"; l = n - 3; }//FASTER SPEED
            else { sufix = "0"; l = n - 1; }//3 pravilo
            int[] cifre = new int[l];
            for (int a = 0; a < l; a++) { cifre[a] = 0; }
            cifre[0] = 1;//5 pravilo
            if (n == 9) { cifre[0] = 5; }
            if (n == 10) { cifre[0] = 6; }
            //
            while (true)
            {
                if (add(cifre, sufix) == -1) { break; }
                if (autobiografskichecker((String.Join("", cifre) + sufix).ToString()))
                {
                    string U = (String.Join("", cifre) + sufix).ToString();
                    if (n == 9 || n == 10) { Console.WriteLine(U); izlaz.WriteLine(U); return; }
                    Console.WriteLine(U); izlaz.WriteLine(U);
                }
            }
        }
        static bool autobiografskichecker(string U)
        {
            char[] a = U.ToCharArray();
            int[] test = new int[a.Length];
            for (int p = 0; p < a.Length; p++) { test[p] = int.Parse(a[p].ToString()); }
            for (int p = 0; p < a.Length; p++)
            {
                if (!brojac(test, test[p], p)) { return false; }
            }
            return true;
        }
        static bool brojac(int[] cifre, int kol, int brojTrazi)
        {
            int E = kol;
            for (int p = 0; p < cifre.Length; p++)
            {
                if (E == -1) { return false; }
                if (cifre[p] == brojTrazi) { E--; }
            }
            if (E != 0) { return false; }
            return true;
        }
        static void Main(string[] args)
        {
            Console.WriteLine("(Ovaj kod izračunava/nalazi sve autobiografske brojeve)");
            Console.WriteLine("BROJEVI:");
            Console.WriteLine();
            using (StreamWriter izlaz = new StreamWriter(@"brojevi.txt"))
            {
                BROJEVI(4, 8, izlaz);
                BROJEVI(9, 9, izlaz);
                BROJEVI(10, 10, izlaz);
            }
            Console.WriteLine();
            Console.WriteLine("BROJEVI USPEŠNO NAĐENI I ZAPISANI!");
            Console.ReadLine();
        }
    }
}
