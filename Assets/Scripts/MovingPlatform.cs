using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField]
    private Transform _targetA, _targetB;
    private Transform _selectedTarget;
    
    [SerializeField]
    private float _speed = 1f;

    private int _movementID = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        switch (_movementID)
        {
            case 0:
                _selectedTarget = _targetB;
                break;
            case 1:
                _selectedTarget = _targetA;
                break;
            default:
                _selectedTarget = _targetB;
                break;
        }
        transform.position = Vector3.MoveTowards(transform.position, _selectedTarget.position, _speed * Time.deltaTime);
        if(transform.position == _targetB.position)
        {
            _movementID = 1;
        }
        else if(transform.position == _targetA.position)
        {
            _movementID = 0;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            other.transform.parent = transform;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player")
        {
            other.transform.parent = null;
        }
    }
}
