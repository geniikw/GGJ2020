using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System.Linq;

public class Selector : MonoBehaviour
{
    public Vector2Int current;

    public Cell[][] grid;

    public int currentRotation;

    public bool isVertical = true;

    public int currentSelectActor = -1;

    [PunRPC]
    public void SetXY(Vector2Int position)
    {

        if (current.x == position.x && current.y == position.y)
            return;
        current = position;

        var phoneSize = GameData.ins.phoneSize;

        if (isVertical ? ValidationVertical(position, phoneSize) : ValidationHorizontal(position, phoneSize))
        {
            if (isVertical)
                ColoringVertical(position, phoneSize);
            else
                ColoringHorizontal(position, phoneSize);
        }
    }

    public bool ValidationHorizontal(Vector2Int point, Vector2Int size)
    {

        for (int y = point.y; y > point.y - size.x; y--)
        {
            for (int x = point.x; x < point.x + size.y; x++)
            {
                if (y < 0 || x < 0)
                {
                    return false;
                }
                if (y >= grid.Length || x >= grid[0].Length)
                {
                    return false;
                }

                if (grid[y][x].isConfirm)
                {
                    return false;
                }
            }
        }
        return true;
    }
    public bool ValidationVertical(Vector2Int point, Vector2Int size)
    {
        for (int y = point.y; y < point.y + size.y; y++)
        {
            for (int x = point.x; x < point.x + size.x; x++)
            {
                if (y < 0 || x < 0)
                {
                    return false;
                }
                if (y >= grid.Length || x >= grid[0].Length)
                {
                    return false;
                }

                if (grid[y][x].isConfirm)
                {

                    return false;
                }
            }
        }
        return true;
    }


    public void ColoringVertical(Vector2Int point, Vector2Int size)
    {
        bool isFirst = true;
        for (int y = 0; y < grid.Length; y++)
        {
            for (int x = 0; x < grid[0].Length; x++)
            {
                if (grid[y][x].state == 1 || grid[y][x].isConfirm)
                    continue;

                if (point.y <= y && y < point.y + size.y && point.x <= x && x < point.x + size.x)
                {
                    grid[y][x].SetState(isFirst ? 3 : 2);
                    isFirst = false;
                }
                else
                    grid[y][x].SetState(0);
            }
        }
    }

    public void ColoringHorizontal(Vector2Int point, Vector2Int size)
    {
        for (int y = 0; y < grid.Length; y++)
        {
            for (int x = 0; x < grid[0].Length; x++)
            {
                if (grid[y][x].state == 1 || grid[y][x].isConfirm)
                    continue;

                if (point.y >= y && y > point.y - size.x && point.x <= x && x < point.x + size.y)
                {
                    grid[y][x].SetState(2);
                    if(y==point.y && x == point.x)
                         grid[y][x].SetState(3);
                }
                else
                    grid[y][x].SetState(0);
            }
        }
    }

    public void Rotate()
    {
        isVertical = !isVertical;
        if (!isVertical)
            current = new Vector2Int(0, 2);
        else
            current = Vector2Int.zero;

        var phoneSize = GameData.ins.phoneSize;



        if (isVertical)
            ColoringVertical(current, phoneSize);
        else
            ColoringHorizontal(current, phoneSize);
        Debug.Log("rotate");


    }


}
