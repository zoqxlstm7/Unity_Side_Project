using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CacheData
{
    public string filePath;
    public int cacheCount;
}

public class CacheSystem
{
    #region Variables
    public const int DEFAUT_CACHE_COUNT = 10;

    Dictionary<string, Queue<GameObject>> caches = new Dictionary<string, Queue<GameObject>>();
    #endregion Variables

    #region Other Methods
    public void Generate(string filePath, GameObject gameObject, int cacheCount, Transform parentTransform = null)
    {
        if (!caches.ContainsKey(filePath))
        {
            Queue<GameObject> queue = new Queue<GameObject>();

            for (int i = 0; i < cacheCount; i++)
            {
                GameObject go = Object.Instantiate(gameObject, Vector3.zero, Quaternion.identity, parentTransform);
                queue.Enqueue(go);
                go.SetActive(false);
            }

            caches.Add(filePath, queue);
        }
        else
        {
            for (int i = 0; i < cacheCount; i++)
            {
                GameObject go = Object.Instantiate(gameObject, Vector3.zero, Quaternion.identity, parentTransform);
                caches[filePath].Enqueue(go);
                go.SetActive(false);
            }
        }
    }

    public GameObject Archive(string filePath, Vector3 appearPos)
    {
        GameObject go = null;

        if (caches[filePath].Count == 0)
        {
#if UNITY_EDITOR
            Debug.Log("Archive not remain. filePath: " + filePath);
#endif
            return null;
        }

        if (caches.ContainsKey(filePath))
        {
            go = caches[filePath].Dequeue();
            go.transform.position = appearPos;
            go.SetActive(true);

            return go;
        }

        return go;
    }

    public bool Restore(string filePath, GameObject gameObject)
    {
        if (caches.ContainsKey(filePath))
        {
            caches[filePath].Enqueue(gameObject);
            gameObject.SetActive(false);

            return true;
        }

        return false;
    }
    #endregion Other Methods
}
