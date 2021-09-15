using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
public class tochkaOstanovki : MonoBehaviour
{
    GameObject pla;
    // Start is called before the first frame update
    void Start()
    {
        pla = GameObject.Find("Player");
        if (PlayerPrefs.GetInt("lvl1go",0)==1) tochkaOst();
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y - GameObject.Find("tochkaOstanovki").GetComponent<Transform>().position.y < 0.02f)
        {
            tochkaOst();
        }
    }
    void tochkaOst()
    {
        GetComponentInChildren<EdgeCollider2D>().enabled = false;
        foreach (BoxCollider2D a in GetComponents<BoxCollider2D>()) a.enabled = false;
        GetComponent<bloks>().enabled = false;
        GameObject.Find("light").GetComponent<Light2D>().enabled = true;
        pla.GetComponent<Animator>().speed *= 1.5f;
        pla.GetComponent<movement>().speed *= 1.75f;
        pla.GetComponent<movement>().speedJump *= 1.5f;
        PlayerPrefs.SetInt("lvl1go", 1);
        PlayerPrefs.Save();
        pla.GetComponent<movement>().nachalo = true;
        gameObject.GetComponent<tochkaOstanovki>().enabled = false;
    }
}
