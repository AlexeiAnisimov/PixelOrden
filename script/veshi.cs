using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class veshi : MonoBehaviour
{
    GameObject player;
    Text nadpis;
    public int value;
    public int kol_vo;
    public string nameObj;
    public string TextPapirus;
    bool bil = false;
    GameObject pap;
    Text papText;
    // Start is called before the first frame update
    void Start()
    {
        //PlayerPrefs.SetInt("key" + value.ToString(), 0);
        gameObject.name = nameObj;
        nadpis = GameObject.Find("Lut").GetComponent<Text>();
        player = GameObject.Find("Player");
        pap = GameObject.Find("Papirus");
        papText = GameObject.Find("PapirusText").GetComponent<Text>();
        if (gameObject.name == "key")
        {
            if (PlayerPrefs.GetInt("key"+value.ToString(), 0) == 1) Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, 0);
        if (Vector2.Distance(player.GetComponent<Transform>().position, transform.position) < 0.2f)
        {
            player.GetComponent<movement>().vesh = true;
            nadpis.text = gameObject.name;
            bil = true;
            if (Input.GetKeyDown(KeyCode.F))
            {
                switch (gameObject.name)
                {
                    case "streli":
                        streli();
                        break;
                    case "hilka":
                        heal();
                        break;
                    case "key":
                        key();
                        break;
                    case "bottle":
                        bottle();
                        break;
                    case "papirus":
                        papirus();
                        break;
                }
                if (nameObj != "papirus")
                {
                    nadpis.text = "";
                    player.GetComponent<movement>().vesh = false;
                    Destroy(gameObject);
                }
            }
            if (Input.GetMouseButtonDown(0) && nameObj == "papirus") endPapirus();
        }
        else if (player.GetComponent<movement>().vesh && bil)
        {
            nadpis.text = "";
            player.GetComponent<movement>().vesh = false;
            bil = false;
            if (nameObj == "papirus") endPapirus();
        }
    }
    void streli()
    {
        player.GetComponent<movement>().streli += kol_vo;
        player.GetComponent<movement>().streliText.text = player.GetComponent<movement>().streli.ToString();
    }
    void heal()
    {
        player.GetComponent<movement>().hp += kol_vo;
        player.GetComponent<movement>().hpText.text = player.GetComponent<movement>().hp.ToString();
    }
    void key()
    {
        player.GetComponent<movement>().key += kol_vo;
        //GameObject.Find("ключ").GetComponent<SpriteRenderer>().enabled=true;
        PlayerPrefs.SetInt("key"+value.ToString(), 1);
    }
    void bottle()
    {
        if(player.GetComponent<movement>().hp<300)
            player.GetComponent<movement>().hp=(int)(300);
        player.GetComponent<movement>().hpText.text = player.GetComponent<movement>().hp.ToString();
    }
    void papirus()
    {
        pap.GetComponent<Image>().enabled = true;
        papText.text = TextPapirus;
    }
    void endPapirus()
    {
        pap.GetComponent<Image>().enabled = false;
        papText.text = "";
    }
}