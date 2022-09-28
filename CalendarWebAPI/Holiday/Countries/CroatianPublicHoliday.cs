using CalendarWebAPI.BaseClass;
using CalendarWebAPI.Helper;
using System;
using System.Collections.Generic;
using System.Text;

namespace CalendarWebAPI.Countries
{
    public class CroatianPublicHoliday : PublicHolidayBase
    {
        /// <summary>
        ///
        /// </summary>
        public enum States
        {
            /// <summary>
            /// All states (Županije)
            /// </summary>
            ALL = 0,

            /// <summary>
            /// Grad Zagreb
            /// </summary>
            ZAGREB = 21,

            /// <summary>
            /// ZAGREBAČKA
            /// </summary>
            ZAGREBAČKA = 1,

            /// <summary>
            /// KRAPINSKO-ZAGORSKA
            /// </summary>
            KRAPINSKO_ZAGORSKA = 2,

            /// <summary>
            /// SISAČKO-MOSLAVAČKA
            /// </summary>
            SISAČKO_MOSLAVAČKA = 3,

            /// <summary>
            /// KARLOVAČKA
            /// </summary>
            KARLOVAČKA = 4,

            /// <summary>
            /// VARAŽDINSKA
            /// </summary>
            VARAŽDINSKA = 5,

            /// <summary>
            /// KOPRIVNIČKO-KRIŽEVAČKA
            /// </summary>
            KOPRIVNIČKO_KRIŽEVAČKA = 6,

            /// <summary>
            /// BJELOVARSKO-BILOGORSKA
            /// </summary>
            BJELOVARSKO_BILOGORSKA = 7,

            /// <summary>
            /// PRIMORSKO-GORANSKA
            /// </summary>
            PRIMORSKO_GORANSKA = 8,

            /// <summary>
            /// LIČKO-SENJSKA
            /// </summary>
            LIČKO_SENJSKA = 9,

            /// <summary>
            /// VIROVITIČKO-PODRAVSKA
            /// </summary>
            VIROVITIČKO_PODRAVSKA = 10,

            /// <summary>
            /// POŽEŠKO-SLAVONSKA
            /// </summary>
            POŽEŠKO_SLAVONSKA = 11,

            /// <summary>
            ///BRODSKO-POSAVSKA
            /// </summary>
            BRODSKO_POSAVSKA = 12,

            /// <summary>
            /// ZADARSKA
            /// </summary>
            ZADARSKA = 13,

            /// <summary>
            /// OSJEČKO-BARANJSKA
            /// </summary>
            OSJEČKO_BARANJSKA = 14,

            /// <summary>
            ///  ŠIBENSKO-KNINSKA
            /// </summary>
            ŠIBENSKO_KNINSKA = 15,

            /// <summary>
            ///  VUKOVARSKO-SRIJEMSKA
            /// </summary>
            VUKOVARSKO_SRIJEMSKA = 16,

            /// <summary>
            ///  SPLITSKO-DALMATINSKA
            /// </summary>
            SPLITSKO_DALMATINSKA = 17,

            /// <summary>
            ///  ISTARSKA
            /// </summary>
            ISTARSKA = 18,

            /// <summary>
            ///   DUBROVAČKO-NERETVANSKA
            /// </summary>
            DUBROVAČKO_NERETVANSKA = 19,

            /// <summary>
            ///  MEĐIMURSKA
            /// </summary>
            MEĐIMURSKA = 20,
        }
        public States State { get; set; }

        /// <summary>
        /// New Year's Day January 1 (Nova Godina)
        /// </summary>
        /// <param name="year"></param>
        public static DateTime NewYear(int year)
        {
            return new DateTime(year, 1, 1);
        }

        public bool TransferredNewYearToWorkingDay = false;

        /// <summary>
        /// Holy Three Kings Epiphany January 6 (Sveta tri kralja
        /// </summary>
        /// <param name="year"></param>
        public static DateTime Epiphany(int year)
        {
            return new DateTime(year, 1, 6);
        }

