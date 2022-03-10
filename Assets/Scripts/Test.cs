using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Test : MonoBehaviour
{
    public Shader combineShader;
    private Material material;
    public Texture texture1;
    public Texture texture2;
    private RenderTexture renderTexture;

    public RawImage image;
    // Start is called before the first frame update
    void Start()
    {
        material = new Material(combineShader);
        renderTexture = new RenderTexture(4096, 4096, 16, RenderTextureFormat.ARGB32);
        renderTexture.Create();
    }

    // Update is called once per frame
    void Update()
    {
        material.SetTexture("_MainTex", texture1);
        material.SetTexture("_OnTopTex", texture2);
        
        Graphics.Blit(texture1, renderTexture, material);
        image.texture = renderTexture;
    }
}
