using System.Collections;
using UnityEngine;

//utiliser un package particulier pour gérer les scènes
using UnityEngine.SceneManagement;

public class FromVoidToMenu : MonoBehaviour
{
    public Animator fadeSystem;

    private void Awake()
    {
        fadeSystem = GameObject.FindGameObjectWithTag("FadeSystem").GetComponent<Animator>();
        SceneManager.LoadScene("Menu");
        //StartCoroutine(loadMenu());
    }
   // public IEnumerator loadMenu()
    //{
        //fadeSystem.SetTrigger("FadeIn");
        //yield return new WaitForSeconds(0.1f);
    //}
}