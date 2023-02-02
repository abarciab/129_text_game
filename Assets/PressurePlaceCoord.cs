using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlaceCoord : MonoBehaviour
{
    [SerializeField] OptionUICoordinator optCoord;
    [SerializeField] float timeRequired = 3;
    float timeLeft;
    bool playerPresent;

    private void Start()
    {
        ResetProgress();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerMovement>()) playerPresent = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerMovement>()) playerPresent = false;
    }

    void ResetProgress()
    {
        timeLeft = timeRequired;
    }

    private void Update()
    {
        timeLeft = Mathf.Clamp(timeLeft, 0, timeRequired);
        timeLeft += Time.deltaTime * (playerPresent ? -1f : 0.5f);

        float val = 1 - (timeLeft / timeRequired);
        optCoord.progress = Mathf.Clamp01(val);
    }
}
