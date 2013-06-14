using UnityEngine;
using System.Collections;

/// <summary>
/// 第二種遊戲-Shape Game Button
/// </summary>
[RequireComponent(typeof(UIBase))]
public class ShapeGameButton : MonoBehaviour
{
    public int CurrentTextureIndex;

    public Texture TextureResoure;   //貼圖素材
    private UIBase uiBase;

    public void SetTexture(Texture texture, int index)
    {
        this.uiBase.TextureStyle.normal.background = (Texture2D)texture;
        this.CurrentTextureIndex = index;
    }

    void Awake()
    {
        this.uiBase = GetComponent<UIBase>();
        if (!this.uiBase)
            Debug.LogWarning(this.name + " -UIBase" + "-Unset");
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
                ShapeGameManager.master.CheckShape(this.CurrentTextureIndex, this);     //確認是否和第一個圖形相同
            }
        }
    }
}