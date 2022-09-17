using UnityEngine;
using System.Collections;

[System.Serializable]
public partial class s_MosaicEntry : MonoBehaviour
{
    public virtual void OnTriggerEnter(Collider other)
    {
        if (!Global.MosaicComplete)
        {
            Global.MosaicAvailable = true;
        }
    }

    public virtual void OnTriggerExit(Collider other)
    {
        if (!Global.MosaicComplete)
        {
            Global.MosaicAvailable = false;
        }
    }

}