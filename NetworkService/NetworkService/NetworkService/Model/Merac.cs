using NetworkService.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace NetworkService.Model
{
    public class Merac:BindableBase
    {
        private int id;
        private string naziv;
        private MeracTip tip;
        private double merenje;

        public Merac(int id, string naziv, MeracTip tip)
        {
            this.id = id;
            this.naziv = naziv;
            this.tip = tip;
        }
        public Merac()
        {
            this.id=0;
            this.naziv = "";
            this.tip=new MeracTip();
            this.merenje=0;
        }
        public int ID
        {
            get { return id; }
            set
            {
                if (id != value)
                {
                    id = value;
                    OnPropertyChanged("ID");
                }
            }
        }
        public string Naziv
        {
            get { return naziv; }
            set
            {
                if (naziv != value)
                {
                    naziv = value;
                    OnPropertyChanged("Naziv");
                }
            }
        }
        public MeracTip Tip
        {
            get { return tip; }
            set
            {
                if (tip != value)
                {
                    tip = value;
                    OnPropertyChanged("Tip");
                }
            }
        }
        public double Merenje
        {
            get { return merenje; }
            set
            {
                if (merenje != value)
                {
                    merenje = value;
                    OnPropertyChanged("Merenje");
                }
            }
        }

        public override string ToString()
        {
            return this.Naziv;
        }
    }
}
