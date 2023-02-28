using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Helpers
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
}
