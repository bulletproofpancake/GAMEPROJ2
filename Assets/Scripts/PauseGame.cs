using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseGame : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;
    private bool _isPaused;
    private bool _showMenu;

    private void Update()
    {
        Time.timeScale = _isPaused ? 0f : 1f;
        pauseMenu.SetActive(_isPaused && _showMenu);
    }

    public void TogglePause(bool showMenu)
    {
        _showMenu = showMenu;
        _isPaused = !_isPaused;
        print(Time.timeScale);
    }

    public void ExitRound()
    {
        RoundStatManager.Instance.EndRound();
        SceneLoader.Instance.LoadScene("Main Menu");
    }
}
