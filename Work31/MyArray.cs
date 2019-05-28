using System;

namespace Work31
{
    class MyArray
    {
        int[] arr;

        public void Assign(int[] arr, int size)
        {
            // 5) add block try (outside of existing block try) try
            // 5) добавить блок try (вне существующего блока try) try
            try
            {
                try
                {
                    this.arr = new int[size];
                    // 1) assign some value to cell of array arr, which index is out of range
                    for (int i = 0; i < arr.Length; i++)
                        this.arr[i] = arr[i] / arr[i + 1];
                }
                //NullReferenceException
                catch (NullReferenceException e)
                {
                    throw new NullReferenceException("Null Reference Exception.", e);
                }
                // 2) catch exception index out of rage
                catch (IndexOutOfRangeException e)
                {
                    Console.WriteLine(e.Message);
                    // Set IndexOutOfRangeException to the new exception's InnerException.
                    //throw new IndexOutOfRangeException("Index Out Of RangeException.", e);
                }
                catch (DivideByZeroException e)
                {
                    throw new DivideByZeroException("Divide By Zero Exception.", e);
                }
                finally
                {
                    // 7) use unchecked to assign result of operation 1000000000 * 100 
                    // to last cell of array
                    this.arr[size-1] = unchecked(1000000000 * 10);
                    Console.WriteLine("The value of the last element of 1000000000 * 100 " + this.arr[size - 1]);
                }
            }
            // 4) catch devision by 0 exception catch
            catch (DivideByZeroException e)
            {
                // output message 
                Console.WriteLine(e.Message);
                //throw new DivideByZeroException("Divide By Zero Exception.", e);
            }
            // 6) add catch block for null reference exception of outside block try  
            // change the code to execute this block (any method of any class) catch 
            catch (NullReferenceException e)
            {
                Console.WriteLine(e.Message);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
            finally
            {
                Console.WriteLine("Output Array:");
                for (int i = 0; i < 4; i++)
                    Console.WriteLine($"{this.arr[i]}");
            }
        }
    }
}