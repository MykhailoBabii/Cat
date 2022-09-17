using UnityEngine;
using System.Collections;

[System.Serializable]
public partial class c_GuiFontStyleController : MonoBehaviour
{
    /*****************************************************************************************
* The styles of all fonts
* 
* @author N-Game Studios 
* Research & Development
* 
*****************************************************************************************/
    private float guiRatio;
    public GUIStyle emptystyle;
    public GUIStyle font_arial_72_c_white;
    public GUIStyle font_arial_53_c_white;
    public GUIStyle font_arial_53_c_s;
    public GUIStyle font_arial_86_cb_white;
    public GUIStyle font_arial_86_cb_s;
    public GUIStyle font_arial_47_lb_white;
    public GUIStyle font_arial_47_lb_s;
    public GUIStyle font_arial_36_c_white;
    public GUIStyle font_arial_36_c_s;
    public GUIStyle font_arial_36_l_white;
    public GUIStyle font_arial_36_cb_green;
    public GUIStyle font_arial_36_cb_red;
    public GUIStyle font_arial_62_cb_white;
    public GUIStyle font_arial_62_cb_s;
    public GUIStyle font_arial_97_cb_white;
    public GUIStyle font_arial_97_cb_s;
    public GUIStyle font_arial_47_cb_white;
    public GUIStyle font_arial_60_l_white;
    public GUIStyle font_calibri_24_l_white;
    public GUIStyle font_calibri_54_cb_white;
    public GUIStyle font_calibri_54_cb_blue;
    public GUIStyle font_calibri_54_cb_s;
    public GUIStyle font_calibri_54_lb_white;
    public GUIStyle font_calibri_95_cb_blue;
    public GUIStyle font_calibri_95_cb_red;
    public GUIStyle font_calibri_95_cb_s;
    public GUIStyle font_century_24_lb_blue;
    public GUIStyle font_century_30_cb_blue;
    public GUIStyle font_century_30_cb_green;
    public GUIStyle font_century_30_cb_red;
    public GUIStyle font_century_48_cb_blue;
    public GUIStyle font_28daysletter_48_cb_blue;
    public GUIStyle font_28daysletter_60_cb_blue;
    public GUIStyle font_28daysletter_60_cb_white;
    public GUIStyle font_28daysletter_79_lb_blue;
    public GUIStyle font_b52_28_l_white;
    public GUIStyle font_b52_28_l_blue;
    public GUIStyle font_b52_36_c_white;
    public GUIStyle font_b52_36_l_white;
    public GUIStyle font_b52_36_l_blue;
    public GUIStyle font_b52_48_c_white;
    public GUIStyle font_b52_48_c_blue;
    public GUIStyle font_b52_60_cb_white;
    public virtual void LateUpdate()
    {
        this.font_arial_72_c_white.fontSize = (int) (72 * Global.guiRatio);
        this.font_arial_53_c_white.fontSize = (int) (53 * Global.guiRatio);
        this.font_arial_53_c_s.fontSize = (int) (53 * Global.guiRatio);
        this.font_arial_86_cb_white.fontSize = (int) (86 * Global.guiRatio);
        this.font_arial_86_cb_s.fontSize = (int) (86 * Global.guiRatio);
        this.font_arial_47_lb_white.fontSize = (int) (47 * Global.guiRatio);
        this.font_arial_47_lb_s.fontSize = (int) (47 * Global.guiRatio);
        this.font_arial_36_c_white.fontSize = (int) (36 * Global.guiRatio);
        this.font_arial_36_c_s.fontSize = (int) (36 * Global.guiRatio);
        this.font_arial_36_l_white.fontSize = (int) (36 * Global.guiRatio);
        this.font_arial_36_cb_green.fontSize = (int) (36 * Global.guiRatio);
        this.font_arial_36_cb_red.fontSize = (int) (36 * Global.guiRatio);
        this.font_arial_62_cb_white.fontSize = (int) (62 * Global.guiRatio);
        this.font_arial_62_cb_s.fontSize = (int) (62 * Global.guiRatio);
        this.font_arial_97_cb_white.fontSize = (int) (97 * Global.guiRatio);
        this.font_arial_97_cb_s.fontSize = (int) (97 * Global.guiRatio);
        this.font_arial_47_cb_white.fontSize = (int) (47 * Global.guiRatio);
        this.font_arial_60_l_white.fontSize = (int) (60 * Global.guiRatio);
        this.font_calibri_24_l_white.fontSize = (int) (24 * Global.guiRatio);
        this.font_calibri_54_cb_white.fontSize = (int) (54 * Global.guiRatio);
        this.font_calibri_54_cb_blue.fontSize = (int) (54 * Global.guiRatio);
        this.font_calibri_54_cb_s.fontSize = (int) (54 * Global.guiRatio);
        this.font_calibri_54_lb_white.fontSize = (int) (54 * Global.guiRatio);
        this.font_calibri_95_cb_blue.fontSize = (int) (95 * Global.guiRatio);
        this.font_calibri_95_cb_red.fontSize = (int) (95 * Global.guiRatio);
        this.font_calibri_95_cb_s.fontSize = (int) (95 * Global.guiRatio);
        this.font_century_24_lb_blue.fontSize = (int) (24 * Global.guiRatio);
        this.font_century_30_cb_blue.fontSize = (int) (30 * Global.guiRatio);
        this.font_century_30_cb_green.fontSize = (int) (30 * Global.guiRatio);
        this.font_century_30_cb_red.fontSize = (int) (30 * Global.guiRatio);
        this.font_century_48_cb_blue.fontSize = (int) (48 * Global.guiRatio);
        this.font_28daysletter_48_cb_blue.fontSize = (int) (48 * Global.guiRatio);
        this.font_28daysletter_60_cb_blue.fontSize = (int) (60 * Global.guiRatio);
        this.font_28daysletter_60_cb_white.fontSize = (int) (60 * Global.guiRatio);
        this.font_28daysletter_79_lb_blue.fontSize = (int) (79 * Global.guiRatio);
        this.font_b52_28_l_white.fontSize = (int) (28 * Global.guiRatio);
        this.font_b52_28_l_blue.fontSize = (int) (28 * Global.guiRatio);
        this.font_b52_36_c_white.fontSize = (int) (36 * Global.guiRatio);
        this.font_b52_36_l_white.fontSize = (int) (36 * Global.guiRatio);
        this.font_b52_36_l_blue.fontSize = (int) (36 * Global.guiRatio);
        this.font_b52_48_c_white.fontSize = (int) (48 * Global.guiRatio);
        this.font_b52_48_c_blue.fontSize = (int) (48 * Global.guiRatio);
        this.font_b52_60_cb_white.fontSize = (int) (60 * Global.guiRatio);
    }

