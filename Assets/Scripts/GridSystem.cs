using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;



public class GridSystem
{
    private int width;
    private int height;
    private float cellSize;
    private Vector3 originPosition;
    private int[,] gridArray;
    private TextMesh[,] debugTextArray;

    public GridSystem(int width, int height, float cellSize, Vector3 originPosition)
    {
        this.width = width;
        this.height = height;
        this.cellSize = cellSize;
        this.originPosition = originPosition;

        gridArray = new int[width, height];
        debugTextArray = new TextMesh[width, height];

        ///Draw the grid
        for (int x = 0; x < gridArray.GetLength(0); x++)
        {
            for (int y = 0; y < gridArray.GetLength(1); y++)
            {
                debugTextArray[x, y] = UtilsClass.CreateWorldText(gridArray[x, y].ToString(), null, GetWorldPosition(x, y)+ new Vector3(cellSize, cellSize)*0.5f, 30, Color.white, TextAnchor.MiddleCenter);
                Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x, y + 1), Color.white, 100f);
                Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x + 1, y), Color.white, 100f);
            }
            Debug.DrawLine(GetWorldPosition(0, height), GetWorldPosition(width, height), Color.white, 100f);
            Debug.DrawLine(GetWorldPosition(width, 0), GetWorldPosition(width, height), Color.white, 100f);

            
        }
        setValue(1, 1, 10);
    }

    private Vector3 GetWorldPosition(int x , int y)//get a position in the world using X and Y
    {
        return new Vector3(x, y) * cellSize + originPosition;
    }
    private void getXY(Vector3 worldPosition, out int x, out int y)//get the X and Y using postion in the world
    {
        x = Mathf.FloorToInt((worldPosition - originPosition).x / cellSize);
        y = Mathf.FloorToInt((worldPosition - originPosition).y / cellSize);

    }

    public void setValue(int x, int y, int value)//set a value to a grid cell based on the provide X and Y value
    {
        if (x >= 0 && y >= 0 && x < width && y < height)
        {
            gridArray[x, y] = value;
            debugTextArray[x, y].text = gridArray[x, y].ToString();
        }
    }
    public void setValue(Vector3 worldPosition, int value)//call the getXY() method to get the X and Y value then pass that X and Y value to setValue(int x, int y, int value)
    {
        int x, y;
        getXY(worldPosition, out x, out y);
        setValue(x, y, value);
    }

    public int GetValue(int x, int y)//get the value stored in grid cell using X and Y value
    {
        if (x >= 0 && y >= 0 && x < width && y < height)
        {
            return gridArray[x, y];
        }
        else
        {
            return 0;
        }
    }
    public int GetValue (Vector3 worldPosition)//get the X and Y value based on world position and pass them to GetValue(int x, int y)
    {
        int x, y;
        getXY(worldPosition, out x, out y);
        return GetValue(x, y);
    }
}
