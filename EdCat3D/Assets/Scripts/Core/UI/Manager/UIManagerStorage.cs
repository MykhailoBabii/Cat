using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core.UI
{
    public static class UIManagerStorage
    {
        private static Dictionary<ScreenType, string> _screens = new Dictionary<ScreenType, string>()
        {
            { ScreenType.Game, "Prefabs/UI/GameScreen" },
            { ScreenType.Loading, "Prefabs/UI/Loading" },
        };

        private static Dictionary<WindowType, string> _windows = new Dictionary<WindowType, string>()
        {
            { WindowType.Settings, "Prefabs/UI/SettingsWindow" },
        };

        public static Dictionary<PopupType, string> _popups = new Dictionary<PopupType, string>()
        {
            { PopupType.DetailedInfo, "Prefabs/UI/ConfirmExitFromBattle" },
        };

        private static string _gameComponent = "Prefabs/Game/WorldSpacePrefabs/Container";

        private static string _messageContainer = "Prefabs/UI/MessageConteiner";

        public static string GetScreen(ScreenType type)
        {
            return _screens[type];
        }

        public static string GetWindow(WindowType type)
        {
            return _windows[type];
        }

        public static string GetPopup(PopupType type)
        {
            return _popups[type];
        }

        public static string GetGameComponent()
        {
            return _gameComponent;
        }

        public static string GetMessageConteiner()
        {
            return _messageContainer;
        }
    }

    [System.Serializable]
    public class ViewResourceDescriptor<TView>
    {
        [SerializeField] private TView _type;
        [SerializeField] private string _path;

        public TView Type => _type;
        public string Path => _path;
    }

    [System.Serializable]
    public class WindowResourceDescriptor: ViewResourceDescriptor<WindowType> {}

    [System.Serializable]
    public class ScreenResourceDescriptor: ViewResourceDescriptor<ScreenType> { }

    [System.Serializable]
    public class PopupResourceDescriptor: ViewResourceDescriptor<PopupType> { }

    [System.Serializable]
    public class PanelResourceDescriptor : ViewResourceDescriptor<PanelType> { }
}
