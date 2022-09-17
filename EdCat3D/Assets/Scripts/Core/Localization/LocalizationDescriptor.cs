using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Localization
{
    public enum LanguageType
    {
        EN,
        RU
    }

    [CreateAssetMenu(fileName = "LanguageDescriptor", menuName ="Create/Language Descriptor")]
    public class LocalizationDescriptor : ScriptableObject
    {
        [SerializeField] private List<LanguageWordData> _languageWordDatas = new List<LanguageWordData>();

        private Dictionary<LanguageType, LanguageWordData> _languageDatas = new Dictionary<LanguageType, LanguageWordData>();

        private void OnEnable()
        {
            PrepareLanguagesPhrase();
        }

        private void PrepareLanguagesPhrase()
        {
            foreach (var languageWordData in _languageWordDatas)
            {
                languageWordData.PrepareWords();
                _languageDatas[languageWordData.Language] = languageWordData;
            }
        }

        public string GetPhrase(LanguageType language, string key)
        {
            return _languageDatas[language].GetPhrase(key);
        }
        
    }


    [System.Serializable]
    public class KeyWordData
    {
        [SerializeField] private string _key;
        [TextArea(3, 10)]
        [SerializeField] private string _phrase;

        public string Key => _key;
        public string Phrase => _phrase;
    }

    [System.Serializable]
    public class LanguageWordData
    {
        [SerializeField] private LanguageType _language;
        [SerializeField] private List<KeyWordData> _keyWordDatas;

        public LanguageType Language => _language;

        private Dictionary<string, string> _words = new Dictionary<string, string>();

        public LanguageWordData()
        {
            PrepareWords();
        }

        public string GetPhrase(string key)
        {
            if (_words.ContainsKey(key))
            {
                return _words[key];
            }
            else
            {
                Debug.LogError($"Cannot find phrase with key {key}");
                return null;
            }
        }

        public void PrepareWords()
        {
            if (_keyWordDatas == null)
                return;
            foreach (var keyWordData in _keyWordDatas)
            {
                _words[keyWordData.Key] = keyWordData.Phrase;
            }
        }
    }
}