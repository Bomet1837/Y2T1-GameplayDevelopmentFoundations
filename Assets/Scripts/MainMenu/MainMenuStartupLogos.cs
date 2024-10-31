using System.Collections;
using UnityEngine;
using UnityEngine.Animations;

public class MainMenuStartupLogos : MonoBehaviour
{
    private Animation startLogosAnimation;
    [SerializeField] private float animationLength = 15f;
    
    public static bool hasPlayedStartupAnimation = false; //Usign static so the bool continues across scene loads

    private void Start()
    {
        if (!hasPlayedStartupAnimation)
        {
            hasPlayedStartupAnimation = true;
            
            startLogosAnimation = GetComponent<Animation>();
            StartCoroutine(PlayStartupAnimation());
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Destroy(this.gameObject);
        }
    }

    IEnumerator PlayStartupAnimation()
    {
        startLogosAnimation.Play("StartupLogosAnim");

        yield return new WaitForSeconds(animationLength);

        Destroy(this.gameObject);
    }
}