using System;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class GameManager : MonoBehaviour
{
    //static instance of gameManager -- use with caution as it does not reset between scenes, ect.
    public static GameManager instance;
    public int scorePlayer1, scorePlayer2;
    public Action onReset;
    public GameUI gameUI;
    public int maxScore = 5;
    public Paddle paddleLeft;
    public Paddle paddleRight;

    private void Awake() {
        if (instance) {
            //destroy any already assigned game managers
            Destroy(gameObject);
        }
        else {
            instance = this;
            gameUI.onStartGame += SetScoresToZero;
        }
    }

    private void OnDestroy() {
        gameUI.onStartGame -= SetScoresToZero;
    }

    //pass id of score zone 
    public void OnscoreZoneReached(int id){
        //if onReset is not null (been subscribed too) then invoke event

        if (id == 1) {
            scorePlayer1++;
        }
        if (id == 2) {
            scorePlayer2++;
        }

        gameUI.UpdateScores(scorePlayer1, scorePlayer2);
        gameUI.HighlightScore(id);
        CheckWin();
    }

    private void CheckWin() {
        int winnerID = scorePlayer1 == maxScore ? 1 : scorePlayer2 == maxScore ? 2 : 0;

        if (winnerID != 0) {
            //someone has won
            gameUI.OnGameEnds(winnerID);
            paddleLeft.DeactivatePaddles();
            paddleRight.DeactivatePaddles();

        }
        else {
            onReset?.Invoke();
        }
    }

    public void SetScoresToZero(){
        scorePlayer1 = 0;
        scorePlayer2 = 0;
        gameUI.UpdateScores(scorePlayer1, scorePlayer2);
    }
}
