#region Imports

using System.ComponentModel;
using System.Runtime.InteropServices;

#endregion

namespace ReaLTaiizor.Controls
{
    #region ParrotVolumeController

    public class ParrotVolumeController : Component
    {
        [DllImport("user32.dll")]
        private static extern void keybd_event(byte bVk, byte bScan, uint dwFlags, uint dwExtraInfo);

        [DllImport("user32.dll")]
        private static extern byte MapVirtualKey(uint uCode, uint uMapType);

        public void VolumeUp()
        {
            keybd_event(VK_VOLUME_UP, MapVirtualKey(VK_VOLUME_UP_U, KEYEVENTF_NONKEY), KEYEVENTF_EXTENDEDKEY, KEYEVENTF_NONKEY);
            keybd_event(VK_VOLUME_UP, MapVirtualKey(VK_VOLUME_UP_U, KEYEVENTF_NONKEY), KEYEVENTF_TOPUP, KEYEVENTF_NONKEY);
        }

        public void VolumeDown()
        {
            keybd_event(VK_VOLUME_DOWN, MapVirtualKey(VK_VOLUME_DOWN_U, KEYEVENTF_NONKEY), KEYEVENTF_EXTENDEDKEY, KEYEVENTF_NONKEY);
            keybd_event(VK_VOLUME_DOWN, MapVirtualKey(VK_VOLUME_DOWN_U, KEYEVENTF_NONKEY), KEYEVENTF_TOPUP, KEYEVENTF_NONKEY);
        }

        public void Mute()
        {
            keybd_event(VK_VOLUME_MUTE, MapVirtualKey(VK_VOLUME_MUTE_U, KEYEVENTF_NONKEY), KEYEVENTF_EXTENDEDKEY, KEYEVENTF_NONKEY);
            keybd_event(VK_VOLUME_MUTE, MapVirtualKey(VK_VOLUME_MUTE_U, KEYEVENTF_NONKEY), KEYEVENTF_TOPUP, KEYEVENTF_NONKEY);
        }

        public void SetVolume(int value)
        {
            for (int num = 0; num != 50; num++)
            {
                keybd_event(VK_VOLUME_DOWN, MapVirtualKey(VK_VOLUME_DOWN_U, KEYEVENTF_NONKEY), KEYEVENTF_EXTENDEDKEY, KEYEVENTF_NONKEY);
                keybd_event(VK_VOLUME_DOWN, MapVirtualKey(VK_VOLUME_DOWN_U, KEYEVENTF_NONKEY), KEYEVENTF_TOPUP, KEYEVENTF_NONKEY);
            }
            if (value > 0)
            {
                for (int num = 0; num != value / 2; num++)
                {
                    keybd_event(VK_VOLUME_UP, MapVirtualKey(VK_VOLUME_UP_U, KEYEVENTF_NONKEY), KEYEVENTF_EXTENDEDKEY, KEYEVENTF_NONKEY);
                    keybd_event(VK_VOLUME_UP, MapVirtualKey(VK_VOLUME_UP_U, KEYEVENTF_NONKEY), KEYEVENTF_TOPUP, KEYEVENTF_NONKEY);
                }
            }
        }

        private const byte VK_VOLUME_MUTE = 173;
        private const uint VK_VOLUME_MUTE_U = 173U;

        private const byte VK_VOLUME_DOWN = 174;
        private const uint VK_VOLUME_DOWN_U = 174U;

        private const byte VK_VOLUME_UP = 175;
        private const uint VK_VOLUME_UP_U = 175U;

        private const uint KEYEVENTF_NONKEY = 0U;
        private const uint KEYEVENTF_EXTENDEDKEY = 1U;
        private const uint KEYEVENTF_KEYUP = 2U;
        private const uint KEYEVENTF_TOPUP = 3U;
    }

    #endregion
}