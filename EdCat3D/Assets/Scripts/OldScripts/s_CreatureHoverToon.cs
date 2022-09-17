using UnityEngine;
using System.Collections;

[System.Serializable]
public partial class s_CreatureHoverToon : MonoBehaviour
{
    /*****************************************************************************************
* Hover toon effect for creatures
* 
* @author N-Game Studios
* Research & Development	
* 
*****************************************************************************************/
    public bool isEnteredJustNow;
    public virtual void Start()
    {
        this.isEnteredJustNow = false;
    }

    public virtual void mouseEnterOnIt()
    {
        if (!Global._isGamePaused)
        {
            Global._GAME_CURSOR = "attack";
        }
        if (this.isEnteredJustNow)
        {
            return;
        }
        this.isEnteredJustNow = true;
        s_AIController someCurrentAiScript = (s_AIController) this.gameObject.GetComponent(typeof(s_AIController));
        if (!Global._isGamePaused)
        {
            Global.globalBus.SendMessage("c_GuiCommonController_command_showNpcBar", new object[] {this.gameObject, someCurrentAiScript.npclevel, someCurrentAiScript.npcname, someCurrentAiScript.npclife, someCurrentAiScript.npclifemax, someCurrentAiScript.npcwho});
        }
    }

    public virtual void mouseExitOnIt()
    {
        Global._GAME_CURSOR = "default";
        if (!this.isEnteredJustNow)
        {
            return;
        }
        this.isEnteredJustNow = false;
        if (this != null)
        {
            Global.globalBus.SendMessage("c_GuiCommonController_command_hideNpcBar");
        }
    }

    public virtual void mouseDownOnIt()
    {
    }

}