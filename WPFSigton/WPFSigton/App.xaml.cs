using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace WPFSigton
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {

        [DllImport("user32", CharSet = CharSet.Unicode)]
        static extern IntPtr FindWindow(string cls, string win);//查找窗体
        [DllImport("user32")]
        static extern IntPtr SetForegroundWindow(IntPtr hWnd);//显示窗体到前台
        [DllImport("user32")]
        static extern bool IsIconic(IntPtr hWnd);
        [DllImport("user32")]
        static extern bool OpenIcon(IntPtr hWnd);

        protected override void OnStartup(StartupEventArgs e)
        {
            bool isNew;
            Mutex mutex = new Mutex(true, "My Singleton Instance", out isNew); //isNew 窗体已存在 返回false
            if (!isNew)
            {
                ActivateOtherWindow();
                Shutdown();
            }
        }
        private static void ActivateOtherWindow()
        {
             IntPtr other = FindWindow(null, "MainWindow");//参数一 类名，参数二 窗口名称
            if (other != IntPtr.Zero)
            {
                SetForegroundWindow(other);
                if (IsIconic(other))
                    OpenIcon(other);
            }
        }

    }
}
