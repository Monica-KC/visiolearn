using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 

public class sceneManagement : MonoBehaviour
{
    // Start is called before the first frame update
    public void OnexploreButtonClicked()
    {
        SceneManager.LoadScene("secondpage");  // Load RegistrationScene
    }

     public void Onlearn()
    {
        SceneManager.LoadScene("OSI");  // Load RegistrationScene
    }

    public void Ontest()
    {
        SceneManager.LoadScene("trial1MCQ");  // Load RegistrationScene
    }

    public void Onhelp()
    {
        SceneManager.LoadScene("RAG");  // Load RegistrationScene
    }

    public void Onhome()
    {
        SceneManager.LoadScene("firstpage");  // Load RegistrationScene
    }
    public void Ontry()
    {
        SceneManager.LoadScene("front");  // Load RegistrationScene
    }


}
