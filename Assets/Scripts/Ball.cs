using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Ball : MonoBehaviour
{
    public GameManager gameManager;
    Vector3 velocity;
    float z,x;
    [SerializeField ,Range(0f,1f)] private float speed = 0.1f;
    void Start()
    {
        ResetBall();
    }

    void ResetBall()
    {
        transform.position = Vector3.zero;
        z = Random.Range(0.2f, 1f);
        x = Random.Range(0.2f, 0.8f)*2 -1f * Random.Range(0.5f,1f);
        velocity = new Vector3(x, 0, z);
    }

    void FixedUpdate()
    {
        
        velocity = velocity.normalized * speed;
        transform.position += velocity;
    }

    private void OnCollisionEnter(Collision collision)
    {
        switch (collision.transform.name)
        {
            case "Bounds East":
            case "Bounds West":
                velocity.x *= -1f;
                return;
            case "Bounds North":
            case "Bounds South":
                ResetBall();
                gameManager.IncrementScore(collision.transform.name);
                return;
            case "Player Paddle":
            case "AI Paddle":
                velocity.z *= -1f;
                return;
            default:
                return;
        }
    }
}
