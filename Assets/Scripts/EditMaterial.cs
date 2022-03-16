using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EditMaterial : MonoBehaviour
{
    [SerializeField] private RawImage albedoImage;
    [SerializeField] private RawImage maskImage;
    [SerializeField] private GameObject matSettings;
    [SerializeField] private GameObject maskSettings;
    [SerializeField] private Color buttonPressed;
    [SerializeField] private Color buttonNotPressed;
    private Transform materialTransformKey;
    private Image lastButtonPressed;
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
        if (newMaterialTransformKey == null)
        {
            Debug.Log("received empty transofrm");
        }
        materialTransformKey = newMaterialTransformKey;
        materialSkeleton = newMaterialSkeleton;
        
        albedoImage.texture = newMaterialSkeleton.baseMap;
    }

    public void OnImageChanged(RawImage image, Texture texture1, Texture texture2)
    {
        if (materialSkeleton.baseMap != null)
        {
            materialSkeleton.baseMap = albedoImage.texture;
            materialSkeleton.maskTexture = maskImage.texture;
            EventSystem<MaterialSkeleton, Transform>.RaiseEvent(EventType.UPDATED_MATERIAL, materialSkeleton, materialTransformKey);
            EventSystem.RaiseEvent(EventType.UPDATE_RENDERER);
        }
    }

    public void ChangeMenuSettings(Image button)
    {
        if(lastButtonPressed)
            lastButtonPressed.color = buttonNotPressed;
        
        button.color = buttonPressed;
        lastButtonPressed = button;
        
        matSettings.SetActive(!matSettings.activeSelf);
        maskSettings.SetActive(!maskSettings.activeSelf);
    }
}
