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
    /// Interaction logic for InfoWindow.xaml
    /// </summary>
    public partial class InfoWindow : Window
    {
        public delegate void CloseDelagate();

        private static System.Timers.Timer t = new System.Timers.Timer();
        public static void StopTimer()
        { t.Stop(); }

        public InfoWindow()
        {
            InitializeComponent();
            this.Closed += (object sender, EventArgs e) =>
                {
                    MainWindow.InfoW = null;
                };
            SysMsgLbl.MouseLeftButtonDown += (object sender, MouseButtonEventArgs e) =>
                {
                    this.Close();
                    MainWindow.InfoW = null;
                };
            AnswerTxt.GotFocus += (object sender, RoutedEventArgs e) =>
                {
                    InfoWindow.StopTimer();
                };
            AnswerBtn.Click += (object sender, RoutedEventArgs e) =>
                {
                    var Tab = new TabItem();

                    foreach (var Tabs in MainWindow.MsgW.Tabs.Items)
                    {
                        if ((SysMsgLbl.Content as TextBlock).Text.IndexOf(((Tabs as TabItem).Header as ContentControl).Content.ToString()) > -1)
                            Tab = (TabItem)Tabs;
                    }

                    foreach (var Child in (Tab.Content as StackPanel).Children)
                    {
                        if ((Child as TextBlock) != null)
                            (Child as TextBlock).Text += "\nYou : " + new TextRange(AnswerTxt.Document.ContentStart, AnswerTxt.Document.ContentEnd).Text.Replace("\r\n", "");
                    }

                };

            t.Interval = 10000;
            t.Elapsed += (object sender, System.Timers.ElapsedEventArgs e) =>
            {
                if (MainWindow.InfoW != null)
                {
                    MainWindow.InfoW.Dispatcher.Invoke(new CloseDelagate(MainWindow.InfoW.Close));
                    MainWindow.InfoW = null;
                }
                t.Stop();
            };
            t.Start();
        }
    }
}