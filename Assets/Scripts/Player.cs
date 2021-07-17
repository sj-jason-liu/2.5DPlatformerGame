using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    private CharacterController _controller;

    [SerializeField]
    private float _moveSpeed = 5f;
    [SerializeField]
    private float _gravity = 1f;
    [SerializeField]
    private float _jumpHeight = 15f;
    private float _yVelocity;

    //variable for player coins
    private int _coin = 0;
    private int _lives = 3;

    private bool _hasDoubleJump;

    void Start()
    {
        _controller = GetComponent<CharacterController>();
        UIManager.Instance.UpdateLifeDisplay(_lives);
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalMove = Input.GetAxis("Horizontal");
        Vector3 movement = new Vector3(horizontalMove, 0, 0);
        Vector3 velocity = movement * _moveSpeed;
        if (_controller.isGrounded)
        {
            _hasDoubleJump = false;
            if (Input.GetKeyDown(KeyCode.Space))
            {
                _yVelocity = _jumpHeight;
            }
        }
        else
        {
            if (!_hasDoubleJump && Input.GetKeyDown(KeyCode.Space))
            {
                _yVelocity += _jumpHeight;
                _hasDoubleJump = true;
            }
            else
            {
                _yVelocity -= _gravity;
            }
        }

        velocity.y = _yVelocity;
        _controller.Move(velocity * Time.deltaTime);
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
}
