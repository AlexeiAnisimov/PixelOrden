using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class chinaBoys : MonoBehaviour
{
    public float nachspeed;
    public int hp = 200;
    public float speed=4;
    int damage;
    bool PlayerIsready = true;
    public int miniboss=0;
    public int storona = 1;
    public float pluvis=0;
    public float runanim=1;
    public float distanceforattack;
    bool run = false;
    bool at = false;
    GameObject player;
    Rigidbody2D rb;
    Animator anim;
    Text hpText;
    Vector2 polet;
    Vector2 wh;
    public GameObject weap;
    Vector3 tempvector;
    private Vector3 m_Velocity = Vector3.zero;
    // Start is called before the first frame update
    void Start()
    {
        if (weap.name == "siruken") { gameObject.name = "chinaYoung"; damage = 25; }
        else if (weap.name == "fireweerk") { gameObject.name = "chinaFat"; damage = 100; }
        else if (weap.name == "patron") { gameObject.name = "chinaDed"; damage = 35; }
        player = GameObject.Find("Player");
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        hpText = GetComponentInChildren<Canvas>().GetComponentInChildren<Text>();
        tempvector = Vector3.right * storona;
        PlayerPrefs.SetInt("lvl3miniboss" + miniboss, 0);
        PlayerPrefs.Save();
        if (PlayerPrefs.GetInt("lvl3miniboss" + miniboss, 0) == 1) Destroy(gameObject);
        hpText.text = hp.ToString();
        anim.speed = runanim;
    }
    void FixedUpdate()
    {
        if (hp > 0)
        {
            Move();
            storona = -(int)Mathf.Sign(transform.position.x - player.transform.position.x);
            hpText.transform.localScale = new Vector2(storona, 1);
            transform.localScale = new Vector2(storona, transform.localScale.y);
        }
    }
    private void Update()
    {
        if (hp > 0)
        {
            if (Vector2.Distance(transform.position, player.transform.position) < distanceforattack)
            {
                if (gameObject.name == "chinaYoung") anim.SetBool("attack", true);
                if (gameObject.name == "chinaFat") anim.SetBool("fireweerk", true);
                if (gameObject.name == "chinaDed")
                {
                    anim.SetBool("hod", true);
                    run = true;
                    if (Vector2.Distance(transform.position, player.transform.position) < 0.9f && !anim.GetBool("attack"))
                    {
                        wh = (-transform.position + player.transform.position).normalized;
                        anim.SetBool("attack", true);
                    }
                }
            }
        }
        else
        {
            if (miniboss != 0)
            {
                PlayerPrefs.SetInt("lvl3miniboss" + miniboss, 1);
                PlayerPrefs.Save();
            }
            hpText.text = "0";
            anim.SetBool("death", true);
            rb.velocity = new Vector2(0, 0);
            rb.gravityScale = 0;
            GetComponent<Collider2D>().enabled = false;
        }
    }
    void Death()
    {
        Destroy(gameObject);
    }
    void Move()
    {
        if (run)
        {
            if(!anim.GetBool("attack"))rb.velocity = new Vector2(storona * speed / 3, rb.velocity.y);
        }
    }
    void fast()
    { 
        rb.AddForce(new Vector2(Mathf.Sign(wh.x) * 5.5f, 0));
    }
    void endattack()
    {
        anim.SetBool("attack", false);
    }
    void palkat()
    {
        at = !at;
        GetComponent<BoxCollider2D>().enabled = at;
    }
    void attack()
    {
        GameObject siruke = rasch(pluvis);
        siruke.name = "siruken";
        siruke.GetComponent<weapon>().hoziain = gameObject.name;
        siruke.GetComponent<weapon>().damage = damage;
        siruke.GetComponent<Rigidbody2D>().velocity = polet.normalized * speed;
    }
    void FireweerkAttack()
    {
        GameObject fireweerk = rasch(pluvis);
        fireweerk.name = "fireweerk";
        fireweerk.GetComponent<weapon>().hoziain = gameObject.name;
        fireweerk.GetComponent<weapon>().damage = damage;
        fireweerk.transform.localScale = new Vector3(storona*transform.localScale.x,transform.localScale.y,transform.localScale.z);
        fireweerk.GetComponent<Rigidbody2D>().velocity = polet.normalized * speed;
    }
    GameObject rasch(float dop)
    {
        Vector2 respawnPulia = new Vector2(storona * 0.2f + transform.position.x, transform.position.y);
        Vector2 cursor = new Vector2(player.transform.position.x, player.transform.position.y + dop);
        polet = cursor - (Vector2)transform.position;
        Quaternion rotation = Quaternion.Euler(0, 0, Mathf.Atan2(polet.y, polet.x) * Mathf.Rad2Deg);
        return Instantiate(weap, respawnPulia, rotation);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "patron" && collision.gameObject.GetComponent<patron>().hoziain != gameObject.name)
        {
            hp -= GameObject.Find(collision.gameObject.GetComponent<patron>().hoziain).GetComponent<movement>().damage / 2;
            hpText.text = hp.ToString();
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (at && collision.gameObject.name == "Player")
        {
            player.GetComponent<movement>().hp -= damage;
            player.GetComponent<movement>().hpText.text = player.GetComponent<movement>().hp.ToString();
            player.GetComponent<Rigidbody2D>().AddForce(new Vector2(0.5f * storona, 0));
        }

    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.name == "kulak" && player.GetComponent<Animator>().GetBool("dwoechka") && PlayerIsready)
        {
            StartCoroutine(wait(1, 0.2f));
        }
        else if (collision.gameObject.name == "lokot" && player.GetComponent<Animator>().GetBool("lokot") && PlayerIsready)
        {
            StartCoroutine(wait(2, 1.5f));
        }
    }
    IEnumerator wait(int variant, float time)
    {
        PlayerIsready = false;
        if (variant == 1) hp -= 20;
        else if (variant == 2) hp -= 100;
        hpText.text = hp.ToString();
        yield return new WaitForSeconds(time);
        PlayerIsready = true;
    }
}
