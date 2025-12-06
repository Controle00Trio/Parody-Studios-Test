using TMPro;
using UnityEngine;
public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI _pointsText;
    public TextMeshProUGUI _timerText;
    public float _timerStartValue;
    public float _maxPointers;
    float _timer;
    float _points;
    bool _isTimerOn;
    public GameObject _gameOverScreen;
    public GameObject _gameCompleteScreen;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        PointsBox._updateScore += UpdateScore;
        _timer = _timerStartValue;
        _isTimerOn = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (_isTimerOn)
        {
            _timer -= Time.deltaTime;
            float seconds = Mathf.Floor(_timer % 60);
            float minutes = Mathf.Floor(_timer / 60);
            _timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
            if (_timer <= 0)
            {
                _timer = 0;
                _gameOverScreen.SetActive(true);
                _isTimerOn = false;
            }
        }

    }
    public void UpdateScore()
    {
        _points++;
        _pointsText.text = _points.ToString();
        if (_points >= _maxPointers)
        {
            _gameCompleteScreen.SetActive(true);
        }
    }
    public void resetUI()
    {
        _points = 0;
        _timer = _timerStartValue;
        _gameOverScreen.SetActive(false);
        _gameCompleteScreen.SetActive(false);
        _isTimerOn = true;
    }
}
