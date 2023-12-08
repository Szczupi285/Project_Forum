namespace Project_Forum.Models
{
    public class FilterPosts
    {
        public TimeFilter SelectedTimeFilter { get; set; } = TimeFilter.sixHours;

        public string? FilterByTags { get; set; } = null;


        /// <summary>
        ///This method takes the current date and time as a starting point and decrement it
        /// based on the SelectedTimeFilter.
        /// </summary>
        /// <returns>
        /// A DateTime object indicating the calculated date and time based on the SelectedTimeFilter.
        /// </returns>
        /// <remarks>
        ///  The supported time filters include: <para/>
        /// - Six hours <para/>
        /// - Twelve hours <para/>
        /// - One day <para/>
        /// - One week <para/>
        /// - One month <para/>
        /// The resulting DateTime represents the adjusted date and time.
        /// </remarks>
        public DateTime GetDateDiffFromCurrentDate()
        {
            DateTime dateTime = DateTime.Now;
            switch (SelectedTimeFilter)
            {
                case TimeFilter.sixHours:
                    dateTime = dateTime.AddHours(-6);
                    break;
                case TimeFilter.twelveHours:
                    dateTime = dateTime.AddHours(-12);
                    break;
                case TimeFilter.day:
                    dateTime = dateTime.AddDays(-1);
                    break;
                case TimeFilter.week:
                    dateTime = dateTime.AddDays(-7);
                    break;
                case TimeFilter.month:
                   dateTime = dateTime.AddMonths(-1);
                    break;
            }
            return dateTime;
        }
    }
}
