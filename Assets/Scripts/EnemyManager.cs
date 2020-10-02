using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    #region Variables
    [SerializeField] CacheData[] cacheDatas;

    Dictionary<string, GameObject> fileCaches = new Dictionary<string, GameObject>();
    #endregion Variables

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

    public Enemy Generate(string filePath, Vector3 appearPos)
    {
        GameObject go = InGameSceneManager.instance.CacheSystem.Archive(filePath, appearPos);

        if (go != null)
        {
            Enemy enemy = go.GetComponent<Enemy>();
            enemy.FilePath = filePath;
            enemy.checkRemainEnemyHandler += InGameSceneManager.instance.SpawnManager.CheckRemainEnemy;

            return enemy;
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
