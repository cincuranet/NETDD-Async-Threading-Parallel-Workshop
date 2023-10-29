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
            //var t = Task.Run(() =>
            //{
            //    Console.WriteLine("Test");
            //    return 10;
            //});
            //Task.Factory.StartNew(() => { }, )

            using (var cts = new CancellationTokenSource())
            {
                cts.Cancel();
                var arr = Enumerable.Range(0, 1024).ToArray();
                var sw = Stopwatch.StartNew();
                var tasks = new List<Task>();
                for (int i = 0; i < arr.Length; i++)
                {
                    var local = i;
                    tasks.Add(Task.Run(() =>
                    {
                        arr[local] = Sum(arr[local], arr[local], cts.Token);
                    }, cts.Token));
                }
                Task.WaitAll(tasks.ToArray());
                Console.WriteLine(sw.Elapsed);
            }

#if false
            var http = new HttpClient();
            using (var response = await http.GetAsync("https://www.tabsoverspaces.com"))
            {
                using (var ms = new MemoryStream())
                using (var stream = response.Content.ReadAsStream())
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
#endif

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

        static int Sum(int a, int b, CancellationToken cancellationToken)
        {
            for (int i = 0; i < 6; i++)
            {
                cancellationToken.ThrowIfCancellationRequested();
                Thread.SpinWait(99999);
            }
            return a + b;
        }


        static int test;

        static void Test()
        {
            var local = Volatile.Read(ref test);
            Interlocked.CompareExchange(ref test, );
        }

        class SimpleLock
        {
            int _lockTaken;

            public void Enter()
            {
                while (Interlocked.Exchange(ref _lockTaken, 1) == 1)
                {
                }
            }

            public void Exit()
            {
                Volatile.Write(ref _lockTaken, 0);
            }
        }
    }
}