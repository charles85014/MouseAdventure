using UnityEngine;
using System.Collections;

/// <summary>
/// 第一種遊戲-打磚塊的Button
/// </summary>
[RequireComponent(typeof(UIBase))]
public class HitGameButton : MonoBehaviour
{
    public Texture[] TextureResoure;   //貼圖素材

    public int RectLife;        //磚塊生命
    public int RectIndex;       //磚塊索引值

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
        this.RectLife = Random.Range(1, this.TextureResoure.Length + 1);
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

            if (this.RectLife - 1 == -1)
                this.uiBase.TextureStyle.normal.background = null;
            else
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
                if (!HitGameManager.master.isColdDown)
                {
                    if (this.RectLife == 1)
                    {
                        int count = this.transform.parent.GetComponentsInChildren<HitGameButton>().Length;
                        int width = (int)Mathf.Sqrt(count);

                        int left = ((this.RectIndex - 1) >= 0) ? this.RectIndex - 1 : -1;
                        int right = ((this.RectIndex + 1) < count) ? this.RectIndex + 1 : -1;
                        int top = ((this.RectIndex - width) >= 0) ? this.RectIndex - 3 : -1;
                        int bottom = ((this.RectIndex + width) < count) ? this.RectIndex + 3 : -1;
                        int self = this.RectIndex;

                        if (self % width == 0)
                            left = -1;
                        else if (self % width == (width - 1))
                            right = -1;

                        foreach (var script in this.transform.parent.GetComponentsInChildren<HitGameButton>())
                        {
                            if (script.RectIndex == left || script.RectIndex == right || script.RectIndex == top ||

script.RectIndex == bottom || script.RectIndex == self)
                            {
                                if (script.RectLife > 0)
                                    script.RectLife--;
                            }
                        }
                        HitGameManager.master.StartColdDown();
                    }
                    else if (this.RectLife > 0)
                    {
                        this.RectLife--;
                        HitGameManager.master.StartColdDown();
                    }
                }
            }
        }
    }
}