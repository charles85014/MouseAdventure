using UnityEngine;
using System.Collections;

public class ChooseStageTouch : MonoBehaviour
{
    public Color TargetColor = new Color(1, 0, 0, 1);
    // Use this for initialization
    void Start()
    {
        this.renderer.material.color = new Color(0, 0, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            this.renderer.material.color = this.TargetColor;

        }
        else if (Input.GetMouseButtonUp(0))
        {
            this.renderer.material.color = new Color(0, 0, 0, 0);
            GameObject obj = (GameObject)Instantiate(GameManager.master.LoadSceneObject);
            obj.GetComponent<LoadNextScene>().SetLoadScene(GameDefinition.SceneIndex.第一神殿第一關);
            Destroy(GameObject.Find("UI"));
        }
    }
}
