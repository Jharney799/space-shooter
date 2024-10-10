using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidEnemy : MonoBehaviour
{
    [SerializeField]
    private float _rotateSpeed = 500.0f;
    [SerializeField]
    private float _speed = 5.0f;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.forward * _rotateSpeed * Time.deltaTime);
        transform.Translate(Vector3.down * _speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Player player = other.transform.GetComponent<Player>();

            if (player != null)
            {
                player.Damage();
            }
        }

        if (other.tag == "Laser")
        {
            Destroy(other.gameObject);
        }
    }
}
