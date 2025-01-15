using System;
using UnityEngine;

public class BallController : MonoBehaviour
{
    private Rigidbody _rb;
    private const float Speed = 5f;
    private const float JumpForce = 5f;
    private bool _midair = true;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        var magnitude = Time.deltaTime * Speed;
        var moveForce = Vector3.zero;
        
        if (Input.GetKey(KeyCode.W))
        {
            moveForce += Vector3.forward;
            //transform.Translate(Vector3.forward * magnitude);
        }
        if (Input.GetKey(KeyCode.S))
        {
            moveForce += Vector3.back;
            //transform.Translate(Vector3.back * magnitude);
        } 
        if (Input.GetKey(KeyCode.A))
        {
            moveForce += Vector3.left;
            //transform.Translate(Vector3.left * magnitude);
        }
        if (Input.GetKey(KeyCode.D))
        {
            moveForce += Vector3.right;
            //transform.Translate(Vector3.right * magnitude);
        } 
        
        else if (Input.GetKey(KeyCode.Space))
        {
            if (!_midair)
            {
                _midair = true;
                _rb.AddForce(Vector3.up * JumpForce, ForceMode.Impulse);
            }
        }

        if (moveForce != Vector3.zero && !_midair)
        {
            _rb.AddForce(moveForce * magnitude, ForceMode.Impulse);
        }

        if (!Physics.Raycast(transform.position, -Vector3.up, Mathf.Infinity))
        {
            transform.position = Vector3.up * 0.5f;
            _rb.linearVelocity = Vector3.zero;
            _rb.angularVelocity = Vector3.zero;
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            _midair = false;
        }
    }
}
