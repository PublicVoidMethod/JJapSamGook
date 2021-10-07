using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstSceneSword : MonoBehaviour
{
    public AudioSource puttingSword;
    // Start is called before the first frame update
    
    public void PuttingSword()
    {
        puttingSword.Play();
    }
}
