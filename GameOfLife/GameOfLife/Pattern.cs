// -----------------------------------------------
//     Author: Ramon Bollen
//      File: GameOfLife.Pattern.cs
// Created on: 20220807
// -----------------------------------------------

using System.Collections.Generic;
using GameOfLife.Extensions;
using GameOfLife.Model;

namespace GameOfLife;

public class Pattern
{
    public Pattern(string name, int columns, int margin, string content)
    {
        Name    = name;
        Content = content.ToMap(columns, margin);
    }

    public string              Name    { get; }
    public IReadOnlyList<Cell> Content { get; }
}