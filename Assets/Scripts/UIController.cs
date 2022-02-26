using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIController : MonoBehaviour
{
    public TMP_InputField input;
    

    private CommandStack commandStack = new CommandStack();
    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z) && !input.isFocused)
        {
            Debug.Log("undo");
            commandStack.UndoLastCommand();
        }
        
    }

    public void InputChanged(TMP_InputField _input)
    {
        //dictionary with previous value?
        Debug.Log(input.isFocused);
        if (input.isFocused)
        {
            commandStack.ExecuteCommand(new InputCommand(_input, _input.text));
            
        }
    }
}
