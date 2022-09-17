using UnityEngine;
using System.Collections;

[System.Serializable]
public partial class c_DestroyController : MonoBehaviour
{
    /*****************************************************************************************
* Destroy Controller
* 
* @author N-Game Studios 
* Research & Development	
* 
*****************************************************************************************/
    private float lastSoundDestroyTime;
    public virtual void Start()
    {
        this.lastSoundDestroyTime = Time.time;
    }

    public virtual void Update()
    {
        if ((Time.time - this.lastSoundDestroyTime) > 5)
        {
            this.c_DestroyController_command_CLEAN_SOUNDGROUP("globalsound_s");
            this.c_DestroyController_command_CLEAN_SOUNDGROUP("globalsound_m");
            this.lastSoundDestroyTime = Time.time;
        }
    }

    public virtual void c_DestroyController_command_CLEAN()
    {
        foreach (DictionaryEntry oneobj in Global._globalDestroyer)
        {
            GameObject someobj = oneobj.Value as GameObject;
            if (someobj != null)
            {
                GameObject.Destroy(someobj);
            }
        }
        Global._globalDestroyer.Clear();
    }

    public virtual void c_DestroyController_command_ADD(GameObject someGO)
    {
        string keyit = ((("nd_" + Random.Range(1111, 9999).ToString()) + "_") + Random.Range(1411, 9599).ToString()) + Random.Range(1171, 9991).ToString();
        if (!Global._globalDestroyer.ContainsKey(keyit))
        {
            Global._globalDestroyer.Add(keyit, someGO);
        }
    }

    public virtual void c_DestroyController_command_ADD_to_GROUP(object[] inArr)
    {
        string someKey = (string) inArr[0];
        GameObject someGO = (GameObject) inArr[1];
        string keyit = ((("nd_" + Random.Range(1111, 9999).ToString()) + "_") + Random.Range(1411, 9599).ToString()) + Random.Range(1171, 9991).ToString();
        if (Global._globalDestroyer_groups.ContainsKey(someKey))
        {
            (Global._globalDestroyer_groups[someKey] as Hashtable).Add(keyit, someGO);
        }
        else
        {
            Global._globalDestroyer_groups.Add(someKey, new Hashtable());
            (Global._globalDestroyer_groups[someKey] as Hashtable).Add(keyit, someGO);
        }
    }

    public virtual void c_DestroyController_command_CLEAN_GROUP(string group)
    {
        if (Global._globalDestroyer_groups.ContainsKey(group))
        {
            foreach (DictionaryEntry oneobj in Global._globalDestroyer_groups[group] as Hashtable)
            {
                GameObject someobj = oneobj.Value as GameObject;
                if (someobj != null)
                {
                    GameObject.DestroyImmediate(someobj);
                }
            }
            Global._globalDestroyer_groups.Remove(group);
        }
    }

    public virtual void c_DestroyController_command_CLEAN_SOUNDGROUP(string wgroup)
    {
        if (Global._globalDestroyer_groups.ContainsKey(wgroup))
        {
            object[] someArr = new object[0];
            foreach (DictionaryEntry oneobj in Global._globalDestroyer_groups[wgroup] as Hashtable)
            {
                GameObject someobj = oneobj.Value as GameObject;
                if (someobj != null)
                {
                    if (!((AudioSource) someobj.GetComponent(typeof(AudioSource))).isPlaying)
                    {
                        GameObject.DestroyImmediate(someobj);
                        //someArr.Push(oneobj.Key);
                    }
                }
                else
                {
                    //someArr.Push(oneobj.Key);
                }
            }
            foreach (string oneKey in someArr)
            {
                (Global._globalDestroyer_groups[wgroup] as Hashtable).Remove(oneKey);
            }
        }
    }

}