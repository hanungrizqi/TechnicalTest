using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp_Palindromik
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //var untuk menyimpan hasil. default/nilai awal 0
            int hasil = 0;

            //var untuk nilai awal & nilai pengali. default bebas bisa 0 atau null
            int nilaiAwal = 0;
            int nilaiPengali = 0;

            //looping untuk bilangan 3 digit, saya set dari yang terbesar 999 sbg nilai awal jadi i--.
            //bisa juga dari bilangan 3 digit terkecil dimulai dari 100 dan i++
            for (int i = 999; i >= 100; i--)
            {
                for (int j = i; j >= 100; j--) //j juga bebas bisa dumulai dari terkecil atau terbesar. jika dimulai dr terkecil maka menggunakan j--, jika dimulai dr terbesar maka menjadi j++
                {
                    int xyz = i * j; //untuk mengalikan looping pertama dan kedua

                    //panggil fungsi IsPalindrome untuk mengecek apakah hasil perkalian 2 bilangan tsb adalah bilangan palindrome?
                    if (IsPalindrome(xyz))
                    {
                        //perbandingan apakah hasil pengecekan IsPalindrome lebih besar dari var hasil saat ini. jika lebih besar akan memperbarui var hasil tersebut sampai xyz tidak ada yg lebih besar dari var hasil.
                        if (xyz > hasil)
                        {
                            hasil = xyz;
                            nilaiAwal = i;
                            nilaiPengali = j;
                        }
                    }
                }
            }

            Console.WriteLine("Palindrom terbesar yang dihasilkan dari perkalian dua bilangan 3 digit adalah:");
            Console.WriteLine($"{nilaiAwal} x {nilaiPengali} = {hasil}");

            Console.ReadLine();
        }

        //fungsi utk cek apakah dia bilangan pailndrome
        static bool IsPalindrome(int number)
        {
            //konversi ke string utk diolah menjadi array. sbg bilangan asli
            string str = number.ToString();

            char[] charArray = str.ToCharArray(); //jadikan array
            Array.Reverse(charArray); //membalik bilangan dari yg belakang menjadi depan
            string reversedStr = new string(charArray); //jadikan char lagi. sbg bilangan hasil

            return str == reversedStr; //utk pengecekan apakah bilangan asli dg bilangan hasil adalah sama?. sekaligus di return. hasil nya true/false
        }
    }
}