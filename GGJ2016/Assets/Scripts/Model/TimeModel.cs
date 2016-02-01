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
