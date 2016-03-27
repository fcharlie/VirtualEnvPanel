using MahApps.Metro.SimpleChildWindow;
using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace VirtualEnvPanel
{
    /// <summary>
    /// EditCommandWindow.xaml 的交互逻辑
    /// </summary>
    public partial class EditCommandWindow : ChildWindow
    {
        public EditCommandWindow()
        {
            InitializeComponent();
        }
        private void CloseSec_OnClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
