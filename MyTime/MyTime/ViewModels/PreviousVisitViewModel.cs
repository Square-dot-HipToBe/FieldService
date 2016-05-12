using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using MyTimeDatabaseLib;

namespace FieldService.ViewModels
{
    public class PreviousVisitViewModel : INotifyPropertyChanged
    {
        private RvPreviousVisitData _previousVisitData = new RvPreviousVisitData
        {
            Books = 0,
            Brochures = 0,
            Date = DateTime.Now,
            RvItemId = -1,
            Magazines = 0,
            Tracts = 0,
            Videos = 0,
            Notes = string.Empty
        };

        private ReturnVisitViewModel _parentRv;
        private bool _saving;
        public event PropertyChangedEventHandler PropertyChanged;

        public ReturnVisitViewModel ParentRv
        {
            get
            {
                if (_parentRv == null) _parentRv = new ReturnVisitViewModel() { ItemId = ReturnVisitItemId };
                return _parentRv;
            }
            set
            {
                if (_parentRv == value) return;
                _parentRv = value;
                OnPropertyChanged("ParentRv");
            }
        }

        public RvPreviousVisitData PreviousVisitData
        {
            get
            {
                return _previousVisitData;
            }
            set
            {
                if (_previousVisitData == value) return;
                _previousVisitData = value;
                OnPropertyChanged("PreviousVisitData");
                OnPropertyChanged("Books");
                OnPropertyChanged("Brochures");
                OnPropertyChanged("Magazines");
                OnPropertyChanged("Date");
                OnPropertyChanged("DaysSinceVisit");
                OnPropertyChanged("Notes");
                OnPropertyChanged("Tracts");
            }
        }

        public int Books
        {
            get { return _previousVisitData.Books; }
            set
            {
                if (_previousVisitData.Books == value) return;
                _previousVisitData.Books = value;
                OnPropertyChanged("Books");
            }
        }

        public int Brochures
        {
            get { return _previousVisitData.Brochures; }
            set
            {
                if (_previousVisitData.Brochures == value) return;
                _previousVisitData.Brochures = value;
                OnPropertyChanged("Brochures");
            }
        }

        public int Magazines
        {
            get { return _previousVisitData.Magazines; }
            set
            {
                if (_previousVisitData.Magazines == value) return;
                _previousVisitData.Magazines = value;
                OnPropertyChanged("Magazines");
            }
        }

        public int Tracts
        {
            get { return _previousVisitData.Tracts; }
            set
            {
                if (_previousVisitData.Tracts == value) return;
                _previousVisitData.Tracts = value;
                OnPropertyChanged("Tracts");
            }
        }

        public int Videos
        {
            get { return _previousVisitData.Videos; }
            set
            {
                if (_previousVisitData.Videos == value) return;
                _previousVisitData.Videos = value;
                OnPropertyChanged("Videos");
            }
        }

        public DateTime Date
        {
            get { return _previousVisitData.Date; }
            set
            {
                if (_previousVisitData.Date == value) return;
                _previousVisitData.Date = value;
                OnPropertyChanged("Date");
                OnPropertyChanged("DaysSinceVisit");
            }
        }

        public string Notes
        {
            get { return _previousVisitData.Notes; }
            set
            {
                if (_previousVisitData.Notes == value) return;
                _previousVisitData.Notes = value;
                OnPropertyChanged("Notes");
            }
        }

        public int DaysSinceVisit => _previousVisitData.Date.DayOfYear == DateTime.Today.DayOfYear &&
                                     _previousVisitData.Date.Year == DateTime.Today.Year
            ? 0 
            : DateTime.Today.Subtract(_previousVisitData.Date).Days+1;

        public string DaysSinceVisitString => BuildDaysSinceString(DaysSinceVisit);


        private int GetMonthsBetween(DateTime from, DateTime to)
        {
            if (from > to) return GetMonthsBetween(to, from);

            var monthDiff = Math.Abs((to.Year * 12 + (to.Month - 1)) - (from.Year * 12 + (from.Month - 1)));

            if (from.AddMonths(monthDiff) > to || to.Day < from.Day)
            {
                return monthDiff - 1;
            }
            else {
                return monthDiff;
            }
        }

        private string BuildDaysSinceString(int _daysSinceLastVisit)
        {
            if (_daysSinceLastVisit == (DateTime.Now - SqlCeConstants.DateTimeMinValue).Days)
                return StringResources.FullRVListPage_NoVisitsSaved;

            if (_daysSinceLastVisit == 0)
            {
                return StringResources.RVPage_Visits_Today;
            }

            if (_daysSinceLastVisit == 1)
            {
                return StringResources.RVPage_Visits_Yesterday;
            }

            if (_daysSinceLastVisit < 14)
            {
                //less than 2 weeks
                return string.Format("{0} {1}", _daysSinceLastVisit, StringResources.RVPage_Visits_DaysSinceDays);
            }
            else if (_daysSinceLastVisit / 7 < 8)
            {               //less than 2 months
                return string.Format("{0} {1}", _daysSinceLastVisit / 7, StringResources.RVPage_Visits_DaysSinceWeeks);
            }
            else if (_daysSinceLastVisit < 365)
            {                                                                 //less than a year
                var d = DateTime.Now.AddDays(-1 * _daysSinceLastVisit);
                return string.Format("{0} {1}", GetMonthsBetween(DateTime.Now, d),
                    StringResources.RVPage_Visits_DaysSinceMonths);
            }
            else {
                //greater than a year
                return string.Format("{0} {1}", Math.Floor(((double)_daysSinceLastVisit) / 365.0),
                    StringResources.RVPage_Visits_DaysSinceYears);
            }
            return null;
        }

        public int PreviousVisitItemId
        {
            get { return _previousVisitData == null ? -1 : _previousVisitData.ItemId; }
            set
            {
                _previousVisitData = RvPreviousVisitsDataInterface.GetCall(value);
                OnPropertyChanged("PreviousVisitData");
                OnPropertyChanged("Notes");
                OnPropertyChanged("Date");
                OnPropertyChanged("Magazines");
                OnPropertyChanged("Brochures");
                OnPropertyChanged("Books");
                OnPropertyChanged("DaysSinceVisit");
                OnPropertyChanged("Tracts");
                OnPropertyChanged("Videos");
            }
        }

        public int ReturnVisitItemId
        {
            get { return _previousVisitData.RvItemId; }
            set
            {
                if (_previousVisitData != null && _previousVisitData.RvItemId == value) return;
                _previousVisitData = new RvPreviousVisitData
                {
                    Books = 0,
                    Brochures = 0,
                    RvItemId = value,
                    Date = DateTime.Now,
                    Magazines = 0,
                    Tracts = 0,
                    Videos = 0,
                    Notes = string.Empty
                };
                OnPropertyChanged("PreviousVisitData");
            }
        }

        public DateTime PreviousVisitDataDate
        {
            get { return PreviousVisitData.Date; }
            set
            {
                if (value == PreviousVisitData.Date) return;
                PreviousVisitData.Date = value;
                OnPropertyChanged("PreviousVisitDataDate");
                OnPropertyChanged("PreviousVisitData");
            }
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }

        public bool AddOrUpdateItem()
        {
            return RvPreviousVisitsDataInterface.AddOrUpdateCall(ref _previousVisitData);
        }
        public bool DeleteCall()
        {
            if (_previousVisitData.ItemId < 0) return false;
            return RvPreviousVisitsDataInterface.DeleteCall(_previousVisitData.ItemId);
        }
    }
}
