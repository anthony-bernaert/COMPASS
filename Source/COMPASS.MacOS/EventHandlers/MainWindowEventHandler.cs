using System;
using COMPASS.Common.Controls;
using COMPASS.Common.Interfaces;
using COMPASS.Common.Views.Windows;
using COMPASS.MacOS.Helpers;

namespace COMPASS.MacOS.EventHandlers;

public class MainWindowEventHandler : IWindowEventHandler<MainWindow>
{
    public void OnConstructed(CompassWindow w)
    {
        var platformHandle = w.TryGetPlatformHandle();
        IntPtr nsWindowHandle = platformHandle?.Handle ?? IntPtr.Zero;
        if (nsWindowHandle != IntPtr.Zero)
        {
            // Move the macOS window controls to the middle of the Avalonia title bar by adding an empty NSToolbar
            AppKitInterop.EnlargeTitleBar(nsWindowHandle);
        }
    }

    public void OnClosed(CompassWindow compassWindow)
    {
    }

    public void OnOpened(CompassWindow compassWindow)
    {
    }
}