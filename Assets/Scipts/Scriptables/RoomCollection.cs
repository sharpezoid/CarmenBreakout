using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//File for storing all the rooms used in a game
[CreateAssetMenu(fileName = "New Room Collection", menuName = "Game Data/Create New Room Collection")]
public class RoomCollection : ScriptableObject
{
    public List<RoomData> Rooms = new List<RoomData>();
}
