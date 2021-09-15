using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ghost : MonoBehaviour
{
    GameObject player;
    Animator boss;
    Rigidbody2D rb;
    Vector2 pl;
    Vector2 tr;
    public float distance=10f;
    public float speed = 0.9f;
    bool ready = true;
    bool atack = false;
    bool active=false;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        rb = GetComponent<Rigidbody2D>();
        GetComponent<Animator>().SetBool("Run", true);
        boss = GameObject.Find("ПРiВiДЕНiЕ").GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        boss = GameObject.Find("ПРiВiДЕНiЕ").GetComponent<Animator>();
        pl = player.transform.position;
        if (ready)
        {
            tr = new Vector2((pl.x - transform.position.x), (pl.y - transform.position.y));
            transform.localScale = new Vector2(-Mathf.Sign(transform.position.x - pl.x), transform.localScale.y);
        }
        else if (!ready && Mathf.Abs(tr.x) > 0.2f) tr = new Vector2(-(pl.x - transform.position.x), (pl.y - transform.position.y) + 0.3f);
        if (Vector2.Distance(pl, transform.position) < distance) active = true;
        if(active)rb.velocity = tr.normalized * speed;
        if (Mathf.Abs(tr.x) < 0.3f && !atack)
        {
            atack = true;
            GetComponent<Animator>().SetBool("attack", true);
        }
        if(boss.GetBool("Death")) GetComponent<Animator>().SetBool("death", true);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "kulak" && (player.GetComponent<Animator>().GetBool("dwoechka")|| player.GetComponent<Animator>().GetBool("lokot")))
        {
            GetComponent<Animator>().SetBool("death", true);
            GetComponent<CapsuleCollider2D>().enabled = false;
        }
        if (collision.gameObject.name == "Player")
        {
            player.GetComponent<movement>().hp -= 5;
            player.GetComponent<movement>().hpText.text = player.GetComponent<movement>().hp.ToString();
            StartCoroutine(wait());
        }
    }
    void death()
    {
        Destroy(gameObject);
    }
    void attack()
    {
        GetComponent<Animator>().SetBool("attack", false);
    }
    IEnumerator wait()
    {
        ready = false;
        yield return new WaitForSeconds(2.2f);
        ready = true;
        atack = false;
    }

}
