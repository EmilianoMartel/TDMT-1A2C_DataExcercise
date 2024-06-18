using System.Collections;
using UnityEngine;

namespace Gameplay
{
    public class LevelManager : MonoBehaviour
    {
        [SerializeField] private Transform levelStart;
        [SerializeField] private PlayerSource _playerSource;

        private PlayerController _playerController;

        private void Awake()
        {
            ValidateParameters();
        }

        private IEnumerator Start()
        {
            while (_playerController == null)
            {
                //TODO DONE: Get reference to player controller from ReferenceManager/DataSource
                if(_playerSource.DataInstance != null)
                    _playerController = _playerSource.DataInstance;

                yield return null;
            }
            _playerController.SetPlayerAtLevelStartAndEnable(levelStart.position);
        }
    
        private void ValidateParameters()
        {
            if (!_playerSource)
            {
                Debug.LogError($"{name}: Player source is null.\nCheck and assigned one.\nDisabling component.");
                enabled = false;
                return;
            }
            if (!levelStart)
            {
                Debug.LogError($"{name}: Level start is null.\nCheck and assigned one.\nDisabling component.");
                enabled = false;
                return;
            }
        }
    }
}
