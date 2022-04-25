using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PauseControl : MonoBehaviour
{
    // Start is called before the first frame update
    public bool paused = false;
    [SerializeField] GameObject pauseMenu;
    void Start()
    {
        pauseMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            
            PauseGame();
        }
    }

    public void PauseGame() {
        paused = !paused;
        if (paused) {
            Time.timeScale = 0f;
            pauseMenu.SetActive(true);
        }
        else {
            Time.timeScale = 1f;
            pauseMenu.SetActive(false);
        }
    }

}
