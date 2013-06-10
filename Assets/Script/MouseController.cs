using UnityEngine;
using System.Collections;

public class MouseController : MonoBehaviour
{
    public static MouseController master;

    public float DistanceToGround;

    public Texture2D Mouse01, Mouse02;

    public enum MouseDirection
    {
        None = 0, Right = 1, Left = 2
    }
    [HideInInspector]
    public MouseDirection MouseDirect = MouseDirection.None;

    private int MousePicCount = 0;
    public float MouseSpeed;
    public float JumpForce;
    public float JumpTime;
    public float DropForce;


    public GameObject CountDownObject;
    [HideInInspector]
    public bool isRunning = false;
    public bool isJump = false;
    public bool inTheAir = false;
    float JumpStep = 0;

    public void ChangeRunState(bool state)
    {
        this.isRunning = state;
        this.rigidbody.velocity = Vector3.zero;
    }

    void Awake()
    {
        master = this;
    }

    // Use this for initialization
    void Start()
    {
        MouseController.master.ChangeRunState(false);
    }

    void MouseChangPic()
    {
        if (MousePicCount == 0)
        {
            this.renderer.material.mainTexture = Mouse01;
            MousePicCount++;
        }
        else if (MousePicCount == 1)
        {
            this.renderer.material.mainTexture = Mouse02;
            MousePicCount--;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isRunning)
        {
            if (isJump == true)
            {
                inTheAir = true;
                this.renderer.material.mainTexture = Mouse01;
                if (MouseDirect == MouseDirection.Right)
                    rigidbody.velocity = new Vector3(MouseSpeed, 0, 0);

                if (MouseDirect == MouseDirection.Left)
                    rigidbody.velocity = new Vector3(-MouseSpeed, 0, 0);
                if (JumpStep < JumpTime)
                {
                    rigidbody.AddForce(0, JumpForce, 0);
                    JumpStep += Time.deltaTime;
                    //print(JumpStep);
                }
                else
                {
                    isJump = false;
                    JumpStep = 0;
                }
            }
            else if (IsGrounded())   //在地上
            {
                inTheAir = false;
                if (MouseDirect == MouseDirection.Right)
                    rigidbody.velocity = new Vector3(MouseSpeed, -DropForce, 0);

                if (MouseDirect == MouseDirection.Left)
                    rigidbody.velocity = new Vector3(-MouseSpeed, -DropForce, 0);

                if (!IsInvoking("MouseChangPic"))
                    Invoke("MouseChangPic", 0.2f);
            }
            else if (!IsGrounded() && isJump == false)             //在空中
            {
                this.renderer.material.mainTexture = Mouse01;
                if (MouseDirect == MouseDirection.Right)
                    rigidbody.velocity = new Vector3(MouseSpeed, -DropForce, 0);

                if (MouseDirect == MouseDirection.Left)
                    rigidbody.velocity = new Vector3(-MouseSpeed, -DropForce, 0);
            }
        }
    }


    bool IsGrounded()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position - new Vector3((GetComponent<BoxCollider>().size.x * this.transform.localScale.x) / 2, 0, 0), -Vector3.up, out hit, DistanceToGround) ||
            Physics.Raycast(transform.position + new Vector3((GetComponent<BoxCollider>().size.x * this.transform.localScale.x) / 2, 0, 0), -Vector3.up, out hit, DistanceToGround) ||
            Physics.Raycast(transform.position, -Vector3.up, out hit, DistanceToGround))
            return true;

        return false;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = new Color(1, 0, 0);
        Gizmos.DrawRay(transform.position, -Vector3.up * DistanceToGround);
        Gizmos.DrawRay(transform.position - new Vector3((GetComponent<BoxCollider>().size.x * this.transform.localScale.x) / 2, 0, 0), -Vector3.up * DistanceToGround);
        Gizmos.DrawRay(transform.position + new Vector3((GetComponent<BoxCollider>().size.x * this.transform.localScale.x) / 2, 0, 0), -Vector3.up * DistanceToGround);
    }
}