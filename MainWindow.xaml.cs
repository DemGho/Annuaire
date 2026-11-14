using System.Windows;
using Annuaire.Services;

namespace Annuaire
{
    public partial class MainWindow : Window
    {
        private readonly RandomUserService _service = new();

        public MainWindow()
        {
            InitializeComponent();
        }

        private async void LoadButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                EmployeesList.ItemsSource = await _service.GetEmployeesAsync(20);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}