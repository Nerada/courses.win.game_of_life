// -----------------------------------------------
//     Author: Ramon Bollen
//      File: GameOfLife.Location.cs
// Created on: 20220806
// -----------------------------------------------

namespace GameOfLife.Model;

public class Location
{
    public Location(int column, int row)
    {
        Column = column;
        Row    = row;
    }

    public int Column { get; }

    public int Row { get; }
}