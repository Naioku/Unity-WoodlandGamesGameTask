using System.Collections.Generic;
using UnityEngine;

namespace _PROJECT.Scripts.Combat
{
    [ExecuteInEditMode]
    public class AISensor : MonoBehaviour
    {
        [SerializeField] private float distance = 10f;
        [SerializeField] [Range(0f, 360f)] private float angle = 30f;
        [SerializeField] private float height = 1f;
        [SerializeField] private Color meshColor = new Color(0.3657968f, 1, 0.3254717f, 0.38f);
        [SerializeField] private Color rangeGizmosColor = Color.green;
        [SerializeField] private Color objectsInRangeGizmosColor = Color.red;
        [SerializeField] private Color objectsInSightGizmosColor = Color.blue;
        [SerializeField] [Tooltip("Times / second")] private int scanFrequency = 30;
        [SerializeField] private LayerMask searchingLayers;
        [SerializeField] private LayerMask occlusionLayers;
        
        private readonly List<GameObject> _objects = new List<GameObject>();
        private Mesh _mesh;
        private readonly Collider[] _colliders = new Collider[50];
        private int _count;
        private float _scanTimer;

        private float ScanInterval => 1f / scanFrequency;
        private float HalfAngle => angle / 2;

        void Update()
        {
            _scanTimer -= Time.deltaTime;
            if (_scanTimer <= 0f)
            {
                _scanTimer += ScanInterval;
                Scan();
            }
        }

        private void OnValidate()
        {
            _mesh = CreateWedgeMesh();
        }

        private void OnDrawGizmos()
        {
            if (!_mesh) return;
            DrawSensorMesh();
            DrawRange();
            HighlightObjectsRange();
            HighlightObjectsInSight();
        }

        private void Scan()
        {
            _count = Physics.OverlapSphereNonAlloc
                (transform.position, distance, _colliders, searchingLayers, QueryTriggerInteraction.Collide);
            
            _objects.Clear();
            for (int i = 0; i < _count; i++)
            {
                GameObject gameObj = _colliders[i].gameObject;
                if (IsInSight(gameObj))
                {
                    _objects.Add(gameObj);
                }
            }
        }

        private Mesh CreateWedgeMesh()
        {
            var mesh = new Mesh();

            int segments = 20;
            int trianglesQuantity = segments * 4 + 2 + 2;
            int verticesQuantity = trianglesQuantity * 3;

            Vector3[] vertices = new Vector3[verticesQuantity];
            int[] triangles = new int[verticesQuantity];
            
            Vector3 bottomCenter = Vector3.zero;
            Vector3 bottomLeft = Quaternion.Euler(0f, -HalfAngle, 0f) * Vector3.forward * distance;
            Vector3 bottomRight = Quaternion.Euler(0f, HalfAngle, 0f) * Vector3.forward * distance;
            
            Vector3 topCenter = bottomCenter + Vector3.up * height;
            Vector3 topLeft = bottomLeft + Vector3.up * height;
            Vector3 topRight = bottomRight + Vector3.up * height;

            int verticesIndex = 0;
            
            // left side
            vertices[verticesIndex++] = bottomCenter;
            vertices[verticesIndex++] = bottomLeft;
            vertices[verticesIndex++] = topLeft;
            
            vertices[verticesIndex++] = topLeft;
            vertices[verticesIndex++] = topCenter;
            vertices[verticesIndex++] = bottomCenter;
            
            // right side
            vertices[verticesIndex++] = bottomCenter;
            vertices[verticesIndex++] = topCenter;
            vertices[verticesIndex++] = topRight;
            
            vertices[verticesIndex++] = topRight;
            vertices[verticesIndex++] = bottomRight;
            vertices[verticesIndex++] = bottomCenter;

            float currentAngle = -HalfAngle;
            float deltaAngle = HalfAngle * 2 / segments;
            for (int i = 0; i < segments; i++)
            {
                bottomLeft = Quaternion.Euler(0f, currentAngle, 0f) * Vector3.forward * distance;
                bottomRight = Quaternion.Euler(0f, currentAngle + deltaAngle, 0f) * Vector3.forward * distance;
            
                topLeft = bottomLeft + Vector3.up * height;
                topRight = bottomRight + Vector3.up * height;
                
                // far side
                vertices[verticesIndex++] = bottomLeft;
                vertices[verticesIndex++] = bottomRight;
                vertices[verticesIndex++] = topRight;
            
                vertices[verticesIndex++] = topRight;
                vertices[verticesIndex++] = topLeft;
                vertices[verticesIndex++] = bottomLeft;
            
                // top side
                vertices[verticesIndex++] = topCenter;
                vertices[verticesIndex++] = topLeft;
                vertices[verticesIndex++] = topRight;
            
                // bottom side
                vertices[verticesIndex++] = bottomCenter;
                vertices[verticesIndex++] = bottomRight;
                vertices[verticesIndex++] = bottomLeft;
                
                currentAngle += deltaAngle;
            }
            
            for (int i = 0; i < verticesQuantity; i++)
            {
                triangles[i] = i;
            }

            mesh.vertices = vertices;
            mesh.triangles = triangles;
            mesh.RecalculateNormals();

            return mesh;
        }
        
        private void DrawSensorMesh()
        {
            Gizmos.color = meshColor;
            Gizmos.DrawMesh(_mesh, transform.position, transform.rotation);
        }

        private void DrawRange()
        {
            Gizmos.color = rangeGizmosColor;
            Gizmos.DrawWireSphere(transform.position, distance);
        }

        private void HighlightObjectsRange()
        {
            Gizmos.color = objectsInRangeGizmosColor;
            for (int i = 0; i < _count; i++)
            {
                Gizmos.DrawSphere(_colliders[i].transform.position, 0.2f);
            }
        }

        private void HighlightObjectsInSight()
        {
            Gizmos.color = objectsInSightGizmosColor;
            foreach (GameObject gameObj in _objects)
            {
                Gizmos.DrawSphere(gameObj.transform.position, 0.2f);
            }
        }
        
        private bool IsInSight(GameObject gameObj)
        {
            Vector3 origin = transform.position;
            Vector3 destination = gameObj.transform.position;
            Vector3 distanceVector = destination - origin;
            
            if (!IsInVerticalSight(distanceVector))
            {
                return false;
            }

            distanceVector.y = 0f;
            float deltaAngle = Vector3.Angle(distanceVector, transform.forward);
            if (!IsInVerticalAngularSight(deltaAngle))
            {
                return false;
            }

            origin.y += height / 2;
            destination.y = origin.y;
            if (!IsVisible(origin, destination))
            {
                return false;
            }
            
            return true;
        }

        private bool IsVisible(Vector3 origin, Vector3 destination)
        {
            return !Physics.Linecast(origin, destination, occlusionLayers);
        }

        private bool IsInVerticalAngularSight(float deltaAngle)
        {
            return deltaAngle <= HalfAngle;
        }

        private bool IsInVerticalSight(Vector3 distanceVector)
        {
            return distanceVector.y >= 0f && distanceVector.y <= height;
        }
    }
}
