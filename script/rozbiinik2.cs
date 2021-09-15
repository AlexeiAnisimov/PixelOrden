using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class rozbiinik2 : MonoBehaviour
{
    GameObject pla;
    Vector2 luk;
    int storona=1;
    bool nanes = false;
    bool enabl = false;
    bool onj = false;
    public float speed=0.5f;
    float raznica = 0.15f;
    float speedJump = 70;
    public int hp = 200;
    Text hpText;
    RaycastHit2D l;
    RaycastHit2D r;
    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, 0);
        gameObject.name = "rozbinik";
        hpText = GetComponentInChildren<Canvas>().GetComponentInChildren<Text>();
        pla = GameObject.Find("Player");
    }
    private void FixedUpdate()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
        if (!enabl)
        {
            if (Mathf.Abs(transform.position.x - pla.transform.position.x) < 4f && Mathf.Abs(transform.position.y - pla.transform.position.y) < 0.5f)
            {
                enabl = true;
                //enabl = true;
            }
        }
        else
        {
            hodiba();
        }
        if (!pla.GetComponent<movement>().nanes && pla.GetComponent<movement>().udar)
            polucheniaDamage();
        if (hp <= 0)
        {
            GetComponent<Rigidbody2D>().gravityScale = 0;
            GetComponent<Collider2D>().enabled = false;
            GetComponent<Animator>().SetBool("Death", true);
        }
        l = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y - 0.135f), Vector2.left, 0.4f);
        r = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y - 0.135f), Vector2.right, 0.4f);
        if (l.collider != null && r.collider != null)
        {
            if ((l.collider.name == "zemlia" || r.collider.name == "zemlia") && !onj&& !GetComponent<Animator>().GetBool("bit"))
            {
                GetComponent<Rigidbody2D>().AddForce(new Vector2(35 * storona, Vector3.up.y * 150));
                onj = true;
            }
        }
        l = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y - 0.135f), Vector2.left, 0.01f);
        r = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y - 0.135f), Vector2.right, 0.01f);
        if (l.collider != null && r.collider != null)
            if ((l.collider.name == "zemlia" || r.collider.name == "zemlia") && onj)
            {
                GetComponent<Rigidbody2D>().AddForce(new Vector2(10 * -storona, Vector3.up.y * 1));
            }
        //else if(onj&&)
    }

    public void Death()
    {
        Destroy(gameObject);
    }
    public void hodiba()
    {
        luk = new Vector2(transform.position.x + storona * raznica, transform.position.y);
        if (Mathf.Abs(luk.x - pla.transform.position.x) >= 0.3f && Vector2.Distance(transform.position, pla.transform.position) < 4f)
        {
            Vector3 tempvector = Vector3.right * storona;
            transform.position = Vector3.MoveTowards(transform.position, transform.position + tempvector, speed * Time.deltaTime);
            GetComponent<Animator>().SetBool("bit", false);
        }
        else if (Mathf.Abs(luk.x - pla.transform.position.x) < 0.3f && transform.position.y - pla.transform.position.y < 1.3f) GetComponent<Animator>().SetBool("bit", true);
        if ((transform.position.x - pla.transform.position.x >= 0.15f && storona == 1) || (storona == -1 && transform.position.x - pla.transform.position.x <= -0.15f))
        {
            //Debug.Log(Mathf.Tan(polet.y / polet.x));
            storona = -storona;
            transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
            hpText.rectTransform.localScale = new Vector2(-hpText.rectTransform.localScale.x, hpText.rectTransform.localScale.y);
        }
    }


    public void firstUdar()
    {
        if (!nanes)
        {
            if (Mathf.Abs(luk.x - pla.transform.position.x) <= raznica && storona == Mathf.Sign(luk.x - pla.transform.position.x) && Mathf.Abs(luk.y - pla.transform.position.y) < 0.1f)
            {
                //Debug.Log(luk.position.x - 0.15f * storona - pla.transform.position.x);
                pla.GetComponent<movement>().hp -= 30;
                pla.GetComponent<movement>().hpText.text = pla.GetComponent<movement>().hp.ToString();
                nanes = true;
            }
        }
    }

    public void EndFirstUdar()
    {
        GetComponent<Animator>().SetBool("bit", false);
        nanes = false;
    }

    public void OnJump()
    {
        if (Mathf.Abs(transform.position.y - pla.transform.position.y) < 0.15f) speedJump = 70;
        else if(pla.transform.position.y - transform.position.y>0) speedJump=(1+(pla.transform.position.y-transform.position.y))/0.009f;
        if (speedJump > 230) speedJump = 230;
        GetComponent<Rigidbody2D>().AddForce(new Vector2(storona*55,Vector3.up.y * speedJump));
    }
    void polucheniaDamage()
    {
        if (pla.GetComponent<Animator>().GetBool("dwoechka"))
        {
            if (Vector2.Distance(pla.GetComponent<movement>().kulak.position, transform.position) <= pla.GetComponent<movement>().raznica+0.01f)
            {
                pla.GetComponent<movement>().nanes = true;
                hp -= pla.GetComponent<movement>().damage / 2;
            }
        }
        else if (pla.GetComponent<Animator>().GetBool("lokot"))
        {
            if (Vector2.Distance(pla.GetComponent<movement>().kulak.position, transform.position) <= (pla.GetComponent<movement>().raznica+0.04f) / 2)
            {
                pla.GetComponent<movement>().nanes = true;
                hp -= pla.GetComponent<movement>().damage * 2;
                GetComponent<Rigidbody2D>().AddForce(new Vector2(pla.GetComponent<movement>().storona * 45, Vector3.up.y * 150));
            }
        }
        hpText.text = hp.ToString();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "zemlia")
        {
            onj = false;
        }
        if (collision.gameObject.name == "patron" && collision.gameObject.GetComponent<patron>().hoziain != gameObject.name)
        {
            hp -= GameObject.Find(collision.gameObject.GetComponent<patron>().hoziain).GetComponent<movement>().damage;
        }
        hpText.text = hp.ToString();

    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.name == "zemlia") onj = true;
    }
}
