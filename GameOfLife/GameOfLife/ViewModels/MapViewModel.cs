// -----------------------------------------------
//     Author: Ramon Bollen
//      File: GameOfLife.MapViewModel.cs
// Created on: 20220807
// -----------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Threading;
using GameOfLife.Model;
using Prism.Commands;

namespace GameOfLife.ViewModels;

public class MapViewModel : ViewModelBase
{
    private readonly DispatcherTimer _dispatcherTimer = new();

    private bool _loading;


    private Map     _map;
    private Pattern _currentPattern = new("Loading", 0, 0, 0, string.Empty);

    public MapViewModel(Pattern initialPattern, Progress progress)
    {
        _map = new Map(_currentPattern, progress);

        _dispatcherTimer.Interval =  new TimeSpan(0, 0, 0, 0, 10);
        _dispatcherTimer.Tick     += (_, _) => Iterate();

        progress.ValueChanged += OnProgressValueChanged;

        IncreaseIteration = new DelegateCommand(Iterate, () => !Loading);
        ToggleAutoRun     = new DelegateCommand(AutoRun, () => !Loading);

        Progress       = progress;
        CurrentPattern = initialPattern;
    }

    public IReadOnlyList<Pattern> Patterns => PatternLib.Patterns.Values.ToList();

    public int Width => _map.Pattern.Columns * 5;

    public int Height => _map.Pattern.Rows * 5;

    public Progress Progress { get; }

    public string PatternName => @$"{_map.Pattern.Info.Name} ({_map.Iteration})";

    public Uri? PatternUri => _map.Pattern.Info.Url;

    public DelegateCommand IncreaseIteration { get; }

    public DelegateCommand ToggleAutoRun { get; }

    public MapBitmap? Bitmap { get; private set; }

    public bool Loading
    {
        get => _loading;
        private set => Set(ref _loading, value);
    }

    public Pattern CurrentPattern
    {
        get => _currentPattern;
        set
        {
            if (!Set(ref _currentPattern, value)) return;

            CreateNewMap();
        }
    }

    private async void CreateNewMap()
    {
        StartLoading();

        Task<Map> newMapTask = Task.Run(() => new Map(CurrentPattern, Progress));

        await newMapTask.ContinueWith(newMap =>
        {
            Application.Current.Dispatcher.BeginInvoke(new Action(() =>
            {
                SetMap(newMap.Result);
                DoneLoading();
            }));
        });
    }

    private void OnProgressValueChanged() => RaisePropertyChanged(nameof(Progress));

    private void SetMap(Map map)
    {
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

    private void StartLoading()
    {
        Loading = true;
        _dispatcherTimer.Stop();

        IncreaseIteration.RaiseCanExecuteChanged();
        ToggleAutoRun.RaiseCanExecuteChanged();
    }

    private void DoneLoading()
    {
        Loading = false;
        Progress.Reset();

        RaisePropertyChanged(nameof(PatternName));
        RaisePropertyChanged(nameof(PatternUri));
        RaisePropertyChanged(nameof(Bitmap));
        RaisePropertyChanged(nameof(Width));
        RaisePropertyChanged(nameof(Height));
        RaisePropertyChanged(nameof(Progress));

        IncreaseIteration.RaiseCanExecuteChanged();
        ToggleAutoRun.RaiseCanExecuteChanged();
    }
}