using System;
using UnityEngine;

public class BallController : MonoBehaviour
{
    private InputManager _inputManager;
    private Rigidbody _rb;
    [SerializeField] [Range(0, 10f)] private float speed = 5f;
    [SerializeField] [Range(0, 10f)] private float jumpForce = 5f;
    private bool _midair = true;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _inputManager = GetComponent<InputManager>();
        _inputManager.onMove.AddListener((Vector3 moveVector) =>
        {
            if (_midair) return;
            if (Mathf.Abs(moveVector.y) > 0) _midair = true;
            _rb.AddForce(
                new Vector3(
                    moveVector.x * speed * Time.deltaTime,
                    moveVector.y * jumpForce,
                    moveVector.z * speed * Time.deltaTime
                ), 
                ForceMode.Impulse
            );
        });
    }
    
    void Update()
    {
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
