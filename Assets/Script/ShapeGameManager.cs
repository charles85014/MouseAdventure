using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ShapeGameManager : MonoBehaviour
{
    public static ShapeGameManager master;

    public GameObject HintTextObject;           //�k�U���ܤ�r����
    public int GameCount;                       //Ĳ�o����

    public List<Texture> ShapeList;             //�k�W�ϧΪ����ܶK��(�K�ϥ����PRelativelyShapeList �۹���)
    public List<Texture> RelativelyShapeList;   //����Ĳ�o���s�ϧΪ��K��(�K�ϥ����PShapeList �۹���)

    public List<GameObject> GameQueue;          //�k�W���ܪ�����(�ݷӶ����\��)

    [HideInInspector]
    public List<int> GameQueueIndex = new List<int>();

    [HideInInspector]
    public GameTrigger TriggerScript;

    /// <summary>
    /// �T�{�ϧάO�_�ۦP
    /// </summary>
    /// <param name="index">Ĳ�o���䪺�ϧνs��</param>
    /// <param name="buttonScipt">Ĳ�o���䪺 ShapeGameButton </param>
    public void CheckShape(int index, ShapeGameButton buttonScipt)
    {
        if (index == this.GameQueueIndex[0])    //���pĲ�o���䪺�ϧε����C�Ĥ@�Ӫ��ϧ�
        {
            this.GameCount--;   //�Ѿl���ƴ�1
            if (this.GameCount == 0)
            {
                //���p���Ƶ���0�A�������C��
                this.StartDestroy();
            }
            else
            {
                this.HintTextObject.GetComponent<DrawUIText>().TextString = "�ѤU" + this.GameCount.ToString() + "��";
                this.HintTextObject.GetComponent<DrawUIText>().UpdateTextContent();
            }
            //�B�z��C(���i���X)
            this.GameQueueIndex.RemoveAt(0);
            int random = Random.Range(0, this.ShapeList.Count);
            this.GameQueueIndex.Add(random);

            //�վ�ϧζ���
            for (int i = 0; i < this.GameQueue.Count; i++)
            {
                this.GameQueue[i].GetComponent<DrawUITextureBase>().uiBase.TextureStyle.normal.background = (Texture2D)this.ShapeList[this.GameQueueIndex[i]];
            }
        }

        //�QĲ�o��Button���s���@�i��
        int Resetrandom = Random.Range(0, this.RelativelyShapeList.Count);
        buttonScipt.SetTexture(this.RelativelyShapeList[Resetrandom], Resetrandom);

    }

    void StartDestroy()
    {
        //UI�����ʵe
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
        this.HintTextObject.GetComponent<DrawUIText>().TextString = "�ѤU" + this.GameCount.ToString() + "��";
        this.HintTextObject.GetComponent<DrawUIText>().UpdateTextContent();

        //��l��
        foreach (var script in this.GetComponentsInChildren<ShapeGameButton>())
        {
            int random = Random.Range(0, this.RelativelyShapeList.Count);
            script.SetTexture(this.RelativelyShapeList[random], random);
        }

        //��l��
        foreach (var obj in GameQueue)
        {
            int random = Random.Range(0, this.ShapeList.Count);
            obj.GetComponent<DrawUITextureBase>().uiBase.TextureStyle.normal.background = (Texture2D)this.ShapeList[random];
            this.GameQueueIndex.Add(random);
        }
    }
}