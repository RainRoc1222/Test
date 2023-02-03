using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Mvi.Wpf
{
    public class SerialPortController : INotifyPropertyChanged
    {
        public int Count { get; set; } = 0;
        public double TotalTime { get; set; }
        public Stopwatch Stopwatch { get; set; }

        private List<byte> myTempData;
        public SerialPortCommNode SerialPortCommNode { get; private set; }

        public event EventHandler<byte[]> ReceiveData;

        public event PropertyChangedEventHandler PropertyChanged;

        public SerialPortController()
        {
            Stopwatch = new Stopwatch();
            myTempData = new List<byte>();
            SerialPortCommNode = new SerialPortCommNode
            {
                PortName = "COM10",
                BaudRate = 9600,
                Parity = Parity.None,
                DataBits = 8,
                StopBits = StopBits.One,
                ReadDataTimeout = 1000,
            };

            SerialPortCommNode.Connect();
            Read();
        }


        private Task Read()
        {
            return Task.Run(() =>
            {
                while (true)
                {
                    var readBytes = SerialPortCommNode.SerialPort.Read();

                    if (readBytes != null)
                    {
                        myTempData.AddRange(readBytes);

                        Stopwatch.Restart();
                        CheckDataAsync();
                        Stopwatch.Stop();

                        TotalTime += Stopwatch.Elapsed.TotalMilliseconds;
                        Count++;
                        Console.WriteLine((Math.Round(TotalTime / Count, 3)));
                    }

                    Task.Delay(10).Wait();
                }
            });
        }

        private Task CheckDataAsync()
        {
            return Task.Run(() =>
            {
                if (myTempData.Count > 0)
                {
                    var first = myTempData.First();

                    if (first == 2)
                    {
                        if (myTempData.IndexOf(3) != -1)
                        {
                            var lastIndex = myTempData.LastIndexOf(myTempData.Last());
                            var etxIndex = myTempData.IndexOf(3);
                            var removeDataCount = lastIndex - etxIndex + 1;

                            myTempData.RemoveRange(etxIndex, removeDataCount);

                            var stxIndex = myTempData.LastIndexOf(first);
                            myTempData.RemoveRange(0, stxIndex);

                            ReceiveData?.Invoke(this, myTempData.ToArray());
                            myTempData.Clear();
                        }
                    }
                    else if (myTempData.IndexOf(2) != -1)
                    {
                        myTempData.RemoveRange(0, myTempData.IndexOf(2));
                    }
                }
            });
        }


        private void Write(byte[] sendData)
        {
            SerialPortCommNode.SerialPort.Write(sendData);
        }
        public void Print()
        {
            Write(new byte[] { 0x50, 0x52, 0x49, 0x4E, 0x54, 0x0D, 0x0A });
        }
    }
}
