// -----------------------------------------------
//     Author: Ramon Bollen
//      File: GameOfLife.Cell.cs
// Created on: 20220805
// -----------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;

namespace GameOfLife.Model;

public class Cell
{
    private readonly BaseCell _baseCell;

    private readonly List<Cell> _neighbors = new(8);

    private CellState _nextState;

    public Cell(BaseCell baseCell)
    {
        _baseCell = baseCell;
        State     = _baseCell.State;
        Location  = _baseCell.Location;
    }

    public Location Location { get; }

    public bool Changed { get; private set; }

    public CellState State { get; private set; }

    public void MoveOn()
    {
        if (_nextState == CellState.Unknown) throw new InvalidOperationException();

        Changed = State != _nextState;

        State      = _nextState;
        _nextState = CellState.Unknown;
    }

    public void PrepareNextState() => _nextState = _neighbors.Count(n => n.State == CellState.Alive) switch
    {
        < 2 => CellState.Dead,
        > 3 => CellState.Dead,
        3   => CellState.Alive,
        _   => State
    };

    public void SetNeighbors(IReadOnlyList<Cell> neighbors)
    {
        _neighbors.Clear();
        _neighbors.AddRange(neighbors);
        PrepareNextState();
    }

    public void ResetCell()
    {
        State = _baseCell.State;
        PrepareNextState();
    }
}