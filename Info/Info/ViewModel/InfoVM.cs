using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace Info
{
    public class InfoVM : ViewModelBase
    {
        #region Private Fields
        INavigation Navigation;

        Page CurrentPage;

        private ObservableCollection<Instruments> instruments;

        private ObservableCollection<string> floorList;

        private string stateInfo;

        private Instruments selectedInstrument;


        private string selectedFloor;

        #endregion

        #region Public Fields

        public string dbPath;

        public ObservableCollection<Instruments> Instruments
        {
            get => instruments;
            set
            {
                instruments = value;
                OnPropertyChanged("Instruments");
            }
        }

        public ObservableCollection<string> FloorList
        {
            get => floorList;
            set
            {
                floorList = value;
                OnPropertyChanged("FloorList");
            }
        }

        public Instruments SelectedInstrument
        {
            get => selectedInstrument;
            set
            {
                selectedInstrument = value;
                OnPropertyChanged("SelectedInstrument");
            }
        }

        public string SelectedFloor
        {
            get => selectedFloor;
            set
            {
                ChooseFloor(value);
                selectedFloor = value;
                OnPropertyChanged("SelectedFloor");
            }
        }

        #endregion

        #region Commands

        public ICommand DelButtonCommand { get; }

        public ICommand EditButtonCommand { get; }

        public ICommand MapButtonCommand { get; }

        public async void DeleteInstrument()
        {
            if (SelectedInstrument != null)
            {
                bool result = await CurrentPage.DisplayAlert("Подтверждение", "Удаляем?", "Да", "Нет");
                if (result)
                {
                    using (ZavodDB db = new ZavodDB(dbPath))
                    {
                        db.Remove(SelectedInstrument);
                        db.SaveChanges();
                        Instruments.Remove(SelectedInstrument);
                    }
                }
            }
        }

        public void EditInstrument()
        {
            if (SelectedInstrument != null)
            {
                ((MasterDetailPage)CurrentPage.Parent.Parent).Detail = new NavigationPage(new EditPage(SelectedInstrument));
            }
        }

        public void MapInstrument()
        {
            if (SelectedInstrument != null)
            {
                ((MasterDetailPage)CurrentPage.Parent.Parent).Detail = new NavigationPage(new Map(SelectedInstrument,stateInfo));
            }
        }
        #endregion

        #region ctor
        public InfoVM(INavigation navigation, Page currentpage)
        {
            Navigation = navigation;
            CurrentPage = currentpage;
            dbPath = DependencyService.Get<IPath>().GetDatabasePath(App.DBFILENAME);
            using (ZavodDB db = new ZavodDB(dbPath))
            {
                Instruments = new ObservableCollection<Instruments>(db.Instruments.ToList());
            }
            DelButtonCommand = new Command(DeleteInstrument);
            EditButtonCommand = new Command(EditInstrument);
            MapButtonCommand = new Command(MapInstrument);
            SelectedFloor = "Все";
            FloorList = new ObservableCollection<string>() { "Шестой", "Пятый", "Четвертый", "Третий", "Ребойлерная", "0", "Следующий месяц", "Текущий месяц", "Предыдущий месяц", "Все" };
        }

        public InfoVM(INavigation navigation, Page currentpage, string state)
        {
            Navigation = navigation;
            CurrentPage = currentpage;
            dbPath = DependencyService.Get<IPath>().GetDatabasePath(App.DBFILENAME);
            DelButtonCommand = new Command(DeleteInstrument);
            EditButtonCommand = new Command(EditInstrument);
            MapButtonCommand = new Command(MapInstrument);
            SelectedFloor = state;
            FloorList = new ObservableCollection<string>() { "Шестой", "Пятый", "Четвертый", "Третий", "Ребойлерная", "0", "Следующий месяц", "Текущий месяц","Предыдущий месяц", "Все" };
        }
        #endregion

        #region helpers

        public void ChooseFloor(string state)
        {
            using (ZavodDB db = new ZavodDB(dbPath))
            {
                stateInfo = state.ToString();
                switch (state.ToString())
                {
                    case "Шестой":
                        {
                            Instruments = new ObservableCollection<Instruments>(db.Instruments.Where(i => i.Floor == 6).ToList());
                            break;
                        }
                    case "Пятый":
                        {
                            Instruments = new ObservableCollection<Instruments>(db.Instruments.Where(i => i.Floor == 5).ToList());
                            break;
                        }
                    case "Четвертый":
                        {
                            Instruments = new ObservableCollection<Instruments>(db.Instruments.Where(i => i.Floor == 4).ToList());
                            break;
                        }
                    case "Третий":
                        {
                            Instruments = new ObservableCollection<Instruments>(db.Instruments.Where(i => i.Floor == 3).ToList());
                            break;
                        }
                    case "Ребойлерная":
                        {
                            Instruments = new ObservableCollection<Instruments>(db.Instruments.Where(i => i.Floor == 1).ToList());
                            break;
                        }
                    case "0":
                        {
                            Instruments = new ObservableCollection<Instruments>(db.Instruments.Where(i => i.Floor == 0).ToList());
                            break;
                        }
                    case "Следующий месяц":
                        {
                            int nextMonth = DateTime.Now.Month + 1;
                            int currentYear = DateTime.Now.Year;
                            Instruments = new ObservableCollection<Instruments>(db.Instruments.
                                                                                              Where(i => i.NextCheckDate.Month == nextMonth
                                                                                              && i.NextCheckDate.Year == currentYear).ToList());
                            break;
                        }
                    case "Текущий месяц":
                        {
                            int currentMonth = DateTime.Now.Month;
                            int currentYear = DateTime.Now.Year;
                            Instruments = new ObservableCollection<Instruments>(db.Instruments.Where(i => i.NextCheckDate.Month == currentMonth
                                                                                                    && i.NextCheckDate.Year == currentYear).ToList());
                            break;
                        }
                        case "Предыдущий месяц":
                        {
                            int currentMonth = DateTime.Now.Month - 1;
                            if (currentMonth == 0)
                                currentMonth = 12;
                            int currentYear = DateTime.Now.Year;
                            Instruments = new ObservableCollection<Instruments>(db.Instruments.Where(i => i.NextCheckDate.Month == currentMonth
                                                                                                    && i.NextCheckDate.Year == currentYear).ToList());
                            break;
                        }
                    case "Все":
                        {
                            Instruments = new ObservableCollection<Instruments>(db.Instruments.ToList());
                            break;
                        }
                }
            }
        }

        #endregion
    }
}
