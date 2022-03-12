using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MaterialItem : MonoBehaviour
{
    public MaterialSkeleton materialSkeleton;
    public Material material;
    public Shader combineShader;
    private Material materialUnderneath;
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
        EventSystem<MaterialSkeleton, Transform>.Subscribe(EventType.UPDATED_MATERIAL, UpdateMaterialSkeleton);

        material = new Material(Shader.Find("Universal Render Pipeline/Lit"));
        combineMat = new Material(combineShader);
        image = GetComponent<RawImage>();

        for (int i = 0; i < rTextures.Length; i++)
        {
            rTextures[i] = new RenderTexture(4096, 4096, 16, RenderTextureFormat.ARGB32);
            rTextures[i].Create();
        }
    }

    private void OnDisable()
    {
        EventSystem<MaterialSkeleton, Transform>.Unsubscribe(EventType.UPDATED_MATERIAL, UpdateMaterialSkeleton);
    }

    public void UpdateMaterial(Material materialUnderneath)
    {
        this.materialUnderneath = materialUnderneath;
        
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

    private void UpdateMaterialSkeleton(MaterialSkeleton newMaterialSkeleton, Transform keyTransform)
    {
        if (keyTransform == null)
        {
            Debug.Log("transform is null");
            return;
        }
        if (transform == keyTransform)
        {
            Debug.Log("update material");
            materialSkeleton = newMaterialSkeleton;
            UpdateMaterial(materialUnderneath);
        }
        else
        {
            Debug.Log("not correct transform  " + transform.GetSiblingIndex() + "    " + keyTransform.GetSiblingIndex());
        }
    }

    public void SelectedMaterial()
    {
        Debug.Log("click");
        EventSystem<MaterialSkeleton, Transform>.RaiseEvent(EventType.SELECTED_MATERIAL, materialSkeleton, transform);
    }
}

public struct MaterialSkeleton
{
    public Texture baseMap;
    public Texture normalMap;
    public Texture aoMap;
}
