using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snake : MonoBehaviour
{
    [SerializeField]
    private Transform _head;

    [SerializeField]
    private float _segmentDistance;

    [SerializeField]
    private float _speed;

    [SerializeField]
    private List<Transform> _segments = new List<Transform>();


    private List<Vector2> _positions = new List<Vector2>();


    private bool _isMoving = false;

    private void Awake()
    {
        _positions.Add(_head.position);
        for(int i =0; i < _segments.Count; i++)
        {
            _positions.Add(_segments[i].position);
        }
        StartCoroutine(IMoveSegments());
    }

    private void Update()
    {

        if (Input.GetKey(KeyCode.W))
        {
            Move(Vector2.up);
        }

        if (Input.GetKey(KeyCode.S))
        {
            Move(Vector2.down);
        }

        if (Input.GetKey(KeyCode.A))
        {
            Move(Vector2.left);
        }

        if (Input.GetKey(KeyCode.D))
        {
            Move(Vector2.right);
        }

    }

    public void Move(Vector2 direction)
    {

        _head.position = (Vector2)_head.position + direction * _speed * Time.deltaTime;
    }

    private IEnumerator IMoveSegments()
    {
        while (true)
        {

            float distance = ((Vector2)_head.position - _positions[0]).magnitude;
            if (distance > _segmentDistance)
            {
                // Ќаправление от старого положени€ головы, к новому
                Vector2 direction = ((Vector2)_head.position - _positions[0]).normalized;

                _positions.Insert(0, _positions[0] + direction * _segmentDistance);
                _positions.RemoveAt(_positions.Count - 1);

                distance -= _segmentDistance;
            }

            for (int i = 0; i < _segments.Count; i++)
            {
                _segments[i].position = Vector2.Lerp(_positions[i + 1], _positions[i], distance / _segmentDistance);
                Vector3 direction = ( (i == 0) ? _head.position : _segments[i - 1].position);
                _segments[i].LookAt2D(direction);
            }

            yield return null;
        }
    }

}
