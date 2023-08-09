using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenCursor : MonoBehaviour
{
    public static ScreenCursor instance;
    public string  s;
    private void Awake()
    {
        // Set hardware cursor off
        Cursor.visible = false;
        instance = this;
    }

    private void Update()
    {
        transform.position = Input.mousePosition;
    }

}
