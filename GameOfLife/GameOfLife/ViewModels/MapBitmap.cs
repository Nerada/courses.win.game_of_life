// -----------------------------------------------
//     Author: Ramon Bollen
//      File: GameOfLife.MapBitmap.cs
// Created on: 20220808
// -----------------------------------------------

using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using GameOfLife.Model;

namespace GameOfLife.ViewModels;

public class MapBitmap
{
    private const    int             Stride     = 4;
    private readonly byte[]          _colorData = new byte[Stride];
    private readonly WriteableBitmap _writeableBitmap;

    public MapBitmap(int width, int height)
    {
        _writeableBitmap = new WriteableBitmap(width, height, 96.0, 96.0, PixelFormats.Pbgra32, null);
    }

    public ImageSource Source => _writeableBitmap;

    public void WritePixel(Location location, Color color)
    {
        _colorData[0] = color.B;
        _colorData[1] = color.G;
        _colorData[2] = color.R;
        _colorData[3] = color.A;

        Int32Rect sourceRect = new(location.Column, location.Row, 1, 1);
        _writeableBitmap.WritePixels(sourceRect, _colorData, Stride, 0);
    }
}