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

        private void Awake()
        {
            WarningAlert();
        }

        private void Start()
        {
            if (_targetSource != null)
                _targetSource.DataInstance = this;
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
            //TODO DONT: Raise event through event system telling the game to show the defeat sequence.
            Debug.Log($"{name}: received an attack!");
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