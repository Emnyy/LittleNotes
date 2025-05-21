using Microsoft.UI.Windowing;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Littlenotes;

/// <summary>
/// An empty window that can be used on its own or navigated to within a Frame.
/// </summary>
public sealed partial class SettingsWindow : Window
{
    public SettingsWindow()
    {
        InitializeComponent();
        Window window = this;

        var displayArea = DisplayArea.GetFromWindowId(window.AppWindow.Id, DisplayAreaFallback.Primary);
        var size = new Windows.Graphics.SizeInt32((int)(0.4 * displayArea.WorkArea.Width), (int)(0.4 * displayArea.WorkArea.Height));
        window.AppWindow.Resize(size);

        window.AppWindow.Move(new Windows.Graphics.PointInt32((int)(displayArea.WorkArea.Width - size.Width) / 2, (int)(displayArea.WorkArea.Height - size.Height) / 2));
    }
    private void Exit(object sender, RoutedEventArgs e)
    {
        App.Current.Exit();
    }
}