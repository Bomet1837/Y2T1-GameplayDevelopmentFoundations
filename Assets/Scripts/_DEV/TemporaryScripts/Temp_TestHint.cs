using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Temp_TestHint : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(TestCoroutine());
    }

    IEnumerator TestCoroutine()
    {
        string title = "Test title";
        string description = "This is a test. This hint should show after 5 seconds of starting the game.";
        yield return new WaitForSeconds(5);
        HintManager.instance.ShowHint(title, description, 5f);
    }
}
