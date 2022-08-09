// -----------------------------------------------
//     Author: Ramon Bollen
//      File: GameOfLife.MapExtensions.cs
// Created on: 20220805
// -----------------------------------------------

using System;
using System.Collections.Generic;
using GameOfLife.Model;

namespace GameOfLife.Extensions;

public static class MapExtensions
{
    public static List<BaseCell> ToMap(this string pointString, int columns, int margin)
    {
        pointString = pointString.ReplaceLineEndings(string.Empty);

        List<BaseCell> map = new();

        int charIndex     = 0;
        int currentColumn = 0;
        int currentRow    = 0;

        string coefficient = string.Empty;

        map.AddRange(CreateMultiple(currentColumn, currentRow, margin * 2 + columns, margin));
        currentRow = margin;

        map.AddRange(CreateMultiple(currentColumn, currentRow, margin));
        currentColumn = margin;

        while (pointString[charIndex] != '!')
        {
            if (char.IsDigit(pointString[charIndex]))
            {
                coefficient += pointString[charIndex];
                charIndex++;
                continue;
            }

            int repeatAmount = string.IsNullOrWhiteSpace(coefficient) ? 1 : Math.Max(1, int.Parse(coefficient));
            coefficient = string.Empty;

            switch (pointString[charIndex])
            {
                case 'b':
                case 'o':
                {
                    map.AddRange(CreateMultiple(currentColumn, currentRow, repeatAmount, 1, pointString[charIndex].ToState()));
                    currentColumn += repeatAmount;
                    break;
                }
                case '$':
                {
                    map.AddRange(CreateMultiple(currentColumn, currentRow, margin * 2 + columns - currentColumn));
                    currentColumn = 0;

                    if (repeatAmount != 1)
                    {
                        currentRow++;

                        for (int emptyRowIndex = currentRow; emptyRowIndex < currentRow + repeatAmount - 1; emptyRowIndex++)
                        {
                            map.AddRange(CreateMultiple(currentColumn, emptyRowIndex, margin * 2 + columns - currentColumn));
                        }

                        currentRow += repeatAmount - 2;
                    }

                    currentRow++;
                    map.AddRange(CreateMultiple(currentColumn, currentRow, margin));
                    currentColumn = margin;
                    break;
                }
            }

            charIndex++;
        }

        map.AddRange(CreateMultiple(currentColumn, currentRow, margin * 2 + columns - currentColumn));
        currentRow++;
        currentColumn = 0;
        map.AddRange(CreateMultiple(currentColumn, currentRow, margin * 2 + columns, margin));

        return map;
    }

    public static void TimesAction(this int repeatCount, Action action)
    {
        for (int i = 0; i < repeatCount; i++) { action(); }
    }

    private static List<BaseCell> CreateMultiple(int columnStartIndex, int rowStartIndex, int columnAmount, int rowAmount = 1, CellState state = CellState.Dead)
    {
        List<BaseCell> cellList = new();

        for (int rowIndex = rowStartIndex; rowIndex < rowStartIndex + rowAmount; rowIndex++)
        {
            for (int columnIndex = columnStartIndex; columnIndex < columnStartIndex + columnAmount; columnIndex++) { cellList.Add(new BaseCell(new Location(columnIndex, rowIndex), state)); }
        }


        return cellList;
    }

    private static CellState ToState(this char character) =>
        character switch
        {
            'b' => CellState.Dead,
            'o' => CellState.Alive,
            _   => throw new InvalidOperationException()
        };
}