using UnityEngine;
using System.Collections;

public class ChooseStageTouch : MonoBehaviour
{
    public Color OriginColor = new Color(1, 1, 1, 1);
    public Color TargetColor = new Color(1, 0, 0, 1);
    public GameDefinition.SceneIndex NextScene;

    void OnMouseDown()
    {
        this.renderer.material.color = this.TargetColor;
    }

    void OnMouseUp()
    {
        this.renderer.material.color = this.OriginColor;
        GameObject obj = (GameObject)Instantiate(GameManager.master.LoadSceneObject);
        obj.GetComponent<LoadNextScene>().SetLoadScene(this.NextScene);
        Destroy(GameObject.Find("UI"));
    }

    // Use this for initialization
    void Start()
    {
        this.renderer.material.color = this.OriginColor;
    }

    // Update is called once per frame
    void Update()
    {
    }
}
