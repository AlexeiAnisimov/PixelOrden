using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Experimental.Rendering.Universal;
public class Teni_Tsorii : MonoBehaviour
{
    bool stcor = false;
    bool atack = false;
    bool PlayerIsready = true;
    bool enabl = false;
    bool ready = true;
    bool at = false;
    bool j = false;
    bool reborn = false;
    bool numrandom = false;
    bool rivend = true;
    bool rebornAttack2 = false;
    bool damageReborn = false;
    bool damageReborn2 = false;
    public bool damageInPlayer = true;
    float time=3.2f;
    public int hp = 1000;
    int storona = 1;
    int podniatii = 1;
    int numattack = 1;
    int run = 250;
    public float speed = 1f;
    public GameObject fire;
    public GameObject lch1;
    public GameObject lch2;
    public GameObject lch3;
    public GameObject key;
    public GameObject artefact;
    GameObject player;
    Text hpText;
    Vector2 wh;
    Vector2 pl;
    Vector2 polet;
    Animator anim;
    Transform respawn;
    Text nameText;
    Rigidbody2D rb;
    Light2D light;
    BoxCollider2D Mech;
    // Start is called before the first frame update
    void Start()
    {
        gameObject.name = "ТЕНИ ТСОРИИ";
        player = GameObject.Find("Player");
        hpText = GameObject.Find("BossHP").GetComponent<Text>();
        nameText = GameObject.Find("BossName").GetComponent<Text>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        respawn = GetComponent<Transform>();
        light = GetComponent<Light2D>();
        Mech = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (hp > 0)
        {
            if (!enabl)
            {
                if (Vector2.Distance(transform.position, player.transform.position) < 2f && Mathf.Abs(transform.position.y - player.transform.position.y) < 0.5f)
                {
                    nameText.text = gameObject.name;
                    hpText.text = hp.ToString();
                    enabl = true;
                    GameObject.Find("dwerKBoss").GetComponent<BoxCollider2D>().enabled = true;
                    GameObject.Find("Audio").GetComponent<AudioSource>().enabled = false;
                    GameObject.Find("AudioTeni").GetComponent<AudioSource>().enabled = true;
                }
                else
                {
                    hpText.text = "";
                    nameText.text = "";
                }
            }
            else
            {
                if (!reborn)
                {
                    storona = (int)Mathf.Sign(player.transform.position.x - transform.position.x);
                    if (ready)
                    {
                        if (!numrandom)
                        {
                            switch (numattack)
                            {
                                case 1:
                                    if (!atack)
                                    {
                                        anim.SetBool("attack1", true);
                                        run = 250;
                                    }
                                    break;
                                case 2:
                                    if (!atack)
                                    {
                                        anim.SetBool("attack2", true);
                                        run = 150;
                                    }
                                    break;
                                case 3:
                                    if (!atack)
                                    {
                                        anim.SetBool("attack3", true);
                                        run = 150;
                                    }
                                    break;
                                case 4:
                                    if (!atack)
                                    {
                                        anim.SetBool("attack4", true);
                                        run = 150;
                                    }
                                    break;
                            }
                        }
                        if (numrandom) StartCoroutine(timeout(time));
                    }
                    if (rivend) transform.localScale = new Vector2(storona * Mathf.Abs(transform.localScale.x), transform.localScale.y);
                    if (!numrandom)
                    {
                        if (hp > 400)
                        {
                            numattack = (int)Random.Range(0, 50);
                            if (numattack < 13) numattack = 1;
                            else if (numattack > 13 && numattack < 23) numattack = 2;
                            else if (numattack > 23 && numattack < 35) numattack = 3;
                            else numattack = 4;
                        }
                        else
                        {
                            time = 2.6f;
                            numattack = (int)Random.Range(0, 50);
                            if (numattack < 5) numattack = 1;
                            else if (numattack > 5 && numattack < 20) numattack = 2;
                            else if (numattack > 20 && numattack < 40) numattack = 3;
                            else numattack = 4;
                        }
                        numrandom = true;
                    }
                    if (j) jump();
                }
                else
                {
                    if (!anim.GetBool("rebornAttack1")&&!anim.GetBool("rebornAttack2")&&!anim.GetBool("rebornAttack3")) rb.velocity = new Vector2(storona * speed, speed * podniatii);
                    if (!stcor)
                    {
                        StartCoroutine(lol());
                        stcor = true;
                    }
                    if (ready)
                    {
                        if (!numrandom)
                        {
                            switch (numattack)
                            {
                                case 1:
                                    if (!atack)
                                    {
                                        anim.SetBool("rebornAttack1", true);
                                        StartCoroutine(timeout(6.5f));
                                        atack = true;
                                    }
                                    break;
                                case 2:
                                    if (!atack)
                                    {
                                        anim.SetBool("rebornAttack2", true);
                                        StartCoroutine(timeout(8f));
                                        StartCoroutine(rebAttack2());
                                        atack = true;
                                    }
                                    break;
                                case 3:
                                    if (!atack)
                                    {
                                        anim.SetBool("rebornAttack3", true);
                                        StartCoroutine(timeout(3f));
                                        atack = true;
                                    }
                                    break;
                            }
                        }
                    }
                    if (!numrandom)
                    {
                        numattack=Random.Range(0, 6);
                        if (numattack < 3) numattack = 1;
                        else if (numattack == 4) numattack = 2;
                        else numattack = 3;
                        numrandom = true;
                    }
                    if (rebornAttack2) rb.velocity = new Vector2(speed * storona * 1.75f, 0);
                }
            }
        }
        else if (!reborn)
        {
            endattack();
            ready = true;
            numrandom = false;
            numattack = 1;
            atack = false;
            hpText.text = hp.ToString();
            rb.velocity = new Vector2(0, 0);
            GetComponent<BoxCollider2D>().enabled = true;
            GetComponent<BoxCollider2D>().offset = new Vector2(0.14f, -0.07f);
            GetComponent<BoxCollider2D>().enabled = false;
            anim.SetBool("reborn", true);
        }
        else
        {
            rb.gravityScale = 0;
            rb.velocity = new Vector2(0, 0);
            GetComponent<Collider2D>().enabled = false;
            anim.SetBool("death", true);
        }
        if (hp < 0) hp = 0;
    }
    void rech()
    {
        hpText.text = "";
        nameText.text = "";
        GetComponent<exodus>().enabled = true;
    }
    void die()
    {
        key.GetComponent<veshi>().value = 13;
        GameObject patrVistrel = Instantiate(key, new Vector2(transform.position.x, player.transform.position.y), Quaternion.identity);
        GameObject.Find("dwerKBoss").GetComponent<BoxCollider2D>().enabled = false;
        Instantiate(artefact, new Vector2(transform.position.x+0.15f, player.transform.position.y), Quaternion.identity);
        Destroy(gameObject);
    }
    void rebAt2()
    {
        rebornAttack2 = true;
    }
    void endattack()
    {
        anim.SetBool("attack2", false);
        anim.SetBool("attack4", false);
        anim.SetBool("attack1", false);
        anim.SetBool("attack3", false);
        if (anim.GetBool("rebornAttack1")) rebornAttack2 = false;
        anim.SetBool("rebornAttack1", false);
        anim.SetBool("rebornAttack3", false);
        rb.gravityScale = 1;
        j = false;
        if (!anim.GetBool("rebornAttack2"))
        {
            rebornAttack2 = false;
            at = false;
        }
        Mech.enabled = false;
    }
    void reb()
    {
        hp = 500;
        hpText.text = hp.ToString();
        reborn = true;
    }
    void endriv()
    {
        rivend = !rivend;
    }
    void rivok()
    {
        transform.localScale = new Vector3(storona, 1, 1);
        wh = (-transform.position + player.transform.position).normalized;
        if(anim.GetBool("attack1"))Mech.enabled = true;
        if (reborn)
        {
            if (Mathf.Abs(player.transform.position.x - transform.position.x) < 1f) run = 130;
            else if (Mathf.Abs(player.transform.position.x - transform.position.x) < 1.7f) run = 30 + (int)(100 * Mathf.Abs(transform.position.x - player.transform.position.x));
            else run = 200;
            rb.AddForce(new Vector2(Mathf.Sign(wh.x) * run, 80));
        }
        else rb.AddForce(new Vector2(Mathf.Sign(wh.x) * run, 0));
    }
    void attack3lch()
    {
        if (reborn)
        {
            storona = (int)Mathf.Sign(player.transform.position.x - transform.position.x);
            transform.localScale = new Vector2(storona, 1);
        }
        polet = new Vector2(storona,0);
        Quaternion rotation = Quaternion.Euler(0, 0, Mathf.Atan2(polet.y, polet.x) * Mathf.Rad2Deg);
        Vector2 respawnPulia = new Vector2(storona * 0.2f + transform.position.x, transform.position.y+0.1f);
        if(!reborn)respGO(lch1, respawnPulia, rotation, "lch1", 35, speed *3);
        else respGO(lch3, respawnPulia, rotation, "lch3", 60, speed * 3);
    }
    void jump()
    {
        if (!reborn)
        {
            j = true;
            if (transform.position.y == player.transform.position.y) j = false;
            rb.AddForce(new Vector2(0, 2));
        }
        else {
            rb.AddForce(new Vector2(0, 260));
        }
    }
    void rebornAttackJump()
    {
        damageReborn2 = !damageReborn2;
        if (damageReborn2) rb.AddForce(new Vector2((player.transform.position.x - transform.position.x)*180, (player.transform.position.y - transform.position.y) * 180));
        GetComponent<EdgeCollider2D>().enabled = damageReborn2;
        at = false;
    }
    void attack4lch()
    {
        rb.velocity = new Vector2(0, 0);
        rb.gravityScale = 0;
        polet = new Vector2(-storona, 0);
        Quaternion rotation = Quaternion.Euler(0, 0, Mathf.Atan2(polet.y, polet.x) * Mathf.Rad2Deg);
        Vector2 respawnPulia = new Vector2(-storona * 0.15f + transform.position.x, transform.position.y + 0.1f);
        respGO(lch2, respawnPulia, rotation, "lch2", 50, speed * 4);
        polet = new Vector2(storona, 0);
        respawnPulia = new Vector2(storona * 0.15f + transform.position.x, transform.position.y + 0.1f);
        respGO(lch2, respawnPulia, rotation, "lch2", 50, speed * 4);
    }
    void damReb()
    {
        damageReborn = !damageReborn;
        at = false;
        GetComponent<BoxCollider2D>().enabled = damageReborn;
    }
    void respFireweerf()
    {
        StartCoroutine(respfire());
    }
    IEnumerator lol()
    {
        storona = (int)Mathf.Sign(player.transform.position.x - transform.position.x);
        yield return new WaitForSeconds(1f);
        podniatii = -1 * podniatii;
        StartCoroutine(lol());
    }
    IEnumerator timeout(float time)
    {
        atack = true;
        ready = false;
        yield return new WaitForSeconds(time);
        ready = true;
        atack = false;
        numrandom = false;
    }
    IEnumerator respfire()
    {
        for (float i = 0; i < 3; i++)
        {
            polet = new Vector2(storona,i/4);
            Quaternion rotation = Quaternion.Euler(0, 0, Mathf.Atan2(polet.y, polet.x) * Mathf.Rad2Deg);
            Vector2 respawnPulia = new Vector2(storona * 0.15f + transform.position.x, transform.position.y+(i)/8);
            respGO(fire, respawnPulia, rotation, "fireweerk", 100, speed *1.5f);
            yield return new WaitForSeconds(0.3f);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "patron"&&!rebornAttack2)
        {
            hp -= 25;
            hpText.text = hp.ToString();
        }
        if (collision.gameObject.name == "Player" && !at && rebornAttack2)
        {
            player.GetComponent<movement>().hp -= 15;
            player.GetComponent<movement>().hpText.text = player.GetComponent<movement>().hp.ToString();
            StartCoroutine(rebA2wa());
        }
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Player" && !at && rebornAttack2)
        {
            player.GetComponent<movement>().hp -= 15;
            player.GetComponent<movement>().hpText.text = player.GetComponent<movement>().hp.ToString();
            StartCoroutine(rebA2wa());
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.name == "kulak" && player.GetComponent<Animator>().GetBool("dwoechka") && PlayerIsready&&!rebornAttack2)
        {
            StartCoroutine(wait(1, 0.2f));
        }
        else if (collision.gameObject.name == "lokot" && player.GetComponent<Animator>().GetBool("lokot") && PlayerIsready&&!rebornAttack2)
        {
            StartCoroutine(wait(2, 1.5f));
        }
        if (damageReborn && !at&& collision.gameObject.name == "Player")
        {
            player.GetComponent<movement>().hp -= 50;
            player.GetComponent<movement>().hpText.text = player.GetComponent<movement>().hp.ToString();
            player.GetComponent<Rigidbody2D>().AddForce(new Vector2(2.5f * storona, 0));
            at = true;
        }
        if(damageReborn2 && !at && collision.gameObject.name == "Player")
        {
            player.GetComponent<movement>().hp -= 100;
            player.GetComponent<movement>().hpText.text = player.GetComponent<movement>().hp.ToString();
            player.GetComponent<Rigidbody2D>().AddForce(new Vector2(2.5f * storona, 0));
            at = true;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            if (anim.GetBool("attack1") && !at)
            {
                player.GetComponent<movement>().hp -= 50;
                player.GetComponent<movement>().hpText.text = player.GetComponent<movement>().hp.ToString();
                at = true;
            }
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
    void respGO(GameObject weap,Vector2 respawnPulia,Quaternion rotation,string name,int damage,float speed)
    {
        GameObject weapon = Instantiate(weap, respawnPulia, rotation);
        weapon.name = name;
        weapon.GetComponent<weapon>().hoziain = gameObject.name;
        weapon.GetComponent<weapon>().damage = damage;
        weapon.transform.localScale = new Vector3(Mathf.Sign(polet.x) * transform.localScale.x, transform.localScale.y, transform.localScale.z);
        weapon.GetComponent<Rigidbody2D>().velocity = polet.normalized * speed;
    }
    IEnumerator rebAttack2()
    {
        yield return new WaitForSeconds(6f);
        rebornAttack2 = false;
        anim.SetBool("rebornAttack2", false);
        endattack();
    }
    IEnumerator rebA2wa()
    {
        at = true;
        yield return new WaitForSeconds(0.2f);
        at = false;
    }
}
