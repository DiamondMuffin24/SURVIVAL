using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ClickAreaUITrigger : MonoBehaviour
{
    [SerializeField] private GameObject gameObjectToEnable; // The GameObject to enable
    [SerializeField] private GameObject uiElementToDisable; // The UI element to disable
    [SerializeField] private GameObject uiElementToDisable2;

    private void Start()
    {
        
    }

    // This method is called when the user clicks on the Image
    public void OnPointerClick(PointerEventData eventData)
    {
        ButtonClick();
    }

    public void ButtonClick()
    {
        Debug.Log("onclick");
        // Enable the target GameObject
        if (gameObjectToEnable != null)
        {
            Debug.Log("gameObjectToEnable not null");
            gameObjectToEnable.SetActive(true);
        }
        else
        {

            Debug.Log("gameObjectToEnable is null");
        }

        // Disable the UI element
        if (uiElementToDisable != null)
        {
            uiElementToDisable.SetActive(false);
        }

        if (uiElementToDisable2 != null)
        {
            uiElementToDisable2.SetActive(false);
        }

    }
}