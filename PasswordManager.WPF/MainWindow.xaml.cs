using AuthServer.Public;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PasswordManager.WPF
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private AuthService _service = new AuthService(Guid.Parse("7f2fe24c-060f-454a-8a13-1c15c5f44237"), "https://auth.chrisimi.dev");
        public MainWindow()
        {
            InitializeComponent();
            _service.UserAuthSuccessfulEvent += _service_UserAuthSuccessfulEvent;
            //Process.Start("cmd", "/C start https://google.at");
        }

        private void _service_UserAuthSuccessfulEvent(Guid userId)
        {
            Task.Run(() => SetText(userId));
        }

        private void SetText(Guid text)
        {
            label1.Content = text.ToString();
        }

        private void startButton_Click(object sender, RoutedEventArgs e)
        {
            Task.Run(async () =>
            {
                string urlString = await _service.RequestAuth();
                System.Diagnostics.Process.Start("cmd", string.Format("/C start {0}", urlString));
            });
        }
    }
}