        /// <summary>
        /// Whether this state observes epiphany.
        /// </summary>
        /// <value>
        /// <c>true</c> if this state observes epiphany; otherwise, <c>false</c>.
        /// </value>
        /// public bool HasEpiphany => Array.IndexOf(new[] { States.BW, States.BY, States.ST }, State) > -1;
        public bool HasEpiphany = true;

        /// <summary>
        /// Good Friday (Velike petak
        /// </summary>
        /// <param name="year"></param>
        public static DateTime GoodFriday(int year)
        {
            var hol = HolidayCalculator.GetEaster(year);
            hol = hol.AddDays(-2);
            return hol;
        }
        /// <summary>
        /// Whether this state observes GoodFriday.
        /// </summary>
        /// <value>
        /// <c>true</c> if this state observes GoodFriday; otherwise, <c>false</c>.
        /// </value>
        /// public bool HasGoodFriday => Array.IndexOf(new[] {  }, State) > -1;
        public bool HasGoodFriday = false;

        /// <summary>
        ///  Easter (Uskrs)
        /// </summary>
        /// <param name="year"></param>
        public static DateTime Easter(int year)
        {
            var hol = HolidayCalculator.GetEaster(year);
            return hol;
        }

        /// <summary>
        ///  Easter Monday (Uskršnji Ponedjeljak)
        /// </summary>
        /// <param name="year"></param>
        public static DateTime EasterMonday(int year)
        {
            var hol = HolidayCalculator.GetEaster(year);
            hol = hol.AddDays(1);
            return hol;
        }

        /// <summary>
        ///  Labour Day (Praznik rada)
        /// </summary>
        /// <param name="year">The year.</param>

        public static DateTime MayDay(int year)
        {
            return new DateTime(year, 5, 1);
        }

        /// <summary>
        /// CorpusChristi (Tijelo Kristovo -Tjelovo)
        /// </summary>
        /// <param name="year"></param>

        public static DateTime VictoryInEuropeDay(int year)
        {
            return new DateTime(year, 5, 9);
        }

        /// <summary>
        /// Whether this state observes VictoryInEuropeDay.
        /// </summary>
        /// <value>
        /// <c>true</c> if this state observes VictoryInEuropeDay; otherwise, <c>false</c>.
        /// </value>
        //public bool HasVictoryInEuropeDay => Array.IndexOf(new[] { }, State) > -1;
        public bool HasVictoryInEuropeDay = false;

        /// <summary>
        ///  National Day (Dan državnosti)
        /// </summary>
        /// <param name="year">The year.</param>

        public static DateTime NationalDay(int year)
        {
            return new DateTime(year, 5, 30);
        }

        /// <summary>
        /// Ascension (Užašće)
        /// </summary>
        /// <param name="year"></param>

        public static DateTime Ascension(int year)
        {
            var hol = HolidayCalculator.GetEaster(year);
            hol = hol.AddDays(4 + (7 * 5));
            return hol;
        }

        /// <summary>
        /// Whether this state observes Ascension.
        /// </summary>
        /// <value>
        /// <c>true</c> if this state observes Ascension; otherwise, <c>false</c>.
        /// </value>
        /// public bool HasGoodFriday => Array.IndexOf(new[] {  }, State) > -1;
        public bool HasAscension = false;

        /// <summary>
        /// Pentecost (Duhovi)
        /// </summary>
        /// <param name="year"></param>

        public static DateTime PentecostMonday(int year)
        {
            var hol = HolidayCalculator.GetEaster(year);
            hol = hol.AddDays((7 * 7) + 1);
            return hol;
        }

        /// <summary>
        /// Whether this state observes PentecostMonday.
        /// </summary>
        /// <value>
        /// <c>true</c> if this state observes PentecostMonday; otherwise, <c>false</c>.
        /// </value>
        /// public bool HasGoodFriday => Array.IndexOf(new[] {  }, State) > -1;
        public bool HasPentecostMonday = false;

