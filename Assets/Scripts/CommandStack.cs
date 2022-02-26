using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandStack
{
    private Stack<ICommand> commandHistory = new Stack<ICommand>();

    public void ExecuteCommand(ICommand command)
    {
        command.Execute();
        commandHistory.Push(command);
    }

    public void UndoLastCommand()
    {
        if (commandHistory.Count <= 0)
        {
            return;
        }
        
        commandHistory.Pop().Undo();
    }
    
}
