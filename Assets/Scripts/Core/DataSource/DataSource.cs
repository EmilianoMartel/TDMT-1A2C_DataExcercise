using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core.DataSource
{
    public abstract class DataSource<T> : ScriptableObject
    {
        public T DataInstance { get; set; }
    }
}