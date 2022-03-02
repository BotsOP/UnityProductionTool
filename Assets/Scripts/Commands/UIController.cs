using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIController : MonoBehaviour
{
    private Dictionary<TMP_InputField, string> previousInput = new Dictionary<TMP_InputField, string>();

    private CommandStack commandStack = new CommandStack();

    private void OnEnable()
    {
        EventSystem<RawImage, Texture, Texture>.Subscribe(EventType.IMAGE_CHANGED, ImageChanged);
    }

    private void OnDisable()
    {
        EventSystem<RawImage, Texture, Texture>.Unsubscribe(EventType.IMAGE_CHANGED, ImageChanged);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            commandStack.UndoLastCommand();
        }
    }

    private void ImageChanged(RawImage image, Texture newTexture, Texture originalTexture)
    {
        commandStack.ExecuteCommand(new ImageCommand(image, newTexture, originalTexture));
    }

    //works with unity's input onChanged events
    public void InputChanged(TMP_InputField input)
    {
        if (!previousInput.ContainsKey(input))
        {
            previousInput.Add(input, "");
        }

        if (input.isFocused)
        {
            commandStack.ExecuteCommand(new InputCommand(input, input.text, previousInput[input]));
        }

        previousInput[input] = input.text;
    }
}
