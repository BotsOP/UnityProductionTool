using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EditMaterial : MonoBehaviour
{
    public RawImage albedoImage;
    private Transform materialTransformKey;
    private MaterialSkeleton materialSkeleton;

    private void OnEnable()
    {
        EventSystem<MaterialSkeleton, Transform>.Subscribe(EventType.SELECTED_MATERIAL, SelectedNewMaterial);
        EventSystem<RawImage, Texture, Texture>.Subscribe(EventType.IMAGE_CHANGED, OnImageChanged);
    }

    private void OnDisable()
    {
        EventSystem<MaterialSkeleton, Transform>.Unsubscribe(EventType.SELECTED_MATERIAL, SelectedNewMaterial);
        EventSystem<RawImage, Texture, Texture>.Unsubscribe(EventType.IMAGE_CHANGED, OnImageChanged);
    }

    private void SelectedNewMaterial(MaterialSkeleton newMaterialSkeleton, Transform newMaterialTransformKey)
    {
        Debug.Log("got selected");
        materialTransformKey = newMaterialTransformKey;
        materialSkeleton = newMaterialSkeleton;
        
        albedoImage.texture = newMaterialSkeleton.baseMap;
    }

    public void OnImageChanged(RawImage image, Texture texture1, Texture texture2)
    {
        if (image == albedoImage)
        {
            Debug.Log("edited setting material");
            materialSkeleton.baseMap = texture1;
            EventSystem<MaterialSkeleton, Transform>.RaiseEvent(EventType.UPDATED_MATERIAL, materialSkeleton, materialTransformKey);
            EventSystem.RaiseEvent(EventType.UPDATE_RENDERER);
        }
        
    }
}
