using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    public GameObject startPanel;
    public GameObject toturial;
    public static bool isGameOver;
    public static bool isGameStart;
    public GameObject completePanel;
    public GameObject LevelFailPanel;
    
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        Time.timeScale = 0;
        Application.targetFrameRate = 60;
    }
    private void Start()
    {
        toturial.SetActive(true);
    }
    public void RestartGame()
    {
        SceneManager.LoadScene("GamePlay");
        isGameOver = false;
        isGameStart = false;
    }
    //public void OnStartButton()
    //{
    //    StartCoroutine(StartGame());
    //}
    //public IEnumerator StartGame()
    //{
    //    Time.timeScale = 1;
    //    isGameOver = false;
    //    startPanel.SetActive(false);
    //    toturial.SetActive(true);
    //    yield return new WaitForSeconds(1.5f);
    //    toturial.SetActive(false);
    //}
    private void Update()
    {
        if (!isGameStart)
        {
            if (Input.touchCount > 0)
            {
                toturial.SetActive(false);
                startPanel.SetActive(false);
                Time.timeScale = 1;
                isGameStart = true;
            }
        }
    }
    public IEnumerator OnLevelComplete()
    {
        yield return new WaitForSeconds(1.5f);
        completePanel.SetActive(true);
    }
    public  IEnumerator OnLevelFail()
    {
        Debug.Log("fail working");
        yield return new WaitForSeconds(1.5f);
        LevelFailPanel.SetActive(true);
    }
}
