using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Work31
{
    class Program
    {
        static void Main(string[] args)
        {
            CatchExceptionClass cec = new CatchExceptionClass();

            int[] arr1 = new int[4] { 1, 4, 8, 5 };
            cec.CatchExceptionMethod(arr1);
            // 3) replace second elevent of array by 0
            int[] arr2 = new int[4] { 1, 0, 8, 5 };
            cec.CatchExceptionMethod(arr2);
            // 11) Make some unhandled exception and study Visual Studio debugger report – 
            // read description and find the reason of exception
            int[] arr3 = new int[3];
            cec.CatchExceptionMethod(arr3);
        }
    }
}
