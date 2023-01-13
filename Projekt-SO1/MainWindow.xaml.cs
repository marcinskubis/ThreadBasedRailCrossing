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
using System.Diagnostics;

namespace Projekt_SO1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Image [] car = new Image[24];
        BitmapImage btm = new BitmapImage();
        RotateTransform t = new RotateTransform();
        public MainWindow()
        {
            InitializeComponent();
            btm.BeginInit();
            btm.UriSource=new Uri("pack://application:,,,/resources/car1.png");
            btm.EndInit();
            Random rnd = new Random();
            Stopwatch stopwatch = new Stopwatch();
            for(int i = 0; i < 24; i++)
            {
                car[i] = new Image();
                car[i].Source = btm;
                car[i].Height = 50;
                car[i].Width = 80;
                Canvas.SetLeft(car[i], -80);
                Canvas.SetTop(car[i], 240);
                Canvas.SetBottom(car[i], 50);
                Map.Children.Add(car[i]);
            }
            stopwatch.Start();
            new Thread(() =>
            {
                for(int i = 0; i < 1; i++)
                {
                    //Thread.Sleep(rnd.Next(3000,20000));
                    DriveDown(car[i], 4);
                }
            }).Start();
        }

        private void DriveDown(Image car,int velocity)
        {
            new Thread(() =>
            {
                //prosta
                for (int i = -19; i < 660; i++)
                {
                    Thread.Sleep(velocity);
                    Dispatcher.Invoke(new Action(() => { Canvas.SetLeft(car, i); ; }));
                }
                //zakręt
                int r = 659;
                while (r > 658 && r < 822)
                {
                    Thread.Sleep(velocity);
                    Dispatcher.Invoke(new Action(() => {
                        t = new RotateTransform((r - 657) * 180 / 100, car.Width / 2, car.Height / 2);
                        car.RenderTransform = t;
                        Canvas.SetTop(car, r - 409);
                        Canvas.SetLeft(car, Math.Sqrt(7550 - Math.Pow((double)r - 735, 2)) + 660); ;
                    }));
                    r++;
                }

                for (int j=660; j > 200; j--)
                {
                    Thread.Sleep(velocity);
                    Dispatcher.Invoke(new Action(() => { Canvas.SetLeft(car, j); ; }));
                };
                
            }).Start();

        }
    }
}
