using System.ComponentModel;

namespace KatalogProduktow4K
{
    //INotifyPropertyChanged powiadamia listę i panel szczegółów o zmianie właściwości,
    //bo ObservableCollection powiadamia tylko o dodaniu i usunięciu elementu
    public class Produkt : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        private void Odswiez(string nazwaWlasciwosci)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nazwaWlasciwosci));
        }

        private string nazwa = "";
        public string Nazwa
        {
            get { return nazwa; }
            set { nazwa = value; Odswiez("Nazwa"); }
        }

        private double cena;
        public double Cena
        {
            get { return cena; }

            set
            {
                cena = value < 0 ? 0 : value;
                Odswiez("Cena");
                Odswiez("WartoscMagazynu");  
            }
        }

        private int ilosc;
        public int Ilosc
        {
            get { return ilosc; }
            set
            {
                ilosc = value;
                Odswiez("Ilosc");
                Odswiez("WartoscMagazynu");   
            }
        }

        private string kategoria = "";
        public string Kategoria
        {
            get { return kategoria; }
            set { kategoria = value; Odswiez("Kategoria"); }
        }

        public bool Promocja { get; set; }

        public double WartoscMagazynu => Cena * Ilosc;
    }
}