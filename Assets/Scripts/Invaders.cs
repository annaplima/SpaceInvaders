using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Invaders : MonoBehaviour
{
    public Invader[] prefabs;
    public int rows = 5;
    public int columns = 11;
    public AnimationCurve speed;
    public int amountKilled { get; private set; }
    public int totalInvaders => this.rows * this.columns;
    public float percentKilled => (float)this.amountKilled / this.totalInvaders;
    public int AmountAlive => this.totalInvaders -this.amountKilled;

    public int score = 0;


    public GUISkin layout;

    public Projectile missilePrefab;

    public float MissileAttackRate = 1.0f;

    private Vector3 _direction = Vector2.right;

    private void Awake()
    {
        for(int row = 0; row < this.rows; row++)
        {
            float width = 2.0f * (this.columns-1);
            float height = 2.0f * (this.rows -1);
            Vector3 centering = new Vector2(-width/2, height/2);
            Vector3 rowPosition = new Vector3(centering.x, centering.y +(row * 2.0f), 0.0f);
            for(int col = 0; col < this.columns; col++)
            {
                Invader invader = Instantiate (this.prefabs[row], this.transform);
                invader.killed+=InvaderKilled;

                Vector3 position = rowPosition;
                position.x += col * 2.0f;
                invader.transform.localPosition = position;
            }
        }
    }


    private void Start(){
        InvokeRepeating(nameof(MissileAttack), this.MissileAttackRate, this.MissileAttackRate);
    }



    private void Update()
    {
        this.transform.position += _direction * this.speed.Evaluate(this.percentKilled) * Time.deltaTime;

        Vector3 rightEdge = new Vector3(11.6f, -2.3f, 0.0f);
        Vector3 leftEdge = new Vector3(-11.1f, -2.3f , 0.0f);

        foreach (Transform invader in this.transform )
        {
            if(!invader.gameObject.activeInHierarchy)
            {
                continue;
            }

            if(_direction == Vector3.right && invader.position.x >= (rightEdge.x -1.0f))
            {
                AdvanceRow();
            }
            else if (_direction == Vector3.left && invader.position.x<=(leftEdge.x + 1.0f))
            {
                AdvanceRow();
            }
        }
    }

    private void AdvanceRow()
    {
        _direction.x *= -1.0f;

        Vector3 position = this.transform.position;
        position.y -= 1.0f;
        this.transform.position = position;
    }

    private void MissileAttack()
    {
        foreach (Transform invader in this.transform )
        {
            if(!invader.gameObject.activeInHierarchy)
            {
            
            if(Random.value < (1.0f / (float)this.AmountAlive))
            {
                Instantiate(this.missilePrefab, invader.position, Quaternion.identity);
                break;
            }
        }
    }
    }


    private void InvaderKilled()
    {
        this.amountKilled++;

        if(this.amountKilled >= this.totalInvaders){
            SceneManager.LoadScene("Win");

        }
    }

    public void AddScore(int points)
    {
        score += points;
    }

    void OnGUI () {
    GUI.skin = layout;
    GUI.Label(new Rect(Screen.width / 60, 20, 100, 100), "PONTUAÇÄO:  " + this.amountKilled + score);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {   
        if (other.CompareTag("Player"))
            {
                SceneManager.LoadScene("GameOver");
            }
        
        else if(other.CompareTag("Wall"))
            {
                SceneManager.LoadScene("GameOver");
            }
    }

}
