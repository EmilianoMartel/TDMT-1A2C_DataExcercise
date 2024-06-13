using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Level
{
    [field: SerializeField] public string SceneName { get; private set; }
    [field: SerializeField] public bool IsUnloadable { get; private set; }
}