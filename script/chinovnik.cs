using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class chinovnik : MonoBehaviour
{
    public float nachspeed;
    public int hp = 3000;
    public float speed = 4;
    public int damage = 150;
    public bool damageinpl = false;
    bool PlayerIsready = true;
    bool enabl = false;
    int numprizivInfunctPriziv = 0;
    int stad = 1;
    int numattack = 1;
    int numpriziv = 0;
    public int storona = 1;
    bool go = false;
    bool prizval = false;
    bool nachMadness = false;
    bool mad = false;
    bool run = false;
    bool snipeat = false;
    bool at = false;
    Vector2 polet;
    GameObject player;
    public GameObject puli;
    public GameObject serp;
    public GameObject snipeluch;
    public GameObject[] lazerVishka;
    public GameObject rocket;
    public GameObject molot;
    public GameObject dw;
    Rigidbody2D rb;
    Animator anim;
    Text hpText;
    Text nameText;
    Vector2 wh;
    // Start is called before the first frame update
    void Start()
    {
        gameObject.name = "ЧIНОВНIК";
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.Find("Player");
        anim = GetComponent<Animator>();
        snipeluch.SetActive(false);
        StartCoroutine(healPlayer());
    }

    private void FixedUpdate()
    {
        if (hp > 0)
        {
            Move();
            storona = -(int)Mathf.Sign(transform.position.x - player.transform.position.x);
            transform.localScale = new Vector2(storona, transform.localScale.y);
        }
        if (snipeluch.active&&!snipeat)
        {
            polet = new Vector2(transform.position.x-player.transform.position.x,transform.position.y+0.18f-player.transform.position.y);
            float atan = Mathf.Atan(polet.y/polet.x*storona);
            snipeluch.transform.rotation = Quaternion.Euler(0, 0, atan*Mathf.Rad2Deg*storona);
            snipeluch.transform.localPosition = new Vector2(Mathf.Cos(atan)*6.3f,Mathf.Sin(atan)*6.3f+0.18f);
        }
    }
    void Update()
    {
        if (hp > 0)
        {
            if (!enabl)
            {
                if (Vector2.Distance(transform.position, player.transform.position) < 3f && Mathf.Abs(transform.position.y - player.transform.position.y) < 1)
                {
                    hpText = GameObject.Find("BossHP").GetComponent<Text>();
                    nameText = GameObject.Find("BossName").GetComponent<Text>();
                    nameText.text = gameObject.name;
                    hpText.text = hp.ToString();
                    anim.SetBool("run", true);
                    enabl = true;
                }
            }
            else
            {
                if (hp > 2200)
                {
                    if (!go)
                    {
                        if (Mathf.Abs(transform.position.x - player.transform.position.x) > 2.5f) anim.SetBool("sniper", true);
                        else if (Mathf.Abs(transform.position.x - player.transform.position.x) < 2.5f && Mathf.Abs(transform.position.x - player.transform.position.x) > 0.5f) anim.SetBool("pistol", true);
                        else if (Mathf.Abs(transform.position.x - player.transform.position.x) < 0.5f) anim.SetBool("serp", true);
                        run = false;
                        go = true;
                    }
                }
                else if (hp > 1200 && hp <= 2200)
                {
                    stad = -1;
                    if (!prizval)
                    {
                        endattack();
                        go = true;
                        numattack =(int)Random.Range(0, 3);
                        if (numattack <2) anim.SetBool("priziv", true);
                        else anim.SetBool("serp", true);
                        StartCoroutine(waitPriziv(6));
                        run = false;
                    }
                    else
                    {
                        if (!go)
                        {
                            if (Mathf.Abs(transform.position.x - player.transform.position.x) < 1.2f) run = true;
                            else
                            {
                                anim.SetBool("sniper", true);
                                run = false;
                                go = true;
                            }
                        }
                    }
                }
                else if (hp <= 1200)
                {
                    stad = 1;
                    if (!prizval)
                    {
                        endattack();
                        go = true;
                        numattack = (int)Random.Range(0, 3);
                        if (numattack < 2) anim.SetBool("priziv", true);
                        else anim.SetBool("serp", true);
                        StartCoroutine(waitPriziv(4));
                        run = false;
                    }
                    else
                    {
                        if (!go)
                        {
                            if (Mathf.Abs(transform.position.x - player.transform.position.x) > 2.3f) run = true;
                            else
                            {
                                anim.SetBool("pistol", true);
                                run = false;
                                go = true;
                            }
                        }
                    }
                }
                if (hp <= 500)
                {
                    if (!mad) {
                        StartCoroutine(madness(1f));
                        priziv();
                    }
                }
            }
        }
        else
        {
            dw.SetActive(true);
            hpText.enabled = false;
            nameText.enabled = false;
            Destroy(gameObject);
        }
    }
    void punch()
    {
        polet = (snipeluch.transform.localPosition).normalized;
        polet = new Vector2(polet.x * storona, polet.y);
        float atan = Mathf.Atan(polet.y / polet.x * storona);
        Quaternion rotation = Quaternion.Euler(0, 0, atan * Mathf.Rad2Deg * storona);
        Vector2 respawnPulia = new Vector2(transform.position.x + 0.4f*storona, transform.position.y + 0.12f);
        respGO(puli, respawnPulia, rotation, "pulia", 200, 20);
    }
    void snipeAttack()
    {
        snipeluch.SetActive(!snipeluch.active);
        snipeat = false;
    }
    void readysnipe()
    {
        snipeat = true;
    }
    void SerpBorn()
    {
        if(GameObject.Find("molot")!=null||(hp>1400)) for(int i = 0; i < 4; i++)
        {
            int ea= 90 * i;
            GameObject s= Instantiate(serp, new Vector2(transform.position.x+0.6f*Mathf.Cos(ea*Mathf.Deg2Rad), transform.position.y+0.6f*Mathf.Sin(ea*Mathf.Deg2Rad)),new Quaternion(0,0,0,0));
            s.GetComponent<serp>().eangl = ea;
            s.transform.parent=transform;
            s.name="serp";
        }
        else for(int i = 0; i < 8; i++)
        {
            int ea= 45 * i-45;
            GameObject m= Instantiate(molot, new Vector2(transform.position.x+0.6f*Mathf.Cos(ea*Mathf.Deg2Rad), transform.position.y+0.6f*Mathf.Sin(ea*Mathf.Deg2Rad)),new Quaternion(0,0,0,0));
            m.GetComponent<serp>().eangl = ea;
            m.transform.parent=transform;
            m.name = "molot";
        }
    }
    void priziv()
    {
        if (hp > 1000) numprizivInfunctPriziv = 0;
        else if (hp < 1000 && hp > 500) numprizivInfunctPriziv = (int)Random.Range(0, 2);
        else numprizivInfunctPriziv = 1;
        if(numprizivInfunctPriziv==0)for(int i = 0; i < 3; i++)
        {
            lazerVishka[i].GetComponent<snipelazer>().enabled = true ;
            lazerVishka[i].GetComponent<snipelazer>().ready = true;
        }
        else for (int i = 0; i < 5; i++)
        {
            polet = new Vector2(0, -1);
            Vector2 respawnPulia = new Vector2(player.transform.position.x + (2 - i) + storona * 0.1f, transform.position.y + 8);
            Quaternion rotation = Quaternion.Euler(0, 0, 0);
            respGO(rocket, respawnPulia, rotation, "rocket", 70, 0.7f);
        }
    }
    void AttackPulia()
    {
        polet = (-transform.position + player.transform.position).normalized;
        Vector2 respawnPulia = new Vector2(transform.position.x + 0.419f * storona, transform.position.y + 0.067f);
        Quaternion rotation = Quaternion.Euler(0, 0, Mathf.Atan2(polet.y, polet.x) * Mathf.Rad2Deg);
        respGO(puli, respawnPulia, rotation, "pulia", 50, 4);
    }
    void endattack()
    {
        anim.SetBool("pistol", false);
        anim.SetBool("sniper", false);
        anim.SetBool("serp", false);
        anim.SetBool("priziv", false);
        snipeluch.SetActive(false);
        go = false;
    }
    void Move()
    {
        if (run) rb.velocity = new Vector2(storona * speed*stad, rb.velocity.y);
    }
    void respGO(GameObject weap, Vector2 respawnPulia, Quaternion rotation, string name, int damage, float speed)
    {
        GameObject weapon = Instantiate(weap, respawnPulia, rotation);
        weapon.name = name;
        weapon.GetComponent<weapon>().hoziain = gameObject.name;
        weapon.GetComponent<weapon>().damage = damage;
        weapon.transform.localScale = new Vector3(Mathf.Sign(polet.x) * transform.localScale.x, transform.localScale.y, transform.localScale.z);
        weapon.GetComponent<Rigidbody2D>().velocity = polet.normalized * speed;
    }
    IEnumerator waitPriziv(float time)
    {
        prizval = true;
        yield return new WaitForSeconds(time);
        prizval = false;
    }
    IEnumerator madness(float time)
    {
        mad = true;
        yield return new WaitForSeconds(time);
        mad = false;
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (anim.GetBool("run"))
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
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "patron")
        {
            hp -= 50;
            hpText.text = hp.ToString();
        }
    }
    IEnumerator wait(int variant, float time)
    {
        PlayerIsready = false;
        if (variant == 1) hp -= 25;
        else if (variant == 2) hp -= 150;
        hpText.text = hp.ToString();
        yield return new WaitForSeconds(time);
        PlayerIsready = true;
    }
    IEnumerator healPlayer()
    {
        yield return new WaitForSeconds(8);
        player.GetComponent<movement>().hp += 50;
        player.GetComponent<movement>().hpText.text = player.GetComponent<movement>().hp.ToString();
        StartCoroutine(healPlayer());
    }
}
