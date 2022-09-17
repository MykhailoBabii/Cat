using UnityEngine;
using System.Collections;

[System.Serializable]
public partial class s_AIController_PushNpcsByWeapon : MonoBehaviour
{
    private float lastCollidedTime;
    public virtual void Start()
    {
        this.lastCollidedTime = Time.time;
    }

    public virtual void OnTriggerEnter(Collider other)
    {
        if (Global._hasAttackSomeOneByHero)
        {
            if (other.gameObject.CompareTag("MONSTER"))
            {
                if (((s_AIController) other.gameObject.GetComponent(typeof(s_AIController))) != null)
                {
                    if (other.gameObject != null)
                    {
                        if (Global._system_CharacterParams.ContainsKey("damagemin") && Global._system_CharacterParams.ContainsKey("damagemax"))
                        {
                            int damageMin = (int) Global._system_CharacterParams["damagemin"];
                            int damageMax = (int) Global._system_CharacterParams["damagemax"];
                            int heroAttack = Mathf.FloorToInt(float.Parse((string) Global._system_CharacterParams["attack"]) * 0.5f);
                            int realDamage = Mathf.FloorToInt(Random.Range(damageMin, damageMax));
                            this.StartCoroutine(((s_AIController) other.gameObject.GetComponent(typeof(s_AIController))).s_AIController_command_makeDamageToCreature(heroAttack + realDamage, true));
                        }
                    }
                }
            }
        }
    }

}