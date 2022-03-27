using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    [SerializeField]
    public int sizeX;
    public int sizeY;
    bool CanMove = false;
    bool fast = true;

    [SerializeField]
    Skulls[] skullsPrefabs;
    Skulls[,] grid;


    void Start()
    {
        grid = new Skulls[sizeX, sizeY * 2];
        for (int i = 0; i < sizeX; i++)
        {
            for (int j = 0; j < sizeY; j++)
            {
                InstantiateTile(i, j);
            }
        }
        Check();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            string s = "";
            for (int j = sizeY * 2 - 1; j >= 0; j--)
            {
                for (int i = sizeX - 1; i >= 0; i--)
                {
                    if (grid[i, j] != null)
                        s += grid[i, j].name;
                    else
                        s += "NULL";
                }
                s += "\n";
            }
            print(s);
        }
    }

    int moveX = -1;
    int moveY = -1;
    public void Move(Skulls skull)
    {
        if (!CanMove)
            return;
        moveX = skull.x;
        moveY = skull.y;
    }
    public void Drop(Skulls skull)
    {
        if (!CanMove)
            return;

        if (moveX == -1 || moveY == -1)
            return;

        SwapSkulls(moveX, moveY, skull.x, skull.y);

        moveX = -1;
        moveY = -1;
    }
    void SwapSkulls(int x1, int y1, int x2, int y2)
    {
        fast = false;
        if (x1 == x2 && y1 == y2)
            return;
        if (x1 == -1 || y1 == -1)
            return;
        if (Mathf.Abs(x1 - x2) > 1)
            return;
        if (Mathf.Abs(y1 - y2) > 1)
            return;
        if (Mathf.Abs(y1 - y2) + Mathf.Abs(x1 - x2) > 1)
            return;
        MoveTile(x1, y1, x2, y2);

        List<Skulls> CheckSkull = CheckHorizontalMatches();
        CheckSkull.AddRange(CheckVerticalMatches());

        if (CheckSkull.Count == 0)
        {
            MoveTile(x1, y1, x2, y2);
        }
        Check();
    }
    void Check()
    {
        List<Skulls> DestroySkull = CheckHorizontalMatches();
        DestroySkull.AddRange(CheckVerticalMatches());

        DestroySkull = DestroySkull.Distinct().ToList();

        bool sw = DestroySkull.Count == 0;

        for (int i = 0; i < DestroySkull.Count; i++)
        {
            if (DestroySkull[i] != null)
            {
                Destroy(DestroySkull[i].gameObject);
                InstantiateTile(DestroySkull[i].x, DestroySkull[i].y + sizeY);
            }
        }

        if (!sw)
            StartCoroutine(Gravity());
    }
    IEnumerator Gravity()
    {
        bool Sw = true;
        while (Sw)
        {
            CanMove = false;
            Sw = false;
            for (int j = 0; j < sizeY * 2; j++)
            {
                for (int i = 0; i < sizeX; i++)
                {
                    if (Fall(i, j))
                    {
                        Sw = true;
                    }
                }

                if (j <= sizeY && !fast)
                {
                    yield return null;

                }
            }
        }
        yield return null;
        CanMove = true;
        Check();

    }
    bool Fall(int x, int y)
    {
        if (x < 0 || y <= 0 || x >= sizeX || y >= sizeY * 2)
            return false;
        if (grid[x, y] == null)
            return false;
        if (grid[x, y - 1] != null)
            return false;

        MoveTile(x, y, x, y - 1);
        return true;
    }
    List<Skulls> CheckHorizontalMatches()
    {
        List<Skulls> CheckSkull = new List<Skulls>();
        List<Skulls> ReturnSkull = new List<Skulls>();
        string Type = "";

        for (int j = 0; j < sizeY; j++)
        {
            for (int i = 0; i < sizeX; i++)
            {
                if (grid[i, j].type != Type)
                {
                    if (CheckSkull.Count >= 3)
                    {
                        ReturnSkull.AddRange(CheckSkull);
                    }
                    CheckSkull.Clear();
                }
                Type = grid[i, j].type;
                CheckSkull.Add(grid[i, j]);
            }

            if (CheckSkull.Count >= 3)
            {
                ReturnSkull.AddRange(CheckSkull);
            }
            CheckSkull.Clear();
        }
        return ReturnSkull;
    }
    List<Skulls> CheckVerticalMatches()
    {
        List<Skulls> CheckSkull = new List<Skulls>();
        List<Skulls> ReturnSkull = new List<Skulls>();
        string Type = "";

        for (int i = 0; i < sizeX; i++)
        {
            for (int j = 0; j < sizeY; j++)
            {
                if (grid[i, j].type != Type)
                {
                    if (CheckSkull.Count >= 3)
                    {
                        ReturnSkull.AddRange(CheckSkull);
                    }
                    CheckSkull.Clear();
                }
                Type = grid[i, j].type;
                CheckSkull.Add(grid[i, j]);
            }

            if (CheckSkull.Count >= 3)
            {
                ReturnSkull.AddRange(CheckSkull);
            }
            CheckSkull.Clear();
        }
        return ReturnSkull;
    }
    void MoveTile(int x1, int y1, int x2, int y2)
    {
        if (grid[x1, y1] != null)
            grid[x1, y1].transform.position = new Vector3(x2, y2);


        if (grid[x2, y2] != null)
            grid[x2, y2].transform.position = new Vector3(x1, y1);


        Skulls temp = grid[x1, y1];
        grid[x1, y1] = grid[x2, y2];
        grid[x2, y2] = temp;

        if (grid[x1, y1] != null)
            grid[x1, y1].ChangePosition(x1, y1);
        if (grid[x2, y2] != null)
            grid[x2, y2].ChangePosition(x2, y2);

    }
    void InstantiateTile(int x, int y)
    {
        Skulls go = Instantiate
        (
             skullsPrefabs[Random.Range(0, skullsPrefabs.Length)], new Vector3(x, y), Quaternion.identity, transform
        ) as Skulls;

        go.Constructor(this, x, y);
        grid[x, y] = go;
    }
}