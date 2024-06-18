using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DataSource;

namespace Core.Interactions 
{
    [CreateAssetMenu(fileName = "ITargetSource", menuName = "DataSource/ITargetSource")]
    public class ITargetDataSource : DataSource<ITarget> { }
}