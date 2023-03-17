using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SingletonPersistent<T> : Singleton<T> where T : MonoBehaviour
{
    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(this.gameObject);
    }
}