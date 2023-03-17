using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Singleton<T> : StaticInstance<T> where T : MonoBehaviour
{
    protected override void Awake()
    {
        if (Instance != null) Destroy(this.gameObject);
        base.Awake();
    }
}