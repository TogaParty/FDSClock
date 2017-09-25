using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel;

namespace FDSClock
{
    class FDSDateTime
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public string DayOfWeek { get; set; }
        public string DisplayDate { get; set; }
        public string DisplayMonth { get; set; }
        public string DisplayHour { get; set; }
        public string DisplayMinute { get; set; }
        public string DisplaySecond { get; set; }
        public string DisplayAMPM { get; set; }

        public FDSDateTime ()
        {
            Update();
        }

        public bool Update()
        {
            DateTime dateTime = new System.DateTime();

            try
            {
                dateTime = System.DateTime.Now;

                DayOfWeek = dateTime.DayOfWeek.ToString();
                DisplayDate = GetDisplayDate(Convert.ToInt32(dateTime.Day));
                DisplayMonth = GetDisplayMonth(Convert.ToInt32(dateTime.Month));

                DisplayHour = GetDisplayHour(Convert.ToInt32(((dateTime.Hour + 11) % 12) + 1));
                DisplayMinute = GetDisplayMinute(Convert.ToInt32(dateTime.Minute.ToString()));
                DisplaySecond = GetDisplaySecond(Convert.ToInt32(dateTime.Second));
                DisplayAMPM = dateTime.ToString("tt");
            }
            catch (Exception ex)
            {
                return false;
            }

            return true;
        }

        private string GetDisplayDate(int date)
        {
            string displayDate = null;
            
            if (date == 11 || date == 12 || date == 13)
            {
                displayDate = date + "th";
            }

            if (displayDate != null)
            {
                return displayDate;
            }
            else
            {
                string lastDigit = date.ToString();
                lastDigit = lastDigit.Substring(lastDigit.Length - 1, 1);

                if (displayDate == null)
                {
                    switch (lastDigit)
                    {
                        case "1":
                            {
                                displayDate = date.ToString() + "st";
                                break;
                            }
                        case "2":
                            {
                                displayDate = date.ToString() + "nd";
                                break;
                            }
                        case "3":
                            {
                                displayDate = date.ToString() + "rd";
                                break;
                            }
                        default:
                            {
                                displayDate = date.ToString() + "th";
                                break;
                            }
                    }
                }
            }

            return displayDate;
        }

        private string GetDisplayMonth(int month)
        {
            string returnMonth = null;

            switch (month)
            {
                case 1:
                    {
                        returnMonth = "January";
                        break;
                    }
                case 2:
                    {
                        returnMonth = "February";
                        break;
                    }
                case 3:
                    {
                        returnMonth = "March";
                        break;
                    }
                case 4:
                    {
                        returnMonth = "April";
                        break;
                    }
                case 5:
                    {
                        returnMonth = "May";
                        break;
                    }
                case 6:
                    {
                        returnMonth = "June";
                        break;
                    }
                case 7:
                    {
                        returnMonth = "July";
                        break;
                    }
                case 8:
                    {
                        returnMonth = "August";
                        break;
                    }
                case 9:
                    {
                        returnMonth = "September";
                        break;
                    }
                case 10:
                    {
                        returnMonth = "October";
                        break;
                    }
                case 11:
                    {
                        returnMonth = "November";
                        break;
                    }
                case 12:
                    {
                        returnMonth = "December";
                        break;
                    }
            }

            return returnMonth;
        }

        private string GetDisplayHour(int hour)
        {
            if(hour == 0)
            {
                hour = 12;
            }

            return hour.ToString();
        }

        private string GetDisplaySecond(int second)
        {
            if (second.ToString().Length < 2)
            {
                return "0" + second.ToString();
            }
            else
            {
                return second.ToString();
            }
        }

        private string GetDisplayMinute(int minute)
        {
            if (minute.ToString().Length < 2)
            {
                return "0" + minute.ToString();
            }
            else
            {
                return minute.ToString();
            }
        }
    }
}
