using UnityEngine;
using System.Collections;

public class LoadNextScene : MonoBehaviour
{
    public void SetLoadScene(GameDefinition.SceneIndex index)
    {
        if (!Application.isLoadingLevel)
        {
            Application.LoadLevelAsync(index.ToString());
        }
    }

    // Use this for initialization
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    void OnLevelWasLoaded(int level)
    {
        Destroy(this.gameObject);
    }
}