using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DesignPatterns.Command
{
    // interface to wrap your actions in a "command object"
    public interface ICommand
    {
        public void Execute();
        public void Undo();
    }
}