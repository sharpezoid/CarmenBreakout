using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public partial class MapEditorWindow : EditorWindow
{        
    // select palette buttons.
    // Current Palette
    // all the tiles or objects in that palette.
    public void DrawPaletteWindow(Rect rect)
    {
        GUILayout.BeginArea(rect);
        GUILayout.BeginHorizontal();
        if (GUILayout.Button(new GUIContent("Select Palette")))
        {
            //load all the palettes...
            LoadPalettes();

            GenericMenu menu = new GenericMenu();
            foreach (TilePalette p in TilePalettes)
            {
                menu.AddItem(new GUIContent(p.name), false, delegate { SelectPalette(p); });
            }

            menu.ShowAsContext();
        }

        if (CurrentPalette != null)
            GUILayout.Label("Current Palette : " + CurrentPalette.ToString());

        //GUILayout.BeginHorizontal();

        if (CurrentPalette != null && LoadedTiles.Count > 0)
        {
            //var row = Math.floor(i / columnCount)
            //var col = i % columnCount
            for (int i = 0; i < LoadedTiles.Count; i++)
            {
                int y = Mathf.FloorToInt(i / 10f);
                int x = (i % 9) ;
                Rect displayRect = new Rect(x * 68 + PADDING * 20, y * 68 + PADDING * 10, 64, 64);
                if (CurrentTile == LoadedTiles[i])
                {
                    GUI.Box(displayRect, "");
                }


                if (GUI.Button(displayRect, LoadedTiles[i].PreviewImage))
                {
                    SelectTile(LoadedTiles[i]);
                    //CurrentTile = DisplayTiles[i];
                }
                
            }
        }
        //GUILayout.EndHorizontal();

        GUILayout.EndHorizontal();
        GUILayout.EndArea();
    }

    void SelectTile(TileData _tile)
    {
        CurrentTile = _tile;
        if (Tool.PaintingBlock== null)
        {
            Tool.PaintingBlock = GameObject.Instantiate(Tool.BlockPrefab).GetComponent<Block>();
        }

        Tool.PaintingBlock.SetSprite(_tile.DamageStages[0]);
    }
}