using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private bool _isGameOver;

    private void Update()
    {
        //if r key is pressed
        //restart current scene
        if (Input.GetKeyDown(KeyCode.R) && _isGameOver == true) 
        {
            SceneManager.LoadScene(1); //current Game Scene
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    //if esc key is pressed
    //quit application
    

    public void GameOver()
    {
        Debug.Log("GameManager: :GameOver() Called");
        _isGameOver = true;
    }



}
