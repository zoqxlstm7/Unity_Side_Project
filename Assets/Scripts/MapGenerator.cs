using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    #region Variables
    [SerializeField] Transform tilePrefab = null;
    [SerializeField] Vector2 mapSize = Vector2.zero;

    [Range(0, 1)]
    [SerializeField] float outlinePercent = 0.0f;
    #endregion Variables

    private void Start()
    {
        GenerateMap();
    }

    public void GenerateMap()
    {
        string holderName = "Generated Map";
        if (transform.Find(holderName))
        {
            DestroyImmediate(transform.Find(holderName).gameObject);
        }

        Transform mapHolder = new GameObject(holderName).transform;
        mapHolder.transform.SetParent(transform);

        for (int x = 0; x < mapSize.x; x++)
        {
            for (int y = 0; y < mapSize.y; y++)
            {
                Vector3 tilePosition = new Vector3(-mapSize.x / 2 + 0.5f + x, 0.0f, -mapSize.y / 2 + 0.5f + y);
                Transform newTile = Instantiate(tilePrefab, tilePosition, Quaternion.Euler(Vector3.right * 90.0f)) as Transform;
                newTile.localScale = Vector3.one * (1 - outlinePercent);
                newTile.SetParent(mapHolder);
            }
        }
    }
}
