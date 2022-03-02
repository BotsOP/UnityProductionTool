using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewMaterial : MonoBehaviour
{
    public RawImage albedo;
    public RawImage normal;
    public RawImage ambientOcclusion;
    public float metallic;
    public float smoothness;

    public Material material;
    
    void Update()
    {
        material.SetTexture("Albedo_", albedo.texture);
        material.SetTexture("Normal_", normal.texture);
        material.SetTexture("AmbientOcclusion_", ambientOcclusion.texture);
        material.SetFloat("Mettalic_", metallic);
        material.SetFloat("Smoothness_", smoothness);
    }
}
