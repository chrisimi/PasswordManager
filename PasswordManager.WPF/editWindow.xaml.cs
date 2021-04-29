using PasswordManager.Domain;
using PasswordManager.Web;
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

namespace PasswordManager.WPF
{
    /// <summary>
    /// Interaktionslogik für editWindow.xaml
    /// </summary>
    public partial class editWindow : Window
    {
        private ILogic logic = new TestLogic();
        private Guid userId;

        public editWindow(Guid userId)
        {
            InitializeComponent();
            this.userId = userId;


            lblOldUsername.Content = logic.GetFromUser(userId);

            lblOldPassword.Content = logic.GetFromUser(userId);

        }




        private void saveBtn_Click(object sender, RoutedEventArgs e)
        {
            var wnd = new EntryWindow(Guid.NewGuid());
            wnd.Show();

            this.Close();

            var newUser = userId.Equals(tbNewUsername);
            var newPassword = userId.Equals(tbNewPassword);

            logic.GetFromUser(newUser);
            logic.GetFromUser(newPassword);


        }
    }
}
