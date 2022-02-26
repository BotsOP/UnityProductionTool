using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Combine : MonoBehaviour
{
    public Material combineMat;
    
    public RawImage image1;
    public RawImage image2;

    public void OnSliderUpdated(float value)
    {
        Debug.Log("test");
        
        combineMat.SetTexture("Texture1_", image1.texture);
        combineMat.SetTexture("Texture2_", image2.texture);
        
        combineMat.SetFloat("Lerp_", value);
    }
}
