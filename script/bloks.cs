using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class bloks : MonoBehaviour
{
    public float podniatiiy = 1;
    public float podniatiix = 0;
    public float time;
    public float speed = 0.5f;
    public bool nachal = false;
    bool nashal = false;
    bool ready = false;
    public bool invise = false;
    public float inviseTime=0;
    public float timeBeforeInvise = 0;
    GameObject pla;
    // Start is called before the first frame update
    void Start()
    {
        gameObject.name = "block";
        if (nachal) StartCoroutine(lol());
        pla = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (nachal)
        {
            Vector3 tempvector = new Vector3(podniatiix, podniatiiy, 0);
            transform.position = Vector3.MoveTowards(transform.position, transform.position + tempvector, speed * Time.deltaTime);
        }
        if (ready&&!nachal&&!invise)
        {
            GameObject.Find("TextInf").GetComponent<Text>().text = "Для того,чтобы поехать, нажмите Q";
            if (Input.GetKeyDown(KeyCode.Q))
            {
                GameObject.Find("TextInf").GetComponent<Text>().text = "";
                foreach (BoxCollider2D a in GetComponents<BoxCollider2D>()) a.enabled = true;
                nachal = true;
                StartCoroutine(lol());
            }
        }
    }
    IEnumerator lol()
    {
        yield return new WaitForSeconds(time);
        podniatiix = -1 * podniatiix;
        podniatiiy = -1 * podniatiiy;
        StartCoroutine(lol());
    }
    public void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            pla.transform.SetParent(transform);
            if (!nachal)
                ready = true;
            if (invise) StartCoroutine(wait());

        }
    }
    public void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            pla.transform.parent=null;
            ready = false;
            GameObject.Find("TextInf").GetComponent<Text>().text = "";
        }
    }
    IEnumerator wait()
    {
        yield return new WaitForSeconds(timeBeforeInvise);
        pla.transform.parent = null;
        var lol = GetComponentsInChildren<SpriteRenderer>();
        foreach(SpriteRenderer a in lol) a.enabled = false;
        GetComponent<BoxCollider2D>().enabled = false;
        yield return new WaitForSeconds(inviseTime);
        foreach (SpriteRenderer a in lol) a.enabled = true;
        GetComponent<BoxCollider2D>().enabled = true;
    }
}
