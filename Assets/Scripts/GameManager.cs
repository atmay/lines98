using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int size = 3;
    public float stepX;
    public float stepY;

    public Cell cellPrefab;
    public Cell[,] grid;
    public Cell selected;

    public Ball ballPrefab;

    public List<Cell> emptyCells = new List<Cell>();

    public Color[] colors;

    void Start()
    {
        grid = new Cell[size, size];
        float centerX = size * stepX / 2f - 0.5f;
        float centerY = size * stepY / 2f - 0.5f;

        for (int y = 0; y < size; y++)
        {
            for (int x = 0; x < size; x++)
            {
                var cell = Instantiate(cellPrefab, transform);
                grid[x, y] = cell;
                cell.transform.position = new Vector3(
                    x * stepX - centerX,
                    y * stepY - centerY,
                    0);
                cell.name = $"cell {x}, {y}";
                cell.x = x;
                cell.y = y;
                emptyCells.Add(cell);
                // подписаться на событие
                cell.Click += HandleClick;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.N))
        {
            SpawnRandom();
            SpawnRandom();
            SpawnRandom();
        }
    }

    void HandleClick(int x, int y)
    {
        Debug.Log($"You pressed {x},{y}");
        Cell newSelect = grid[x, y];

        if (newSelect.ball != null)
        {
            if (selected)
                selected.SetSelected(false);
            selected = newSelect;
            selected.SetSelected(true);

            // После выбора клетки вызвать FindEmptyNeighbours
            // Напечатать список соседей и подсветить их, SetSelected(true)
        }

        else
        {
            if (selected)
            {
                var path = FindPath(selected, newSelect);
                if (path != null)
                {
                    FollowPath(path, selected.ball);
                }
                else
                {
                    selected.SetSelected(false);
                    selected = null;
                }
            }
        }
    }

    List<Cell> FindPath(Cell start, Cell end)
    {
        return null;
    }

    IEnumerator FollowPath(List<Cell> path, Ball ball)
    {
        yield return null;
    }

    void SpawnRandom()
    {
        if (emptyCells.Count > 0)
        {
            int idx = Random.Range(0, emptyCells.Count);
            Cell curCell = emptyCells[idx];
            emptyCells.RemoveAt(idx);

            Ball newBall = Instantiate(ballPrefab);
            newBall.transform.SetParent(curCell.anchor, false);
            curCell.ball = newBall;

            idx = Random.Range(0, colors.Length);
            newBall.renderer.material.color = colors[idx];
        }
    }

    List<Cell> FindEmptyNeighbours(Cell cell)
    {
        List<Cell> neighbours = new List<Cell>();

        for (int y = cell.y - 1; y <= cell.y + 1; y++)
        {
            if (!CheckValidCell(y))
                continue;
            for (int x = cell.x - 1; x <= cell.x + 1; x++)
            {
                if (!CheckValidCell(x))
                    continue;

                if (y == cell.y && x == cell.x)
                    continue;

                // Взять нужную клетку из грида
                // Проверить есть ли в ней шарик
                // И если нет - добавить в список соседей
            }
        }

        return neighbours;
    }

    float Distance(Cell a, Cell b)
    {
        return Vector3.Distance(a.transform.position, b.transform.position);
    }

    bool CheckValidCell(int c)
    {
        return c >= 0 && c < size;
    }
}
