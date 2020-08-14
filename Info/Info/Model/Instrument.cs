using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
namespace Info
{
    public class Instruments
    {
        [Key]
        public int IdInstrument { get; set; }

        public int Floor { get; set; }

        public string InstrumentName { get; set; }

        public string InstrumentType { get; set; }

        public string FactoryNumber { get; set; }

        public string AccuracyClassInstrument { get; set; }

        public double MinValue { get; set; }

        public double MaxValue { get; set; }

        public string UnitValue { get; set; }

        public string PlaceInstrument { get; set; }

        public string PositionInstrument { get; set; }

        public int PeriodCheck { get; set; }

        public DateTime LastCheckDate { get; set; }

        public DateTime NextCheckDate { get; set; }

        public Instruments()
        {

        }

        public Instruments(int floor, string instrumentName, string instrumentType, string factoryNumber, string accuracyClassInstrument, double minValue, double maxValue, string unitValue, string placeInstrument, string positionInstrument, int periodCheck, DateTime lastCheckDate)
        {
            Floor = floor;
            InstrumentName = instrumentName;
            InstrumentType = instrumentType;
            FactoryNumber = factoryNumber;
            AccuracyClassInstrument = accuracyClassInstrument;
            MinValue = minValue;
            MaxValue = maxValue;
            UnitValue = unitValue;
            PlaceInstrument = placeInstrument;
            PositionInstrument = positionInstrument;
            PeriodCheck = periodCheck;
            LastCheckDate = lastCheckDate.Date;
            NextCheckDate = CountNextDate(lastCheckDate.Date, periodCheck);
        }

        public DateTime CountNextDate(DateTime LastCheckDate, int periodCheck)
        {
            DateTime NextDateTime = new DateTime();
            if (periodCheck == 12)
            {
                NextDateTime = LastCheckDate.AddYears(1);
            }
            else if (periodCheck == 24)
            {
                NextDateTime = LastCheckDate.AddYears(2);
            }
            else if (periodCheck == 36)
            {
                NextDateTime = LastCheckDate.AddYears(3);
            }
            else if (periodCheck == 48)
            {
                NextDateTime = LastCheckDate.AddYears(4);
            }
            else if (periodCheck == 60)
            {
                NextDateTime = LastCheckDate.AddYears(5);
            }
            else if (periodCheck == 72)
            {
                NextDateTime = LastCheckDate.AddYears(6);
            }
            else if (periodCheck == 84)
            {
                NextDateTime = LastCheckDate.AddYears(7);
            }
            else if (periodCheck == 96)
            {
                NextDateTime = LastCheckDate.AddYears(8);
            }
            else if (periodCheck == 108)
            {
                NextDateTime = LastCheckDate.AddYears(9);
            }
            else if (periodCheck == 120)
            {
                NextDateTime = LastCheckDate.AddYears(10);
            }
            else if (periodCheck == 360)
            {
                NextDateTime = LastCheckDate.AddYears(30);
            }
            return NextDateTime;
        }
    }
}
