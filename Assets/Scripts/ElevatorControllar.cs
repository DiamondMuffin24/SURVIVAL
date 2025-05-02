using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ElevatorController : MonoBehaviour
{
    public Animator elevatorAnimator;
    public Animator fadeAnimator;

    public GameObject elevatorMenuUI; // Assign your ElevatorMenu panel here in Inspector

    private bool playerInElevator = false;

    void Update()
    {
        if (playerInElevator && Input.GetKeyDown(KeyCode.E))
        {
            ShowElevatorMenu();
        }
    }

    private void ShowElevatorMenu()
    {
        Time.timeScale = 0f; // Pause the game
        elevatorMenuUI.SetActive(true);
    }

    public void OnSelectFloor(string sceneName)
    {
        // Close menu and resume game
        Time.timeScale = 1f;
        elevatorMenuUI.SetActive(false);

        // Start elevator sequence
        StartCoroutine(UseElevator(sceneName));
    }

    private IEnumerator UseElevator(string sceneName)
    {
        if (elevatorAnimator != null)
            elevatorAnimator.SetTrigger("CloseDoors");

        yield return new WaitForSeconds(1.5f);

        if (fadeAnimator != null)
            fadeAnimator.SetTrigger("FadeOut");

        yield return new WaitForSeconds(1f);

        SceneManager.LoadScene(sceneName);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            playerInElevator = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            playerInElevator = false;
    }
}
