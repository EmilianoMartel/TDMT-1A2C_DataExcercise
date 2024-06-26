using Core.Interactions;
using System.Collections;
using UnityEngine;
using DataSource;

namespace AI
{
    public class EnemyBrain : MonoBehaviour
    {
        [SerializeField] private float attackDistance;
        [SerializeField] private ITargetDataSource _targetSource;
        private ISteerable _steerable;

        private void Awake()
        {
            _steerable = GetComponent<ISteerable>();
            if( _steerable == null)
            {
                Debug.LogError($"{name}: cannot find a {nameof(ISteerable)} component!");
                enabled = false;
            }
        }

        private void Start()
        {
            StartCoroutine(WaitForTarget());
        }

        private void Update()
        {
            //TODO DONE: Add logic to get the target from a source/reference system
            if (_targetSource.DataInstance == null)
                return;
            //          AB        =         B        -          A
            var directionToTarget = _targetSource.DataInstance.transform.position - transform.position;
            var distanceToTarget = directionToTarget.magnitude;
            if (distanceToTarget < attackDistance)
            {
                _targetSource.DataInstance.ReceiveAttack();
            }
            else
            {
                Debug.DrawRay(transform.position, directionToTarget.normalized, Color.red);
                _steerable.SetDirection(directionToTarget.normalized);
            }
        }

        private IEnumerator WaitForTarget()
        {
            while (_targetSource.DataInstance == null)
            {
                yield return null;
            }
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, attackDistance);
        }
    
        private void ValidateParameters()
        {
            if (!_targetSource)
            {
                Debug.LogError($"{name}: ITarget source is null.\nCheck and assigned one.\nDisabling component.");
                enabled = false;
                return;
            }
        }
    }
}
