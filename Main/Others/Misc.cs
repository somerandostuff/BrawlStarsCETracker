using System.Runtime.InteropServices;

namespace Main.Others
{
    public static class ModifyProgressBarColor
    {
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = false)]
        static extern IntPtr SendMessage(IntPtr WindowHandler, uint Msg, IntPtr WParam, IntPtr LParam);
        public static void SetState(this ProgressBar ProgBar, int State)
        {
            SendMessage(ProgBar.Handle, 1040, State, IntPtr.Zero);
        }
    }
}
