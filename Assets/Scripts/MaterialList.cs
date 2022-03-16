using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MaterialList : MonoBehaviour
{
    [SerializeField] private MeshRenderer planeTarget;
    [SerializeField] private GameObject materialItemUI;
    [SerializeField] private Transform panel;
    private List<MaterialItem> materials = new List<MaterialItem>();
    private void OnEnable()
    {
        EventSystem<MaterialSkeleton>.Subscribe(EventType.FINISHED_MATERIAL, AddMaterial);
        EventSystem.Subscribe(EventType.UPDATE_RENDERER, UpdateRender);
    }

    private void OnDisable()
    {
        EventSystem<MaterialSkeleton>.Unsubscribe(EventType.FINISHED_MATERIAL, AddMaterial);
        EventSystem.Unsubscribe(EventType.UPDATE_RENDERER, UpdateRender);
    }
    
    public void OpenNewMatWindow()
    {
        EventSystem.RaiseEvent(EventType.OPEN_MAT_WINDOW);
    }

    private void Update()
    {
        // foreach (var mat in materials)
        // {
        //     mat.UpdateMaterial(new MaterialSkeleton());
        // };
    }

    private void AddMaterial(MaterialSkeleton newMaterial)
    {
        GameObject materialUI = Instantiate(materialItemUI, panel);
        materialUI.transform.SetSiblingIndex(0);
        MaterialItem materialItem = materialUI.GetComponent<MaterialItem>();
        materialItem.materialSkeleton = newMaterial;
        materials.Insert(0, materialItem);
        Debug.Log(materialItem.MaterialIndex);
        UpdateRender();
    }

    private void UpdateRender()
    {
        for (int i = 0; i < materials.Count; i++)
        {
            Move(materials, i, materials[i].MaterialIndex);
        }
        for (int i = 0; i < materials.Count; i++)
        {
            //Debug.Log(i + "    " + materials[i].MaterialIndex);
        }

        for (int i = materials.Count - 2; i >= 0; i--)
        {
            Debug.Log(i);
            materials[i].UpdateMaterial(materials[i + 1].material);
        }
        
        materials[materials.Count - 1].UpdateMaterial(new Material(Shader.Find("Universal Render Pipeline/Lit")));
        
        //get top mat
        planeTarget.material = materials[0].material;
    }
    
    public void Move<T>(List<T> list, int oldIndex, int newIndex)
    {
        T item = list[oldIndex];
        list.RemoveAt(oldIndex);
        list.Insert(newIndex, item);
    }
    
}
