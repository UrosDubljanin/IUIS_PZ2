using NetworkService.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetworkService.ViewModel
{
    public class TastaturaViewModel : BindableBase
    {
        public MyICommand<string> KlikniSlovo {  get; set; }
        public MyICommand KlikniEnter {  get; set; }
        public MyICommand KlikniSpace { get; set; }
        public MyICommand IzbrisiSlovo { get; set; }
        public event Action OnWordCompleted;
        public event Action<string> NaUnetoSlovo;
        public event Action Obrisi;

        private string isEnable = "true";

        public string IsEnable
        {
            get { return isEnable; }
            set
            {
                if (isEnable != value)
                {
                    isEnable = value;
                    OnPropertyChanged("IsEnable");
                }
            }
        }

        public TastaturaViewModel() {
            KlikniSlovo = new MyICommand<string>(Slovo);
            KlikniEnter = new MyICommand(Enter);
            IzbrisiSlovo = new MyICommand(Izbrisi);
        }
        private void Slovo(string slovo)
        {
            NaUnetoSlovo?.Invoke(slovo);
        }
        private void Enter()
        {
            OnWordCompleted?.Invoke();
        }
        private void Izbrisi()
        {
            Obrisi?.Invoke();
        }
    }
}
