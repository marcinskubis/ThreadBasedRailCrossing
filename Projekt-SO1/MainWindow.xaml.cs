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
        Image [] train = new Image[10];
        BitmapImage btm = new BitmapImage();
        BitmapImage btm2 = new BitmapImage();
        RotateTransform t = new RotateTransform();
        bool TrainIsComing = false;
        public MainWindow()
        {
            InitializeComponent();
            btm.BeginInit();
            btm.UriSource=new Uri("pack://application:,,,/resources/car1.png");
            btm.EndInit();
            btm2.BeginInit();
            btm2.UriSource = new Uri("pack://application:,,,/resources/train.png");
            btm2.EndInit();
            Random rnd = new Random();
            Stopwatch stopwatch = new Stopwatch();

            

            for(int i=0; i < 10; i++)
            {
                train[i] = new Image();
                train[i].Source = btm2;
                train[i].Height = 200;
                train[i].Width = 400;
                Canvas.SetLeft(train[i], 1100);
                Canvas.SetTop(train[i], 490);
                Map.Children.Add(train[i]);
            }

            new Thread(() =>
            {
                for (int i = 0; i < 10; i++)
                {
                    Thread.Sleep(rnd.Next(10000, 30000));
                    Train(train[i]);
                }
            }).Start();

            for (int i = 0; i < 24; i++)
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

            new Thread(() =>
            {
                for(int i = 0; i < 24; i++)
                {
                    Thread.Sleep(rnd.Next(1000,5000));
                    DriveDown(car[i], rnd.Next(3,7));
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
                        t = new RotateTransform((r - 657) * 180 / 162, car.Width / 2, car.Height / 2);
                        car.RenderTransform = t;
                        Canvas.SetTop(car, r - 409);
                        Canvas.SetLeft(car, Math.Sqrt(7550 - Math.Pow((double)r - 735, 2)) + 660); ;
                    }));
                    r++;
                }

                for (int j=660; j > 200; j--)
                {
                    Thread.Sleep(velocity);
                    Dispatcher.Invoke(new Action(() => {
                        t = new RotateTransform(180, car.Width / 2, car.Height / 2);
                        car.RenderTransform = t;
                        Canvas.SetLeft(car, j);
                        Canvas.SetTop(car,405); 
                    }));
                };
                r = 814;
                while(r>813 && r < 1075) //977
                {
                    
                    Thread.Sleep(velocity);
                    Dispatcher.Invoke(new Action(() => {
                        t = new RotateTransform((1075 - r) * 180 / 290, car.Width / 2, car.Height / 2);
                        car.RenderTransform = t;
                        Canvas.SetTop(car, r - 409);
                        Canvas.SetLeft(car, (-1) * Math.Sqrt(17200 - Math.Pow((double)r - 944, 2)) + 205); ;
                    }));
                    r++;
                    
                    if (TrainIsComing && r==929)
                    {
                        do
                        {
                            Thread.Sleep(1);
                        } while (TrainIsComing);
                    }
                }

                //ostatnia prosta
                for(int k=200; k < 1200; k++)
                {
                    Thread.Sleep(velocity);
                    Dispatcher.Invoke(new Action(() => {
                        Canvas.SetLeft(car, k);
                        Canvas.SetTop(car, 670);
                    }));
                }

            }).Start();

        }

        public void Train(Image train)
        {
            new Thread(() =>
            {
                TrainIsComing = true;
                for (int i = 1100; i > -450; i--)
                {
                    Thread.Sleep(4);
                    Dispatcher.Invoke(new Action(() => {
                        Canvas.SetLeft(train, i);
                    }));
                }
                TrainIsComing = false;
            }).Start();
        }
    }
}
