using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Info
{
    public class AddVM: InstrumentViewModelBase
    {
        private string dbPath;
        INavigation Navigation;
        Page CurrentPage;
        Instruments lastInstrument = new Instruments();

        private RelayCommand addButton;
        public RelayCommand AddButtonCommand
        {
            get
            {
                return addButton ??
                        (addButton = new RelayCommand(x =>
                        {
                            using (ZavodDB db = new ZavodDB(dbPath))
                            {
                                Instruments instrument = new Instruments(
                                    Floor,
                                    InstrumentName,
                                    InstrumentType,
                                    FactoryNumber,
                                    AccuracyClassInstrument,
                                    MinValue,
                                    MaxValue,
                                    UnitValue,
                                    PlaceInstrument,
                                    PositionInstrument,
                                    PeriodCheck,
                                    LastCheckDate
                                );
                                db.Instruments.Add(instrument);
                                db.SaveChanges();
                                Floor = 1;
                                InstrumentName = InstrumentType = AccuracyClassInstrument = PlaceInstrument = PositionInstrument = FactoryNumber = "";
                                MinValue = MaxValue = 0;
                                UnitValue = "Null";
                                PeriodCheck = 12;
                                LastCheckDate = DateTime.Now;
                            }
                        }));
            }
        }

        public AddVM(INavigation navigation, Page currentpage)
        {
            Navigation = navigation;
            CurrentPage = currentpage;
            dbPath = DependencyService.Get<IPath>().GetDatabasePath(App.DBFILENAME);
        }
    }
}
