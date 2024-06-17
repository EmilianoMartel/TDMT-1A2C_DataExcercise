using System.Collections;
using System.Collections.Generic;
using System.IO.Pipes;
using UnityEngine;

namespace Scenary
{
    [CreateAssetMenu(fileName = "LevelsContainer", menuName = "LevelsContainer")]
    public class LevelsContainer : ScriptableObject
    {
        [SerializeField] private List<ScenaryContainer> _levels = new();

        public List<ScenaryContainer> levels { get { return _levels; } }
    }
}