using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy : MonoBehaviour
{
    [SerializeField]
    private float  _speed = 2f;
    [SerializeField]
    private GameObject _enemylaser;
    private player _player;
    private  Animator enemy_anim;
    private AudioSource enemysound;
    private float _firerate = 3.0f;
    private float _canfire = -1f;


     void Start()
    {
        _player = GameObject.Find("Player").GetComponent<player>();
        enemy_anim = gameObject.GetComponent<Animator>();
        enemysound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        CalculateMovement();
        if(Time.time > _canfire )
        {
            _firerate = Random.Range(3.0f, 7.0f);
            _canfire = Time.time + _firerate;
            GameObject enemylaser=  Instantiate(_enemylaser, transform.position, Quaternion.identity);
             laser[] lasers = enemylaser.GetComponentsInChildren<laser>();

            for (int i = 0;i< lasers.Length;i++)
            {
                lasers[i].AssignEnemyLaser();
            } 
        }
    }

    void CalculateMovement()
    {
        transform.Translate(Vector3.down * Time.deltaTime * _speed);
        if (transform.position.y < -5)
        {
            float randomX = Random.Range(-9f, 9f);
            transform.position = new Vector3(randomX, 7, 0);
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag =="Player")
        {
            player player = other.transform.GetComponent<player>();
            if (player != null)
            {
                player.Damage();
            }
            enemy_anim.SetTrigger("ondeath");
            _speed = 0f;
            enemysound.Play();
            Destroy(gameObject,1.5f);
        }

        else if (other.tag =="laser")
        {
            Destroy(other.gameObject);
            _player.ScoreUpadte();
            enemy_anim.SetTrigger("ondeath");
            _speed = 1f;
            enemysound.Play();
            Destroy(this.GetComponent<Collider2D>());
            Destroy(this.gameObject,1.5f);
        }
    }
}
