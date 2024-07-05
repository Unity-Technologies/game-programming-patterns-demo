using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DesignPatterns.StatePattern
{
    // extra interface added just for the example project,
    // used to change the player's color

    public interface IColorable 
    {
        public Color MeshColor { get; set; }
    }
}
