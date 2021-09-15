using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class rozbinikBOSS : MonoBehaviour
{
    GameObject pla;
    public GameObject patron;
    public GameObject key;
    int storona=1;
    float speed;
    Vector2 polet;
    Vector2 respawnPulia;
    Vector2 luk;
    bool nanes = false;
    public bool enabl = false;
    bool PlayerIsready = true;
    bool ochered_udarov=false;
    float speedHodibi = 0.7f;
    float raznica = 0.375f;
    float speedJump = 70;
    public int hp = 3000;
    int kol_vo=0;
    Text hpText;
    Text nameText;
    bool coroutinstart = false;
    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, 0);
        gameObject.name = "РОЗБiЙНИК";
        pla = GameObject.Find("Player");
        hpText = GameObject.Find("BossHP").GetComponent<Text>();
        nameText = GameObject.Find("BossName").GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!enabl)
        {
            if (Vector2.Distance(transform.position, pla.transform.position) < 4f&&Mathf.Abs(transform.position.y-pla.transform.position.y)<1)
            {
                nameText.text = gameObject.name;
                hpText.text = hp.ToString();
                enabl = true;
                GameObject.Find("dwerKBoss").GetComponent<BoxCollider2D>().enabled = true;
            }
            else
            {
                hpText.text = "";
                nameText.text = "";
            }
        }
        else
        {
            if (hp > 2000) firstStadia();
            else if (hp > 500 && hp <= 2000) SecondStadia();
            else if (hp < 500&&hp>0)
            {
                GetComponent<Animator>().SetBool("Streliat", false);
                GetComponent<Animator>().SetBool("First", false);
                ThierdStadia();
            }
            else if (hp <= 0)
            {
                hp = 0;
                //GetComponent<Collider2D>().enabled = false;
                GetComponent<Animator>().SetBool("Death", true);
            }
        }
        if ((transform.position.x - pla.transform.position.x >= raznica && storona == 1) || (storona == -1 && transform.position.x - pla.transform.position.x <= -raznica))
        {
            //Debug.Log(Mathf.Tan(polet.y / polet.x));
            storona = -storona;
            transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
            //hpText.rectTransform.localScale = new Vector2(-hpText.rectTransform.localScale.x, hpText.rectTransform.localScale.y);
        }
    }
    public void Death()
    {
        key.GetComponent<veshi>().value = 5;
        GameObject patrVistrel = Instantiate(key, new Vector2(transform.position.x, pla.transform.position.y), Quaternion.identity);
        key.GetComponent<veshi>().value = 6;
        GameObject patrVistrel1 = Instantiate(key, new Vector2(transform.position.x+0.15f, pla.transform.position.y), Quaternion.identity);
        GameObject.Find("dwerKBoss").GetComponent<BoxCollider2D>().enabled = false;
        nameText.text="";
        hpText.text = "";
        Destroy(gameObject);
    }
    void firstStadia()
    {
        GetComponent<Animator>().SetBool("Second", false);
        GetComponent<Animator>().speed = 1f;
        if (kol_vo != 1)
        {
            if (!ochered_udarov) Streliba();
            else hodiba();
        }
        else if(!coroutinstart)
        {
            StartCoroutine(reload(1f));
        }
    }
    void SecondStadia()
    {
        GetComponent<Animator>().SetBool("Second", false);
        GetComponent<Animator>().speed = 1.2f;
        if (kol_vo != 3)
        {
            if (!ochered_udarov) Streliba();
            else hodiba();
        }
        else if (!coroutinstart)
        {
            StartCoroutine(reload(2f));
        }
    }
    void ThierdStadia()
    {
        Debug.Log(kol_vo);
        GetComponent<Animator>().speed = 1.5f;
        if (kol_vo != 6)
        {
            GetComponent<Animator>().SetBool("Second", true);
        }
        else if (!coroutinstart)
        {
            GetComponent<Animator>().SetBool("Second", false);
            StartCoroutine(reload(2f));
        }
    }
    void hodiba()
    {
        luk = new Vector2(transform.position.x + storona * raznica, transform.position.y);
        Vector3 tempvector = Vector3.right * storona;
        transform.position = Vector3.MoveTowards(transform.position, transform.position + tempvector, speedHodibi * Time.deltaTime);
        if (Mathf.Abs(luk.x - pla.transform.position.x) <= 1f)
        {
            GetComponent<Animator>().SetBool("First", true);
        }
        else speedHodibi = 0.7f;
    }
    void Streliba()
    {
        Vector2 cursor = new Vector2(pla.transform.position.x, pla.transform.position.y + 0.2f);
        polet = cursor - (Vector2)transform.position;
        speed = 3f * Vector2.Distance(cursor, transform.position)+1;
        GetComponent<Animator>().SetBool("Streliat", true);
    }
    public void First()
    {
        if (!nanes)
        {
            if (Mathf.Abs(luk.x - pla.transform.position.x) <= raznica && storona == Mathf.Sign(luk.x - pla.transform.position.x) && Mathf.Abs(transform.position.y-0.2f - pla.transform.position.y) < raznica)
            {
                //Debug.Log(luk.position.x - 0.15f * storona - pla.transform.position.x);
                pla.GetComponent<movement>().hp -= 50;
                pla.GetComponent<movement>().hpText.text = pla.GetComponent<movement>().hp.ToString();
                nanes = true;
            }
        }
    }
    public void Second()
    {
        luk = new Vector2(transform.position.x + storona * raznica, transform.position.y);
        if (!nanes)
        {
            if (Mathf.Abs(luk.x - pla.transform.position.x) <= raznica && storona == Mathf.Sign(luk.x - pla.transform.position.x) &&  Mathf.Abs(transform.position.y-0.1f - pla.transform.position.y) < raznica)
            {
                //Debug.Log(luk.position.x - 0.15f * storona - pla.transform.position.x);
                pla.GetComponent<movement>().hp -= 70;
                pla.GetComponent<movement>().hpText.text = pla.GetComponent<movement>().hp.ToString();
                hp += 30;
                hpText.text = hp.ToString();
                nanes = true;
            }
        }
    }

    public void EndSecond()
    {
        kol_vo++;
        nanes = false;
    }

    public void OnJump()
    {
        speedJump = (1 + (pla.transform.position.y - transform.position.y)) / 0.008f;
        GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 20));
        GetComponent<Rigidbody2D>().AddForce(new Vector2(storona * 125, Vector3.up.y * speedJump));
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "patron" && collision.gameObject.GetComponent<patron>().hoziain != gameObject.name)
        {
            hp -= GameObject.Find(collision.gameObject.GetComponent<patron>().hoziain).GetComponent<movement>().damage;
        }
        hpText.text = hp.ToString();
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.name == "kulak" && pla.GetComponent<Animator>().GetBool("dwoechka")&&PlayerIsready)
        {
            StartCoroutine(wait(1,0.2f));
        }
        else if(collision.gameObject.name == "lokot" && pla.GetComponent<Animator>().GetBool("lokot") && PlayerIsready)
        {
            StartCoroutine(wait(2,1.5f));
        }
    }
    IEnumerator wait(int variant,float time)
    {
        PlayerIsready = false;
        if (variant == 1)hp -= 15;
        else if (variant == 2) hp -= 130;
        hpText.text = hp.ToString();
        yield return new WaitForSeconds(time);
        PlayerIsready = true;
    }
    void OnVistrel()
    {

        for (int i = 0; i < 3; i++)
        {
            generatePatron(i,false);
        }
    }
    void OnVistrel2()
    {
        generatePatron(0,true);
    }
    IEnumerator reload(float time)
    {
        if (ochered_udarov) GetComponent<Animator>().SetBool("First", false);
        else GetComponent<Animator>().SetBool("Streliat", false);
        coroutinstart = true;
        yield return new WaitForSeconds(time);
        kol_vo = 0;
        ochered_udarov = !ochered_udarov;
        coroutinstart = false;
    }
    void generatePatron(int i,bool storoni)
    {
        respawnPulia = new Vector2(storona * 0.1f + transform.position.x, transform.position.y + 0.1f * (i - 1));
        Quaternion rotation = Quaternion.Euler(0, 0, Mathf.Atan2(polet.y, polet.x + 0.2f * (i - 1)) * Mathf.Rad2Deg);
        if(storoni) rotation = Quaternion.Euler(0, 0, storona*80+90);
        GameObject patrVistrel = Instantiate(patron, respawnPulia, rotation);
        if(!storoni)patrVistrel.GetComponent<Rigidbody2D>().velocity = polet.normalized * speed;
        else patrVistrel.GetComponent<Rigidbody2D>().velocity = new Vector2(storona*10,0);
        patrVistrel.GetComponent<patron>().hoziain = gameObject.name;
    }
}
