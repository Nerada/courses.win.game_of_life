// -----------------------------------------------
//     Author: Ramon Bollen
//      File: GameOfLife.Progress.cs
// Created on: 20220816
// -----------------------------------------------

using System;
using System.Windows;

namespace GameOfLife.ViewModels;

public delegate void VoidHandler();

public class Progress
{
    public double Max => 100.0;

    public double Value { get; private set; }

    public void IncreaseValue(double amount) => Application.Current.Dispatcher.BeginInvoke(new Action(() =>
    {
        Value += amount;
        ValueChanged?.Invoke();
    }));

    public void Reset()
    {
        Value = 0;
        ValueChanged?.Invoke();
    }

    public event VoidHandler? ValueChanged;
}