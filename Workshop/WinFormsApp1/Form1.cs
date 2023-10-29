using System.Windows.Forms;

namespace WinFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        async void button1_Click(object sender, EventArgs e)
        {
            //BlockAndNEverDoItAgain(Read);
            var tasks = new List<Task>();
            flowLayoutPanel1.Controls.Clear();
            for (var i = 0; i < 100; i++)
            {
                flowLayoutPanel1.Controls.Add(new PictureBox() { Size = new(200, 100) });
                tasks.Add(Do(i));
            }
            await Task.WhenAll(tasks.ToArray());
            button1.Text = "OK";
        }

        async Task Do(int index)
        {
            var data = await Load($"test{index}");
            var pb = flowLayoutPanel1.Controls[index] as PictureBox;
            pb.Image = Image.FromStream(new MemoryStream(data));
        }

        //static async Task Read()
        //{
        //    await Task.Delay(1000);
        //    throw new NotImplementedException();
        //}
        //static async Task Write()
        //{ }

        //static void BlockAndNEverDoItAgain(Func<Task> t)
        //{
        //    Task.Run(t).Wait();
        //}

        static async ValueTask<byte[]> Load(string text)
        {
            await Task.Delay(Random.Shared.Next(500, 2000));
            var http = new HttpClient();
            using (var response = await http.GetAsync($"https://dummyimage.com/200x100/000/fff&text={text}"))
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
                                return ms.ToArray();
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
    }
}