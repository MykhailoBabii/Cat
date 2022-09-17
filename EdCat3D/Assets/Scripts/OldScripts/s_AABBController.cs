using UnityEngine;
using System.Collections;

[System.Serializable]
public partial class s_AABBController : MonoBehaviour
{
    public virtual void Start()
    {
        ((MeshRenderer) this.GetComponent(typeof(MeshRenderer))).enabled = false;
    }

}