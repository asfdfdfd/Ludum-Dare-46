using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapChest : MonoBehaviour
{
    public bool isOpened = false;

    private GameObject _gameObjectOpenedTop;
    private GameObject _gameObjectOpenedBottom;
    
    private GameObject _gameObjectClosedTop;
    private GameObject _gameObjectClosedBottom;
    
    private void Awake()
    {
        _gameObjectOpenedTop = GameObject.Find("Opened_Top");
        _gameObjectOpenedBottom = GameObject.Find("Opened_Bottom");
        _gameObjectClosedTop = GameObject.Find("Closed_Top");
        _gameObjectClosedBottom = GameObject.Find("Closed_Bottom");
        
        SetupChestPartsVisibility();
    }

    private void SetupChestPartsVisibility()
    {
        _gameObjectOpenedTop.SetActive(isOpened);
        _gameObjectOpenedBottom.SetActive(isOpened);
        _gameObjectClosedTop.SetActive(!isOpened);
        _gameObjectClosedBottom.SetActive(!isOpened);
    }

    public void Open()
    {
        isOpened = true;
    }

    private void Update()
    {
        SetupChestPartsVisibility();
    }
}
