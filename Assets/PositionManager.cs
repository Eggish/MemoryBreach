using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionManager : MonoBehaviour
{
    [SerializeField] private GameObject[] PositionMarkers;
    private Vector2Int SelectorPos;

    private const int GridWidth = 3;
    private const int GridHeight = 3;
    void Start()
    {
        SelectorPos = new Vector2Int(1, 1);
    }

    void Update()
    {
        CheckInput();
    }

    private void CheckInput()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            MoveSelector(Direction.RIGHT);
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            MoveSelector(Direction.LEFT);
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            MoveSelector(Direction.DOWN);
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            MoveSelector(Direction.UP);
        }
    }

    public void MoveSelector(Direction pDirection)
    {
        GameObject newTile;
        switch (pDirection)
        {
            case Direction.UP:
                newTile = GetTile(SelectorPos.x, SelectorPos.y - 1);

                if (newTile != null)
                {
                    newTile.SetActive(true);
                    GetTile(SelectorPos.x, SelectorPos.y).SetActive(false);
                    SelectorPos.y--;
                }
                break;
            case Direction.RIGHT:
                newTile = GetTile(SelectorPos.x + 1, SelectorPos.y);

                if (newTile != null)
                {
                    newTile.SetActive(true);
                    GetTile(SelectorPos.x, SelectorPos.y).SetActive(false);
                    SelectorPos.x++;
                }
                break;
            case Direction.DOWN:
                newTile = GetTile(SelectorPos.x, SelectorPos.y + 1);

                if (newTile != null)
                {
                    newTile.SetActive(true);
                    GetTile(SelectorPos.x, SelectorPos.y).SetActive(false);
                    SelectorPos.y++;
                }
                break;
            case Direction.LEFT:
                newTile = GetTile(SelectorPos.x - 1, SelectorPos.y);

                if (newTile != null)
                {
                    newTile.SetActive(true);
                    GetTile(SelectorPos.x, SelectorPos.y).SetActive(false);
                    SelectorPos.x--;
                }
                break;
            default:
                break;
        }
    }

    private GameObject GetTile(int x, int y)
    {
        if ((y * GridWidth + x) < 0
            || (y * GridWidth + x) > PositionMarkers.Length - 1)
            return null; //Outside array
        if (x > GridWidth - 1
            || y > GridHeight - 1
            || x < 0
            || y < 0)
            return null; //Edges
        return PositionMarkers[y * GridWidth + x];
    }
}
