using UnityEngine;
using System.Collections;

public class MaterialColorTo : MonoBehaviour
{
    // ITween color data
    public bool RunColor;
    public Color CurrentColor;
    public Color TargetColor = new Color(1, 1, 1, 1);
    public float ColorTime = 1;
    public float ColorDelay;
    public iTween.EaseType ColorEasetype;
    public iTween.LoopType ColorLooptype;

    public Color color_backup { get; set; }

    void OnEnable()
    {
        this.color_backup = this.CurrentColor;
        this.transform.renderer.material.color = this.CurrentColor;

        if (this.RunColor)
        {
            iTween.ColorTo(this.gameObject, iTween.Hash(
            "color", this.TargetColor,
            "delay", this.ColorDelay,
            "time", this.ColorTime,
            "easetype", this.ColorEasetype,
            "loopType", this.ColorLooptype,
            "name", "ColorTo"));
        }
    }


    void OnDisable()
    {
        if (this.RunColor)
            this.StopColorTo();
    }

    void StopColorTo()
    {
        this.CurrentColor = this.color_backup;
        iTween.StopByName(this.gameObject, "ColorTo");
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
