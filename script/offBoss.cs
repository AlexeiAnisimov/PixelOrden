using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class offBoss : MonoBehaviour
{
    public GameObject boss;
    // Start is called before the first frame update
    void Start()
    {
       
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            if(SceneManager.GetActiveScene().name=="level1")
                boss.GetComponent<rozbinikBOSS>().enabl = false;
        }
    }
}
