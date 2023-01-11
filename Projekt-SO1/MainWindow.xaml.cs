﻿using System;
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
        public MainWindow()
        {
            InitializeComponent();
            btm.BeginInit();
            btm.UriSource=new Uri("pack://application:,,,/resources/car1.png");
            btm.EndInit();
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
            Random rnd = new Random();
            Stopwatch stopwatch = new Stopwatch();
            //DriveDown(car,rnd.Next(3,10));
            stopwatch.Start();
            new Thread(() =>
            {
                for(int i = 0; i < 24; i++)
                {
                    Thread.Sleep(2000);
                    DriveDown(car[i], rnd.Next(3, 10));
                }
            }).Start();
        }

        private void DriveDown(Image car,int velocity)
        {
            new Thread(() =>
            {
                //prosta
                for (int i = -19; i < 650; i++)
                {
                    Thread.Sleep(velocity);
                    Dispatcher.Invoke(new Action(() => { Canvas.SetLeft(car, i); ; }));
                }
                //zakręt
                /*while (pozycja<436)
                {
                    Thread.Sleep(velocity);
                    Dispatcher.Invoke(new Action(() => {
                        Canvas.SetTop(car, (double)car.GetValue(Canvas.TopProperty)+1);
                        if ((double)car.GetValue(Canvas.LeftProperty) < 800)
                        {
                            Canvas.SetLeft(car, (double)car.GetValue(Canvas.LeftProperty) + 1);
                        }
                        else
                        {
                            Canvas.SetLeft(car, (double)car.GetValue(Canvas.LeftProperty) - 1);
                        }
                        pozycja = Convert.ToInt32(car.GetValue(Canvas.TopProperty));
                        ;
                        ; }));
                }*/
                int r = 650;
                /*while (r > 649 && r < 900)
                {
                    Thread.Sleep(velocity);
                    Dispatcher.Invoke(new Action(() => {
                        Canvas.SetTop(car, r - 350);
                        Canvas.SetLeft(car, Math.Sqrt(6000 - Math.Pow((double)r - 745, 2)) + 650); ;
                    }));
                    r++;
                }*/
            }).Start();

        }
    }
}
