using UnityEditor;
using UnityEngine;

public partial class MapEditorWindow : EditorWindow
{
    public MapController.eLayers CurrentLayer;

    public void DrawLayersWindow(Rect rect)
    {
        GUILayout.BeginArea(rect);
        GUILayout.BeginHorizontal();
        GUILayout.Space(90);
        EditorGUILayout.LabelField(new GUIContent("Current Layer:"), GUILayout.Width(100));
        // select layer dropdown.
        if(EditorGUILayout.DropdownButton(new GUIContent(CurrentLayer.ToString()),FocusType.Keyboard))
        {
            GenericMenu menu = new GenericMenu();

            for (int i = 0; i < System.Enum.GetValues(typeof(MapController.eLayers)).Length; i++)
            {
                int layerIndex = i;
                menu.AddItem(new GUIContent(((MapController.eLayers)layerIndex).ToString()), false, delegate { CurrentLayer = (MapController.eLayers)layerIndex; });
            }

            menu.ShowAsContext();
        }

        GUILayout.EndHorizontal();
        GUILayout.EndArea();
    }
}