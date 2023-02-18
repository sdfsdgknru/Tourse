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

namespace Tours
{
    /// <summary>
    /// Логика взаимодействия для EditPage.xaml
    /// </summary>
    public partial class EditPage : Page
    {
        bool f = false;
        public EditPage(Hotel selectedHotel)
        {
            InitializeComponent();
            if (selectedHotel != null)
                _currenthotel = selectedHotel;
            else f = true;

            DataContext = _currenthotel;
            ComboCountries.ItemsSource = ToursBaseEntities.GetContext().Country.ToList();

        }

        private Hotel _currenthotel = new Hotel();

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();
            if (string.IsNullOrWhiteSpace(_currenthotel.Name))
                errors.AppendLine("Укажите название отеля");
            if (_currenthotel.CountOfStars < 1 || _currenthotel.CountOfStars > 5)
                errors.AppendLine("Количество звезд - число от 1 до 5");
            if (_currenthotel.Country == null)
                errors.AppendLine("Выберите страну");
            if (errors.Length>0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }

            if (_currenthotel.Id == 0)
            {
                ToursBaseEntities.GetContext().Hotel.Add(_currenthotel);
            }
            try 
            {
                ToursBaseEntities.GetContext().SaveChanges();
                MessageBox.Show("Информация сохранена");
                Manager.MainFrame.GoBack(); 
            }
            catch (Exception ex)
            { MessageBox.Show(ex.ToString()); }
                

        }
    }
}
