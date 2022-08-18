using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapController : MonoBehaviour
{
    public GameObject BlockPrefab;

    public List<Dictionary<Vector2Int, Block>> Blocks = new List<Dictionary<Vector2Int, Block>>();
    public List<GameObject> Entities = new List<GameObject>();

    public const int WIDTH = 20;
    public const int HEIGHT = 12;
    public const int HALF_WIDTH = WIDTH / 2;
    public const int HALF_HEIGHT = HEIGHT / 2;

    public Vector3 MapOffset = new Vector3(0, 0.36f);

    public RoomCollection Rooms;
    [HideInInspector]
    public int CurrentRoom = 0;

public enum eLayers
    {
        Rear,
        Mid,
        Front
    }
    eLayers CurrentLayer = eLayers.Mid;
    Transform[] layerContainers;

    private void CreateContainers()
    {
        layerContainers = new Transform[(int)System.Enum.GetValues(typeof(eLayers)).Length];
        for(int i = 0; i < layerContainers.Length; i++)
        {
            layerContainers[i] = new GameObject(((eLayers)i).ToString() + " Layer").transform;
            layerContainers[i].transform.SetParent(transform);
            layerContainers[i].localPosition = Vector3.zero;
            Blocks.Add(new Dictionary<Vector2Int, Block>());
        }
    }

    public void BuildRoom(int _roomIndex)
    {
        BuildRoom(Rooms.Rooms[_roomIndex]);
    }

    public void BuildRoom(RoomData _room)
    {
        // tiles
        for (int i = 0; i < _room.MapData.Count; i++)
        {
            CreateBlock(_room.MapData[i]);         
        }

        // entities
        for(int i = 0; i  < _room.EntityData.Count; i++)
        {
            CreateEntity(_room.EntityData[i]);
        }
    }

    public void ReplaceBlock(TileMapData _data)
    {
        Block block = Blocks[(int)_data.Layer][_data.Position];
        block.SetupBlock(_data);
    }

    public void CreateBlock(TileMapData _data)
    {
        //check all the layers are in place, if not make them on first time pass
        if (layerContainers == null)
            CreateContainers();

        GameObject newBlock = GameObject.Instantiate(BlockPrefab, transform);
        Block block = newBlock.GetComponent<Block>();
        block.SetupBlock(_data);
        Debug.Log("Data Layer : " + (int)_data.Layer + "      " + _data.Layer);
        Debug.Log("No of Containers : " + layerContainers.Length);
        
        newBlock.transform.SetParent(layerContainers[(int)_data.Layer]);
        newBlock.transform.position = new Vector3(_data.Position.x + 0.5f + MapOffset.x, _data.Position.y + 0.5f + MapOffset.y, (int)_data.Layer);

        Debug.Log("Creating Block " + (int)_data.Layer);
        Blocks[(int)_data.Layer].Add(_data.Position, block);
    }

    public bool RemoveBlock(Vector2Int _pos, eLayers _layer)
    {
        if (HasBlock(_pos, _layer) && Blocks[(int)_layer][_pos] != null)
        {
            Destroy(Blocks[(int)_layer][_pos].gameObject);
            Blocks[(int)_layer].Remove(_pos);
            return true;
        }
        return false;
    }

    public bool HasBlock(Vector2Int _pos, eLayers _layer)
    {
        return Blocks[(int)_layer].ContainsKey(_pos);
    }

    public void ClearMap()
    {
        for (int i = 0; i < Blocks.Count; i++) 
        {
            foreach (KeyValuePair<Vector2Int, Block> block in Blocks[i])
            {
                if (block.Value != null && block.Value.gameObject != null)
                {
                    Destroy(block.Value.gameObject);
                }
            }
        }
        Blocks.Clear();

        for (int i = 0; i < (int)System.Enum.GetValues(typeof(eLayers)).Length; i++)
        {
            Blocks.Add(new Dictionary<Vector2Int, Block>());
        }
    }

    void CreateEntity(EntityMapData _data)
    {

    }
}
