using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameController : MonoBehaviour
{
    [SerializeField] Canvas canvasStart;
    [SerializeField] Canvas canvasGameover;
    [SerializeField] TextMeshProUGUI scoreText;

    [SerializeField] float sceneLoadDelay = 1.0f;
    ScoreKeeper scoreKeeper;
    CatBaseController catBaseController;
    EnemySpawner enemySpawner;

    void Awake()
    {
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
        catBaseController = FindObjectOfType<CatBaseController>();
        enemySpawner = FindObjectOfType<EnemySpawner>();
    }

    void Start()
    {
        ShowCanvasStart();
        HideCanvasGameover();
    }

    public void ShowCanvasStart()
    {
        canvasStart.gameObject.SetActive(true);
    }

    public void HideCanvasStart()
    {
        canvasStart.gameObject.SetActive(false);
    }

    public void ShowCanvasGameover()
    {
        canvasGameover.gameObject.SetActive(true);
        scoreText.text = scoreKeeper.GetCurrentScore().ToString();
        enemySpawner.StopSpawn();
    }

    public void HideCanvasGameover()
    {
        canvasGameover.gameObject.SetActive(false);
    }

    public void StartGame()
    {
        HideCanvasStart();
        catBaseController.StartCatBase();
        enemySpawner.StartSpawn();
    }

    public void ReplayGame()
    {
        StartCoroutine(WaitAndLoad("MainScene", sceneLoadDelay));
    }

    IEnumerator WaitAndLoad(string sceneName, float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(sceneName);
    }
}
