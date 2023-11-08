using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Player : MonoBehaviour
{
    public Projectile laserPrefab;
    public float speed = 5.0f;
    private bool _laserActive;
    public int lives = 5;

    public GUISkin layout;

    private void Update()
    {
        if( Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            this.transform.position += Vector3.left * this.speed * Time.deltaTime;
        }
        else if(Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)){
            this.transform.position += Vector3.right * this.speed * Time.deltaTime;
        }
        if( Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        if(!_laserActive)
        {
            Projectile projectile =Instantiate(this.laserPrefab, this.transform.position, Quaternion.identity);
            projectile.destroyed += LaserDestroyed;
            _laserActive = true;
        }
    }

    private void LaserDestroyed(){
        _laserActive = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Invader") 
           || other.gameObject.layer == LayerMask.NameToLayer("Missile")){
            lives--;
            if (lives <= 0)
            {
                SceneManager.LoadScene("GameOver");
            }
            else if(other.gameObject.layer == LayerMask.NameToLayer("Invader")){
                SceneManager.LoadScene("GameOver");
            }

            else if (other.CompareTag("Invaders"))
            {
                SceneManager.LoadScene("GameOver");
            }
        }
    }

    void OnGUI () {
    GUI.skin = layout;
    GUI.Label(new Rect(Screen.width / 2 - 150 - 12, 20, 100, 100), "VIDAS:  " + lives);
    }
}
