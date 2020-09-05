using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinder : MonoBehaviour
{
    private int[,] map;
    private Vector2Int startPoint, endPoint;

    private MapController mapController;
    private EnemyController ec;

    private int row, column;
    private Node[,] nodes;
    private List<Node> openList, closeList;

    private DrawToolkit drawer;

    public Path defaultPath;

    void Start()
    {
        mapController = GameObject.Find("Map").GetComponent<MapController>();
        ec = GameObject.Find("Map").GetComponent<EnemyController>();

        row = mapController.row;
        column = mapController.column;

        startPoint = new Vector2Int(mapController.CellToArray(ec.enemyStart).x, mapController.CellToArray(ec.enemyStart).y);
        endPoint = new Vector2Int(mapController.CellToArray(ec.home).x, mapController.CellToArray(ec.home).y);


        //drawer = GameObject.Find("DrawToolkit").GetComponent<DrawToolkit>();

        SetMap(mapController.mapArray, row, column);

        //Debug.Log(startPoint + " to " + endPoint);

        defaultPath = new Path(FindPath(startPoint, endPoint));

        //PrintPath(FindPath(startPoint, endPoint));

        //DrawEnemyPath();

        //GetPathDirection(startPoint, endPoint);
    }


    public void SetMap(int[,] m, int r, int c)
    {
        map = m;
        row = r;
        column = c;
        nodes = new Node[column, row];
        for(int i = 0; i < column; i++)
        {
            for(int j = 0; j < row; j++)
            {
                nodes[i, j] = new Node(i,j,map[i,j]);
            }
        }

        Debug.Log("map is initialized.");
        //Debug.Log(map);
    }

    private void RefreshMap()
    {
        //map = m;
        for (int i = 0; i < column; i++)
        {
            for (int j = 0; j < row; j++)
            {
                //int a = map[i, j];
                //nodes[i, j].SetWalkable(a);
                nodes[i, j].SetWalkable (map[i,j]);
                nodes[i, j].G = 0;
                nodes[i, j].H = 0;
                nodes[i, j].parent = null;
            }
        }
        //defaultPath = new Path(FindPath(startPoint, endPoint));


    }

    public void RefreshDefaultPath()
    {
        //Debug.Log("refresh default path");
        RefreshMap();
        defaultPath = new Path(FindPath(startPoint, endPoint));
    }

    /// <summary>
    /// 获得一个节点的周围节点
    /// </summary>
    /// <param name="node"></param>
    /// <returns></returns>
    private List<Node> GetAroundNodes(Node node)
    {
        List<Node> around = new List<Node>();
        int x = node.x;
        int y = node.y;
        if (x > 0)
        {
            around.Add(nodes[x - 1, y]);
        }
        if (x < column - 1)
        {
            around.Add(nodes[x + 1, y]);
        }
        if (y > 0)
        {
            around.Add(nodes[x, y-1]);
        }
        if (y < row - 1)
        {
            around.Add(nodes[x, y+1]);
        }
        return around;
    }

    
    /// <summary>
    /// 估计两点之间距离
    /// </summary>
    /// <param name="a"></param>
    /// <param name="b"></param>
    /// <returns></returns>
    private int GetDistance(Node a, Node b)
    {
        int r, c;
        c = Mathf.Abs(a.x - b.x);
        r = Mathf.Abs(a.y - b.y);

        return r + c;
    }

    private Node FindPath(Vector2Int start, Vector2Int goal)
    {
        //RefreshMap();
        //Debug.Log("the newest map is loaded.");

        openList = new List<Node>();
        closeList = new List<Node>();

        Node currentNode;

        openList.Add(nodes[start.x, start.y]);


        
        while(openList .Count > 0)
        {
            //Debug.Log("openlist count: " + openList.Count);
            currentNode = null;

            //取出openList中F最小的点，即第一个
            openList.Sort((x,y) => (x.F()).CompareTo (y.F()));
            currentNode = openList[0];

            //如果到达目标，结束寻路。
            if(currentNode .pos == goal)
            {
                //Debug.Log("reach the goal.");
                return currentNode ;
            }
            
            

            //将周围的可到达的节点加入openList
            List<Node> around = GetAroundNodes(currentNode);
            for(int i =0;i<around.Count; i++)
            {
                if (!around[i].walkable || closeList .Contains (around[i]))
                {
                    //Debug.Log(i+" can't move");
                    //around.RemoveAt(i);
                    continue;
                }

                //若此节点位于open，比较估价
                if(openList .Contains(around[i]))
                {
                    //Debug.Log(i + " is in openlist");
                    continue;
                }
                //若此节点不位于open，加入open
                else
                {
                    
                    //设置父节点
                    around[i].parent = currentNode;
                    //计算估价
                    around[i].G = currentNode.G + 1;
                    around[i].H = GetDistance(around[i], nodes[goal.x, goal.y]);
                    
                    openList.Add(around[i]);
                    //Debug.Log("should add to open");
                }
            }
            

            //当前节点从open删除加入close
            openList.Remove(currentNode);
            closeList.Add(currentNode);
        }

        //Debug.Log("no path is available");
        
        return null;
    }

    public bool CanPlaceTurret(Vector2Int pos)
    {
        RefreshMap ();
        nodes[pos.x, pos.y].walkable = false;

        Node n = FindPath(startPoint, endPoint);

        nodes[pos.x, pos.y].walkable = true;
        return n != null;

    }

    public Path FindPathToGoal(Vector2Int start)
    {
        RefreshMap();
        return new Path(FindPath(start, endPoint));
    }

    private void PrintPath(Node goal)
    {
        Node now, last;
        now = goal;
        last = now.parent;
        while(last.parent != null)
        {
            Debug.Log(now.pos);
            now = last;
            last = last.parent;

        }
        Debug.Log(now.pos);
    }

    private int GetPathDirection(Vector2Int start, Vector2Int next)
    {
        if(next.y == start.y)
        {
            if(start.x < next.x)
            {
                return Utils.RIGHT;
            }
            else
            {
                return Utils.LEFT;
            }
        }
        else
        {
            if (start.y < next.y)
            {
                return Utils.UP;
            }
            else
            {
                return Utils.DOWN;
            }
        }
        
    }

    public int EnemyNextDirection(Vector2Int pos)
    {



        
        Node now = FindPath(pos, endPoint );
        Node last = now.parent ;
        
        while(last.parent != null)
        {
            now = last;
            last = last.parent;
        }
        return GetPathDirection(last.pos, now.pos);


        //return Utils.RIGHT;
        //return GetPathDirection (pos, endPoint );
    }

    //public void DrawEnemyPath()
    //{

    //    Debug.Log("drawing enemy path...");
    //    RefreshMap(map);

    //    /*
    //    Node now = FindPath(new Vector2Int(1,1), new Vector2Int(8, 8));
    //    Node last = now.parent;
    //    while(last.parent != null)
    //    {
    //        Vector3 wPos_1 = mapController.ArrayToWorld(new Vector3Int(now.pos.x, now.pos.y,0));
    //        Vector3 wPos_2 = mapController.ArrayToWorld(new Vector3Int(last.pos.x, last.pos.y, 0));
    //        drawer.DrawLine(wPos_1, wPos_2);
    //        now = last;
    //        last = last.parent;
    //    }*/

    //    drawer.DrawLine(new Vector3(0, 0, 0), new Vector3(900, 900, 0));
    //}
}
