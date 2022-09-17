using UnityEngine;
using System.Collections;

[System.Serializable]
public partial class s_UFOEntry : MonoBehaviour
{
    public virtual void OnTriggerEnter(Collider other)
    {
        Global.UFOOpenDoor = true;
        Global._isStorageFaraway = false;
    }

    public virtual void OnTriggerExit(Collider other)
    {
        Global.UFOOpenDoor = false;
        Global._isStorageFaraway = true;
    }

}