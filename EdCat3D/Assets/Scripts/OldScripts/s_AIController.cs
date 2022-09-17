using UnityEngine;
using System.Collections;

[System.Serializable]
public partial class s_AIController : MonoBehaviour
{
    /*****************************************************************************************
* AI Controller for enemies
* 
* @author N-Game Studios Ltd. 
* Research & Development	
* 
*****************************************************************************************/
    private CharacterMotor motor;
    private CharacterController controller;
    public GameObject character;
    // ==================================
    public UnityEngine.AI.NavMeshAgent _agent;
    public float srv_attackrange;
    public int srv_damagemin;
    public int srv_damagemax;
    public int npclevel;
    public int srv_defence;
    public int npclife;
    public int npclifemax;
    public float srv_attackspeed;
    public float srv_aspeed;
    public float srv_reactionradius;
    public float srv_crit;
    public string srv_npctype;
    public string npcwho;
    public int npcexp;
    public string uniqueID;
    private float _npc_sliceAddControlRandom;
    private int _npc_sliceAddControlRandom_hitavoidance;
    private float _npc_critAddControlRandom;
    private int _npc_critAddControlRandom_hitavoidance;
    private bool soundRun;
    // ==================================
    public string npcname;
    public bool isNpcDead;
    public bool creatureCanAttack;
    private float creatureCanAttackTime;
    // Follow settings
    public float desiredDistance;
    public float automaticMashineIntervalMin; // in seconds
    public float automaticMashineIntervalMax; // in seconds
    private bool isAutomaticMode;
    private float lastAutomaticMashineTime;
    private float automaticMashineIntervalReal; // in seconds
    private float lastTransformPointWhenCollidedTime;
    private Vector3 lastTransformPointWhenCollided;
    private bool isAttackPosition;
    private bool isFindOutOtherWay;
    private float findOutOtherWayTime;
    private GameObject _aiCurrentTarget;
    private bool _isRealyAiCurrentTarget;
    private bool _LetItBleed;
    private bool _isAddExp;
    public float idleTime;
    public float attackTime;
    public float hitTime;
    public float deathTime;
    private Animator anim;
    private AnimationClip clip;
    public GameObject enemyBatExplosion;
    public GameObject enemyBatShot;
    public GameObject normalModel;
    public GameObject paraliticModel;
    public GameObject enemySelect;
    private bool StopAnim;
    public string lootType;
    public int lootAmount;
    private int randomAttack;
    public virtual void Start()
    {
        if (this.normalModel != null)
        {
            this.normalModel.SetActive(true);
        }
        if (this.paraliticModel != null)
        {
            this.paraliticModel.SetActive(false);
        }
        this.anim = (Animator) this.character.GetComponent(typeof(Animator));
        this.motor = (CharacterMotor) this.GetComponent(typeof(CharacterMotor));
        this.controller = (CharacterController) this.GetComponent(typeof(CharacterController));
        this.lastTransformPointWhenCollidedTime = Time.time;
        this.creatureCanAttackTime = Time.time;
        this.findOutOtherWayTime = Time.time;
        this.s_AIController_UpdateAnimClipTimes();
        foreach (DictionaryEntry oneNpcToon in Global._npcsToonsScripts)
        {
            if (oneNpcToon.Key.ToString() != this.name)
            {
                Global._npcsToonsScripts.Add(this.name, (s_CreatureHoverToon) this.transform.Find("character").GetComponent(typeof(s_CreatureHoverToon)));
            }
        }
    }

    public virtual void s_AIController_UpdateAnimClipTimes()
    {
        AnimationClip[] clips = this.anim.runtimeAnimatorController.animationClips;
        foreach (AnimationClip clip in clips)
        {
            switch (clip.name)
            {
                case "attack_1":
                    this.attackTime = clip.length;
                    break;
            }
        }
    }

    public virtual void s_AIController_command_BeginBeAutomaticBoy()
    {
        this.automaticMashineIntervalReal = Random.Range(this.automaticMashineIntervalMin, this.automaticMashineIntervalMax);
        this.isAutomaticMode = true;
        this.lastAutomaticMashineTime = Time.time;
    }