        /// <summary>
        /// CorpusChristi (Tijelo Kristovo -Tjelovo)
        /// </summary>
        /// <param name="year"></param>

        public static DateTime CorpusChristi(int year)
        {
            var hol = HolidayCalculator.GetEaster(year);
            //first Thursday after Trinity Sunday (Pentecost + 1 week)
            hol = hol.AddDays((7 * 8) + 4);
            return hol;
        }

        /// <summary>
        /// Whether this state observes CorpusChristi.
        /// </summary>
        /// <value>
        /// <c>true</c> if this state observes Fronleichnam; otherwise, <c>false</c>.
        /// </value>
        //public bool HasCorpusChristi => Array.IndexOf(new[] { States.BW, States.BY, States.HE, States.NW, States.RP, States.SL }, State) > -1;
        public bool HasCorpusChristi = true;

        /// <summary>
        ///  Day of Antifascist Struggle (Dan antifašističke borbe)
        /// </summary>
        /// <param name="year">The year.</param>

        public static DateTime AntifascistStruggleDay(int year)
        {
            return new DateTime(year, 6, 22);
        }

        /// <summary>
        ///  Homeland Thanksgiving Day (Dan domovinske zahvalnosti)
        /// </summary>
        /// <param name="year">The year.</param>

        public static DateTime ThanksGivingDay(int year)
        {
            return new DateTime(year, 8, 5);
        }

        /// <summary>
        /// Assumption of Mary (Velika Gospa)
        /// </summary>
        /// <param name="year"></param>
        public static DateTime Assumption(int year)
        {
            return new DateTime(year, 8, 15);
        }

        /// <summary>
        /// Whether this state observes Assumption.
        /// </summary>
        /// <value>
        /// <c>true</c> if this state observes Mariä Himmelfahrt; otherwise, <c>false</c>.
        /// </value>
        public bool HasAssumption = true;

        /// <summary>
        /// All Saints (Svi Sveti)
        /// </summary>
        /// <param name="year"></param>
        public static DateTime AllSaints(int year)
        {
            return new DateTime(year, 11, 1);
        }

        /// <summary>
        /// Whether this state observes AllSaints
        /// </summary>
        /// <value>
        /// <c>true</c> if this state observes Allerheiligen; otherwise, <c>false</c>.
        /// </value>
        public bool HasAllSaints = true;

        /// <summary>
        /// Croatian Homeland War Remembrance Day
        /// Dan sjećanja na žrtve Domovinskog rata i 
        /// Dan sjećanja na žrtvu Vukovara i Škabrnje
        /// </summary>
        /// <param name="year"></param>
        public static DateTime CroatiaRemembranceDay(int year)
        {
            return new DateTime(year, 11, 18);
        }

        /// <summary>
        /// Christmas (Božić)
        /// </summary>
        /// <param name="year"></param>

        public static DateTime Christmas(int year)
        {
            return new DateTime(year, 12, 25);
        }

        /// <summary>
        /// Day Before Christmas (Badnjak)
        /// </summary>
        /// <param name="year"></param>

        public static DateTime DayBeforeChristmas(int year)
        {
            return new DateTime(year, 12, 24);
        }

        /// <summary>
        /// St Stephens (Sveti Stjepan)
        /// </summary>
        /// <param name="year"></param>

        public static DateTime StStephen(int year)
        {
            return new DateTime(year, 12, 26);
        }

        /// <summary>
        /// Day Before New Year (Silvestrovo)
        /// </summary>
        /// <param name="year"></param>

        public static DateTime DayBeforeNewYear(int year)
        {
            return new DateTime(year, 12, 31);
        }
        /// <summary>
        /// Whether this state observes DayBeforeNewYear
        /// </summary>
        /// <value>
        /// <c>true</c> if this state observes DayBeforeNewYear; otherwise, <c>false</c>.
        /// </value>
        public bool HasDayBeforeNewYear = false;

