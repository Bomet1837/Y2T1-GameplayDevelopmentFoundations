using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Temp_DemoDestroy : MonoBehaviour
{
    public float destroyTime = 1.5f;
    void Start()
    {
        StartCoroutine(DestroyOnSeconds());
    }

    IEnumerator DestroyOnSeconds()
    {
        yield return new WaitForSeconds(destroyTime);

        Destroy(this.gameObject);
    }
}
