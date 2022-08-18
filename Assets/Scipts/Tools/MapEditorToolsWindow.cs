using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public partial class MapEditorWindow : EditorWindow
{ 
    public void DrawTools(Rect rect)
    {
        GUILayout.BeginArea(rect);
        EditorGUILayout.BeginHorizontal();

        for (int i = 0; i < System.Enum.GetValues(typeof(eToolType)).Length; i++)
        {
            string toolName = ((eToolType)i).ToString();
            if (CurrentTool == (eToolType)i)
            {
                GUILayout.Button(new GUIContent(toolName));
                Rect lastRect = GUILayoutUtility.GetLastRect();
                lastRect.x -= PADDING/2;
                lastRect.y -= PADDING/2;
                lastRect.width += PADDING;
                lastRect.height += PADDING/2;
                
                EditorGUI.DrawRect(lastRect, new Color(210,210,210));
                GUI.color = Color.black;
                GUI.Box(lastRect,toolName);
                GUI.color = Color.white;
            }
            else if (GUILayout.Button(new GUIContent(toolName)))
            {
                CurrentTool = (eToolType)i;
                switch (CurrentTool)
                {
                    case eToolType.Erase:
                        Tool.ToolCursorIcon.sprite = Tool.EraseSprite;
                        break;
                        
                    case eToolType.Paint:
                        if (CurrentTile != null)
                            Tool.ToolCursorIcon.sprite = CurrentTile.DamageStages[0];
                        else
                            Tool.ToolCursorIcon.sprite = Tool.SelectSprite;
                        break;

                    case eToolType.Fill:
                        Tool.ToolCursorIcon.sprite = Tool.FloodFillSprite;
                        break;
                }
            }
        }

        EditorGUILayout.EndHorizontal();
        GUILayout.EndArea();
    }
}
