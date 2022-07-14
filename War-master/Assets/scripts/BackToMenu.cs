using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackToMenu : MonoBehaviour
{
    public void loadNextScene()
    {
        SceneManager.LoadScene(0);

    }
    public void backToMenu()
    {
        SceneManager.LoadScene(0);
    }
}
