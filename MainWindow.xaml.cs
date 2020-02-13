using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using Kirov_X;
using System.Threading.Tasks;

namespace Kirov_X
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        DispatcherTimer timer = new DispatcherTimer();
        public static bool isLoading = true;


        public MainWindow()
        {
            InitializeComponent();
            timer.Tick += new EventHandler(timer_Tick);
            Label_Time.Content = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
            timer.Start();
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
                if (isLoading == true)
                {
                    await this.ShowMessageAsync("错误", "终端已在重启", MessageDialogStyle.Affirmative);
                    return;
                }
                Funtion.ConsoleInit(Console);
            }
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
            if (isLoading == false)
            {
                if (s.Replace(" ", "") != "")
                {
                    Funtion.ConsoleRun(s, Console);
                }
                else
                {
                    Funtion.ConsoleDisplay("", Console);
                }
            }
        }

        private void Tile_Test_Click(object sender, RoutedEventArgs e)
        {
            Funtion.baidu.token = Funtion.baidu.getToken();
            Funtion.ConsoleDisplay(Funtion.baidu.token,Console);
            Funtion.baidu.getAudio(Funtion.baidu.token,"这是一段测试语音");
            //baidu.getAudio("24.bb6c896c1b9e79fbab2b5f496681ac9a.2592000.1584161326.282335-18455211","这是一段测试语音");
            Funtion.ConsoleDisplay("这是一段测试语音", Console);
            Funtion.baidu.playSound();
        }

        private void MetroWindow_Loaded(object sender, RoutedEventArgs e)
        {
            Funtion.ConsoleInit(Console);
        }

        private void Console_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            ConsoleScroll.ScrollToBottom();
        }
    }
}
