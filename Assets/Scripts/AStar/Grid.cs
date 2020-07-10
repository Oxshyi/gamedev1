using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Grid<TGridObject> : MonoBehaviour
{ 
    private int width;
    private int height;
    private float cellSize;
    Vector3 originPosition;
    private TGridObject[,] gridArray;
    private TextMesh[,] debugTextArray;
    
    public Grid(int width, int height, float cellSize, Vector3 originPosition, Func<TGridObject> createGridObject){
        this.width = width;
        this.height = height;
        this.cellSize = cellSize;
        this.originPosition = originPosition;

        this.gridArray = new TGridObject[width, height];
        this.debugTextArray = new TextMesh[width, height];
        bool showDebug = true;

        for (int x = 0; x < gridArray.GetLength(0); x++){
            for (int y = 0; y < gridArray.GetLength(1); y++){
               gridArray[x, y] = createGridObject();
            }
        }
        if (showDebug){
            for (int x = 0; x < gridArray.GetLength(0); x++){
                for (int y = 0; y < gridArray.GetLength(1); y++){
                    debugTextArray[x, y] = Utils.CreateWorldText(gridArray[x, y].ToString(), null, GetWorldPostion(x, y) + new Vector3(cellSize, cellSize) * 0.5f, 20, Color.white, TextAnchor.MiddleCenter);
                    Debug.DrawLine(GetWorldPostion(x, y), GetWorldPostion(x, y + 1), Color.white, 100f);
                    Debug.DrawLine(GetWorldPostion(x, y), GetWorldPostion(x + 1, y), Color.white, 100f);
                }
            }
            Debug.DrawLine(GetWorldPostion(0, height), GetWorldPostion(width, height), Color.white, 100f);
            Debug.DrawLine(GetWorldPostion(width, 0), GetWorldPostion(width, height), Color.white, 100f);
        }
    }
    private Vector3 GetWorldPostion(int x, int y){
        return new Vector3(x, y) * cellSize + originPosition;
    }
    private void GetXY(Vector3 worldPosition, out int x, out int y){
        Debug.Log(worldPosition);
        x = Mathf.FloorToInt((worldPosition - originPosition).x / cellSize);
        y = Mathf.FloorToInt((worldPosition - originPosition).y / cellSize);
    }
    public void SetGridObject(int x, int y, TGridObject value){
        if (x >= 0 && y >= 0 && x < width && y < height)
            gridArray[x, y] = value;
            debugTextArray[x, y].text = gridArray[x, y].ToString();
    }
    public void SetGridObject(Vector3 worldPosition, TGridObject value){
        int x, y;
        GetXY(worldPosition, out x, out y);
        SetGridObject(x, y, value);
    }
    public TGridObject GetGridObject(int x, int y){
        if (x >= 0 && y >= 0 && x < width && y < height){
            return gridArray[x , y];
        }else{
            return default(TGridObject);
        }
    }
    public TGridObject GetGridObject(Vector3 worldPosition){
        int x, y;
        GetXY(worldPosition, out x, out y);
        return GetGridObject(x , y);
    }
}
