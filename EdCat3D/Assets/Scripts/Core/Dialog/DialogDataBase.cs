using Core.CES;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core.Dialog
{
    public enum DialogType
    {
        FirstGrayDialog,
        SecondGrayDialog
    }

    [CreateAssetMenu(fileName ="DialogsDB", menuName = "Create/Dialogs DB")]
    public class DialogDataBase:ScriptableObject
    {
        [SerializeField] private List<DialogDescriptor> _dialogs = new List<DialogDescriptor>();

        public DialogDescriptor GetDialog(string dialogId)
        {
            var result = _dialogs.Find(dialog => dialog.DialogId == dialogId);
            return result;
        }
    }
}