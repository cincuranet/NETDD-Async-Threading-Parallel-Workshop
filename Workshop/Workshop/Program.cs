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

            //var arr = Enumerable.Range(0, 1024).ToArray();
            //var sw = Stopwatch.StartNew();
            //var tasks = new List<Task>();
            //for (int i = 0; i < arr.Length; i++)
            //{
            //    var local = i;
            //    tasks.Add(Task.Run(() =>
            //    {
            //        arr[local] = Sum(arr[local], arr[local]);
            //    }));
            //}
            //Task.WaitAll(tasks.ToArray());
            //Console.WriteLine(sw.Elapsed);

            Console.ReadLine();
        }

        //static int Sum(int a, int b)
        //{
        //    Thread.SpinWait(5*99999);
        //    return a + b;
        //}
    }
}