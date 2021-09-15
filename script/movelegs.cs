using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
public class movelegs : MonoBehaviour
{
    GameObject player;
    Collider2D col;
    string tag;
    Vector2 lastpos;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
    }

    private void Update()
    {

        if (Input.GetKey(KeyCode.S))
        {
            if(tag=="lest1"||tag=="lest2")col.GetComponent<TilemapCollider2D>().isTrigger = true;
            if (tag == "lest1") GameObject.Find("lest2").GetComponent<TilemapCollider2D>().isTrigger = false;
            if (tag == "lest2") GameObject.Find("lest1").GetComponent<TilemapCollider2D>().isTrigger = false;
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if(!collision.GetComponent<Collider2D>().isTrigger)player.GetComponent<movement>().naEarth();
        col = collision;
        tag = collision.tag;
        if (collision.gameObject.name == "DeathPol") GetComponentInParent<movement>().hp = 0;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        player.GetComponent<movement>().exitEarth();
        if(collision.tag=="lest1"||collision.tag=="lest2")StartCoroutine(lp(collision));
    }
    IEnumerator lp(Collider2D collision)
    {
        lastpos = transform.position;
        yield return new WaitForSeconds(0.05f);
        if ((collision.tag == "lest1"||collision.tag=="lest2") && transform.position.y - lastpos.y > 0)
        {
            collision.GetComponent<TilemapCollider2D>().isTrigger = false;
            if (tag == "lest1") GameObject.Find("lest2").GetComponent<TilemapCollider2D>().isTrigger = true;
            if (tag == "lest2") GameObject.Find("lest1").GetComponent<TilemapCollider2D>().isTrigger = true;
        }
    }
}
