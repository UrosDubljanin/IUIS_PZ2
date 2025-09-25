using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetworkService.Model
{
    public class GrupaMeraca
    {
        public string NazivTipa { get; set; }
        public ObservableCollection<Merac> Meraci { get; set; }

        public GrupaMeraca(string nazivTipa)
        {
            NazivTipa = nazivTipa;
            Meraci = new ObservableCollection<Merac>();
        }
    }
}
