using NetworkService.Helpers;
using NetworkService.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using static System.Net.Mime.MediaTypeNames;

namespace NetworkService.ViewModel
{
    public class EntitiesViewModel : BindableBase
    {
        private bool prvi = true;
        public static string last = "";
        public static string lastSearch = "";
        public static Merac lastItem = new Merac();

        public static ObservableCollection<Merac> lastList { get; set; }
        public static ObservableCollection<Merac> Entities { get; set; } = new ObservableCollection<Merac>(); //sve u tabeli
        
        public ObservableCollection<Merac> SearchEntities { get; set; } = new ObservableCollection<Merac>(); //u tabeli poslije search-a
        public ObservableCollection<MeracTip> TipoviMeraca { get; set; } //lista tipova
        public ICollectionView EntitiesCollectionView { get; }  //za manipulaciju nad podacima (filter, search)

        public MyICommand DodajCommand { get; set; }
        public MyICommand IzbrisiCommand { get; set; }
        public MyICommand PretraziCommand { get; set; }
        public MyICommand RestartCommand { get; set; }
        public MyICommand FocusTextBox {  get; set; }
        public MyICommand FocusBrojBox {  get; set; }
        public MyICommand LosFocusTextBox {  get; set; }

        private Merac selektovaniEntitet = new Merac();
        private int idCurrent;  //sve sto binding radimo sa tim mora biti private
        private string nameCurrent;

        private MeracTip odabranaOpcijaDodaj = null;

        private string textBoxSearch;   
        private bool isVeceChecked;
        private bool isManjeChecked;
        private bool isJednakoChecked;


        public Merac SelektovaniEntitet
        {
            get { return selektovaniEntitet; }
            set
            {
                if (selektovaniEntitet != value)
                {
                    selektovaniEntitet = value;
                    OnPropertyChanged("SelektovaniEntitet");
                }
            }
        }
        public MeracTip OdabranaOpcijaDodaj
        {
            get { return odabranaOpcijaDodaj; }
            set
            {
                if (odabranaOpcijaDodaj != value)
                {
                    odabranaOpcijaDodaj = value;
                    OnPropertyChanged("OdabranaOpcijaDodaj");
                }
            }
        }
        private MeracTip odabranaOpcijaPretrazi;
        public MeracTip OdabranaOpcijaPretrazi
        {
            get { return odabranaOpcijaPretrazi; }
            set
            {
                if (odabranaOpcijaPretrazi != value)
                {
                    odabranaOpcijaPretrazi = value;
                    OnPropertyChanged("OdabranaOpcijaPretrazi");
                }
            }
        }
        public TastaturaViewModel TastaturaVM {  get; }
        private string _isTastaturaVisible = "Collapsed";
        public string IsTastaturaVisible
        {
            get => _isTastaturaVisible;
            set { _isTastaturaVisible = value; OnPropertyChanged("IsTastaturaVisable"); }
        }

        private string _visinaTastature = "0";
        public string VisinaTastature
        {
            get => _visinaTastature;
            set { _visinaTastature = value; OnPropertyChanged("VisinaTastature"); }
        }
        private string _textBoxContent;
        private string _lastValidContent;

        public string TextBoxContent
        {
            get => _textBoxContent;
            set
            {
                _textBoxContent = value;
                OnPropertyChanged(nameof(TextBoxContent));
            }
        }

        private string _canClickButton = "true";
        public string CanClickButton
        {
            get => _canClickButton;
            set
            {
                _canClickButton = value;
                OnPropertyChanged("CanClickButton");
            }
        }
        private string _canWriteBroj="false";
        public string CanWriteBroj
        {
            get => _canWriteBroj;
            set
            {
                _canWriteBroj = value;
                OnPropertyChanged("CanWriteBroj");
            }
        }
        private string _canWriteText="false";
        public string CanWriteText
        {
            get => _canWriteText;
            set
            {
                _canWriteText = value;
                OnPropertyChanged("CanWriteText");
            }
        }
        private string borderTip1 = "Black";
        public string BorderTip1
        {
            get { return borderTip1; }
            set
            {
                borderTip1 = value;
                OnPropertyChanged(nameof(BorderTip1));
            }
        }
        private string borderTip2 = "Black";
        public string BorderTip2 
        {
            get { return borderTip2; }
            set
            {
                borderTip2 = value;
                OnPropertyChanged(nameof(BorderTip2));
            }
        }
        private string borderText = "Black";
        public string BorderText
        {
            get { return borderText; }
            set
            {
                borderText = value;
                OnPropertyChanged(nameof(BorderText));
            }
        }
        private string borderBroj = "Black";
        public string BorderBroj
        {
            get { return borderBroj; }
            set
            {
                borderBroj = value;
                OnPropertyChanged(nameof(BorderBroj));
            }
        }