    public c_GuiFontStyleController()
    {
        this.guiRatio = (Screen.width / 1920f);
        this.emptystyle = new GUIStyle();
        this.font_arial_72_c_white = new GUIStyle();
        this.font_arial_53_c_white = new GUIStyle();
        this.font_arial_53_c_s = new GUIStyle();
        this.font_arial_86_cb_white = new GUIStyle();
        this.font_arial_86_cb_s = new GUIStyle();
        this.font_arial_47_lb_white = new GUIStyle();
        this.font_arial_47_lb_s = new GUIStyle();
        this.font_arial_36_c_white = new GUIStyle();
        this.font_arial_36_c_s = new GUIStyle();
        this.font_arial_36_l_white = new GUIStyle();
        this.font_arial_36_cb_green = new GUIStyle();
        this.font_arial_36_cb_red = new GUIStyle();
        this.font_arial_62_cb_white = new GUIStyle();
        this.font_arial_62_cb_s = new GUIStyle();
        this.font_arial_97_cb_white = new GUIStyle();
        this.font_arial_97_cb_s = new GUIStyle();
        this.font_arial_47_cb_white = new GUIStyle();
        this.font_arial_60_l_white = new GUIStyle();
        this.font_calibri_24_l_white = new GUIStyle();
        this.font_calibri_54_cb_white = new GUIStyle();
        this.font_calibri_54_cb_blue = new GUIStyle();
        this.font_calibri_54_cb_s = new GUIStyle();
        this.font_calibri_54_lb_white = new GUIStyle();
        this.font_calibri_95_cb_blue = new GUIStyle();
        this.font_calibri_95_cb_red = new GUIStyle();
        this.font_calibri_95_cb_s = new GUIStyle();
        this.font_century_24_lb_blue = new GUIStyle();
        this.font_century_30_cb_blue = new GUIStyle();
        this.font_century_30_cb_green = new GUIStyle();
        this.font_century_30_cb_red = new GUIStyle();
        this.font_century_48_cb_blue = new GUIStyle();
        this.font_28daysletter_48_cb_blue = new GUIStyle();
        this.font_28daysletter_60_cb_blue = new GUIStyle();
        this.font_28daysletter_60_cb_white = new GUIStyle();
        this.font_28daysletter_79_lb_blue = new GUIStyle();
        this.font_b52_28_l_white = new GUIStyle();
        this.font_b52_28_l_blue = new GUIStyle();
        this.font_b52_36_c_white = new GUIStyle();
        this.font_b52_36_l_white = new GUIStyle();
        this.font_b52_36_l_blue = new GUIStyle();
        this.font_b52_48_c_white = new GUIStyle();
        this.font_b52_48_c_blue = new GUIStyle();
        this.font_b52_60_cb_white = new GUIStyle();
    }

}