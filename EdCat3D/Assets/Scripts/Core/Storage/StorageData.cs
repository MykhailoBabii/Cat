using Core.Dialog;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core.Storage
{
    public interface IStoragable
    {
        void Save();
        void Load();
    }


    public interface IStoragable<TData>
    {
        void Save(TData data);
        void Load(TData data);
    }


    [System.Serializable]
    public class StorageData
    {
        public LevelProgressData ProgressData;
        public int Coins;

        public StoreStorageData StoreData;
    }

    [System.Serializable]
    public class StoreStorageData
    {
        //Что куплено
        //Текущий выбранный элемент магазина
    }

    [System.Serializable]
    public class LevelProgressData
    {
        public bool IsFirstIntroShowed;
        public bool IsSecondIntroShowed;
        public bool IsFirstDialogShowed;
    }

    public class DialogProgressData
    {
        public DialogType Type;
        public string DialogId;
        public bool Showed;
    }


}