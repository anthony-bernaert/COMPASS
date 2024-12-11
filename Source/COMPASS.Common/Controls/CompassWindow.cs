using System;
using System.Collections;
using System.Collections.Generic;
using Autofac;
using Avalonia.Controls;
using Avalonia.Input;
using COMPASS.Common.Interfaces;

namespace COMPASS.Common.Controls;

public class CompassWindow : Window
{
    
    public CompassWindow()
    {
        InitializeWindowEventHandlers();
    }

    public void InitializeWindowEventHandlers()
    {
        if (App.Container is not null) // FIXME: should always be available
        {
            var windowType = this.GetType();
            var eventHandlerType = typeof (IWindowEventHandler<>).MakeGenericType(windowType); // Construct type IWindowEventHandler<T> where T is the current window type
            var eventHandlerListType = typeof (IEnumerable<>).MakeGenericType(eventHandlerType); // Construct type IEnumerable<IWindowEventHandler<T>>
            if (App.Container.Resolve(eventHandlerListType) is IEnumerable handlerObjects) // Resolve all IWindowEventHandlers implementations for the current window type
            {
                foreach (IWindowEventHandler handler in handlerObjects)
                {
                    AddHandler(WindowOpenedEvent, (sender, args) => handler.OnOpened(this));
                    AddHandler(WindowClosedEvent, (sender, args) => handler.OnClosed(this));
                    handler.OnConstructed(this);
                }
            }
        }
    }
}