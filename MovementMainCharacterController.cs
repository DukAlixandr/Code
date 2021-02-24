using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class MovementMainCharacterController : MonoBehaviour {
    [SerializeField] private float _speedMove;
    [SerializeField] private GameObject _MainCharacter;

    [SerializeField] private Camera _camera1;
    [SerializeField] private Camera _camera2;
    [SerializeField] private Camera _camera3;
    [SerializeField] private Camera _camera4;

    private float _gravityForce;
    private Vector3 _moveVector;
    private CharacterController _mainCharacterController;
    private Animator _mainCharacterAnimator;
    
    void Start()
    {
        _mainCharacterController = GetComponent<CharacterController>();
        _mainCharacterAnimator = GetComponent<Animator>();      
    }
    void FixedUpdate()
    {
        CharacterMove();
        GamingGravity();
        CameraConrtroler();
    }
    private void CharacterMove()
    {
        _moveVector = Vector3.zero;
        if(_camera1.enabled==true || _camera2.enabled == true)
        {            
            _moveVector.x = -(Input.GetAxis("Horizontal") * _speedMove);
            _moveVector.z = -(Input.GetAxis("Vertical") * _speedMove);
        }
        if (_camera3.enabled == true || _camera4.enabled == true)
        {
            _moveVector.x = (Input.GetAxis("Horizontal") * _speedMove);
            _moveVector.z = (Input.GetAxis("Vertical") * _speedMove);
        }
        if (_moveVector.x != 0 || _moveVector.z != 0)
        {        
            _mainCharacterAnimator.SetBool("Walk", true);
        }
        else _mainCharacterAnimator.SetBool("Walk", false);

        if (Vector3.Angle(Vector3.forward, _moveVector) > 1f || Vector3.Angle(Vector3.forward, _moveVector) == 0)
        {
            Vector3 direct = Vector3.RotateTowards(transform.forward, _moveVector, _speedMove, 0f);
            transform.rotation = Quaternion.LookRotation(direct);
        }
        _moveVector.y = _gravityForce;
        _mainCharacterController.Move(_moveVector * Time.deltaTime);
       
    }
    private void GamingGravity()
    {
        if (!_mainCharacterController.isGrounded)
            _gravityForce -= 20f * Time.deltaTime;
        else
            _gravityForce = -1f;        
    }
    private void CameraConrtroler()
    {              
        if (_MainCharacter.transform.position.x < 2.2 )
        {
            _camera1.enabled = true;
            _camera2.enabled = false;
            _camera3.enabled = false;
            _camera4.enabled = false;
        }
        if (_MainCharacter.transform.position.x>2.2 && _MainCharacter.transform.position.z > 3)
        {
            _camera1.enabled = false;
            _camera2.enabled = true;
            _camera3.enabled = false;
            _camera4.enabled = false;
        }
        if (_MainCharacter.transform.position.z < -0 && _MainCharacter.transform.position.x< 12.5)
        {
            _camera1.enabled = false;
            _camera2.enabled = false;
            _camera3.enabled = true;
            _camera4.enabled = false;
        }
        if (_MainCharacter.transform.position.z < -0 && _MainCharacter.transform.position.x> 12.5)
        {
            _camera1.enabled = false;
            _camera2.enabled = false;
            _camera3.enabled = false;
            _camera4.enabled = true;
        }

    }
}
