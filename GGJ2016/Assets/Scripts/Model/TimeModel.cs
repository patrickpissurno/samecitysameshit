using System.Linq;

public class TimeModel {
    private int hour = 6;
    private int minute = 20;
    private int day = 1;

    private string month;

    public static readonly string[] Months = new string[] {
        "Jan",
        "Feb", 
        "Mar",
        "Apr",
        "May",
        "Jun",
        "Jul",
        "Aug",
        "Sep",
        "Oct",
        "Nov",
        "Dec"
    };
    public int Hour
    {
        get
        {
            return hour;
        }
        set
        {
            while (value > 23)
                value -= 24;
            while (value < 0)
                value += 24;
            hour = value;
        }
    }
    public int Minute
    {
        get
        {
            return minute;
        }
        set
        {
            while(value > 59)
            {
                value -= 60;
                Hour++;
            }
            while(value < 0)
            {
                value += 60;
                Hour--;
            }
            minute = value;
        }
    }
    public int TotalMinutes
    {
        get
        {
            return Hour * 60 + Minute;
        }
    }
    public int Day
    {
        get
        {
            return day;
        }
        set
        {
            day = value;
        }
    }
    public string Month
    {
        get
        {
            return month;
        }
        set
        {
            if (Months.Contains(value))
                month = value;
        }
    }
}
