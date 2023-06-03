using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : Singleton<T>
{
    private static T instance;
    public static T Instance { get { return instance; } }

    protected virtual void Awake()
    {
        if (instance != null && gameObject && gameObject != instance)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = (T)this;
        }

        if (!instance.transform.parent)
        {
            DontDestroyOnLoad(instance);
        }
    }
}
