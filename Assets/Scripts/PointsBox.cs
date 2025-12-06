using UnityEngine;
using System;
public class PointsBox : MonoBehaviour
{
    public static Action _updateScore;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter(Collider other)
    {
        _updateScore?.Invoke();
        transform.gameObject.SetActive(false);
    }
}
