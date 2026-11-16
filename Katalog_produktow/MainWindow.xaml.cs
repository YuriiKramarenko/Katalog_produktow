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
using System.Collections.ObjectModel;
using System.Globalization;

namespace KatalogProduktow4K
{
    public partial class MainWindow : Window
    {
        public ObservableCollection<Produkt> produkty { get; set; } = new ObservableCollection<Produkt>();
        public MainWindow()
        {
            InitializeComponent();
            KategorieComboBox.ItemsSource = new string[] { "Elektronika", "Odzież", "Książki", "Meble" };
            KategorieComboBox.SelectedIndex = 0;
            produkty.Add(new Produkt { Nazwa = "Laptop", Cena = 3500, Ilosc = 5, Kategoria = "Elektronika" });
            produkty.Add(new Produkt { Nazwa = "Koszula", Cena = 120, Ilosc = 10, Kategoria = "Odzież" });
            DataContext = this;
        }

        private void Oblicz(object sender, RoutedEventArgs e)
        {
            string tekstCeny = CenaTextBox.Text.Trim().Replace(',', '.');
            if (!double.TryParse(tekstCeny, NumberStyles.Float, CultureInfo.InvariantCulture, out double cena)
                || double.IsNaN(cena) || double.IsInfinity(cena) || cena < 0)
            {
                WynikTextBlock.Text = "Niepoprawna wartość ceny";
                return;
            }
            if (!int.TryParse(IloscTextBox.Text.Trim(), out int ilosc) || ilosc < 0)
            {
                WynikTextBlock.Text = "Niepoprawna wartość ilości";
                return;
            }


            string kategoria = KategorieComboBox.SelectedItem as string ?? "brak kategorii";

            double wartoscZamowienia = cena * ilosc;

            if (PromocjaCheckBox.IsChecked == true)
            {
                wartoscZamowienia *= 0.9;  
            }

            WynikTextBlock.Text = $"{NazwaTextBox.Text} ({kategoria}): {wartoscZamowienia:F2} zł";
        }

        private void DodajProdukt(object sender, RoutedEventArgs e)
        {
            string nazwa = NazwaTextBox.Text.Trim();
            if (string.IsNullOrWhiteSpace(nazwa))
            {
                MessageBox.Show("Podaj nazwę produktu.");
                return;
            }
            string tekstCeny = CenaTextBox.Text.Trim().Replace(',', '.');
            if (!double.TryParse(tekstCeny, NumberStyles.Float, CultureInfo.InvariantCulture, out double cena)
                || double.IsNaN(cena) || double.IsInfinity(cena) || cena < 0)
            {
                MessageBox.Show("Niepoprawna wartość ceny.");
                return;
            }
            if (!int.TryParse(IloscTextBox.Text.Trim(), out int ilosc) || ilosc < 0)
            {
                MessageBox.Show("Niepoprawna wartość ilości.");
                return;
            }
            string kategoria = KategorieComboBox.SelectedItem as string ?? "brak kategorii";

            produkty.Add(new Produkt
            {
                Nazwa = nazwa,
                Cena = cena,
                Ilosc = ilosc,
                Kategoria = kategoria,
                Promocja = PromocjaCheckBox.IsChecked == true
            });
        }

        private void UsunProdukt(object sender, RoutedEventArgs e)
        {
            if (ListaProduktow.SelectedItem is Produkt wybranyProdukt)
            {
                MessageBoxResult wynik =
                    MessageBox.Show($"Czy na pewno chcesz usunąć produkt: {wybranyProdukt.Nazwa}?",
                    "Potwierdzenie", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (wynik == MessageBoxResult.Yes)
                {
                    produkty.Remove(wybranyProdukt);
                }
            }
            else
            {
                MessageBox.Show("Nie wybrano produktu do usunięcia.", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void PokazZaznaczony(object sender, SelectionChangedEventArgs e)
        {
            if (ListaProduktow.SelectedItem is not Produkt p) return;
            NazwaTextBox.Text = p.Nazwa;
            CenaTextBox.Text = p.Cena.ToString("F2");
            IloscTextBox.Text = p.Ilosc.ToString();
            KategorieComboBox.SelectedItem = p.Kategoria;
            PromocjaCheckBox.IsChecked = p.Promocja;
        }

        private void Zapisz(object sender, RoutedEventArgs e)
        {

            if (ListaProduktow.SelectedItem is not Produkt p)
            {
                MessageBox.Show("Zaznacz produkt do edycji.");
                return;
            }
            string nazwa = NazwaTextBox.Text.Trim();
            if (string.IsNullOrWhiteSpace(nazwa))
            {
                MessageBox.Show("Podaj nazwę produktu.");
                return;
            }

            string tekstCeny = CenaTextBox.Text.Trim().Replace(',', '.');
            if (!double.TryParse(tekstCeny, NumberStyles.Float, CultureInfo.InvariantCulture, out double cena)
                || double.IsNaN(cena) || double.IsInfinity(cena) || cena < 0)
            {
                MessageBox.Show("Niepoprawna wartość ceny.");
                return;
            }
            if (!int.TryParse(IloscTextBox.Text.Trim(), out int ilosc) || ilosc < 0)
            {
                MessageBox.Show("Niepoprawna wartość ilości.");
                return;
            }
    
            p.Nazwa = nazwa;
            p.Cena = cena;
            p.Ilosc = ilosc;
            p.Kategoria = KategorieComboBox.SelectedItem as string ?? "brak kategorii";
            p.Promocja = PromocjaCheckBox.IsChecked == true;
            WynikTextBlock.Text = $"Zapisano zmiany dla produktu: {p.Nazwa}";
        }
    }
}