using UnityEngine;
using System.Collections;

public class TimeModel {
    private int hour = 5;
    private int minute = 0;
    private int day = 1;

    private readonly string[] months = new string[] {
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
    public string[] Months
    {
        get
        {
            return months;
        }
    }
}
