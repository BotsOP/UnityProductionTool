using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MaterialList : MonoBehaviour
{
    public MeshRenderer planeTarget;
    public GameObject materialItemUI;
    public Transform panel;
    private List<MaterialItem> materials = new List<MaterialItem>();
    private void OnEnable()
    {
        EventSystem<Material>.Subscribe(EventType.FINISHED_MATERIAL, AddMaterial);
        EventSystem.Subscribe(EventType.UPDATE_RENDERER, UpdateRender);
    }

    private void OnDisable()
    {
        EventSystem<Material>.Unsubscribe(EventType.FINISHED_MATERIAL, AddMaterial);
        EventSystem.Unsubscribe(EventType.UPDATE_RENDERER, UpdateRender);
    }
    
    public void OpenNewMatWindow()
    {
        EventSystem.RaiseEvent(EventType.OPEN_MAT_WINDOW);
    }

    private void AddMaterial(Material newMaterial)
    {
        GameObject materialUI = Instantiate(materialItemUI, panel);
        MaterialItem materialItem = materialUI.GetComponent<MaterialItem>();
        materialItem.material = newMaterial;
        materials.Add(materialItem);
        Debug.Log(materialItem.MaterialIndex);
        UpdateRender();
    }

    private void UpdateRender()
    {
        //get top mat
        Material topMaterial = null;
        
        foreach (var materialItem in materials)
        {
            if (materialItem.MaterialIndex == 0)
            {
                Debug.Log("found material");
                topMaterial = materialItem.material;
            }
        }

        planeTarget.material = topMaterial;
    }
    
}
