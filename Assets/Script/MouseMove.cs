using UnityEngine;
using System.Collections;

public class MouseMove : MonoBehaviour {
    public Texture2D Mouse01, Mouse02;
    int MouseDirect = 1;
     CharacterController chars;
    int MousePicCount = 0;
    public float MouseSpeed;
	// Use this for initialization
	void Start () {
        int MouseDirect = 1;
        int MousePicCount = 0;
        chars = GetComponent<CharacterController>();
	}

    void MouseChangPic() {
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
	void Update () {

       


        if(MouseDirect == 1)
            this.chars.Move(new Vector3(MouseSpeed * Time.deltaTime, 0, 0));

        if(MouseDirect == 2)
            this.chars.Move(new Vector3(-MouseSpeed * Time.deltaTime, 0, 0));

        if (chars.isGrounded) {
            if (!IsInvoking("MouseChangPic"))
                Invoke("MouseChangPic", 0.2f);
            
        }
	}
}
