using GeforceExperienceHotKey.UI;
using GeforceExperienceHotKey.Utils;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GeforceExperienceHotKey
{
    /// <summary>
    /// Monobehaviours (scripts) are added to GameObjects.
    /// For a full list of Messages a Monobehaviour can receive from the game, see https://docs.unity3d.com/ScriptReference/MonoBehaviour.html.
    /// </summary>
    public class GeforceExperienceHotKeyController : MonoBehaviour
    {
        public static GeforceExperienceHotKeyController instance { get; private set; }
        #region Monobehaviour Messages
        /// <summary>
        /// Only ever called once, mainly used to initialize variables.
        /// </summary>
        private void Awake()
        {
            // For this particular MonoBehaviour, we only want one instance to exist at any time, so store a reference to it in a static property
            //   and destroy any that are created while one already exists.
            if (instance != null) {
                Logger.log?.Warn($"Instance of {this.GetType().Name} already exists, destroying.");
                GameObject.DestroyImmediate(this);
                return;
            }
            GameObject.DontDestroyOnLoad(this); // Don't destroy this object on scene changes
            instance = this;
            SceneManager.activeSceneChanged -= this.SceneManager_activeSceneChanged;
            SceneManager.activeSceneChanged += this.SceneManager_activeSceneChanged;
            Logger.log?.Debug($"{name}: Awake()");
        }

        private void SceneManager_activeSceneChanged(Scene arg0, Scene arg1)
        {
            try {
                var Winhdl = WindowManager.FindWindow(null, "Beat Saber");
                if (Winhdl == IntPtr.Zero) {
                    return;
                }
                WindowManager.ActiveWindow(Winhdl);
            }
            catch (Exception e) {
                Logger.log.Error(e);
            }
        }

        public void SetUp()
        {
            HotKeyButton.instance.SetUp();
        }

        private void OnDestroy()
        {
            Logger.log?.Debug($"{name}: OnDestroy()");
            instance = null; // This MonoBehaviour is being destroyed, so set the static instance property to null.
        }
        #endregion
    }
}
