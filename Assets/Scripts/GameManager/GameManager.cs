using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public Transform player = null;
    [SerializeField] private Button restartButton = null;

    private GameManagerActions gameManagerActions = null;
    public UnityEvent onGameEnd = null;

    private void Awake()
    {
        gameManagerActions = new GameManagerActions();
        gameManagerActions.SetTimeScale(1f);

        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
    }

    private void OnEnable()
    {
        onGameEnd.AddListener(FinishGame);

        restartButton.onClick.AddListener(gameManagerActions.Respawn);
    }

    private void OnDisable()
    {
        onGameEnd.RemoveListener(FinishGame);
        restartButton.onClick.RemoveListener(gameManagerActions.Respawn);
    }

    private void FinishGame()
    {
        gameManagerActions.SetTimeScale(0f);
    }
}
