﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Tasks;
using MyTimeDatabaseLib;

namespace FieldService
{
    public class Reporting
    {
        public static void SendReport(string body)
        {
            body += StringResources.Sharing_Thanks;
            var sendType = addressType.Email;
            string sendTo = String.Empty;
            bool includeSig = true;
            try
            {
                string nickName = App.Settings.nickname;
                body += $",\n{nickName}";
                sendType = App.Settings.SendMethodEnum;
                sendTo = App.Settings.csoEmail;
                includeSig = App.Settings.shareFSApp;
            }
            catch (Exception) { }

            if (sendType == addressType.Email)
            {
                if (includeSig)
                {
                    body += StringResources.Sharing_Signature;
                }
                var emailcomposer = new EmailComposeTask { Subject = StringResources.Sharing_Subject, Body = body, To = sendTo };
                emailcomposer.Show();
                return;
            }
            var composeSms = new SmsComposeTask { Body = body, To = sendTo };
            composeSms.Show();
        }

        public static TimeData[] BuildTimeReport(DateTime fromDate, DateTime toDate, SortOrder so)
        {
            TimeData[] entries = TimeDataInterface.GetEntries(fromDate, toDate, so);

            try
            {
                bool countCalls = App.Settings.manuallyTrackRvs;

                if (!countCalls) return entries;
                RvPreviousVisitData[] calls = RvPreviousVisitsDataInterface.GetCallsByDate(fromDate, toDate);

                if (calls != null)
                {
                    var entriesMore = new List<TimeData>(entries);

                    foreach (var c in calls)
                    {
                        bool found = false;
                        foreach (var e in entries)
                        {
                            if (e.Date.Date != c.Date.Date) continue;
                            // Check for call data which happened on the same date as another service day
                            // If it did, add the values, otherwise continue
                            e.Magazines += c.Magazines;
                            e.Books += c.Books;
                            e.Brochures += c.Brochures;
                            e.Tracts += c.Tracts;
                            e.Videos += c.Videos;
                            e.ReturnVisits += RvPreviousVisitsDataInterface.IsInitialCall(c) ? 0 : 1;
                            found = true;
                            break;
                        }

                        if (!found)
                        { // We found a call, but no service time was recorded on this date
                            entriesMore.Add(new TimeData()
                            {
                                Magazines = c.Magazines,
                                Books = c.Books,
                                Brochures = c.Brochures,
                                Tracts = c.Tracts,
                                Videos = c.Videos,
                                Date = c.Date,
                                ReturnVisits = RvPreviousVisitsDataInterface.IsInitialCall(c) ? 0 : 1
                            });
                        }
                    }
                    entries = entriesMore.OrderBy(s => s.Date).ToArray();
                }
                return entries;
            }
            catch
            {
                return entries;
            }
        }
    }
}
