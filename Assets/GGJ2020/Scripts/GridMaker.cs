using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridMaker : MonoBehaviour
{
    public GameObject itemPrefab;
    public int width = 15;
    public int height = 15;


    // Start is called before the first frame update
    void Start()
    {
        var selector = GetComponentInParent<Selector>();
        selector.grid = new Cell[height][];
        for (int y = 0; y < height; y++)
        {
            selector.grid[y] = new Cell[width];
            for (int x = 0; x < width; x++)
            {
                var go = Instantiate(itemPrefab);
                go.transform.SetParent(transform);
                var cell = go.GetComponent<Cell>();
                cell.SetState(0);
                cell.x = x;
                cell.y = y;
                selector.grid[y][x] = cell;

                if (5 <= x && x <= 9 && 6 <= y && y <= 8){
                    cell.SetState(1);
                }

            }
        }


    }



}
