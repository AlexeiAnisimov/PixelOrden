using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class buttonProv : MonoBehaviour
{
    public Image yes;
    public Image no;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log((gameObject.name == "level3" && PlayerPrefs.GetString("level2done","no") == "no"));
        if (PlayerPrefs.GetString(gameObject.name + "done","no") == "complete")
            yes.enabled = true;
        if ((gameObject.name == "level2" && PlayerPrefs.GetString("level1done","no") == "no") ||
            (gameObject.name == "level3" && PlayerPrefs.GetString("level2done","no") == "no") ||
            (gameObject.name == "level4" && PlayerPrefs.GetString("level2done","no") == "no") ||
            (gameObject.name == "level5" && (PlayerPrefs.GetString("level3done","no") == "no" || PlayerPrefs.GetString("level4done","no") == "no"))
            ||((gameObject.name == "level6" && PlayerPrefs.GetString("level5done", "no") == "no"))){
                no.enabled = true;
                GetComponent<Button>().enabled = false;
        }
    }
}
