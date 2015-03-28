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

namespace ZapChatTest
{
    /// <summary>
    /// Interaction logic for TestWindow.xaml
    /// </summary>
    public partial class TestWindow : Window
    {
        public TestWindow()
        {
            InitializeComponent();

            AddBtn.Click += (object sender, RoutedEventArgs e) =>
                {
                    Util.Manager.Add(new Logic.Contact("NewContact " + Guid.NewGuid().ToString().Substring(0, 5)));
                };
        }
    }
}
