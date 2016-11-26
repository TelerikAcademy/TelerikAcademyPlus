using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace II.FreezingUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            this.InitializeComponent();
        }

        private IEnumerable<Car> ProcessCarsFile(string filePath)
        {
            var cars = new List<Car>(600);
            var lines = File.ReadAllLines(filePath).Skip(2);

            foreach (var line in lines)
            {
                cars.Add(Car.Parse(line));
            }

            return cars;
        }

        private void BtnFreezeUI_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Why is this not displaying on the UI?
                this.btnFreezeUI.Content = "Freezing...";

                // Freeze the UI thread
                Thread.Sleep(2000);

                this.btnFreezeUI.Content = "UI has been frozen (Click to freeze again)";
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }

        private void BtnShowDetails_Click(object sender, RoutedEventArgs e)
        {
            var selectedCar = this.listViewCars.SelectedItem ?? "NO CAR SELECTED";
            MessageBox.Show(selectedCar.ToString(), "Car details", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void BtnLoadCars_Click(object sender, RoutedEventArgs e)
        {
            var carsDataPath = @"..\..\..\Data\cars.csv";
            var cars = this.ProcessCarsFile(carsDataPath);

            this.listViewCars.ItemsSource = cars;
        }
    }
}
    