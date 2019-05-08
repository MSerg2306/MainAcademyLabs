using System;
using System.Threading;

namespace Work24
{
    //Implement class Morse_matrix derived from String_matrix, which realize IMorse_crypt
    class Morse_matrix : String_matrix, IMorse_crypt
    {
        public const int Size_2 = Alphabet.Size;
        private string[,] mor_matrix = new string[Size1, Size_2];
        private int offset_key = 0;

        public Morse_matrix()
        {
            Fd(Alphabet.Dictionary_arr);
        }
        //// indexer
        public string this[int x, int y]
        {
            get { return mor_matrix[x, y]; }
            set { mor_matrix[x, y] = value; }
        }
        //Implement Morse_matrix constructor with the int parameter for offset
        //Use fd(Alphabet.Dictionary_arr) and sd() methods
        public Morse_matrix(int keyCode)
        {
            Fd(Alphabet.Dictionary_arr);
            offset_key = keyCode;
            Sd();
        }
        //Implement Morse_matrix constructor with the string [,] Dict_arr and int parameter for offset
        //Use fd(Dict_arr) and sd() methods
        public Morse_matrix(string[,] matrix, int keyCode)
        {
            Fd(matrix);
            offset_key = keyCode;
            Sd();
        }

        private void Fd(string[,] Dict_arr)
        {
            for (int ii = 0; ii < Size1; ii++)
                for (int jj = 0; jj < Size_2; jj++)
                    this[ii, jj] = Dict_arr[ii, jj];
        }
        private void Sd()
        {
            int off = Size_2 - offset_key;
            for (int jj = 0; jj < off; jj++)
                this[1, jj] = this[1, jj + offset_key];
            for (int jj = off; jj < Size_2; jj++)
                this[1, jj] = this[1, jj - off];
        }
        //Implement Morse_matrix operator +
        public override string Str_clmn()
        {
            string clmn = null;
            for (int j = 0; j < Size2; j++)
            {

                clmn += this[1, j];
            }
            return clmn;
        }
        public override bool Equals(object obj)
        {
            Morse_matrix strob = (Morse_matrix)obj;
            return String.Compare(strob.Str_clmn(), this.Str_clmn()) == 0 ? true : false; ;
        }
        //Realize crypt() with string parameter
        //Use indexers
        public string Crypt(string text)
        {
            string cryptWord = null;
            string[] temp = new string[text.Length];
            for (int j = 0; j < text.Length; j++)
            {
                bool findChar = false;
                for (int i = 0; i < Size_2; i++)
                {
                    if (mor_matrix[0, i] == text.ToCharArray()[j].ToString().ToLower())
                    {
                        temp[j] = mor_matrix[1, i];
                        findChar = true;
                        break;
                    }
                }
                if (findChar != true) temp[j] = " ";
            }
            for (int j = 0; j < text.Length; j++)
            {
                cryptWord += temp[j];
            }
            return cryptWord + " ";
        }
        //Realize decrypt() with string array parameter
        //Use indexers
        public string Decrypt(string[] MorseChar)
        {
            string decryptMorseChar = null;
            string[] temp = new string[MorseChar.Length];
            for (int j = 0; j < MorseChar.Length; j++)
            {
                bool findChar = false;
                for (int i = 0; i < Size_2; i++)
                {
                    if (mor_matrix[1, i] == MorseChar[j])
                    {
                        temp[j] = mor_matrix[0, i];
                        findChar = true;
                        break;
                    }
                }
                if (findChar != true) temp[j] = " ";
            }
            for (int j = 0; j < MorseChar.Length; j++)
            {
                decryptMorseChar += temp[j];
            }
            return decryptMorseChar + " ";
        }
        //Implement Res_beep() method with string parameter to beep the string
        internal void Res_beep(string rslt)
        {
            //Длительность тире равна трём точкам. 
            //Пауза между элементами одного знака — одна точка, 
            //между знаками в слове — 3 точки, 
            //между словами — 7 точек.
            int point = 100;
            int dash = point * 3;
            int intersymbol = point * 3;
            int interword = point * 7;
            int frequency = 3000;

            for (int i = 0; i < rslt.Length; i = i + 5)
            {
                if (rslt.ToCharArray()[i].ToString() != " ")
                {
                    for (int j = i; j <= i + 5; j++)
                    {
                        switch (rslt.ToCharArray()[j].ToString())
                        {
                            case ".":
                                {
                                    Console.Beep(frequency, point);
                                    Thread.Sleep(point);
                                    break;
                                }
                            case "-":
                                {
                                    Console.Beep(frequency, dash);
                                    Thread.Sleep(point);
                                    break;
                                }
                            default:
                                {
                                    break;
                                }
                        }
                    }
                    Thread.Sleep(intersymbol);
                }
                else
                {
                    Thread.Sleep(interword);
                    i = i + 1;
                }
            }
        }
    }
}

