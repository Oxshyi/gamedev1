using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Testing : MonoBehaviour
{
    private Grid<bool> grid;
    void Start()
    {
        grid = new Grid<bool>(4, 2, 10f, new Vector3(20, 0, 0), () => new BoxCollider2D());
    }
    private void Update(){
        if (Input.GetMouseButtonDown(0)){   
        }
        if (Input.GetMouseButtonDown(1)){
            Debug.Log(grid.GetGridObject(Utils.GetMouseWorldPosition(gameObject.transform)));
        }
    }
}
