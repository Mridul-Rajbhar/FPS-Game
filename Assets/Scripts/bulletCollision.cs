using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletCollision : MonoBehaviour
{
    public int damage;

    private void OnCollisionEnter(Collision collision)
    {
        
        if (collision.gameObject.layer == LayerMask.NameToLayer("enemy") || collision.gameObject.layer == LayerMask.NameToLayer("ground") || collision.gameObject.layer == LayerMask.NameToLayer("wall"))
            Destroy(gameObject);
        else if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<playerMovement>().TakeDamage(damage);
            Destroy(gameObject);
        }


    }
}
