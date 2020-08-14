using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace Info
{
    public class MapVM : ViewModelBase
    {
        #region Fields

        private string dbPath;

        private string stateInfo;

        INavigation Navigation;

        Page CurrentPage;

        private Location selectedLocation;

        private ObservableCollection<Location> table;

        private string imageMap;

        private ObservableCollection<Instruments> instruments;

        private Instruments selectedInstrument;

        private int floor;
        #endregion

        #region Properties

        public ObservableCollection<Location> Table
        {
            get
            {
                return table;
            }

            set
            {
                if (table == value)
                {
                    return;
                }

                table = value;
                OnPropertyChanged("Table");
            }
        }

        public Location SelectedLocation
        {
            get
            {
                return selectedLocation;
            }

            set
            {
                if (selectedLocation == value)
                {
                    return;
                }

                selectedLocation = value;
                OnPropertyChanged("SelectedLocation");
            }
        }

        public Instruments SelectedInstrument
        {
            get
            {
                return selectedInstrument;
            }

            set
            {
                if (selectedInstrument == value)
                {
                    return;
                }
                selectedInstrument = value;
                OnPropertyChanged("SelectedInstrument");
            }
        }

        public string ImageMap
        {
            get => imageMap;
            set
            {
                imageMap = value;
                OnPropertyChanged("ImageMap");
            }
        }

        public ObservableCollection<Instruments> Instruments
        {
            get => instruments;
            set
            {
                instruments = value;
                OnPropertyChanged("Instruments");
            }
        }

        public int Floor
        {
            get
            {
                return floor;
            }

            set
            {
                if (floor == value)
                {
                    return;
                }
                floor = value;
                OnPropertyChanged("Floor");
            }
        }
        #endregion

        #region Command

        public ICommand AddEditCommand { get; }

        public ICommand BackCommand { get; }

        public void AddEditLocation()
        {
            using (ZavodDB db = new ZavodDB(dbPath))
            {
                Location location = new Location();
                location.Floor = SelectedInstrument.Floor;
                location.InstrumentId = SelectedInstrument.IdInstrument;
                location.NumberPlace = selectedLocation.IdLocation;
                if (db.Locations.Where(y => y.InstrumentId == SelectedInstrument.IdInstrument).FirstOrDefault() != null)
                {
                    Location loc = db.Locations.Where(y => y.InstrumentId == SelectedInstrument.IdInstrument).FirstOrDefault();

                    Table.Where(z => z.IdLocation == loc.NumberPlace).FirstOrDefault().Opacity = 0.3;

                    loc.NumberPlace = location.NumberPlace;

                    db.Entry<Location>(loc).State = Microsoft.EntityFrameworkCore.EntityState.Modified;

                    Table.Where(z => z.IdLocation == loc.NumberPlace).FirstOrDefault().Opacity = 1;
                }
                else
                {
                    db.Locations.Add(location);
                    Table.Where(z => z.IdLocation == location.NumberPlace).FirstOrDefault().Opacity = 1;

                }
                db.SaveChanges();
            }
        }

        public void BackToInfo()
        {
             ((MasterDetailPage)CurrentPage.Parent.Parent).Detail = new NavigationPage(new InfoPage(stateInfo));
        }

        #endregion

        #region ctor

        public MapVM(INavigation navigation, Page currentpage,Instruments SelInstrument,string state)
        {
            Navigation = navigation;
            CurrentPage = currentpage;
            stateInfo = state;
            dbPath = DependencyService.Get<IPath>().GetDatabasePath(App.DBFILENAME);

            Table = new ObservableCollection<Location>();
            for (int i = 0; i < 297; i++)
                Table.Add(new Location() { IdLocation = i });
            foreach (var item in Table)
            {
                item.Opacity = 0.3;
            }
            SelectedInstrument = SelInstrument;
            Floor = SelectedInstrument.Floor;
            AddEditCommand = new Command(AddEditLocation);
            BackCommand = new Command(BackToInfo);
            SetImage();
            using (ZavodDB db = new ZavodDB(dbPath))
            {
                Location loc = db.Locations.Where(y => y.InstrumentId == selectedInstrument.IdInstrument).FirstOrDefault();
                if (loc != null)
                    Table.Where(x => x.IdLocation == loc.NumberPlace).FirstOrDefault().Opacity = 1;
            }
        }

        #endregion

        #region Helpers

        public void SetImage()
        {
            switch (SelectedInstrument.Floor)
            {
                case 1:
                    SetImage("reb");
                    break;
                case 3:
                    SetImage("third");
                    break;
                case 4:
                    SetImage("forth");
                    break;
                case 5:
                    SetImage("five");
                    break;
                case 6:
                    SetImage("six");
                    break;
            }
        }
        public void SetImage(string value)
        {
            ImageMap = value;
        }

        #endregion
    }
}
