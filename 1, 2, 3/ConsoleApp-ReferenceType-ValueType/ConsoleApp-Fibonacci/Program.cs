using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp_Fibonacci
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int a = 0; //bilangan fibonacci pertama
            int b = 1; //bilangan fibonaci kedua

            //var bertipe integer utk simpan nilai bilangan genap
            int jumlahBilanganGenap = 0;

            //tampilkan fibonaci pertama
            Console.WriteLine(a);

            //looping utk menentukan fibonanci berikut nya setelah yang pertama menggunakan while
            while (b < 4000000)
            {
                Console.WriteLine(b); //tampilkan fibonanci berikut nya berdasarkan urutan sampai kurang dari 4 juta

                //kondisi utk mengetahui apakah bilangan fibonanci saat ini adalah genap?
                if (b % 2 == 0)
                {
                    //jika iya maka simpan dan tambahkan ke var
                    jumlahBilanganGenap += b;
                }

                int temp = a + b; //logic perhitungan utk menentukan bilangan fibo berikutnya
                a = b;
                b = temp;
            }

            Console.WriteLine($"\nJumlah bilangan genap yang ada pada bilangan fibonacci 0 s/d 4jt : {jumlahBilanganGenap}");
            Console.ReadLine();
        }
    }
}