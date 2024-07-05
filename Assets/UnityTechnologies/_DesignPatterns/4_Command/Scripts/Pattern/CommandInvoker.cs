using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DesignPatterns.Command
{
public class CommandInvoker
{
    // Stack of command objects to undo
    private static Stack<ICommand> s_UndoStack = new Stack<ICommand>();

    // Second stack of redoable commands
    private static Stack<ICommand> s_RedoStack = new Stack<ICommand>();

    // Execute a command object directly and save to the undo stack
    public static void ExecuteCommand(ICommand command)
    {
        command.Execute();
        s_UndoStack.Push(command);

        // Clear out the redo stack if we make a new move
        s_RedoStack.Clear();
    }

    public static void UndoCommand()
    {
        // If we have commands to undo
        if (s_UndoStack.Count > 0)
        {
            ICommand activeCommand = s_UndoStack.Pop();
            s_RedoStack.Push(activeCommand);
            activeCommand.Undo();
        }
    }

    public static void RedoCommand()
    {
        // If we have commands to redo
        if (s_RedoStack.Count > 0)
        {
            ICommand activeCommand = s_RedoStack.Pop();
            s_UndoStack.Push(activeCommand);
            activeCommand.Execute();
        }
    }
}
}