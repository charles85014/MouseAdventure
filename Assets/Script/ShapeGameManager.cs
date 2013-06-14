using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ShapeGameManager : MonoBehaviour
{
    public static ShapeGameManager master;

    public GameObject HintTextObject;           //右下提示文字物件
    public int GameCount;                       //觸發次數

    public List<Texture> ShapeList;             //右上圖形的提示貼圖(貼圖必須與RelativelyShapeList 相對應)
    public List<Texture> RelativelyShapeList;   //左邊觸發按鈕圖形的貼圖(貼圖必須與ShapeList 相對應)

    public List<GameObject> GameQueue;          //右上提示的物件(需照順序擺放)

    [HideInInspector]
    public List<int> GameQueueIndex = new List<int>();

    [HideInInspector]
    public GameTrigger TriggerScript;

    /// <summary>
    /// 確認圖形是否相同
    /// </summary>
    /// <param name="index">觸發按鍵的圖形編號</param>
    /// <param name="buttonScipt">觸發按鍵的 ShapeGameButton </param>
    public void CheckShape(int index, ShapeGameButton buttonScipt)
    {
        if (index == this.GameQueueIndex[0])    //假如觸發按鍵的圖形等於佇列第一個的圖形
        {
            this.GameCount--;   //剩餘次數減1
            if (this.GameCount == 0)
            {
                //假如次數等於0，結束此遊戲
                this.StartDestroy();
            }
            else
            {
                this.HintTextObject.GetComponent<DrawUIText>().TextString = "剩下" + this.GameCount.ToString() + "個";
                this.HintTextObject.GetComponent<DrawUIText>().UpdateTextContent();
            }
            //處理佇列(先進先出)
            this.GameQueueIndex.RemoveAt(0);
            int random = Random.Range(0, this.ShapeList.Count);
            this.GameQueueIndex.Add(random);

            //調整圖形順序
            for (int i = 0; i < this.GameQueue.Count; i++)
            {
                this.GameQueue[i].GetComponent<DrawUITextureBase>().uiBase.TextureStyle.normal.background = (Texture2D)this.ShapeList[this.GameQueueIndex[i]];
            }
        }

        //被觸發的Button重新換一張圖
        int Resetrandom = Random.Range(0, this.RelativelyShapeList.Count);
        buttonScipt.SetTexture(this.RelativelyShapeList[Resetrandom], Resetrandom);

    }

    void StartDestroy()
    {
        //UI消失動畫
        foreach (var script in this.GetComponentsInChildren<UpdateRectTo>())
            script.RunUpdate();

        StartCoroutine(RunDestroy(this.TriggerScript.RunTime));
    }

    IEnumerator RunDestroy(float time)
    {
        yield return new WaitForSeconds(time);
        MouseController.master.ChangeRunState(true);
        this.TriggerScript.EndRunPillar();
        Destroy(this.gameObject);
    }

    // Use this for initialization
    void Awake()
    {
        master = this;
    }

    void Start()
    {
        this.HintTextObject.GetComponent<DrawUIText>().TextString = "剩下" + this.GameCount.ToString() + "個";
        this.HintTextObject.GetComponent<DrawUIText>().UpdateTextContent();

        //初始化
        foreach (var script in this.GetComponentsInChildren<ShapeGameButton>())
        {
            int random = Random.Range(0, this.RelativelyShapeList.Count);
            script.SetTexture(this.RelativelyShapeList[random], random);
        }

        //初始化
        foreach (var obj in GameQueue)
        {
            int random = Random.Range(0, this.ShapeList.Count);
            obj.GetComponent<DrawUITextureBase>().uiBase.TextureStyle.normal.background = (Texture2D)this.ShapeList[random];
            this.GameQueueIndex.Add(random);
        }
    }
}