using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagement : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LevelMenu() {
        // pause.pausekey = !pause.pausekey;
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
        // AudioController.instance.MenuMusic();
        
    }

    public void Level1Begin() {
        UnityEngine.SceneManagement.SceneManager.LoadScene("SampleScene");
        PlayerPrefs.SetInt("currentHealth", 1);
    }
}
