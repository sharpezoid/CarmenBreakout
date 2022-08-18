using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Tile Palette", menuName = "Game Data/Create New Tile Palette")]
public class TilePalette : ScriptableObject
{
    public List<TileData> Tiles = new List<TileData>();
}
