using NetworkService.Helpers;
using NetworkService.Model;
using NetworkService.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace NetworkService.ViewModel
{
    public class DisplayViewModel : BindableBase
    {
        private static Merac selektovan = new Merac();
        public Merac Selektovan
        {
            get { return selektovan; }
            set { selektovan = value;
                OnPropertyChanged("Selektovan");
            }
        }
        private Merac trenutnaStavka=new Merac();
        public Merac TrenutnaStavka
        {
            get
            {
                return trenutnaStavka;
            }
            set
            {
                trenutnaStavka = value;
                OnPropertyChanged("TrenutnaStavka");
            }
        }
        private bool dragging = false;
        private bool fromList = true;




        private ObservableCollection<Merac> meraci;
        public ObservableCollection<Merac> Meraci
        {
            get { return meraci; }
            set
            {
                if (meraci != value)
                {
                    meraci = value;                   
                    OnPropertyChanged("Meraci");
                }
            }
        }
        private ObservableCollection<Merac> zapreminskiMeraci;
        public ObservableCollection<Merac> ZapreminskiMeraci
        {
            get { return zapreminskiMeraci; }
            set
            {
                if (zapreminskiMeraci != value)
                {
                    zapreminskiMeraci = value;
                    OnPropertyChanged("ZapreminskiMeraci");
                }
            }
        }
        private ObservableCollection<Merac> turbinskiMeraci;
        public ObservableCollection<Merac> TurbinskiMeraci
        {
            get { return turbinskiMeraci; }
            set
            {
                if (turbinskiMeraci != value)
                {
                    turbinskiMeraci = value;
                    OnPropertyChanged("TurbinskiMeraci");
                }
            }
        }
        private ObservableCollection<Merac> elektronskiMeraci;
        public ObservableCollection<Merac> ElektronskiMeraci
        {
            get { return elektronskiMeraci; }
            set
            {
                if (elektronskiMeraci != value)
                {
                    elektronskiMeraci = value;
                    OnPropertyChanged("ElektronskiMeraci");
                }
            }
        }
        public DisplayViewModel() {
            Meraci= new ObservableCollection<Merac>();
            if (EntitiesViewModel.Entities != null)
            {
                Meraci = EntitiesViewModel.Entities;
            }
            

            ZapreminskiMeraci = new ObservableCollection<Merac>();
            TurbinskiMeraci= new ObservableCollection<Merac>();
            ElektronskiMeraci=new ObservableCollection<Merac>();


            Rasporedi();
        }
        
        


        private void Rasporedi()
        {
            if (Meraci.Count > 0)
            {
                ZapreminskiMeraci.Clear();
                TurbinskiMeraci.Clear();
                ElektronskiMeraci.Clear();
                foreach (Merac m in Meraci)
                {
                    if (m.Tip.NazivTipa == "Turbinski")
                    {
                        TurbinskiMeraci.Add(m);
                    }
                    else if (m.Tip.NazivTipa == "Elektronski")
                    {
                        ElektronskiMeraci.Add(m);
                    }
                    else if (m.Tip.NazivTipa == "Zapreminski")
                    {
                        ZapreminskiMeraci.Add(m);
                    }
                }
            }
        }

    }
}
