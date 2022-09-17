using UnityEngine;
using System.Collections;

[System.Serializable]
public partial class s_NPCHoverToon : MonoBehaviour
{
    /*****************************************************************************************
* Hover toon effect for NPCs
* 
* @author N-Game Studios
* Research & Development	
* 
*****************************************************************************************/
    //private var someShader:Shader;
    public bool isEnteredJustNow;
    public virtual void Start()//someShader = this.gameObject.GetComponent(SkinnedMeshRenderer).material.shader;
    {
        this.isEnteredJustNow = false;
    }

    //function OnMouseEnter()
    public virtual void mouseEnterOnIt()
    {
        if (this.isEnteredJustNow)
        {
            return;
        }
        this.isEnteredJustNow = true;
        //Outlined/Diffuse
        /*this.gameObject.GetComponent(SkinnedMeshRenderer).material.shader = Shader.Find("Outlined/Diffuse"); //Toon/Basic Outline"); // Outlined/Silhouetted Diffuse
	this.gameObject.GetComponent(SkinnedMeshRenderer).material.SetColor("_OutlineColor", Color.red);
	this.gameObject.GetComponent(SkinnedMeshRenderer).material.SetFloat("_Outline", 0.005); //0.002*/
        s_NPCController someCurrentNPCScript = (s_NPCController) this.gameObject.transform.parent.GetComponent(typeof(s_NPCController));
        if (!Global._isGamePaused)
        {
            Global.globalBus.SendMessage("c_GuiCommonController_command_showNPCName", new object[] {this.gameObject, someCurrentNPCScript.npcname});
        }
    }

    //function OnMouseExit()
    public virtual void mouseExitOnIt()
    {
        if (!this.isEnteredJustNow)
        {
            return;
        }
        this.isEnteredJustNow = false;
        if (this != null)
        {
             //this.gameObject.GetComponent(SkinnedMeshRenderer).material.shader = someShader;
             //Global.globalBus.SendMessage("c_GuiCommonController_command_hideNpcBar");
            Global.isNpcNameShow = false;
        }
    }

    //function OnMouseDown()
    public virtual void mouseDownOnIt()
    {
    }

}