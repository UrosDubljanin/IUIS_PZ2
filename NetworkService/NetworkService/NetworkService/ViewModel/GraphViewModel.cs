using NetworkService.Helpers;
using NetworkService.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace NetworkService.ViewModel
{
    public class GraphViewModel : BindableBase
    {
        private FileSystemWatcher watcher;

        private ObservableCollection<double> merenja;
        private ObservableCollection<DateTime> vreme;
        private ObservableCollection<Merac> entiteti;
        private Merac izabraniMerac;
        public Merac IzabraniMerac
        {
            get { return izabraniMerac; }
            set
            {
                if(izabraniMerac != value)
                {
                    izabraniMerac = value;
                    PromenaMeraca();
                    OnPropertyChanged("IzabraniMerac");
                }
            }
        }

        private string prviVrednost="0";
        private string drugiVrednost="0";
        private string treciVrednost = "0";
        private string cetvrtiVrednost = "0";
        private string petiVrednost = "0";

        public string PrviVrednost
        {
            get { return prviVrednost; }
            set
            {
                if (prviVrednost != value)
                {
                    prviVrednost = value;
                    OnPropertyChanged("PrviVrednost");
                }
            }
        }
        public string DrugiVrednost
        {
            get { return drugiVrednost; }
            set
            {
                if (drugiVrednost != value)
                {
                    drugiVrednost = value;
                    OnPropertyChanged("DrugiVrednost");
                }
            }
        }
        public string TreciVrednost
        {
            get { return treciVrednost; }
            set
            {
                if (treciVrednost != value)
                {
                    treciVrednost = value;
                    OnPropertyChanged("TreciVrednost");
                }
            }
        }
        public string CetvrtiVrednost
        {
            get { return cetvrtiVrednost; }
            set
            {
                if (cetvrtiVrednost != value)
                {
                    cetvrtiVrednost = value;
                    OnPropertyChanged("CetvrtiVrednost");
                }
            }
        }
        public string PetiVrednost
        {
            get { return petiVrednost; }
            set
            {
                if (petiVrednost != value)
                {
                    petiVrednost = value;
                    OnPropertyChanged("PetiVrednost");
                }
            }
        }

        private string prviBoja = "Transparent";
        private string drugiBoja = "Transparent";
        private string treciBoja = "Transparent";
        private string cetvrtiBoja = "Transparent";
        private string petiBoja = "Transparent";

        public string PrviBoja
        {
            get { return prviBoja; }
            set
            {
                if (prviBoja != value)
                {
                    prviBoja = value;
                    OnPropertyChanged("PrviBoja");
                }
            }
        }
        public string DrugiBoja
        {
            get { return drugiBoja; }
            set
            {
                if (drugiBoja != value)
                {
                    drugiBoja = value;
                    OnPropertyChanged("DrugiBoja");
                }
            }
        }
        public string TreciBoja
        {
            get { return treciBoja; }
            set
            {
                if (treciBoja != value)
                {
                    treciBoja = value;
                    OnPropertyChanged("TreciBoja");
                }
            }
        }
        public string CetvrtiBoja
        {
            get { return cetvrtiBoja; }
            set
            {
                if (cetvrtiBoja != value)
                {
                    cetvrtiBoja = value;
                    OnPropertyChanged("CetvrtiBoja");
                }
            }
        }
        public string PetiBoja
        {
            get { return petiBoja; }
            set
            {
                if (petiBoja != value)
                {
                    petiBoja = value;
                    OnPropertyChanged("PetiBoja");
                }
            }
        }
        private string prviVreme = "0";
        private string drugiVreme = "0";
        private string treciVreme = "0";
        private string cetvrtiVreme = "0";
        private string petiVreme = "0";

        public string PrviVreme
        {
            get { return prviVreme; }
            set
            {
                if (prviVreme != value)
                {
                    prviVreme = value;
                    OnPropertyChanged("PrviVreme");
                }
            }
        }
        public string DrugiVreme
        {
            get { return drugiVreme; }
            set
            {
                if (drugiVreme != value)
                {
                    drugiVreme = value;
                    OnPropertyChanged("DrugiVreme");
                }
            }
        }
        public string TreciVreme
        {
            get { return treciVreme; }
            set
            {
                if (treciVreme != value)
                {
                    treciVreme = value;
                    OnPropertyChanged("TreciVreme");
                }
            }
        }
        public string CetvrtiVreme
        {
            get { return cetvrtiVreme; }
            set
            {
                if (cetvrtiVreme != value)
                {
                    cetvrtiVreme = value;
                    OnPropertyChanged("CetvrtiVreme");
                }
            }
        }
        public string PetiVreme
        {
            get { return petiVreme; }
            set
            {
                if (petiVreme != value)
                {
                    petiVreme = value;
                    OnPropertyChanged("PetiVreme");
                }
            }
        }

        private string prviLevo = "0";
        private string drugiLevo = "0";
        private string treciLevo = "0";
        private string cetvrtiLevo = "0";
        private string petiLevo = "0";

        public string PrviLevo
        {
            get { return prviLevo; }
            set
            {
                if (prviLevo != value)
                {
                    prviLevo = value;
                    OnPropertyChanged("PrviLevo");
                }
            }
        }
        public string DrugiLevo
        {
            get { return drugiLevo; }
            set
            {
                if (drugiLevo != value)
                {
                    drugiLevo = value;
                    OnPropertyChanged("DrugiLevo");
                }
            }
        }
        public string TreciLevo
        {
            get { return treciLevo; }
            set
            {
                if (treciLevo != value)
                {
                    treciLevo = value;
                    OnPropertyChanged("TreciLevo");
                }
            }
        }
        public string CetvrtiLevo
        {
            get { return cetvrtiLevo; }
            set
            {
                if (cetvrtiLevo != value)
                {
                    cetvrtiLevo = value;
                    OnPropertyChanged("CetvrtiLevo");
                }
            }
        }
        public string PetiLevo
        {
            get { return petiLevo; }
            set
            {
                if (petiLevo != value)
                {
                    petiLevo = value;
                    OnPropertyChanged("PetiLevo");
                }
            }
        }

        private string prviGore = "0";
        private string drugiGore = "0";
        private string treciGore = "0";
        private string cetvrtiGore = "0";
        private string petiGore = "0";

        public string PrviGore
        {
            get { return prviGore; }
            set
            {
                if (prviGore != value)
                {
                    prviGore = value;
                    OnPropertyChanged("PrviGore");
                }
            }
        }
        public string DrugiGore
        {
            get { return drugiGore; }
            set
            {
                if (drugiGore != value)
                {
                    drugiGore = value;
                    OnPropertyChanged("DrugiGore");
                }
            }
        }
        public string TreciGore
        {
            get { return treciGore; }
            set
            {
                if (treciGore != value)
                {
                    treciGore = value;
                    OnPropertyChanged("TreciGore");
                }
            }
        }
        public string CetvrtiGore
        {
            get { return cetvrtiGore; }
            set
            {
                if (cetvrtiGore != value)
                {
                    cetvrtiGore = value;
                    OnPropertyChanged("CetvrtiGore");
                }
            }
        }
        public string PetiGore
        {
            get { return petiGore; }
            set
            {
                if (petiGore != value)
                {
                    petiGore = value;
                    OnPropertyChanged("PetiGore");
                }
            }
        }


        public GraphViewModel() {
            Entiteti = new ObservableCollection<Merac>(EntitiesViewModel.Entities);

            Merenja = new ObservableCollection<double>();
            Vreme = new ObservableCollection<DateTime>();

            string logFilePath = System.IO.Path.Combine(Environment.CurrentDirectory, "log.txt");
            if (File.Exists(logFilePath))
            {
                watcher = new FileSystemWatcher(Environment.CurrentDirectory, "log.txt");
                watcher.NotifyFilter = NotifyFilters.LastWrite | NotifyFilters.Size;
                watcher.Changed += (s, e) =>
                {
                    // debounce jer se Changed okida 2x
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        PromenaMeraca();
                    });
                };
                watcher.EnableRaisingEvents = true;
            }
        }
        private void PromenaMeraca()
        {
            if (IzabraniMerac != null)
            {
                //Citanje poslijednjih 5 mjerenja za izabrani entitet
                string logFilePath = Environment.CurrentDirectory + @"\log.txt"; // Putanja
                if (File.Exists(logFilePath))
                {
                    var lines = File.ReadAllLines(logFilePath);
                    //izvlacimo informacije splitovanjem podataka iz fajla
                    var derMerenja = lines
                        .Select(line =>
                        {
                            var parts = line.Split(',');
                            return new
                            {
                                DateTime = DateTime.ParseExact(parts[0], "dd.MM.yyyy HH:mm", CultureInfo.InvariantCulture),
                                Id = int.Parse(parts[1]),
                                Merenje = double.Parse(parts[2])
                            };
                        })
                        .Where(entry => entry.Id == IzabraniMerac.ID)
                        .OrderByDescending(entry => entry.DateTime)
                        .Take(5)
                        .Select(entry => entry.Merenje);
                    //izvlacimo vrijeme
                    var derTime = lines
                        .Select(line =>
                        {
                            var parts = line.Split(',');
                            return new
                            {
                                DateTime = DateTime.ParseExact(parts[0], "dd.MM.yyyy HH:mm", CultureInfo.InvariantCulture),
                                Id = int.Parse(parts[1]),
                                Measurement = double.Parse(parts[2])
                            };
                        })
                        .Where(entry => entry.Id == IzabraniMerac.ID)
                        .OrderByDescending(entry => entry.DateTime)
                        .Take(5)
                        .Select(entry => entry.DateTime);


                    Merenja.Clear();
                    foreach (var m in derMerenja)
                    {
                        Merenja.Add(m);
                    }
                    // Update the Vrijeme collection
                    Vreme.Clear();
                    foreach (var v in derTime)
                    {
                        Vreme.Add(v);
                    }
                }
                else
                {
                    MessageBox.Show("Log file not found.");
                }
            }
            else
            {
                Merenja.Clear();
                Vreme.Clear();
            }


            UpdateGraph();
        }
        private void UpdateGraph()
        {
            //Cistimo Canvas
            PrviVrednost = "0";
            DrugiVrednost = "0";
            TreciVrednost = "0";
            CetvrtiVrednost = "0";
            PetiVrednost = "0";

            if (Merenja.Count > 0) //sacuvano poslijednjih 5 mjerenja za odredjeni entitet
            {

                for (int i = 0; i < Vreme.Count; i++)
                {
                    if (i == 0)
                    {
                        PrviVreme = IzvuciVreme(Vreme[0]);
                    }
                    else if (i == 1)
                    {
                        DrugiVreme = IzvuciVreme(Vreme[1]);
                    }
                    else if (i == 2)
                    {
                        TreciVreme = IzvuciVreme(Vreme[2]);
                    }
                    else if (i == 3)
                    {
                        CetvrtiVreme = IzvuciVreme(Vreme[3]);
                    }
                    else if (i == 4)
                    {
                        PetiVreme = IzvuciVreme(Vreme[4]);
                    }
                }

                //prolazimo kroz sva mjerenja
                for (int i = 0; i < Merenja.Count; i++)
                {
                    double vrednost = Merenja[i];
                    string boja = "Transparent";
                    
                    double skalirano = 0;
                    

                    if (vrednost >= 670 && vrednost <= 735)
                    {
                        skalirano = SkaliranjeVrednosti(vrednost);
                        boja = "Green";
                    }else if (vrednost < 670)
                    {
                        skalirano = SkaliranjeVrednosti(vrednost);
                        boja = "Yellow";
                    }else if (vrednost > 735)
                    {
                        skalirano = SkaliranjeVrednosti(vrednost);
                        boja = "Red";
                    }
                    double mesto = 240 - (skalirano / 2);
                    double levo1 = 60 - (skalirano / 2);
                    double levo2 = 140 - (skalirano / 2);
                    double levo3 = 220 - (skalirano / 2);
                    double levo4 = 300 - (skalirano / 2);
                    double levo5 = 380 - (skalirano / 2);

                    switch (i)
                    {
                        case 0:
                            PrviVrednost = skalirano.ToString();
                            PrviGore = mesto.ToString();
                            PrviLevo = levo1.ToString();
                            PrviBoja = boja;
                            break;
                        case 1:
                            DrugiVrednost = skalirano.ToString();
                            DrugiGore = mesto.ToString();
                            DrugiLevo = levo2.ToString();
                            DrugiBoja = boja;
                            break;
                        case 2:
                            TreciVrednost = skalirano.ToString();
                            TreciGore = mesto.ToString();
                            TreciLevo = levo3.ToString();
                            TreciBoja = boja;
                            break;
                        case 3:
                            CetvrtiVrednost = skalirano.ToString();
                            CetvrtiGore = mesto.ToString();
                            CetvrtiLevo = levo4.ToString();
                            CetvrtiBoja = boja;
                            break;
                        case 4:
                            PetiVrednost = skalirano.ToString();
                            PetiGore = mesto.ToString();
                            PetiLevo = levo5.ToString();
                            PetiBoja = boja;
                            break;
                    }
                }
            }
        }
        private string IzvuciVreme(DateTime dateTime)
        {
            return dateTime.ToString("HH:mm");
        }
        public ObservableCollection<Merac> Entiteti
        {
            get { return entiteti; }
            set { SetProperty(ref entiteti, value); }
        }

        public ObservableCollection<double> Merenja
        {
            get { return merenja; }
            set { SetProperty(ref merenja, value); }
        }
        public ObservableCollection<DateTime> Vreme
        {
            get { return vreme; }
            set { SetProperty(ref vreme, value); }
        }

        public double SkaliranjeVrednosti(double value)
        {
            double minSrc = 670;
            double maxSrc = 735;
            double minDst = 35;
            double maxDst = 50;

            return (value - minSrc) / (maxSrc - minSrc) * (maxDst - minDst) + minDst;
        }


    }
}
