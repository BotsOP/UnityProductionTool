using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MaterialItem : MonoBehaviour
{
    public MaterialSkeleton materialSkeleton;
    public Material material;
    public Texture texture;
    public Shader combineShader;
    private RenderTexture[] rTextures = new RenderTexture[3];
    private Material combineMat;
    private RawImage image;
    private static readonly int OcclusionMap = Shader.PropertyToID("_OcclusionMap");
    private static readonly int BaseMap = Shader.PropertyToID("_BaseMap");
    private static readonly int OnTopTex = Shader.PropertyToID("_OnTopTex");
    private static readonly int MainTex = Shader.PropertyToID("_MainTex");
    private static readonly int BumpMap = Shader.PropertyToID("_BumpMap");
    public int MaterialIndex => transform.GetSiblingIndex();

    private void OnEnable()
    {
        material = new Material(Shader.Find("Universal Render Pipeline/Lit"));
        combineMat = new Material(combineShader);
        image = GetComponent<RawImage>();

        for (int i = 0; i < rTextures.Length; i++)
        {
            rTextures[i] = new RenderTexture(4096, 4096, 16, RenderTextureFormat.ARGB32);
            rTextures[i].Create();
        }
    }

    public void UpdateMaterial(Material materialUnderneath)
    {
        combineMat.SetTexture(MainTex, materialUnderneath.GetTexture(BaseMap));
        combineMat.SetTexture(OnTopTex, materialSkeleton.baseMap);
        Graphics.Blit(materialUnderneath.GetTexture(BaseMap), rTextures[0], combineMat);
        image.texture = rTextures[0];
        material.SetTexture(BaseMap, rTextures[0]);
        
        combineMat.SetTexture(MainTex, materialUnderneath.GetTexture(BaseMap));
        combineMat.SetTexture(OnTopTex, materialSkeleton.normalMap);
        Graphics.Blit(materialUnderneath.GetTexture(BaseMap), rTextures[1], combineMat);
        material.SetTexture(BumpMap, rTextures[1]);
        
        combineMat.SetTexture(MainTex, materialUnderneath.GetTexture(OcclusionMap));
        combineMat.SetTexture(OnTopTex, materialSkeleton.aoMap);
        Graphics.Blit(materialUnderneath.GetTexture(OcclusionMap), rTextures[2], combineMat);
        material.SetTexture(OcclusionMap, rTextures[2]);
    }
}

public struct MaterialSkeleton
{
    public Texture baseMap;
    public Texture normalMap;
    public Texture aoMap;
}