    public virtual void s_AIController_command_StopBeAutomaticBoy()
    {
        this.isAutomaticMode = false;
        this.lastAutomaticMashineTime = Time.time;
    }

    private IEnumerator aiStateMashine()
    {
        if (((Global._hero_dolly != null) && !Global._system_isHeroDead) && (Vector3.Distance(Global._hero_dolly.transform.position, this.transform.position) < this.srv_reactionradius))
        {
            if (!Global._besideMobsList.Contains(this.gameObject))
            {
                Global._besideMobsList.Add(this.gameObject);
            }
            else
            {
                if (Vector3.Distance(Global._hero_dolly.transform.position, this.transform.position) > this.desiredDistance)
                {
                    this.isAttackPosition = false;
                    ((Animator) this.character.GetComponent(typeof(Animator))).ResetTrigger("mobIdle");
                    this.s_AIController_command_follow(Global._hero_dolly.transform);
                }
                else
                {
                    if (this.creatureCanAttack)
                    {
                        this.randomAttack = Random.Range(1, 4);
                        this.isAttackPosition = true;
                        this.creatureCanAttack = false;
                        this.creatureCanAttackTime = Time.time;
                        this._agent.SetDestination(this.transform.position);
                        if (this._agent.velocity.sqrMagnitude > Mathf.Epsilon)
                        {
                            this.transform.rotation = Quaternion.LookRotation(this._agent.velocity.normalized);
                        }
                        this.s_AIController_command_stop();
                        this.gameObject.transform.LookAt(new Vector3(Global._hero_dolly.transform.position.x, this.gameObject.transform.position.y, Global._hero_dolly.transform.position.z));
                        if (this.npcwho == "npctestmonster")
                        {
                            if (this.randomAttack == 1)
                            {
                                ((Animator) this.character.GetComponent(typeof(Animator))).SetTrigger("mobAttack");
                            }
                            else
                            {
                                if (this.randomAttack == 2)
                                {
                                    ((Animator) this.character.GetComponent(typeof(Animator))).SetTrigger("mobAttack2");
                                }
                                else
                                {
                                    if (this.randomAttack == 3)
                                    {
                                        ((Animator) this.character.GetComponent(typeof(Animator))).SetTrigger("mobAttack3");
                                    }
                                }
                            }
                        }
                        else
                        {
                            ((Animator) this.character.GetComponent(typeof(Animator))).SetTrigger("mobAttack");
                        }
                        this.StartCoroutine(this.s_AIController_command_makeDamageForNpcHero());
                        yield return new WaitForSeconds(this.attackTime / 3);
                        if (!Global._isAttackMob)
                        {
                            ((Animator) ((s_Character) Global._hero_dolly.GetComponent(typeof(s_Character))).character.GetComponent(typeof(Animator))).SetTrigger("playerHit");
                        }
                        yield return new WaitForSeconds(this.attackTime / 2);
                    }
                    else
                    {
                    }
                }
            }
             //nothing
            this._isRealyAiCurrentTarget = false;
        }
        else
        {
            Global._besideMobsList.Remove(this.gameObject);
            this._agent.SetDestination(this.transform.position);
            if (this._agent.velocity.sqrMagnitude > Mathf.Epsilon)
            {
                this.transform.rotation = Quaternion.LookRotation(this._agent.velocity.normalized);
            }
            this.s_AIController_command_stop();
        }
        this.automaticMashineIntervalReal = Random.Range(this.automaticMashineIntervalMin, this.automaticMashineIntervalMax);
        this.lastAutomaticMashineTime = Time.time;
    }

