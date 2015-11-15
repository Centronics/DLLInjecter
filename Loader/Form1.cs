using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Resources;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;

[assembly: CLSCompliant(false)]
namespace Loader
{
    public partial class MainFrm : Form
    {
        static class NativeMethods
        {
            [DllImport("kernel32.dll")]
            public static extern UIntPtr CreateRemoteThread(UIntPtr hProcess, UIntPtr lpThreadAttributes, UIntPtr dwStackSize, UIntPtr lpStartAddress, UIntPtr lpParameter, UInt32 dwCreationFlags, out UIntPtr lpThreadId);

            [DllImport("kernel32.dll")]
            public static extern UIntPtr OpenProcess(UInt32 dwDesiredAccess, UInt32 bInheritHandle, Int32 dwProcessId);

            [DllImport("kernel32.dll")]
            public static extern UInt32 CloseHandle(UIntPtr hObject);

            [DllImport("kernel32.dll", SetLastError = true)]
            public static extern UInt32 VirtualFreeEx(UIntPtr hProcess, UIntPtr lpAddress, UIntPtr dwSize, UInt32 dwFreeType);

            [DllImport("kernel32.dll", CharSet = CharSet.Ansi, ExactSpelling = true, BestFitMapping = false, ThrowOnUnmappableChar = true)]
            public static extern UIntPtr GetProcAddress(UIntPtr hModule, String procName);

            [DllImport("kernel32.dll", SetLastError = true)]
            public static extern UIntPtr VirtualAllocEx(UIntPtr hProcess, UIntPtr lpAddress, UIntPtr dwSize, UInt32 flAllocationType, UInt32 flProtect);

            [DllImport("kernel32.dll", CharSet = CharSet.Unicode)]
            public static extern Int32 WriteProcessMemory(UIntPtr hProcess, UIntPtr lpBaseAddress, String lpBuffer, UIntPtr nSize, out UIntPtr lpNumberOfBytesWritten);

            [DllImport("kernel32.dll", CharSet = CharSet.Unicode, ExactSpelling = true)]
            public static extern UIntPtr GetModuleHandleW(String lpModuleName);

            [DllImport("kernel32.dll", SetLastError = true)]
            public static extern UInt32 WaitForSingleObject(UIntPtr handle, UInt32 milliseconds);
        }

        enum ERESULT
        {
            FILENOTFOUND,
            INTERNERROR,
            OK
        }

        readonly static string myfile = Application.StartupPath + "\\cheal.dll";
        readonly static FileInfo fi = new FileInfo(myfile);
        readonly Icon BoxR = null, BoxO = null, BoxG = null;
        readonly Thread MainThread = null;

        public MainFrm()
        {
            InitializeComponent();
            ResourceManager resourceManager = new ResourceManager("Loader.Indicator", typeof(MainFrm).Assembly);
            BoxR = (Icon)resourceManager.GetObject("Boxr");
            BoxO = (Icon)resourceManager.GetObject("Boxo");
            BoxG = (Icon)resourceManager.GetObject("Boxg");
            MainThread = new Thread(new ThreadStart(FindProcess));
            MainThread.Name = "DLL Injecter Thread";
            MainThread.Start();
        }

        static Int32 FEARMPId
        {
            get
            {
                Process[] procs = Process.GetProcesses();
                foreach (Process proc in procs)
                    if (proc.ProcessName == "FEARMP")
                        return proc.Id;
                return 0;
            }
        }

        static ERESULT InjectDLL(Int32 pr)
        {
            fi.Refresh();
            if (!fi.Exists)
                return ERESULT.FILENOTFOUND;
            ERESULT result = ERESULT.INTERNERROR;
            if (pr != 0)
            {
                Thread.Sleep(20000);
                try
                {
                    UIntPtr hProcess = NativeMethods.OpenProcess(0xFFFF, 0, pr);
                    if ((hProcess != UIntPtr.Zero))
                    {
                        UIntPtr bytesout = UIntPtr.Zero;
                        UInt32 LenWrite = Convert.ToUInt32((myfile.Length + 1) * 2);
                        if (LenWrite > 1)
                        {
                            UIntPtr AllocMem = NativeMethods.VirtualAllocEx(hProcess, UIntPtr.Zero, (UIntPtr)LenWrite, 0x1000, 0x40);
                            if (AllocMem != UIntPtr.Zero)
                            {
                                if (NativeMethods.WriteProcessMemory(hProcess, AllocMem, myfile, (UIntPtr)LenWrite, out bytesout) != 0)
                                {
                                    UIntPtr Injector = NativeMethods.GetProcAddress(NativeMethods.GetModuleHandleW("Kernel32"), "LoadLibraryW");
                                    if (Injector != UIntPtr.Zero)
                                    {
                                        UIntPtr hThread = NativeMethods.CreateRemoteThread(hProcess, UIntPtr.Zero, UIntPtr.Zero, Injector, AllocMem, 0, out bytesout);
                                        if (hThread != UIntPtr.Zero)
                                        {
                                            result = ERESULT.OK;
                                            if (NativeMethods.WaitForSingleObject(hThread, 0xFFFFFFFF) != 0) result = ERESULT.INTERNERROR;
                                            if (NativeMethods.VirtualFreeEx(hProcess, AllocMem, UIntPtr.Zero, 0x8000) == 0) result = ERESULT.INTERNERROR;
                                            if (NativeMethods.CloseHandle(hThread) == 0) result = ERESULT.INTERNERROR;
                                            if (NativeMethods.CloseHandle(hProcess) == 0) result = ERESULT.INTERNERROR;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                catch
                {
                    return ERESULT.INTERNERROR;
                }
            }
            else
                return ERESULT.INTERNERROR;
            return result;
        }

        static bool ProcessFound = false;
        private void FindProcess()
        {
            while (!Program.ProgramFinished)
            {
                if (!ProcessFound)
                    switch (InjectDLL(FEARMPId))
                    {
                        case ERESULT.OK:
                            ni.Icon = BoxG;
                            ProcessFound = true;
                            break;
                        case ERESULT.FILENOTFOUND:
                            ni.Icon = BoxO;
                            break;
                        case ERESULT.INTERNERROR:
                            ni.Icon = BoxR;
                            break;
                    }
                else
                    ProcessFound = (FEARMPId != 0);
                GC.Collect();
                Thread.Sleep(500);
            }
        }

        static readonly FrmHelp helper = new FrmHelp();
        private void ni_MouseUp(object sender, MouseEventArgs e)
        {
            switch (e.Button)
            {
                case System.Windows.Forms.MouseButtons.Left:
                    if (!helper.Visible)
                        helper.ShowDialog();
                    break;
                case System.Windows.Forms.MouseButtons.Right:
                    helper.Dispose();
                    Program.ProgramFinished = true;
                    MainThread.Join();
                    Application.Exit();
                    break;
            }
        }
    }
}