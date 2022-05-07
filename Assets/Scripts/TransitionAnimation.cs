using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionAnimation : MonoBehaviour
{
    public static TransitionAnimation instance;
    Animator transitionAnimator;
    private void Awake() {
        if (instance != null && instance != this) {
            Destroy(gameObject);
        }
        else {
            instance = this;
        }
    }
    void Start()
    {
        transitionAnimator = GetComponent<Animator>();
    }

    public void FadeOut() {
        transitionAnimator.SetTrigger("FadeOut");
    }
    public void FadeIn() {
        transitionAnimator.SetTrigger("FadeIn");
    }
    public void TogglePlayerMovement(int value) {
        GridMovement playerMovement = PlayerManager.instance.GetComponent<GridMovement>();
        playerMovement.takeMovementInput = value > 0 ? true : false;
    }
}
