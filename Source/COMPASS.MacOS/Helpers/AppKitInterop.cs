using System;
using System.Runtime.InteropServices;

namespace COMPASS.MacOS.Helpers;

public static class AppKitInterop
{
    [DllImport("/System/Library/Frameworks/AppKit.framework/AppKit")]
    private static extern IntPtr objc_getClass(string className);

    [DllImport("/System/Library/Frameworks/AppKit.framework/AppKit")]
    private static extern IntPtr sel_registerName(string selector);

    [DllImport("/System/Library/Frameworks/AppKit.framework/AppKit")]
    private static extern void objc_msgSend(IntPtr receiver, IntPtr selector, IntPtr arg);
    
    [DllImport("/usr/lib/libobjc.dylib", EntryPoint = "objc_msgSend")]
    private static extern IntPtr Intptr_objc_msgSend(IntPtr receiver, IntPtr selector);

    public static void EnlargeTitleBar(IntPtr nsWindow)
    {
        objc_msgSend(nsWindow, sel_registerName("setStyleMask:"), 32769);
        objc_msgSend(nsWindow, sel_registerName("setTitlebarAppearsTransparent:"), 1);
        IntPtr toolbarClass = objc_getClass("NSToolbar");
        IntPtr alloc = sel_registerName("alloc");
        IntPtr init = sel_registerName("init");
        IntPtr toolbar = Intptr_objc_msgSend(Intptr_objc_msgSend(toolbarClass, alloc), init);
        objc_msgSend(nsWindow, sel_registerName("setToolbarStyle:"), 3);
    }
}