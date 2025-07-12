using UnityEngine;
using UnityEngine.UI;

public class UIAnimationEvents : MonoBehaviour
{
    public Animator logoAnimator;
    public Animator mainMenuAnimator;
    public GameObject mainMenuParent;
    public Animator buttonAnimator;
    public GameObject buttonParent;
    public Button playButton;
    public Button infoButton;
    public void GameLogoGoUp()
    {
        logoAnimator.Play("GameLogoMoveUp");
    }
    public void PlayMainMenu()
    {
        mainMenuParent.SetActive(true);
        mainMenuAnimator.Play("MainMenuFadeIn");
    }
    public void PlayButtonFadeIn()
    {
        buttonParent.SetActive(true);
        playButton.interactable = false;
        infoButton.interactable = false;
        buttonAnimator.Play("ButtonFadeIn");
    }
    public void SetButtonEnable()
    {
        playButton.interactable = true;
        infoButton.interactable = true;
    }
}
