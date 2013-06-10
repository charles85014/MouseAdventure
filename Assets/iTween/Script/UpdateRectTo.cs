using UnityEngine;
using System.Collections;

public class UpdateRectTo : MonoBehaviour
{
    private UIBase uiBase;

    public Rect TargetRect;
    public float RectTime = 1;
    public float RectDelay = 0;
    public iTween.EaseType RectEasetype;
    public iTween.LoopType RectLooptype;

    void OnEnable()
    {
        this.uiBase = GetComponent<UIBase>();
        if (!this.uiBase)
            Debug.LogWarning(this.name + " -UIBase" + "-Unset");
    }

    public void RunUpdate()
    {
        this.uiBase.TextureStyle.wordWrap = false;
        //this.uiBase.StopRectTo();
        this.RectTo();
    }

    public void RectTo()
    {
        iTween.ValueTo(this.gameObject, iTween.Hash(
                    "from", this.uiBase.CurrentRect,
                    "to", this.TargetRect,
                    "delay", this.RectDelay,
                    "time", this.RectTime,
                    "easetype", this.RectEasetype,
                    "onupdate", "updateRect",
                    "oncomplete", "completeRect",
                     "loopType", this.RectLooptype,
                     "name", "RectTo"));
    }

    void updateRect(Rect rect)
    {
        this.uiBase.CurrentRect = rect;
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
