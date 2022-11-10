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
    /// Логика взаимодействия для AdminPage.xaml
    /// </summary>
    public partial class AdminPage : Page
    {
        private List<HistoryChanged> historyList = new List<HistoryChanged>();
        List<TypeChanged> typeChangeds;
        public AdminPage()
        {
            InitializeComponent();
            historyList = EntitiesConnect.DB.HistoryChanged.ToList();
            HistoryListView.ItemsSource = historyList;
            typeChangeds = new List<TypeChanged>();
            typeChangeds.Add(new TypeChanged { TypeOfAction = "Все" });
            EntitiesConnect.DB.TypeChanged.ToList().ForEach(x => { typeChangeds.Add(x); });
            TypeBox.ItemsSource = typeChangeds;
        }

        private void LogOutClick(object sender, MouseButtonEventArgs e)
        {
            NavigationService.GoBack();
            EntitiesConnect.userAuth = null;
        }

        private void SortParametrChanged(object sender, SelectionChangedEventArgs e)
        {
            SortList();
        }

        private void SortList()
        {
            try
            {
                List<HistoryChanged> sortValueList = historyList;
                if (((TypeChanged)TypeBox.SelectedItem).TypeOfAction != "Все")
                {
                    sortValueList = historyList.Where(x => x.TypeId == ((TypeChanged)TypeBox.SelectedItem).Id).ToList();
                }
                if (DateBox.SelectedDate != null)
                {
                    DateTime start = ((DateTime)DateBox.SelectedDate).Date;
                    DateTime end = ((DateTime)DateBox.SelectedDate).Date.AddDays(1);
                    sortValueList = sortValueList.Where(x => start <= x.DateChagned && x.DateChagned < end).ToList();
                }
                HistoryListView.ItemsSource = sortValueList;
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message);
            }
        }
    }
}
