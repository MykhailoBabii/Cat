using UnityEngine;
using System.Collections;

[System.Serializable]
public partial class s_TempleExit : MonoBehaviour
{
    public virtual void OnTriggerEnter(Collider other)
    {
        Global.TempleExitDoor = true;
    }

    public virtual void OnTriggerExit(Collider other)
    {
        Global.TempleExitDoor = false;
    }

}