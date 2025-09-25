using NetworkService.Helpers;
using NetworkService.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Packaging;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media.TextFormatting;

namespace NetworkService.ViewModel
{
    public class MainWindowViewModel : BindableBase
    {
        private int count = 15; // Inicijalna vrednost broja objekata u sistemu
                                // ######### ZAMENITI stvarnim brojem elemenata
                                //           zavisno od broja entiteta u listi

        private Stack<string> history = new Stack<string>();


        bool fileExists = false;
        string logPath = Environment.CurrentDirectory + @"\log.txt";
        public HomeViewModel HomeVM { get; }

        private object _currentView;
        public object CurrentView
        {
            get => _currentView;
            set { _currentView = value; OnPropertyChanged("CurrentView"); }
        }

        private string _isHomeVisible="Collapsed";
        public string IsHomeVisible
        {
            get => _isHomeVisible;
            set { _isHomeVisible = value; OnPropertyChanged("IsHomeVisable"); }
        }

        private string _sirinaHome = "0";  
        public string SirinaHome
        {
            get => _sirinaHome;
            set { _sirinaHome = value; OnPropertyChanged("SirinaHome"); }
        }

        private string undoEnable = "false";
        public string UndoEnable
        {
            get { return undoEnable; }
            set
            {
                if (undoEnable != value)
                {
                    undoEnable = value;
                    OnPropertyChanged("UndoEnable");
                }
            }
        }

        public ICommand ToggleMenuCommand { get; }
        public MyICommand UndoCommand { get; private set; }


        public EntitiesViewModel EntitiesVM { get; }
        public GraphViewModel GraphVM { get; } 
        public DisplayViewModel DisplayVM { get; } 
        public PocetniViewModel PocetniVM { get; }


        public MainWindowViewModel()
        {
            EntitiesVM=new EntitiesViewModel();
            


            GraphVM =new GraphViewModel();
            DisplayVM=new DisplayViewModel();
            PocetniVM=new PocetniViewModel();
            HomeVM = new HomeViewModel();
            HomeVM.OnNavigationRequested += Navigate;

            UndoCommand = new MyICommand(Undo);
            ToggleMenuCommand = new RelayCommand(_ => ToggleMenu());
            CurrentView = PocetniVM;

            UndoCommand=new MyICommand(Undo);


            createListener(); //Povezivanje sa serverskom aplikacijom
        }
        private void Undo()
        {
            if (history.Count > 0)
            {
                string lastView = history.Pop();
                switch (lastView)
                {
                    case "PocetniView":
                        CurrentView = PocetniVM;
                        break;
                    case "EntitiesView":
                        CurrentView = EntitiesVM;
                        break;
                    case "GraphView":
                        CurrentView = GraphVM;
                        break;
                    case "DisplayView":
                        CurrentView = DisplayVM;
                        break;
                }

                
            }
            if (history.Count == 0)
            {
                    UndoEnable = "false";
            }
        }

        private void Navigate(string destination)
        {
            if (CurrentView == PocetniVM)
            {
                history.Push("PocetniView");
            }
            else if (CurrentView == EntitiesVM)
            {
                history.Push("EntitiesView");
            }
            else if (CurrentView == GraphVM)
            {
                history.Push("GraphView");
            }else if(CurrentView == DisplayVM)
            {
                history.Push("DisplayView");
            }

            if(history.Count > 0)
            {
                UndoEnable = "true";
            }


            switch (destination)
            {
                case "EntitiesView":
                    CurrentView = EntitiesVM;
                    break;
                case "GraphView":
                    CurrentView = GraphVM;
                    break;
                case "DisplayView":
                    CurrentView = DisplayVM;
                    break;
                case "PocetniView":
                    CurrentView = PocetniVM;
                    break;
            }
            

            SirinaHome = "0";
        }


        private void ToggleMenu()
        {
            if (SirinaHome == "0")
            {
                SirinaHome = "200";
                IsHomeVisible = "Visible";
            }
            else
            {
                SirinaHome = "0";
                IsHomeVisible = "Collapsed";
            }
        }




        private void createListener()
        {
            var tcp = new TcpListener(IPAddress.Any, 25675);
            tcp.Start();

            var listeningThread = new Thread(() =>
            {
                while (true)
                {
                    var tcpClient = tcp.AcceptTcpClient();
                    ThreadPool.QueueUserWorkItem(param =>
                    {
                        //Prijem poruke
                        NetworkStream stream = tcpClient.GetStream();
                        string incomming;
                        byte[] bytes = new byte[1024];
                        int i = stream.Read(bytes, 0, bytes.Length);
                        //Primljena poruka je sacuvana u incomming stringu
                        incomming = System.Text.Encoding.ASCII.GetString(bytes, 0, i);

                        //Ukoliko je primljena poruka pitanje koliko objekata ima u sistemu -> odgovor
                        if (incomming.Equals("Need object count"))
                        {
                            //Response
                            /* Umesto sto se ovde salje count.ToString(), potrebno je poslati 
                             * duzinu liste koja sadrzi sve objekte pod monitoringom, odnosno
                             * njihov ukupan broj (NE BROJATI OD NULE, VEC POSLATI UKUPAN BROJ)
                             * */
                            Byte[] data = System.Text.Encoding.ASCII.GetBytes(count.ToString());
                            stream.Write(data, 0, data.Length);
                        }
                        else
                        {
                            //U suprotnom, server je poslao promenu stanja nekog objekta u sistemu
                            Console.WriteLine(incomming); //Na primer: "Entitet_1:272"

                            //################ IMPLEMENTACIJA ####################
                            // Obraditi poruku kako bi se dobile informacije o izmeni
                            // Azuriranje potrebnih stvari u aplikaciji
                            string[] data=incomming.Split('_',':');
                            int id=Int32.Parse(data[1]);
                            int val=Int32.Parse(data[2]);

                            int j = 0;

                            while (j < EntitiesViewModel.Entities.Count)
                            {
                                if (EntitiesViewModel.Entities[j].ID == id)
                                {
                                    EntitiesViewModel.Entities[j].Merenje = val;
                                    break;
                                }
                                j++;
                            }
                            if (fileExists == false)
                            {
                                StreamWriter writer;
                                using (writer = new StreamWriter(logPath))
                                {
                                    writer.WriteLine(DateTime.Now.ToString("dd.MM.yyyy HH:mm") + "," + id + "," + val);
                                }
                                fileExists = true;
                            }
                            else  //ako postoji
                            {
                                StreamWriter writer;
                                using (writer = new StreamWriter(logPath, true))
                                {
                                    writer.WriteLine(DateTime.Now.ToString("dd.MM.yyyy HH:mm") + "," + id + "," + val);
                                }
                            }
                            fileExists = true;
                        }
                    }, null);
                }
            });

            listeningThread.IsBackground = true;
            listeningThread.Start();
        }
    }
}
