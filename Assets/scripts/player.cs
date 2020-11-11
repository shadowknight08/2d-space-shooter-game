using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class player : MonoBehaviour
{
    
    [SerializeField]
    private  float _speed = 10f;
    private float speedmulti = 2.0f;
    private float _firerate = 0.5f;
    private float _canfire = -1f;
    public int _lives = 3;
    private int _score;
    private int shieldlevel = 0;
    private int ammocount = 15;
   

    [SerializeField]
    private GameObject _laser;
    [SerializeField]
    private GameObject _tripleshot;
    [SerializeField]
    private GameObject shieldchild;
    [SerializeField]
    private GameObject leftengine;
    [SerializeField]
    private GameObject rightengine;
    [SerializeField]
    private AudioSource Sourceaudio;
    [SerializeField]
    private AudioClip laserAudio;
    private Transform thruster;
    private SpriteRenderer shieldcolor;

    private bool tripleshoton = false;
    private bool speedbooston = false;
    private bool shieldon = false;

    private spawnmanager _spawnManager;
    private UImanager _uimanage;
    // Start is called before the first frame update
    void Start()
    {
        //take the current position = new position(0,0,0)
        transform.position = new Vector3(0, 0, 0);
        _spawnManager = GameObject.Find("spawn_manager").GetComponent<spawnmanager>();
        _uimanage = GameObject.Find("Canvas").GetComponent<UImanager>();
        shieldcolor = shieldchild.GetComponent<SpriteRenderer>();

        if(_spawnManager == null)
        {
            Debug.LogError("spawn manager is null ");
        }
        Sourceaudio = GetComponent<AudioSource>();
        Sourceaudio.clip = laserAudio;
    }

    // Update is called once per frame
    void Update()
    {
        calculatemovements();
        

        if(Input.GetKeyDown(KeyCode.Space) && Time.time > _canfire)
        {
            Ammocount();
            if(ammocount>0)
            {
                firelase();
                ammocount--;
            }
            
            
        }
        if(Input.GetKey(KeyCode.LeftShift))
        {
            Thruster();
        }
        else
        {
            Vector3 normalscale = new Vector3(0.6f, 0.8f, 0.8f);
            thruster = this.gameObject.transform.GetChild(1);
            thruster.localScale = normalscale;

        }
        if(shieldon==true)
        {
            ShieldStrengh();
        }

    }

    void calculatemovements()
    {

        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 directions = new Vector3(horizontalInput, verticalInput, 0);
        // new Vector3(1,0,0)*horinzontalInput*speed*realtime
        if(speedbooston==true)
        {
            transform.Translate(directions * _speed * Time.deltaTime);
        }
        else
        {
            transform.Translate(directions * _speed * Time.deltaTime); // input 
        }
        if (transform.position.y >= 0)
        {
            transform.position = new Vector3(transform.position.x, 0, 0);

        }
        else if (transform.position.y <= -4)
        {
            transform.position = new Vector3(transform.position.x, -4, 0);
        }

        if (transform.position.x >= 9)
        {
            transform.position = new Vector3(9, transform.position.y, 0);
        }
        else if (transform.position.x <= -9)
        {
            transform.position = new Vector3(-9, transform.position.y, 0);
        }

    }
     void firelase()
    {
        _canfire = Time.time + _firerate;
        Vector3 offset = new Vector3(0, 1f, 0);
        if (tripleshoton == true)
        {

            Instantiate(_tripleshot, transform.position, Quaternion.identity);
        }
        else
        {
            Instantiate(_laser, transform.position + offset, Quaternion.identity);
           // Debug.Break();
        }
        Sourceaudio.Play();
    }

    public void Damage()
    {
        
        if(shieldon==true)
        {
            
            if (shieldlevel > 1)
            {
                shieldlevel--;
                return;
            }
            else
            {
                shieldon = false;
                shieldlevel--;
                shieldchild.SetActive(false);
                return;
            }

        }
        _lives--;
        if(_lives==2)
        {
            rightengine.SetActive(true);
        }
        if (_lives==1)
        {
            leftengine.SetActive(true);
        }
        _uimanage.UpdateLives(_lives);

        if(_lives==0)
        {
            _spawnManager.onplayerdeath();
            
            Destroy(this.gameObject);
        }

    }

    public void TripleShotActive()
    {
        tripleshoton = true;
        StartCoroutine(tripleshottime());
    }
    public void SpeedbootsActive()
    {
        speedbooston = true;
        _speed *= speedmulti;
        StartCoroutine(speedboosttime());
    }
    public void ShieldActive()
    {
        shieldchild.SetActive(true);
        shieldon = true;
        shieldlevel++;
    }

    public void ScoreUpadte()
    {
        _score = _score + 10;
        _uimanage.ScoreUI(_score);
    }

    public void Thruster()
    {
        Vector3 scalechange = new Vector3(0.8f, 1f, 1f);
        _speed = 20f;
        thruster = this.gameObject.transform.GetChild(1);
        thruster.localScale = scalechange;


        /* else
         {
             _speed = 10f;
             thruster.localScale = normalscale;
         }*/
    }

    public void ShieldStrengh()
    {
        if (shieldlevel > 1)
        {
            shieldcolor.color = Color.green;
        }
    }

    public void Ammocount()
    {
        if (ammocount < 5)
        {
            _uimanage.Lowammo();
        }
        if (ammocount == 0)
        {
            _uimanage.Noammo();
            Debug.Log("no ammo");
        }
    }


    IEnumerator tripleshottime()
    {
            yield return new WaitForSeconds(5);
            tripleshoton = false;
    }
    IEnumerator speedboosttime()
    {
        yield return new WaitForSeconds(5);
        speedbooston = false;
        _speed =( _speed / speedmulti);
    }

  
    
}
