using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public partial class MapEditorWindow : EditorWindow
{
    public void DrawRoomsWindow(Rect rect)
    {
        GUILayout.BeginArea(rect);
        GUILayout.BeginHorizontal();
        //if (GUILayout.Button(new GUIContent("Create New Room")))
        //{
        //    Rooms.Add(CreateNewRoom());
        //}

        if (GUILayout.Button(new GUIContent("Select Room")))
        {
            LoadRooms();

            GenericMenu menu = new GenericMenu();
            foreach (RoomData room in Rooms)
            {
                menu.AddItem(new GUIContent(room.name), false, delegate { SelectRoom(room); });
            }
            menu.AddSeparator("");
            menu.AddItem(new GUIContent("Create New Room"), false, delegate 
            {
                RoomData room = CreateNewRoom();
                Rooms.Add(room);
                SelectRoom(room);
            });
            menu.ShowAsContext();
        }

        string roomName = "No Room Selected";
        if (CurrentRoom != null)
        {
            roomName = CurrentRoom.name;
        }
        GUILayout.Label(new GUIContent("Current Room: " + roomName));
        GUILayout.EndHorizontal();
        GUILayout.EndArea();
    }
}
