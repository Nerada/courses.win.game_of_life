﻿// -----------------------------------------------
//     Author: Ramon Bollen
//      File: GameOfLife.Pattern.cs
// Created on: 20220807
// -----------------------------------------------

using System;
using System.Collections.Generic;
using GameOfLife.Extensions;
using GameOfLife.Model;

namespace GameOfLife;

public class Pattern
{
    public Pattern(string name, int columns, int rows, int margin, string content, Uri? source = null)
    {
        Info    = new MetaData(name, source);
        Content = content.ToMap(columns, margin);
        Columns = columns + margin * 2;
        Rows    = rows    + margin * 2;
    }

    public MetaData Info { get; }

    public int Columns { get; }

    public int Rows { get; }

    public IReadOnlyList<BaseCell> Content { get; }

    public record MetaData(string Name, Uri? Url);
}