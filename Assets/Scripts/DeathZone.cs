using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathZone : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.name == "Ball")
        {
            Destroy(collider.gameObject);
        }
        else if (collider.gameObject.tag == "Enemy")
        {
            collider.gameObject.GetComponent<Enemy>().Death();
        }
    }
}
