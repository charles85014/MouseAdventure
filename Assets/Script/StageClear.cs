using UnityEngine;
using System.Collections;

/// <summary>
/// 過關後的STAGE CLEAR字幕
/// </summary>
[RequireComponent(typeof(UIBase))]
public class StageClear : MonoBehaviour {

    public Texture TextureResoure;   //貼圖素材

    private UIBase uiBase;

    public int ClearPicAppear = 0;
    public float TimeConter = 0;
    public bool NextS = false;

    void Awake()
    {
        this.uiBase = GetComponent<UIBase>();
        if (!this.uiBase)
            Debug.LogWarning(this.name + " -UIBase" + "-Unset");
    }

    void OnTriggerEnter(Collider other) {

        ClearPicAppear = 1;
        MouseController.master.ChangeRunState(false);
    }

    void OnGUI()
    {
        if (ClearPicAppear == 1) {

            GUI.depth = this.uiBase.GUIdepth;
            GUI.color = this.uiBase.CurrentColor;

            GUI.Box(new Rect(
                this.uiBase.CurrentRect.x * GameDefinition.WidthOffset,
                this.uiBase.CurrentRect.y * GameDefinition.HeightOffset,
                this.uiBase.CurrentRect.width * GameDefinition.WidthOffset,
                this.uiBase.CurrentRect.height * GameDefinition.HeightOffset),
                string.Empty,
                this.uiBase.TextureStyle
                );
        }
        
    }
	// Use this for initialization
	void Start () {
        if (this.TextureResoure)
            this.uiBase.TextureStyle.normal.background = (Texture2D)this.TextureResoure;
	}
	
	// Update is called once per frame
	void Update () {
        if (ClearPicAppear == 1)
        {
            TimeConter += Time.deltaTime;
            if (TimeConter > 2)
                NextS = true;
        }
        if (NextS == true)
        {
            if (Input.GetMouseButtonDown(0))
            {
                GameObject obj = (GameObject)Instantiate(GameManager.master.LoadSceneObject);
                obj.GetComponent<LoadNextScene>().SetLoadScene(GameDefinition.SceneIndex.第一神殿選擇);
             
            }
        }
	}
}
