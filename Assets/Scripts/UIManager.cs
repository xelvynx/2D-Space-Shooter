using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TMP_Text _scoreText;
    [SerializeField] private Image _lifeDisplay;
    [SerializeField] private Sprite[] _livesSprites;
    [SerializeField] private TMP_Text _gameOverText;
    // Start is called before the first frame update
    void Start()
    {
        _scoreText.text = "Score: " + 50;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void UpdateScoreText(int playerScore) 
    {

        _scoreText.text = "Score: " + playerScore;
    }
    public void UpdateLivesSprite(int currentLives)
    {
        _lifeDisplay.sprite = _livesSprites[currentLives];
        if (currentLives <= 0)
        {
            _gameOverText.gameObject.SetActive(true);
            StartCoroutine(GameOverFlicker());
        }
    }
    IEnumerator GameOverFlicker() 
    {
        while (true)
        {
            _gameOverText.text = "GAME OVER";
            yield return new WaitForSeconds(.5f);
            _gameOverText.text = "";
            yield return new WaitForSeconds(.5f);
        }
        
    }
}
