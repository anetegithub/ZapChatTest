﻿using System;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

using ZapChatTest.DataLayer;

namespace ZapChatTest
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static MainWindow MainW = null;
        public static MessageWindow MsgW = null;
        public static InfoWindow InfoW = null;
        public static TestWindow TestW = null;

        public MainWindow()
        {
            InitializeComponent();
            MainW = this;
            //positions
            this.Height = SystemParameters.VirtualScreenHeight - 50;
            this.Left = SystemParameters.VirtualScreenWidth - this.Width;
            this.Top = 0;
            ContactsUI.Height = this.Height - 150;            

            //windows init
            this.Closed += (object sender, EventArgs e) =>
                {
                    Environment.Exit(0);
                };
            TestW = new TestWindow();
            TestW.Top = 0;
            TestW.Left = 0;
            TestW.Show();

            InitContactList();
        }

        private void InitContactList()
        {
            //init CL
            Data.Manager = new ContactManager();
            foreach (var c in Data.Manager.ContactList)
            {
                ContactsUI.Items.Add(c.Name);
            }
            Data.Manager.SetListUI(ContactsUI);
            Data.Manager.OnAdd += (Contact NewContact, ListView UIList) =>
            {
                UIList.Items.Add(NewContact.Name);
            };

            ContactsUI.MouseDoubleClick += (object sender, MouseButtonEventArgs e) =>
            {
                DependencyObject dep = (DependencyObject)e.OriginalSource;

                while ((dep != null) && !(dep is ListViewItem))
                {
                    dep = VisualTreeHelper.GetParent(dep);
                }

                if (dep == null)
                    return;

                string SelectedItem = (string)ContactsUI.ItemContainerGenerator.ItemFromContainer(dep);
                MainWindow.ChatWith(SelectedItem,this);
            };
        }

        public static void ChatWith(string Contact,MainWindow Window)
        {
            if (MsgW == null)
            {
                MsgW = new MessageWindow();
                Window.InitMessageWindow(MsgW);
                MsgW.Show();
                MsgW.TabWithThisContact((from a in Data.Manager.ContactList where a.Name == Contact select a).ToList()[0]);
            }
            else
            {
                MsgW.Focus();
                MsgW.TabWithThisContact((from a in Data.Manager.ContactList where a.Name == Contact select a).ToList()[0]);
            }
        }

        private void InitMessageWindow(MessageWindow TestW)
        {
            TestW.Height = 500;
            TestW.Top = SystemParameters.VirtualScreenHeight - TestW.Height;
            TestW.Width = 500;
            TestW.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            TestW.Tabs.Height = 440;
            TestW.Tabs.Width = 460;
        }
    }
}
