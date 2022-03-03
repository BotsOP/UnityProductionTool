using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialItem : MonoBehaviour
{
    public Material material;
    public int MaterialIndex
    {
        get
        {
            return transform.GetSiblingIndex();
        }
    }
}
