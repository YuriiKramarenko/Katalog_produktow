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
using System.Xml.Linq;

namespace Katalog_produktow
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }




        private void Oblicz(object sender, RoutedEventArgs e)
        {
            double cena;
            double ilosc;

            if (double.TryParse(CenaTextBox.Text, out cena) && double.TryParse(IloscTextBox.Text, out ilosc))
            {
                if (cena < 0 || ilosc < 0)
                {
                    WynikTextBlock.Text = "Podaj poprawnw wartości";
                }
                else
                {
                    WynikTextBlock.Text = "Wynik: " + cena * ilosc;
                }
                
            }
            else
            {
                WynikTextBlock.Text = "Podaj poprawnw wartości";
            }
        }
    }
}