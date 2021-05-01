using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using System.Xml;
using System.Xml.Linq;

namespace carpimTablosuOgret
{
    /// <summary>
    /// Interaction logic for Page2.xaml
    /// </summary>
    public partial class PageAyarlar : Page
    {
        public PageAyarlar()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            App.mw.Content = App.pageMain;
        }

        XmlDocument xml;
        XmlNodeList xmlNodelst;
        string fileName = System.AppDomain.CurrentDomain.BaseDirectory + "\\db.xml";
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            if (!System.IO.File.Exists(fileName))
            {
                label1.Content = "Dosya bulunamadı. | " + fileName;
            }
            else
            {
                comboBox1.Items.Clear();
                comboBox2.Items.Clear();
                xml = new XmlDocument();
                xml.Load(fileName);
                // xml bilgileri çekiliyor                

                xmlNodelst = xml.GetElementsByTagName("saniyeler");

                foreach (XmlNode node in xmlNodelst)
                {
                    comboBox1.Items.Add(node["saniye"].InnerText);
                }

                xmlNodelst = xml.GetElementsByTagName("sorular");

                foreach (XmlNode node in xmlNodelst)
                {
                    comboBox2.Items.Add(node["soru"].InnerText);
                }


                xmlNodelst = xml.GetElementsByTagName("programKaydi");

                foreach (XmlNode node in xmlNodelst)
                {
                    comboBox1.SelectedIndex = comboBox1.Items.IndexOf(node["saniye"].InnerText);
                    comboBox2.SelectedIndex = comboBox2.Items.IndexOf(node["soru"].InnerText);
                }


            }
        }
        

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void comboBox1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                xmlNodelst = xml.GetElementsByTagName("programKaydi");

                foreach (XmlNode node in xmlNodelst)
                {
                    node["saniye"].InnerText = comboBox1.SelectedItem.ToString();
                }

                xml.Save(fileName);
            }
            catch { } // program çalıştığında bilgiler yüklenemdiği için hata alıyor


        }

        private void comboBox2_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                xmlNodelst = xml.GetElementsByTagName("programKaydi");

                foreach (XmlNode node in xmlNodelst)
                {
                    node["soru"].InnerText = comboBox2.SelectedItem.ToString();
                }

                xml.Save(fileName);
            }
            catch { } // program çalıştığında bilgiler yüklenemdiği için hata alıyor
        }



    }
}
