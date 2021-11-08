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
    [SerializeField] private GameObject _rayCaster;

    private bool _hasRgidbody = false;
    private bool _hasColider = false;
    private bool _hasTransform = false;
    private bool _UiOpen = false;
    

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
        if (_hasTransform && !_UiOpen)
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
            RaycastHit hit;
            if (_rayCaster != null && !_UiOpen)
            {
                if (Physics.Raycast(_rayCaster.transform.position, _rayCaster.transform.TransformDirection(Vector3.forward),out hit, 10 ))
                {
                    DialogueManager.GetInstance().TriggerInteractionByTag(hit.collider.gameObject.tag);
                    _UiOpen = true;
                }
            }
            else
            {
                _UiOpen = DialogueManager.GetInstance().ContinueStory();
            }
        }
    }

    public void Move(InputAction.CallbackContext context)
    {
        if (_UiOpen)
        {
            Movement.x = 0;
            Movement.z = 0;
            return;
        }
        
        
        
        if (context.performed || context.canceled)
        {
            Vector2 direction = context.ReadValue<Vector2>();

            Movement.x = direction.x;
            Movement.z = direction.y;

        }   
        
    }

    public void Rotate(InputAction.CallbackContext context)
    {
        if (_UiOpen)
        {
            Rotation.y = 0;
            return;
        }
        
        if (context.performed || context.canceled)
        {
            Vector2 direction = context.ReadValue<Vector2>();

            Rotation.y = direction.x;
        }
        
    }

    #endregion

}
