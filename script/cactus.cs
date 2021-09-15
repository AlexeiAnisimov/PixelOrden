using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class cactus : MonoBehaviour
{
    public float nachspeed;
    public int hp=200;
    float speed;
    float oldSt;
    int storona=1;
    bool ready = true;
    bool ata = false;
    bool havedamage = false;
    bool ju=true;
    GameObject player;
    Rigidbody2D rb;
    Animator anim;
    Text hpText;
    Vector3 tempvector;
    private Vector3 m_Velocity = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        hpText = GetComponentInChildren<Canvas>().GetComponentInChildren<Text>();
        tempvector = Vector3.right * storona;
    }

    void FixedUpdate()
    {
        Move();
        hpText.transform.localScale = new Vector2(storona,1);
        storona = -(int)Mathf.Sign(transform.position.x - player.transform.position.x);
        transform.localScale = new Vector2(storona, transform.localScale.y);
    }
    // Update is called once per frame
    void Update()
    {
        if (hp > 0)
        {
            if (!ata)
            {
                if (Mathf.Abs(transform.position.x - player.transform.position.x) < 3f && Mathf.Abs(transform.position.y - player.transform.position.y) < 0.5f && ready)
                {
                    anim.SetBool("run", true);
                    speed = nachspeed;
                }
                if (Vector2.Distance(player.transform.position, transform.position) < 1f && ready)
                {
                    anim.SetBool("attack", true);
                }
            }
            else speed = 2.4f * nachspeed;
        }
        else anim.SetBool("death", true);
    }
    void Move()
    {
        if(!anim.GetBool("attack"))
            tempvector = new Vector3(storona,0,0);
        transform.position = Vector3.MoveTowards(transform.position, transform.position + tempvector, speed * Time.deltaTime);
    }
    void Death()
    {
        Destroy(gameObject);
    }
    void atack()
    {
        ready = false;
        ata = true;
    }
    void corwait()
    {
        StartCoroutine(wait());
    }
    IEnumerator wait()
    {
        speed = 0;
        anim.SetBool("attack", false);
        ata=false;
        yield return new WaitForSeconds(2f);
        havedamage = false;
        ready = true;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "patron" && collision.gameObject.GetComponent<patron>().hoziain != gameObject.name)
        {
            hp -= GameObject.Find(collision.gameObject.GetComponent<patron>().hoziain).GetComponent<movement>().damage/2;
            hpText.text = hp.ToString();
        }
        if (collision.gameObject.name == "Player") {
            if (anim.GetBool("attack"))
            {
                collision.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(storona * 2, 2));
                if (!havedamage)
                {
                    havedamage = true;
                    collision.gameObject.GetComponent<movement>().hp -= 40;
                }
                player.GetComponent<movement>().hpText.text = player.GetComponent<movement>().hp.ToString();
            }
            else
            {
                collision.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(storona * 0.8f, 0.1f));
            }
        }
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        ContactPoint2D[] contact=new ContactPoint2D[10];
        if (GetComponent<EdgeCollider2D>().GetContacts(contact)!=0) ju = false;
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        ju = true;
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.tag=="peshera"&& !anim.GetBool("attack")&&ju) GetComponent<Rigidbody2D>().AddForce(new Vector2(storona * 5, 50));
    }
}
