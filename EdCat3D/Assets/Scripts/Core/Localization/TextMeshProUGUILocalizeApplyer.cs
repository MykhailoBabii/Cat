using Core.DISimple;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
namespace Localization
{

    [RequireComponent(typeof(TextMeshProUGUI))]
    public class TextMeshProUGUILocalizeApplyer : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _text;
        [SerializeField] private string _key;

        void Start()
        {
            var di = ServiceLocator.Resolve<Core.DI.DiContainer>();
            var localizationService = di.GetService<ILocalizationService>();
            _text.text = localizationService.GetPhrase(_key);
        }

    }
}