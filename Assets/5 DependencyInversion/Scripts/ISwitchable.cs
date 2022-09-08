using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DesignPatterns.DIP
{
    public interface ISwitchable 
    {
        public bool IsActive { get; }

        public void Activate();
        public void Deactivate();
    }
}
