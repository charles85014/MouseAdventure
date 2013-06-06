using UnityEngine;
using System.Collections;

public class HitGameManager : MonoBehaviour
{
    public static HitGameManager master;

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
}
