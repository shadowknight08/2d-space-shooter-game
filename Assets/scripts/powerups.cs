using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class powerups : MonoBehaviour
{
    private float speed = 3.0f;
    [SerializeField]
    private int powerupID;
    [SerializeField]
    private AudioClip powerupSound;
    //private player _player;

    // Start is called before the first frame update
    void Start()
    {
       // _player = GameObject.Find("player").GetComponent<player>();   // this one is my method 
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * speed * Time.deltaTime);

        if(transform.position.y< -7 )
        {
            Destroy(this.gameObject);
        }
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag=="Player")
        {
            
            player player = other.transform.GetComponent<player>();
            AudioSource.PlayClipAtPoint(powerupSound, transform.position);
            if (player !=null)
            {
               /* if(powerupID==0)
                {
                    player.TripleShotActive();
                }
                else if(powerupID==1)
                {

                }
                else if(powerupID==2)
                {

                }*/

                switch(powerupID)  // more optimize way 
                {
                    case 0:
                        player.TripleShotActive();
                        break;
                    case 1:
                        player.SpeedbootsActive();
                        break;
                    case 2:
                      
                        player.ShieldActive();
                        break;
                }
                
               
            }
            Destroy(this.gameObject);
           // _player.TripleShotActive();
        }
    }
}
