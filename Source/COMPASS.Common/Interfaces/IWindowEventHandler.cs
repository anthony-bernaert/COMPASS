using COMPASS.Common.Controls;

namespace COMPASS.Common.Interfaces;

public interface IWindowEventHandler<in T> : IWindowEventHandler where T : CompassWindow
{
}

public interface IWindowEventHandler
{
    public void OnConstructed(CompassWindow compassWindow);
    
    public void OnClosed(CompassWindow compassWindow);

    public void OnOpened(CompassWindow compassWindow);
}