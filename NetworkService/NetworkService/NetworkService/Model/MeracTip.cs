using NetworkService.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace NetworkService.Model
{
    public class MeracTip : BindableBase
    {
        private string nazivTipa;
        private string slikaTipa;

        public MeracTip() { }
        public MeracTip(string nazivTipa)
        {
            this.nazivTipa = nazivTipa;
            if(nazivTipa == "Zapreminski")
            {
                slikaTipa = "/NetworkService;component/Slike/zapremisnki.jpg";
            }else if(nazivTipa == "Turbinski")
            {
                slikaTipa = "/NetworkService;component/Slike/turbinski.jpg";
            }else if(nazivTipa == "Elektronski")
            {
                slikaTipa = "/NetworkService;component/Slike/elektronski.jpg";
            }
        }

        public string NazivTipa
        {
            get { return nazivTipa; }
            set
            {
                nazivTipa = value;
                OnPropertyChanged("NazivTipa");
            }
        }

        public string SlikaTipa
        {
            get { return slikaTipa; }
            set
            {
                slikaTipa = value;
                OnPropertyChanged("SlikaTipa");
            }
        }
        public override string ToString()
        {
            return this.nazivTipa;
        }
    }
}
