using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnmanager : MonoBehaviour
{
    [SerializeField]
    private GameObject _enemy;
    [SerializeField]
    private GameObject _enemycontainer;
    [SerializeField]
    private GameObject[] _powerups;

    private bool _stopspawn = false;
   
    public void StartSpawning()
    {
        StartCoroutine(spawnenemytime());
        StartCoroutine(spawnpoweruptime());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //spawn game objects every 5 seconds
    //create a corountine of type IEnumerator -- yield events 
     IEnumerator spawnenemytime()
    {
        yield return new WaitForSeconds(3.0f);
        while(_stopspawn==false)
        {
            Vector3 spawnpos = new Vector3(Random.Range(-9f, 9f), 10, 0);
             GameObject newenemy =  Instantiate(_enemy, spawnpos, Quaternion.identity);
            newenemy.transform.parent = _enemycontainer.transform;
            yield return new WaitForSeconds(5.0f);
        }

    }

    IEnumerator spawnpoweruptime()
    {
        yield return new WaitForSeconds(3.0f);
        while (_stopspawn == false)
        {
            Vector3 spawnpos = new Vector3(Random.Range(-9f, 9f), 10, 0);
            int randompower = Random.Range(0, 3);
            GameObject newpowerup = Instantiate(_powerups[randompower], spawnpos, Quaternion.identity);
            yield return new WaitForSeconds(Random.Range(7, 10));
        }
    }
    public void onplayerdeath()
    {
        _stopspawn = true;
    }


}
