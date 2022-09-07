using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Player : MonoBehaviour
{
    public float speed = 10;

    [Header("Key Setup")]
    public KeyCode moveUpKey = KeyCode.UpArrow;
    public KeyCode moveDownKey = KeyCode.DownArrow;

    [Header("References")]
    public Rigidbody2D rb;
    public TextMeshProUGUI lblScore;
    public Vector3 initialPosition;
    public String screenName = "Player";

    public Action scored;

    private int _points;
    private GameManager gameManager;
    private bool _canMove;

    public bool CanMove { get => _canMove; set => _canMove = value; }

    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_canMove)
        {
            if (Input.GetKey(moveUpKey))
            {
                rb.MovePosition(transform.position + transform.up * speed * Time.deltaTime);
            }
            else if (Input.GetKey(moveDownKey))
            {
                rb.MovePosition(transform.position + transform.up * speed * Time.deltaTime * -1);
            }
        }
    }

    public int GetPlayerPoints()
    {
        return _points;
    }

    public void AddPoint()
    {
        _points++;
        lblScore.text = _points.ToString();
        scored?.Invoke();
    }

    public void Reset()
    {
        transform.localPosition = initialPosition;
        _points = 0;
        lblScore.text = _points.ToString();
    }
}
