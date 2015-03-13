﻿using System;
using System.Collections.ObjectModel;
using Telerik.Windows.Controls;

namespace FieldService.ViewModels
{
        public class HowToDataViewModel : ViewModelBase
        {
                private ObservableCollection<HowToDataItemViewModel> _items;

                /// <summary>
                /// Initializes the items.
                /// </summary>
                private void InitializeItems()
                {
                    this._items = new ObservableCollection<HowToDataItemViewModel>();
                    //_items.Add(new HowToDataItemViewModel() {
                    //    Title = StringResources.HowTo_WhatsNew,
                    //    Information = StringResources.WhatsNew_Presentations1,
                    //    ImageSource = new Uri("/Images/whatsnew-presentations.png", UriKind.Relative)
                    //});
                    _items.Add(new HowToDataItemViewModel() {
                        Title = StringResources.HowTo_WhatsNext_T1,
                        Information = StringResources.HowTo_WhatsNext_I1
                    });
                }

                /// <summary>
                /// A collection for <see cref="HowToDataItemViewModel"/> objects.
                /// </summary>
                public ObservableCollection<HowToDataItemViewModel> Items {
                        get
                        {
                                if (this._items == null) {
                                        this.InitializeItems();
                                }
                                return this._items;
                        }
                        private set
                        {
                                this._items = value;
                        }
                }
        }
}
