using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialList : MonoBehaviour
{
    private void OnEnable()
    {
        EventSystem<Material>.Subscribe(EventType.FINISHED_MATERIAL, AddMaterial);
    }

    private void OnDisable()
    {
        EventSystem<Material>.Unsubscribe(EventType.FINISHED_MATERIAL, AddMaterial);
    }
    
    public void OpenNewMatWindow()
    {
        EventSystem.RaiseEvent(EventType.OPEN_MAT_WINDOW);
    }

    private void AddMaterial(Material newMaterial)
    {
        
    }
    
}