        public EntitiesViewModel() 
        {
            TextBoxContent = "Naziv meraca";
            _lastValidContent = TextBoxContent;

            


            EntitiesCollectionView = CollectionViewSource.GetDefaultView(Entities);
            TipoviMeraca = new ObservableCollection<MeracTip>();

            TipoviMeraca.Add(Constants.TipoviMeraca[0]);
            TipoviMeraca.Add(Constants.TipoviMeraca[1]);
            TipoviMeraca.Add(Constants.TipoviMeraca[2]);

            if (Entities.Count==0)
            {
                Entities.Add(new Merac(0, "Merac_0", Constants.TipoviMeraca[0]));
                Entities.Add(new Merac(1, "Merac_1", Constants.TipoviMeraca[1]));
                Entities.Add(new Merac(2, "Merac_2", Constants.TipoviMeraca[2]));
                prvi = false;
            }

            DodajCommand = new MyICommand(OnAdd);
            IzbrisiCommand = new MyICommand(OnDelete);
            PretraziCommand = new MyICommand(OnSearch);
            RestartCommand = new MyICommand(OnRestart);
            FocusTextBox = new MyICommand(FocusTB);
            FocusBrojBox = new MyICommand(FocusBB);
            LosFocusTextBox=new MyICommand(LostFocusTB);
            

            TextBoxSearch = "ID...";
            lastSearch = "";

            TastaturaVM=new TastaturaViewModel();
            TastaturaVM.NaUnetoSlovo += UnetoSlovo;
            TastaturaVM.OnWordCompleted += LostFocusTB;
            TastaturaVM.Obrisi += IzbrisiSlovo;
        }
        private void LostFocusTB()
        {
            if (VisinaTastature == "200")
            {
                VisinaTastature = "0";
                IsTastaturaVisible = "Collapsed";
            }
            if (string.IsNullOrWhiteSpace(TextBoxContent))
                TextBoxContent = _lastValidContent;
            else
                _lastValidContent = TextBoxContent;

            CanClickButton = "true";
            CanWriteBroj = "false";
            CanWriteText = "false";
        }
        private void FocusTB()
        {
            if (VisinaTastature == "0")
            {
                VisinaTastature = "200";
                IsTastaturaVisible = "Visible";
            }
            BorderText = "Black";
            TextBoxContent = "";

            TastaturaVM.IsEnable = "true";
            CanClickButton = "false";
            CanWriteBroj = "true";

        }
        private void FocusBB()
        {
            if (VisinaTastature == "0")
            {
                VisinaTastature = "200";
                IsTastaturaVisible = "Visible";
            }
            BorderBroj = "Black";
            TextBoxSearch = "";

            TastaturaVM.IsEnable = "false";
            CanClickButton = "false";
            CanWriteText = "true";

        }
        private void IzbrisiSlovo()
        {
            if (TastaturaVM.IsEnable=="true")
            {
                if (!string.IsNullOrWhiteSpace(TextBoxContent))
                {
                    TextBoxContent = TextBoxContent.Substring(0, TextBoxContent.Length - 1);
                }
            }
            else
            {
                if (!string.IsNullOrWhiteSpace(TextBoxSearch))
                {
                    TextBoxSearch = TextBoxSearch.Substring(0, TextBoxSearch.Length - 1);
                }
            }

        }
        private void UnetoSlovo(string slovo)
        {
            if (TastaturaVM.IsEnable == "true")
            {
                TextBoxContent += slovo;
            }
            else
            {
                TextBoxSearch += slovo;
            }
        }
        private void OnAdd()
        {
            last = "add";
            Merac m = new Merac();

            if (!string.IsNullOrWhiteSpace(TextBoxContent) && TextBoxContent!="Naziv meraca")
            {
                BorderText = "Black";
                if (OdabranaOpcijaDodaj != null)
                {
                    BorderTip2 = "Black";
                    m.Naziv = TextBoxContent;
                    bool ima = true;
                    int i = 0;
                    while (ima)
                    {
                        i++;
                        foreach (Merac merac in Entities)
                        {
                            if (merac.ID == i)
                            {
                                ima = true;
                                break;
                            }
                            else
                            {
                                ima = false;
                            }
                        }
                    }
                    m.ID = i;
                    m.Tip=OdabranaOpcijaDodaj;
                    Entities.Add(m);
                    TextBoxContent = "Naziv";
                    OdabranaOpcijaDodaj = null;
                }
                else
                {
                    MessageBox.Show("Moras izbrati tip meraca!!!");
                    BorderTip2 = "Red";
                }
            }
            else
            {
                MessageBox.Show("Moras uneti naziv meraca!!!");
                BorderText = "Red";
            }
        }
        private void OnDelete()
        {
            if (SelektovaniEntitet == null) return;
            last = "delete";
            int id = SelektovaniEntitet.ID;
            for (int i = 0; i < Entities.Count; i++)
            {
                if (id == Entities[i].ID)
                {
                    lastItem = Entities[i];
                    Entities.RemoveAt(i);
                    SelektovaniEntitet=null;
                    break;
                }
            }
        }
        private void OnSearch()
        {
            last = "serach";
            if (!string.IsNullOrEmpty(TextBoxSearch))
            {
                BorderBroj = "Black";
                if (IsJednakoChecked)
                {
                    EntitiesCollectionView.Filter = item => ((Merac)item).ID==(Int32.Parse(TextBoxSearch));
                }else if (IsVeceChecked)
                {
                    EntitiesCollectionView.Filter = item => ((Merac)item).ID>(Int32.Parse(TextBoxSearch));
                }else if (IsManjeChecked)
                {
                    EntitiesCollectionView.Filter = item => ((Merac)item).ID<(Int32.Parse(TextBoxSearch));
                }
                else
                {
                    MessageBox.Show("Moras izabrati jedan od operatora(<,>,=)!!!");
                }
            }
            if (OdabranaOpcijaPretrazi != null)
            {
                BorderTip1 = "Black";
                EntitiesCollectionView.Filter = item => ((Merac)item).Tip.NazivTipa==OdabranaOpcijaPretrazi.NazivTipa;
            }
            if(string.IsNullOrEmpty(TextBoxSearch) && OdabranaOpcijaPretrazi == null)
            {
                MessageBox.Show("Moras uneti vrednost indeksa ako hoces da vrsis pretragu preko njega ili izbrati tip!!!");
                BorderBroj = "Red";
                BorderTip1 = "Red";
            }

        }
        private void OnRestart()
        {
            last = "restart";
            EntitiesCollectionView.Filter = null;
            OdabranaOpcijaPretrazi = null;
            TextBoxSearch = "ID";
            IsJednakoChecked = false;
            IsManjeChecked= false;
            IsVeceChecked = false;
            BorderText = "Black";
            BorderBroj = "Black";
            BorderTip1 = "Black";
            BorderTip2 = "Black";
        }

