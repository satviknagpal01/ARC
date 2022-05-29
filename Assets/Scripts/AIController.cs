using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : MonoBehaviour
{
    public Transform ball;

    [SerializeField, Range(0f, 1f)] private float skill;
    Vector3 newPos;
    void Update()
    {
        newPos = transform.position;
        newPos.x = Mathf.Lerp(transform.position.x,ball.position.x,skill);
        transform.position = newPos;
    }
}
