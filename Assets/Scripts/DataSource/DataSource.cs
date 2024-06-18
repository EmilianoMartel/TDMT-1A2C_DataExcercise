using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DataSource
{
    public abstract class DataSource<T> : ScriptableObject
    {
        public T DataInstance { get; set; }
    }
}