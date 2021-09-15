using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class alchimick : MonoBehaviour
{
    public float nachspeed;
    public int hp = 2000;
    public float speed = 4;
    public int damage = 150;
    public bool damageinpl = false;
    bool PlayerIsready = true;
    bool enabl = false;
    bool ready;
    bool numrandom = false;
    int numattack = 0;
    int numtele = 0;
    int kol_voPopadanii = 0;
    public int storona = 1;
    int maxnum = 5;
    public float animKadr = -1;
    float time = 1;
    bool plaInobj = false;
    bool at = false;
    bool is_cold = false;
    bool is_poison = false;
    Vector2 polet;
    GameObject player;
    public GameObject[] Probirka_for_attack;
    public GameObject rainbow;
    public GameObject coin;
    public GameObject flower;
    public GameObject key;
    GameObject []teleport;
    Rigidbody2D rb;
    Animator anim;
    Text hpText;
    Text nameText;
    Vector2 wh;
    // Start is called before the first frame update
    void Start()
    {
        gameObject.name = "АЛХIМIК";
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.Find("Player");
        anim = GetComponent<Animator>();
        teleport = GameObject.FindGameObjectsWithTag("teleport");
        GameObject.Find("Audio").GetComponent<AudioSource>().enabled = false;
        GameObject.Find("AudioAlchimick").GetComponent<AudioSource>().enabled = true;
    }
    // Update is called once per frame
    void Update()
    {
        if (hp > 0)
        {
            storona = -(int)Mathf.Sign(transform.position.x - player.transform.position.x);
            transform.localScale = new Vector2(storona, transform.localScale.y);
            if (!enabl)
            {
                if (Vector2.Distance(transform.position, player.transform.position) < 3f && Mathf.Abs(transform.position.y - player.transform.position.y) < 1)
                {
                    hpText = GameObject.Find("BossHP").GetComponent<Text>();
                    nameText = GameObject.Find("BossName").GetComponent<Text>();
                    GameObject.Find("dwerKBoss").GetComponent<BoxCollider2D>().enabled = true;
                    nameText.text = gameObject.name;
                    hpText.text = hp.ToString();
                    enabl = true;
                    ready = true;
                }
            }
            else
            {
                if (ready)
                {
                    if (numrandom)
                    {
                        if (numattack == 1) anim.SetBool("attack1", true);
                        if (numattack == 2) anim.SetBool("attack2", true);
                        if (numattack == 3) anim.SetBool("attack3", true);
                        if (numattack == 6) anim.SetBool("rainbow", true);
                        if (numattack == 5) anim.SetBool("money", true);
                        if (numattack == 4) anim.SetBool("kleverBorn", true);
                    }
                    if (hp < 1500)
                    {
                        anim.speed = 1.5f;
                        maxnum = 6;
                    }
                    if (hp < 1000)
                    {
                        time = 0.3f;
                        maxnum = 7;
                    }
                    if (hp < 500) time = 0;
                }
                if (!numrandom)
                {
                    numattack = (int)Random.Range(1, 6.9999f);
                    numrandom = true;
                }
            }
        }
        else
        {
            if (is_cold)
            {
                player.GetComponent<movement>().speed = 1.5f * player.GetComponent<movement>().speed;
                player.GetComponent<movement>().speedJump = 1.5f * player.GetComponent<movement>().speedJump;
            }
            GameObject.Find("Audio").GetComponent<AudioSource>().enabled = true;
            GameObject.Find("AudioAlchimick").GetComponent<AudioSource>().enabled = false;
            Death();
        }
    }
    void Death()
    {
        nameText.text = "";
        hpText.text = "";
        key.GetComponent<veshi>().value = 21;
        GameObject patrVistrel = Instantiate(key, new Vector2(transform.position.x, player.transform.position.y), Quaternion.identity);
        GameObject.Find("dwerKBoss").GetComponent<BoxCollider2D>().enabled = false;
        Destroy(gameObject);
    }
    public void c()
    {
        if(!is_cold)StartCoroutine(cold());
    }
    public void p()
    {
        if(!is_poison)StartCoroutine(poison());
    }
    void nachTelep()
    {
        GetComponent<BoxCollider2D>().enabled = false;
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
    }
    void teleportation()
    {
        if (!anim.GetBool("reteleport"))
        {
            transform.position = teleport[numtele].transform.position;
            anim.SetBool("reteleport", true);
        }
        else
        {
            rb.constraints = RigidbodyConstraints2D.None;
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;
            GetComponent<BoxCollider2D>().enabled = true;
            anim.SetBool("reteleport", false);
            anim.SetBool("teleport", false);
        }
    }
    void rainb()
    {
        polet = new Vector2(0, 0);
        Vector2 respawnPulia = new Vector2(storona * 0.27f + transform.position.x, transform.position.y);
        respGO(rainbow, respawnPulia, new Quaternion(0, 0, 0, 0), "rainbow", 10, 0);
    }
    void coinAttack()
    {
        Vector2 cursor = new Vector2(player.transform.position.x, player.transform.position.y + 0.2f);
        polet = (cursor - (Vector2)transform.position).normalized;
        Vector2 respawnPulia = new Vector2(storona * 0.27f + transform.position.x, transform.position.y);
        respGO(coin, respawnPulia, new Quaternion(0, 0, 0, 0), "coin", 10, 3);
    }
    void kleverBorn()
    {
        polet = new Vector2(0, 0);
        Vector2 respawnPulia = new Vector2(player.transform.position.x+Random.Range(-0.5f,0.5f), transform.position.y);
        respGO(flower, respawnPulia, new Quaternion(0, 0, 0, 0), "flower", 150, 0);
    }
    void create()
    {
        float rast = Mathf.Abs((transform.position.x - player.transform.position.x));
        float rast2 = player.transform.position.y-transform.position.y;
        float Vy;
        if (rast < 2.5f && rast2 < 1f && rast2 > 0f) Vy = 2;
        else if (rast2 > 1f) Vy = 2 + rast2;
        else if (rast2 < 0f) Vy = 0;
        else Vy = 2 + rast - 2.5f;
        float Vx;
        if (rast * 9.8f - Vy * Vy > 0.1f) Vx = Mathf.Sqrt(rast * 9.8f - Vy * Vy);
        else Vx = 2;
        polet = new Vector2(Vx*storona,Vy);
        Vector2 respawnPulia = new Vector2(storona * 0.115f + transform.position.x, transform.position.y + 0.175f);
        respGO(Probirka_for_attack[numattack-1],respawnPulia,new Quaternion(0, 0, 0, 0), "prob"+numattack.ToString(), 5,1);
    }
    void respGO(GameObject weap, Vector2 respawnPulia, Quaternion rotation, string name, int damage, float speed)
    {
        GameObject weapon = Instantiate(weap, respawnPulia, rotation);
        weapon.name = name;
        weapon.GetComponent<weapon>().hoziain = gameObject.name;
        weapon.GetComponent<weapon>().damage = damage;
        if (name == "prob") weapon.transform.localScale = new Vector3(2, 1.5f, 1);
        else weapon.transform.localScale = new Vector3(storona * transform.localScale.x, transform.localScale.y, transform.localScale.z);
        weapon.GetComponent<Rigidbody2D>().velocity = polet * speed;
    }
    void endat()
    {
        if (numattack == 1) anim.SetBool("attack1", false);
        if (numattack == 2) anim.SetBool("attack2", false);
        if (numattack == 3) anim.SetBool("attack3", false);
        if (numattack == 6) anim.SetBool("rainbow", false);
        if (numattack == 5) anim.SetBool("money", false);
        if (numattack == 4) anim.SetBool("kleverBorn", false);
        StartCoroutine(waitAt());
    }
    IEnumerator waitAt()
    {
        ready = false;
        if (Vector2.Distance(transform.position, player.transform.position) < 0.5f|| Vector2.Distance(transform.position, player.transform.position) > 6f||kol_voPopadanii>=4)
        {
            while (Vector2.Distance(teleport[numtele].transform.position, transform.position) < 1f || Vector2.Distance(teleport[numtele].transform.position, player.transform.position) < 1f)
            {
                numtele = (int)Random.Range(0, teleport.Length);
            }
            kol_voPopadanii = 0;
            if (!anim.GetBool("reteleport")) anim.SetBool("teleport", true);
        }
        yield return new WaitForSeconds(time);
        numrandom = false;
        ready = true;
    }
    IEnumerator poison()
    {
        is_poison = true;
        for(int i = 0; i < 15; i++)
        {
            yield return new WaitForSeconds(1f);
            player.GetComponent<movement>().hp -= 5;
            player.GetComponent<movement>().hpText.text = player.GetComponent<movement>().hp.ToString();
        }
        is_poison = false;
    }
    IEnumerator cold()
    {
        is_cold = true;
        player.GetComponent<movement>().speed /= 1.5f;
        player.GetComponent<movement>().speedJump /= 1.5f;
        yield return new WaitForSeconds(10);
        player.GetComponent<movement>().speed = 1.5f* player.GetComponent<movement>().speed;
        player.GetComponent<movement>().speedJump = 1.5f*player.GetComponent<movement>().speedJump;
        is_cold = false;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "patron")
        {
            kol_voPopadanii++;
            hp -= 50;
            hpText.text = hp.ToString();
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
        if (variant == 1) hp -= 25;
        else if (variant == 2) hp -= 170;
        hpText.text = hp.ToString();
        yield return new WaitForSeconds(time);
        PlayerIsready = true;
    }
}
