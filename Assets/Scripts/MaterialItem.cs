using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MaterialItem : MonoBehaviour
{
    public MaterialSkeleton materialSkeleton;
    public Material material;
    [SerializeField] private Shader combineShaderAlpha;
    [SerializeField] private Shader combineShaderRed;
    private Material materialUnderneath;
    private RenderTexture[] rTextures = new RenderTexture[3];
    private Material combineMatAlpha;
    private Material combineMatRed;
    private RawImage image;
    private static readonly int OcclusionMap = Shader.PropertyToID("_OcclusionMap");
    private static readonly int BaseMap = Shader.PropertyToID("_BaseMap");
    private static readonly int OnTopTex = Shader.PropertyToID("_OnTopTex");
    private static readonly int MainTex = Shader.PropertyToID("_MainTex");
    private static readonly int BumpMap = Shader.PropertyToID("_BumpMap");
    private static readonly int MaskTex = Shader.PropertyToID("_MaskTex");
    public int MaterialIndex => transform.GetSiblingIndex();

    private void OnEnable()
    {
        EventSystem<MaterialSkeleton, Transform>.Subscribe(EventType.UPDATED_MATERIAL, UpdateMaterialSkeleton);

        material = new Material(Shader.Find("Universal Render Pipeline/Lit"));
        combineMatAlpha = new Material(combineShaderAlpha);
        combineMatRed = new Material(combineShaderRed);
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

        if (materialSkeleton.maskTexture != null)
        {
            Debug.Log("use mask tex");
            combineMatRed.SetTexture(MaskTex, materialSkeleton.maskTexture);
            
            combineMatRed.SetTexture(MainTex, materialUnderneath.GetTexture(BaseMap));
            combineMatRed.SetTexture(OnTopTex, materialSkeleton.baseMap);
            Graphics.Blit(materialUnderneath.GetTexture(BaseMap), rTextures[0], combineMatRed);
            image.texture = rTextures[0];
            material.SetTexture(BaseMap, rTextures[0]);
        
            combineMatRed.SetTexture(MainTex, materialUnderneath.GetTexture(BaseMap));
            combineMatRed.SetTexture(OnTopTex, materialSkeleton.normalMap);
            Graphics.Blit(materialUnderneath.GetTexture(BaseMap), rTextures[1], combineMatRed);
            material.SetTexture(BumpMap, rTextures[1]);
        
            combineMatRed.SetTexture(MainTex, materialUnderneath.GetTexture(OcclusionMap));
            combineMatRed.SetTexture(OnTopTex, materialSkeleton.aoMap);
            Graphics.Blit(materialUnderneath.GetTexture(OcclusionMap), rTextures[2], combineMatRed);
            material.SetTexture(OcclusionMap, rTextures[2]);
            
            return;
        }
        
        combineMatAlpha.SetTexture(MainTex, materialUnderneath.GetTexture(BaseMap));
        combineMatAlpha.SetTexture(OnTopTex, materialSkeleton.baseMap);
        Graphics.Blit(materialUnderneath.GetTexture(BaseMap), rTextures[0], combineMatAlpha);
        image.texture = rTextures[0];
        material.SetTexture(BaseMap, rTextures[0]);
        
        combineMatAlpha.SetTexture(MainTex, materialUnderneath.GetTexture(BaseMap));
        combineMatAlpha.SetTexture(OnTopTex, materialSkeleton.normalMap);
        Graphics.Blit(materialUnderneath.GetTexture(BaseMap), rTextures[1], combineMatAlpha);
        material.SetTexture(BumpMap, rTextures[1]);
        
        combineMatAlpha.SetTexture(MainTex, materialUnderneath.GetTexture(OcclusionMap));
        combineMatAlpha.SetTexture(OnTopTex, materialSkeleton.aoMap);
        Graphics.Blit(materialUnderneath.GetTexture(OcclusionMap), rTextures[2], combineMatAlpha);
        material.SetTexture(OcclusionMap, rTextures[2]);
    }

    private void UpdateMaterialSkeleton(MaterialSkeleton newMaterialSkeleton, Transform keyTransform)
    {
        if (keyTransform == null)
        {
            Debug.Log("transform is null, got " + keyTransform.name);
            return;
        }
        if (transform == keyTransform)
        {
            Debug.Log("update material");
            materialSkeleton = newMaterialSkeleton;
            EventSystem.RaiseEvent(EventType.UPDATE_RENDERER);
        }
        else
        {
            Debug.Log("not correct transform  " + transform.GetSiblingIndex() + "    " + keyTransform.GetSiblingIndex());
        }
    }

    public void SelectedMaterial()
    {
        EventSystem<MaterialSkeleton, Transform>.RaiseEvent(EventType.SELECTED_MATERIAL, materialSkeleton, transform);
    }
}

public struct MaterialSkeleton
{
    public Texture baseMap;
    public Texture normalMap;
    public Texture aoMap;
    public Texture maskTexture;
}
