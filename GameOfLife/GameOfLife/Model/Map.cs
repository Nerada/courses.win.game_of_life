// -----------------------------------------------
//     Author: Ramon Bollen
//      File: GameOfLife.Map.cs
// Created on: 20220806
// -----------------------------------------------

using System.Collections.Generic;
using System.Linq;
using GameOfLife.Extensions;

namespace GameOfLife.Model;

public class Map
{
    private readonly List<Cell> _cells;

    public Map(Pattern layout)
    {
        _cells = layout.Content.ToList();

        PatternName = layout.Name;

        foreach (Cell cell in _cells) { cell.SetNeighbors(CalculateNeighbors(cell.Location)); }
    }

    public IReadOnlyList<Cell> Cells => _cells;

    public string PatternName { get; }

    public int Iteration { get; private set; }

    public void IncreaseIteration(int amount = 1)
    {
        Iteration += amount;

        amount.TimesAction(() =>
        {
            _cells.ForEach(c => c.MoveOn());
            _cells.ForEach(c => c.PrepareNextState());
        });
    }

    private IReadOnlyList<Cell> CalculateNeighbors(Location location)
    {
        List<Cell> neighbors = new();

        for (int columnNeighbor = location.Column - 1; columnNeighbor <= location.Column + 1; columnNeighbor++)
        {
            for (int rowNeighbor = location.Row - 1; rowNeighbor <= location.Row + 1; rowNeighbor++)
            {
                if (columnNeighbor == location.Column && rowNeighbor == location.Row) continue;

                (int column, int row) boundLocation = BoundaryCheck(columnNeighbor, rowNeighbor);

                neighbors.Add(_cells.Single(m => m.Location.Column == boundLocation.column && m.Location.Row == boundLocation.row));
            }
        }

        return neighbors;
    }

    private (int column, int row) BoundaryCheck(int column, int row)
    {
        int boundColumn = column < 0 ? _cells.Max(m => m.Location.Column) : column > _cells.Max(m => m.Location.Column) ? _cells.Min(m => m.Location.Column) : column;

        int boundRow = row < 0 ? _cells.Max(m => m.Location.Row) : row > _cells.Max(m => m.Location.Row) ? _cells.Min(m => m.Location.Row) : row;

        return (boundColumn, boundRow);
    }
}