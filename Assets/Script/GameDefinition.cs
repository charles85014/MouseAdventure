using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameDefinition
{
    public const float StandardScreenWidth = 854.0f;
    public const float StandardScreenHeight = 480.0f;

    public static float WidthOffset = 1;
    public static float HeightOffset = 1;

    public enum SceneIndex
    {
        Title = 0, ChoosePlayerScene = 1, GameScene = 2
    }
}
