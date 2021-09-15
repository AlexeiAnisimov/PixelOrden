using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ButtonSc : MonoBehaviour
{
    public GameObject Panel;
    public GameObject Audio;
    public GameObject CanvasGroup;
    GameObject Au;
    string buttonName;
    // Start is called before the first frame update
    void Start()
    {
        if (!GameObject.Find("Audio"))
        {
            //Au = Instantiate(Audio);
            //Au.name = "Audio";
            //DontDestroyOnLoad(Au);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnClickLevel1()
    {
        SceneManager.LoadScene("level1");
    }
    public void OnClickLevel2()
    {
        SceneManager.LoadScene("level2");
    }
    public void OnClicklevel4()
    {
        SceneManager.LoadScene("level4");
    }
    public void OnClicklevel3()
    {
        SceneManager.LoadScene("level3");
    }
    public void OnClicklevel5()
    {
        SceneManager.LoadScene("level5");
    }
    public void OnClickFinal()
    {
        SceneManager.LoadScene("level6");
    }
    public void Onclick()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void Back()
    {
        Panel.SetActive(false);
        CanvasGroup.SetActive(true);
    }
    public void OnClickLevelSelection()
    {
        SceneManager.LoadScene("levelSelection");

    }
    public void setting()
    {
        Panel.SetActive(true);
        CanvasGroup.SetActive(false);
    }
    public void Exit()
    {
        Application.Quit();
    }
    public void Inmenu()
    {
        SceneManager.LoadScene("menu");
    }
    public void Del()
    {
        PlayerPrefs.DeleteAll();
    }
}
