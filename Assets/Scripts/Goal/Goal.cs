using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    public Player player;
    public string targetTag = "Ball";

    void OnTriggerEnter2D(Collider2D hit)
    {
        if (hit.transform.tag == targetTag)
        {
            Debug.Log("Goal!");
            Score();
        }
    }

    private void Score() {
        player.AddPoint();
    }
}
