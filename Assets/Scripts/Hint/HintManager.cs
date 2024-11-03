using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using TMPro;

public class HintManager : MonoBehaviour
{
    public static HintManager instance;
    
    [SerializeField] private GameObject hintUI;
    private Animation hintAnim;
    
    [SerializeField] private TMP_Text titleText;
    [SerializeField] private TMP_Text descriptionText;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        hintAnim = hintUI.GetComponent<Animation>();
    }

    public void ShowHint(string title, string description, float duration)
    {
        hintUI.SetActive(true);
        
        //Set the hint information
        if (title == null)
        {
            titleText.gameObject.SetActive(false);
            Debug.Log("Hint title text is null");
        }

        if (description == null)
        {
            descriptionText.gameObject.SetActive(false);
            Debug.Log("Hint description text is null");
        }
        titleText.text = title;
        descriptionText.text = description;
        
        //Start the coroutine
        StartCoroutine(HintCoroutine(title, description, duration));
    }

    IEnumerator HintCoroutine(string title, string description, float duration)
    {
        hintAnim.Play("OpenHintAnim");
        yield return new WaitForSeconds(duration);
        hintAnim.Play("CloseHintAnim");
        yield return new WaitForSeconds(1f);
        
        //Reset gameobjects
        titleText.text = "";
        descriptionText.text = "";
        
        titleText.gameObject.SetActive(true);
        descriptionText.gameObject.SetActive(true);
        
        hintUI.SetActive(false);
    }
}
