using UnityEngine;
using System.Collections;

public class GameTrigger : MonoBehaviour
{
    public GameObject GameUIObject;
    public float RunTime = 1;
    public float RunDistance;
    public LayerMask MouseLayer;

    private bool isEnd = false;

    void OnTriggerEnter(Collider other)
    {
        if (!this.isEnd)
        {
            if (((1 << other.gameObject.layer) & this.MouseLayer.value) > 0)
            {
                //Do Something
                MouseController.master.ChangeRunState(false);
                GameObject obj = (GameObject)Instantiate(this.GameUIObject);

                if (obj.GetComponent<HitGameManager>())
                    obj.GetComponent<HitGameManager>().TriggerScript = this;

                if (obj.GetComponent<ShapeGameManager>())
                    obj.GetComponent<ShapeGameManager>().TriggerScript = this;

                this.isEnd = true;
            }
        }
    }

    public void EndRunPillar()
    {
        iTween.MoveTo(this.transform.parent.gameObject, this.transform.parent.transform.position - new Vector3(0, this.RunDistance, 0), this.RunTime);
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
