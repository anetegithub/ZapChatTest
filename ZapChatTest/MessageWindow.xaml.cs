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
    /// Interaction logic for MessageWindow.xaml
    /// </summary>
    public partial class MessageWindow : Window
    {
        private List<Logic.Contact> OpenedTabs = new List<Logic.Contact>();

        public MessageWindow()
        {
            InitializeComponent();
        }

        public void OpenNewTab(Logic.Contact NewTab)
        {
            if (OpenedTabs.IndexOf(NewTab) == -1)
            {
                OpenedTabs.Add(NewTab);
                Tabs.Items.Add(new TabItem() { Header = NewTab.Name });
                (Tabs.Items[Tabs.Items.Count - 1] as TabItem).Focus();
                
                var stackPanel = new StackPanel();
                string tab = (Tabs.Items.Count - 1).ToString();
                var text = new TextBlock() {Text=NewTab.Name+" : "+NewTab.Talk, Height = 260, Width = 420 };
                var rich = new RichTextBox() { Height = 100, Width = 380, Margin = new Thickness(0, 0, 0, 0) };
                stackPanel.Children.Add(text);
                stackPanel.Children.Add(rich);
                Button btn = new Button() { Content = "Send" };
                btn.Click += (object sender, RoutedEventArgs e) =>
                    {
                        text.Text += "\nYou : " + new TextRange(rich.Document.ContentStart, rich.Document.ContentEnd).Text.Replace("\r\n", "");
                      
                    };
                stackPanel.Children.Add(btn);
                
                (Tabs.Items[Tabs.Items.Count - 1] as TabItem).Content = stackPanel;
                //tabItem.Content = stackPanel;
            }
            else
            {
                int id = Tabs.Items.IndexOf((from object a in Tabs.Items where (a as TabItem).Header.ToString() == NewTab.Name select a).ToList()[0]);
                (Tabs.Items[id] as TabItem).Focus();
            }
        }
    }
}
