using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DataSource;
using Gameplay;

namespace Gameplay
{
    [CreateAssetMenu(fileName = "PlayerSource", menuName = "DataSource/PlayerSource")]
    public class PlayerSource : DataSource<PlayerController> { }
}