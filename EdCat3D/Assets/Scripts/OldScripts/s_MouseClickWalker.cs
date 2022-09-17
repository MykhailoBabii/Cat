using UnityEngine;
using System.Collections;

[System.Serializable]
public partial class s_MouseClickWalker : MonoBehaviour
{
    /*****************************************************************************************
* Top-down movement system
* 
* @author N-Game Studios Ltd. 
* Research & Development	
* 
*****************************************************************************************/
    public Vector3 dir;
    public float speed;
    public float gravity;
    public UnityEngine.AI.NavMeshAgent _agent;
    public UnityEngine.AI.NavMeshHit hit;
    private Vector3 gravityPower;
    private CharacterController yourCharacterController;
    private Vector3 lastTransformPointWhenCollided;
    private float lastTransformPointWhenCollidedTime;
    private bool isReadyToUse;
    private bool isPlayS;
    private bool isPlayRunAnimation;
    public virtual IEnumerator Start()
    {
        while (((s_Character) this.gameObject.GetComponent(typeof(s_Character))).character == null)
        {
            yield return null;
        }
        this.yourCharacterController = (CharacterController) this.GetComponent(typeof(CharacterController));
        this.gravityPower.y = this.gravity;
        this.lastTransformPointWhenCollidedTime = Time.time;
        this.isReadyToUse = true;
    }

    public virtual void FixedUpdate()
    {
        if (Global._dlgRun)
        {
            this.isReadyToUse = false;
            Global._isClickDlg = false;
            if (Global.soundBegun != null)
            {
                Global._stopSoundBegun();
            }
            if (((s_Character) this.GetComponent(typeof(s_Character))).character != null)
            {
                ((Animator) ((s_Character) this.GetComponent(typeof(s_Character))).character.GetComponent(typeof(Animator))).SetTrigger("playerIdle");
            }
        }
        else
        {
            this.isReadyToUse = true;
            Global._isClickDlg = true;
            Global._isUnderAttack = false;
        }
        if (!this.isReadyToUse)
        {
            return;
        }
        if ((((this.gameObject.transform.position.x != Global._heroTargetPoint.x) && (this.gameObject.transform.position.z != Global._heroTargetPoint.z)) && Global._isBegun) && !Global._isUnderAttack)
        {
            if ((((s_Character) this.GetComponent(typeof(s_Character))).character != null) && Global._isMoveAllowed)
            {
                ((Animator) ((s_Character) this.GetComponent(typeof(s_Character))).character.GetComponent(typeof(Animator))).ResetTrigger("playerHit");
                ((Animator) ((s_Character) this.GetComponent(typeof(s_Character))).character.GetComponent(typeof(Animator))).SetTrigger("playerRun");
            }
            if (Input.GetMouseButtonUp(0))
            {
                this.isPlayRunAnimation = true;
            }
            this.StartCoroutine(this.Stopper(this.gameObject.transform.position));
            if (!this.isPlayS)
            {
                this.isPlayS = true;
            }
            if (!Global._dlgRun)
            {
                this.dir = Global._heroTargetPoint - this.transform.position;
                if ((Global._isRemoteMeleeAttack || Global._isUnderAttack) && (Global._isRemoteMeleeAttackNPC != null))
                {
                    this.transform.LookAt(new Vector3(Global._isRemoteMeleeAttackNPC.transform.position.x, this.transform.position.y, Global._isRemoteMeleeAttackNPC.transform.position.z));
                }
                else
                {
                    this.transform.LookAt(new Vector3(Global._heroTargetPoint.x, this.transform.position.y, Global._heroTargetPoint.z));
                }
                this._agent.SetDestination(Global._heroTargetPoint);
                if (this._agent.velocity.sqrMagnitude > Mathf.Epsilon)
                {
                    this.transform.rotation = Quaternion.LookRotation(this._agent.velocity.normalized);
                }
            }
        }
        else
        {
            this.gameObject.transform.Rotate(0f, 0f, 0f, Space.World);
            if (Global.soundBegun != null)
            {
                if (Global.soundBegun != null)
                {
                    Global._stopSoundBegun();
                }
            }
            if (!Global._isPopUpOpen)
            {
                Global._isClickExit = true;
            }
            if (((s_Character) this.GetComponent(typeof(s_Character))).character != null)
            {
                if (!Global._isBattleRagesOn)
                {
                    ((Animator) ((s_Character) this.GetComponent(typeof(s_Character))).character.GetComponent(typeof(Animator))).ResetTrigger("playerRun");
                    ((Animator) ((s_Character) this.GetComponent(typeof(s_Character))).character.GetComponent(typeof(Animator))).ResetTrigger("playerAttack2");
                    ((Animator) ((s_Character) this.GetComponent(typeof(s_Character))).character.GetComponent(typeof(Animator))).SetTrigger("playerIdle");
                }
                else
                {
                    ((Animator) ((s_Character) this.GetComponent(typeof(s_Character))).character.GetComponent(typeof(Animator))).ResetTrigger("playerIdle");
                    ((Animator) ((s_Character) this.GetComponent(typeof(s_Character))).character.GetComponent(typeof(Animator))).ResetTrigger("playerRun");
                }
            }
            this.isPlayS = false;
            this.isPlayRunAnimation = true;
        }
    }

    public virtual IEnumerator Stopper(Vector3 position)
    {
        yield return new WaitForSeconds(0.1f);
        if (this.gameObject.transform.position == position)
        {
            Global._heroTargetPoint = this.gameObject.transform.position;
            ((Animator) ((s_Character) this.GetComponent(typeof(s_Character))).character.GetComponent(typeof(Animator))).SetTrigger("playerIdle");
        }
    }

    public virtual void PlayRunAnimation()
    {
        ((Animator) ((s_Character) this.GetComponent(typeof(s_Character))).character.GetComponent(typeof(Animator))).SetTrigger("playerRun");
    }

    public virtual void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if ((Time.time - this.lastTransformPointWhenCollidedTime) > 0.2f)
        {
            if ((this.gameObject.transform.position.x == Global._heroTargetPoint.x) && (this.gameObject.transform.position.z == Global._heroTargetPoint.z))
            {
                Global._heroTargetPoint = this.gameObject.transform.position;
            }
            this.lastTransformPointWhenCollided = this.gameObject.transform.position;
            this.lastTransformPointWhenCollidedTime = Time.time;
        }
    }

    public s_MouseClickWalker()
    {
        this.speed = 10f;
        this.gravity = -20f;
        this.gravityPower = Vector3.zero;
        this.isPlayRunAnimation = true;
    }

}