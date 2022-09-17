using UnityEngine;
using System.Collections;

[System.Serializable]
public partial class s_NPCController : MonoBehaviour
{
    /*****************************************************************************************
* AI Controller for neutral NPC
* 
* @author N-Game Studios Ltd. 
* Research & Development	
* 
*****************************************************************************************/
    private CharacterController controller;
    public string npcwho;
    public string npcname;
    public bool creatureCanSpeak;
    public string uniqueID;
    public int dlgCount;
    public string[] dlgTitle;
    public bool[] dlgIsActive;
    public string dlgTitleCurrent;
    private float dlg_reactionradius;
    private float desiredDistance;
    private float automaticMashineIntervalMin; // in seconds
    private float automaticMashineIntervalMax; // in seconds
    private bool isAutomaticMode;
    private float lastAutomaticMashineTime;
    private float automaticMashineIntervalReal; // in seconds
    private float lastTransformPointWhenCollidedTime;
    private Vector3 lastTransformPointWhenCollided;
    public virtual void Start()
    {
        this.controller = (CharacterController) this.GetComponent(typeof(CharacterController));
        if (this.controller == null)
        {
            Debug.Log("AI Controller needs CharacterController script attached!!");
        }
        this.lastTransformPointWhenCollidedTime = Time.time;
    }

    public virtual void s_NPCController_command_BeginBeAutomaticBoy()
    {
        this.automaticMashineIntervalReal = Random.Range(this.automaticMashineIntervalMin, this.automaticMashineIntervalMax);
        this.isAutomaticMode = true;
        this.lastAutomaticMashineTime = Time.time;
    }

    public virtual void s_NPCController_command_StopBeAutomaticBoy()
    {
        this.isAutomaticMode = false;
        this.lastAutomaticMashineTime = Time.time;
    }

    private void NPCStateMashine()
    {
        if (((Global._hero_dolly != null) && !Global._system_isHeroDead) && (Vector3.Distance(Global._hero_dolly.transform.position, this.transform.position) < this.dlg_reactionradius))
        {
            if (!Global._besideNPCList.Contains(this.gameObject))
            {
                Global._besideNPCList.Add(this.gameObject);
            }
            if (this.creatureCanSpeak)
            {
                this.s_NPCController_command_stop();
            }
            else
            {
                this.s_NPCController_command_stop();
            }
        }
        else
        {
            Global._besideNPCList.Remove(this.gameObject);
            this.s_NPCController_command_stop();
        }
        this.automaticMashineIntervalReal = Random.Range(this.automaticMashineIntervalMin, this.automaticMashineIntervalMax);
        this.lastAutomaticMashineTime = Time.time;
    }

    public virtual void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (!Global._system_isHeroDead)
        {
            if ((Time.time - this.lastTransformPointWhenCollidedTime) > 0.5f)
            {
                if ((Mathf.Abs(this.lastTransformPointWhenCollided.x - this.gameObject.transform.position.x) < 0.05f) && (Mathf.Abs(this.lastTransformPointWhenCollided.z - this.gameObject.transform.position.z) < 0.05f))
                {
                    GameObject transformCopy = new GameObject();
                    GameObject.Destroy(transformCopy);
                }
                this.lastTransformPointWhenCollided = this.gameObject.transform.position;
                this.lastTransformPointWhenCollidedTime = Time.time;
            }
        }
    }

    public virtual void Update()
    {
        if ((Time.time - this.lastAutomaticMashineTime) > this.automaticMashineIntervalReal)
        {
            this.NPCStateMashine();
            this.lastAutomaticMashineTime = Time.time;
        }
        this.s_NPCController_command_PushBackward();
    }

    public virtual void s_NPCController_command_follow(Transform target) //nothing
    {
    }

    private Transform _PushBackwardTargetTransform;
    private bool _isPushBackward;
    private float _PushBackwardVelocity;
    public virtual void s_NPCController_command_StartPushBackward(Transform target) //nothing
    {
    }

    public virtual void s_NPCController_command_PushBackward() //nothing
    {
    }

    public virtual void s_NPCController_command_stop() //nothing
    {
    }

    public virtual void OnExternalVelocity() // todo : event from CharacterMotor when "SetVelocity" call
    {
    }

    public s_NPCController()
    {
        this.npcwho = "";
        this.npcname = "";
        this.creatureCanSpeak = true;
        this.uniqueID = "";
        this.dlg_reactionradius = 5f;
        this.desiredDistance = 0.1f;
        this.automaticMashineIntervalMin = 0.2f;
        this.automaticMashineIntervalMax = 0.5f;
        this.lastTransformPointWhenCollided = Vector3.zero;
        this._PushBackwardVelocity = 2;
    }

}