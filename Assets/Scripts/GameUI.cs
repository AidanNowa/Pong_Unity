using TMPro;
using UnityEngine;

public class GameUI : MonoBehaviour
{
    public ScoreText scoreTextPlayer1, scoreTextPlayer2;
    public GameObject menuObject; //fine to use game object since we only want to turn it on and off
    public System.Action onStartGame;
    public TextMeshProUGUI winText;


    public void UpdateScores(int scorePlayer1, int scorePlayer2) {
        scoreTextPlayer1.SetScore(scorePlayer1);
        scoreTextPlayer2.SetScore(scorePlayer2);
    }

    public void HighlightScore(int id) {
        if (id == 1) {
            scoreTextPlayer1.Highlight();
        }
        else {
            scoreTextPlayer2.Highlight();
        }
    }

    public void OnStartGameButtonClicked() {
        menuObject.SetActive(false);
        onStartGame?.Invoke();
    }

    public void OnGameEnds(int winnerID) {
        menuObject.SetActive(true);
        winText.text = $"Player {winnerID} wins!";
    }

}
