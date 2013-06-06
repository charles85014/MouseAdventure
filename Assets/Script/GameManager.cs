using UnityEngine;
using System.Collections;
using System.Collections.Generic;



public class GameManager : MonoBehaviour
{
    public static GameManager master;
    public GameObject LoadSceneObject;

    public GameDefinition.SceneIndex CurrentScene;

    /// <summary>
    /// 設定遊戲場景的初始狀態
    /// </summary>
    void Awake()
    {
        master = this;

        GameDefinition.WidthOffset = (float)Screen.width / GameDefinition.StandardScreenWidth;
        GameDefinition.HeightOffset = (float)Screen.height / GameDefinition.StandardScreenHeight;

        switch (this.CurrentScene)
        {
            case GameDefinition.SceneIndex.Title:
                break;

            case GameDefinition.SceneIndex.Atlas:
                break;

            case GameDefinition.SceneIndex.Stage01:
                break;

            default:
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        GameDefinition.WidthOffset = (float)Screen.width / GameDefinition.StandardScreenWidth;
        GameDefinition.HeightOffset = (float)Screen.height / GameDefinition.StandardScreenHeight;

        switch (this.CurrentScene)
        {
            case GameDefinition.SceneIndex.Title:
                break;
            case GameDefinition.SceneIndex.Atlas:
                break;
            case GameDefinition.SceneIndex.Stage01:
            default:
                break;
        }
    }

    public static void SetGameState(GameState state)
    {
        switch (state)
        {
            case GameState.Nothing:
                break;
            default:
                break;
        }
    }

    public static void UIButtonClick(UIButtonEvent choose)
    {
        switch (choose)
        {
            case UIButtonEvent.Nothing:

                break;

            case UIButtonEvent.LeftMove:
                GameObject.Find("Mouse").renderer.material.mainTextureScale = new Vector2(-1, 1);
                GameObject.Find("Mouse").GetComponent<MouseMove>().MouseDirect = 2;
                break;

            case UIButtonEvent.RightMove:
                GameObject.Find("Mouse").renderer.material.mainTextureScale = new Vector2(1, 1);
                GameObject.Find("Mouse").GetComponent<MouseMove>().MouseDirect = 1;
                break;

            default:
                break;
        }
    }

    public static int GetGameValue(GameValue value, int playerIndex = -1)
    {
        switch (value)
        {
            case GameValue.Templete:
                return 0;

            default:
                return 0;
        }
    }

    #region Enum Define

    public enum GameState
    {
        Nothing = 0
    }

    public enum UIButtonEvent
    {
        Nothing = 0, LeftMove, RightMove
    }

    public enum GameValue
    {
        Templete = 0
    }

    public enum UITextPattern
    {
        ShadowAndOutline,
        Shadow,
        Outline
    }
    #endregion
}
