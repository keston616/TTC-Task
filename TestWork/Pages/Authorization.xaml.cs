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
using TestWork.Entities;

namespace TestWork
{
    /// <summary>
    /// Логика взаимодействия для Authorization.xaml
    /// </summary>
    public partial class Authorization : Page
    {
        public List<Users> userList = new List<Users>();
        public Authorization()
        {
            InitializeComponent();
            userList = EntitiesConnect.DB.Users.ToList();
        }

        private void LogInClick(object sender, RoutedEventArgs e)
        {
            try
            {
                if (String.IsNullOrWhiteSpace(LoginField.Text))
                {
                    MessageBox.Show("Поле логин пусто");
                }
                else if (String.IsNullOrWhiteSpace(PasswordField.Password))
                {
                    MessageBox.Show("Поле пароль пусто");
                }
                else
                {
                    Users userValue = userList.Where(x => x.Login == LoginField.Text).FirstOrDefault();
                    if (userValue == null)
                    {
                        MessageBox.Show("Пользователь не найден");
                    }
                    else if (userValue.Password == PasswordField.Password)
                    {
                        EntitiesConnect.userAuth = userValue;
                        if (userValue.RoleId == 1)
                        {
                            NavigationService.Navigate(new AdminPage());
                        }
                        else
                        {
                            NavigationService.Navigate(new UserPage());
                        }
                    }
                    else
                    {
                        MessageBox.Show("Пароль введен неверно");
                    }
                }
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message);
            }
        }
    }
}
