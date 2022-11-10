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
using System.Windows.Forms;
using Gma.System.MouseKeyHook;
using TestWork.Entities;
using System.CodeDom;
using System.Net.Mail;
using System.Net;
using Button = System.Windows.Controls.Button;
using System.Runtime.InteropServices;
using System.IdentityModel;

namespace TestWork
{
    /// <summary>
    /// Логика взаимодействия для UserPage.xaml
    /// </summary>
    public partial class UserPage : Page
    {
        private IKeyboardMouseEvents m_GlobalHook;
        private bool IsActivChangedController = false;
        public int CountChanged { get; set; } = 0;
        public int XCoordinationDefault { get; set; } = 0;
        public int YCoordinationDefault { get; set; } = 0;
        public UserPage()
        {

            InitializeComponent();
            DataContext = this;
            CountChanged = EntitiesConnect.DB.HistoryChanged.
            Where(x => x.UserId == EntitiesConnect.userAuth.Id).ToList().Count();
        }

        public void Subscribe()
        {
            m_GlobalHook = Hook.GlobalEvents();
            m_GlobalHook.MouseDownExt += GlobalHookMouseDownExt;
            m_GlobalHook.MouseMove += HookManager_MouseMove;
        }
        private async void HookManager_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            try
            {
                if (Math.Abs(XCoordinationDefault - e.X) > 9 || Math.Abs(YCoordinationDefault - e.Y) > 9)
                {
                    HistoryChanged typeNew = new HistoryChanged();
                    typeNew.UserId = EntitiesConnect.userAuth.Id;
                    typeNew.TypeId = 1;
                    typeNew.DateChagned = DateTime.Now;
                    typeNew.XCoordinates = e.X;
                    typeNew.YCoordinates = e.Y;
                    await SaveDB(typeNew);
                    XCoordinationDefault = e.X;
                    YCoordinationDefault = e.Y;
                    UpdateCount();
                }
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message);
            }
        }

        private void UpdateCount()
        {
            try
            {
                CountChanged++;
                if (CountChanged % 50 == 0)
                {
                    Unsubscribe();
                    startStopButton.Content = "Старт";
                    IsActivChangedController = false;
                    MessageBoxResult result = System.Windows.MessageBox.Show("Отправить уведомление на почту", "Подтверждение действия", MessageBoxButton.YesNo);
                    if (result == MessageBoxResult.Yes)
                    {
                        //Email send
                        MailAddress fromEmail = new MailAddress("isppteam@mail.ru", "TTC");
                        string myAdress = EntitiesConnect.userAuth.Email;
                        MailAddress toEmail = new MailAddress(myAdress);
                        MailMessage m = new MailMessage(fromEmail, toEmail);
                        m.Subject = "Информация о кол-ве записей";
                        m.Body = String.Format("Количество записей в базе: {0}", CountChanged);
                        SmtpClient smtp = new SmtpClient("smpt.mail.ru", 25);
                        smtp.Credentials = new NetworkCredential("isppteam@mail.ru", "33LXJxbtctn4bhBDcXAB");
                        smtp.EnableSsl = true;
                        smtp.Send(m);

                        //string from = "+79178726092";
                        //string to = EntitiesConnect.userAuth.Phone;
                        //string message = String.Format("Количество записей в базе: {0}", CountChanged); ;

                        //WhatsApp whatsAppApi = new WhatsApp(from, "Password", "Airat", false, false);
                        //whatsAppApi.OnConnectSuccess += () =>
                        //{
                        //    MessageBox.Show("Connect to WhatsAp...");
                        //    whatsAppApi.OnLoginSuccess += (phoneNumber, data) =>
                        //    {
                        //        whatsAppApi.SendMessage(to, message);
                        //        MessageBox.Show("Message sent.");
                        //    };
                        //    whatsAppApi.OnLoginFailed += (data) =>
                        //    {
                        //        MessageBox.Show("Login Failed :{0}", data);
                        //    };

                        //    whatsAppApi.Login();
                        //};
                        //whatsAppApi.OnConnectFailed += (ex) =>
                        //{
                        //    MessageBox.Show("Connection Failed...");
                        //};
                        //whatsAppApi.Connect();
                    }
                }
                valCountLabel.Text = "Количество записей в базе: " + CountChanged.ToString();
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message);
            }
        }

        private async Task SaveDB(HistoryChanged hc)
        {
            try
            {
                MainEntities mn = new MainEntities();
                mn.HistoryChanged.Add(hc);
                await Task.Run(() => mn.SaveChanges());
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message);
            }
        }

        private async void GlobalHookMouseDownExt(object sender, MouseEventExtArgs e)
        {
            try
            {
                HistoryChanged typeNew = new HistoryChanged();
                typeNew.UserId = EntitiesConnect.userAuth.Id;
                if (e.Button == MouseButtons.Left)
                {
                    typeNew.TypeId = 2;
                }
                else if (e.Button == MouseButtons.Right)
                {
                    typeNew.TypeId = 3;
                }
                else if (e.Button == MouseButtons.Middle)
                {
                    typeNew.TypeId = 4;
                }
                typeNew.DateChagned = DateTime.Now;
                typeNew.XCoordinates = e.X;
                typeNew.YCoordinates = e.Y;
                await SaveDB(typeNew);
                UpdateCount();
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message);
            }
        }

        public void Unsubscribe()
        {
            m_GlobalHook.MouseDownExt -= GlobalHookMouseDownExt;
            m_GlobalHook.MouseMove -= HookManager_MouseMove;
            m_GlobalHook.Dispose();
        }

        private void LogOutClick(object sender, MouseButtonEventArgs e)
        {
            Unsubscribe();
            NavigationService.GoBack();
            EntitiesConnect.userAuth = null;
        }

        private void StartStopClick(object sender, RoutedEventArgs e)
        {
            if (!IsActivChangedController)
            {
                Subscribe();
                (sender as Button).Content = "Стоп";
                IsActivChangedController = true;
            }
            else
            {
                Unsubscribe();
                (sender as Button).Content = "Старт";
                IsActivChangedController = false;
            }
        }
    }
}
