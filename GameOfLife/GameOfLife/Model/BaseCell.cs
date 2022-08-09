// -----------------------------------------------
//     Author: Ramon Bollen
//      File: GameOfLife.BaseCell.cs
// Created on: 20220809
// -----------------------------------------------

namespace GameOfLife.Model;

public class BaseCell
{
    public BaseCell(Location location, CellState status)
    {
        Location = location;
        State    = status;
    }

    public Location Location { get; }

    public CellState State { get; }
}