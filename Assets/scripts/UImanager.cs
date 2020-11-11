using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UImanager : MonoBehaviour
{
    [SerializeField]
    private Text _scoretext;
    [SerializeField]
    private Text _gameovertext;
    [SerializeField]
    private Text _restart;
    [SerializeField]
    private Image _lives;
    [SerializeField]
    private Sprite[] _livessprite;
    [SerializeField]
    private Text lowammo;

    private GameManager _gamemanager;


    // Start is called before the first frame update
    void Start()
    {
        _gameovertext.gameObject.SetActive(false);
        _scoretext.text = "score:" + 0;
        _gamemanager = GameObject.Find("Game_manager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        

    }

    public void ScoreUI(int score)
    {
        _scoretext.text = "score:" + score;
    }
    public  void UpdateLives(int livescount)
    {
        _lives.sprite = _livessprite[livescount];

        if(livescount<1)
        {
            _gameovertext.gameObject.SetActive(true);
            _restart.gameObject.SetActive(true);
            _gamemanager.GameOver();

            StartCoroutine(GameoverFlicker());
        }
    }
    public void Lowammo()
    {
        lowammo.gameObject.SetActive(true);
    }
    public void Noammo()
    {
        lowammo.text = " No ammo";
    }
    IEnumerator GameoverFlicker()
    {
        while(true)
        {
            _gameovertext.text = "Game Over";
            yield return new WaitForSeconds(0.5f);
            _gameovertext.text = "";
            yield return new WaitForSeconds(0.5f);
        }
    }
  
}
