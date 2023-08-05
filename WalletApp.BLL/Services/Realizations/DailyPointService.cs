using AutoMapper;
using System;
using WalletApp.BLL.Dtos.DailyPointDtos;
using WalletApp.BLL.Services.Interfaces;
using WalletApp.Common.Exceptions;
using WalletApp.Common.Mapping.ValueConverters;
using WalletApp.DAL.Entities;
using WalletApp.DAL.Repositories;

namespace WalletApp.BLL.Services.Realizations;

public class DailyPointService : IDailyPointService
{
    public DailyPointService()
    {

    }

    public DailyPointReadDto Get()
    {
        double points = CalculatePoints();

        return new DailyPointReadDto()
        {
            Count = points,
            DisplayCount = new DailyPointIntToStringValueConverter().Convert(points)
        };
    }

    private double CalculatePoints()
    {
        DateTime now = DateTime.UtcNow;

        DateTime seasonStart = GetStartsSeasonDate(now);

        int totalDays = GetDaysCountBetween(seasonStart, now);

        double points = GetPoints(totalDays);

        return points;
    }

    private double GetPoints(int days)
    {
        const double percentTwoDayAgo = 100;
        const double percentOneDayAgo = 60;

        const double firstDayPoint = 2;
        const double secondDayPoint = 3;

        if (days == 1)
        {
            return firstDayPoint;
        }

        if (days == 2)
        {
            return secondDayPoint;
        }

        double twoDayAgo = firstDayPoint;
        double oneDayAge = secondDayPoint;

        double points = 0;

        for (int i = 3; i <= days; i++)
        {
            double newDay = 0;

            newDay += twoDayAgo * (percentTwoDayAgo / 100);

            twoDayAgo = oneDayAge;

            newDay += oneDayAge * (percentOneDayAgo / 100);

            oneDayAge = newDay;

            points = newDay;
        }


        return points;
    }

    private DateTime GetStartsSeasonDate(DateTime dateTime)
    {
        int dateTimeMonth = dateTime.Month;
        int dateTimeYear = dateTime.Year;

        int year = 0;
        int month = 0;


        if (new int[] { 12, 1, 2 }.Contains(dateTimeMonth))
        {
            if (dateTimeMonth == 12)
            {
                year = dateTimeYear;
            }
            else
            {
                year = dateTimeYear - 1;
            }

            month = 12;
        }
        else if (new int[] { 3, 4, 5 }.Contains(dateTimeMonth))
        {
            year = dateTimeYear;
            month = 3;
        }
        else if (new int[] { 6, 7, 8 }.Contains(dateTimeMonth))
        {
            year = dateTimeYear;
            month = 6;
        }
        else if (new int[] { 9, 10, 11 }.Contains(dateTimeMonth))
        {
            year = dateTimeYear;
            month = 9;
        }

        return new DateTime(year, month, 1);
    }

    private int GetDaysCountBetween(DateTime from, DateTime to)
    {
        from = new DateTime(from.Year, from.Month, from.Day);
        to = new DateTime(to.Year, to.Month, to.Day);

        TimeSpan diffence = to - from;

        return diffence.Days + 1;
    }
}