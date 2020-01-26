using Pelit;
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

namespace Polttopallo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Pelit.Peli peli = new Pelit.Peli();
        public MainWindow()
        {
            InitializeComponent();
            peli.Init();
            pisteet.Content = peli.Pisteet;
            hutit.Content = peli.Hutit;
            System.Windows.Threading.DispatcherTimer dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
            dispatcherTimer.Tick += dispatcherTimer_Tick;
            dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
            dispatcherTimer.Start();
        }
        public void tyhjä()
        {
            Image[] lista = new Image[] { _1, _2, _3, _4, _5, _6, _7, _8, _9 };
            foreach (var item in lista)
            {
                item.Visibility = Visibility.Hidden;
            }
        }
        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            tyhjä();
            peli.Siirto();
            Siirrä(peli.rnd+1);
        }
        private void Siirrä(int ruutu)
        {
            switch (ruutu)
            {
                case 1:
                    _1.Visibility = Visibility.Visible;
                    break;
                case 2:
                    _2.Visibility = Visibility.Visible;
                    break;
                case 3:
                    _3.Visibility = Visibility.Visible;
                    break;
                case 4:
                    _4.Visibility = Visibility.Visible;
                    break;
                case 5:
                    _5.Visibility = Visibility.Visible;
                    break;
                case 6:
                    _6.Visibility = Visibility.Visible;
                    break;
                case 7:
                    _7.Visibility = Visibility.Visible;
                    break;
                case 8:
                    _8.Visibility = Visibility.Visible;
                    break;
                case 9:
                    _9.Visibility = Visibility.Visible;
                    break;
                default:
                    break;
            }
        }
        private void Click(int button)
        {
            peli.OnkoOsuma(button);
            pisteet.Content = peli.Pisteet;
            hutit.Content = peli.Hutit;
            if (peli.OnkoValmis() == true)
            {
                game_over.Visibility = Visibility.Visible;
                /*MediaPlayer Sound1 = new MediaPlayer();
                Sound1.Open(new Uri(@"C:\Users\Aki Rönnkvist.LAPTOP-TPE55JDQ\source\repos\Polttopallo\Polttopallo\Resources\gameover.wav"));
                Sound1.Play();*/
                MediaPlayer mplayer = new MediaPlayer();
                mplayer.Open(new Uri("gameover.wav", UriKind.Relative));
                mplayer.Play();
                System.Media.SoundPlayer player = new System.Media.SoundPlayer(Properties.Resources.gameover);
                player.Play();
            }
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Click(1);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Click(2);
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            Click(3);
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            Click(4);
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            Click(5);
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            Click(6);
        }

        private void Button_Click_6(object sender, RoutedEventArgs e)
        {
            Click(7);
        }

        private void Button_Click_7(object sender, RoutedEventArgs e)
        {
            Click(8);
        }

        private void Button_Click_8(object sender, RoutedEventArgs e)
        {
            Click(9);
        }

        private void Button_Click_9(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
