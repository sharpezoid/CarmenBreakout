using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Tile Data", menuName = "Game Data/Create New Tile Data")]
public class TileData : ScriptableObject
{
    public bool IsSolid;
    public List<Sprite> DamageStages = new List<Sprite>();

    private Texture2D previewImage;
    public Texture2D PreviewImage
    {
        get {
            if (previewImage == null)
            {
                previewImage = new Texture2D((int)DamageStages[0].rect.width, (int)DamageStages[0].rect.height);

                var pixels = DamageStages[0].texture.GetPixels(
                    (int)DamageStages[0].textureRect.x,
                    (int)DamageStages[0].textureRect.y,
                    (int)DamageStages[0].textureRect.width,
                    (int)DamageStages[0].textureRect.height);

                previewImage.SetPixels(pixels);
                previewImage.Apply();
            }
            return previewImage; }
    }
}