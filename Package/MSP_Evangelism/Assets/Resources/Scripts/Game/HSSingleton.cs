using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HSSingleton<T> : MonoBehaviour where T : MonoBehaviour
{
    protected static T instance = null;
    public static T I
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType(typeof(T)) as T;

                if (instance == null)
                {
                    Debug.Log("Nothing" + instance.ToString());
                    return null;
                }
            }
            return instance;
        }
    }
}
