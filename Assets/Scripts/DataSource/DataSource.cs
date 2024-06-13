using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DataSource<T> : ScriptableObject
{
    public T DataInstance { get; set; }
}