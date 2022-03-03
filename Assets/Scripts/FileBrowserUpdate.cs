
using AnotherFileBrowser.Windows;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class FileBrowserUpdate : MonoBehaviour
{
    public void OpenFileBrowser(RawImage targetImage)
    {
        targetImage.color = Color.white;
        targetImage.transform.GetChild(0).GetChild(0).GetComponent<TMP_Text>().text = "";
        
        var bp = new BrowserProperties();
        bp.filter = "Image files (*.jpg, *.jpeg, *.jpe, *.jfif, *.png) | *.jpg; *.jpeg; *.jpe; *.jfif; *.png";
        bp.filterIndex = 0;

        new FileBrowser().OpenFileBrowser(bp, path =>
        {
            //Load image from local path with UWR
            StartCoroutine(LoadImage(path, targetImage));
        });
    }

    IEnumerator LoadImage(string path, RawImage targetImage)
    {
        using (UnityWebRequest uwr = UnityWebRequestTexture.GetTexture(path))
        {
            yield return uwr.SendWebRequest();

            if (uwr.result == UnityWebRequest.Result.ConnectionError || uwr.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.Log(uwr.error);
            }
            else
            {
                var uwrTexture = DownloadHandlerTexture.GetContent(uwr);
                Texture originalTexture = targetImage.texture;
                EventSystem<RawImage, Texture, Texture>.RaiseEvent(EventType.IMAGE_CHANGED, targetImage, uwrTexture, originalTexture);
            }
        }
    }
}
