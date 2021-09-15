using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
public class lvl3moveJump : MonoBehaviour
{
    public int st;
    Animator an;
    int storona;
    public float dop=0;
    Rigidbody2D rb;
    bool lok;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponentInParent<Rigidbody2D>();
        an = GetComponentInParent<Animator>();
        if (gameObject.transform.parent.name == "Player") storona = GetComponentInParent<movement>().storona;
        else
        {
            if(Application.loadedLevelName=="level3")storona = gameObject.transform.parent.GetComponent<chinaBoys>().storona;
            if (Application.loadedLevelName == "level4") storona = gameObject.transform.parent.GetComponent<knight>().storona;
        }
        if (gameObject.transform.parent.name == "Player") lok = GameObject.Find("lokot").GetComponent<lvl3trigLokot>().lokot;
        else if(gameObject.transform.parent.name == "Knight") lok = GetComponentInChildren<lvl3trigLokot>().lokot;
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if ((collision.tag == "zemlia" &&an.GetBool("hod")&&st==storona&& gameObject.transform.parent.name == "Player"&&!gameObject.transform.parent.GetComponent<Animator>().GetBool("jump")&&!lok)
        ||(gameObject.transform.parent.name != "Player"&&st==storona&& (collision.tag == "zemlia"||((collision.tag=="lest1"||collision.tag=="lest2")&&!collision.GetComponent<TilemapCollider2D>().isTrigger))))
        {
            rb.AddForce(new Vector2(0,0.65f+dop));
        }
    }

}
