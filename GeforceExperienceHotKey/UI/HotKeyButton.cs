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

        /// <summary>ボタンの有効か無効か を取得、設定</summary>
        private bool buttonIntaractable_;
        /// <summary>ボタンの有効か無効か を取得、設定</summary>
        [UIValue("button-intaractable")]
        public bool ButtonIntaractable
        {
            get => this.buttonIntaractable_;

            set
            {
                if (this.buttonIntaractable_ != value) {
                    this.buttonIntaractable_ = value;
                    this.NotifyPropertyChanged();
                }
            }
        }

        public void SetUp()
        {
            this._resultsViewController = Resources.FindObjectsOfTypeAll<ResultsViewController>().FirstOrDefault();
#if DEBUG
            foreach (var item in this._resultsViewController.GetComponentsInChildren<object>(true)) {
                Logger.log.Debug($"{item}");
            }
#endif
            BSMLParser.instance.Parse(Utilities.GetResourceContent(Assembly.GetExecutingAssembly(), this.ResourceName), this._resultsViewController.gameObject, this);
            this._resultsViewController.didActivateEvent -= this._resultsViewController_didActivateEvent;
            this._resultsViewController.didActivateEvent += this._resultsViewController_didActivateEvent;
        }

        private void _resultsViewController_didActivateEvent(bool firstActivation, bool addedToHierarchy, bool screenSystemEnabling)
        {
            this.ButtonIntaractable = true;
        }

        [UIAction("rec-click")]
        void RecButtonClick()
        {
            try {
                Logger.log.Debug("Clicked REC");
                
                keybd_event(ALT, 0, 0, UIntPtr.Zero);
                keybd_event(F10, 0, 0, UIntPtr.Zero);

                keybd_event(F10, 0, KEYEVENTF_KEYUP, UIntPtr.Zero);
                keybd_event(ALT, 0, KEYEVENTF_KEYUP, UIntPtr.Zero);
                this.ButtonIntaractable = false;
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

        private static readonly byte KEYEVENTF_KEYUP = 0x0002;

    }
}
