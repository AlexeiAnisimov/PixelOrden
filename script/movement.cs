using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Tilemaps;
using UnityEngine.Experimental.Rendering.Universal;

public class movement : MonoBehaviour
{
    public bool OnJump = true;
    public bool doubleJump = false;
    public bool vesh = false;
    public bool udar = false;
    public bool nachalo = false;
    bool dostupKArbaletu=true;
    bool dostupKDoubleJump=true;
    bool deathP = false;
    bool gotov = true;
    public bool click = false;
    bool arbal = false;
    public bool nanes = false;
    public int storona=1;
    float Jumpdop = 1f;
    int kol_voJump = 0;
    public float speedJump;
    public float speed = 1f;
    public float speedpul = 5f;
    public int hp = 100;
    public int streli=100;
    public float raznica = 0.08f;
    public Text hpText;
    public Text streliText;
    public string typeZemli;
    Vector3 nachpad;
    public Transform kulak;
    public Vector3 tempvector;
    public GameObject patron;
    public int damage=50;
    public int key = 0;
    // Start is called before the first frame update
    void Start()
    {
        if (Application.loadedLevelName == "level1")
        {
            //PlayerPrefs.SetInt("lvl1go", 0);
            GetComponent<Animator>().speed = GetComponent<Animator>().speed / 1.35f;
            speed = speed / 1.5f;
            speedJump = speedJump / 1.5f;
            if (PlayerPrefs.GetInt("lvl1go",0) == 1)
            {
                transform.position = new Vector2(GameObject.Find("tochkaOstanovki").transform.position.x, GameObject.Find("tochkaOstanovki").transform.position.y+0.4f);
            }
        }
        //nachalo = true;
        else if (Application.loadedLevelName == "level2")
        {
            dostupKArbaletu = false;
            InvokeRepeating("Delay", 3, 1f);
            nachalo = true;
        }
        else
        {
            dostupKArbaletu = true;
            nachalo = true;
        }
        if (Application.loadedLevelName == "level4" || Application.loadedLevelName == "level6") { GameObject.Find("light").GetComponent<Light2D>().enabled = true; nachalo = true; }
        streliText.text = streli.ToString();
        kulak = GameObject.Find("kulak").GetComponent<Transform>();
        //transform.position = new Vector3(transform.position.x, transform.position.y, 0);
    }

