using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Experimental.Rendering.Universal;
public class knightBOSS : MonoBehaviour
{
    public float nachspeed;
    public int hp = 1200;
    public float speed = 4;
    public int damage = 70;
    public bool damageinpl = false;
    bool PlayerIsready = true;
    bool enabl = false;
    bool vol = true;
    bool numrandom = true;
    bool perehod = false;
    int numat = 0;
    int nominal = 4;
    int numattack = 1;
    int numpriziv = 0;
    public int storona = 1;
    public float animKadr = -1;
    bool plaInobj = false;
    bool at = false;
    Vector2 polet;
    GameObject sekira;
    GameObject player;
    GameObject weapon;
    public GameObject []perehodLightnings;
    public GameObject cloud;
    public GameObject lightning;
    public GameObject sek;
    public GameObject artefact;
    public GameObject dw;
    Rigidbody2D rb;
    Animator anim;
    Text hpText;
    Text nameText;
    Vector2 wh;
    Light2D light;
    // Start is called before the first frame update
    void Start()
    {
        gameObject.name = "РIЦАРЬ";
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.Find("Player");
        sekira = GameObject.Find("sekira");
        anim = GetComponent<Animator>();
        light = GetComponent<Light2D>();
        GameObject.Find("Audio").GetComponent<AudioSource>().enabled = false;
        GameObject.Find("AudioKnight").GetComponent<AudioSource>().enabled = true;
    }
    private void FixedUpdate()
    {
        if (hp > 0)
        {
            Move();
            transform.localScale = new Vector2(storona, transform.localScale.y);
            if (anim.GetBool("run")) light.intensity = 0;
            else
            {
                if (vol)
                {
                    if (light.intensity < 0.9f) light.intensity += 0.01f;
                    else vol = false;
                }
                else
                {
                    if (light.intensity > 0.5f) light.intensity -= 0.01f;
                    else vol = true;
                }
            }
        }
    }
    void Update()
    {
        if (hp > 0)
        {
            if (!enabl)
            {
                if (Vector2.Distance(transform.position, player.transform.position) < 1f && Mathf.Abs(transform.position.y - player.transform.position.y) < 1)
                {
                    hpText = GameObject.Find("BossHP").GetComponent<Text>();
                    nameText = GameObject.Find("BossName").GetComponent<Text>();
                    nameText.text = gameObject.name;
                    hpText.text = hp.ToString();
                    enabl = true;
                    anim.SetBool("attack", true);
                }
            }
            else
            {
                if (numat == nominal)
                {
                    numat = 0;
                    anim.SetBool("met", true);
                    if (numattack == 1) nominal = (int)Random.Range(1, 4);
                    else nominal = 1;
                }
                if(hp<=750) if (!perehod) { anim.SetBool("perehod", true); anim.SetBool("attack", false); }
                if (!numrandom&&!anim.GetBool("perehod"))
                {
                    if (hp > 750) numattack = 1;
                    else numattack = (int)Random.Range(1, 50);
                    if (numattack <= 10) { numattack = 1; anim.SetBool("attack", true); }
                    else if (numattack > 10 && numattack <= 35) { numattack = 2; anim.SetBool("cloud", true); }
                    else if (numattack<50&&numattack>35) anim.SetBool("lightning", true);
                    numrandom = true;
                    
                }
            }
        }
        else
        {
            Instantiate(artefact, new Vector2(transform.position.x + 0.15f, player.transform.position.y), Quaternion.identity); 
            hpText.enabled = false ;
            nameText.enabled = false;
            dw.SetActive(true);
            Destroy(gameObject);
        }
    }
    void endat()
    {
        anim.SetBool("attack", false);
        anim.SetBool("cloud", false);
        anim.SetBool("lightning", false);
        numrandom = false;
    }
    void end_perehod() {
        perehod =true;
        light.color = new Color(0.9f, 0.9f, 0);
        anim.SetBool("perehod", false);
    }
    void priziv()
    {
        perehodLightnings[numpriziv].SetActive(true);
        numpriziv++;
    }
    void lightAt()
    {
        Vector2 respawnPulia = new Vector2(storona * 0.32f + transform.position.x, transform.position.y-0.12f);
        respGO(lightning, respawnPulia, new Quaternion(0, 0, 30, 0), "lightning", 100, speed * 6f);
        Vector2 respawnPulia2 = new Vector2(-storona * 0.32f + transform.position.x, transform.position.y - 0.12f);
        respGO(lightning, respawnPulia2, new Quaternion(0, 0, 30, 0), "lightning", 100, -speed * 6f);
    }
    void cloud_at()
    {
        Vector2 respawnPulia = new Vector2(player.transform.position.x, transform.position.y+0.52f);
        respGO(cloud, respawnPulia, new Quaternion(0, 0, 0, 0), "cloud", 100, speed*0.4f);
    }
    void Move()
    {
        storona = -(int)Mathf.Sign(transform.position.x - player.transform.position.x);
        if (anim.GetBool("run") && weapon != null)
        {
            storona = (int)Mathf.Sign(weapon.transform.position.x - transform.position.x);
            rb.velocity = new Vector2(speed * storona, rb.velocity.y);
        }
    }
    void apTr()
    {
        animKadr++;
        if (animKadr == 0)
        {
            sekira.GetComponent<Collider2D>().enabled = true;
            sekira.transform.localPosition = new Vector2(0, 0);
        }
        else if (animKadr == 1)
        {
            sekira.transform.localPosition = new Vector2(0.17f, 0.04f);
            sekira.transform.Rotate(new Vector3(0, 0, 130));
        }
        else if (animKadr == 2)
        {
            sekira.transform.localPosition = new Vector2(-0.08f, 0.18f);
            sekira.transform.Rotate(new Vector3(0, 0, -40));
        }
        else if (animKadr == 3)
        {
            rb.AddForce(new Vector2(90*storona, 50));
            sekira.transform.localPosition = new Vector2(0.137f, -0.062f);
            sekira.transform.Rotate(new Vector3(0, 0, -40));
            animKadr = -2;
        }
        else if (animKadr == -1)
        {
            damageinpl = false;
            sekira.transform.localPosition = new Vector2(0, 0);
            sekira.transform.Rotate(new Vector3(0, 0, -50));
            numat++;
        }
    }
    void met()
    {
        sekira.GetComponent<Collider2D>().enabled = false;
        Vector2 respawnPulia = new Vector2(storona * 0.35f + transform.position.x, transform.position.y);
        respGO(sek, respawnPulia, new Quaternion(0,0,0,0), "sek", 100, speed*8f);
        anim.SetBool("run", true);
    }
    void respGO(GameObject weap, Vector2 respawnPulia, Quaternion rotation, string name, int damage, float speed)
    {
        weapon = Instantiate(weap, respawnPulia, rotation);
        weapon.name = name;
        weapon.GetComponent<weapon>().hoziain = gameObject.name;
        weapon.GetComponent<weapon>().damage = damage;
        weapon.transform.localScale = new Vector3(storona * transform.localScale.x, transform.localScale.y, transform.localScale.z);
        if(name=="lightning") weapon.transform.localScale = new Vector3(-transform.localScale.x*(int)Mathf.Sign(speed), transform.localScale.y, transform.localScale.z);
        weapon.GetComponent<Rigidbody2D>().velocity =  speed*new Vector2(storona,0.2f);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject==weapon)
        {
            anim.SetBool("run", false);
            Destroy(weapon);
            endat();
            anim.SetBool("met", false);
        }
        if (anim.GetBool("run"))
        {
            if (collision.gameObject == player)
            {
                if ((int)Mathf.Sign(transform.position.x - weapon.transform.position.x) == (int)Mathf.Sign(transform.position.x - player.transform.position.x)) rb.AddForce(new Vector2(100, 120));
            }
            if (collision.gameObject.name == "patron")
            {
                hp -= 30;
                hpText.text = hp.ToString();
            }
        }
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
    IEnumerator wait(int variant, float time)
    {
        PlayerIsready = false;
        if (variant == 1) hp -= 15;
        else if (variant == 2) hp -= 130;
        hpText.text = hp.ToString();
        yield return new WaitForSeconds(time);
        PlayerIsready = true;
    }
}
