using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingletonMonoBehavior<T> : MonoBehaviour where T : Component
{
    private static T instance;
    public static T Instance
    {
        get
        {
            if(instance == null)
            {
                if((instance = FindObjectOfType<T>()) == null)
                {
                    GameObject newInstance = new GameObject();
                    instance = newInstance.AddComponent<T>();
                }
            } 
            return instance;
        }
    }
}
