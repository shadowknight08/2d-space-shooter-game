using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class asteroid : MonoBehaviour
{
    [SerializeField]
    private float _rotatespeed;
    [SerializeField]
    private GameObject _explosionprefab;
    private spawnmanager _spawnmanager;

    void Start()
    {
        _spawnmanager = GameObject.Find("spawn_manager").GetComponent<spawnmanager>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.forward * Time.deltaTime * _rotatespeed);
        
    }
    public void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag =="laser")
        {

            Instantiate(_explosionprefab, transform.position, Quaternion.identity);
            Destroy(other);
            Destroy(gameObject,0.25f);
            _spawnmanager.StartSpawning();
        }

        
    }
}
