using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private Canvas ui;
    [SerializeField] private Canvas pauseMenuCanvas;
    private UiControls controller;
    // Start is called before the first frame update

    private void Start()
    {
        controller = GameManager.getUiController();
        controller.UI.Pause.performed += Pause;
    }

    private void Pause(InputAction.CallbackContext ctx)
    {
        if (!GameManager.playingCutscene && GameManager.currentTutorial == null)
        {
            Cursor.visible = true;
            pauseMenuCanvas.enabled = true;
            CharacterMovement.LockControls();
            ui.enabled = false;
            Time.timeScale = 0f;
        }
    }

    public void Resume()
    {
        Time.timeScale = 1.0f;
        pauseMenuCanvas.enabled = false;
        if (!GameManager.playingCutscene)
        {
            CharacterMovement.UnlockControls();
            ui.enabled = true;
        }
        Cursor.visible = false;
    }


    public void Quit()
    {
        Application.Quit();
    }
}
