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
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Graphics;
using Windows.Graphics.Display;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Littlenotes
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Window window = this;
            window.ExtendsContentIntoTitleBar = true;
            window.AppWindow.TitleBar.PreferredHeightOption = TitleBarHeightOption.Collapsed;

            var displayArea = DisplayArea.GetFromWindowId(window.AppWindow.Id, DisplayAreaFallback.Primary);
            var wSize = 0.4 * displayArea.WorkArea.Height;

            var newSize = new Windows.Graphics.SizeInt32((int)(wSize), (int)(wSize));
            window.AppWindow.Resize(newSize);

            var dragRect = new Windows.Graphics.RectInt32(0, 0, newSize.Width, newSize.Height);
            window.AppWindow.TitleBar.SetDragRectangles([dragRect]);

        }
        private void NoteExit(object sender, global::Microsoft.UI.Xaml.Input.PointerRoutedEventArgs e)
        {
            App.Current.Exit();
        }
        private void NoteHover(object sender, global::Microsoft.UI.Xaml.Input.PointerRoutedEventArgs e)
        {
            if (sender is Border border && border.Child is FontIcon icon)
            {
                if (Application.Current.Resources["TextOnAccentFillColorSecondaryBrush"] is SolidColorBrush brush)
                {
                    icon.Foreground = new SolidColorBrush(brush.Color);
                }
            }
        }
        private void NoteHoverStop(object sender, global::Microsoft.UI.Xaml.Input.PointerRoutedEventArgs e)
        {
            if (sender is Border border && border.Child is FontIcon icon)
            {
                if (Application.Current.Resources["TextOnAccentFillColorPrimaryBrush"] is SolidColorBrush brush)
                {
                    icon.Foreground = new SolidColorBrush(brush.Color);
                }
            }
        }
    }
}