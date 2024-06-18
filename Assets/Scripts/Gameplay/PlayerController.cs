using Characters;
using UnityEngine;
using DataSource;

namespace Gameplay
{
    [RequireComponent(typeof(Character))]
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private Vector2Channel _directionEvent;
        [SerializeField] private BoolChannel _runEvent;
        [SerializeField] private PlayerSource _playerSource;

        private Character _character;

        private void Reset()
        {
            _character = GetComponent<Character>();
        }

        private void Awake()
        {
            WarningsEventChannels();

            _character ??= GetComponent<Character>();
            if (_character)
            {
                _character.enabled = false;
            }
        }

        private void OnEnable()
        {
            //TODO DONE: Subscribe to inputs via event manager/event channel
            //TODO DONE: Set itself as player reference via ReferenceManager/DataSource
            if(_playerSource != null)
                _playerSource.DataInstance = this;
            
            _directionEvent?.Sucription(HandleMove);
            _runEvent?.Sucription(HandleRun);
        }

        private void OnDisable()
        {
            //TODO DONE: Unsubscribe from all inputs via event manager/event channel
            //TODO DONE: Remove itself as player reference via reference manager/dataSource

            if (_playerSource != null)
                _playerSource.DataInstance = null;

            _directionEvent?.Unsuscribe(HandleMove);
            _runEvent?.Unsuscribe(HandleRun);
        }

        public void SetPlayerAtLevelStartAndEnable(Vector3 levelStartPosition)
        {
            transform.position = levelStartPosition;
            _character.enabled = true;
        }
        
        private void HandleMove(Vector2 direction)
        {
            _character.SetDirection(new Vector3(direction.x, 0, direction.y));
        }

        private void HandleRun(bool shouldRun)
        {
            if (shouldRun)
                _character.StartRunning();
            else
                _character.StopRunning();
        }

        private void WarningsEventChannels()
        {
            if (!_directionEvent)
            {
                Debug.LogWarning($"{name}: Direction event is null.\nIf player doesnt move could be for that.");
            }
            if (!_runEvent)
            {
                Debug.LogWarning($"{name}: Direction event is null.\nIf player doesnt move could be for that.");
            }
            if (!_playerSource)
            {
                Debug.LogWarning($"{name}: Player Souce is null.");
            }
        }
    }
}
