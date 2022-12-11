using UnityEngine;
using UnityEngine.EventSystems;
using System;
using System.Collections;

public class Joystick : MonoBehaviour, IDragHandler, IEndDragHandler, IBeginDragHandler
{

    [SerializeField]
    private Snake _snake;

    [Header("Joystick objs"), SerializeField]
    private Transform _mainJoystick;

    [SerializeField]
    private Transform _handle;

    [SerializeField]
    private float _moveRadius;

    private Vector2 _direction;

    public void OnBeginDrag(PointerEventData eventData)
    {
        StartCoroutine(IDragProcess());
    }

    //Максимальная длина, на которую может удаляться ручка от центра джойстика

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 inputPosition = Camera.main.ScreenToWorldPoint(eventData.position);

        Vector2 offset = Vector2.ClampMagnitude(inputPosition - (Vector2)_mainJoystick.position, _moveRadius);

        _direction = offset;

        //Расстояние между ручкой и зоной.

        _handle.position = (Vector2)_mainJoystick.position + Vector2.ClampMagnitude(offset, _moveRadius);
        //Ограничиваем позицию ручки "зоной"
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        StopAllCoroutines();
        _handle.localPosition = Vector3.zero;
    }

    private IEnumerator IDragProcess()
    {
        while (true)
        {
            _snake.Move(_direction);
            yield return null;
        }
    }

}
