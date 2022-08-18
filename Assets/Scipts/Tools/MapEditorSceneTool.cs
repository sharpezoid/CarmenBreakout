#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class MapEditorSceneTool : MonoBehaviour
{
    private MapEditorWindow MapEditor = null;
    MapController MapController;
    public Material lineMat;
    public Block SelectedBlock = null;
    GameObject gameControllerObject;
    GameObject gameCanvasObject;

    public GameObject ToolCursor;
    public SpriteRenderer ToolCursorIcon;
    public GameObject BlockPrefab;
    public Block PaintingBlock;
    Vector3 toolPos;

    public Sprite MoveSprite;
    public Sprite SelectSprite;
    public Sprite EraseSprite;
    public Sprite FloodFillSprite;

    private void Awake()
    {
        // Turn off the game scene GameController and UI
        gameControllerObject = FindObjectOfType<GameController>().gameObject;
        gameControllerObject.SetActive(false);
        gameCanvasObject = FindObjectOfType<UIController>().gameObject;
        gameCanvasObject.SetActive(false);

        MapController = FindObjectOfType<MapController>();

        MapEditor = MapEditorWindow.OpenMapEditor(this);

        //Blocks = new List<Block[,]>();

        //for (int i = 0; i < System.Enum.GetValues(typeof(MapEditorWindow.eLayers)).Length; i++)
        //{
        //    Blocks.Add(new Block[levelWidth, levelHeight]);

        //    GameObject newLayer = new GameObject(((MapEditorWindow.eLayers)i).ToString());
        //    newLayer.transform.SetParent(transform);

        //    for (int x = 0; x < levelWidth; x++)
        //    {
        //        for (int y = 0; y < levelHeight; y++)
        //        {
        //            GameObject blockObj = GameObject.Instantiate(DefaultBlock, newLayer.transform);
        //            blockObj.transform.position = new Vector3(x - halfWidth + 0.5f, y - halfHeight + 0.5f - 0.36f, 0);
        //            Blocks[i][x, y] = blockObj.GetComponent<Block>();
        //        }
        //    }
        //}
    }

    private void Update()
    {   
        toolPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        toolPos.x = Mathf.Floor(toolPos.x) + 0.5f;
        toolPos.y = Mathf.Floor(toolPos.y) + 0.5f - 0.36f;
        toolPos.z = 0;

        ToolCursor.transform.position = toolPos;

        //move preview with mouse cursor
        if (PaintingBlock != null)
            PaintingBlock.transform.position = toolPos;

        if (Input.GetMouseButton(0))
        {
            switch (MapEditor.CurrentTool)
            {
                case MapEditorWindow.eToolType.Paint:
                    PaintTile();
                    break;

                case MapEditorWindow.eToolType.Erase:
                    EraseTile();
                    break;
            }
            if (MapEditor.CurrentRoom != null)
            {
                EditorUtility.SetDirty(MapEditor.CurrentRoom);
            }
        }
    }

    void EraseTile()
    {
        if (MapEditor.CurrentRoom == null)
            return;

        Vector2Int tilePos = new Vector2Int(Mathf.FloorToInt(toolPos.x), Mathf.FloorToInt(toolPos.y));
        Debug.Log("Testing Paint @ " + tilePos.x + ", " + tilePos.y);

        // remove block from display
        MapController.RemoveBlock(tilePos, MapEditor.CurrentLayer);

        // remove data from map data
        for (int i = 0; i < MapEditor.CurrentRoom.MapData.Count; i++)
        {
            if (MapEditor.CurrentRoom.MapData[i].Position == tilePos 
                && MapEditor.CurrentRoom.MapData[i].Layer == MapEditor.CurrentLayer)
            {
                MapEditor.CurrentRoom.MapData.RemoveAt(i);
                break;
            }
        }
    }

    void PaintTile()
    {
        if (MapEditor.CurrentRoom == null || MapEditor.CurrentTool == null)
            return;

        Vector2Int tilePos = new Vector2Int(Mathf.FloorToInt(toolPos.x), Mathf.FloorToInt(toolPos.y));
        Debug.Log("Testing Paint @ " + tilePos.x + ", " + tilePos.y);
        
        TileMapData data = null;
        // make sure data for the tile exists
        for (int i = 0; i < MapEditor.CurrentRoom.MapData.Count; i++)
        {
            if (MapEditor.CurrentRoom.MapData[i].Position == tilePos)
            {
                MapEditor.CurrentRoom.MapData[i].Layer = MapEditor.CurrentLayer;
                MapEditor.CurrentRoom.MapData[i].TileData = MapEditor.CurrentTile;
                data = MapEditor.CurrentRoom.MapData[i];
            }
        }
        if (data == null)
        {
            data = new TileMapData(tilePos, MapEditor.CurrentTile, MapEditor.CurrentLayer);
            MapEditor.CurrentRoom.MapData.Add(data);
        }


        //replace existing block if there is one otherwise create one.
        if (MapController.HasBlock(tilePos, MapEditor.CurrentLayer))//.Blocks[(int)MapEditor.CurrentLayer][tilePos.x, tilePos.y] != null)
        {
            MapController.ReplaceBlock(data);
        }
        else
        {
            MapController.CreateBlock(data);
        }
        //    // replace existing map data if existing otherwise create one.

        //    if (MapController.Blocks[(int)MapEditor.CurrentLayer][tilePos.x,tilePos.y] != null)
        //{
        //    Debug.Log("Existing Tile");
        //    for (int i = 0; i < MapEditor.CurrentRoom.MapData.Count; i++)
        //    {
        //        if (MapEditor.CurrentRoom.MapData[i].Position == tilePos)
        //        {
        //            MapEditor.CurrentRoom.MapData[i].Layer = MapEditor.CurrentLayer;
        //            MapEditor.CurrentRoom.MapData[i].TileData = MapEditor.CurrentTile;
        //            foundExisting = true;
        //        }
        //    }
        //    if (!foundExisting)
        //    {
        //        MapEditor.CurrentRoom.MapData.Add(new TileMapData(tilePos, MapEditor.CurrentTile, MapEditor.CurrentLayer));

        //    }
        //}
        //else
        //{
        //    MapEditor.CurrentRoom.MapData.Add(new TileMapData(tilePos, MapEditor.CurrentTile, MapEditor.CurrentLayer));

        //}

        //for (int i = 0; i < MapEditor.CurrentRoom.MapData.Count; i++)
        //{
        //    if (MapEditor.CurrentRoom.MapData[i].Position == tilePos)
        //    {
        //        Debug.Log("Existing Tile");
        //        MapEditor.CurrentRoom.MapData[i].Layer = MapEditor.CurrentLayer;
        //        MapEditor.CurrentRoom.MapData[i].TileData = MapEditor.CurrentTile;
        //        foundExisting = true;
        //        break;
        //    }
        //}

        //if (!foundExisting)
        //{
        //    Debug.Log("No Tile Found");
        //    MapEditor.CurrentRoom.MapData.Add(new TileMapData(tilePos, MapEditor.CurrentTile, MapEditor.CurrentLayer));
        //}


        //Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);//, Camera.main.transform.forward);
        //Debug.DrawRay(ray.origin, ray.direction, Color.cyan, 15.0f);
        //if (hit.transform != null)
        //{
        //    Block block = hit.transform.GetComponent<Block>();
        //    if (block != null)
        //    {
        //        //for(int i = 0; i < MapEditor.CurrentRoom.Tiles.Count; i++)
        //        //{
        //        //   // MapEditor.CurrentRoom.Tiles[i] = MapEditor.CurrentTile;
        //        //}
        //        Debug.Log("Hit " + hit.transform.gameObject, hit.transform.gameObject);
        //    }
        //}
    }

    private void OnDrawGizmos()
    {
        GL.Begin(GL.LINES);
        lineMat.SetPass(0);
        GL.Color(lineMat.color);

        for (int x = 0; x < MapController.WIDTH + 1; x++)
        {
            GL.Vertex3(x - MapController.HALF_WIDTH, -MapController.HALF_HEIGHT, 0);
            GL.Vertex3(x - MapController.HALF_WIDTH, -MapController.HALF_HEIGHT + MapController.HEIGHT, 0);
        }
        for (int y = 0; y < MapController.HEIGHT + 1; y++)
        {
            GL.Vertex3(-MapController.HALF_WIDTH, y - MapController.HALF_HEIGHT - 0.36f, 0);
            GL.Vertex3(-MapController.HALF_WIDTH + MapController.WIDTH, y - MapController.HALF_HEIGHT - 0.36f, 0);
        }

        GL.End();
    }

    public void BuildRoomOnMap()
    {
        MapController.ClearMap();

        MapController.BuildRoom(MapEditor.CurrentRoom);

        //for (int i = 0; i < MapEditor.CurrentRoom.MapData.Count; i++)
        //{

        //}

        //for (int j = 0; j < MapEditor.CurrentRoom.Tiles.Count; j++)
        //{
        //    //Blocks.Add(new Block[levelWidth, levelHeight]);

        //    for (int i = 0; i < MapEditor.CurrentRoom.Tiles[j].Count; i++)
        //    {
        //        int x = i % levelWidth;
        //        int y = i / levelWidth;

        //        GameObject newBlock = GameObject.Instantiate(DefaultBlock);
        //        newBlock.transform.position = new Vector3(x, y, 0);

        //        Blocks[j][x, y] = newBlock.GetComponent<Block>();
        //    }
        //}
    }
    private void OnDisable()
    {
        // turn any game scene required objects back on
        if (gameControllerObject != null)
            gameControllerObject.SetActive(true);
        if (gameCanvasObject != null)
            gameCanvasObject.SetActive(true);


        MapController.ClearMap();

        UnityEngine.SceneManagement.SceneManager.UnloadSceneAsync("Level Editor");
       
        MapEditor.Close();
    }
}

#endif