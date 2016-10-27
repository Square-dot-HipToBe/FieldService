using System;
using System.ComponentModel;
using MyTimeDatabaseLib;

namespace FieldService.ViewModels
{
    public class AddModifyTimeViewModel : INotifyPropertyChanged
    {
        private TimeData _timeData;

        public TimeData TimeData
        {
            get
            {
                return _timeData ?? (_timeData = new TimeData
                {
                    BibleStudies = 0,
                    Books = 0,
                    Brochures = 0,
                    Date = DateTime.Today,
                    Magazines = 0,
                    Minutes = 0,
                    Notes = string.Empty,
                    ReturnVisits = 0,
                    Tracts = 0,
                    Videos = 0
                });
            }
            set
            {
                if (_timeData == value) return;
                _timeData = value;
                OnPropertyChanged("TimeData");
                OnPropertyChanged("TimeDataDate");
                OnPropertyChanged("TimeDataTotalTime");
                OnPropertyChanged("TimeDataMagazines");
                OnPropertyChanged("TimeDataBibleStudies");
                OnPropertyChanged("TimeDataBooks");
                OnPropertyChanged("TimeDataBrochures");
                OnPropertyChanged("TimeDataTracts");
                OnPropertyChanged("TimeDataVideos");
                OnPropertyChanged("TimeDataNotes");
                OnPropertyChanged("TimeDataReturnVisits");
            }
        }

        public DateTime TimeDataDate
        {
            get { return TimeData.Date; }
            set
            {
                if (TimeData.Date.Equals(value)) return;
                TimeData.Date = value;
                OnPropertyChanged("TimeData");
                OnPropertyChanged("TimeDataDate");
            }
        }

        public TimeSpan TimeDataTotalTime
        {
            get { return TimeData.TotalTime; }
            set
            {
                if (TimeData.TotalTime == value) return;
                TimeData.TotalTime = value;
                OnPropertyChanged("TimeData");
                OnPropertyChanged("TimeDataTotalTime");
            }
        }

        public int TimeDataItemId
        {
            get { return _timeData == null ? -1 : _timeData.ItemId; }
            set
            {
                if (value < 0) return;
                _timeData = TimeDataInterface.GetTimeDataItem(value);
                OnPropertyChanged("TimeData");
                OnPropertyChanged("TimeDataDate");
                OnPropertyChanged("TimeDataTotalTime");
                OnPropertyChanged("TimeDataMagazines");
                OnPropertyChanged("TimeDataBibleStudies");
                OnPropertyChanged("TimeDataBooks");
                OnPropertyChanged("TimeDataBrochures");
                OnPropertyChanged("TimeDataTracts");
                OnPropertyChanged("TimeDataVideos");
                OnPropertyChanged("TimeDataNotes");
                OnPropertyChanged("TimeDataReturnVisits");
            }
        }

        public int TimeDataMagazines
        {
            get { return TimeData.Magazines; }
            set
            {
                if (TimeData.Magazines == value) return;
                TimeData.Magazines = value;
                OnPropertyChanged("TimeData");
                OnPropertyChanged("TimeDataMagazines");
            }
        }

        public int TimeDataBrochures
        {
            get { return TimeData.Brochures; }
            set
            {
                if (TimeData.Brochures == value) return;
                TimeData.Brochures = value;
                OnPropertyChanged("TimeData");
                OnPropertyChanged("TimeDataBrochures");
            }
        }

        public int TimeDataBooks
        {
            get { return TimeData.Books; }
            set
            {
                if (TimeData.Books == value) return;
                TimeData.Books = value;
                OnPropertyChanged("TimeData");
                OnPropertyChanged("TimeDataBooks");
            }
        }

        public int TimeDataVideos
        {
            get { return TimeData.Videos; }
            set
            {
                if (TimeData.Videos == value) return;
                TimeData.Videos = value;
                OnPropertyChanged("TimeData");
                OnPropertyChanged("TimeDataVideos");
            }
        }

