using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Info
{
    public class EditVM : InstrumentViewModelBase
    {
        private Instruments instrument;
        private string dbPath;
        INavigation Navigation;
        Page CurrentPage;

        private RelayCommand editCommand;
        public RelayCommand EditButtonCommand
        {
            get
            {
                return editCommand ??
                        (editCommand = new RelayCommand(x =>
                        {
                            using (ZavodDB db = new ZavodDB(dbPath))
                            {
                                instrument.Floor = Floor;
                                instrument.InstrumentName = InstrumentName;
                                instrument.InstrumentType = InstrumentType;
                                instrument.FactoryNumber = FactoryNumber;
                                instrument.AccuracyClassInstrument = AccuracyClassInstrument;
                                instrument.MinValue = MinValue;
                                instrument.MaxValue = MaxValue;
                                instrument.UnitValue = UnitValue;
                                instrument.PlaceInstrument = PlaceInstrument;
                                instrument.PositionInstrument = PositionInstrument;
                                instrument.PeriodCheck = PeriodCheck;
                                if (instrument.LastCheckDate != LastCheckDate)
                                {
                                    instrument.LastCheckDate = LastCheckDate;
                                    NextCheckDate = instrument.CountNextDate(LastCheckDate, PeriodCheck);
                                }
                                instrument.NextCheckDate = NextCheckDate;
                                db.Entry<Instruments>(instrument).State = EntityState.Modified;
                                db.SaveChanges();
                            }
                            CurrentPage.DisplayAlert("Уведомление", "Изменения сохранены", "Ок");
                        }));
            }
        }


        public EditVM(INavigation navigation, Page currentpage, Instruments SelectedIntstrument)
        {

            Navigation = navigation;
            CurrentPage = currentpage;
            dbPath = DependencyService.Get<IPath>().GetDatabasePath(App.DBFILENAME);

            this.instrument = SelectedIntstrument;
            Floor = instrument.Floor;
            InstrumentName = instrument.InstrumentName;
            InstrumentType = instrument.InstrumentType;
            FactoryNumber = instrument.FactoryNumber;
            AccuracyClassInstrument = instrument.AccuracyClassInstrument;
            MinValue = instrument.MinValue;
            MaxValue = instrument.MaxValue;
            UnitValue = instrument.UnitValue;
            PlaceInstrument = instrument.PlaceInstrument;
            PositionInstrument = instrument.PositionInstrument;
            PeriodCheck = instrument.PeriodCheck;
            LastCheckDate = instrument.LastCheckDate;
            NextCheckDate = instrument.NextCheckDate;
        }
    }
}
