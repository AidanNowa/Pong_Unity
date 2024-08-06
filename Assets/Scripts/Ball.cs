using UnityEditor;
using UnityEditor.Build.Content;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public Rigidbody2D rb2d;
    public float maxInitialAngle = 0.67f;
    public float moveSpeed = 1f;
    public float acceleration = 1.1f;
    public float maxStartY = 4f;
    private float startX = 0f;

    private void Start() {
        //InitialPush(); removed to prevent ball from starting when game launches
        //add listener method to onReset action
        //will call ResetBall when onReset is called 
        GameManager.instance.onReset += ResetBall;
        GameManager.instance.gameUI.onStartGame += ResetBall;
    }

    private void OnDestroy() {
        GameManager.instance.onReset -= ResetBall;
        GameManager.instance.gameUI.onStartGame -= ResetBall;
    }
    private void ResetBall() {
        ResetBallPosition();
        InitialPush();
    }

    private void ResetBallPosition() {
        float posY = Random.Range(-maxStartY, maxStartY);
        Vector2 position = new Vector2(startX, posY);
        transform.position = position;
    }
    private void InitialPush() {
        //randomly send ball left or right on scale from Random.range(0,1) shorthand
        Vector2 dir = Random.value < 0.5f ? Vector2.left : Vector2.right;
        dir.y = Random.Range(-maxInitialAngle,maxInitialAngle);
        rb2d.velocity = dir * moveSpeed;
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        ScoreZone scoreZone = collision.GetComponent<ScoreZone>();
        if (scoreZone) {
            GameManager.instance.OnscoreZoneReached(scoreZone.id);
        }
        
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        Paddle paddle = collision.collider.GetComponent<Paddle>();
        if (paddle) {
            //Debug.Log("Hit Paddle");
            rb2d.velocity *= acceleration;
        }
    }
}
