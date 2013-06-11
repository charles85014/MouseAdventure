using UnityEngine;
using System.Collections;

[RequireComponent(typeof(UIBase))]
public class DrawUIText : MonoBehaviour
{
    public bool isMuiltTextContent = false;
    public float MaxWidth;      //文字的最大寬度，如果超過自動換下一列
    public bool isAutoWordwarp = true;
    public GameDefinition.TextContentIndex ChooseTextContent;
    public string TextString;

    public GameManager.GameState AfterFinishNextState;
    private string talkContent;

    private bool isRunFinish;
    private UIBase uiBase;
    private GUIContent currentContent = new GUIContent();

    void Start()
    {
        this.UpdateTextContent();
    }

    public void UpdateTextContent()
    {
        if (this.ChooseTextContent == GameDefinition.TextContentIndex.None)
            this.talkContent = this.TextString;

        else
            this.SetTextContent(this.ChooseTextContent);

        this.isRunFinish = false;
        this.uiBase = GetComponent<UIBase>();
        if (!this.uiBase)
            Debug.LogWarning(this.name + " -UIBase" + "-Unset");

        this.uiBase.TextureStyle.wordWrap = this.isAutoWordwarp;
        //this.uiBase.RunRect = false;
        this.uiBase.TargetValue = this.talkContent.Length;

        if (this.uiBase.RunValueto)
        {
            this.uiBase.StopValueTo();
            this.uiBase.ValueTo();
        }
        else
        {
            this.uiBase.CurrentValue = this.uiBase.TargetValue;
        }
    }

    void SetTextContent(GameDefinition.TextContentIndex content)
    {
        this.talkContent = GameDefinition.GetTextContent(content);
        if (this.talkContent == string.Empty)
        {
            this.isRunFinish = true;
            GameManager.SetGameState(this.AfterFinishNextState);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!this.isRunFinish)
        {
            if ((int)this.uiBase.CurrentValue >= this.talkContent.Length)
            {
                Time.timeScale = 1;
                if (Input.GetMouseButtonDown(0))
                {
                    if (this.isMuiltTextContent)
                    {
                        this.uiBase.ValueDelay = 0.1f;
                        this.UpdateTextContent();
                    }
                    else
                    {
                        this.isRunFinish = true;
                        GameManager.SetGameState(this.AfterFinishNextState);
                    }
                }
            }
            //else
            //{
            //    if (Input.GetMouseButton(0))
            //        Time.timeScale = 2;
            //    else
            //        Time.timeScale = 1;
            //}
        }

        this.currentContent.text = this.talkContent.Substring(0, (int)this.uiBase.CurrentValue);
        if (this.uiBase.TextureStyle.wordWrap)
        {
            Vector2 newContentShape = this.uiBase.TextureStyle.CalcSize(this.currentContent);

            if (newContentShape.x >= this.MaxWidth * GameDefinition.WidthOffset)
            {
                this.uiBase.CurrentRect.width = this.MaxWidth * GameDefinition.WidthOffset;
                this.uiBase.CurrentRect.height = this.uiBase.TextureStyle.CalcHeight(this.currentContent, this.MaxWidth * GameDefinition.WidthOffset);
            }
            else
            {
                this.uiBase.CurrentRect.width = newContentShape.x;
                this.uiBase.CurrentRect.height = newContentShape.y;
            }
        }
    }

    void OnGUI()
    {
        GUI.depth = this.uiBase.GUIdepth;
        GUI.color = this.uiBase.CurrentColor;

        if (this.isAutoWordwarp)
        {
            GUI.Button(new Rect(
                    this.uiBase.CurrentRect.x * GameDefinition.WidthOffset,
                    this.uiBase.CurrentRect.y * GameDefinition.HeightOffset,
                    this.uiBase.CurrentRect.width,
                    this.uiBase.CurrentRect.height),
            this.currentContent, this.uiBase.TextureStyle);
        }
        else
        {
            GUI.Button(new Rect(
                    this.uiBase.CurrentRect.x * GameDefinition.WidthOffset,
                    this.uiBase.CurrentRect.y * GameDefinition.HeightOffset,
                    this.uiBase.CurrentRect.width * GameDefinition.WidthOffset,
                    this.uiBase.CurrentRect.height * GameDefinition.HeightOffset),
            this.currentContent, this.uiBase.TextureStyle);
        }
    }
}