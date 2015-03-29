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

using System.Windows.Media.Animation;

using ZapChatTest.DataLayer;

namespace ZapChatTest
{
    /// <summary>
    /// Interaction logic for TestWindow.xaml
    /// </summary>
    public partial class TestWindow : Window
    {        
        private static Random r = new Random();

        public TestWindow()
        {
            InitializeComponent();

            AddBtn.Click += (object sender, RoutedEventArgs e) =>
                {
                    Data.Manager.Add(new DataLayer.Contact("NewContact " + Guid.NewGuid().ToString().Substring(0, 5)));
                };
            CntBtn.Click += (object sender, RoutedEventArgs e) =>
                {
                    InfoWindow_Msg("Connected!", false, Colors.Green);
                };
            DntBtn.Click += (object sender, RoutedEventArgs e) =>
                {
                    InfoWindow_Msg("Disconnected!", false, Colors.Red);
                };
            MsgBtn.Click += (object sender, RoutedEventArgs e) =>
                {
                    string ContactName = MainWindow.MainW.ContactsUI.Items[r.Next(0, MainWindow.MainW.ContactsUI.Items.Count)].ToString();
                    MainWindow.ChatWith(ContactName, MainWindow.MainW);
                    for (Contact C = new Contact() { Id = 0 }; C.Id < Data.Manager.ContactList.Count; C.Id++)
                        if (Data.Manager.ContactList[C.Id].Name.Equals(ContactName))
                            InfoWindow_Msg(
                                Data.Manager.ContactList[C.Id].Name + " : " + Data.Manager.ContactList[C.Id].LastReplica,
                                true,
                                Colors.Black);

                    
                };
        }

        private void InfoWindow_Msg(string Msg,bool Button, Color Color)
        {
            if (MainWindow.InfoW != null)
                MainWindow.InfoW.Close();

            MainWindow.InfoW = new InfoWindow();
            MainWindow.InfoW.Show();
            MainWindow.InfoW.Top = SystemParameters.VirtualScreenHeight - 40 - MainWindow.InfoW.Height;
            MainWindow.InfoW.Left = SystemParameters.VirtualScreenWidth - MainWindow.InfoW.Width;

            MainWindow.InfoW.SysMsgLbl.Visibility = System.Windows.Visibility.Visible;
            TextBlock labelText = new TextBlock();
            labelText.Text = Msg;
            labelText.Foreground = new SolidColorBrush(Color);
            MainWindow.InfoW.SysMsgLbl.Content = labelText;

            if (Button)
            {
                MainWindow.InfoW.AnswerTxt.Visibility = System.Windows.Visibility.Visible;
                MainWindow.InfoW.AnswerBtn.Visibility = System.Windows.Visibility.Visible;
            }

            Storyboard sb = new Storyboard();

            DoubleAnimation SlideEffect = new DoubleAnimation();
            SlideEffect.From = 0;
            SlideEffect.To = MainWindow.InfoW.Height;
            SlideEffect.SpeedRatio = 1;
            Storyboard.SetTarget(SlideEffect, MainWindow.InfoW);
            Storyboard.SetTargetProperty(SlideEffect, new PropertyPath(Window.HeightProperty));

            //MainWindow.InfoW.BeginAnimation(System.Windows.Window.HeightProperty, myAnimation);
            DoubleAnimation PosEffect = new DoubleAnimation();
            PosEffect.From = SystemParameters.VirtualScreenHeight - 40;
            PosEffect.To = MainWindow.InfoW.Top;
            PosEffect.SpeedRatio = 1;

            Storyboard.SetTarget(PosEffect, MainWindow.InfoW);
            Storyboard.SetTargetProperty(PosEffect, new PropertyPath(Window.TopProperty));

            //MainWindow.InfoW.BeginAnimation(System.Windows.Window.HeightProperty, SlideEffect);
            sb.Children.Add(SlideEffect);
            sb.Children.Add(PosEffect);
            sb.Begin();
        }
    }
}
