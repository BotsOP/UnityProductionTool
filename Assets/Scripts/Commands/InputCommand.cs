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

    public InputCommand(TMP_InputField input, string newValue, string originalValue)
    {
        this.input = input;
        this.originalValue = originalValue;
        this.newValue = newValue;
    }
    public void Execute()
    {
        input.text = newValue;
    }
    public void Undo()
    {
        input.text = originalValue;
    }
}
