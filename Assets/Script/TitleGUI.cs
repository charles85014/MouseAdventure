using UnityEngine;
using System.Collections;

public class TitleGUI : MonoBehaviour {
    public Texture2D TitlePic;
	// Use this for initialization
	void Start () {
	
	}
    void OnGUI() { 
     GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), TitlePic);
	
    
    }
	// Update is called once per frame
    void Update()
    {
    }
}
