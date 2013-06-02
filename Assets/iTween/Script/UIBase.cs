using UnityEngine;
using System.Collections;

public class UIBase : MonoBehaviour
{
    public int GUIdepth;            //貼圖深度(值越小越後畫)
    //public float fontSize;
    public GUIStyle TextureStyle;   //貼圖style

    // ITween color data
    public bool RunColor;
    public Color CurrentColor;
    public Color TargetColor = new Color(1, 1, 1, 1);
    public float ColorTime = 1;
    public float ColorDelay;
    public iTween.EaseType ColorEasetype;
    public iTween.LoopType ColorLooptype;

    // ITween Rect data
    private int currentRectIndex;

    public bool RunRect;
    public bool EndAutoClose;
    public Rect CurrentRect;
    public Rect[] TargetRectList = new Rect[1];
    public float[] RectTimeList = new float[1] { 1 };
    public float[] RectDelayList = new float[1];
    public iTween.EaseType RectEasetype;
    public iTween.LoopType RectLooptype;

    // ITween Value data
    public bool RunValueto;
    public float CurrentValue;
    public float TargetValue;
    public float ValueTime = 1;
    public float ValueDelay;
    public iTween.EaseType ValueEasetype;
    public iTween.LoopType ValueLooptype;


    public Color color_backup { get; set; }
    public Rect rect_backup { get; set; }
    public float Value_backup { get; set; }
    public float fontSize_backup { get; set; }

    void OnEnable()
    {
        this.currentRectIndex = 0;

        this.color_backup = this.CurrentColor;
        this.rect_backup = this.CurrentRect;
        this.Value_backup = this.CurrentValue;

        if (this.RunRect)
            this.RectTo();
        if (this.RunColor)
            this.ColorTo();
        if (this.RunValueto)
            this.ValueTo();
    }

    void OnDisable()
    {
        if (this.RunRect)
            this.StopRectTo();
        if (this.RunColor)
            this.StopColorTo();
        if (this.RunValueto)
            this.StopValueTo();
    }

    // Update is called once per frame
    void Update()
    {
        //this.TextureStyle.fontSize = (int)(this.fontSize * GameDefinition.WidthOffset);
    }

    void RectTo()
    {
        iTween.ValueTo(this.gameObject, iTween.Hash(
                    "from", this.CurrentRect,
                    "to", this.TargetRectList[this.currentRectIndex],
                    "delay", this.RectDelayList[this.currentRectIndex],
                    "time", this.RectTimeList[this.currentRectIndex],
                    "easetype", this.RectEasetype,
                    "onupdate", "updateRect",
                    "oncomplete", "completeRect",
                     "loopType", this.RectLooptype,
                     "name", "RectTo"));
    }

    void updateRect(Rect rect)
    {
        this.CurrentRect = rect;
    }

    void completeRect()
    {
        this.currentRectIndex++;
        if (this.TargetRectList.Length > this.currentRectIndex)
        {
            iTween.StopByName(this.gameObject, "RectTo");
            this.RectTo();
        }
        else
        {
            if (this.EndAutoClose)
                this.gameObject.SetActive(false);
        }
    }

    public void StopRectTo()
    {
        this.CurrentRect = this.rect_backup;
        iTween.StopByName(this.gameObject, "RectTo");
    }

    void ColorTo()
    {
        iTween.ValueTo(this.gameObject, iTween.Hash(
            "from", this.CurrentColor,
            "to", this.TargetColor,
            "delay", this.ColorDelay,
            "time", this.ColorTime,
            "easetype", this.ColorEasetype,
            "onupdate", "updateColor",
             "loopType", this.ColorLooptype,
             "name", "ColorTo"));
    }

    void updateColor(Color color)
    {
        this.CurrentColor = color;
    }

    void StopColorTo()
    {
        this.CurrentColor = this.color_backup;
        iTween.StopByName(this.gameObject, "ColorTo");
    }

    public void ValueTo()
    {
        iTween.ValueTo(this.gameObject, iTween.Hash(
            "from", this.CurrentValue,
            "to", this.TargetValue,
            "delay", this.ValueDelay,
            "time", this.ValueTime,
            "easetype", this.ValueEasetype,
            "onupdate", "updateValue",
             "loopType", this.ValueLooptype,
             "name", "ValueTo"));
    }

    void updateValue(float value)
    {
        this.CurrentValue = value;
    }

    public void StopValueTo()
    {
        this.CurrentValue = this.Value_backup;
        iTween.StopByName(this.gameObject, "ValueTo");
    }
}