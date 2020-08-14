using System;
using System.Collections.Generic;
using System.Text;

namespace Info
{
    public class InstrumentViewModelBase : ViewModelBase
    {
        private int floor;

        private string instrumentName;

        private string instrumentType;

        private string factoryNumber;

        private string unitValue;

        private string placeInstrument;

        private string positionInstrument;

        private string accuracyClassInstrument;

        private double minValue;

        private double maxValue;

        private int periodCheck;

        private DateTime lastCheckDate;

        private DateTime nextCheckDate;

        public int Floor
        {
            get => floor;
            set
            {
                floor = value;
                OnPropertyChanged("Floor");

            }
        }
        public string InstrumentName
        {
            get => instrumentName;
            set
            {
                instrumentName = value;
                OnPropertyChanged("InstrumentName");

            }
        }
        public string InstrumentType
        {
            get => instrumentType;
            set
            {
                instrumentType = value;
                OnPropertyChanged("InstrumentType");

            }
        }
        public string FactoryNumber
        {
            get => factoryNumber;
            set
            {
                factoryNumber = value;
                OnPropertyChanged("FactoryNumber");

            }
        }
        public string UnitValue
        {
            get => unitValue;
            set
            {
                unitValue = value;
                OnPropertyChanged("UnitValue");

            }
        }
        public string PlaceInstrument
        {
            get => placeInstrument;
            set
            {
                placeInstrument = value;
                OnPropertyChanged("PlaceInstrument");

            }
        }

        public string PositionInstrument
        {
            get => positionInstrument;
            set
            {
                positionInstrument = value;
                OnPropertyChanged("PositionInstrument");

            }
        }
        public string AccuracyClassInstrument
        {
            get => accuracyClassInstrument;
            set
            {
                accuracyClassInstrument = value;
                OnPropertyChanged("AccuracyClassInstrument");

            }
        }
        public double MaxValue
        {
            get => maxValue;
            set
            {
                maxValue = value;
                OnPropertyChanged("MaxValue");

            }
        }
        public double MinValue
        {
            get => minValue;
            set
            {
                minValue = value;
                OnPropertyChanged("AccuracyClassInstrument");

            }
        }
        public int PeriodCheck
        {
            get => periodCheck;
            set
            {
                periodCheck = value;
                OnPropertyChanged("PeriodCheck");

            }
        }
        public DateTime LastCheckDate
        {
            get => lastCheckDate;
            set
            {
                lastCheckDate = value;
                OnPropertyChanged("LastCheckDate");

            }
        }
        public DateTime NextCheckDate
        {
            get => nextCheckDate;
            set
            {
                nextCheckDate = value;
                OnPropertyChanged("NextCheckDate");

            }
        }
    }
}
