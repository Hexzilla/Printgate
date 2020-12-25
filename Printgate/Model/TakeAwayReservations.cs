﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Printgate.Model
{
    public class TakeAwayReservation
    {
        public long id { get; set; }
        public string checkin_ts { get; set; }
        public string purchaser_nominative { get; set; }
        public string purchaser_phone { get; set; }
        public string purchaser_mail { get; set; }
        public string status { get; set; }

        public string Name => purchaser_nominative + "[" + purchaser_phone + "]";

        public string Date => _gate.GetDataTimeFromTimeStamp(double.Parse(checkin_ts));

        public string Email => purchaser_mail;

        public string Status => status;

        private Gate _gate;

        public TakeAwayReservation()
        {
            _gate = new Gate();
        }
    }
    public class TakeAwayReservations
    {
        public TakeAwayReservations()
        {
            Items = new ObservableCollection<TakeAwayReservation>();
        }

        public ObservableCollection<TakeAwayReservation> Items { get; set; }

        public void Add(TakeAwayReservation item)
        {
            Items.Add(item);
        }
    }
}
