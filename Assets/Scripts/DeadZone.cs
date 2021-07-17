using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadZone : MonoBehaviour
{
    [SerializeField]
    private GameObject _startPosition;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Player player = other.GetComponent<Player>();
            if (player != null)
            {
                player.Damage();
            }

            CharacterController cc = other.GetComponent<CharacterController>();
            if(cc != null)
            {
                cc.enabled = false;
            }

            other.transform.position = _startPosition.transform.position;

            StartCoroutine(RestartController(cc));
        }
    }

    IEnumerator RestartController(CharacterController controller)
    {
        yield return new WaitForSeconds(0.5f);
        controller.enabled = true;
    }
}
