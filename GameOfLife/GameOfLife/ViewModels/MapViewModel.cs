// -----------------------------------------------
//     Author: Ramon Bollen
//      File: GameOfLife.MapViewModel.cs
// Created on: 20220807
// -----------------------------------------------

using System.Linq;
using System.Windows.Input;
using System.Windows.Media;
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

        Bitmap = new MapBitmap(_map.Pattern.Columns, _map.Pattern.Rows);
        _map.Cells.ToList().ForEach(UpdatePixel);
    }

    public MapBitmap Bitmap { get; }

    public int Width => _map.Pattern.Columns * 10;

    public int Height => _map.Pattern.Rows * 10;

    public string PatternName => @$"{_map.Pattern.Info.Name} ({_map.Iteration})";

    public ICommand IncreaseIteration { get; }

    private void Iterate()
    {
        _map.IncreaseIteration();
        _map.Cells.Where(cell => cell.Changed).ToList().ForEach(UpdatePixel);
        RaisePropertyChanged(nameof(PatternName));
        RaisePropertyChanged(nameof(Bitmap));
    }

    private void UpdatePixel(Cell cell) => Bitmap.WritePixel(cell.Location, cell.Status == Cell.State.Alive ? Colors.OrangeRed : Colors.Gray);
}