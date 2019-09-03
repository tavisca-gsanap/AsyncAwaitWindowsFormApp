using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Hi");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Debug.WriteLine("First is started");
            int a = DoFirst(10);
            Debug.WriteLine("First is done "+a);
        }

        private async void button3_Click(object sender, EventArgs e)
        {
            Debug.WriteLine("First is started");
            int a = await DoFirstAsync(10);
            Debug.WriteLine("First is done " + a);
        }
        
        private static int DoFirst(int v)
        {
            Thread.Sleep(v * 1000);
            return v;
        }

        private static async Task<int> DoFirstAsync(int v)
        {
            return await Task.Run(() =>
            {
                return DoFirst(v);
            });
        }
        
        private async void button4_Click(object sender, EventArgs e)
        {
            Console.WriteLine("First is started");
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            var a = await DoSecondAsync(10);
            var sec = stopwatch.ElapsedMilliseconds;
            stopwatch.Stop();
            Debug.WriteLine("First is done "+ sec +" millisec");
        }

        private static async Task<int[]> DoSecondAsync(int v)
        {
            List<Task<int>> tasks = new List<Task<int>>();
            for(var i = 0; i < 3; i++)
            {
                tasks.Add(Task.Run(() =>
                {
                    return DoFirst(v);
                }));
            }
            var result = await Task.WhenAll(tasks);
            return result;
        }
    }
}
