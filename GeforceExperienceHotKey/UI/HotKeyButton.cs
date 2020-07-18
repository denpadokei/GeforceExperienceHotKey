using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows.Forms;
using BeatSaberMarkupLanguage;
using BeatSaberMarkupLanguage.Attributes;
using BeatSaberMarkupLanguage.Components;
using BeatSaberMarkupLanguage.ViewControllers;
using UnityEngine;

namespace GeforceExperienceHotKey.UI
{
    internal class HotKeyButton : NotifiableSingleton<HotKeyButton>
    {
        // For this method of setting the ResourceName, this class must be the first class in the file.
        public string ResourceName => string.Join(".", GetType().Namespace, "HotKeyButton.bsml");

        public void SetUp()
        {
            this._resultsViewController = Resources.FindObjectsOfTypeAll<ResultsViewController>().FirstOrDefault();
            BSMLParser.instance.Parse(Utilities.GetResourceContent(Assembly.GetExecutingAssembly(), this.ResourceName), this._resultsViewController.gameObject, this);
        }

        [UIAction("rec-click")]
        void RecButtonClick()
        {
            try {
                Logger.log.Debug("Clicked REC");
                
                keybd_event(ALT, 0, KEYEVENTF_EXTENDEDKEY, UIntPtr.Zero);
                keybd_event(F10, 0, KEYEVENTF_EXTENDEDKEY, UIntPtr.Zero);

                keybd_event(ALT, 0, KEYEVENTF_KEYUP, UIntPtr.Zero);
                keybd_event(F10, 0, KEYEVENTF_KEYUP, UIntPtr.Zero);

            }
            catch (Exception e) {
                Logger.log.Error(e);
            }
        }

        [DllImport("user32.dll")]
        public static extern uint keybd_event(byte bVk, byte bScan, uint dwFlags, UIntPtr dwExtraInfo);

        private ResultsViewController _resultsViewController;

        private static readonly byte ALT = 0x0012;
        private static readonly byte F10 = 0x0079;

        private static readonly byte KEYEVENTF_EXTENDEDKEY = 0x0001;
        private static readonly byte KEYEVENTF_KEYUP = 0x0002;

    }
}
