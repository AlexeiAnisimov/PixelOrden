using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class knight : MonoBehaviour
{
    public float nachspeed;
    public int hp = 500;
    public float speed = 4;
    public string name;
    public int damage=150;
    bool damageinpl = false;
    bool PlayerIsready = true;
    public int miniboss = 0;
    public int storona = 1;
    public float runanim = 1;
    public float distanceforattack;
    bool run = false;
    bool plaInobj = false;
    bool ratVst = false;
    bool at = false;
    Vector2 polet;
    public GameObject weap;
    public GameObject key;
    public GameObject heal;
    GameObject player;
    Rigidbody2D rb;
    Animator anim;
    Text hpText;
    Vector2 wh;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        hpText = GetComponentInChildren<Canvas>().GetComponentInChildren<Text>();
        hpText.text = hp.ToString();
        if (name == "rat") hpText.enabled = false;
        //PlayerPrefs.SetInt("lvl3miniboss" + miniboss, 0);
        //PlayerPrefs.Save();
        //if (PlayerPrefs.GetInt("lvl3miniboss" + miniboss, 0) == 1) Destroy(gameObject);
    }
    private void FixedUpdate()
    {
        if (hp > 0)
        {
            Move();
            storona = -(int)Mathf.Sign(transform.position.x - player.transform.position.x);
            hpText.transform.localScale = new Vector2(storona, 1);
            transform.localScale = new Vector2(storona, transform.localScale.y);
        }
    }
    void Update()
    {
        if (hp > 0)
        {
            if (Vector2.Distance(transform.position, player.transform.position) < distanceforattack)
            {
                if (name == "knight" && Mathf.Abs(transform.position.y - player.transform.position.y) < 0.5f) knight_and_rat(0.3f);
                else if (name == "archer") anim.SetBool("attack", true);
                else if (name == "rat"&&Mathf.Abs(transform.position.y - player.transform.position.y) < 0.3f) knight_and_rat(0.2f);
            }
        }
        else
        {
            if (miniboss != 0)
            {
                if (miniboss == 1) die();
            }
            if(name=="knight") Instantiate(heal, new Vector2(transform.position.x, player.transform.position.y), Quaternion.identity);
            Destroy(gameObject);
        }
    }
    void die()
    {
        GameObject patrVistrel = Instantiate(key, new Vector2(transform.position.x, player.transform.position.y), Quaternion.identity);
        patrVistrel.GetComponent<veshi>().value = 18;
    }
    void knight_and_rat(float rast)
    {
        anim.SetBool("hod", true);
        run = true;
        if (Vector2.Distance(transform.position, player.transform.position) < rast && !anim.GetBool("attack"))
        {
            wh = (-transform.position + player.transform.position).normalized;
            anim.SetBool("attack", true);
        }
    }
    void Move()
    {
        if (run) if (!anim.GetBool("attack")) rb.velocity = new Vector2(storona * speed, rb.velocity.y);
    }
    void Attack()
    {
        at = !at;
        GetComponent<BoxCollider2D>().enabled = at;
        if (!at)
        {
            damageinpl = false;
            anim.SetBool("attack", false);
        }
    }
    void up()
    {
        ratVst = !ratVst;
        if (ratVst)
        {
            GetComponent<CapsuleCollider2D>().direction = CapsuleDirection2D.Vertical;
            GetComponent<CapsuleCollider2D>().offset = new Vector2(0.017f, 0.02f);
            GetComponent<CapsuleCollider2D>().size = new Vector2(0.1f, 0.15f);
        }
        else
        {
            GetComponent<CapsuleCollider2D>().direction = CapsuleDirection2D.Horizontal;
            GetComponent<CapsuleCollider2D>().offset = new Vector2(0.017f, -0.011f);
            GetComponent<CapsuleCollider2D>().size = new Vector2(0.17f, 0.06f);
        }
    }
    void attackArch()
    {
        GameObject patron = rasch(0);
        patron.name = "strel";
        patron.GetComponent<weapon>().hoziain = gameObject.name;
        patron.GetComponent<weapon>().damage = damage;
        patron.GetComponent<Rigidbody2D>().velocity = polet.normalized * speed;
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
        if (name == "knight")
        {
            if (at && !damageinpl && collision.gameObject.name == "Player")
            {
                player.GetComponent<movement>().hp -= damage;
                player.GetComponent<movement>().hpText.text = player.GetComponent<movement>().hp.ToString();
                player.GetComponent<Rigidbody2D>().AddForce(new Vector2(4f * storona, 0));
                damageinpl = true;
            }
        }
        provdam(collision);
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
        if (name == "rat")
        {
            if (at && !damageinpl && collision.gameObject.name == "Player")
            {
                player.GetComponent<movement>().hp -= damage;
                player.GetComponent<movement>().hpText.text = player.GetComponent<movement>().hp.ToString();
                damageinpl = true;
            }
        }
    }
    IEnumerator wait(int variant, float time)
    {
        PlayerIsready = false;
        if (variant == 1) hp -= 20;
        else if (variant == 2) hp -= 100;
        if(name!="rat")hpText.text = hp.ToString();
        yield return new WaitForSeconds(time);
        PlayerIsready = true;
    }
    void provdam(Collision2D collision)
    {
        if (collision.gameObject.name == "patron" && collision.gameObject.GetComponent<patron>().hoziain != gameObject.name)
        {
            hp -= GameObject.Find(collision.gameObject.GetComponent<patron>().hoziain).GetComponent<movement>().damage / 2;
            if (name != "rat") hpText.text = hp.ToString();
        }
    }
}
