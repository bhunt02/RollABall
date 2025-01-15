
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class InputManager: MonoBehaviour
{
    public UnityEvent<Vector3> onMove = new ();

    void Update()
    {
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
        
        if (Input.GetKey(KeyCode.Space))
        {
            moveForce += Vector3.up; 
        }
        
        onMove.Invoke(moveForce);
    }
}