    public virtual void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (!Global._system_isHeroDead)
        {
            if (((((Time.time - this.lastTransformPointWhenCollidedTime) > 0.5f) && !this.isAttackPosition) && !this.isFindOutOtherWay) && !this.isNpcDead)
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
        if (((Time.time - this.lastAutomaticMashineTime) > this.automaticMashineIntervalReal) && !this.isFindOutOtherWay)
        {
            this.StartCoroutine(this.aiStateMashine());
            this.lastAutomaticMashineTime = Time.time;
        }
        if (((Time.time - this.findOutOtherWayTime) > 0.5f) && this.isFindOutOtherWay)
        {
            this.isFindOutOtherWay = false;
            this.findOutOtherWayTime = Time.time;
        }
        if (((Time.time - this.creatureCanAttackTime) > 0.7f) && !this.isFindOutOtherWay)
        {
            this.creatureCanAttack = true;
            this.creatureCanAttackTime = Time.time;
        }
        if (this.npclife <= 0)
        {
            ((Animator) ((s_Character) Global._hero_dolly.GetComponent(typeof(s_Character))).character.GetComponent(typeof(Animator))).SetTrigger("playerEndBattle");
            this.StartCoroutine(this.s_AIController_command_getCreatureToDeath());
        }
        this.s_AIController_command_PushBackward();
    }

    public virtual void s_AIController_command_makeDamageForCurrentTarget()
    {
    }

    public virtual IEnumerator s_AIController_command_makeDamageForNpcHero()
    {
        if ((Global._hero_dolly != null) && !Global._system_isHeroDead)
        {
            GameObject spellFlyGO = null;
            GameObject spellBumGO = null;
            Vector3 oldHeroPos = new Vector3(Global._hero_dolly.transform.position.x, Global._hero_dolly.transform.position.y, Global._hero_dolly.transform.position.z);
            if (this.npcwho == "npctestmonster")
            {
                yield return new WaitForSeconds(0.49f);
            }
            if (this.npcwho == "npcbat")
            {
                yield return new WaitForSeconds(0.49f);
                spellFlyGO = GameObject.Instantiate(this.enemyBatShot, new Vector3(this.gameObject.transform.position.x, this.gameObject.transform.position.y + 1, this.gameObject.transform.position.z), Quaternion.identity);
                ((ParticleSystem) spellFlyGO.GetComponent(typeof(ParticleSystem))).Play(true);
                Global.globalBus.SendMessage("c_DestroyController_command_ADD_to_GROUP", new object[] {"newbattleeffects", spellFlyGO});
                iTween.MoveTo(spellFlyGO, new Hashtable() { {"x", oldHeroPos.x },  {"y", oldHeroPos.y + 1 },  {"z", oldHeroPos.z },  {"time", 0.4f },  {"delay", 0 },  {"easetype", "easeOutQuad" }, });
                yield return new WaitForSeconds(0.42f);
                iTween.MoveTo(spellFlyGO, new Hashtable() { {"x", oldHeroPos.x },  {"y", oldHeroPos.y - 30 },  {"z", oldHeroPos.z },  {"time", 0.1f },  {"delay", 0 },  {"easetype", "linear" }, });
                spellBumGO = GameObject.Instantiate(this.enemyBatExplosion, new Vector3(oldHeroPos.x, oldHeroPos.y + 1, oldHeroPos.z), Quaternion.identity);
                ((ParticleSystem) spellBumGO.GetComponent(typeof(ParticleSystem))).Play(true);
                Global.globalBus.SendMessage("c_DestroyController_command_ADD_to_GROUP", new object[] {"newbattleeffects", spellBumGO});
            }
            float explosionDistance = Vector3.Distance(oldHeroPos, Global._hero_dolly.transform.position);
            if (explosionDistance <= 2)
            {
                int damage = Mathf.FloorToInt(Random.Range(this.srv_damagemin, this.srv_damagemax) * Global._difficultAttackCoeff);
                // apply damage substract (depends on user's defence)
                damage = damage - System.Convert.ToInt32((damage * (System.Convert.ToSingle(Global._system_CharacterParams["defence"]) * 0.1f)) / 100f);
                // apply damage slice (depends on npc's defence)
                if (this._npc_sliceAddControlRandom_hitavoidance > 0)
                {
                    this._npc_sliceAddControlRandom_hitavoidance--;
                }
                else
                {
                    float sliceChance = (System.Convert.ToSingle(Global._system_CharacterParams["defence"]) * 0.002375f) + this._npc_sliceAddControlRandom;
                    if (Random.Range(0, 100) < sliceChance)
                    {
                        damage = Mathf.FloorToInt(damage / 2f);
                        this._npc_sliceAddControlRandom_hitavoidance = Mathf.FloorToInt(((100f - this._npc_sliceAddControlRandom) * 1f) / sliceChance);
                        this._npc_sliceAddControlRandom = 0;
                    }
                    else
                    {
                        this._npc_sliceAddControlRandom = this._npc_sliceAddControlRandom + sliceChance;
                    }
                }
                // apply damage crit (depends on npc's crit chance)
                bool wasCritical = false;
                if (this._npc_critAddControlRandom_hitavoidance > 0)
                {
                    this._npc_critAddControlRandom_hitavoidance--;
                }
                else
                {
                    if (Random.Range(0, 100) < (this.srv_crit + this._npc_critAddControlRandom))
                    {
                        wasCritical = true;
                        damage = Mathf.FloorToInt(damage * 2f);
                        this._npc_critAddControlRandom_hitavoidance = Mathf.FloorToInt(((100f - this._npc_critAddControlRandom) * 1f) / this.srv_crit);
                        this._npc_critAddControlRandom = 0;
                    }
                    else
                    {
                        this._npc_critAddControlRandom = this._npc_critAddControlRandom + this.srv_crit;
                    }
                }
                Global._userCurrentHealth = Global._userCurrentHealth - damage;
                if ((((s_Character) Global._hero_dolly.GetComponent(typeof(s_Character))).character != null) && !Global._isAttackMob)
                {
                }
                 //nothing
                //Comment for Screens
                GameObject someGoFlying = new GameObject();
                ((s_FlyDigitScript) someGoFlying.AddComponent(typeof(s_FlyDigitScript))).goFlyingDigit(-1 * damage, new Vector3(Global._hero_dolly.transform.position.x, Global._hero_dolly.transform.position.y + 1.5f, Global._hero_dolly.transform.position.z), wasCritical);
                Global.globalBus.SendMessage("c_GameController_Battle_command_unsetComboPoint");
                if (Global._userCurrentHealth <= 0)
                {
                    Global._userCurrentHealth = 0;
                    Global._system_isHeroDead = true;
                    Global._system_isDead = true;
                    Global._hero_dolly.GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = false;
                    if (((s_Character) Global._hero_dolly.GetComponent(typeof(s_Character))).character != null)
                    {
                        ((Animator) ((s_Character) Global._hero_dolly.GetComponent(typeof(s_Character))).character.GetComponent(typeof(Animator))).SetTrigger("playerDeath");
                    }
                    yield return new WaitForSeconds(2);
                    if (Global.soundBegun != null)
                    {
                        Global._stopSoundBegun();
                    }
                    Global.globalBus.SendMessage("c_GameController_Base_command_setPause");
                    Global._gui_AddInterface("DeathScreen");
                }
            }
            if (this.npcwho == "npcbat")
            {
                yield return new WaitForSeconds(2);
                if (spellFlyGO != null)
                {
                    GameObject.Destroy(spellFlyGO);
                }
                yield return new WaitForSeconds(0.6f);
                if (spellBumGO != null)
                {
                    GameObject.Destroy(spellBumGO);
                }
            }
        }
    }

