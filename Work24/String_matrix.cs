using System;

namespace Work24
{
    abstract class String_matrix
    {
        public const int Size1 = 2;
        public const int Size2 = Alphabet.Size;
        private string[,] str_matrix = new string[Size1, Size2];
        // indexer
        public string this[int x, int y]
        {
            get { return str_matrix[x, y]; }
            set { str_matrix[x, y] = value; }
        }

        public void Write_matrix()
        {
            Console.WriteLine();
            for (int i = 0; i < Size2; i++)
            {
                Console.Write(" ");                
                for (int j = 0; j < Size1; j++)
                {                    
                    Console.Write(" " + this[j, i] );
                }
                Console.WriteLine(" ");
            }
            Console.WriteLine();
        }
        public virtual string Str_clmn()
        {
            string clmn = null;
            for (int j = 0; j < Size2; j++)
            {
                
                clmn += this[1, j];               
            }
            return clmn;
        }
        public static bool operator >=(String_matrix tbl1, String_matrix tbl2)
        {
            bool res_str_matrix = String.Compare(tbl1.Str_clmn(), tbl2.Str_clmn()) >= 0 ? true : false;
            return res_str_matrix;
        }
        public static bool operator <=(String_matrix tbl1, String_matrix tbl2)
        {
            bool res_str_matrix = String.Compare(tbl1.Str_clmn(), tbl2.Str_clmn()) <= 0 ? true : false;
            return res_str_matrix;
        }
        public static bool operator >(String_matrix tbl1, String_matrix tbl2)
        {
            bool res_str_matrix = String.Compare(tbl1.Str_clmn(), tbl2.Str_clmn()) > 0 ? true : false;
            return res_str_matrix;
        }
        public static bool operator <(String_matrix tbl1, String_matrix tbl2)
        {
            bool res_str_matrix = String.Compare(tbl1.Str_clmn(), tbl2.Str_clmn()) < 0 ? true : false;
            return res_str_matrix;
        }
        public override bool Equals(object obj)
        {
            String_matrix strob = (String_matrix)obj;
            return String.Compare(strob.Str_clmn(), this.Str_clmn()) == 0 ? true : false;;
        }
        public override int GetHashCode()
        {
           return this.Str_clmn().GetHashCode();
        }
    }
}
