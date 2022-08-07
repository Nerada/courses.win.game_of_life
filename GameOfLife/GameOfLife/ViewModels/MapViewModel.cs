// -----------------------------------------------
//     Author: Ramon Bollen
//      File: GameOfLife.MapViewModel.cs
// Created on: 20220807
// -----------------------------------------------

using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using GameOfLife.Model;
using Prism.Commands;

namespace GameOfLife.ViewModels;

public class MapViewModel : ViewModelBase
{
    private readonly Map _map;

    public MapViewModel(Map map)
    {
        _map              = map;
        IncreaseIteration = new DelegateCommand(Iterate);
    }

    public string PatternName => @$"{_map.PatternName} ({_map.Iteration})";

    public IReadOnlyList<Cell> Cells => _map.Cells.ToList();

    public ICommand IncreaseIteration { get; }

    private void Iterate()
    {
        _map.IncreaseIteration();
        RaisePropertyChanged(nameof(PatternName));
        RaisePropertyChanged(nameof(Cells));
    }
}