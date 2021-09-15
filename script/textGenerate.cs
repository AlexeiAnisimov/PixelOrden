using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class textGenerate : MonoBehaviour
{
    public Text learn;
    public Transform player;
    bool continuetext;
    int num = 0;
    string[] txt;
    Transform[] cord;
    // Start is called before the first frame update
    void Start()
    {

        cord = GameObject.Find("control").GetComponentsInChildren<Transform>();
        //for (int i = 0; i < cord.Length; i++) Debug.Log(cord[i].position);
        txt = new string[cord.Length];
        txt[1] = "пойти налево-кнопка A" + '\n' + "пойти направо-кнопка D";
        txt[2] = "чтобы прыгнуть нажмите" + '\n' + "space ";
        txt[3] = "для двойного прыжка нажмите" + '\n' + "кнопку прыжка два раза";
        txt[4] = "потренеруйтесь в стрельбе. Попадите в мишени. Чтобы одеть оружие прокрутите колесико.Стрелять-левая кнопка мыши.";
        txt[5] = "будьте осторожны!" + '\n' + "при сильном падении вы умрете или потеряете жизни";
        txt[6] = "чтобы подбобрать предмет нужно нажать r";
        txt[8] = "дальше будет много врагов!";
        txt[7] = "приготовься стрелять,рядом враг. Для стрельбы нажимать на левую кнопку мыши";
        txt[9] = "возможно здесь ты что-то найдешь";
        txt[10] = "для того,чтобы ударить, нажмите левую кнопку мыши для быстрого удара,правую для мощного(после него появляется KD)";
        txt[11] = "кажется подходим к ...";
        txt[12] = "это ключ.Им можно открыть дверь. Чтобы его взять нажмите r";
        txt[13] = "много вещей";
        txt[14] = "совет: чтобы убить персонажа со 100 или меньше hp,можно подойти вплотную и атаковать мощным ударом";
        txt[15] = "осторожно! вы можете упасть";
        txt[16] = "Справа поднимающаяся платформа. Чтобы активировать,запрыгните на нее";
        txt[17] = "УДАЧИ В СРАЖЕНИИ";
        txt[18] = "Добро пожаловать в pixel orden!";
        txt[19] = "К: 'Интересно,кто это мог заказать пиццу в таком разваленном доме'";
        txt[20] = "К: 'Видимо,опять наркоманы заказали'";
        txt[21] = "К: 'Почему у этой лесницы такие огромные ступеньки??'";
        txt[22] = "К: 'Господи,какой лифт-то! Написано 10 этаж,значит придется ехать на нем'";
        txt[23] = "К: 'По-моему я еду вниз,а не вверх...'";
    }

    // Update is called once per frame
    void Update()
    {
        for(int i=1;i<cord.Length;i++)
            if(Mathf.Abs(player.position.x-cord[i].position.x)<0.2f&& Mathf.Abs(player.position.y - cord[i].position.y )< 0.5f)
            {
                num = i;
                StartCoroutine(kek());
            }
    }
    IEnumerator kek()
    {
        if (num >= 19 && num <= 23) GameObject.Find("dialog").GetComponent<Text>().text = txt[num];
        else learn.text = txt[num];
        yield return new WaitForSeconds(4f);
        for (int i = 1; i < cord.Length; i++)
        {
            if (Mathf.Abs(player.position.x - cord[i].position.x) < 0.2f && Mathf.Abs(player.position.y - cord[i].position.y) < 0.5f)
            {
                num = i;
                StartCoroutine(kek());
                break;
            }
            if (i == cord.Length - 1)
            {
                if (num >= 19 && num <= 23) GameObject.Find("dialog").GetComponent<Text>().text = "";
                learn.text = "";
            }

        }
        //if (num == 0) learn.text = "";
    }
}
