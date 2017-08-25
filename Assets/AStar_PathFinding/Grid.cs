using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour {

    public bool displaygridgizmo;
    public LayerMask unwalkableMask;
    public Vector2 gridWorldSize; //18 -10
    public float nodeRadius;
    public int ProximityPenalty = 10;

    int minpenalty = int.MaxValue;
    int maxpenalty = int.MinValue;

    Node[,] grid;
    public TerrainType[] walkableRegions;
    LayerMask walkableMask;
    Dictionary<int, int> walkableRegionsDictionary = new Dictionary<int, int>();

    float nodeDiameter;
    int gridSizeX, gridSizeY;

    void Awake()
    {
        nodeDiameter = nodeRadius * 2;
        gridSizeX = Mathf.RoundToInt(gridWorldSize.x / nodeDiameter);
        gridSizeY = Mathf.RoundToInt(gridWorldSize.y / nodeDiameter);

        foreach(TerrainType T in walkableRegions)
        {
            walkableMask.value |= T.terrainMask.value;
            walkableRegionsDictionary.Add((int)Mathf.Log(T.terrainMask.value,2),T.terrainPenalty);
        }

 //       CreateGrid();
    }

    void CreateGrid()
    {
        grid = new Node[gridSizeX, gridSizeY];
        Vector2 worldbottomleft = (Vector2)transform.position - Vector2.right * gridWorldSize.x/2 - Vector2.up * gridWorldSize.y / 2;
        for(int i = 0;i<gridSizeX;i++)
        {
            for (int j = 0; j < gridSizeY; j++)
            {
                Vector2 worldPoint = worldbottomleft + Vector2.right *(i * nodeDiameter + nodeRadius) + Vector2.up * (j * nodeDiameter + nodeRadius);
                //bool walkable = !Physics.CheckSphere(worldPoint, nodeRadius);
                bool walkable = !Physics2D.CircleCast(worldPoint,nodeRadius,Vector2.zero,0,unwalkableMask);
                int penaltymovement = 0;

                    //Ray2D ray = new Ray2D(worldPoint,Vector2.right * (nodeRadius/2));
                    RaycastHit2D hit = Physics2D.Raycast(worldPoint, Vector2.right, (nodeRadius / 2), walkableMask);
                    if(hit)
                    {
                        walkableRegionsDictionary.TryGetValue(hit.collider.gameObject.layer, out penaltymovement);
                    }
                    
                if(!walkable)
                {
                    penaltymovement += ProximityPenalty;
                }

                grid[i, j] = new Node(walkable, worldPoint,i,j, penaltymovement);
            }
        }

        BlurPenaltyMap(3);

    }

    public void CreateNewGrid()
    {
        //if (grid != null)
        //{
        //    for(int i =0;i< gridSizeX; i++)
        //    {
        //        for (int j = 0; i < gridSizeY; j++)
        //        {
        //            grid[i, j] = null;
        //        }
        //    }
        //}


        CreateGrid();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.G))
        {
            CreateNewGrid();
        }
    }
    void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, new Vector2(gridWorldSize.x, gridWorldSize.y));


        if (grid != null && displaygridgizmo)
        {
            foreach (Node n in grid)
            {
                Gizmos.color = Color.Lerp(Color.white, Color.black, Mathf.InverseLerp(minpenalty, maxpenalty, n.penaltyValue));
                Gizmos.color = (n.walkable) ? Gizmos.color : Color.red;
                Gizmos.DrawCube(n.worldPosition, Vector3.one * nodeRadius);
            }
        }
    }

    public int MaxSize
    {
        get { return gridSizeX * gridSizeY; }
    }

    public Node NodeFromWorldPoint(Vector2 worldpos)
    {
        Vector2 cam = Camera.main.transform.position;

        Vector2 pos = Vector2.zero;

        pos.x = worldpos.x - cam.x;
        pos.y = worldpos.y - cam.y;


        float clampedx = pos.x % 18;
       float clampedy = pos.y % 10;


        float perx = (clampedx + gridWorldSize.x / 2) / gridWorldSize.x;
        float pery = (clampedy + gridWorldSize.y / 2) / gridWorldSize.y;
        perx = Mathf.Clamp01(perx);
        pery = Mathf.Clamp01(pery);

        int x = Mathf.RoundToInt((gridSizeX - 1) * perx);
        int y = Mathf.RoundToInt((gridSizeY - 1) * pery);


        return grid[x,y];
    }

    public Node RandomWalkableNode()
    {
        Node toreturn = new Node(false,Vector2.zero,0,0,0);
        if (grid != null)
        {
            bool found = false;
            Transform cam = Camera.main.transform;
            
            while (!found)
            {
                Vector2 selectedpos = Camera.main.ViewportToWorldPoint(new Vector3(Random.Range(0, 1), Random.Range(0, 1), 0));

                toreturn = NodeFromWorldPoint(selectedpos);
                if (toreturn.walkable)
                {
                    found = true;
                }

            }            
        }
        return toreturn;
    }

    public bool IsLocationPossible(Vector2 v)
    {
        if(NodeFromWorldPoint(v).walkable)
        {
            return true;
        }
        return false;
    }

    public List<Node> GetNeightbours(Node node)
    {
        List<Node> neightbours = new List<Node>();

        for(int x = -1;x <=1;x++)
        {
            for (int y = -1; y <= 1; y++)
            {
                if (x == 0 && y == 0)
                    continue;
                int checkX = node.gridx + x;
                int checkY = node.gridy + y;

                if(checkX >=0 && checkX<gridSizeX && checkY >= 0 && checkY < gridSizeY)
                {
                    neightbours.Add(grid[checkX,checkY]);
                }
            }
        }

        return neightbours;
    }

    void BlurPenaltyMap(int blursize)
    {
        int kernelsize = blursize * 2 + 1;
        int kernelextense = (kernelsize - 1) / 2;
        int[,] penaltyhorizontalPass = new int[gridSizeX,gridSizeY];
        int[,] penaltyverticalPass = new int[gridSizeX, gridSizeY];

        for(int y = 0;y<gridSizeY;y++)
        {
            for(int x = -kernelextense;x <=kernelextense;x++)
            {
                int samplex = Mathf.Clamp(x,0,kernelextense);
                penaltyhorizontalPass[0, y] += grid[samplex,y].penaltyValue;
            }
            for (int x = 1; x < gridSizeX; x++)
            {
                int removeindex = Mathf.Clamp(x - kernelextense - 1, 0, gridSizeX);
                int addindex = Mathf.Clamp(x + kernelextense, 0, gridSizeX -1);

                penaltyhorizontalPass[x, y] = penaltyhorizontalPass[x-1,y] - grid[removeindex,y].penaltyValue + grid[addindex,y].penaltyValue;
            }
        }



        for (int x = 0; x < gridSizeX; x++)
        {
            for (int y = -kernelextense; y <= kernelextense; y++)
            {
                int sampley = Mathf.Clamp(y, 0, kernelextense);
                penaltyverticalPass[x, 0] += penaltyhorizontalPass[x, sampley];
            }
            int blurredpenalty = Mathf.RoundToInt((float)penaltyverticalPass[x, 0] / (kernelsize * kernelsize));
            grid[x, 0].penaltyValue = blurredpenalty;
            for (int y = 1; y < gridSizeY; y++)
            {
                int removeindex = Mathf.Clamp(y - kernelextense - 1, 0, gridSizeY);
                int addindex = Mathf.Clamp(y + kernelextense, 0, gridSizeY - 1 );

                penaltyverticalPass[x, y] += penaltyverticalPass[x, y-1] - penaltyhorizontalPass[x, removeindex] + penaltyhorizontalPass[x, addindex];
                blurredpenalty = Mathf.RoundToInt( (float)penaltyverticalPass[x, y] / (kernelsize * kernelsize));
                grid[x, y].penaltyValue = blurredpenalty;

                if(blurredpenalty > maxpenalty)
                {
                    maxpenalty = blurredpenalty;
                }
                 else if(blurredpenalty < minpenalty)
                {
                    minpenalty = blurredpenalty;
                }

            }
        }

    }

}

[System.Serializable]
public class TerrainType
{
    public LayerMask terrainMask;
    public int terrainPenalty;
}
