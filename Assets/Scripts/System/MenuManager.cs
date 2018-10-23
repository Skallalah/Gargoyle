using UnityEngine;
using System.Collections;
public class MenuManager : MonoBehaviour {

    public string firstLevel;
	// Use this for initialization
    public void LoadFirstLevel()
    {
        Application.LoadLevel(firstLevel);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
