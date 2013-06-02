using UnityEngine;
using System.Collections;

[RequireComponent(typeof(UIBase))]
public class DrawUITextureBase : MonoBehaviour
{
    private UIBase uiBase;
    public Texture TextureResoure;   //¶K¹Ï¯À§÷

    void Awake()
    {
        this.uiBase = GetComponent<UIBase>();
        if (!this.uiBase)
            Debug.LogWarning(this.name + " -UIBase" + "-Unset");
    }

    // Use this for initialization
    void Start()
    {
        this.uiBase.TextureStyle.normal.background = (Texture2D)this.TextureResoure;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    void OnGUI()
    {
        GUI.depth = this.uiBase.GUIdepth;
        GUI.color = this.uiBase.CurrentColor;

        GUI.Box(new Rect(
            this.uiBase.CurrentRect.x * GameDefinition.WidthOffset,
            this.uiBase.CurrentRect.y * GameDefinition.HeightOffset,
            this.uiBase.CurrentRect.width * GameDefinition.WidthOffset,
            this.uiBase.CurrentRect.height * GameDefinition.HeightOffset),
            string.Empty,
            this.uiBase.TextureStyle
            );
    }
}

