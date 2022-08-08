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
    public enum State
    {
        Unknown,
        Alive,
        Dead
    }

    private readonly List<Cell> _neighbors = new(8);

    private State _nextState;

    public Cell(Location location, State status)
    {
        Location = location;
        Status   = status;
    }

    public Location Location { get; }

    public bool Changed { get; private set; }

    public State Status { get; private set; }

    public void MoveOn()
    {
        if (_nextState == State.Unknown) throw new InvalidOperationException();

        Changed = Status != _nextState;

        Status     = _nextState;
        _nextState = State.Unknown;
    }

    public void PrepareNextState() => _nextState = _neighbors.Count(n => n.Status == State.Alive) switch
    {
        < 2 => State.Dead,
        > 3 => State.Dead,
        3   => State.Alive,
        _   => Status
    };

    public void SetNeighbors(IReadOnlyList<Cell> neighbors)
    {
        _neighbors.Clear();
        _neighbors.AddRange(neighbors);
        PrepareNextState();
    }
}