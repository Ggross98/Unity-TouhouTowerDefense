using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Path
{

    List<Node> nodes;

    public int OnPath(int x, int y)
    {
        for(int i = 0; i < nodes.Count; i++)
        {
            if(nodes[i].x == x && nodes[i].y == y)
            {
                return i;
            }
        }

        return -1;
    }

    public int GetDirection(int index)
    {
        int x1 = nodes[index].x;
        int y1 = nodes[index].y;
        int x2 = nodes[index+1].x;
        int y2 = nodes[index+1].y;

        if (x1 == x2)
        {
            if (y1 < y2) return Utils.UP;
            else return Utils.DOWN;
        }
        else if (y1 == y2)
        {
            if (x1 < x2) return Utils.RIGHT;
            else return Utils.LEFT;
        }
        else return -1;
    }

    public Path(Node goal)
    {
        nodes = new List<Node>();
        Node now = goal;
        while (now != null)
        {
            nodes.Insert(0, now);
            now = now.parent;
        }
    }


}
