using UnityEngine;
using System.Collections;

/// <summary>
/// UI�]�p-ø�s�i�۩w�q�Ϥ���Number list
/// </summary>
[RequireComponent(typeof(UIBase))]
public class GameTimer : MonoBehaviour
{
    public static GameTimer master;

    public bool isRunTimer = false;

    public enum NumberAlignment
    { Left, Center }
    public NumberAlignment alignment;

    public Texture[] TextureResoure;    //�K�ϯ���

    private float addValue;
    private string numberToString;       //�ƭ���r��
    private UIBase uiBase;

    void OnEnable()
    {
        this.uiBase = GetComponent<UIBase>();
        if (!this.uiBase)
            Debug.LogWarning(this.name + " -UIBase" + "-Unset");
    }

    void Awake()
    {
        master = this;
    }

    void Start()
    {
        this.addValue = 0;
        this.numberToString = this.addValue.ToString();
    }

    void Update()
    {
        if (this.isRunTimer)
        {
            //this.numberToString = GameManager.GetGameValue(this.WhichGameValue).ToString();
            this.numberToString = ((int)(this.addValue += Time.deltaTime)).ToString();
        }
    }

    void OnGUI()
    {
        if (this.isRunTimer)
        {
            GUI.color = this.uiBase.CurrentColor;
            GUI.depth = this.uiBase.GUIdepth;

            if (this.alignment == NumberAlignment.Center)
            {
                for (int i = 0; i < this.numberToString.Length; i++)
                {
                    GUI.Box(new Rect((this.uiBase.CurrentRect.x - (this.numberToString.Length * this.uiBase.CurrentRect.width) / 2 + (this.uiBase.CurrentRect.width) * i) * GameDefinition.WidthOffset,
                                this.uiBase.CurrentRect.y * GameDefinition.HeightOffset,
                                this.uiBase.CurrentRect.width * GameDefinition.WidthOffset,
                                this.uiBase.CurrentRect.height * GameDefinition.HeightOffset),
                            this.TextureResoure[int.Parse(this.numberToString[i].ToString())],
                            this.uiBase.TextureStyle);
                }
            }
            else if (this.alignment == NumberAlignment.Left)
            {
                for (int i = 0; i < this.numberToString.Length; i++)
                {
                    GUI.Box(new Rect((this.uiBase.CurrentRect.x + (this.uiBase.CurrentRect.width) * i) * GameDefinition.WidthOffset,
                                this.uiBase.CurrentRect.y * GameDefinition.HeightOffset,
                                this.uiBase.CurrentRect.width * GameDefinition.WidthOffset,
                                this.uiBase.CurrentRect.height * GameDefinition.HeightOffset),
                            this.TextureResoure[int.Parse(this.numberToString[i].ToString())],
                            this.uiBase.TextureStyle);
                }
            }
        }
    }
}