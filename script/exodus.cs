using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class exodus : MonoBehaviour
{
    int podniatii = 1;int num=0;
    bool trig = false;
    bool trigend = false;
    float speed = 0.1f;
    bool nachalo = false;
    public float distance = 1.7f;
    bool ster = false;
    int lvlnum;
    Text dialog;
    GameObject pla;
    string[] text;
    int[] govoriashii;
    // Start is called before the first frame update
    void Start()
    {
        dialog = GameObject.Find("dialog").GetComponent<Text>();
        pla = GameObject.Find("Player");
        text = new string[10];
        if (Application.loadedLevelName == "level1")
        {
            govoriashii = new int[10];
            for (int i = 0; i < govoriashii.Length; i++)
            {
                govoriashii[i] = i % 2;
            }
            lvlnum = 9;
            text[0] = "К: Здраствуйте, вы заказывали пиццу?";
            text[1] = "E: Да, конечно я. Ты в этой комнате видишь еще кого-то?";
            text[2] = "К: Нет. Меня поразил ваш район. Как вы вообще здесь живете?";
            text[3] = "E: Со временем привыкаешь к необычной компании людей...";
            text[4] = "К: ААА, понимаю. Подпишитесь внизу о том,что получили заказ";
            text[5] = "Е: Обязательно, но вначале подпишись в моем документе,что ты здесь был";
            text[6] = "Здесь будет выбор варианта ответа(подписаться или нет)(пока не сделал)";
            text[7] = "E: ААХХАХАХАХАХХАХАХАХХ, ТЕПЕРЬ ТЫ МНЕ БУДЕШЬ СЛУЖИТЬ ДЛЯ МОИХ ЦЕЛЕЙ!!!!!";
            text[8] = "К: да,ааа,понятно. Ну,видимо выбора у меня нету. И что же мне делать???";
            text[9] = "E: ТЕБЕ НУЖНО ЗАЙТИ В ЭТУ ДВЕРЬ И УЖЕ ТАМ ТЫ УЗНАЕШЬ ЗАДАНИЕ.";
        }
        if(Application.loadedLevelName == "level2")
        {
            govoriashii = new int[10];
            for (int i = 0; i < govoriashii.Length; i++)
            {
                if ((i + 1) % 3 == 0) govoriashii[i] = 0;
                else govoriashii[i] = 1;
            }
            lvlnum = 8;
            text[0] = "E: О,да ты молодец,достал нужный артефакт";
            text[1] = "E: Тебе нужно будет еще их достать и прийти в определенное место в определенное время";
            text[2] = "К: А куда и когда? И что вообще доставать??";
            text[3] = "E: Такие же артефакты в разных временных эпохах";
            text[4] = "E: Если ты не понял,вот эта дверь является порталом во времени";
            text[5] = "К: А куда дальше то?";
            text[6] = "E: Сам выбирай. Однако мне нужно,чтобы ты достал все артефакты и решай сам как их доставать";
            text[7] = "E: Встретимся еще потом. Доставай и доставляй";
        }
        if (Application.loadedLevelName == "level3")
        {
            if (gameObject.name == "ТЕНИ ТСОРИИ")
            {
                govoriashii = new int[4];
                for (int i = 0; i < govoriashii.Length; i++)
                {
                    govoriashii[i] = 1;
                }
                text[0] = "спасибо,что освободил меня от демона";
                text[1] = "без тебя с Тсорией могло случиться что угодно";
                text[2] = "теперь я могу спать спокойно";
                lvlnum = 3;
            }
            else
            {
                govoriashii = new int[5];
                for (int i = 0; i < govoriashii.Length; i++)
                {
                    govoriashii[i] = i%2;
                }
                text[0] = "К: что ты делаешь здесь?";
                text[1] = "Р: слежу за тобой,чтобы выполнял миссию EXODUS";
                text[2] = "К: понятно,а где EXODUS?";
                text[3] = "Р: не твое дело, книгу достань!";
                lvlnum = 4;
            }
        }
        if (Application.loadedLevelName == "level4")
        {
            if (gameObject.name == "deva")
            {
                govoriashii = new int[4];
                for (int i = 0; i < govoriashii.Length; i++)
                {
                    govoriashii[i] = 1;
                }
                text[0] = "Здравствуй, путник, ты пришел слишком поздно";
                text[1] = "Я дева, которая была женой великого Короля. Черный рыцарь меня заточил здесь";
                text[2] = "Уже приходили воевать с ним, но никто не смог его одолеть.";
                lvlnum = 3;
            }
            else if (gameObject.name == "alchimick")
            {
                govoriashii = new int[5];
                for (int i = 0; i < govoriashii.Length; i++)
                {
                    govoriashii[i] = 1;
                    if(i==2) govoriashii[i] = 0;
                }
                text[0] = "Чего ты забыл в этом прекрасном замке?";
                text[1] = "Я тебя не пропущу дальше. Иди своей дорогой.";
                text[2] = "Не пойду";
                text[3] = "Ну тогда готовься к сражению со мной!";
                lvlnum = 4;
            }
            else if(gameObject.name == "knightBOSS")
            {
                govoriashii = new int[4];
                for (int i = 0; i < govoriashii.Length; i++)
                {
                    govoriashii[i] = 1;
                    if (i == 1) govoriashii[i] = 0;
                }
                text[0] = "Зря ты пришел ко мне, путник";
                text[1] = "Мне нужен артефакт, который ты получил у EXODUS";
                text[2] = "Не могу пойти на сделку.Я уже устал убивать вас";
                lvlnum = 3;
            }
        }
        if(Application.loadedLevelName == "level5")
        {
            govoriashii = new int[5];
            for (int i = 0; i < govoriashii.Length; i++)
            {
                govoriashii[i] = 1;
                if (i == 2) govoriashii[i] = 0;
            }
            text[0] = "Ты пришел ко мне от моего прекрасного друга EXODUS?";
            text[1] = "Рад встречи. Отдай пожалуйста артефакты, которые ты нашел в других временных рамках";
            text[2] = "Не отдам. Вы все отвратительны и ужасны. Хватит с этого";
            text[3] = "Видимо ты не понимаешь, во что попал. Ладно. Сейчас я тебе покажу, что значит не слушаться повелителей";
            lvlnum = 4;
        }
        if (Application.loadedLevelName == "level6")
        {
            govoriashii = new int[5];
            for (int i = 0; i < govoriashii.Length; i++)
            {
                govoriashii[i] = 1;
            }
            lvlnum = 4;
            text[0] = "E: Зачем ты убил чиновника?";
            text[1] = "E: У нас изначально был другой уговор";
            text[2] = "E: ТЫ посмел ослушаться меня";
            text[3] = "E: тебе не жить, ты будешь служить мне до конца своих дней!";
        }
        transform.position = new Vector3(transform.position.x, transform.position.y, 0);
        StartCoroutine(lol());
    }
    private void FixedUpdate()
    {
        if (GameObject.Find("Main Camera").GetComponent<Camera>().orthographicSize > 1.2f && trig == true)
            GameObject.Find("Main Camera").GetComponent<Camera>().orthographicSize -= 0.01f;
        else trig = false;
        if (GameObject.Find("Main Camera").GetComponent<Camera>().orthographicSize < 1.7f && trigend == true)
            GameObject.Find("Main Camera").GetComponent<Camera>().orthographicSize = GameObject.Find("Main Camera").GetComponent<Camera>().orthographicSize + 0.01f;
        else if(trigend==true) GetComponent<exodus>().enabled = false;
    }
    // Update is called once per frame
    void Update()
    {
        if (!nachalo&&Mathf.Abs(Vector2.Distance(pla.transform.position, transform.position)) < distance)
        {
            trig = true;
            pla.GetComponent<movement>().enabled=false;
            pla.GetComponent<Animator>().SetBool("hod", false);
            GameObject.Find("Main Camera").GetComponent<camera>().enabled = false;
            //GameObject.Find("Main Camera").transform.position = new Vector3(pla.transform.position.x,pla.transform.position.y+1, GameObject.Find("Main Camera").transform.position.z);
            dialog.text = text[0];
            nachalo = true;
            if (govoriashii[num] == 0) GameObject.Find("Main Camera").transform.position = new Vector3(pla.transform.position.x, pla.transform.position.y + 0.3f, GameObject.Find("Main Camera").transform.position.z);
            else GameObject.Find("Main Camera").transform.position = new Vector3(transform.position.x, transform.position.y + 0.3f, GameObject.Find("Main Camera").transform.position.z);
        }
        else if (Input.GetMouseButtonDown(0) && num < lvlnum&&nachalo)
        {
            num++;
            dialog.text = text[num];
            if (govoriashii[num] == 0) GameObject.Find("Main Camera").transform.position = new Vector3(pla.transform.position.x, pla.transform.position.y + 0.3f, GameObject.Find("Main Camera").transform.position.z);
            else GameObject.Find("Main Camera").transform.position = new Vector3(transform.position.x, transform.position.y + 0.3f, GameObject.Find("Main Camera").transform.position.z);
        }
        
        if (num == lvlnum)
        {
            pla.GetComponent<movement>().enabled = true;
            GameObject.Find("Main Camera").GetComponent<camera>().enabled = true;
            trigend = true;
            if ((Mathf.Abs(Vector2.Distance(pla.transform.position, transform.position)) < 1 || Mathf.Abs(Vector2.Distance(pla.transform.position, transform.position)) > 2.2f) && !ster)
            {
                ster = true;
                dialog.text = "";
            }
            if (Application.loadedLevelName == "level3" && (gameObject.name == "ТЕНИ ТСОРИИ")) GetComponent<Animator>().SetBool("deathall", true);
            if (Application.loadedLevelName == "level4" && (gameObject.name == "alchimick")) GetComponent<alchimick>().enabled = true;
            if (Application.loadedLevelName == "level4" && (gameObject.name == "knightBOSS")) GetComponent<knightBOSS>().enabled = true;
            if (Application.loadedLevelName == "level5") GetComponent<chinovnik>().enabled = true;
            if (Application.loadedLevelName == "level6") GetComponent<EXODUSboss>().enabled = true;
        }
        Vector3 tempvector = new Vector3(0,podniatii,0);
        if (gameObject.name == "EXODUS") transform.position = Vector3.MoveTowards(transform.position, transform.position + tempvector, speed * Time.deltaTime);
    }
    IEnumerator lol()
    {
        yield return new WaitForSeconds(2f);
        podniatii = -1 * podniatii;
        StartCoroutine(lol());
    }
}
