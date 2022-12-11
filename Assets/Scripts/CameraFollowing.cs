using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowing : MonoBehaviour
{

    public GameObject _followingObject;


    void Update()
    {
        gameObject.transform.position = new Vector3(_followingObject.transform.position.x, _followingObject.transform.position.y, -10);

    }
}
