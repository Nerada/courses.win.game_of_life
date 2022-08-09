// -----------------------------------------------
//     Author: Ramon Bollen
//      File: GameOfLife.MainWindow.xaml.cs
// Created on: 20220807
// -----------------------------------------------

using System.Diagnostics;
using System.Windows.Navigation;

namespace GameOfLife.View;

/// <summary>
///     Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow
{
    public MainWindow()
    {
        InitializeComponent();
    }

    private void Hyperlink_RequestNavigate(object sender, RequestNavigateEventArgs e) => Process.Start(new ProcessStartInfo(e.Uri.AbsoluteUri) {UseShellExecute = true});
}