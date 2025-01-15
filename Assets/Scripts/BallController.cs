using UnityEngine;

public class BallController : MonoBehaviour
{
    private MeshRenderer _meshRenderer;
    private InputManager _inputManager;
    private Rigidbody _rb;
    [SerializeField] [Range(0, 10f)] private float speed = 5f;
    [SerializeField] [Range(0, 1f)] private float jumpForce = 0.5f;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
        _rb = GetComponent<Rigidbody>();
        _inputManager = GetComponent<InputManager>();
        _inputManager.onMove.AddListener(moveVector =>
        {
            if (IsMidair()) return;
            
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
        if (Physics.Raycast(transform.position, -Vector3.up, Mathf.Infinity)) return;
        
        transform.position = Vector3.up * 0.5f;
        _rb.linearVelocity = Vector3.zero;
        _rb.angularVelocity = Vector3.zero;
    }

    private bool IsMidair()
    {
        return !Physics.Raycast(transform.position, Vector3.down, _meshRenderer.bounds.size.y / 2); 
    }
}
