using UnityEngine;
using System.Collections;

public class MouseMove : MonoBehaviour
{
    public float distToGround;

    public Texture2D Mouse01, Mouse02;
    public int MouseDirect = 1;

    private int MousePicCount = 0;
    public float MouseSpeed;
    public float JumpForce;
    public float JumpTime;
    public float DropForce;
    

    public GameObject CountDownObject;
    public bool isGameStart = false;
    public bool isJump = false;
    public bool inTheAir = false;
    float JumpStep = 0;

    // Use this for initialization
    void Start()
    {
        StartCoroutine(CountDownStart());
        // get the distance to ground
        //distToGround = collider.bounds.extents.y;
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


    IEnumerator CountDownStart()
    {
        this.CountDownObject.SetActive(true);
        yield return new WaitForSeconds(3);
        this.isGameStart = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (isGameStart)
        {
            if (isJump == true) {
                inTheAir = true;
                this.renderer.material.mainTexture = Mouse01;
                if (MouseDirect == 1)
                    rigidbody.velocity = new Vector3(MouseSpeed, 0, 0);

                if (MouseDirect == 2)
                    rigidbody.velocity = new Vector3(-MouseSpeed, 0, 0);
                if (JumpStep < JumpTime)
                {
                    rigidbody.AddForce(0, JumpForce, 0);
                    JumpStep += Time.deltaTime;
                    print(JumpStep);
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
                    if (MouseDirect == 1)
                        rigidbody.velocity = new Vector3(MouseSpeed, -DropForce, 0);

                    if (MouseDirect == 2)
                        rigidbody.velocity = new Vector3(-MouseSpeed, -DropForce, 0);

                    if (!IsInvoking("MouseChangPic"))
                        Invoke("MouseChangPic", 0.2f);
            }
            else if (!IsGrounded() && isJump == false)             //在空中
            {
                this.renderer.material.mainTexture = Mouse01;
                if (MouseDirect == 1)
                    rigidbody.velocity = new Vector3(MouseSpeed, -DropForce, 0);

                if (MouseDirect == 2)
                    rigidbody.velocity = new Vector3(-MouseSpeed, -DropForce, 0);
            }

        }
    }

    RaycastHit hit;
    bool IsGrounded()
    {
        if (Physics.Raycast(transform.position - new Vector3((GetComponent<BoxCollider>().size.x * this.transform.localScale.x) / 2, 0, 0), -Vector3.up, out hit, distToGround) ||
            Physics.Raycast(transform.position + new Vector3((GetComponent<BoxCollider>().size.x * this.transform.localScale.x) / 2, 0, 0), -Vector3.up, out hit, distToGround) ||
            Physics.Raycast(transform.position, -Vector3.up, out hit, distToGround))
            return true;

        return false;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = new Color(1, 0, 0);
        Gizmos.DrawRay(transform.position, -Vector3.up * distToGround);
        Gizmos.DrawRay(transform.position - new Vector3((GetComponent<BoxCollider>().size.x * this.transform.localScale.x) / 2, 0, 0), -Vector3.up * distToGround);
        Gizmos.DrawRay(transform.position + new Vector3((GetComponent<BoxCollider>().size.x * this.transform.localScale.x) / 2, 0, 0), -Vector3.up * distToGround);
    }
}