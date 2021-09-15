using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Experimental.Rendering.Universal;
public class EXODUSboss : MonoBehaviour
{
    bool PlayerIsready = true;
    bool enabl = false;
    bool ready = true;
    bool lch = false;
    bool numrandom = false;
    bool rivend = true;
    bool riv = false;
    public bool damageInPlayer = true;
    float time = 3.2f;
    public int hp = 3000;
    int storona = 1;
    int podniatii = 1;
    int podniatiiPriLuch = 1;
    int numattack = 1;
    int run = 250;
    public float speed = 1f;
    public GameObject prizivSinii;
    public GameObject fireb;
    public GameObject lazerLuch;
    public GameObject key;
    public GameObject[] spawnNOTenemy;
    public GameObject[] spawnEnemy;
    GameObject player;
    Text hpText;
    Vector2 wh;
    Vector2 pl;
    Vector2 polet;
    Animator anim;
    Transform respawn;
    Text nameText;
    Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        gameObject.name = "EXODUS";
        GetComponent<Collider2D>().isTrigger = true;
        player = GameObject.Find("Player");
        hpText = GameObject.Find("BossHP").GetComponent<Text>();
        nameText = GameObject.Find("BossName").GetComponent<Text>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        GetComponent<exodus>().enabled = false;
        StartCoroutine(lol());
    }
    private void FixedUpdate()
    {
        if (hp > 0)
        {
            Move();
            if(!riv&&!lch)transform.localScale = new Vector2(storona*Mathf.Abs(transform.localScale.x), transform.localScale.y);
        }
        else
        {
            hpText.enabled = false; ;
            nameText.enabled = false ;
            PlayerPrefs.SetString(SceneManager.GetActiveScene().name + "done", "complete");
            PlayerPrefs.Save();
            GameObject.Find("DieMessage").GetComponent<Text>().fontSize = 13;
            GameObject.Find("DieMessage").GetComponent<Text>().text = "Поздравляем! Вы освободили этот мир от злости. Теперь тут одна доброта). Для выхода нажмите <<Выход>>.Ждите патч релиза и DLC";
            Destroy(gameObject);
        }
        if (GameObject.Find("Main Camera").GetComponent<Camera>().orthographicSize < 1.9f)
            GameObject.Find("Main Camera").GetComponent<Camera>().orthographicSize = GameObject.Find("Main Camera").GetComponent<Camera>().orthographicSize + 0.01f;
    }
    // Update is called once per frame
    void Update()
    {
        if (!enabl)
        {
            if (Vector2.Distance(transform.position, player.transform.position) < 4f && Mathf.Abs(transform.position.y - player.transform.position.y) < 1)
            {
                nameText.text = gameObject.name;
                hpText.text = hp.ToString();
                enabl = true;
                anim.SetBool("boss", true);
                //GameObject.Find("dwerKBoss").GetComponent<BoxCollider2D>().enabled = true;
            }
            else
            {
                hpText.text = "";
                nameText.text = "";
            }
        }
        else
        {
            if ((!riv && Mathf.Abs(transform.position.y - player.transform.position.y) < 0.5f)&&!lch)
            {
                Vector3 tempvector = new Vector3(0, podniatii, 0);
                transform.position = Vector3.MoveTowards(transform.position, transform.position + tempvector, speed * Time.deltaTime);
            }
            else if(Mathf.Abs(transform.position.y - player.transform.position.y) >= 0.5f&&!riv&&!lch)
            {
                Vector3 tempvector = new Vector3(0, -Mathf.Sign(transform.position.y - player.transform.position.y+0.2f), 0);
                transform.position = Vector3.MoveTowards(transform.position, transform.position + tempvector, speed*5 * Time.deltaTime);
            }
            if (lch)
            {
                Vector3 tempvector = new Vector3(0, podniatiiPriLuch, 0);
                transform.position = Vector3.MoveTowards(transform.position, transform.position + tempvector, speed * Time.deltaTime);
            }
            if (ready)
            {
                if (hp > 2000)
                {
                    int n = (int)Random.Range(0, 3);
                    if (n == 0) anim.SetBool("fireball", true);
                    else if (n == 1) anim.SetBool("spawnNOTenemy", true);
                    else anim.SetBool("rivok", true);
                }
                else if (hp > 1000 && hp <= 2000)
                {
                    int n = (int)Random.Range(0, 100);
                    if (n < 20) anim.SetBool("fireball", true);
                    else if (n >= 20 && n < 35) anim.SetBool("luch", true);
                    else if (n >= 35 && n < 65) anim.SetBool("rivok", true);
                    else if (n >= 65 && n < 85) anim.SetBool("spawnNOTenemy", true);
                    else anim.SetBool("spawn", true);
                }
                else if (hp > 0 && hp <= 1000)
                {
                    int n = (int)Random.Range(0, 3);
                    if (Mathf.Abs(transform.position.x - player.transform.position.x) > 3)
                    {
                        if (n == 0) anim.SetBool("spawnNOTenemy", true);
                        else if (n == 1) anim.SetBool("spawn", true);
                        else if(n==2) anim.SetBool("luch", true);
                    }
                    else anim.SetBool("rivok", true);
                }
                ready = false;
            }
        }
    }
    void fireball()
    {
        for (int i = 0; i < 3; i++)
        {
            int ea = 90+120 * i;
            GameObject s = Instantiate(fireb, new Vector2(transform.position.x + 0.6f * Mathf.Cos(ea * Mathf.Deg2Rad), transform.position.y + 0.6f * Mathf.Sin(ea * Mathf.Deg2Rad)), new Quaternion(0, 0, 0, 0));
            s.GetComponent<serp>().eangl = ea;
            s.transform.parent = transform;
            s.name = "fireball";
        }
    }
    void rivok()
    {
        int st = storona;
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        riv = true;
        transform.localScale = new Vector2(storona * Mathf.Abs(transform.localScale.x), transform.localScale.y);
        rb.velocity = new Vector2(speed * 30 * st, 0);
        if(hp>=1000)StartCoroutine(wr(1.5f));
        else StartCoroutine(wr(4f));
    }
    void luch()
    {
        speed *= 7;
        StartCoroutine(lazerAttack());
    }
    void luchAttackend()
    {
        speed /= 7;
        anim.SetBool("luch", false);
        var ObjectBonus1 = GameObject.FindGameObjectsWithTag("luch");
        damageInPlayer = true;
        lch = false;
        for (int i = 0; i < ObjectBonus1.Length; i++)
        {
            Destroy(ObjectBonus1[i]);
        }
        endAt();
    }
    void priziv()
    {
        GameObject portal=Instantiate(prizivSinii, new Vector2(transform.position.x + storona * 1,transform.position.y+0.5f), new Quaternion(0, 0, 0, 0));
        StartCoroutine(wpr(portal));
    }
    void prizivNOTenemy()
    {
        GameObject obj;
        int kek;
        if (hp < 1300) kek = (int)Random.Range(0, 3);
        else kek = Random.Range(1, 3);
        if (kek < 2) numattack = 0;
        else if (kek == 2) numattack = 1;
        else numattack = 2;
        if (numattack == 0)
        {
            polet = new Vector2(0, 0);
            obj = respGO(spawnNOTenemy[numattack], new Vector2(transform.position.x + Random.Range(1, 2.5f)*storona, transform.position.y + 0.5f), new Quaternion(0, 0, 0, 0), "vulkan", 20, 0);
        }
        else if (numattack == 1)
        {
            polet = new Vector2(0, 0);
            obj=respGO(spawnNOTenemy[numattack], new Vector2(transform.position.x + Random.Range(1, 2.5f)*storona, transform.position.y + 0.5f),new Quaternion(0,0,0,0), "flower", 150, 0);
        }
        else
        {
            polet = new Vector2(storona, 0);
            Quaternion rotation = Quaternion.Euler(0, 0, Mathf.Atan2(polet.y, polet.x) * Mathf.Rad2Deg);
            Vector2 respawnPulia = new Vector2(storona * 0.2f + transform.position.x, transform.position.y + 0.1f);
            obj=respGO(spawnNOTenemy[numattack], respawnPulia, rotation, "lch3", 60, 3);
        }
        lightOn(obj);
        endAt();
    }
    void endAt()
    {
        StartCoroutine(end());
    }
    IEnumerator wpr(GameObject portal)
    {
        if(hp<1300)numattack =(int) Random.Range(0, 4);
        else numattack = (int)Random.Range(0, 2);
        yield return new WaitForSeconds(1);
        GameObject obj=Instantiate(spawnEnemy[numattack], portal.transform);
        obj.transform.position = portal.transform.position;
        lightOn(obj);
        yield return new WaitForSeconds(1);
        endAt();
        obj.transform.parent = null;
        Destroy(portal);
    }
    IEnumerator end()
    {
        yield return new WaitForSeconds(1);
        ready = true;
        anim.SetBool("rivok", false);
        anim.SetBool("fireball", false);
        anim.SetBool("luch", false);
        anim.SetBool("spawnNOTenemy", false);
        anim.SetBool("spawn", false);
    }
    void lightOn(GameObject obj)
    {
        if (obj.GetComponent<Light2D>() == null)
        {
            obj.AddComponent<Light2D>();
            obj.GetComponent<Light2D>().lightType = Light2D.LightType.Point;
            obj.GetComponent<Light2D>().intensity = 1;
            obj.GetComponent<Light2D>().pointLightOuterRadius = 1;
            obj.GetComponent<Light2D>().intensity = 1;
        }
    }
    IEnumerator wr(float time)
    {
        yield return new WaitForSeconds(time);
        anim.SetBool("rivok", false);
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
        riv = false;
        endAt();
    }
    IEnumerator lazerAttack()
    {
        lch = true;
        StartCoroutine(ppl());
        int st = storona;
        for (int i = 0; i < 50; i++)
        {
            yield return new WaitForSeconds(0.03f);
            Vector2 lol = new Vector2(transform.position.x + 0.3f * st * (1 + i) + 0.2f * st, transform.position.y);
            GameObject kek = Instantiate(lazerLuch, lol, Quaternion.identity);
            kek.GetComponent<luch>().boss = true;
            kek.GetComponent<luch>().hoz = "lazerLuch";
            kek.transform.localScale = new Vector2(storona * 2, 2);
            kek.transform.SetParent(transform);
            kek.transform.position = lol;
        }
        yield return new WaitForSeconds(1);
        luchAttackend();
    }
    public IEnumerator attackDamage(float time)
    {
        damageInPlayer = false;
        if (anim.GetBool("luch"))
        {
            player.GetComponent<movement>().hp -= 10;
            player.GetComponent<movement>().hpText.text = player.GetComponent<movement>().hp.ToString();
            hpText.text = hp.ToString();
        }
        yield return new WaitForSeconds(time);
        damageInPlayer = true;
    }
    void Move()
    {
        storona = -(int)Mathf.Sign(transform.position.x - player.transform.position.x);
    }
    GameObject respGO(GameObject weap, Vector2 respawnPulia, Quaternion rotation, string name, int damage, float speed)
    {
        GameObject weapon = Instantiate(weap, respawnPulia, rotation);
        weapon.name = name;
        weapon.GetComponent<weapon>().hoziain = gameObject.name;
        weapon.GetComponent<weapon>().damage = damage;
        weapon.transform.localScale = new Vector3(Mathf.Sign(polet.x) * transform.localScale.x, transform.localScale.y, transform.localScale.z);
        weapon.GetComponent<Rigidbody2D>().velocity = polet.normalized * speed;
        return weapon;
    }
    void endPerehod() { anim.SetBool("endPerehod", true); }
    IEnumerator lol()
    {
        yield return new WaitForSeconds(1f);
        podniatii = -1 * podniatii;
        StartCoroutine(lol());
    }
    IEnumerator ppl()
    {
        podniatiiPriLuch = (int)-Mathf.Sign(transform.position.y - player.transform.position.y-0.2f);
        yield return new WaitForSeconds(0.4f);
        if (lch) StartCoroutine(ppl());
        else podniatiiPriLuch = 1;
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.name == "kulak" && player.GetComponent<Animator>().GetBool("dwoechka") && PlayerIsready)
        {
            StartCoroutine(wait(1, 0.2f));
            player.GetComponent<movement>().hp += 2;
            player.GetComponent<movement>().hpText.text = player.GetComponent<movement>().hp.ToString();
        }
        else if (collision.gameObject.name == "lokot" && player.GetComponent<Animator>().GetBool("lokot") && PlayerIsready)
        {
            StartCoroutine(wait(2, 1.5f));
            player.GetComponent<movement>().hp += 25;
            player.GetComponent<movement>().hpText.text = player.GetComponent<movement>().hp.ToString();
        }
        if (collision.gameObject.name == "patron")
        {
            hp -= 30;
            player.GetComponent<movement>().hp += 5;
            player.GetComponent<movement>().hpText.text = player.GetComponent<movement>().hp.ToString();
            hpText.text = hp.ToString();
            Destroy(collision.gameObject);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Player" && riv)
        {
            player.GetComponent<movement>().hp -= 25;
            player.GetComponent<movement>().hpText.text = player.GetComponent<movement>().hp.ToString();
        }
    }
    IEnumerator wait(int variant, float time)
    {
        PlayerIsready = false;
        if (variant == 1) hp -= 25;
        else if (variant == 2) hp -= 140;
        hpText.text = hp.ToString();
        yield return new WaitForSeconds(time);
        PlayerIsready = true;
    }
}
