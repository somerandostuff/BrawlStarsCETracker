using System.Runtime.InteropServices;

namespace Main.Others
{
    public static class ModifyProgressBarColor
    {
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = false)]
        static extern IntPtr SendMessage(IntPtr HWnd, uint Msg, IntPtr WParam, IntPtr LParam);
        public static void SetState(this ProgressBar ProgBar, ProgressBarState State)
        {
            SendMessage(ProgBar.Handle, 1040, (int)State, IntPtr.Zero);
        }
    }
    public class LoadFontIntoMemory
    {
        [DllImport("gdi32.dll")]
        public static extern IntPtr AddFontMemResourceEx(IntPtr PbFont, uint CbFont,
            IntPtr Pdv, [In] ref uint PcFonts);
    }

    public enum ProgressBarState
    {
        Normal = 0,
        Warning = 1,
        Stopped = 2
    }
}
