using System;
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
using Microsoft.Win32;
using System.Collections;

namespace carpimTablosuOgret
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class mainApp : Window
    {
        public mainApp()
        {
            InitializeComponent();
        }
        // Bu pencere kapanırsa tüm app kapanır ana app budur. bu yüzden yönlendirmeyi yapıp kendisini gizleyeceğiz daha sonra
        // Windows içerğini page çektiğimde page içeriği geldikten app de statik değişebilecek şekilde ayarladığım windowu istediğim zaman erişebileceğim
        // bu sayede Window kontrolünü page sayfalarındada yapabilecek durumda olacağım.
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            App.mw.Show();
            this.Hide();
        }

       
    }
}
