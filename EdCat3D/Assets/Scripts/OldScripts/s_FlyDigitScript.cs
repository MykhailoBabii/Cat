using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public partial class s_FlyDigitScript : MonoBehaviour
{
    /*****************************************************************************************
* Flying digits script
* 
* @author N-Game Studios
* Research & Development	
* 
*****************************************************************************************/
    private bool thisIsExists;
    private bool isDamageFIRST;
    private bool isDamageVisible;
    private int damageNum;
    private Vector3 labelPos;
    private Vector3 objectPos;
    private Stack<object> myDigitArr;
    private bool isCritical;
    public virtual void goFlyingDigit(int value, Vector3 pos, bool isCrit)
    {
        if (value != 0)
        {
            this.damageNum = value;
            this.isDamageVisible = true;
            this.objectPos = pos;
            this.isCritical = isCrit;
            string myStr = Mathf.FloorToInt(Mathf.Abs(this.damageNum)).ToString();
            int i1 = 0;
            while (i1 < myStr.Length)
            {
                this.myDigitArr.Push("" + myStr[i1]);
                i1++;
            }
        }
        else
        {
            UnityEngine.Object.Destroy(this.gameObject, 1);
        }
    }

    private IEnumerator putOff()
    {
        yield return new WaitForSeconds(2);
        this.isDamageVisible = false;
        this.isDamageFIRST = true;
        this.thisIsExists = false;
    }

    private GUIStyle getDigitCriticalPictureStyle(string what)
    {
        GUIStyle picStyle = new GUIStyle();
        picStyle.stretchWidth = false;
        picStyle.fixedWidth = 22;
        picStyle.fixedHeight = 30;
        picStyle.normal.background = Resources.Load("digits/red/digits_red_" + what) as Texture2D;
        return picStyle;
    }

    private GUIStyle getDigitDamagePictureStyle(string what)
    {
        GUIStyle picStyle = new GUIStyle();
        Texture2D image = null;
        picStyle.stretchWidth = false;
        picStyle.fixedWidth = 22;
        picStyle.fixedHeight = 30;
        image = Resources.Load("digits/yellow/digits_yellow_" + what) as Texture2D;
        picStyle.normal.background = image;
        return picStyle;
    }

    private GUIStyle getDigitHealthPictureStyle(string what)
    {
        GUIStyle picStyle = new GUIStyle();
        picStyle.stretchWidth = false;
        picStyle.fixedWidth = 22;
        picStyle.fixedHeight = 30;
        picStyle.normal.background = Resources.Load("digits/green/digits_green_" + what) as Texture2D;
        return picStyle;
    }

    public virtual void OnGUI()
    {
        if (this.isDamageVisible)
        {
            if (this.isDamageFIRST)
            {
                this.isDamageFIRST = false;
                Vector3 userPos = this.objectPos;
                userPos.y = userPos.y + 1;
                this.labelPos = Camera.main.WorldToScreenPoint(userPos);
                this.labelPos.y = Screen.height - this.labelPos.y;
            }
            this.StartCoroutine(this.putOff());
            GUILayout.BeginArea(new Rect(this.labelPos.x, this.labelPos.y, 200, 100));
            GUILayout.BeginHorizontal(new GUILayoutOption[] {});
            if (this.damageNum < 0)
            {
                GUILayout.Box("", this.isCritical ? this.getDigitCriticalPictureStyle("minus") : this.getDigitDamagePictureStyle("minus"), new GUILayoutOption[] {});
                foreach (string curDigit in this.myDigitArr)
                {
                    GUILayout.Box("", this.isCritical ? this.getDigitCriticalPictureStyle(curDigit) : this.getDigitDamagePictureStyle(curDigit), new GUILayoutOption[] {});
                }
            }
            else
            {
                GUILayout.Box("", this.getDigitHealthPictureStyle("plus"), new GUILayoutOption[] {});
                foreach (string curDigit in this.myDigitArr)
                {
                    GUILayout.Box("", this.getDigitHealthPictureStyle(curDigit), new GUILayoutOption[] {});
                }
            }
            GUILayout.EndHorizontal();
            GUILayout.EndArea();
            this.labelPos.x = this.labelPos.x - 0.2f;
            this.labelPos.y = this.labelPos.y - 0.9f;
        }
        if (!this.thisIsExists)
        {
            UnityEngine.Object.Destroy(this.gameObject, 1);
        }
    }

    public s_FlyDigitScript()
    {
        this.thisIsExists = true;
        this.isDamageFIRST = true;
        this.myDigitArr = new Stack<object>();
    }

}