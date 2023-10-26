using System.Diagnostics;

namespace Workshop
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //var t = Task.Run(() =>
            //{
            //    Console.WriteLine("Test");
            //    return 10;
            //});
            ////Task.Factory.StartNew(() => { }, )

            var arr = Enumerable.Range(0, 1024).ToArray();
            var sw = Stopwatch.StartNew();
            for (int i = 0; i < arr.Length; i++)
            {
                arr[i] = Sum(arr[i], arr[i]);
            }
            Console.WriteLine(sw.Elapsed);

            Console.ReadLine();
        }

        static int Sum(int a, int b)
        {
            Thread.SpinWait(5*99999);
            return a + b;
        }
    }
}