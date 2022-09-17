using UnityEngine;
using System.Collections;

[System.Serializable]
public partial class s_DropHoverToon : MonoBehaviour
{
    private Shader someShader;
    public bool isEnteredJustNow;
    public virtual void Start()//someShader = this.gameObject.GetComponent(MeshRenderer).material.shader;
    {
        this.isEnteredJustNow = false;
    }

    //function OnMouseEnter()
    public virtual void mouseEnterOnIt()//	));
    {
        Global._GAME_CURSOR = "take";
        if (this.isEnteredJustNow)
        {
            return;
        }
        this.isEnteredJustNow = true;
    }

    //function OnMouseExit()
    public virtual void mouseExitOnIt()//if (this != null) Global._GAME_CURSOR = "default";//this.gameObject.GetComponent(MeshRenderer).material.shader = someShader;
    {
        Global._GAME_CURSOR = "default";
        if (!this.isEnteredJustNow)
        {
            return;
        }
        this.isEnteredJustNow = false;
    }

    //function OnMouseDown()
    public virtual void mouseDownOnIt() //Global._GAME_CURSOR = "default";
    {
    }

}