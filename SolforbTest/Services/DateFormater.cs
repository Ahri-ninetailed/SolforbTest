namespace SolforbTest.Services
{
    public static class DateFormater
    {
        public static string GetMonthAgoDateString()
        {
            DateTime monthAgoDate = DateTime.Now.AddMonths(-1);
            string day = getCorrectDayOrMonth(monthAgoDate.Day);
            string month = getCorrectDayOrMonth(monthAgoDate.Month);
            string date = $"{monthAgoDate.Year}-{month}-{day}";
            return date;
        }
        public static string GetTodayDateString()
        {
            DateTime todayDate = DateTime.Now;
            string day = getCorrectDayOrMonth(todayDate.Day);
            string month = getCorrectDayOrMonth(todayDate.Month);
            string date = $"{todayDate.Year}-{month}-{day}";
            return date;
        }
        private static string getCorrectDayOrMonth(int dayOrMonth)
        {
            return dayOrMonth >= 10 ? dayOrMonth.ToString() : $"0{dayOrMonth}";
        }
    }
}
