using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class dwer : MonoBehaviour
{
    public float rast = 0.5f;
    public int value;
    public bool endDoor;
    int opened=0;
    bool bil = false;
    bool vipoln = false;
    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, 0);
        opened = PlayerPrefs.GetInt("opened"+value.ToString(),opened);
        if(opened==1) GetComponent<Animator>().SetBool("prishel", true);
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector2.Distance(transform.position, GameObject.Find("Player").transform.position) < rast)
        {
            if (opened == 0 && !vipoln)
            {
                if (PlayerPrefs.GetInt("key" + value.ToString()) == 1)
                {
                    GameObject.Find("Noopen").GetComponent<Text>().text = "нажмите q,чтобы открыть дверь";
                    if (Input.GetKeyDown(KeyCode.Q))
                    {
                        GetComponent<Animator>().SetBool("prishel", true);
                        GameObject.Find("Player").GetComponent<movement>().key--;
                        GameObject.Find("ключ").GetComponent<SpriteRenderer>().enabled = false;
                        GameObject.Find("Noopen").GetComponent<Text>().text = "";
                        vipoln = true;
                    }
                }
                else if (value > 1000)
                {
                    GameObject.Find("Noopen").GetComponent<Text>().text = "нажмите q,чтобы открыть дверь";
                    if (Input.GetKeyDown(KeyCode.Q))
                    {
                        GetComponent<Animator>().SetBool("prishel", true);
                        GameObject.Find("Noopen").GetComponent<Text>().text = "";
                        vipoln = true;
                    }
                }
            }
            else if (opened == 1 && endDoor)
            {
                GameObject.Find("Noopen").GetComponent<Text>().text = "нажмите q,чтобы открыть дверь";
                if (Input.GetKeyDown(KeyCode.Q))
                {
                    PlayerPrefs.SetString(SceneManager.GetActiveScene().name + "done", "complete");
                    PlayerPrefs.Save();
                    SceneManager.LoadScene("levelSelection");
                }
            }
            bil = true;
        }
        else if (bil) was();
    }
    void OnPrishel()
    {
        if (GetComponent<BoxCollider2D>() != null) GetComponent<BoxCollider2D>().enabled = false;
        GetComponent<Animator>().SetBool("prishel", false);
        opened = 1;
        PlayerPrefs.SetInt("opened"+value.ToString(), opened);
        PlayerPrefs.Save();
    }
    void was()
    {
        GameObject.Find("Noopen").GetComponent<Text>().text = "";
        bil = false;
    }
}
