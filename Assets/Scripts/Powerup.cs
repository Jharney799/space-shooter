using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    [SerializeField]
    private float _speed = 3.0f;

    [SerializeField]     //ID for powerups 0 = triple shot 1 = speed 2 = shield
    private int powerupID;

    [SerializeField]
    private AudioClip _powerupSoundsClip;


    void Update()
    {
        transform.Translate(Vector3.down * _speed *Time.deltaTime);

        if(transform.position.y < -5.46f)
        {
            Destroy(this.gameObject);
        }

    }

    //ontriggercollision
    //oinly be collectable by player (Use Tags)
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Player player = other.transform.GetComponent<Player>();

            AudioSource.PlayClipAtPoint(_powerupSoundsClip, transform.position);
            
            if (player !=null)
            {
                switch (powerupID)
                {
                    case 0:
                        player.TripleShotActive();
                        break;
                    case 1:
                        player.SpeedBoostActive();                       
                        break;
                    case 2:
                        player.ShieldPowerupActive();                       
                        break;
                    default:
                        Debug.Log("Default Value");
                        break;
                }
            }
            
            Destroy(this.gameObject);
        }
    }
}
