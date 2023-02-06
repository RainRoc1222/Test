using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Mvi.Wpf
{
    /// <summary>
    /// MainWindow.xaml 的互動邏輯
    /// </summary>
    public partial class MainWindow : Window,INotifyPropertyChanged
    {
        public MviRs232_Controller MviRs232_Controller { get; set; }
 
        public MainWindow()
        {

            MviRs232_Controller = new MviRs232_Controller();
            InitializeComponent();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MviRs232_Controller.MeasureReuslt = new MeasureReuslt();
            MviRs232_Controller.LevelResult = new LevelResult();
            MviRs232_Controller.OkCount = 0;
            MviRs232_Controller.NgCount = 0;
        }
    }
}
