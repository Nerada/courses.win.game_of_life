// -----------------------------------------------
//     Author: Ramon Bollen
//      File: GameOfLife.App.xaml.cs
// Created on: 20220805
// -----------------------------------------------

using System.Diagnostics.CodeAnalysis;
using System.Windows;
using GameOfLife.Model;
using GameOfLife.View;
using GameOfLife.ViewModels;

// ReSharper disable CommentTypo

namespace GameOfLife;

public partial class App
{
    /// <summary>
    ///     https://en.wikipedia.org/wiki/Conway%27s_Game_of_Life
    ///     The universe of the Game of Life is an infinite, two-dimensional orthogonal grid of square cells, each of which is
    ///     in one of two possible states, live or dead (or populated and unpopulated, respectively).
    ///     Every cell interacts with its eight neighbours, which are the cells that are horizontally, vertically, or
    ///     diagonally adjacent.
    ///     At each step in time, the following transitions occur:
    ///     1. Any live cell with fewer than two live neighbours dies, as if by underpopulation.
    ///     2. Any live cell with two or three live neighbours lives on to the next generation.
    ///     3. Any live cell with more than three live neighbours dies, as if by overpopulation.
    ///     4. Any dead cell with exactly three live neighbours becomes a live cell, as if by reproduction.
    /// </summary>
    [SuppressMessage("ReSharper", "StringLiteralTypo")]
    private void OnStartup(object sender, StartupEventArgs e)
    {
        //https://conwaylife.com/patterns/2eceater.rle
        Pattern pattern = new("2-engine Cordership", 41, 49, 10, @"
19b2o$19b4o$19bob2o2$20bo$19b2o$19b3o$21bo$33b2o$33b2o7$36bo$35b2o$34b
o3bo$35b2o2bo$40bo$37bobo$38bo$38bo$38b2o$38b2o3$13bo10bo$12b5o5bob2o
11bo$11bo10bo3bo9bo$12b2o8b3obo9b2o$13b2o9b2o12bo$2o13bo21b3o$2o35b3o
7$8b2o$8b2o11b2o$19b2o2bo$24bo3bo$18bo5bo3bo$19bo2b2o3bobo$20b3o5bo$
28bo!", "https://conwaylife.com/patterns/2enginecordership.rle");

        Map          newMap       = new(pattern);
        MapViewModel mapViewModel = new(newMap);

        MainWindow mainWindow = new();
        mainWindow.DataContext = mapViewModel;
        mainWindow.Show();
    }
}