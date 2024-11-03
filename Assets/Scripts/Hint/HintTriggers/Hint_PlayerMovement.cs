using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hint_PlayerMovement : MonoBehaviour
{
    [Header("Trigger Information")]
    [SerializeField] private float hintWaitDuration = 10f;
    [SerializeField] private bool enableHint = true;
    [SerializeField] private bool runAutomatically = true; //This is used if I want to make this trigger run by waiting for hintWaitDuration or wait for a trigger
    
    [Header("Hint UI")]
    [SerializeField] private string hintTitle = "";
    [SerializeField] private string hintDescription = "";
    [SerializeField] private float hintDuration = 5f;
    void Start()
    {
        if (enableHint)
        {
            if (runAutomatically)
            {
                StartCoroutine(RunHint());
            }
        }
        else
        {
            Destroy(this);
        }
    }

    void Update()
    {
        if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
        {
            Debug.Log("Player moved, disabling player movement hint");

            Destroy(this);
        }

    }

    IEnumerator RunHint()
    {
        yield return new WaitForSeconds(hintWaitDuration);
        
        HintManager.instance.ShowHint(hintTitle, hintDescription, hintDuration);
        
        Destroy(this);
    }
}
