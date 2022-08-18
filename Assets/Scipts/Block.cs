using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    public bool Unbreakable;
    public int Hits = 1;
    public int GoldValue = 100;
    public SpriteRenderer SpriteRenderer;
    
    public void SetupBlock(TileMapData _data)
    {
        Unbreakable = _data.TileData.IsSolid;
        Hits = _data.Health;
        GoldValue = _data.GoldValue;
        SpriteRenderer.sprite = _data.TileData.DamageStages[0];
    }

    public void SetSprite(Sprite _sprite)
    {
        SpriteRenderer.sprite = _sprite;
    }

    public void OnHit()
    {
        if (Unbreakable)
            return;

        Hits--;
        
        if (Hits <= 0)
        {
            FindObjectOfType<GameController>().AddGold(GoldValue);
            gameObject.SetActive(false);
        }
    }
}