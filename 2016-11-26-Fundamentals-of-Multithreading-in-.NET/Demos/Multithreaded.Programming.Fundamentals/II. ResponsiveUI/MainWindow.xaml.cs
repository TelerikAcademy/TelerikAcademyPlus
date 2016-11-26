using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace II.ResponsiveUI
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
                // CALLED BY THE MAIN THREAD
                Task.Run(() =>
                {
                    // Show the threadID that executes the following statement
                    var currentThreadId = Thread.CurrentThread.ManagedThreadId;

                    Dispatcher.Invoke(() =>
                    {
                        // Show the thread id that executes the following statement
                        this.btnFreezeUI.Content = "Freezing...";
                    });
                    
                    var uiThreadId = Thread.CurrentThread.ManagedThreadId;

                    // DOES REALLY LONG WORK
                    Thread.Sleep(5000);

                    Dispatcher.Invoke(()=>
                    {
                        this.btnFreezeUI.Content = "UI has been frozen (Click to freeze again)";
                    });

                    //var uiThreadId = Thread.CurrentThread.ManagedThreadId;
                });
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }

        private void BtnShowDetails_Click(object sender, RoutedEventArgs e)
        {
            var selectedCar = this.listViewCars.SelectedItem ?? "NO CAR SELECTED";

            var messageBoxText = selectedCar.ToString();
            var caption = "Car details";
            var messageBoxButton = MessageBoxButton.OK;
            var messageBoxIcon = MessageBoxImage.Information;

            MessageBox.Show(messageBoxText, caption, messageBoxButton, messageBoxIcon);
        }

        private void BtnLoadCars_Click(object sender, RoutedEventArgs e)
        {
            var carsDataPath = @"..\..\..\Data\cars.csv";
            var cars = this.ProcessCarsFile(carsDataPath);

            this.listViewCars.ItemsSource = cars;
        }
    }
}
