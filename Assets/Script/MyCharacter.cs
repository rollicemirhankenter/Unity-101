using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyCharacter : MonoBehaviour
{
    public CharacterController _characterController;
    public MeshRenderer _meshRenderer;
    public float movementSpeed;
    public float jumpHeight = 1f;

    private Vector3 _playerVelocity;

    private const float GravityValue = -9.81f;

    private bool _isGrounded;
    private bool _isDead;
    private bool _isFinished;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (_isDead || _isFinished) return;
        _isGrounded = _characterController.isGrounded;

        if (_isGrounded && _playerVelocity.y < 0f)
        {
            _playerVelocity.y = 0f;
        }
        
        var horizontalInput = Input.GetAxis("Horizontal");

        var directionVector = new Vector3(horizontalInput, 0, 1);

        _characterController.Move(directionVector * (Time.deltaTime * movementSpeed));

        if (directionVector != Vector3.zero)
        {
            transform.forward = directionVector;
        }

        if (Input.GetButtonDown("Jump") && _isGrounded)
        {
            _playerVelocity.y += Mathf.Sqrt(jumpHeight * -2f * GravityValue);
        }

        _playerVelocity.y += GravityValue * Time.deltaTime;
        _characterController.Move(_playerVelocity * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Collectible") && other.TryGetComponent(out Collectible collectible))
        {
            collectible.OnCollected();
            var color = collectible.GetColor();
            _meshRenderer.material.color = color;
        }
        else if (other.CompareTag("FinishLine"))
        {
            Finish();
        }
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.CompareTag("Obstacle"))
        {
            Die();    
        }
    }

    private void Die()
    {
        if (_isDead) return;
        _isDead = true;
        Debug.Log("Died");
    }

    private void Finish()
    {
        if (_isFinished) return;
        _isFinished = true;
    }
}
