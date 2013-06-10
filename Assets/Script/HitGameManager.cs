using UnityEngine;
using System.Collections;

public class HitGameManager : MonoBehaviour
{
    public static HitGameManager master;

    [HideInInspector]
    public GameTrigger TriggerScript;

    public int TotalLife;

    public float ColdDownTime;
    public bool isColdDown { get; private set; }

    public void StartColdDown()
    {
        this.isColdDown = true;
        Invoke("RunColdDown", this.ColdDownTime);
    }

    void RunColdDown()
    {
        this.isColdDown = false;
    }

    // Use this for initialization
    void Awake()
    {
        master = this;
        this.isColdDown = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void StartDestroy()
    {
        this.GetComponentInChildren<UpdateRectTo>().RunUpdate();
        StartCoroutine(RunDestroy(this.GetComponentInChildren<UpdateRectTo>().RectTime));
    }

    IEnumerator RunDestroy(float time)
    {
        yield return new WaitForSeconds(time);
        MouseController.master.ChangeRunState(true);
        this.TriggerScript.EndRunPillar();
        Destroy(this.gameObject);
    }
}
