// -----------------------------------------------
//     Author: Ramon Bollen
//      File: GameOfLife.Map.cs
// Created on: 20220806
// -----------------------------------------------

using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GameOfLife.Extensions;

namespace GameOfLife.Model;

public class Map
{
    private readonly List<Cell> _cells;

    public Map(Pattern layout)
    {
        _cells = layout.Content.ToList();

        Pattern = layout;

        foreach (Cell cell in _cells) { cell.SetNeighbors(CalculateNeighbors(cell.Location)); }
    }

    public IReadOnlyList<Cell> Cells => _cells;

    public Pattern Pattern { get; }

    public int Iteration { get; private set; }

    public void IncreaseIteration(int amount = 1)
    {
        Iteration += amount;

        amount.TimesAction(() =>
        {
            _cells.ForEach(c => c.MoveOn());
            Parallel.ForEach(_cells, c => c.PrepareNextState());
        });
    }

    private IReadOnlyList<Cell> CalculateNeighbors(Location location)
    {
        ConcurrentBag<Cell> neighbors = new();

        Parallel.For(location.Column - 1, location.Column + 2, columnNeighbor =>
        {
            for (int rowNeighbor = location.Row - 1; rowNeighbor <= location.Row + 1; rowNeighbor++)
            {
                if (columnNeighbor == location.Column && rowNeighbor == location.Row) continue;

                (int column, int row) boundLocation = BoundaryCheck(columnNeighbor, rowNeighbor);

                neighbors.Add(_cells.Single(m => m.Location.Column == boundLocation.column && m.Location.Row == boundLocation.row));
            }
        });

        return neighbors.ToList();
    }

    private (int column, int row) BoundaryCheck(int column, int row)
    {
        int boundColumn = column < 0 ? _cells.Max(m => m.Location.Column) : column > _cells.Max(m => m.Location.Column) ? _cells.Min(m => m.Location.Column) : column;

        int boundRow = row < 0 ? _cells.Max(m => m.Location.Row) : row > _cells.Max(m => m.Location.Row) ? _cells.Min(m => m.Location.Row) : row;

        return (boundColumn, boundRow);
    }
}