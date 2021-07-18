using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    private CharacterController _controller;

    private Vector3 _movement, _velocity;
    private Vector3 _wallSurfaceNormal;

    [SerializeField]
    private float _moveSpeed = 5f;
    [SerializeField]
    private float _gravity = 1f;
    [SerializeField]
    private float _jumpHeight = 15f;
    private float _yVelocity;
    [SerializeField]
    private float _pushPower = 3f;

    //variable for player coins
    private int _coin = 0;
    private int _lives = 3;

    private bool _hasDoubleJump;
    private bool _canWallJump;

    void Start()
    {
        _controller = GetComponent<CharacterController>();
        UIManager.Instance.UpdateLifeDisplay(_lives);
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalMove = Input.GetAxis("Horizontal");
        
        if (_controller.isGrounded)
        {
            _movement = new Vector3(horizontalMove, 0, 0);
            _velocity = _movement * _moveSpeed;

            _hasDoubleJump = false;
            _canWallJump = false;
            if (Input.GetKeyDown(KeyCode.Space))
            {
                _yVelocity = _jumpHeight;
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if(!_hasDoubleJump && !_canWallJump)
                {
                    _yVelocity += _jumpHeight;
                    _hasDoubleJump = true;
                }
                else if(_canWallJump)
                {
                    _yVelocity = _jumpHeight;
                    _velocity = _wallSurfaceNormal * _moveSpeed;
                }
            }
            else
            {
                _yVelocity -= _gravity;
            }
        }

        _velocity.y = _yVelocity;
        _controller.Move(_velocity * Time.deltaTime);
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if(hit.transform.tag == "MovingBox")
        {
            Rigidbody rb = hit.collider.attachedRigidbody;
            if(rb != null)
            {
                Vector3 pushDir = new Vector3(hit.moveDirection.x, 0, 0);
                rb.velocity = _pushPower * pushDir;
            }
        }
        
        if (_controller.isGrounded == false && hit.transform.tag == "Wall")
        {
            _wallSurfaceNormal = hit.normal;
            _canWallJump = true;
        }
    }

    public void CoinUpdate()
    {
        _coin++;
        UIManager.Instance.UpdateCoinDisplay(_coin);
    }

    public void Damage()
    {
        _lives--;
        UIManager.Instance.UpdateLifeDisplay(_lives);
        if (_lives <= 0)
        {
            SceneManager.LoadScene(0);
        }
    }

    public int GetCoinCounts()
    {
        return _coin;
    }
}
