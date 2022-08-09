// -----------------------------------------------
//     Author: Ramon Bollen
//      File: GameOfLife.MapViewModel.cs
// Created on: 20220807
// -----------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;
using GameOfLife.Model;
using Prism.Commands;

namespace GameOfLife.ViewModels;

public class MapViewModel : ViewModelBase
{
    private readonly DispatcherTimer _dispatcherTimer = new();


    private Map     _map;
    private Pattern _currentPattern;

    public MapViewModel(Map map)
    {
        IncreaseIteration = new DelegateCommand(Iterate);
        ToggleAutoRun     = new DelegateCommand(AutoRun);

        _currentPattern = map.Pattern;

        _map = map;
        SetMap(map);

        _dispatcherTimer.Interval =  new TimeSpan(0, 0, 0, 0, 10);
        _dispatcherTimer.Tick     += (_, _) => Iterate();
    }

    public IReadOnlyList<Pattern> Patterns => PatternLib.Patterns.Values.ToList();

    public int Width => _map.Pattern.Columns * 5;

    public int Height => _map.Pattern.Rows * 5;

    public string PatternName => @$"{_map.Pattern.Info.Name} ({_map.Iteration})";

    public Uri? PatternUri => _map.Pattern.Info.Url;

    public ICommand IncreaseIteration { get; }

    public ICommand ToggleAutoRun { get; }

    public MapBitmap? Bitmap { get; private set; }

    public Pattern CurrentPattern
    {
        get => _currentPattern;
        set
        {
            if (!Set(ref _currentPattern, value)) return;

            SetMap(new Map(CurrentPattern));

            RaiseAllPropertyChanged();
        }
    }

    private void SetMap(Map map)
    {
        _dispatcherTimer.Stop();

        _map = map;

        Bitmap = new MapBitmap(_map.Pattern.Columns, _map.Pattern.Rows);
        _map.Cells.ToList().ForEach(UpdatePixel);
    }

    private void AutoRun()
    {
        if (_dispatcherTimer.IsEnabled)
            _dispatcherTimer.Stop();
        else
            _dispatcherTimer.Start();
    }

    private void Iterate()
    {
        _map.IncreaseIteration();
        _map.Cells.Where(cell => cell.Changed).ToList().ForEach(UpdatePixel);
        RaisePropertyChanged(nameof(PatternName));
    }

    private void UpdatePixel(Cell cell) => Bitmap?.WritePixel(cell.Location, cell.State == CellState.Alive ? Colors.OrangeRed : Colors.Gray);

    private void RaiseAllPropertyChanged()
    {
        RaisePropertyChanged(nameof(PatternName));
        RaisePropertyChanged(nameof(Bitmap));
        RaisePropertyChanged(nameof(Width));
        RaisePropertyChanged(nameof(Height));
    }
}