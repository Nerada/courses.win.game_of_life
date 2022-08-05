// -----------------------------------------------
//     Author: Ramon Bollen
//      File: GameOfLife.Cell.cs
// Created on: 20220805
// -----------------------------------------------

using System.Windows;

namespace GameOfLife.Model;

public class Cell
{
    public enum State
    {
        Alive,
        Dead
    }

    public Cell(Point location, State status)
    {
        Location = location;
        Status   = status;
    }

    public State Status { get; private set; }

    public Point Location { get; }

    public void ChangeStatus(State newStatus) => Status = newStatus;
}