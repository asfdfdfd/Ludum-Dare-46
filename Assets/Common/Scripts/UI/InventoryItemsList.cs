using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryItemsList : MonoBehaviour
{
    public GameObject itemListPrefab;
        
    void Start()
    {
        for (int i = 0; i < 5; i++)
        {
            GameObject gameObjectListItem = Instantiate(itemListPrefab);
            gameObjectListItem.transform.parent = transform;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
