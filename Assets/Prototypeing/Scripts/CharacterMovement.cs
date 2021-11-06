using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private Collider _colider;
    [SerializeField] private Transform _transform;
    [SerializeField] private float _speed;
    [SerializeField] private float _rotationSpeed;

    private bool _hasRgidbody = false;
    private bool _hasColider = false;
    private bool _hasTransform = false;
    

    private Vector3 Movement = new Vector3(0, 0, 0);
    private Vector3 Rotation = new Vector3(0, 0, 0);
    

    private void OnEnable()
    {
        _hasRgidbody = _rigidbody != null;
        _hasColider = _colider != null;
        _hasTransform = _transform != null;
    }

    private void Update()
    {
        if (_hasTransform)
        {
            _transform.Translate(Movement * (_speed * Time.deltaTime));          

            _transform.Rotate(Vector3.up * Rotation.y * (Time.deltaTime * _rotationSpeed));
        }
    }

    #region Callbacks

    public void Interact(InputAction.CallbackContext context)
    {

        if (context.started)
        {
            Debug.Log("interact");
        }
    }

    public void Move(InputAction.CallbackContext context)
    {
        if (context.performed || context.canceled)
        {
            Vector2 direction = context.ReadValue<Vector2>();

            Movement.x = direction.x;
            Movement.z = direction.y;

        }   
        
    }

    public void Rotate(InputAction.CallbackContext context)
    {
        if (context.performed || context.canceled)
        {
            Vector2 direction = context.ReadValue<Vector2>();

            Rotation.y = direction.x;
        }
        
    }

    #endregion

}
