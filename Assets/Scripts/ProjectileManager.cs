using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileManager : MonoBehaviour
{
    #region Variablse
    [SerializeField] CacheData[] cacheDatas;

    Dictionary<string, GameObject> fileCaches = new Dictionary<string, GameObject>();
    #endregion Variablse

    #region Unity Methods
    private void Start()
    {
        PrepareCache();
    }
    #endregion Unity Methods

    #region Other Methods
    void PrepareCache()
    {
        for (int i = 0; i < cacheDatas.Length; i++)
        {
            GameObject go = Load(cacheDatas[i].filePath);
            if (go != null)
                InGameSceneManager.instance.CacheSystem.Generate(cacheDatas[i].filePath, go, cacheDatas[i].cacheCount, transform);
        }
    }

    GameObject Load(string filePath)
    {
        GameObject go = null;

        if (!fileCaches.ContainsKey(filePath))
        {
            go = Resources.Load<GameObject>(filePath);
            if (go == null)
            {
                Debug.Log("Load Error! filepath: " + filePath);
                return null;
            }

            fileCaches.Add(filePath, go);
        }
        else
        {
            go = fileCaches[filePath];
        }

        return go;
    }

    public Projectile Generate(string filePath, Vector3 appearPos)
    {
        GameObject go = InGameSceneManager.instance.CacheSystem.Archive(filePath, appearPos);

        if (go != null)
        {
            Projectile projectile = go.GetComponent<Projectile>();
            projectile.FilePath = filePath;

            return projectile;
        }
        else
        {
            InGameSceneManager.instance.CacheSystem.Generate(filePath, Load(filePath), CacheSystem.DEFAUT_CACHE_COUNT, transform);
            return Generate(filePath, appearPos);
        }
    }

    public void Remove(string filePath, GameObject gameObject)
    {
        InGameSceneManager.instance.CacheSystem.Restore(filePath, gameObject);
    }
    #endregion Other Methods
}
