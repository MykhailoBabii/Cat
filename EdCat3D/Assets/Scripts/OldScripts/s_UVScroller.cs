using UnityEngine;
using System.Collections;

[System.Serializable]
public partial class s_UVScroller : MonoBehaviour
{
    // Scroll main texture based on time
    //@script ExecuteInEditMode
    public float scrollSpeed;
    private MeshRenderer _renderer;
    public virtual void Start()
    {
        this._renderer = GetComponent<MeshRenderer>();
    }

    public virtual void Update()
    {
         //if (_renderer.material.shader.isSupported) Camera.main.depthTextureMode |= DepthTextureMode.Depth;
        float offset = Time.time * this.scrollSpeed;
        this._renderer.material.SetTextureOffset("_BumpMap", new Vector2(offset / -7f, offset));
        //_renderer.material.SetTextureOffset("_MainTex", Vector2(offset/10.0, offset));
        this._renderer.material.SetTextureOffset("_MainTex", new Vector2(0, -offset / 20f));
    }

    public s_UVScroller()
    {
        this.scrollSpeed = 0.1f;
    }

}