using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public Vector2 startSpeed;
    public Vector2 speed;

    private GameManager gameManager;
    private Boolean _canMove;

    public Boolean CanMove { get => _canMove; set => _canMove = value; }

    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!this._canMove) return;
        transform.Translate(speed.x * Time.deltaTime, speed.y * Time.deltaTime, 0);
    }

    public void ResetBall()
    {
        transform.localPosition = new Vector3(0, 0, 0);
        speed = startSpeed;
    }

    void OnCollisionEnter2D(Collision2D hit)
    {
        if (hit.transform.tag == "Wall")
        {
            speed = new Vector2(speed.x, speed.y * -1);
        }
        else if (hit.transform.tag == "Player")
        {
            speed = new Vector2((speed.x + UnityEngine.Random.Range(-15, 15)) * -1, speed.y + UnityEngine.Random.Range(-15, 15));
        }
    }

    private void CheckOutOfBounds() {
        // TODO: verificar se a bola está dentro do espaço do jogo
        // Caso não esteja, chamar GameManager.ResetBall();
    }
}