    public virtual IEnumerator s_AIController_command_makeDamageToCreature(int damage, bool needAddComboPoint)
    {
        int TempCrit = 0;
        if (!Global._isUnderAttack)
        {
            ((Animator) this.character.GetComponent(typeof(Animator))).SetTrigger("mobHit"); //charAnimation.setHitAnimation();
        }
        damage = damage + Mathf.FloorToInt((damage * Global.growDamage) / 100f);
        damage = damage - Mathf.FloorToInt((damage * (this.srv_defence * 0.00475f)) / 100f);
        // apply damage slice (depends on npc's defence)
        if (Global._sliceAddControlRandom_hitavoidance > 0)
        {
            Global._sliceAddControlRandom_hitavoidance--;
        }
        else
        {
            float sliceChance = (this.srv_defence * 0.002375f) + Global._sliceAddControlRandom;
            if (Random.Range(0, 100) < sliceChance)
            {
                damage = Mathf.FloorToInt(damage / 2f);
                Global._sliceAddControlRandom_hitavoidance = Mathf.FloorToInt(((100f - Global._sliceAddControlRandom) * 1f) / sliceChance);
                Global._sliceAddControlRandom = 0;
            }
            else
            {
                Global._sliceAddControlRandom = Global._sliceAddControlRandom + sliceChance;
            }
        }
        // apply damage crit (depends on user's crit chance)
        bool wasCritical = false;
        if (Global._critAddControlRandom_hitavoidance > 0)
        {
            Global._critAddControlRandom_hitavoidance--;
        }
        else
        {
            if (Random.Range(0, 100) < (System.Convert.ToSingle(Global._system_CharacterParams["crit"]) + Global._critAddControlRandom))
            {
                wasCritical = true;
                TempCrit = (int) Global._system_CharacterParams["crit"];
                damage = Mathf.FloorToInt(damage + (damage * (Global._userCrit / 100f)));
                Global._critAddControlRandom_hitavoidance = Mathf.FloorToInt(((100f - Global._critAddControlRandom) * 1f) / TempCrit);
                Global._critAddControlRandom = 0;
            }
            else
            {
                Global._critAddControlRandom = Global._critAddControlRandom + System.Convert.ToSingle(Global._system_CharacterParams["crit"]);
            }
        }
        if (this.npcwho == "npcmystic")
        {
            yield return new WaitForSeconds(0.49f);
            Global._playSound(this.gameObject, "sound_monk_hit");
        }
        this.npclife = this.npclife - damage;
        //Comment for Screens
        GameObject someGoFlying = new GameObject();
        ((s_FlyDigitScript) someGoFlying.AddComponent(typeof(s_FlyDigitScript))).goFlyingDigit(-1 * damage, new Vector3(this.gameObject.transform.position.x, this.gameObject.transform.position.y + 1.5f, this.gameObject.transform.position.z), wasCritical);
        if (needAddComboPoint)
        {
            Global.globalBus.SendMessage("c_GameController_Battle_command_setComboPoint");
        }
        Global.globalBus.SendMessage("c_GuiCommonController_command_updateNpcBar", new object[] {this.npclife, this.npclifemax});
        yield return new WaitForSeconds(0.7f);
    }

