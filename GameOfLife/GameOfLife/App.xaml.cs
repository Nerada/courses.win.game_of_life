// -----------------------------------------------
//     Author: Ramon Bollen
//      File: GameOfLife.App.xaml.cs
// Created on: 20220805
// -----------------------------------------------

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
    private void OnStartup(object sender, StartupEventArgs e)
    {
        MapViewModel mapViewModel = new(new Map(PatternLib.Patterns["Ramon"]));

        MainWindow mainWindow = new()
        {
            DataContext = mapViewModel
        };

        mainWindow.Show();
    }
}