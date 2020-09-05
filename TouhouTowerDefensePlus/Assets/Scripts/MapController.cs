using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

/*
    管理地图的类。
    1.读取瓦片地图，生成二维数组，并将世界坐标转换成这二者坐标
    2.待补充
 */
public class MapController : MonoBehaviour
{
    //瓦片网格
    //[SerializeField]
    private Grid grid;
    public GameObject grid_01, grid_02, grid_03, grid_04;

    //瓦片地图
    //[SerializeField]
    private Tilemap background, road, barrier, ornament; 

    //地图左下角、右上角位置标记
    public GameObject mark_start, mark_end;
    //瓦片网格的左下角、右上角坐标
    private int startX, startY, endX, endY;
    private Vector3Int position_start, position_end;

    //瓦片的尺寸
    public float cellLocalSize, cellWorldSize;

    //地图的行列数
    public int row, column;

    //地图数组
    //public static int GROUND = 0, ROAD = 1, BARRIER = -1, TURRET = 2;
    public int[,] mapArray;



    public int[,] GetMapArray()
    {
        if (mapArray != null) return mapArray;

        return null;
    }

    //读取Editor中绘制的瓦片地图，生成数组，标记位置
    private void LoadTilemap() {
     
        position_start = grid.WorldToCell(mark_start.transform.position);
        position_end = grid.WorldToCell(mark_end.transform.position);

        //Debug.Log(position_start + ", " + position_end);

        startX = position_start.x;
        startY = position_start.y;
        endX = position_end.x;
        endY = position_end.y;

        row = endY - startY + 1;
        column = endX - startX + 1;

        mapArray = new int[column, row];

        int i, j;
        for (i = 0; i <= endX - startX; i++)
        {
            for (j = 0; j <= endY - startY; j++)
            {
                int tile = -1;

                if (background.GetTile(new Vector3Int(startX + i, startY + j, 0)) != null) tile = Utils.GROUND ;
                if (road.GetTile(new Vector3Int(startX + i, startY + j, 0)) != null) tile = Utils.ROAD ;
                if (barrier.GetTile(new Vector3Int(startX + i, startY + j, 0)) != null) tile = Utils.BARRIER ;


                mapArray[i, j] = tile;
            }
        }

        //Debug.Log("mapArray: " + mapArray);

        //cellSize = grid.CellToWorld(position_end).x - grid.CellToWorld(new Vector3Int (position_end .x -1, position_end .y,0)).x;

        Vector3 p1 = ArrayToWorld(new Vector3Int(0, 0, 0));
        Vector3 p2 = ArrayToWorld(new Vector3Int(1, 0, 0));

        cellLocalSize = Camera.main.WorldToScreenPoint(p2).x- Camera .main.WorldToScreenPoint (p1).x;


        //float x2 = ArrayToWorld(new Vector3Int(1, 0, 0)).x;
        //cellSize = x2 - x1;

        //Debug.Log("cell size: "+cellLocalSize);

    }


    // Start is called before the first frame update
    void Awake()
    {
        if (grid == null)
        {
            int i = 3;
            if (PlayerInfo.instance != null)
                i = PlayerInfo.instance.level;

            GameObject g;
            switch (i)
            {
                case 0:
                    g = Instantiate(grid_01, this.transform);
                    break;
                case 1:
                    g = Instantiate(grid_02, this.transform);
                    break;
                case 2:
                    g = Instantiate(grid_03, this.transform);
                    break;
                case 3:
                    g = Instantiate(grid_04, this.transform);
                    break;
                default:
                    g = Instantiate(grid_01, this.transform);
                    break;
            }
            //GameObject g = Instantiate(grid_01, this.transform );
            grid = g.GetComponent<Grid>();

            
        }
        background = GameObject.Find("Background").GetComponent<Tilemap>();
        road = GameObject.Find("Road").GetComponent<Tilemap>();
        barrier = GameObject.Find("Barrier").GetComponent<Tilemap>();
        ornament = GameObject.Find("Ornament").GetComponent<Tilemap>();


        LoadTilemap();
        
    }

    public bool CellOutOfBound(Vector3Int pos)
    {
        int x = pos.x;
        int y = pos.y;

        if (x >= startX && x <= endX && y >= startY && y <= endY)
            return false;

        return true;
    }

    public Vector3Int ArrayToCell(Vector3Int aPos)
    {

        return new Vector3Int(aPos.x+startX ,aPos .y+startY,0);

    }

    public Vector3Int CellToArray(Vector3Int cPos)
    {
        int _x = cPos.x - startX;
        int _y = cPos.y - startY;
        return new Vector3Int(_x, _y, 0);

    }

    public Vector3Int WorldToArray(Vector3 wPos)
    {
        return CellToArray(grid.WorldToCell(wPos));

    }
    /*
    public Vector3Int LocalToArray(Vector3 lPos)
    {
        return CellToArray(grid.LocalToCell(lPos));

    }*/

    public Vector3 ArrayToWorld(Vector3Int aPos) {

        return grid.CellToWorld(ArrayToCell(aPos));

    }
    /*
    public Vector3 ArrayToLocal(Vector3Int aPos)
    {

        return grid.CellToLocal(ArrayToCell(aPos));

    }*/

    public Vector3 CellToWorld(Vector3Int cPos)
    {
        return grid.GetCellCenterWorld (cPos);
    }

    public Vector3Int WorldToCell(Vector3 wPos)
    {
        return grid.WorldToCell(wPos);
    }



    // Update is called once per frame
    void Update()
    {
        if (grid == null) return;

        /*
        if (Input.GetMouseButtonDown(0))
        {
            // 将鼠标点击的屏幕坐标转换为世界坐标
            Vector3 Pos = Input.mousePosition;
            Vector3 wPos = Camera.main.ScreenToWorldPoint(Pos);
            // 将世界坐标转换为瓦片坐标
            Vector3Int cellPos = grid.WorldToCell(wPos);
            // 由于是2D, 所以手动将瓦片的Z坐标改为0, 实际项目可以先查看瓦片的Z坐标值.
            cellPos.z = 0;
            Debug.Log(wPos);
            Debug.Log(cellPos);
            //Debug.Log(grid.CellToWorld(cellPos));
        }*/
       
    }
}
