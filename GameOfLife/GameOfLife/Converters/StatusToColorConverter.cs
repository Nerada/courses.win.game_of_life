// -----------------------------------------------
//     Author: Ramon Bollen
//      File: GameOfLife.StatusToColorConverter.cs
// Created on: 20220807
// -----------------------------------------------

using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;
using GameOfLife.Model;

namespace GameOfLife.Converters;

public class StatusToColorConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is not CellState cellState) throw new InvalidOperationException();

        return cellState == CellState.Alive ? new SolidColorBrush(Colors.OrangeRed) : new SolidColorBrush(Colors.Gray);
    }


    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => throw new NotImplementedException();
}