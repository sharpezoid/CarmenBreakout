using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockController : MonoBehaviour
{
    public float AValue = 5;

    //public List<Block> Blocks = new List<Block>();

    //GameController gc;

    //float respawnWaitDuration = 0.5f;
    //float respawnTime;
    //bool waitingToRespawn = false;

    //private void Start()
    //{
    //    gc = FindObjectOfType<GameController>();
    //}

    //private void Update()
    //{
    //    if (!waitingToRespawn)
    //    {
    //        bool hasBlocks = false;
    //        foreach (Block b in Blocks)
    //        {
    //            if (b.gameObject.activeSelf)
    //                hasBlocks = true;
    //        }
    //        if (!hasBlocks)
    //        {
    //            respawnTime = respawnWaitDuration;
    //            waitingToRespawn = true;
    //        }
    //    }

    //    if (waitingToRespawn && respawnTime <= 0)
    //    { 
    //        gc.Level++;
    //        waitingToRespawn = false;
    //        RespawnBlocks();
    //    }
    //    else if (waitingToRespawn)
    //    {
    //        respawnTime -= Time.deltaTime;
    //    }

    //}

    ///// <summary>
    ///// This is the Respawn Blocks Summary
    ///// </summary>
    //void RespawnBlocks()
    //{
    //    foreach(Block b in Blocks)
    //    {
    //        b.gameObject.SetActive(true);
    //        b.Hits = gc.Level;
    //    }
    //}
}
