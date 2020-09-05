using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node
{
    public Vector2Int pos;

    public bool walkable= true;

    public int x, y;

    /// <summary>
    /// G:起始点到此点的真实距离
    /// H:此点到目标点的估计距离
    /// </summary>
    public int H, G;

    public Node parent;

    public Node(int x, int y, int type)
    {
        this.x = x;
        this.y = y;
        pos = new Vector2Int(x, y);
        if (type == Utils.GROUND || type == Utils.ROAD) walkable = true;
        else walkable = false;
    }

    public void SetWalkable(int type)
    {
        if (type == Utils.GROUND || type == Utils.ROAD) walkable = true;
        else walkable = false;
    }

    public int F()
    {
        return H + G;
    }
}
