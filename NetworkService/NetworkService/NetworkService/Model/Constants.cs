using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetworkService.Model
{
    public class Constants
    {
        static List<string> tipovi = new List<string>() { "Zapreminski","Turbinski","Elektronski"};

        public static List<MeracTip> TipoviMeraca = new List<MeracTip>
        {
            new MeracTip(tipovi[0]),
            new MeracTip(tipovi[1]),
            new MeracTip(tipovi[2])
        };
    }
}
