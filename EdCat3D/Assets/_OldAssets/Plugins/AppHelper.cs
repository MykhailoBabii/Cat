
using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

public static class AppHelper
{
    [DllImport("user32.dll")]
    private static extern bool SetForegroundWindow(IntPtr hWnd);
    [DllImport("user32.dll")]
    private static extern bool ShowWindowAsync(IntPtr hWnd, int nCmdShow);
    [DllImport("user32.dll")]
    private static extern bool IsIconic(IntPtr hWnd);

    public static bool isAlreadyRunning()
    {
        /*
        const int SW_HIDE = 0;
        const int SW_SHOWNORMAL = 1;
        const int SW_SHOWMINIMIZED = 2;
        const int SW_SHOWMAXIMIZED = 3;
        const int SW_SHOWNOACTIVATE = 4;
        const int SW_RESTORE = 9;
        const int SW_SHOWDEFAULT = 10;
        */
        const int swRestore = 9;
 
        var me = Process.GetCurrentProcess();
        var arrProcesses = Process.GetProcesses();

        if (arrProcesses.Length > 1)
        {
            for (var i = 0; i < arrProcesses.Length; i++)
            {
				try
				{
	                if ((arrProcesses[i].ProcessName == me.ProcessName)&&(arrProcesses[i].Id != me.Id))
	                {
	                    IntPtr hWnd = arrProcesses[i].MainWindowHandle;
	
	                    if (IsIconic(hWnd)) ShowWindowAsync(hWnd, swRestore);
	 
	                    SetForegroundWindow(hWnd);
	                    return true;
	                }
				}
				catch (Exception e)
				{
					// nothing
				}
            }
        }
        return false;
    }
}