        public string TextBoxSearch
        {
            get { return textBoxSearch; }
            set
            {
                textBoxSearch = value;
                OnPropertyChanged("TextBoxSearch");
            }
        }
        public bool IsVeceChecked
        {
            get { return isVeceChecked; }
            set
            {
                if (isVeceChecked != value)
                {
                    isVeceChecked = value;
                    OnPropertyChanged(nameof(IsVeceChecked));

                    if (isVeceChecked)
                    {
                        IsManjeChecked = false;
                        IsJednakoChecked = false;
                    }
                }
            }
        }
        public bool IsManjeChecked
        {
            get { return isManjeChecked; }
            set
            {
                if (isManjeChecked != value)
                {
                    isManjeChecked = value;
                    OnPropertyChanged(nameof(IsManjeChecked));

                    if (isManjeChecked)
                    {
                        IsVeceChecked = false;
                        IsJednakoChecked = false;
                    }
                }
            }
        }
        public bool IsJednakoChecked
        {
            get { return isJednakoChecked; }
            set
            {
                if (isJednakoChecked != value)
                {
                    isJednakoChecked = value;
                    OnPropertyChanged(nameof(IsJednakoChecked));

                    if (isJednakoChecked)
                    {
                        IsVeceChecked = false;
                        IsManjeChecked = false;
                    }
                }
            }
        }



    }
}
