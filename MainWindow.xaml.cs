using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;

namespace Kirov_X
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        DispatcherTimer timer = new DispatcherTimer();
        

        public MainWindow()
        {
            InitializeComponent();
            timer.Tick += new EventHandler(timer_Tick);
            Label_Time.Content = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
            timer.Start();
            Console.Text += "[Admin]AkulaKirov>";
        }

        private void Tile_Click(object sender, RoutedEventArgs e)
        {
            
        }

        void timer_Tick(object sender, EventArgs e)
        {
            Label_Time.Content = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
        }

        private void Button_Enter_Click(object sender, RoutedEventArgs e)
        {
            ConsoleInput(TextBox_Input.Text);
            TextBox_Input.Text = null;
        }

        private async void Tile_Restart_Click(object sender, RoutedEventArgs e)
        {
            MessageDialogResult result = await this.ShowMessageAsync("提示", "确定重启终端吗？",MessageDialogStyle.AffirmativeAndNegative);
            if (result == MessageDialogResult.Affirmative)
            {
                Console.Text = null;
            }
            Console.Text += "[Admin]AkulaKirov>";
        }

        private void TextBox_Input_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                Button_Enter_Click(null,null);
            }
        }

        private void ConsoleInput(string s)
        {
            Console.Text += s + "\n";
            string a = s.Replace(" ","");
            if (a != "")
            {
                Funtion.Run(s, this.Console);
            }

            Console.Text += "\n" + "[Admin]AkulaKirov>";
            ConsoleScroll.ScrollToBottom();
        }

        private void Tile_Test_Click(object sender, RoutedEventArgs e)
        {
            Console.Text += "say 测试" + "\n";
            Funtion.Run("say 测试",this.Console);
            Console.Text += "\n" + "[Admin]AkulaKirov>";
        }
    }
}
