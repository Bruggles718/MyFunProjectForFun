using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public static class Helpers
{
    private static Dictionary<float, WaitForSeconds> waitForSecondsDict = new Dictionary<float, WaitForSeconds>();

    public static Vector3 MousePos2D()
    {
        var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        return new Vector3(mousePos.x, mousePos.y);
    }

    public static WaitForSeconds WaitForSeconds(float duration)
    {
        waitForSecondsDict.TryGetValue(duration, out var result);
        if (result == null)
        {
            waitForSecondsDict[duration] = new WaitForSeconds(duration);
            result = waitForSecondsDict[duration];
        }
        return result;
    }

    public static Vector2 Vec2(this Vector3 vec3)
    {
        return new Vector2(vec3.x, vec3.y);
    }

    public static Vector3 RotateAroundZ(this Vector3 vec3, float angleDegrees)
    {
        return Quaternion.AngleAxis(angleDegrees, Vector3.forward) * vec3;
    }

    public static Vector3 Vec3(this Vector2 vec2)
    {
        return new Vector3(vec2.x, vec2.y, 0f);
    }
}
