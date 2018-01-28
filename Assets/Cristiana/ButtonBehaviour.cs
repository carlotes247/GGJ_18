using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonBehaviour : MonoBehaviour {

    public enum Button { START, PAUSE, RESUME, RESTART, EXIT};

    public Button button;
    private bool eaten;
    private Vector3 initialPos;

	// Use this for initialization
	void Start () {
        eaten = false;
        initialPos = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		if (eaten)
        {
            eaten = false;
            ChooseAction();
        }
	}

    public void EatIt()
    {
        eaten = true;
    }

    void ChooseAction()
    {
        switch (button)
        {
            case Button.START:
                StartGame();
                break;
            case Button.PAUSE:
                Pause();
                break;
            case Button.RESUME:
                Resume();
                break;
            case Button.RESTART:
                RestartGame();
                break;
            case Button.EXIT:
                ExitGame();
                break;
            default:
                break;
        }
    }

    void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    void Pause()
    {
        gameObject.SetActive(true);
        Time.timeScale = 0;
    }

    void Resume()
    {
        Time.timeScale = 1;
        transform.position = initialPos;
        gameObject.SetActive(false);
    }

    void RestartGame()
    {
        int scene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(scene, LoadSceneMode.Single);
    }

    void ExitGame()
    {
        Application.Quit();
    }
}
