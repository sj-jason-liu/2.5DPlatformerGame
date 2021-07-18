using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorPanel : MonoBehaviour
{
    [SerializeField]
    private GameObject _callButton;

    private Elevator _elevator;

    [SerializeField]
    private int _targetCoinCounts = 8;

    private bool _calledElevator = false;

    private void Start()
    {
        _elevator = GameObject.Find("Elevator").GetComponent<Elevator>();
        if(_elevator == null)
        {
            Debug.LogError("Elevator is null!");
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "Player")
        {
            Player player = other.GetComponent<Player>();
            if(player != null)
            {
                if (Input.GetKeyDown(KeyCode.E) && player.GetCoinCounts() >= _targetCoinCounts)
                {
                    if (_callButton != null)
                    {
                        if(_calledElevator)
                        {
                            _callButton.GetComponent<MeshRenderer>().material.color = Color.red;
                        }
                        else
                        {
                            _callButton.GetComponent<MeshRenderer>().material.color = Color.green;
                        }
                        _calledElevator = !_calledElevator;
                        _elevator.CallElevator();
                    }
                }
            } 
        }
    }
}
