using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Experimental.Rendering.Universal;
public class GhostBOSS : MonoBehaviour
{
    bool atack = false;
    bool PlayerIsready=true;
    bool enabl = false;
    bool ready = true;
    bool at4 = false;
    bool vol = true;
    bool mouse = false;
    bool numrandom = false;
    public bool damageInPlayer=true;
    int hp = 1500;
    int storona = 1;
    int numattack = 1;
    public float speed = 1f;
    public GameObject ghost;
    public GameObject luch;
    public GameObject key;
    public GameObject artefact;
    GameObject pla;
    Text hpText;
    Vector2 tr;
    Vector2 pl;
    Animator anim;
    Transform respawn;
    Text nameText;
    Rigidbody2D rb;
    Light2D light;
    // Start is called before the first frame update
    void Start()
    {
        gameObject.name = "ПРiВiДЕНiЕ";
        pla = GameObject.Find("Player");
        hpText = GameObject.Find("BossHP").GetComponent<Text>();
        nameText = GameObject.Find("BossName").GetComponent<Text>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        respawn = GetComponent<Transform>();
        light = GetComponent<Light2D>();
    }

    private void FixedUpdate()
    {
        if (anim.GetBool("attack4")) light.intensity = 0;
        else
        {
            if (vol)
            {
                if (light.intensity < 1) light.intensity += 0.01f;
                else vol = false;
            }
            else
            {
                if (light.intensity > 0.5f) light.intensity -= 0.01f;
                else vol = true;
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        Debug.Log(numattack);
        if (hp > 0)
        {
            if (!enabl)
            {
                if (Vector2.Distance(transform.position, pla.transform.position) < 5f && Mathf.Abs(transform.position.y - pla.transform.position.y) < 3)
                {
                    nameText.text = gameObject.name;
                    hpText.text = hp.ToString();
                    enabl = true;
                    GameObject.Find("dwerKBoss").GetComponent<BoxCollider2D>().enabled = true;
                    GameObject.Find("AudioGhost").GetComponent<AudioSource>().enabled = true;
                    GameObject.Find("Audio").GetComponent<AudioSource>().enabled = false;
                }
                else
                {
                    hpText.text = "";
                    nameText.text = "";
                }
            }
            else
            {
                if (Mathf.Abs(pla.transform.position.x - transform.position.x) > 0.07f)
                    storona = (int)Mathf.Sign(pla.transform.position.x - transform.position.x);
                pl = pla.transform.position;
                if (ready)
                {
                    anim.SetBool("Run", true);
                    tr = new Vector2((pl.x - transform.position.x), (pl.y - transform.position.y));
                    transform.localScale = new Vector2(storona * Mathf.Abs(transform.localScale.x), transform.localScale.y);
                    switch (numattack)
                    {
                        case 1:
                            if (Mathf.Abs(tr.x) < 0.5f && !atack)
                            {
                                anim.SetBool("attack1", true);
                                StartCoroutine(timeout(1.5f));
                                rb.velocity = tr.normalized * speed * 1.5f;
                            }
                            break;
                        case 2:
                            if (Mathf.Abs(tr.x) < 0.8f && !atack)
                            {
                                anim.SetBool("attack2", true);
                                StartCoroutine(waitforattack2());
                                rb.velocity = new Vector2(0, 0);
                            }
                            break;
                        case 3:
                            if (Mathf.Abs(tr.x) > 1.1f && !atack)
                            {
                                damageInPlayer = true;
                                anim.SetBool("attack3", true);
                                StartCoroutine(timeout(3f));
                            }
                            else numrandom = false;
                            break;
                        case 4:
                            if (Mathf.Abs(tr.x) < 0.5f && !atack)
                            {
                                anim.SetBool("attack4", true);
                                StartCoroutine(timeout(7f));
                                rb.velocity = new Vector2(0, 0);
                            }
                            break;
                    }
                }
                else
                {
                    if (numattack == 3)
                    {
                        if (Mathf.Abs(pl.y - transform.position.y) > 0.2f) rb.velocity = new Vector2(0, Mathf.Sign(pla.transform.position.y - transform.position.y) * speed / 1.5f);
                        else rb.velocity = new Vector2(0, speed / 1.5f);
                    }
                    if (numattack == 4)
                    {
                        if (Mathf.Abs(pl.x - transform.position.x) > 0.2f && at4)
                        {
                            tr = new Vector2((pl.x - transform.position.x), (pl.y - transform.position.y));
                            transform.localScale = new Vector2(storona * Mathf.Abs(transform.localScale.x), transform.localScale.y);
                        }
                        else StartCoroutine(waitforattack4());
                    }

                }
                if (!numrandom)
                {
                    numattack = 4;
                    if (hp > 800)
                    {
                        numattack = (int)Random.Range(0, 40);
                        if (numattack < 20) numattack = 1;
                        else if (numattack > 20 && numattack < 30) numattack = 2;
                        else numattack = 3;
                    }
                    else if (hp > 300 && hp < 800)
                    {
                        numattack = (int)Random.Range(0, 50);
                        if (numattack < 10) numattack = 1;
                        else if (numattack > 10 && numattack < 25) numattack = 2;
                        else if (numattack > 25 && numattack < 40) numattack = 3;
                        else numattack = 4;
                    }
                    else
                    {
                        numattack = (int)Random.Range(0, 50);
                        if (numattack > 0 && numattack < 10) numattack = 2;
                        else if (numattack > 10 && numattack < 15) numattack = 3;
                        else numattack = 4;
                    }
                    numrandom = true;
                }
                if (!atack)
                {
                    rb.velocity = tr.normalized * speed;
                }
                if (numattack == 4) rb.velocity = tr.normalized * speed * 1.3f;
            }
        }
        else
        {
            hpText.text = "0";
            anim.SetBool("Death", true);
            rb.velocity = new Vector2(0, 0);
        }
    }
    void attack1()
    {
        anim.SetBool("attack1", false);
    }
    void Death()
    {
        nameText.text = "";
        hpText.text = "";
        key.GetComponent<veshi>().value = 15;
        GameObject patrVistrel = Instantiate(key, new Vector2(transform.position.x, pla.transform.position.y), Quaternion.identity);
        GameObject.Find("dwerKBoss").GetComponent<BoxCollider2D>().enabled = false;
        Instantiate(artefact, new Vector2(transform.position.x + 0.15f, pla.transform.position.y), Quaternion.identity);
        Destroy(gameObject);
    }
    void attack3()
    {
        transform.localScale = new Vector2(-Mathf.Sign(transform.position.x - pl.x) * transform.localScale.y, transform.localScale.y);
        StartCoroutine(waitforattack3());
    }
    void attack4()
    {
        at4 = true;
    }
    void mouseLight()
    {
        mouse = true;
    }
    void attack3end()
    {
        anim.SetBool("attack3", false);
        var ObjectBonus1 = GameObject.FindGameObjectsWithTag("luch");
        for (int i = 0; i < ObjectBonus1.Length; i++)
        {
            Destroy(ObjectBonus1[i]);
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.name == "kulak" && pla.GetComponent<Animator>().GetBool("dwoechka") && PlayerIsready)
        {
            StartCoroutine(wait(1, 0.2f));
        }
        else if (collision.gameObject.name == "lokot" && pla.GetComponent<Animator>().GetBool("lokot") && PlayerIsready)
        {
            StartCoroutine(wait(2, 1.5f));
        }
        if (collision.gameObject.name == "Player" && anim.GetBool("attack4") && damageInPlayer) StartCoroutine(attackDamage(0.4f));
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player" && anim.GetBool("attack1"))
        {
            pla.GetComponent<movement>().hp -= 20;
            pla.GetComponent<movement>().hpText.text = pla.GetComponent<movement>().hp.ToString();
        }
    }
    IEnumerator wait(int variant, float time)
    {
        PlayerIsready = false;
        if (variant == 1) hp -= 25;
        else if (variant == 2) hp -= 125;
        hpText.text = hp.ToString();
        yield return new WaitForSeconds(time);
        PlayerIsready = true;
    }
    IEnumerator timeout(float time)
    {
        atack = true;
        ready = false;
        yield return new WaitForSeconds(time);
        ready = true;
        atack = false;
        numrandom = false;
        attack3end();
        anim.SetBool("attack2", false);
        anim.SetBool("attack4", false);
        mouse = false;
    }
    IEnumerator waitforattack3()
    {
        int st = storona;
        for (int i = 0; i < 50; i++)
        {
            yield return new WaitForSeconds(0.04f);
            Vector2 lol = new Vector2(respawn.position.x + 0.3f * st * (1 + i)+0.2f*st, respawn.position.y);
            GameObject kek = Instantiate(luch, lol, Quaternion.identity);
            kek.GetComponent<luch>().boss = true;
            kek.GetComponent<luch>().hoz = "lazer";
            kek.transform.SetParent(transform);
            kek.transform.position = lol;
        }
    }
    IEnumerator waitforattack2()
    {
        atack = true;
        ready = false;
        for (int i = 0; i < 5; i++)
        {
            yield return new WaitForSeconds(0.1f);
            Vector2 lol = new Vector2(respawn.position.x + (i / 2 - 1), respawn.position.y + (1 - Mathf.Abs(2 - i) / 2));
            GameObject kek = Instantiate(ghost, lol, respawn.rotation);
        }
        yield return new WaitForSeconds(1);
        StartCoroutine(timeout(0));
    }
    public IEnumerator attackDamage(float time)
    {
        damageInPlayer = false;
        if (mouse)
        {
            pla.GetComponent<movement>().hp -= 10;
            hp += 10;
        }
        else if (anim.GetBool("attack3"))
            pla.GetComponent<movement>().hp -= 15;
        pla.GetComponent<movement>().hpText.text = pla.GetComponent<movement>().hp.ToString();
        hpText.text = hp.ToString();
        yield return new WaitForSeconds(time);
        damageInPlayer = true;
    }
    IEnumerator waitforattack4()
    {
        at4 = false;
        yield return new WaitForSeconds(0.5f);
        at4 = true;
    }
}
