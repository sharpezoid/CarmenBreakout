using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Storage for rooms to load and save between editor and game.
/// </summary>
public class RoomData : ScriptableObject
{
    // 3 depths of the room
    public List<EntityMapData> EntityData = new List<EntityMapData>();
    public List<TileMapData> MapData = new List<TileMapData>();
}

[System.Serializable]
public class EntityMapData : MapEntity
{
    EntityData entity;
}
[System.Serializable]
public class TileMapData : MapEntity
{
    public MapController.eLayers Layer;
    public TileData TileData;

    public TileMapData(Vector2Int _pos, TileData _data, MapController.eLayers _layer)
    {
        Position = _pos;
        TileData = _data;
        Layer = _layer;
    }

}
[System.Serializable]
public class MapEntity
{
    public Vector2Int Position;
    public int Health;
    public int GoldValue;
}