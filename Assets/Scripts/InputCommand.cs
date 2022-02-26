using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InputCommand : ICommand
{
    private TMP_InputField input;
    private string originalValue;
    private string newValue;

    public InputCommand(TMP_InputField input, string newValue)
    {
        this.input = input;
        originalValue = input.text;
        this.newValue = newValue;
        Debug.Log( originalValue + "   " + newValue);
    }
    public void Execute()
    {
        Debug.Log( originalValue + "   " + newValue);
        input.text = newValue;
    }
    public void Undo()
    {
        Debug.Log( originalValue + "   " + newValue);
        input.text = originalValue;
    }
}
