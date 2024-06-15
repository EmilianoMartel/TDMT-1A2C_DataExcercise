using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "ScenaryContainer", menuName = "ScenaryContainer")]
public class ScenaryContainer : ScriptableObject
{
    [SerializeField] private SceneLevel _scene;

    public SceneLevel scene { get { return _scene; } }
}