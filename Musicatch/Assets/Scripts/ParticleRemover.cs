using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleRemover : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("Remove1s");
    }

    IEnumerator Remove1s()
    {
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }
    // Update is called once per frame
    void Update()
    {

    }
}
