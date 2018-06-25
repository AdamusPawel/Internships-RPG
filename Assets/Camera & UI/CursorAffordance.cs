﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(CameraRaycaster))]
public class CursorAffordance : MonoBehaviour
{
    [SerializeField] Texture2D walkCursor = null;
    [SerializeField] Texture2D targetCursor = null;
    [SerializeField] Texture2D unnamedCursor = null;
    [SerializeField] Vector2 cursorHotspot = new Vector2(0, 0);


    // TODO solve the fight between serialize and const
    [SerializeField] const int walkableLayerNumber = 8;
    [SerializeField] const int enemyLayerNumber = 9;


    CameraRaycaster cameraRaycaster;

    // Use this for initialization
    void Start()
    {
        cameraRaycaster = GetComponent<CameraRaycaster>();
        cameraRaycaster.notifyLayerChangeObservers += OnLayerChanged; // registering
    }

    void OnLayerChanged(int newLayer) // only called when layer changes
    {
        //print("CursorAffordances delegate reporting for duty!");
        print("Cursor over new layer!");
        switch (newLayer)
        {
            case walkableLayerNumber:
                Cursor.SetCursor(walkCursor, cursorHotspot, CursorMode.Auto);
                break;
            case enemyLayerNumber:
                Cursor.SetCursor(targetCursor, cursorHotspot, CursorMode.Auto);
                break;
            default:
                Cursor.SetCursor(unnamedCursor, cursorHotspot, CursorMode.Auto);
                return;
        }
        
    }
}
