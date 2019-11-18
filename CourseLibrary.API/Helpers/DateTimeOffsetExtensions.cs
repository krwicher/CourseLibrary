using System;

namespace CourseLibrary.API.Helpers
{
    public static class DateTimeOffsetExtensions
    {
        public static int GetCurrentAge(this DateTimeOffset timeOffset)
        {
            int age = DateTime.UtcNow.Year - timeOffset.Year;

            if (DateTime.UtcNow < timeOffset.AddYears(age))
                age--;
            return age;
        }
    }
}