using Core.CES;
using Core.UI;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core.Dialog
{

    [CreateAssetMenu(fileName = "DialogDescriptor_", menuName ="Create/Dialog")]
    //[Sirenix.OdinInspector.ExtensionOfNativeClass]
    public class DialogDescriptor: SerializedScriptableObject
    {
        [SerializeField] private string _dialogId;
        //[SerializeField] private List<PhraseDescriptor> _phrasesDescriptors = new List<PhraseDescriptor>();

        public string DialogId => _dialogId;
        [ShowInInspector]
        public List<PhraseDescriptor> PhrasesDescriptor;
    }
}