using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "LevelContainer", menuName = "LevelContainer")]
public class LevelContainer : ScriptableObject
{
    [SerializeField] private Level _level;

    public Level level { get { return _level; } }
}