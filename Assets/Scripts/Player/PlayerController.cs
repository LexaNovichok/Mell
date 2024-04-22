using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController : MonoBehaviour
{
    [SerializeField] float _speed;
    public float _jumpForce;
    public float _moveInput;

    private Rigidbody2D rb;

    [SerializeField] private Joystick _joystick;
    

    private void Start()
    {
        //DontDestroyOnLoad(gameObject);
        rb = GetComponent<Rigidbody2D>();
        if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer)
        {
            
        }
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        playerMove();
    }
    

    private void playerMove()
    {
        // Для ПК используем клавиши A и D
        if (Application.platform == RuntimePlatform.WindowsEditor || Application.platform == RuntimePlatform.WindowsPlayer)
        {
            _joystick.enabled = false;
            playerMovePC();
        }

        // Для мобильных устройств используем ввод с сенсорного экрана
        else if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer)
        {
            _joystick.enabled = true;
            playerMoveMobile();
        }
    }

    private void playerMovePC()
    {
        if (Input.GetKey(KeyCode.A))
        {
            Vector3 newPosition = transform.position - transform.right * Time.deltaTime * _speed;
            newPosition.x = Mathf.Clamp(newPosition.x, -11f, 11f);
            transform.position = newPosition;
        }

        if (Input.GetKey(KeyCode.D))
        {
            Vector3 newPosition = transform.position + transform.right  * Time.deltaTime * _speed;
            newPosition.x = Mathf.Clamp(newPosition.x, -11f, 11f);
            transform.position = newPosition;
        }
        if (Input.GetKey(KeyCode.Space))
        {
            transform.Translate(Vector2.up * Time.deltaTime * _jumpForce);
        }
    }

    private void playerMoveMobile()
    {
        _moveInput = _joystick.Horizontal;
        rb.velocity = new Vector2(_moveInput * _speed, rb.velocity.y);
    }

   
}
