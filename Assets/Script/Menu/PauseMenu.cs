using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject ui;
    [SerializeField] private Canvas pauseMenuCanvas;
    private MovementController controller;
    // Start is called before the first frame update

    private void Start()
    {
        controller = CharacterMovement.getMovement();
        controller.Main.Pause.performed += Pause;
    }

    private void Pause(InputAction.CallbackContext ctx)
    {
        pauseMenuCanvas.enabled = true;
        CharacterMovement.LockControls();
        ui.SetActive(false);
        Time.timeScale = 0f;
    }

    public void Resume()
    {
        Time.timeScale = 1.0f;
        pauseMenuCanvas.enabled = false;
        CharacterMovement.UnlockControls();
        ui.SetActive(true);
    }


    public void Quit()
    {
        Application.Quit();
    }
}
