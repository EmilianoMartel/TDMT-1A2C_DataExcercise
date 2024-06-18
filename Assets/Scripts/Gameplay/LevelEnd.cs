using System;
using UnityEngine;
using EventChannel;

namespace Gameplay
{
    public class LevelEnd : MonoBehaviour
    {
        [SerializeField] private EmptyAction _endLevel;

        private void OnTriggerEnter(Collider other)
        {
            //TODO DONE: Raise event through event system telling the game to show the win sequence.
            Debug.Log($"{name}: Player touched the flag!");
            _endLevel.InvokeEvent();
        }
    }
}
