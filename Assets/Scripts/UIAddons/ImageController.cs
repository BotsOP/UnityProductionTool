using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageController : MonoBehaviour
{
    private RawImage image;
    private Texture originalTexture;
    private void OnEnable()
    {
        image = gameObject.GetComponent<RawImage>();
    }

    private void Update()
    {
        if (image.texture != originalTexture)
        {
            Debug.Log("IMAGE CHANGED");
            //EventSystem<RawImage, Texture>.RaiseEvent(EventType.IMAGE_CHANGED, image, image.texture);
            originalTexture = image.texture;
        }
    }
}
