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
        materialUI.transform.SetSiblingIndex(0);
        MaterialItem materialItem = materialUI.GetComponent<MaterialItem>();
        materialItem.material = newMaterial;
        materials.Insert(0, materialItem);
        Debug.Log(materialItem.MaterialIndex);
        UpdateRender();
    }

    private void UpdateRender()
    {

        if (materials.Count > 1)
        {
            Debug.Log("Doing something");
            //Get second last mat
            Material secondLastMaterial = materials[materials.Count - 2].material;
            Material lastMaterial = materials[materials.Count - 1].material;
            Debug.Log(materials[materials.Count - 2].MaterialIndex);
            Debug.Log(materials[materials.Count - 1].MaterialIndex);
        
            Material newMaterial = new Material(Shader.Find("Shader Graphs/BasicCombineShader"));
            newMaterial.SetTexture("Albedo_", secondLastMaterial.GetTexture("Albedo_"));
            newMaterial.SetTexture("Normal_", secondLastMaterial.GetTexture("Normal_"));
            newMaterial.SetTexture("AmbientOcclusion_", secondLastMaterial.GetTexture("AmbientOcclusion_"));
        
            newMaterial.SetTexture("Albedo2_", lastMaterial.GetTexture("Albedo_"));
            newMaterial.SetTexture("Normal2_", lastMaterial.GetTexture("Normal_"));
            newMaterial.SetTexture("AmbientOcclusion2_", lastMaterial.GetTexture("AmbientOcclusion_"));

            materials[materials.Count - 2].material = newMaterial;
        }
        
        //get top mat
         Material topMaterial = null;
        
         foreach (var materialItem in materials)
         {
             if (materialItem.MaterialIndex == 0)
             {
                 Debug.Log("found material   " + materialItem.MaterialIndex);
                 topMaterial = materialItem.material;
             }
         }
        
         planeTarget.material = topMaterial;
    }
    
}
