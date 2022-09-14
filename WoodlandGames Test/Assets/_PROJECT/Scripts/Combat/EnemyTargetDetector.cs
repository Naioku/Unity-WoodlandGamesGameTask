using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace _PROJECT.Scripts.Combat
{
    [ExecuteInEditMode]
    public class EnemyTargetDetector : MonoBehaviour
    {
        [SerializeField] private float radius = 5f;
        
        [Range(0,360)]
        [SerializeField] private float angle = 120f;

        [SerializeField] private float waitTimeBetweenChecking = 0.2f;

        private readonly HashSet<Transform> _listOfTargets = new HashSet<Transform>();

        private void Start()
        {
            StartCoroutine(FieldOfViewCoroutine());
        }

        private void OnDrawGizmos()
        {
            Handles.color = Color.white;
            
            var ownerPosition = transform.position;
            Handles.DrawWireArc(ownerPosition, Vector3.up, Vector3.forward, 360, radius);

            Vector3 viewAngle01 = DirectionFromAngle(-angle / 2);
            Vector3 viewAngle02 = DirectionFromAngle(angle / 2);

            Handles.color = Color.yellow;
            Handles.DrawLine(ownerPosition, ownerPosition + viewAngle01 * radius);
            Handles.DrawLine(ownerPosition, ownerPosition + viewAngle02 * radius);

            Handles.color = Color.green;
            foreach (var target in _listOfTargets)
            {
                Vector3 targetPosition = target.transform.position;
                if (target.TryGetComponent(out CapsuleCollider targetCollider))
                {
                    targetPosition += Vector3.up * targetCollider.height / 2;
                }
                
                Handles.DrawLine(transform.position, targetPosition);
            }
        }

        /// <summary>
        /// Coroutine used for better performance.
        /// </summary>
        /// <returns></returns>
        private IEnumerator FieldOfViewCoroutine()
        {
            while (true)
            {
                yield return new WaitForSecondsRealtime(waitTimeBetweenChecking);
                FieldOfViewCheck();
            }
        }

        private Vector3 DirectionFromAngle(float angleInDegrees)
        {
            angleInDegrees += transform.eulerAngles.y;

            return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
        }

        private void FieldOfViewCheck()
        {
            _listOfTargets.Clear();
            
            Collider[] rangeChecks = Physics.OverlapSphere(transform.position, radius);

            if (rangeChecks.Length != 0)
            {
                foreach (var potentialTarget in rangeChecks)
                {
                    Vector3 directionToTarget = (potentialTarget.transform.position - transform.position).normalized;
                    if (!IsTargetInAngularRange(directionToTarget)) continue;
                    
                    var ownerPosition = transform.position;
                    float distanceToTarget = Vector3.Distance(ownerPosition, potentialTarget.transform.position);
                    var ray = new Ray(ownerPosition, directionToTarget);
                    if (!Physics.Raycast(ray, out RaycastHit hit, distanceToTarget)) continue;
                    if (hit.transform.GetComponent<EnemyTarget>() == null) continue;
                        
                    _listOfTargets.Add(potentialTarget.transform);
                }
            }
        }

        private bool IsTargetInAngularRange(Vector3 directionToTarget)
        {
            return Vector3.Angle(transform.forward, directionToTarget) <= angle / 2;
        }
    }
}
