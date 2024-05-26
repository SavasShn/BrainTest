using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SahneYonet : MonoBehaviour
{
   [SerializeField] AudioSource Click;
   public void SahneBaba()
    {
        Click.Play();

        SceneManager.LoadScene(1);
    }
}
