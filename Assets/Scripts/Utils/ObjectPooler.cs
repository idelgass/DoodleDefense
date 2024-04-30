using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

// TODO: Object pooler will need to be able to pool different types of enemies

public class ObjectPooler : MonoBehaviour
{
    [SerializeField] private GameObject prefab;
    [SerializeField] private int poolSize = 10;

    // Removed static
    private List<GameObject> pool;
    private GameObject poolContainer;

    

    private GameObject CreateInstance()
    {
        GameObject newInstance = Instantiate(prefab);
        // Makes all entities in pool collapsable in editor
        newInstance.transform.SetParent(poolContainer.transform);
        newInstance.SetActive(false);
        return newInstance;
    }

    private void CreatePool()
    {
        for(int i = 0; i < poolSize; i++)
        {
            pool.Add(CreateInstance());
        }
    }

    public GameObject GetInstFromPool()
    {
        foreach(GameObject obj in pool)
        {
            if(!obj.activeInHierarchy)
            {
                return obj;
            }
        }
        
        return CreateInstance();
    }

    // public static void ReturnToPool(GameObject instance)
    // {
    //     instance.SetActive(false);
    // }

    // public static IEnumerator ReturnToPoolDelay(GameObject instance, float delay)
    // {
    //     yield return new WaitForSeconds(delay);
    //     instance.SetActive(false);
    // }

    private void Awake()
    {
        pool = new List<GameObject>();
        poolContainer = new GameObject(name:$"Pool - {prefab.name}");
        CreatePool();
    }


}
