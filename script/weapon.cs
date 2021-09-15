using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.Experimental.Rendering.Universal;
public class weapon : MonoBehaviour
{
    public string hoziain;
    public int damage;
    int storona = 1;
    public bool damageInPlayer=false;
    bool endBorn = false;
    int zn = 1;
    // Start is called before the first frame update
    private void Start()
    {
        if (gameObject.name == "rainbow") StartCoroutine(ra());
    }
    void Update()
    {
        if(gameObject.name=="siruken")transform.Rotate(new Vector3(0, 0, 20));
        if(gameObject.name=="flower"&&Vector2.Distance(GameObject.Find("Player").transform.position,transform.position)<0.3f&&endBorn) GetComponent<Animator>().SetBool("BAM", true);
    }
    private void FixedUpdate()
    {
        if (gameObject.name == "rainbow")
        {
            transform.localScale = new Vector2(transform.localScale.x + 0.1f*zn*storona, transform.localScale.y);
            transform.position = new Vector2(transform.position.x + 0.1f / 5.372f*storona, transform.position.y);
            if (transform.localScale.x < 0.1f) Death();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (gameObject.name == "rocket" && collision.gameObject.name == "Player" && !GetComponent<Animator>().enabled && !damageInPlayer)
        {
            collision.gameObject.GetComponent<movement>().hp -= damage;
            collision.gameObject.GetComponent<movement>().hpText.text = collision.gameObject.GetComponent<movement>().hp.ToString();
            damageInPlayer = true;
        }
        if (collision.gameObject.name != hoziain|| collision.gameObject.name != gameObject.name)
        {
            if (gameObject.name == "fireweerk")
            {
                GetComponent<Animator>().SetBool("Death", true);
                GetComponent<BoxCollider2D>().enabled = false;
            }
            else if (gameObject.tag == "prob")
            {
                GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
                GetComponent<Animator>().enabled = true;
                GetComponent<BoxCollider2D>().offset = new Vector2(0.012f, 0.004f);
                GetComponent<BoxCollider2D>().size = new Vector2(0.27f, 0.058f);
                GetComponent<BoxCollider2D>().isTrigger = true;
            }
            else if (gameObject.name == "sek")
            {
                GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
                GetComponent<Rigidbody2D>().freezeRotation = true;
                GetComponent<weapon>().enabled = false;
            }
            else if (gameObject.name == "flower") { }
            else if(gameObject.name == "rocket") GetComponent<Animator>().enabled = true;
            else Death();
        }
        if (collision.gameObject.name == "Player"&&gameObject.name!="flower"&&gameObject.name!="rocket")
        {
            collision.gameObject.GetComponent<movement>().hp -= damage;
            collision.gameObject.GetComponent<movement>().hpText.text = collision.gameObject.GetComponent<movement>().hp.ToString();
            if (gameObject.name == "sek") damage = 1;
        }
        else { if (gameObject.name == "sek") damage = 1; }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (gameObject.tag == "prob")
        {
            if (collision.GetComponent<TilemapCollider2D>() != null)
            {
                GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
            }
            if (collision.gameObject.name == "Player") {
                if (gameObject.name == "prob1" && !damageInPlayer)
                {
                    GameObject.Find("АЛХIМIК").GetComponent<alchimick>().c();
                    damageInPlayer = true;
                }
                else if (gameObject.name == "prob2" && !damageInPlayer)
                {
                    StartCoroutine(wait(0.15f));
                    collision.gameObject.GetComponent<movement>().hp -= 20;
                    collision.gameObject.GetComponent<movement>().hpText.text = collision.gameObject.GetComponent<movement>().hp.ToString();
                }
                else if (gameObject.name == "prob3" && !damageInPlayer)
                {
                    damageInPlayer = true;
                    GameObject.Find("АЛХIМIК").GetComponent<alchimick>().p();
                }
            }
        }
        if ((gameObject.name == "rainbow"|| gameObject.name == "rocket"||(gameObject.name=="vulkan"&&endBorn)) && !damageInPlayer && collision.gameObject.name == "Player")
        {
            collision.gameObject.GetComponent<movement>().hp -= damage;
            collision.gameObject.GetComponent<movement>().hpText.text = collision.gameObject.GetComponent<movement>().hp.ToString();
            if(gameObject.name == "rainbow"||(gameObject.name=="vulkan"&&endBorn)) StartCoroutine(wait(0.15f));
            if (gameObject.name == "rocket") damageInPlayer = true;
        }
        if ((gameObject.name == "rocket"||gameObject.name=="vulkan") && collision.GetComponent<TilemapCollider2D>() != null) GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePosition;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (gameObject.name == "flower"&&collision.name=="Player")
        {
            collision.gameObject.GetComponent<movement>().hp -= damage;
            collision.gameObject.GetComponent<movement>().hpText.text = collision.gameObject.GetComponent<movement>().hp.ToString();
            GetComponent<BoxCollider2D>().enabled = false;
        }
        if ((gameObject.name == "cloud"||gameObject.name=="lightning") && collision.name == "Player"&&!damageInPlayer)
        {
            collision.gameObject.GetComponent<movement>().hp -= damage;
            collision.gameObject.GetComponent<movement>().hpText.text = collision.gameObject.GetComponent<movement>().hp.ToString();
            damageInPlayer = true;
        }
    }
    void BAM()
    {
        GetComponent<BoxCollider2D>().enabled = true;
        GetComponent<EdgeCollider2D>().enabled = false;
        GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionY;
    }
    void cloud_sost1()
    {
        if (GetComponent<BoxCollider2D>().enabled)
        {
            GetComponent<BoxCollider2D>().offset = new Vector2(0, 0f);
            GetComponent<BoxCollider2D>().size = new Vector2(0.4f, 0.15f);
            damageInPlayer = false;
        }
        GetComponent<BoxCollider2D>().enabled = !GetComponent<BoxCollider2D>().enabled;
    }
    void cloud_sost2()
    {
        //damage =damage* 5;
        GetComponent<BoxCollider2D>().offset = new Vector2(0, -0.09f);
        GetComponent<BoxCollider2D>().size = new Vector2(0.4f, 0.33f);
    }
    void cloud_sost3()
    {
        GetComponent<BoxCollider2D>().offset = new Vector2(0, -0.17f);
        GetComponent<BoxCollider2D>().size = new Vector2(0.4f, 0.48f);
    }
    void lightning_sost1()
    {
        GetComponent<CapsuleCollider2D>().offset = new Vector2(0, 0);
        GetComponent<CapsuleCollider2D>().size = new Vector2(0.5f, 0.23f);
    }
    void lightning_sost2()
    {
        GetComponent<CapsuleCollider2D>().offset = new Vector2(0, 0.11f);
        GetComponent<CapsuleCollider2D>().size = new Vector2(0.44f, 0.44f);
    }
    void rocket_sost1()
    {
        GetComponent<BoxCollider2D>().offset = new Vector2(0, 0.1f);
        GetComponent<BoxCollider2D>().size = new Vector2(0.21f, 0.2f);
    }
    void rocket_sost2()
    {
        GetComponent<BoxCollider2D>().offset = new Vector2(0.01f, 0.23f);
        GetComponent<BoxCollider2D>().size = new Vector2(0.52f, 0.46f);
        GetComponent<BoxCollider2D>().isTrigger = true;
        damageInPlayer = false;
    }
    void vulkan_sost1()
    {
        if (!endBorn)
        {
            endBorn = true;
            GetComponent<Light2D>().enabled = true;
        }
        GetComponent<BoxCollider2D>().offset = new Vector2(0, -0.02f);
        GetComponent<BoxCollider2D>().size = new Vector2(0.26f, 0.27f);
    }
    void vulkan_sost2()
    {
        GetComponent<BoxCollider2D>().offset = new Vector2(0, -0.06f);
        GetComponent<BoxCollider2D>().size = new Vector2(0.26f, 0.2f);
    }
    void vulkan_end()
    {
        endBorn = false;
        GetComponent<Light2D>().enabled = false;
    }
    void endFlower() { endBorn = true; }
    void Death()
    {
        Destroy(gameObject);
    }
    IEnumerator w()
    {
        yield return new WaitForSeconds(0.08f);
        GetComponent<BoxCollider2D>().enabled = false;
    }
    IEnumerator wait(float time)
    {
        damageInPlayer = true;
        yield return new WaitForSeconds(time);
        damageInPlayer = false;
    }
    IEnumerator ra()
    {
        storona = GameObject.Find("АЛХIМIК").GetComponent<alchimick>().storona;
        zn = storona;
        yield return new WaitForSeconds(2);
        zn = -storona;
    }
}
