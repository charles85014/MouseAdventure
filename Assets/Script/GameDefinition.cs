using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameDefinition
{
    public const float StandardScreenWidth = 1024.0f;
    public const float StandardScreenHeight = 600.0f;

    public static float WidthOffset = 1;
    public static float HeightOffset = 1;

    public enum SceneIndex
    {
        Title = 0, Atlas = 1, 第一神殿第一關 = 2, 第一神殿第二關 = 3
    }
}