        public int TimeDataTracts
        {
            get { return TimeData.Tracts; }
            set
            {
                if (TimeData.Tracts == value) return;
                TimeData.Tracts = value;
                OnPropertyChanged("TimeData");
                OnPropertyChanged("TimeDataTracts");
            }
        }

        public int TimeDataReturnVisits
        {
            get { return TimeData.ReturnVisits; }
            set
            {
                if (TimeData.ReturnVisits == value) return;
                TimeData.ReturnVisits = value;
                OnPropertyChanged("TimeData");
                OnPropertyChanged("TimeDataReturnVisits");
            }
        }

        public int TimeDataBibleStudies
        {
            get { return TimeData.BibleStudies; }
            set
            {
                if (TimeData.BibleStudies == value) return;
                TimeData.BibleStudies = value;
                OnPropertyChanged("TimeData");
                OnPropertyChanged("TimeDataBibleStudies");
            }
        }

        public string TimeDataNotes
        {
            get { return TimeData.Notes; }
            set
            {
                if (TimeData.Notes == value) return;
                TimeData.Notes = value;
                OnPropertyChanged("TimeData");
                OnPropertyChanged("TimeDataNotes");
            }
        }

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }

        public bool AddOrUpdateTime() { return TimeDataInterface.AddOrUpdateTime(ref _timeData); }

        public bool ConvertToRBCTime()
        {
            var rtd = new RBCTimeData
            {
                Minutes = TimeData.Minutes,
                Date = TimeData.Date,
                Notes = TimeData.Notes
            };
            return DeleteTime() && RBCTimeDataInterface.AddOrUpdateTime(ref rtd);
        }

        public bool DeleteTime()
        {
            bool v = _timeData.ItemId >= 0 && TimeDataInterface.DeleteTime(_timeData.ItemId);
            if (v)
            {
                _timeData = null;
                OnPropertyChanged("TimeDataDate");
                OnPropertyChanged("TimeDataTotalTime");
                OnPropertyChanged("TimeDataMagazines");
                OnPropertyChanged("TimeDataBibleStudies");
                OnPropertyChanged("TimeDataBooks");
                OnPropertyChanged("TimeDataBrochures");
                OnPropertyChanged("TimeDataTracts");
                OnPropertyChanged("TimeDataVideos");
                OnPropertyChanged("TimeDataNotes");
                OnPropertyChanged("TimeDataReturnVisits");
            }
            return v;
        }

        public bool AddOrUpdateTime(int idExisting)
        {
            //throw new NotImplementedException();
            var timeNewData = _timeData;
            TimeDataItemId = idExisting;
            TimeData.BibleStudies += timeNewData.BibleStudies;
            TimeData.Books += timeNewData.Books;
            TimeData.Brochures += timeNewData.Brochures;
            TimeData.Date = timeNewData.Date;
            TimeData.Magazines += timeNewData.Magazines;
            TimeData.Minutes += timeNewData.Minutes;
            TimeData.Notes = string.IsNullOrWhiteSpace(timeNewData.Notes) ? TimeData.Notes : string.Format("{0}\n\n{1}", TimeData.Notes, timeNewData.Notes);
            TimeData.ReturnVisits += timeNewData.ReturnVisits;
            TimeData.Tracts += timeNewData.Tracts;
            TimeData.Videos += timeNewData.Videos;

			OnPropertyChanged("TimeData");
            OnPropertyChanged("TimeDataDate");
            OnPropertyChanged("TimeDataTotalTime");
            OnPropertyChanged("TimeDataMagazines");
            OnPropertyChanged("TimeDataBibleStudies");
            OnPropertyChanged("TimeDataBooks");
            OnPropertyChanged("TimeDataBrochures");
            OnPropertyChanged("TimeDataTracts");
            OnPropertyChanged("TimeDataVideos");
            OnPropertyChanged("TimeDataNotes");
            OnPropertyChanged("TimeDataReturnVisits");


            return AddOrUpdateTime();

        }

        public bool IsDoubleDataEntry(out int id)
        {
            //throw new NotImplementedException();
            id = -1;
            return _timeData.ItemId <= 0 && TimeDataInterface.IsDoubleDataEntry(_timeData.Date, out id);
        }
    }
}