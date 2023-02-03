using GlueNet.Communication.Core;
using GodSharp.SerialPort;
using System;
using System.Diagnostics;
using System.IO.Ports;
using System.Threading.Tasks;

namespace Mvi.Wpf
{
    public class SerialPortCommNode : ICommNode
    {
        private SerialConfigOptions mySerialConfigOptions;

        public GodSerialPort SerialPort { get; set; }

        public string PortName
        {
            get
            {
                return mySerialConfigOptions.PortName;
            }
            set
            {
                mySerialConfigOptions.PortName = value;
            }
        }

        public int BaudRate
        {
            get
            {
                return mySerialConfigOptions.BaudRate;
            }
            set
            {
                mySerialConfigOptions.BaudRate = value;
            }
        }

        public int DataBits
        {
            get
            {
                return mySerialConfigOptions.DataBits;
            }
            set
            {
                mySerialConfigOptions.DataBits = value;
            }
        }

        public StopBits StopBits
        {
            get
            {
                return mySerialConfigOptions.StopBits;
            }
            set
            {
                mySerialConfigOptions.StopBits = value;
            }
        }

        public Parity Parity
        {
            get
            {
                return mySerialConfigOptions.Parity;
            }
            set
            {
                mySerialConfigOptions.Parity = value;
            }
        }

        public Handshake Handshake
        {
            get
            {
                return mySerialConfigOptions.Handshake;
            }
            set
            {
                mySerialConfigOptions.Handshake = value;
            }
        }

        public bool IsConnected => SerialPort?.IsOpen ?? false;

        public long ReadDataTimeout { get; set; }

        public event EventHandler<byte[]> OnSentMessage;

        public event EventHandler<byte[]> OnReceivedMessage;

        public event EventHandler<byte[]> OnReceivedRequest;

        public event EventHandler<byte[]> OnReceivedResponse;

        public event EventHandler<byte[]> OnDataSent;

        public event EventHandler<byte[]> OnDataReceived;

        public SerialPortCommNode()
        {
            mySerialConfigOptions = new SerialConfigOptions();
        }

        public void Connect()
        {
            if (SerialPort == null)
            {
                OpenComm();
            }
            else if (SerialPort.IsOpen)
            {
                Console.WriteLine("Already connected.");
            }
            else
            {
                OpenComm();
            }
        }

        private void OpenComm()
        {
            SerialPort = new GodSerialPort(mySerialConfigOptions);
            SerialPort.Open();
            if (this.OnDataReceived != null)
            {
                SerialPort.UseDataReceived(flag: true, DataReceivedDelegate);
            }
        }

        public void Disconnect()
        {
            SerialPort.Close();
        }

        public void SendMessage(byte[] messageData)
        {
            SerialPort.Write(messageData, 0, messageData.Length);
            this.OnDataSent?.Invoke(this, messageData);
        }

        public byte[] SendRequest(byte[] requestData)
        {
            SerialPort.UseDataReceived(flag: true, null);
            SerialPort.Write(requestData);
            this.OnDataSent?.Invoke(this, requestData);
            Task.Delay(50).Wait();
            byte[] array = ReadWithTimeout();
            this.OnDataReceived?.Invoke(this, array);
            SerialPort.UseDataReceived(flag: true, DataReceivedDelegate);
            return array;
        }

        private void DataReceivedDelegate(GodSerialPort arg1, byte[] arg2)
        {
            this.OnDataReceived?.Invoke(this, arg2);
        }

        private byte[] ReadWithTimeout()
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            while (stopwatch.ElapsedMilliseconds < ReadDataTimeout)
            {
                byte[] array = SerialPort.Read();
                if (array != null)
                {
                    return array;
                }

                Task.Delay(10).Wait();
            }

            throw new TimeoutException("Read data timeout exception.");
        }
    }
}
