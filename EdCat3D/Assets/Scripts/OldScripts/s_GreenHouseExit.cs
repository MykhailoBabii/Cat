using UnityEngine;
using System.Collections;

[System.Serializable]
public partial class s_GreenHouseExit : MonoBehaviour
{
    public virtual void OnTriggerEnter(Collider other)
    {
        Global.GreenHouseExitDoor = true;
    }

    public virtual void OnTriggerExit(Collider other)
    {
        Global.GreenHouseExitDoor = false;
    }

}