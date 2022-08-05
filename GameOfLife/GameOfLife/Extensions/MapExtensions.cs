using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using GameOfLife.Model;

namespace GameOfLife.Extensions;

public static class MapExtensions
{
    public static List<Cell> ToMap(this string pointString)
    {
        List<Cell> map = new();

        List<string> pointRows = pointString.Split("\r\n", StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries).ToList();

        for (int columnIndex = 0; columnIndex < pointRows.Count; columnIndex++)
        {
            for (int rowIndex = 0; rowIndex < pointRows[columnIndex].Length; rowIndex++)
            {
                map.Add(new Cell(new Point(rowIndex, columnIndex), pointRows[columnIndex][rowIndex].ToState()));
            }
        }

        return map;
    }

    private static Cell.State ToState(this char character) =>
        character switch
        {
            '.' => Cell.State.Dead,
            '#' => Cell.State.Alive,
            _   => throw new InvalidOperationException()
        };
}