    public virtual void s_AIController_command_getExp() //nothing
    {
    }

    public virtual IEnumerator s_AIController_command_getCreatureToDeath()
    {
        int DropChanceTEMP = Random.Range(1, 100);
        //Comment for Screens
        if (!this.isNpcDead)
        {
            if (DropChanceTEMP < Global._userDropChance)
            {
                Global._dropPlacePoses.Push(this.gameObject.transform.position);
                Global.globalBus.SendMessage("c_GameController_Base_DropMob", new object[] {this.npcwho, this.lootType, this.lootAmount, this.npclevel.ToString()});
            }
            this.isNpcDead = true;
            if (Global._isRemoteMeleeAttackNPC != null)
            {
                ((MeshRenderer) ((s_AIController) Global._isRemoteMeleeAttackNPC.GetComponent(typeof(s_AIController))).enemySelect.GetComponent(typeof(MeshRenderer))).enabled = false;
            }
            Global._isBattleRagesOn = false;
        }
        this.s_AIController_command_StopBeAutomaticBoy();
        ((Animator) this.character.GetComponent(typeof(Animator))).SetTrigger("mobDeath");
        this.gameObject.GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = false;
        ((CapsuleCollider) this.gameObject.GetComponent(typeof(CapsuleCollider))).enabled = false;
        if ((Global._paramCurrentExp < 100) && !this._isAddExp)
        {
            this._isAddExp = true;
            Global._isMobDead = true;
            Global._paramCurrentExp = Global._paramCurrentExp + this.npcexp;
        }
        else
        {
            if ((Global._paramCurrentExp >= 100) && !this._isAddExp)
            {
                this._isAddExp = true;
                Global._isMobDead = true;
                Global._paramCurrentExp = 100;
            }
        }
        if ((Global._paramCurrentExp == 100) && Global._isMobDead)
        {
            Global._isGetNewLevel = true;
            Global._isMobDead = false;
            Global._paramCurrentExp = 0;
            Global._paramHeroLevel = Global._paramHeroLevel + 1;
            Global._userMaxHealth = Global._userMaxHealth + 5;
            Global._userCurrentHealth = Global._userMaxHealth;
            Global._system_CharacterParams["damagemin"] = ((int) Global._system_CharacterParams["damagemin"]) + 1;
            Global._system_CharacterParams["damagemax"] = ((int) Global._system_CharacterParams["damagemax"]) + 2;
        }
        Global._besideMobsList.Remove(this.gameObject);
        ((Animator) ((s_Character) Global._hero_dolly.GetComponent(typeof(s_Character))).character.GetComponent(typeof(Animator))).ResetTrigger("playerEndBattle");
        yield return new WaitForSeconds(10);
        Global._npcsToonsScripts.Remove(this.gameObject.name);
        GameObject.Destroy(this.gameObject);
        this._isAddExp = false;
    }

