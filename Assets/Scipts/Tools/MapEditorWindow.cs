#if UNITY_EDITOR
using System.Collections;
using UnityEditor;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;

public partial class MapEditorWindow : EditorWindow
{
    public const float WINDOW_WIDTH = 800.0f;
    public const float WINDOW_HEIGHT = 400.0f;
    public const float PADDING = 5f;

    private static MapEditorWindow Instance;
    private static MapEditorSceneTool Tool;

    private Rect toolWindow;
    private Rect paletteWindow;
    private Rect tileWindow;

    private Vector2 tileScroll = Vector2.zero;

    [MenuItem("Tools/Map Editor")]
    public static void StartEditor()
    {
        //open game scene, then load in the level editor scene.
        if (!EditorSceneManager.GetActiveScene().name.Contains("Game"))
        {
            EditorSceneManager.OpenScene("Assets/Scenes/Game.unity", OpenSceneMode.Single);
        }

        EditorSceneManager.OpenScene("Assets/Scenes/Level Editor.unity", OpenSceneMode.Additive);

        // start to play.
        if (!EditorApplication.isPlaying)
        {
            EditorApplication.isPlaying = true;
        }
    }

    public static MapEditorWindow OpenMapEditor(MapEditorSceneTool sceneTool)
    {
        Instance = EditorWindow.GetWindow<MapEditorWindow>("Map Editor", true);
        Instance.maxSize = new Vector2(WINDOW_WIDTH + 0.1f, WINDOW_HEIGHT + 0.1f);
        Instance.minSize = new Vector2(WINDOW_WIDTH, WINDOW_HEIGHT);

        Tool = sceneTool;

        Instance.Focus();

        return Instance;
    }

    public enum eToolType
    {
        Select,
        Paint,
        PaintBox,
        Fill,
        Erase
    }
    public eToolType CurrentTool = eToolType.Select;

    public TilePalette CurrentPalette;
    public TileData CurrentTile;
    public RoomData CurrentRoom;


    List<TilePalette> TilePalettes = new List<TilePalette>();
    List<TileData> LoadedTiles = new List<TileData>();

    public List<RoomData> Rooms = new List<RoomData>();

    Rect toolsRect;
    Rect roomsRect;
    Rect layersRect;
    Rect paletteRect;
    Rect blockInspectorRect;

    private void OnEnable()
    {
        toolsRect = new Rect(WINDOW_WIDTH / 4, 
                             PADDING, 
                             WINDOW_WIDTH / 4 * 2 - PADDING * 2, 
                             20);

        roomsRect = new Rect(PADDING,
                             toolsRect.y + toolsRect.height + PADDING,
                             WINDOW_WIDTH / 4 * 2 - PADDING * 2,
                             20);

        layersRect = new Rect(roomsRect.x + roomsRect.width + PADDING,
                              roomsRect.y,
                              roomsRect.width,
                              20);

        paletteRect = new Rect(PADDING,
                               roomsRect.y + roomsRect.height + PADDING * 2,
                               WINDOW_WIDTH - PADDING*2,
                               WINDOW_HEIGHT-PADDING*2-(layersRect.y+layersRect.height));

        

        CurrentTool = eToolType.Paint;
    }

    private void OnGUI() 
    {
        //EditorGUI.DrawRect(toolsRect, Color.red);
        //EditorGUI.DrawRect(roomsRect, Color.yellow);
        //EditorGUI.DrawRect(layersRect, Color.blue);
        //EditorGUI.DrawRect(paletteRect, Color.green);
        //Debug.Log(paletteRect.width);

        DrawTools(toolsRect);
        DrawRoomsWindow(roomsRect);
        DrawLayersWindow(layersRect);
        DrawPaletteWindow(paletteRect);
        
        if (Tool.SelectedBlock != null)
        {
            DrawBlockInspectorWindow(blockInspectorRect);
        }

        Repaint();
    }

    public void OnDisable()
    {
        //#todo check for save or not
        EditorApplication.isPlaying = false;
    }

    void LoadPalettes()
    {
        string[] guids = AssetDatabase.FindAssets("t:TilePalette");
        for(int i = 0; i < guids.Length; i++)
        {
            TilePalette p = (TilePalette)AssetDatabase.LoadAssetAtPath(AssetDatabase.GUIDToAssetPath(guids[i]), typeof(TilePalette));
            
            TilePalettes.Add(p); 
        }

        SelectPalette(TilePalettes[0]);
    }

    void LoadRooms()
    {
        string[] guids = AssetDatabase.FindAssets("t:RoomData");
        for (int i = 0; i < guids.Length; i++)
        {
            RoomData rd = (RoomData)AssetDatabase.LoadAssetAtPath(AssetDatabase.GUIDToAssetPath(guids[i]), typeof(RoomData));
            Rooms.Add(rd);
        }

        if (Rooms.Count == 0)
        {
            if (EditorUtility.DisplayDialog("No Rooms Found!", "No rooms found, would you like to create one?", "yes", "No"))
            {
                Rooms.Add(CreateNewRoom());
            }
        }
        //if (Rooms.Count > 0)
        //{
        //    SelectRoom(Rooms[0]);
        //}
    }

    RoomData CreateNewRoom()
    {
        RoomData asset = ScriptableObject.CreateInstance<RoomData>();

        AssetDatabase.CreateAsset(asset, "Assets/Game Data/Rooms/New Room.asset");
        AssetDatabase.SaveAssets();

        return asset;
    }

    void SelectRoom(RoomData _room)
    {
        CurrentRoom = _room;

        Tool.BuildRoomOnMap();
    }



    void SelectPalette(TilePalette _palette)
    {
        CurrentPalette = _palette;

        BuildDisplayTiles();
    }

    void BuildDisplayTiles()
    {
        LoadedTiles.Clear();

        for (int i = 0; i < CurrentPalette.Tiles.Count; i++)
        {
            LoadedTiles.Add(CurrentPalette.Tiles[i]);
        }
    }
}

//class DisplayTile
//{
//    public Texture2D Image;
//    public TileData Tile;

//    public DisplayTile(TileData _tile)
//    {
//        Tile = _tile;

//        if (_tile.DamageStages.Count > 0)
//        {
//            Image = new Texture2D((int)_tile.DamageStages[0].rect.width, (int)_tile.DamageStages[0].rect.height);
          
//            var pixels = _tile.DamageStages[0].texture.GetPixels(
//                (int)_tile.DamageStages[0].textureRect.x,
//                (int)_tile.DamageStages[0].textureRect.y,
//                (int)_tile.DamageStages[0].textureRect.width,
//                (int)_tile.DamageStages[0].textureRect.height);

//            Image.SetPixels(pixels);
//            Image.Apply();
//        }
//    }
//}

#endif