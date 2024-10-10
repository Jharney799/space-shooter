using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float _speed = 4.0f;

    private Player _player;

    private Animator _enemyDeath;

    private AudioSource _audioSource;

    [SerializeField]
    private GameObject _explosionPrefab;
    [SerializeField]
    private GameObject _laserPrefab;

    private float _fireRate = 3.0f;
    private float _canFire = -1;


    // Start is called before the first frame update
    void Start()
    {
        float randomX = Random.Range(9.44f, -9.44f);
        _audioSource = GetComponent<AudioSource>();

        transform.position = new Vector3(randomX, 7.4f, 0);

        _player = GameObject.Find("Player").GetComponent<Player>();

        if (_player == null)
        {
            Debug.LogError("Player is NULL");
        }

        _enemyDeath = GetComponent<Animator>();

        if (_enemyDeath == null)
        {
            Debug.LogError("Animator is Null");
        }

    }

    void Update()
    {
        CalculateMovement();

        if(Time.time > _canFire)
        {
            _fireRate = Random.Range(3.0f, 7f);
            _canFire = Time.time + _fireRate;
            GameObject enemyLaser = Instantiate(_laserPrefab, transform.position, Quaternion.identity);
            //two laser components to grab from prefab - store instantiated object into gameobject array or variable to access component.
            Laser[] lasers = enemyLaser.GetComponentsInChildren<Laser>();

            for (int i = 0; i < lasers.Length; i++)
            {
                lasers[i].AssignEnemyLaser();
            }
        }
    }

    void CalculateMovement()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);


        if (transform.position.y < -5.4f)
        {
            float randomX = Random.Range(9.44f, -9.44f);
            transform.position = new Vector3(randomX, 7.4f, 0);

        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            //damage player
            //script communication ^^ - has access to player through variable other and checks for player
            //can only directly access through the root which is the transform
            //.Damage is calling damage method from player component (script)
            //null checking component
            Player player = other.transform.GetComponent<Player>();

            if (player != null)
            {
                player.Damage();
            }

            _enemyDeath.SetTrigger("OnEnemyDeath");
            _speed = 0;

            _audioSource.Play();
            Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
            Destroy(this.gameObject);

        }

        if (other.tag == "Laser")
        {
            Destroy(other.gameObject);

            if (_player != null)
            {
                _player.AddScore(10);
            }

            _enemyDeath.SetTrigger("OnEnemyDeath");
            _speed = 0;

            _audioSource.Play();
            Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
            Destroy(this.gameObject);

        }

    }

}