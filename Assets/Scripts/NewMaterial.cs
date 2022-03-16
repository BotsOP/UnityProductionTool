using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewMaterial : MonoBehaviour
{
    [SerializeField] private RawImage albedo;
    [SerializeField] private RawImage normal;
    [SerializeField] private RawImage ambientOcclusion;
    [SerializeField] private float metallic;
    [SerializeField] private float smoothness;

    public MeshRenderer sphere;
    
    public GameObject newMatWindow;

    private Material material;
    private MaterialSkeleton materialSkeleton;
    
    private void OnEnable()
    {
        EventSystem<RawImage, Texture, Texture>.Subscribe(EventType.IMAGE_CHANGED, ImageChanged);
        EventSystem.Subscribe(EventType.OPEN_MAT_WINDOW, OpenMatWindow);
    }

    private void OnDisable()
    {
        EventSystem<RawImage, Texture, Texture>.Unsubscribe(EventType.IMAGE_CHANGED, ImageChanged);
        EventSystem.Unsubscribe(EventType.OPEN_MAT_WINDOW, OpenMatWindow);
    }

    void Update()
    {
        // material.SetFloat("Mettalic_", metallic);
        // material.SetFloat("Smoothness_", smoothness);
    }
    
    private void ImageChanged(RawImage image, Texture newTexture, Texture originalTexture)
    {
        material.SetTexture("Albedo_", albedo.texture);
        material.SetTexture("Normal_", normal.texture);
        material.SetTexture("AmbientOcclusion_", ambientOcclusion.texture);

    }

    private void OpenMatWindow()
    {
        material = new Material(Shader.Find("Shader Graphs/BasicShader"));
        sphere.material = material;
        newMatWindow.SetActive(true);
    }

    public void CloseMatWindow()
    {
        albedo.texture = null;
        normal.texture = null;
        ambientOcclusion.texture = null;
        //materialSkeleton = new MaterialSkeleton();
        newMatWindow.SetActive(false);
    }

    public void FinishMaterial()
    {
        materialSkeleton.baseMap = albedo.texture;
        materialSkeleton.normalMap = normal.texture;
        materialSkeleton.aoMap = ambientOcclusion.texture;
        
        EventSystem<MaterialSkeleton>.RaiseEvent(EventType.FINISHED_MATERIAL, materialSkeleton);
        CloseMatWindow();
    }
}
