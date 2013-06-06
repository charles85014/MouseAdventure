using UnityEngine;
using System.Collections;

public class MouseMove : MonoBehaviour
{
    public float distToGround;

    public Texture2D Mouse01, Mouse02;
    public int MouseDirect = 1;

    int MousePicCount = 0;
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

            if (MouseDirect == 1)
                rigidbody.velocity = new Vector3(MouseSpeed * Time.deltaTime, -0.1f, 0);

            if (MouseDirect == 2)
                rigidbody.velocity = new Vector3(-MouseSpeed * Time.deltaTime, -0.1f, 0);

            if (IsGrounded())
            {
                if (!IsInvoking("MouseChangPic"))
                    Invoke("MouseChangPic", 0.2f);

            }

        }
    }

    RaycastHit hit;
    bool IsGrounded()
    {
        return Physics.Raycast(transform.position, -Vector3.up, out hit, distToGround + 0.1f);
    }




}
