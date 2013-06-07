using UnityEngine;
using System.Collections;

public class MouseMove : MonoBehaviour
{
    public float distToGround;

    public Texture2D Mouse01, Mouse02;
    public int MouseDirect = 1;

    private int MousePicCount = 0;
    public float MouseSpeed;

    public GameObject CountDownObject;
    public bool isGameStart = false;

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
            if (IsGrounded())   //在地上
            {
                if (MouseDirect == 1)
                    rigidbody.velocity = new Vector3(MouseSpeed * Time.deltaTime, -0.1f, 0);

                if (MouseDirect == 2)
                    rigidbody.velocity = new Vector3(-MouseSpeed * Time.deltaTime, -0.1f, 0);

                if (!IsInvoking("MouseChangPic"))
                    Invoke("MouseChangPic", 0.2f);
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    //rigidbody.velocity += new Vector3(0, 50, 0);
                    rigidbody.AddForce(0, 1000, 0);
                }
            }
            else                    //在空中
            {
                rigidbody.velocity = new Vector3(0, Physics.gravity.y, 0);
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