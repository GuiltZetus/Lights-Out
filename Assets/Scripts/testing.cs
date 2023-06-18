using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;

public class testing : MonoBehaviour
{
    private GridSystem grid;
    void Start()
    {
        grid = new GridSystem(4, 2, 10f, new Vector3(20, 0));

    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            grid.setValue(UtilsClass.GetMouseWorldPosition(), 56);
        }
        if (Input.GetMouseButtonDown(1))
        {
            Debug.Log(grid.GetValue(UtilsClass.GetMouseWorldPosition()));
        }
    }
}
