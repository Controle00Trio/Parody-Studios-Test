using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    public UIManager _uIManager;
    public Transform _playerStartPosition;
    public Transform _Player;
    public List<GameObject> _listOFBoxes;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void resetGame()
    {
        _Player.position = _playerStartPosition.position;
        _Player.eulerAngles = _playerStartPosition.eulerAngles;
        foreach (var box in _listOFBoxes)
        {
            box.gameObject.SetActive(true);
        }
        _uIManager.resetUI();
    }
}
