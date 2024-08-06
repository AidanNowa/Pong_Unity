
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;
using UnityEngine.UIElements.Experimental;

public class Paddle : MonoBehaviour
{
    public Rigidbody2D rb2d;
    public float id;
    public float moveSpeed = 2f;

    private Vector3 startPosition;
    private bool activePaddles = false;

    private void Start() {
        //save start position
        startPosition = transform.position;
        //add resetPositon as a listener, will be called when onReset is called
        GameManager.instance.onReset += ResetPosition;
         GameManager.instance.gameUI.onStartGame += ActivatePaddles;
    }

    //may feel a bit jerky to have the paddles reset at each point -- can be removed
    private void ResetPosition() {
        transform.position = startPosition;
    }

    private void Update() {
        float movement = ProcessInput();
        if (activePaddles == true) {
            Move(movement);
        }
        //Move(movement);
    }

    private float ProcessInput() {
        float movement = 0f;
        switch (id) {
            //get our predefined axis for each player
            case 1:
                movement = Input.GetAxis("MovePlayer1");
                break;
            case 2:
                movement = Input.GetAxis("MovePlayer2");
                break;
        }
        
        return movement;
    }

    private void Move(float value) {
        //rb2d.velocity.y = value * moveSpeed; -- classic unity :/
        Vector2 velo = rb2d.velocity;
        velo.y = moveSpeed * value;
        rb2d.velocity = velo;
    }

    private void ActivatePaddles() {
        activePaddles = true;
    }

    public void DeactivatePaddles() {
        activePaddles = false;
    }
}
