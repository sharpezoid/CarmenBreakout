using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(MapEditorSceneTool))]
public class MapEditorSceneDrawer : Editor
{
    //bool blockingMouseInput = true;

    //void OnSceneGUI()
    //{
    //    Event e = Event.current;

    //    Debug.Log("On Scene GUI!");

    //    if (e.type == EventType.MouseDown)
    //    {
    //        ////if we're clicking within our desired area, block regular input
    //        //if (isInArea(e.mousePosition)
    //        //{
    //        //    blockingMouseInput = true;
    //        //}
    //    }
    //    else if (e.type == EventType.MouseDrag)
    //    {

    //    }
    //    else if (e.type == EventType.MouseMove)
    //    {

    //    }
    //    else if (e.type == EventType.MouseUp)
    //    {
    //        if (blockingMouseInput)
    //        {
    //            Debug.Log("Mouse Up!");
    //            e.Use();
    //        }
    //        blockingMouseInput = false;
    //    }
    //    else if (e.type == EventType.Layout)
    //    {
    //        //somehow this allows e.Use() to actually function and block mouse input
    //        HandleUtility.AddDefaultControl(GUIUtility.GetControlID(GetHashCode(), FocusType.Passive));
    //    }
    //}
}
