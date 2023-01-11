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
using System.Threading;

namespace Projekt_SO1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Image car = new Image();
        BitmapImage btm= new BitmapImage();
        public MainWindow()
        {
            InitializeComponent();

            btm.BeginInit();
            btm.UriSource=new Uri("pack://application:,,,/resources/car1.png");
            btm.EndInit();
            car.Source = btm;
            car.Height = 50;
            car.Width = 80;
            Map.Children.Add(car);
            Canvas.SetLeft(car, 60);
            Canvas.SetTop(car, 240);
            Canvas.SetBottom(car, 50);
            //Drive(car);
            for (int i = 241; i < 560; i++)
            {
                Thread.Sleep(200);
                Canvas.SetLeft(car, i);
            }
        }

        public void Drive(Image car)
        {
            for(int i = 241; i < 560; i++)
            {
                Thread.Sleep(200);
                Canvas.SetLeft(car, i);
            }
        }
    }
}