    // Moves the character according to the received target.
    public virtual void s_AIController_command_follow(Transform target)
    {
        if (this.character != null)
        {
            Vector3 targetVector = target.position - this.transform.position;
            float speed = (targetVector.magnitude - this.desiredDistance) * 2f;
            Vector3 directionVector = targetVector.normalized * speed;
            this.gameObject.transform.LookAt(new Vector3(target.position.x, this.gameObject.transform.position.y, target.position.z));
            ((Animator) this.character.GetComponent(typeof(Animator))).ResetTrigger("mobAttack");
            ((Animator) this.character.GetComponent(typeof(Animator))).ResetTrigger("mobIdle");
            ((Animator) this.character.GetComponent(typeof(Animator))).SetTrigger("mobRun");
            this._agent.SetDestination(target.position);
            if (this._agent.velocity.sqrMagnitude > Mathf.Epsilon)
            {
                this.transform.rotation = Quaternion.LookRotation(this._agent.velocity.normalized);
            }
        }
    }

    public virtual void s_AIController_command_Cry()
    {
    }

    private Transform _PushBackwardTargetTransform;
    private bool _isPushBackward;
    private float _PushBackwardVelocity;
    public virtual IEnumerator s_AIController_command_StartPushBackward(Transform target)
    {
        this._PushBackwardTargetTransform = target;
        this.motor.movement.maxBackwardsSpeed = 0.5f;
        this._isPushBackward = true;
        yield return new WaitForSeconds(0.4f);
        this._isPushBackward = false;
        this.motor.movement.maxBackwardsSpeed = 0;
    }

    // Moves the character some backward (after get the hit from hero)
    public virtual void s_AIController_command_PushBackward()
    {
        if ((this.motor != null) && this._isPushBackward)
        {
            Vector3 targetVector = (this.transform.position - this._PushBackwardTargetTransform.position).normalized;
            this.motor.SetVelocity(targetVector * this._PushBackwardVelocity);
            this.motor.inputMoveDirection = targetVector * this._PushBackwardVelocity;
        }
    }

    public virtual void s_AIController_command_stop()
    {
        this.soundRun = false;
        ((Animator) this.character.GetComponent(typeof(Animator))).ResetTrigger("mobRun");
        ((Animator) this.character.GetComponent(typeof(Animator))).SetTrigger("mobIdle");
    }

    public virtual void OnExternalVelocity() // todo : event from CharacterMotor when "SetVelocity" call
    {
    }

    public s_AIController()
    {
        this.srv_attackrange = 1f;
        this.srv_damagemin = 1;
        this.srv_damagemax = 1;
        this.npclevel = 1;
        this.srv_defence = 1;
        this.npclife = 25;
        this.npclifemax = 25;
        this.srv_attackspeed = 2f;
        this.srv_aspeed = 1.5f;
        this.srv_reactionradius = 5f;
        this.srv_crit = 1f;
        this.srv_npctype = "";
        this.npcwho = "";
        this.uniqueID = "";
        this.npcname = "";
        this.creatureCanAttack = true;
        this.automaticMashineIntervalMin = 0.2f;
        this.automaticMashineIntervalMax = 0.5f;
        this.lastTransformPointWhenCollided = Vector3.zero;
        this.lootType = "";
        this._PushBackwardVelocity = 2;
    }

}