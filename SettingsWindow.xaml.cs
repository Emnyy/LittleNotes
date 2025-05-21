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
using System.Text.Json;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.Storage.AccessCache;
using Windows.Storage.Pickers;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Littlenotes;

/// <summary>
/// An empty window that can be used on its own or navigated to within a Frame.
/// </summary>
public sealed partial class SettingsWindow : Window
{
    private readonly Window _window;

    public SettingsWindow()
    {
        InitializeComponent();
        _window = this;

        var displayArea = DisplayArea.GetFromWindowId(_window.AppWindow.Id, DisplayAreaFallback.Primary);
        var size = new Windows.Graphics.SizeInt32((int)(0.4 * displayArea.WorkArea.Width), (int)(0.4 * displayArea.WorkArea.Height));
        _window.AppWindow.Resize(size);
        _window.AppWindow.Move(new Windows.Graphics.PointInt32((int)(displayArea.WorkArea.Width - size.Width) / 2, (int)(displayArea.WorkArea.Height - size.Height) / 2));

        string path = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "My Notes");
        string configPath = System.IO.Path.Combine(path, "config.json");
        if (!File.Exists(configPath))
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            try
            {
                string json = JsonSerializer.Serialize(new Settings(), new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(configPath, json);
            }
            catch (Exception)
            {
                return;
            }
        }
        var settings = JsonSerializer.Deserialize<Settings>(File.ReadAllText(configPath));
        CurrentFolder.Text = settings.System.FolderLocation;
    }

    public Window MainWindow => _window;

    private void Exit(object sender, RoutedEventArgs e)
    {
        App.Current.Exit();
    }
    private async void Browse(object sender, RoutedEventArgs e)
    {
        //disable the button to avoid double-clicking
        var senderButton = sender as Button;
        senderButton.IsEnabled = false;

        // Create a folder picker
        FolderPicker openPicker = new();

        // Retrieve the window handle (HWND) of the current WinUI 3 window.
        var hWnd = WinRT.Interop.WindowNative.GetWindowHandle(_window);

        // Initialize the folder picker with the window handle (HWND).
        WinRT.Interop.InitializeWithWindow.Initialize(openPicker, hWnd);

        // Set options for your folder picker
        openPicker.SuggestedStartLocation = PickerLocationId.Desktop;
        openPicker.FileTypeFilter.Add("*");

        // Open the picker for the user to pick a folder
        StorageFolder folder = await openPicker.PickSingleFolderAsync();
        if (folder != null)
        {
            CurrentFolder.Text = folder.Path;
        }

        //re-enable the button
        senderButton.IsEnabled = true;
    }
}