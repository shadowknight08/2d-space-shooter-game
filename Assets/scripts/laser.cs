using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class laser : MonoBehaviour
{
    [SerializeField]
    private float _speed = 8f;
    private bool enemylaser = false;

    // Update is called once per frame
    void Update()
    {
        if(enemylaser==false)
        {
            MoveUp();
        }
        else
        {
            MoveDown();
        }
        
    }

    void MoveUp()
    {
        transform.Translate(Vector3.up * _speed * Time.deltaTime);

        if (transform.position.y >= 10)
        {
            if (gameObject.transform.parent != null)
            {
                Destroy(transform.parent.gameObject);
            }
            Destroy(gameObject);
        }
    }


    void MoveDown()
    {
        transform.Translate(Vector3.down* _speed * Time.deltaTime);

        if (transform.position.y <= -10)
        {
            if (gameObject.transform.parent != null)
            {
                Destroy(transform.parent.gameObject);
            }
            Destroy(gameObject);
        }
    }

    public void AssignEnemyLaser()
    {
        enemylaser = true;
    }
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (gameObject.transform.parent.tag == "Player" || gameObject.transform.parent.tag=="tripleshot")
        {
            return;
        }
        else
        {
            if (other.tag == "Player")
            {
                player player = other.GetComponent<player>();
                if (player != null)
                {
                    player.Damage();               
                }
                Destroy(this.gameObject);
            }
        }
    }
}