    void Delay()
    {
        if (hp > 0&&typeZemli=="pesok")
        {
            hp -= 3;
            hpText.text = hp.ToString();
        }
        else if(hp<=0)
        {
            CancelInvoke("Delay");
        }
    }
    // Update is called once per frame
    void Update()
    {
        // Debug.Log(gotovkvistrel);
        if (hp > 0)
        {
            GetComponent<Animator>().SetBool("hod", false);
            if (Input.GetButton("Horizontal"))
            {
                GetComponent<Animator>().SetBool("hod", true);
                Move();
                if (storona != Mathf.Sign(tempvector.x))
                {
                    Flip();
                }
            }
            if (Input.GetButtonDown("Jump"))
            {
                click = true;
                if (OnJump || (!OnJump && doubleJump))
                {
                    if (!OnJump && doubleJump) doubleJump = false;
                    Jump();
                }
            }
            if (nachalo)
            {
                if (Input.GetMouseButton(0) && streli > 0 && gotov && arbal)
                {
                    //GameObject.Find("kooldown").GetComponent<Text>().enabled = true;
                    StartCoroutine(Wait(0.5f));
                    vistrel();
                }
                if (Input.GetMouseButton(0) && gotov && !arbal)
                {
                    dwoechka();
                }
                if (Input.GetMouseButton(1) && gotov && !arbal)
                {
                    GameObject.Find("kooldown").GetComponent<Text>().enabled = true;
                    loktem();
                }
                if (Input.GetAxis("Mouse ScrollWheel") != 0 && dostupKArbaletu)
                {
                    if (Input.GetAxis("Mouse ScrollWheel") > 0) arbal = true;
                    if (Input.GetAxis("Mouse ScrollWheel") < 0) arbal = false;
                    GameObject.Find("mis").GetComponent<SpriteRenderer>().enabled = arbal;
                    GameObject.Find("АРБАЛЕТ").GetComponent<SpriteRenderer>().enabled = arbal;
                    GameObject.Find("fist").GetComponent<SpriteRenderer>().enabled = !arbal;
                    GameObject.Find("streliText").GetComponent<Text>().enabled = arbal;
                    GameObject.Find("imageOrushiia").GetComponent<SpriteRenderer>().enabled = arbal;
                }
            }
        }
        else if (!deathP)
        {
            hpText.text = "0";
            GetComponent<Animator>().SetBool("Death", true);
        }
    }
    void Move()
    {
        tempvector = Vector3.right * Input.GetAxis("Horizontal");
        transform.position = Vector3.MoveTowards(transform.position, transform.position + tempvector, speed * Time.deltaTime);
    }
    void Jump()
    {
        if (kol_voJump < 2)
        {
            GetComponent<Animator>().SetBool("jump", true);
            kol_voJump++;
            GetComponent<Rigidbody2D>().AddForce(Vector3.up * speedJump * Jumpdop);
            click = false;
        }
    }
    void Flip()
    {
        storona = (int)Mathf.Sign(tempvector.x);
        transform.localScale = new Vector2(-1 * transform.localScale.x, transform.localScale.y);
    }
    void dwoechka()
    {
        GetComponent<Animator>().SetBool("dwoechka", true);
        gotov = false;
    }
    void loktem()
    {

        GetComponent<Animator>().SetBool("lokot", true);
        GetComponent<Rigidbody2D>().AddForce(Vector3.up * speedJump * Jumpdop / 1.5f);
        gotov = false;
    }
    public void EndUdar()
    {
        udar = false;
        nanes = false;
    }
    public void Udar()
    {
        udar = true;
    }
    public void Allend()
    {
        EndUdar();
        if (GetComponent<Animator>().GetBool("lokot"))
        {
            GetComponent<Animator>().SetBool("lokot", false);
            StartCoroutine(Wait(1.5f));
        }
        else
        {
            GetComponent<Animator>().SetBool("dwoechka", false);
            gotov = true;
        }
    }
    void vistrel()
    {
        Vector2 cursor = GameObject.Find("aim").GetComponent<Transform>().position;
        Vector2 polet = cursor - (Vector2)transform.position;
        if (storona != Mathf.Sign(polet.x))
        {
            //Debug.Log(Mathf.Tan(polet.y / polet.x));
            storona = -storona;
            transform.localScale = new Vector2(-1 * transform.localScale.x, transform.localScale.y);
        }
        Quaternion rotation = Quaternion.Euler(0, 0, Mathf.Atan2(polet.y, polet.x) * Mathf.Rad2Deg);
        GameObject patrVistrel = Instantiate(patron, GameObject.Find("АРБАЛЕТ").GetComponent<Transform>().position, rotation);
        patrVistrel.GetComponent<Rigidbody2D>().velocity = polet.normalized*speedpul;
        patrVistrel.GetComponent<patron>().hoziain = gameObject.name;
        streli--;
        streliText.text = streli.ToString();
    }
    void Death()
    {
        deathP = true;
        GetComponent<Animator>().SetBool("Death",false);
        if (Application.loadedLevelName == "level1")
        {
            StartCoroutine(die(GameObject.Find("Global Light 2D").GetComponent<Light2D>()));
            StartCoroutine(cvetmen(GameObject.Find("Tilemap").GetComponent<Tilemap>()));
        }
        else if (Application.loadedLevelName == "level2")
        {
            if (typeZemli == "pesok") StartCoroutine(die(GameObject.Find("sun").GetComponent<Light2D>()));
            else StartCoroutine(die(GameObject.Find("pesheraLight").GetComponent<Light2D>()));
        }
        else
        {
            StartCoroutine(die(GameObject.Find("Global Light 2D").GetComponent<Light2D>()));
        }
        GameObject.Find("DieMessage").GetComponent<Text>().text = "Вы умерли.Чтобы начать снова, нажмите <<переиграть>>";
        //Destroy(gameObject);
    }
    public void exitEarth()
    {
        GetComponent<Animator>().SetBool("jump", true);
        nachpad = new Vector3(0, transform.position.y, 0);
        OnJump = false;
        StartCoroutine(kek());
    }
    public void padenie()
    {
        if (nachpad != null && nachpad.y - transform.position.y > 1.75f)
        {
            hp = (int)(hp - ((nachpad.y - transform.position.y) * 100 - 175) / 3);
            nachpad = transform.position;
        }
    }
    public void naEarth()
    {
        OnJump = true;
        doubleJump = false;
        Jumpdop = 1;
        kol_voJump = 0;
        click = false;
        GetComponent<Animator>().SetBool("jump", false);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "patron" && collision.gameObject.GetComponent<patron>().hoziain != gameObject.name) hp -= 25; 
        else if (collision.gameObject.name == "patron" && collision.gameObject.GetComponent<patron>().hoziain == "РОЗБiЙНИК") hp -= 30;
        if (collision.collider.tag == "Dangerous") hp -= 50;
        hpText.text = hp.ToString();
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (Application.loadedLevelName == "level2")
        {
            if (collision.gameObject.tag != typeZemli)
            {
                if (collision.gameObject.tag == "pesok")
                {
                    GetComponent<respawnGhost>().enabled = true;
                    GetComponent<Animator>().speed = GetComponent<Animator>().speed / 1.35f;
                    GameObject.Find("pesheraLight").GetComponent<Light2D>().enabled = false;
                    GameObject.Find("sun").GetComponent<Light2D>().enabled = true;
                    GameObject.Find("light").GetComponent<Light2D>().enabled = false;

                    speed = speed / 1.4f;
                    speedJump = speedJump / 1.3f;
                    dostupKDoubleJump = false;
                    dostupKArbaletu = false;
                }
                else if (collision.gameObject.tag == "peshera")
                {
                    GetComponent<respawnGhost>().enabled = false;
                    GetComponent<Animator>().speed = GetComponent<Animator>().speed * 1.35f;
                    GameObject.Find("sun").GetComponent<Light2D>().enabled = false;
                    GameObject.Find("pesheraLight").GetComponent<Light2D>().enabled = true;
                    GameObject.Find("light").GetComponent<Light2D>().enabled = true;
                    speed = speed * 1.4f;
                    speedJump = speedJump * 1.3f;
                    dostupKDoubleJump = true;
                    dostupKArbaletu = true;
                }
            }
            if (collision.gameObject.tag != "Untagged" && collision.gameObject.tag != "Dangerous")
                typeZemli = collision.gameObject.tag;
            if (!OnJump && !doubleJump) GetComponent<Rigidbody2D>().AddForce(new Vector2(-storona * 0.1f, 0));
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (Application.loadedLevelName == "level4")typeZemli = collision.gameObject.tag;
    }
    IEnumerator kek()
    {
        yield return new WaitForSeconds(0.2f);
        Jumpdop = 0.7f;
        if (dostupKDoubleJump && OnJump == false)
            doubleJump = true;
        if (click&&dostupKDoubleJump&&!OnJump)
        {
            doubleJump = false;
            Jump();
        }
    }
    IEnumerator Wait(float time)
    {
        gotov = false;
        yield return new WaitForSeconds(time);
        gotov = true;
        GameObject.Find("kooldown").GetComponent<Text>().enabled = false;
    }
    IEnumerator die(Light2D light)
    {
        if (light.intensity > 0)
        {
            yield return new WaitForSeconds(0.05f);
            light.intensity -= 0.01f;
            StartCoroutine(die(light));
        }
        else Destroy(gameObject);
    }
    IEnumerator cvetmen(Tilemap kek)
    {
        if (kek.color.r+kek.color.g+kek.color.b!= 0)
        {
            yield return new WaitForSeconds(0.05f);
            kek.color = new Color(kek.color.g-0.01f, kek.color.r - 0.01f, kek.color.b - 0.01f);
            StartCoroutine(cvetmen(kek));
        }
    }
}
