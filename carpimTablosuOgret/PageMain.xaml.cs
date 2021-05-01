using Microsoft.Win32;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using System.Xml;

namespace carpimTablosuOgret
{
    /// <summary>
    /// Interaction logic for PageMain.xaml
    /// </summary>
    public partial class PageMain : Page
    {
        public PageMain()
        {
            InitializeComponent();
        }
        DispatcherTimer dispatcherTimer = new DispatcherTimer();
        DispatcherTimer dispatcherTimer2 = new DispatcherTimer();
        int xmlMaxSoru = 5;
        int dakikaBekleme = 0;
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            try
            {
                RegistryKey runKey = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run", true);
                runKey.SetValue("CarpimOgren", "\"" + Application.ResourceAssembly.Location.ToString() + "\"");
                runKey.Close();
            }
            catch { }

            var desktopWorkingArea = System.Windows.SystemParameters.WorkArea;
            App.mw.Left = desktopWorkingArea.Right - this.Width;
            App.mw.Top = desktopWorkingArea.Bottom - this.Height - 15;

            XmlDocument xml = new XmlDocument();
            string fileName = System.AppDomain.CurrentDomain.BaseDirectory + "\\db.xml";
            xml.Load(fileName);
            XmlNodeList xmlNodelst = xml.GetElementsByTagName("programKaydi");
            dakikaBekleme = 0;
            foreach (XmlNode node in xmlNodelst)
            {
                if (node["saniye"].InnerText.ToString() != "Sınırsız")
                    dakikaBekleme = Convert.ToInt32(node["saniye"].InnerText.ToString().Replace("Dakika", "").Trim());
                else
                    dakikaBekleme = 0;
                xmlMaxSoru = Convert.ToInt32(node["soru"].InnerText.ToString());
            }
            

            soruyuVer();
            dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick); // 3 saniye sonra sonuç kaybolma
            dispatcherTimer.Interval = new TimeSpan(0, 0, 3);
            dispatcherTimer.Start();

            dispatcherTimer2.Tick += new EventHandler(dispatcherTimer_Tick2); // bekleme 
            dispatcherTimer2.Interval = new TimeSpan(0, dakikaBekleme, 0);
            /*if(dakikaBekleme != 0)
                dispatcherTimer.Start();*/

            label3.Margin = new Thickness { Top = 10, Left = (this.Width / 2) - 80 };

            // Sıfırlama 
            label3.Foreground = Brushes.Green;
            label3.Visibility = Visibility.Visible;
            label3.Content = "Başlıyalım";
            dispatcherTimer.Stop();
            dispatcherTimer.Start();
            maxsorma = 0; // yükleme sıfırlaması.
            toplamDogru = 0;
            toplamYanlis = 0;
            pas = 0;
            try
            {
                label2.Content = "";
            }
            catch { }
        }

        ArrayList liste = new ArrayList();
        Random rnd;
        int snc = 0;
        int toplamDogru = 0;
        int toplamYanlis = 0;
        int pas = 0;
        int maxsorma = 0;
        int s1 = 0;
        int s2 = 0;
        private bool soruyuVer()
        {
            if (maxsorma >= xmlMaxSoru)
            {
                maxsorma = 0;
                if (dakikaBekleme != 0)
                {
                    App.mw.Visibility = Visibility.Hidden;
                    dispatcherTimer2.Start();
                }
            }

            if (liste.Count == 63)
            {
                liste.Clear();
            }

            rnd = new Random();
            s1 = rnd.Next(2, 10);
            s2 = rnd.Next(2, 10);

            if (liste.IndexOf("" + s1 + " X " + s2 + " =  ?".ToString()) != -1)
            {
                return false;
            }
            else
            {
                label1.Content = "" + s1 + " X " + s2 + " =  ?".ToString();
                liste.Add("" + s1 + " X " + s2 + " =  ?".ToString());
                snc = s1 * s2;
                maxsorma++;
                return true;
            }
        }


        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            label3.Foreground = Brushes.White;
            label3.Visibility = Visibility.Hidden;
            dispatcherTimer.Stop();
        }
        private void dispatcherTimer_Tick2(object sender, EventArgs e)
        {
            App.mw.Visibility = Visibility.Visible;
            dispatcherTimer2.Stop();
        }

        private void textbox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                label3.Visibility = Visibility.Visible;
                if (snc.ToString() == textbox1.Text.ToString())
                {
                    label3.Content = "DOĞRU";
                    label3.Foreground = Brushes.Green;
                    toplamDogru++;
                    label2.Content = (100 / (toplamDogru + toplamYanlis)) * toplamDogru + " % | Toplam Doğru: " + toplamDogru + " | Toplam Yanlış: " + toplamYanlis + " | Pas: " + pas;
                    textbox1.Text = "";
                    bool test = false;
                    while (test != true)
                        test = soruyuVer();
                    dispatcherTimer.Start();
                }
                else
                {
                    label3.Content = "YANLIŞ";
                    label3.Foreground = Brushes.Red;
                    toplamYanlis++;
                    label2.Content = (100 / (toplamDogru + toplamYanlis)) * toplamDogru + " % | Toplam Doğru: " + toplamDogru + " | Toplam Yanlış: " + toplamYanlis + " | Pas: " + pas;
                    textbox1.Text = "";
                    bool test = false;
                    while (test != true)
                        test = soruyuVer();
                    dispatcherTimer.Start();
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            label3.Visibility = Visibility.Visible;
            label3.Foreground = Brushes.Magenta;
            label3.Content = "Cevap: " + snc;
            pas++;
            bool test = false;
            while (test != true)
                test = soruyuVer();
        }
        bool trs = true;
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (!trs)
            {
                var desktopWorkingArea = System.Windows.SystemParameters.WorkArea;
                App.mw.Left = desktopWorkingArea.Right - this.Width;
                App.mw.Top = desktopWorkingArea.Bottom - this.Height - 15;
                trs = true;
            }
            else
            {
                trs = false;
                var desktopWorkingArea = System.Windows.SystemParameters.WorkArea;
                App.mw.Left = desktopWorkingArea.Right - this.Width;
                App.mw.Top = desktopWorkingArea.Top;
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            App.mw.Content = App.pAyar;
            
        }
    }
}
