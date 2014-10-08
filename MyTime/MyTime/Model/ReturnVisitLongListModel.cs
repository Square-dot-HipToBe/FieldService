﻿// ***********************************************************************
// Assembly         : MyTime
// Author           : trevo_000
// Created          : 11-10-2012
//
// Last Modified By : trevo_000
// Last Modified On : 11-10-2012
// ***********************************************************************
// <copyright file="ReturnVisitLongListViewModel.cs" company="">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Media.Imaging;
using MyTimeDatabaseLib;

namespace FieldService.Model
{
        /// <summary>
        /// Class ReturnVisitLLItemModel
        /// </summary>
        public class ReturnVisitLLItemModel : INotifyPropertyChanged
        {
                /// <summary>
                /// The _address1
                /// </summary>
                private string _address1;
                /// <summary>
                /// The _address2
                /// </summary>
                private string _address2;
                /// <summary>
                /// The _image
                /// </summary>
                private BitmapImage _image;
                /// <summary>
                /// The _item ID
                /// </summary>
                private int _itemID;
                /// <summary>
                /// The _text
                /// </summary>
                private string _text;

                private string _city;

            private int _daysSinceLastVisit;

            public int DaysSinceInt
            {
                get { return _daysSinceLastVisit; }
                set
                {
                    if (value == _daysSinceLastVisit) return;
                    _daysSinceLastVisit = value;
                    NotifyPropertyChanged("DaysSinceLastVisit");
                    NotifyPropertyChanged("DaysSinceInt");
                }
            }

            /// <summary>
                /// Gets or sets the item id.
                /// </summary>
                /// <value>The item id.</value>
                public int ItemId
                {
                        get { return _itemID; }
                        set
                        {
                                if (value != _itemID) {
                                        _itemID = value;
                                        NotifyPropertyChanged("ItemId");
                                }
                        }
                }

            public string DaysSinceLastVisit
            {
                get
                {
                    if (_daysSinceLastVisit == (DateTime.Now - SqlCeConstants.DateTimeMinValue).Days)
                        return StringResources.FullRVListPage_NoVisitsSaved;


                    if (_daysSinceLastVisit < 14) {                                                                         //less than 2 weeks
                        return string.Format("{0} {1}", _daysSinceLastVisit, StringResources.RVPage_Visits_DaysSinceDays);
                    } else if (_daysSinceLastVisit / 7 < 8) {               //less than 2 months
                        return string.Format("{0} {1}", _daysSinceLastVisit / 7, StringResources.RVPage_Visits_DaysSinceWeeks);
                    } else if (_daysSinceLastVisit < 365) {                                                                 //less than a year
                        var d = DateTime.Now.AddDays(-1*_daysSinceLastVisit);
                        return string.Format("{0} {1}", GetMonthsBetween(DateTime.Now, d),
                            StringResources.RVPage_Visits_DaysSinceMonths);
                    }
                    else {                                                                                                  //greater than a year
                        return string.Format("{0} {1}", Math.Floor(((double)_daysSinceLastVisit)/365.0), StringResources.RVPage_Visits_DaysSinceYears);
                    }
                    return null;
                }
            }

            private int GetMonthsBetween(DateTime from, DateTime to)
            {
                if (from > to) return GetMonthsBetween(to, from);

                var monthDiff = Math.Abs((to.Year * 12 + (to.Month - 1)) - (from.Year * 12 + (from.Month - 1)));

                if (from.AddMonths(monthDiff) > to || to.Day < from.Day) {
                    return monthDiff - 1;
                } else {
                    return monthDiff;
                }
            }

            /// <summary>
            /// Sample ViewModel property; this property is used in the view to display its value using a Binding.
            /// </summary>
            /// <value>The image source.</value>
            /// <returns></returns>
            public BitmapImage ImageSource
                {
                        get { return _image; }

                        set
                        {
                                if (value != _image) {
                                        _image = value;
                                        NotifyPropertyChanged("ImageSource");
                                }
                        }
                }


                /// <summary>
                /// Gets or sets the text.
                /// </summary>
                /// <value>The text.</value>
                public string Text
                {
                        get { return _text; }
                        set
                        {
                                if (_text == value) return;
                                _text = value;
                                NotifyPropertyChanged("Text");
                        }
                }

                /// <summary>
                /// Gets or sets the address1.
                /// </summary>
                /// <value>The address1.</value>
                public string Address1
                {
                        get { return _address1; }

                        set
                        {
                                if (_address1 == value) return;
                                _address1 = value;
                                NotifyPropertyChanged("Address1");
                        }
                }

                /// <summary>
                /// Gets or sets the address2.
                /// </summary>
                /// <value>The address2.</value>
                public string Address2
                {
                        get { return _address2; }
                        set
                        {
                                if (_address2 == value) return;
                                _address2 = value;
                                NotifyPropertyChanged("Address2");
                        }
                }

                public string City { get { return _city; }
                        set
                        {
                                if (_city != value)
                                {
                                        _city = value;
                                        NotifyPropertyChanged("City");
                                }
                        } }

                #region INotifyPropertyChanged Members

                /// <summary>
                /// Occurs when [property changed].
                /// </summary>
                public event PropertyChangedEventHandler PropertyChanged;

                #endregion

                /// <summary>
                /// Notifies the property changed.
                /// </summary>
                /// <param name="propertyName">Nameof the property.</param>
                private void NotifyPropertyChanged(String propertyName)
                {
                        PropertyChangedEventHandler handler = PropertyChanged;
                        if (null != handler) {
                                handler(this, new PropertyChangedEventArgs(propertyName));
                        }
                }
        }
}