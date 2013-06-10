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

            case GameDefinition.SceneIndex.第一神殿第一關:
                StartCoroutine(CountDownStart());
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
            case GameDefinition.SceneIndex.第一神殿第一關:
                break;
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
                if (MouseController.master.isRunning)
                {
                    MouseController.master.renderer.material.mainTextureScale = new Vector2(-1, 1);
                    MouseController.master.MouseDirect = MouseController.MouseDirection.Left;
                }
                break;

            case UIButtonEvent.RightMove:
                if (MouseController.master.isRunning)
                {
                    MouseController.master.renderer.material.mainTextureScale = new Vector2(1, 1);
                    MouseController.master.MouseDirect = MouseController.MouseDirection.Right;
                }
                break;

            case UIButtonEvent.Jump:
                if (MouseController.master.isRunning)
                {
                    if (MouseController.master.isJump == false
                         && MouseController.master.inTheAir == false)
                    {
                        MouseController.master.isJump = true;
                    }
                }
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

    IEnumerator CountDownStart()
    {
        yield return new WaitForSeconds(3);
        MouseController.master.ChangeRunState(true);
        GameTimer.master.isRunTimer = true;
    }

    #region Enum Define

    public enum GameState
    {
        Nothing = 0
    }

    public enum UIButtonEvent
    {
        Nothing = 0, LeftMove, RightMove, Jump
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
