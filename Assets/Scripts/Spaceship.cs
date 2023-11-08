using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spaceship : MonoBehaviour
{
    public int scoreValue = 30;
    private Invaders invadersScript;

    private void Start()
    {
        invadersScript = FindObjectOfType<Invaders>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Laser") && invadersScript != null)
        {
            invadersScript.AddScore(scoreValue);
            Destroy(gameObject);
        }
    }
}
