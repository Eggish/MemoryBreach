using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;

public class PositionManager : MonoBehaviour
{
    [SerializeField] private GameObject[] PositionMarkers = null;
    [SerializeField] private CanonManager CanonManager = null;
    [SerializeField] private SoundManager SoundManager = null;
    private Vector2Int SelectorPos;

    public static Vector3 PlayerPos; 

    private const int GridWidth = 3;
    private const int GridHeight = 3;
    void Start()
    {
        SelectorPos = new Vector2Int(1, 1);
        PlayerPos = GetTile(SelectorPos.x, SelectorPos.y).transform.position;
    }

    void Update()
    {
        if (GameManager.GameIsOver)
            return;
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

    private void MoveSelector(Direction pDirection)
    {
        GameObject newTile = null;
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

        if (newTile != null)
        {
            List<Bullet> bullets = CanonManager.GetBullets();
            foreach (Bullet b in bullets)
            {
                if (Vector3.Distance(PlayerPos, b.Position) +
                    Vector3.Distance(newTile.transform.position, b.Position) -
                    Vector3.Distance(PlayerPos, newTile.transform.position) < 0.1f)
                {
                    SoundManager.PlayGameOver();
                    GameManager.GameOver();
                }
            }
            PlayerPos = newTile.transform.position;
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
