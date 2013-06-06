using UnityEngine;
using System.Collections;

/// <summary>
/// UI�]�p-ø�s�i�۩w�q�Ϥ���Number list
/// </summary>
[RequireComponent(typeof(UIBase))]
public class DrawUICustomNumber : MonoBehaviour
{
    public enum NumberAlignment
    { Left, Center }
    public NumberAlignment alignment;

    public GameManager.GameValue WhichGameValue;    //��GameManager����
    public Texture[] TextureResoure;    //�K�ϯ���

    private string numberToString;       //�ƭ���r��
    private Vector2 textureSize;        //�K�Ϥؤo
    private UIBase uiBase;

    void OnEnable()
    {
        this.uiBase = GetComponent<UIBase>();
        if (!this.uiBase)
            Debug.LogWarning(this.name + " -UIBase" + "-Unset");
    }

    void Start()
    {
        this.textureSize = new Vector2(this.TextureResoure[0].width, this.TextureResoure[0].height);
    }

    void Update()
    {
        this.numberToString = GameManager.GetGameValue(this.WhichGameValue).ToString();
    }

    void OnGUI()
    {
        GUI.color = this.uiBase.CurrentColor;
        GUI.depth = this.uiBase.GUIdepth;

        if (this.alignment == NumberAlignment.Center)
        {
            for (int i = 0; i < this.numberToString.Length; i++)
            {
                GUI.Box(new Rect((this.uiBase.CurrentRect.x - (this.numberToString.Length * this.textureSize.x) / 2 + (this.textureSize.x) * i) * GameDefinition.WidthOffset,
                            this.uiBase.CurrentRect.y * GameDefinition.HeightOffset,
                            this.textureSize.x * GameDefinition.WidthOffset,
                            this.textureSize.y * GameDefinition.HeightOffset),
                        this.TextureResoure[int.Parse(this.numberToString[i].ToString())],
                        this.uiBase.TextureStyle);
            }
        }
        else if (this.alignment == NumberAlignment.Left)
        {
            for (int i = 0; i < this.numberToString.Length; i++)
            {
                GUI.Box(new Rect((this.uiBase.CurrentRect.x + (this.textureSize.x) * i) * GameDefinition.WidthOffset,
                            this.uiBase.CurrentRect.y * GameDefinition.HeightOffset,
                            this.textureSize.x * GameDefinition.WidthOffset,
                            this.textureSize.y * GameDefinition.HeightOffset),
                        this.TextureResoure[int.Parse(this.numberToString[i].ToString())],
                        this.uiBase.TextureStyle);
            }
        }
    }
}