        /// <summary>
        /// Determines whether date is a public holiday.
        /// </summary>
        /// <param name="dt">The date.</param>
        /// <returns>
        ///   <c>true</c> if is public holiday; otherwise, <c>false</c>.
        /// </returns>
        public override bool IsPublicHoliday(DateTime dt)
        {
            var year = dt.Year;
            var date = dt.Date;

            if (Easter(year) == date)
                return true;
            if (EasterMonday(year) == date)
                return true;
            if (CorpusChristi(year) == date)
                return true;

            switch (dt.Month)
            {
                case 1:
                    if (NewYear(year) == date)
                        return true;
                    if (HasEpiphany && Epiphany(year) == date)
                        return true;
                    break;
                case 2:
                    break;

                case 3:
                    break;
                case 4:
                    break;
                case 5:
                    if (MayDay(year) == date)
                        return true;
                    if (NationalDay(year) == date)
                        return true;
                    break;
                case 6:
                    if (AntifascistStruggleDay(year) == date)
                        return true;
                    break;
                case 7:

                    break;
                case 8:
                    if (ThanksGivingDay(year) == date)
                        return true;
                    if (Assumption(year) == date)
                        return true;
                    break;
                case 9:

                    break;
                case 10:

                    break;
                case 11:
                    if (AllSaints(year) == date)
                        return true;
                    if (CroatiaRemembranceDay(year) == date)
                        return true;
                    break;
                case 12:
                    if (Christmas(year) == date)
                        return true;
                    if (StStephen(year) == date)
                        return true;
                    break;
            }

            return false;
        }



        /// <summary>
        /// Get a list of dates with names for all holidays in a year.
        /// </summary>
        /// <param name="year">The year.</param>
        /// <returns>
        /// Dictionary of bank holidays
        /// </returns>
        public override IDictionary<DateTime, string> PublicHolidayNames(int year)
        {
            var bHols = new Dictionary<DateTime, string> { { NewYear(year), "Nova Godina" } };
            bHols.Add(Epiphany(year), "Sveta tri kralja");
            bHols.Add(Easter(year), "Uskrs");
            bHols.Add(EasterMonday(year), "Uskršnji ponedjeljak");
            bHols.Add(MayDay(year), "Praznik rada");
            bHols.Add(NationalDay(year), "Dan državnosti");
            bHols.Add(CorpusChristi(year), "Tijelovo");
            bHols.Add(AntifascistStruggleDay(year), "Dan antifašističke borbe");
            bHols.Add(ThanksGivingDay(year), "Dan domovinske zahvalnosti");
            bHols.Add(Assumption(year), "Velika gospa");
            bHols.Add(AllSaints(year), "Dan svih svetih");
            bHols.Add(CroatiaRemembranceDay(year), "Dan sjećanja na žrtve Domovinskog rata i Dan sjećanja na žrtvu Vukovara i Škabrnje");
            bHols.Add(Christmas(year), "Božić");
            bHols.Add(StStephen(year), "Sveti Stjepan");

            return bHols;
        }

        /// <summary>
        /// List of federal and state holidays (for defined <see cref="State"/>)
        /// </summary>
        /// <param name="year">The year.</param>
        public override IList<DateTime> PublicHolidays(int year)
        {
            var bHols = new List<DateTime> { NewYear(year) };
            bHols.Add(Epiphany(year));
            bHols.Add(Easter(year));
            bHols.Add(EasterMonday(year));
            bHols.Add(MayDay(year));
            bHols.Add(NationalDay(year));
            bHols.Add(CorpusChristi(year));
            bHols.Add(AntifascistStruggleDay(year));
            bHols.Add(ThanksGivingDay(year));
            bHols.Add(Assumption(year));
            bHols.Add(AllSaints(year));
            bHols.Add(CroatiaRemembranceDay(year));
            bHols.Add(Christmas(year));
            bHols.Add(StStephen(year));

            return bHols;
        }
    }
}
