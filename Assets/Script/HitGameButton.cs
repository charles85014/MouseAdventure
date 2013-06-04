using UnityEngine;
using System.Collections;

/// <summary>
/// �Ĥ@�عC��-���j����Button
/// </summary>
[RequireComponent(typeof(UIBase))]
public class HitGameButton : MonoBehaviour
{
    public GameManager.UIButtonEvent ButtonEvent;   //Buton Event
    public Texture[] TextureResoure;   //�K�ϯ���

    public int RectLife;      //�j���ͩR
    public int RectIndex;       //�j�����ޭ�


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

            this.uiBase.TextureStyle.normal.background = (Texture2D)this.TextureResoure[this.RectLife - 1];

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