using UnityEngine;
using Core.Interactions;
using DataSource;

namespace Characters
{
    public class Character : MonoBehaviour, ISteerable, ITarget
    {
        [SerializeField] private float speed = 2.5f;
        [SerializeField] private float runningSpeed = 5;
        private Vector3 _currentDirection = Vector3.zero;
        private bool _isRunning = false;
        [SerializeField] private ITargetDataSource _targetSource;
        [SerializeField] private BoolChannel _dieEvent;

        private void OnEnable()
        {
            if (_targetSource != null)
                _targetSource.DataInstance = this;
        }

        private void OnDisable()
        {
            if (_targetSource)
                _targetSource.DataInstance = null;
        }

        private void Awake()
        {
            WarningAlert();
        }

        private void Update()
        {
            var currentSpeed = _isRunning ? runningSpeed : speed;
            transform.Translate(_currentDirection * (Time.deltaTime * currentSpeed), Space.World);
        }

        public void SetDirection(Vector3 direction)
        {
            _currentDirection = direction;
        }

        public void StartRunning() => _isRunning = true;

        public void StopRunning() => _isRunning = false;

        public void ReceiveAttack()
        {
            //TODO DONE: Raise event through event system telling the game to show the defeat sequence.
            Debug.Log($"{name}: received an attack!");
            
            if(_dieEvent)
                _dieEvent.InvokeEvent(false);
        }

        private void WarningAlert()
        {
            if (_targetSource == null)
            {
                Debug.LogWarning($"{name}: Target source is null.");
            }
        }
    }
}