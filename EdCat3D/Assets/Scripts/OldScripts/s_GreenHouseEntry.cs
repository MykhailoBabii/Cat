using UnityEngine;
using System.Collections;

[System.Serializable]
public partial class s_GreenHouseEntry : MonoBehaviour
{
    public virtual void OnTriggerEnter(Collider other)
    {
        Global.GreenHouseOpenDoor = true;
    }

    public virtual void OnTriggerExit(Collider other)
    {
        Global.GreenHouseOpenDoor = false;
    }

}