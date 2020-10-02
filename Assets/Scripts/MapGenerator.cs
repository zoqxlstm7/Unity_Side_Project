using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    #region Variables
    [SerializeField] Transform tilePrefab = null;
    [SerializeField] Transform maskPrefab = null;
    [SerializeField] Vector2 mapSize = Vector2.zero;

    [Range(0, 1)]
    [SerializeField] float outlinePercent = 0.0f;

    [SerializeField] float maskOutlineLength = 2.0f;
    [SerializeField] float maskHeight = 2.0f;
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

        SetColliderMask(mapHolder);
    }

    void SetColliderMask(Transform mapHolder)
    {
        Transform leftMask = Instantiate(maskPrefab, Vector3.left * (mapSize.x + maskOutlineLength) / 2, Quaternion.identity) as Transform;
        leftMask.SetParent(mapHolder);
        leftMask.localScale = new Vector3(maskOutlineLength, maskHeight, mapSize.y);

        Transform rightMask = Instantiate(maskPrefab, Vector3.right * (mapSize.x + maskOutlineLength) / 2, Quaternion.identity) as Transform;
        rightMask.SetParent(mapHolder);
        rightMask.localScale = new Vector3(maskOutlineLength, maskHeight, mapSize.y);

        Transform topMask = Instantiate(maskPrefab, Vector3.forward * (mapSize.y + maskOutlineLength) / 2, Quaternion.identity) as Transform;
        topMask.SetParent(mapHolder);
        topMask.localScale = new Vector3(mapSize.x + maskOutlineLength * 2, maskHeight, maskOutlineLength);

        Transform bottomMask = Instantiate(maskPrefab, Vector3.back * (mapSize.y + maskOutlineLength) / 2, Quaternion.identity) as Transform;
        bottomMask.SetParent(mapHolder);
        bottomMask.localScale = new Vector3(mapSize.x + maskOutlineLength * 2, maskHeight, maskOutlineLength);
    }
}
