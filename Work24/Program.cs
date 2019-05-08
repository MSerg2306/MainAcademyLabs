using System;

namespace Work24
{
    class Program
    {
        static void Main(string[] args)
        {
            int a;
            try
            {
                do
                {
                    Console.WriteLine(@"Please,  type the number:
                        1.  To Morse code
                        2.  From Morse code
                        3.  To/from Morse code with key
                        4.  Compare codes
                        ");
                    try
                    {
                        a = int.Parse(Console.ReadLine());
                        switch (a)
                        {
                            case 1:
                                Str_matr_crypt();
                                Console.WriteLine("");
                                break;
                            case 2:
                                Str_matr_decrypt();
                                Console.WriteLine("");
                                break;
                            case 3:
                                Str_matr_crypt_key();
                                Console.WriteLine("");
                                break;
                            case 4:
                                Str_matr_comp();
                                Console.WriteLine("");
                                break;

                            default:
                                Console.WriteLine("Exit");
                                break;
                        }

                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Error");
                    }

                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Press Spacebar to exit; press any key to continue");
                    Console.ForegroundColor = ConsoleColor.White;
                }
                while (Console.ReadKey().Key != ConsoleKey.Spacebar);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }
        #region Str_matrix
        static void Str_matr_crypt()
        {
            Console.WriteLine("Beep sos");
            string word = "sos Vasya balbes";
            Morse_matrix code_tbl = new Morse_matrix();
            code_tbl.Write_matrix();
            string rslt = code_tbl.Crypt(word);
            Console.WriteLine("sos: " + rslt);
            code_tbl.Res_beep(rslt);
        }
        static void Str_matr_decrypt()
        {
            string word;
            Morse_matrix code_tbl = new Morse_matrix();
            code_tbl.Write_matrix();
            Console.WriteLine("From beep to sos");
            string[] sos = { "...  ", "---  ", "...  ", " ", "...  ", "---  ", "...  " };
            word = code_tbl.Decrypt(sos);
            Console.WriteLine("sos decrypt: " + word);
        }
        static void Str_matr_crypt_key()
        {
            Console.WriteLine("Beep sos");
            string word = "sos";
            Console.WriteLine("Please, write key from 1 to 35 :");
            int b = int.Parse(Console.ReadLine());
            Morse_matrix code_tbl = new Morse_matrix(Alphabet.Dictionary_arr, b);
            code_tbl.Write_matrix();
            string rslt = code_tbl.Crypt(word);
            Console.WriteLine("sos : " + rslt);
            code_tbl.Res_beep(rslt);

            Console.WriteLine("From beep to sos");
            string[] sos = { rslt.Substring(0, 5), rslt.Substring(5, 5), rslt.Substring(10, 5) };
            word = code_tbl.Decrypt(sos);
            Console.WriteLine("sos decrypt   :" + word);
        }
        static void Str_matr_comp()
        {
            Console.WriteLine("Compare codes");

            Console.WriteLine("Please, write key from 1 to 35 :");
            int b = int.Parse(Console.ReadLine());
            Morse_matrix code_tbl1 = new Morse_matrix(b);
            Console.WriteLine("Please, write key from 1 to 35 :");
            int d = int.Parse(Console.ReadLine());
            Morse_matrix code_tbl2 = new Morse_matrix(Alphabet.Dictionary_arr, d);
            Console.WriteLine("Code 1 ");
            code_tbl1.Write_matrix();
            Console.WriteLine("Code 2 ");
            code_tbl2.Write_matrix();

            if (code_tbl1.Equals(code_tbl2))
                Console.WriteLine("True");
            else Console.WriteLine("False");
        }
        #endregion
    }
}