using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mvi.Wpf
{
    public class MviRs232_Controller : INotifyPropertyChanged
    {
        public SerialPortController SerialPortController { get; set; }
        public MviParser MviParser { get; set; }
        public MeasureReuslt MeasureReuslt { get; set; }
        public LevelResult LevelResult { get; set; }
        public int OkCount { get; set; }
        public int NgCount { get; set; }

        public MviRs232_Controller()
        {
            LevelResult = new LevelResult();
            MeasureReuslt = new MeasureReuslt();
            MviParser = new MviParser();
            SerialPortController = new SerialPortController();
            SerialPortController.ReceiveData += SerialPortController_ReceiveData;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void SerialPortController_ReceiveData(object sender, byte[] e)
        {
            var input = Encoding.ASCII.GetString(e);
            var data = input.Split(',');

            switch (data.Length)
            {
                case 32:
                    MeasureReuslt = MviParser.MeasureParse(input);
                    OkCount++;
                    break;

                case 13:
                    LevelResult = MviParser.LevelParse(input);
                    OkCount++;
                    break;

                default:
                    NgCount++;
                    break;
            }
        }
    }
}
