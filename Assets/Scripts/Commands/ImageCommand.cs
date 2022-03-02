using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageCommand : ICommand
{
    private RawImage image;
    private Texture originalTexture;
    private Texture newTexture;

    public ImageCommand(RawImage image, Texture newTexture, Texture originalTexture)
    {
        this.image = image;
        this.originalTexture = originalTexture;
        this.newTexture = newTexture;
    }
    
    public void Execute()
    {
        image.texture = newTexture;
    }

    public void Undo()
    {
        image.texture = originalTexture;
    }
}
