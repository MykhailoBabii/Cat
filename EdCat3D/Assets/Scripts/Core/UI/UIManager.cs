using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core.UI
{
    public class UIManager : MonoBehaviour
    {
        private static UIManager _instanceInner;

        private static UIManager _instance
        {
            get
            {
                if (_instanceInner == null)
                {
                    var go = new GameObject("UIManager");//nameof(UIManager));
                    _instanceInner = go.AddComponent<UIManager>();
                    DontDestroyOnLoad(go);
                }

                return _instanceInner;
            }
        }

        private Dictionary<System.Type, BaseView> _allViews = new Dictionary<System.Type, BaseView>();

        private void Awake()
        {
            if (_instanceInner == null)
            {
                _instanceInner = this;
                DontDestroyOnLoad(gameObject);
            }
        }

        public static void RegisterView<TView>(TView view) where TView: BaseView
        {
            if (_instance._allViews.ContainsKey(typeof(TView)) == true)
            {
                Debug.LogError(string.Format("View {0} already exist", typeof(TView).Name));
            }
            else
            {
                _instance._allViews[typeof(TView)] = view;
            }
        }

        public static void UnregisterView<TView>() where TView:BaseView
        {
            if (_instance._allViews.ContainsKey(typeof(TView)) == false)
            {
                Debug.LogError(string.Format("View {0} did not exist", typeof(TView).Name));
            }
            else
            {
                _instance._allViews.Remove(typeof(TView));
            }
        }

        public static TView GetView<TView>() where TView : BaseView
        {
            if (_instance._allViews.ContainsKey(typeof(TView)) == false)
            {
                Debug.LogError(string.Format("View {0} did not exist", typeof(TView).Name));
                return null;
            }
            else
            {
               return (TView) _instance._allViews[typeof(TView)];
            }
        }
    }
}