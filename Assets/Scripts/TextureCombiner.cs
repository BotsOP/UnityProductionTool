using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

public class TextureCombiner : MonoBehaviour
{
    public Texture2D sourceTex1;
    public Texture2D sourceTex2;
    public float warpFactor = 1.0f;
    
    Texture2D destTex1;
    Color[] destPix1;
    
    Texture2D destTex2;
    Color[] destPix2;
    void Start()
    {
        destTex1 = new Texture2D(sourceTex1.width, sourceTex1.height);
        destPix1 = new Color[destTex1.width * destTex1.height];
        
        destTex2 = new Texture2D(sourceTex2.width, sourceTex2.height);
        destPix2 = new Color[destTex2.width * destTex2.height];
        
        for (var y = 0; y < destTex1.height; y++)
        {
            for (var x = 0; x < destTex1.width; x++)
            {
                float xFrac = x * 1.0f / (destTex1.width - 1);
                float yFrac = y * 1.0f / (destTex1.height - 1);

                if (sourceTex1.GetPixelBilinear(xFrac, yFrac).a == 0)
                {
                    continue;
                }
                
                Color pixelColor = Color.Lerp(sourceTex1.GetPixelBilinear(xFrac, yFrac), sourceTex2.GetPixelBilinear(xFrac, yFrac), 0.5f);
                //Color pixelColor = Color.blue;

                // Get the non-integer pixel positions using GetPixelBilinear.
                destPix1[y * destTex1.width + x] = pixelColor;
            }
        }
        
        destTex1.SetPixels(destPix1);
        destTex1.Apply();
        
        byte[] bytes = destTex1.EncodeToPNG();
        
        File.WriteAllBytes(Application.dataPath + "/../SavedScreen.png", bytes);
        Debug.Log("saved something");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
