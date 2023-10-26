using System.Diagnostics;
using System.Net;

namespace Workshop
{
    internal class Program
    {
        //static Task Main()
        //{
        //    Console.WriteLine();
        //    return Task.CompletedTask.ContinueWith(_ =>
        //    {
        //        Console.WriteLine();
        //        Task.CompletedTask.ContinueWith(_ =>
        //        {
        //            Console.WriteLine();
        //        });
        //    });
        //}

        static void await(object o) { }

        static async Task Main(string[] args)
        {
            #region 
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
            #endregion

            var request = WebRequest.CreateHttp("https://www.tabsoverspaces.com");
            using (var response = await request.GetResponseAsync())
            {
                using (var ms = new MemoryStream((int)response.ContentLength))
                {
                    using (var stream = response.GetResponseStream())
                    {
                        var buffer = new byte[4096];
                        while (true)
                        {
                            try
                            {
                                var read = await stream.ReadAsync(buffer, 0, buffer.Length);
                                if (read == 0)
                                    break;
                                await ms.WriteAsync(buffer, 0, read);
                            }
                            catch (IOException)
                            {
                                Console.WriteLine("ERROR");
                            }
                        }
                    }
                }
            }
            Console.WriteLine("HERE");

            Console.ReadLine();
        }

        static void ReadStream(Stream source, Stream destination, Action onDone)
        {
            var buffer = new byte[4096];
            source.BeginRead(buffer, 0, buffer.Length, ar =>
            {
                int read;
                try
                {
                    read = source.EndRead(ar);
                }
                catch (IOException)
                {
                    return;
                }
                if (read == 0)
                {
                    onDone();
                    return;
                }
                destination.BeginWrite(buffer, 0, read, ar2 =>
                {
                    try
                    {
                        destination.EndWrite(ar2);
                    }
                    catch (IOException)
                    {
                        return;
                    }
                    ReadStream(source, destination, onDone);
                }, null);
            }, null);
        }

        //static int Sum(int a, int b)
        //{
        //    Thread.SpinWait(5*99999);
        //    return a + b;
        //}
    }
}