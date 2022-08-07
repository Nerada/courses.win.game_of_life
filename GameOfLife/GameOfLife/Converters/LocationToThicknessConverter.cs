// -----------------------------------------------
//     Author: Ramon Bollen
//      File: GameOfLife.LocationToThicknessConverter.cs
// Created on: 20220807
// -----------------------------------------------

using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using GameOfLife.Model;

namespace GameOfLife.Converters;

public class LocationToThicknessConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is not Location location || !int.TryParse(parameter.ToString(), out int multiplier)) throw new InvalidOperationException();

        return new Thickness(location.Column * multiplier, location.Row * multiplier, 0, 0);
    }


    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => throw new NotImplementedException();
}