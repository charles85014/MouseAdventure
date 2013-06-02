using UnityEngine;
using System.Collections;

/// <summary>
/// UI設計-繪製一張可自定義的Button UI
/// </summary>
[RequireComponent(typeof(UIBase))]
public class DrawUICustomButton : MonoBehaviour
{
    public GameManager.UIButtonEvent ButtonEvent;   //Buton Event
    public Texture TextureResoure;   //貼圖素材

    private UIBase uiBase;

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

    void Update()
    {
        
    }

    void OnGUI()
    {
        if (this.TextureResoure != null)
        {
            GUI.depth = this.uiBase.GUIdepth;
            GUI.color = this.uiBase.CurrentColor;

            if (GUI.Button(new Rect(
                this.uiBase.CurrentRect.x * GameDefinition.WidthOffset,
                this.uiBase.CurrentRect.y * GameDefinition.HeightOffset,
                this.uiBase.CurrentRect.width * GameDefinition.WidthOffset,
                this.uiBase.CurrentRect.height * GameDefinition.HeightOffset),
                string.Empty,
                this.uiBase.TextureStyle
                ))
            {
                GameManager.UIButtonClick(this.ButtonEvent);
            }
        }
    }
}