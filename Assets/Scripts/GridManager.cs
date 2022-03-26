using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GridManager : MonoBehaviour
{
    public BoardManager boardManager;
    public Sprite[] pieces;
    int width = 9;
    int height = 14;
    Node[,] board;

    System.Random random;

    void Start()
    {
        
    }
    void StartGame()
    {
        string seed = getRandomSeed();
        random = new System.Random(seed.GetHashCode());

        InitializeBoard();
    }

    void InitializeBoard()
    {
        board = new Node[width, height];

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                board[x, y] = new Node((boardManager.rows[y].row[x]) ? -1 : fillPiece(), new SlotBehaviour(x, y));
            }
        }
    }

    void VerifyBoard()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                SlotBehaviour p = new SlotBehaviour(x, y);
                int val = getValueAtPoint(p);
                if (val <= 0) continue;


            }
        }
    }

    List<SlotBehaviour> isConnected(SlotBehaviour p, bool main)
    {
        List<SlotBehaviour> connected = new List<SlotBehaviour>();
        int val = getValueAtPoint(p);
        SlotBehaviour[] directions =
        {
            SlotBehaviour.up,
            SlotBehaviour.right,
            SlotBehaviour.down,
            SlotBehaviour.left
        };



        foreach (SlotBehaviour dir in directions) //checks for 2 or more same skulls in the directions
        {
            List<SlotBehaviour> line = new List<SlotBehaviour>();

            int same = 0;
            for (int i = 1; i < 3; i++)
            {
                SlotBehaviour check = SlotBehaviour.add(p, SlotBehaviour.mult(dir, i));
                if (getValueAtPoint(check) == val)
                {
                    line.Add(check);
                    same++;
                }
            }

            if(same > 1) // if there are more than 1 of the same shape in the direction then its a match
            {
                AddPoints(ref connected, line); //adds the points to the overarching connected list
            }
        }

        for (int i = 0; i < 2; i++) //checking if we are in the middle of two of the same shapes
        {
            List<SlotBehaviour> line = new List<SlotBehaviour>();

            int same = 0;
            SlotBehaviour[] check = { SlotBehaviour.add(p, directions[i]), SlotBehaviour.add(p, directions[i + 2]) };
            
            foreach(SlotBehaviour next in check) //check both sides of the piece, adds them to the list if they are the same
            {
                if (getValueAtPoint(next) == val)
                {
                    line.Add(p);
                    same++;
                }
            }

            if (same > 1)
            {
                AddPoints(ref connected, line);
            }
        }
        for (int i = 0; i < 4; i++) //check for 2x2
        {
            List<SlotBehaviour> square = new List<SlotBehaviour>();

            int same = 0;
            int next = i + 1;
            if (next >= 4)
            {
                next -= 4;

                SlotBehaviour[] check = { SlotBehaviour.add(p, directions[i]), SlotBehaviour.add(p, directions[next]), SlotBehaviour.add(p, SlotBehaviour.add(directions[i], directions[next])};
                foreach (SlotBehaviour pnt in check) //check both sides of the piece, adds them to the list if they are the same
                {
                    if (getValueAtPoint(pnt) == val)
                    {
                        square.Add(p);
                        same++;
                    }
                }
                if (same > 2)
                {
                    AddPoints(ref connected, square);
                }
            }
            if (main)
            {

            }
        }
    }

    void AddPoints(ref List<SlotBehaviour> points, List<SlotBehaviour> add)
    {

    }

    int fillPiece()
    {
        int val = 1;
        val = (random.Next(0, 100) / (100 / pieces.Length)) + 1;
        return val;
    }

    int getValueAtPoint(SlotBehaviour p)
    {
        return board[p.x, p.y].value;
    }

    void Update()
    {
        
    }
    string getRandomSeed()
    {
        string seed = "";
        string acceptableChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz123456789!@#$%^&*()";
        
        for (int i = 0; i < 20; i++)
        {
            seed += acceptableChars[Random.Range(0, acceptableChars.Length)];
        }

        return seed;
    }
}

[System.Serializable]
public class Node
{
    public int value;
    public SlotBehaviour index;

    public Node(int v, SlotBehaviour i)
    {
        value = v;
        index = i;
    }
}