using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SlotBehaviour
{
    public int x;
    public int y;

    public SlotBehaviour(int nx, int ny)
    {
        x = nx;
        y = ny;
    }

    public void mult(int m)
    {
        x *= m;
        y *= m;
    }
    public void add(SlotBehaviour p)
    {
        x += p.x;
        y += p.y;
    }
    public Vector2 ToVector()
    {
        return new Vector2(x, y);
    }
    public bool Equals(SlotBehaviour p)
    {
        return (x == p.x && y == p.y);
    }
    public static SlotBehaviour fromVector(Vector2 v)
    {
        return new SlotBehaviour((int)v.x, (int)v.y);
    }
    public static SlotBehaviour fromVector(Vector3 v)
    {
        return new SlotBehaviour((int)v.x, (int)v.y);
    }
    public static SlotBehaviour mult(SlotBehaviour p, int m)
    {
        return new SlotBehaviour(p.x * m, p.y * m);
    }
    public static SlotBehaviour add(SlotBehaviour p, SlotBehaviour o)
    {
        return new SlotBehaviour(p.x + o.x, p.y + o.y);
    }
    public static SlotBehaviour clone(SlotBehaviour p)
    {
        return new SlotBehaviour(p.x, p.y);
    }

    public static SlotBehaviour zero
    {
        get { return new SlotBehaviour(0, 0); }
    }
    public static SlotBehaviour one
    {
        get { return new SlotBehaviour(1, 1); }
    }
    public static SlotBehaviour up
    {
        get { return new SlotBehaviour(0, 1); }
    }
    public static SlotBehaviour down
    {
        get { return new SlotBehaviour(0, -1); }
    }
    public static SlotBehaviour right
    {
        get { return new SlotBehaviour(1, 0); }
    }
    public static SlotBehaviour left
    {
        get { return new SlotBehaviour(-1, 0); }
    }
}