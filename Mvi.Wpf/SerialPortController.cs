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

        private List<byte> myTempData;
        public SerialPortCommNode SerialPortCommNode { get; private set; }

        public event EventHandler<byte[]> ReceiveData;

        public event PropertyChangedEventHandler PropertyChanged;

        public SerialPortController()
        {
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
                        CheckDataAsync();
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
                            FilterData(myTempData.LastIndexOf(first) + 1);
                        }
                    }
                    else
                    {
                        if (myTempData.IndexOf(2) != -1)
                        {
                            FilterData(myTempData.IndexOf(2) + 1);
                        }
                    }
                }
            });
        }

        private void FilterData(int count)
        {
            myTempData.RemoveRange(0, count);

            var lastIndex = myTempData.LastIndexOf(myTempData.Last());
            myTempData.RemoveRange(myTempData.IndexOf(3), lastIndex - myTempData.IndexOf(3) + 1);

            ReceiveData?.Invoke(this, myTempData.ToArray());
            myTempData.Clear();
        }


        private void Write(byte[] sendData)
        {
            SerialPortCommNode.SerialPort.Write(sendData);
        }
    }
}
