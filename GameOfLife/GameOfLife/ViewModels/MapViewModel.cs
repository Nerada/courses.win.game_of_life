// -----------------------------------------------
//     Author: Ramon Bollen
//      File: GameOfLife.MapViewModel.cs
// Created on: 20220807
// -----------------------------------------------

using System;
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
    private readonly Map             _map;

    public MapViewModel(Map map)
    {
        _map              = map;
        IncreaseIteration = new DelegateCommand(Iterate);
        ToggleAutoRun     = new DelegateCommand(AutoRun);

        Bitmap = new MapBitmap(_map.Pattern.Columns, _map.Pattern.Rows);
        _map.Cells.ToList().ForEach(UpdatePixel);

        _dispatcherTimer.Interval =  new TimeSpan(0, 0, 0, 0, 10);
        _dispatcherTimer.Tick     += (_, _) => Iterate();
    }

    public MapBitmap Bitmap { get; }

    public int Width => _map.Pattern.Columns * 5;

    public int Height => _map.Pattern.Rows * 5;

    public string PatternName => @$"{_map.Pattern.Info.Name} ({_map.Iteration})";

    public Uri PatternUri => _map.Pattern.Info.Url;

    public ICommand IncreaseIteration { get; }

    public ICommand ToggleAutoRun { get; }

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

    private void UpdatePixel(Cell cell) => Bitmap.WritePixel(cell.Location, cell.Status == Cell.State.Alive ? Colors.OrangeRed : Colors.Gray);
}