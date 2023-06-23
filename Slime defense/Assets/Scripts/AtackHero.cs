using UnityEngine;
using UnityEngine.UI;

public class AtackHero : MonoBehaviour
{
    public GameObject Sword;
    public GameObject Shield;

    public float CoolDownHit = 0.5f;
    public float timer = 0;

    RaycastHit hittedObject;
    Ray ray;

    public GameObject prefabCircle;
    public GameObject createdCircle;
    public GameObject flameStrike;
    public GameObject createdFlameStrike;

    public Text manaText;

    public bool is_R_pressed = false;
    public string currentAnimatorState;

    public float sunstrikeDamage;
    public float sunstrikeScale;

    public float manaPlayer = 100.0f;
    public float maxManaPlayer = 100.0f;
    public float sunstrikePrice = 40.0f;

    public float manaPerSecond = 1.0f;
    public float nextManaRegenTime = 0.0f;

    void Start()
    {
        sunstrikeDamage = 20;
        sunstrikeScale = 1;
        InvokeRepeating("GiveManaPerSecond", 0.0f, 3F);
    }

    void Update()
    {
        currentAnimatorState = GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).fullPathHash.ToString();
        manaText.text = manaPlayer.ToString();
        Shield.GetComponent<BoxCollider>().enabled = false;

        if (Camera.main.transform.parent != null && Time.timeScale != 0f)
        {
            if (Input.GetMouseButton(1) && currentAnimatorState != "-86853306" && currentAnimatorState != "1011701323")
            {
                Defend();
            }
            else if (timer < 0)
            {
                if (Input.GetMouseButton(0) && currentAnimatorState != "-86853306" && currentAnimatorState != "-1550725538")
                {
                    SwordAttack();
                }

                Sunstrike();
            }
            else
            {
                timer -= Time.deltaTime;
            }
        }
    }

    void SwordAttack()
    {
        if (!is_R_pressed)
        {
            GetComponent<Animator>().speed = 0;
            GetComponent<Animator>().Play("Attack01_SwordAndShiled");
            GetComponent<Animator>().speed = 1;

            Sword.GetComponent<Sword>().LastHittedSlime = null;
            timer = CoolDownHit;
        } 
    }

    void Defend()
    {
        if (!is_R_pressed)
        {
            GetComponent<Animator>().speed = 0;
            GetComponent<Animator>().Play("Defend_SwordAndShield");
            GetComponent<Animator>().speed = 1;

            Shield.GetComponent<BoxCollider>().enabled = true;
        }
    }

    void Sunstrike()
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Physics.Raycast(ray, out hittedObject, Mathf.Infinity, 1 << 8);

        if (Input.GetKeyDown(KeyCode.R) && manaPlayer >= sunstrikePrice)
        {
            if (!is_R_pressed)
            {
                if (hittedObject.collider)
                {
                    createdCircle = Instantiate(prefabCircle, hittedObject.point, Quaternion.identity);
                    createdCircle.transform.localScale += new Vector3(sunstrikeScale - 1, 0, sunstrikeScale - 1) / 2;
                    is_R_pressed = true;
                }
            }
            else
            {
                Destroy(createdCircle);
                is_R_pressed = false;
            }
        }
        else if (is_R_pressed)
        {
            if (hittedObject.collider)
            {
                createdCircle.transform.position = hittedObject.point;

                if (Input.GetMouseButtonDown(0))
                {
                    manaPlayer -= sunstrikePrice;
                    createdFlameStrike = Instantiate(flameStrike, createdCircle.transform.position, Quaternion.identity);
                    createdFlameStrike.transform.GetChild(4).GetComponent<FlameStrike>().damage = sunstrikeDamage;
                    createdFlameStrike.transform.GetChild(4).GetComponent<BoxCollider>().size += new Vector3 (sunstrikeScale - 1, 0, sunstrikeScale - 1);
                    Destroy(createdCircle);
                    is_R_pressed = false;
                    timer = CoolDownHit;
                }
            }
        }
    }

    void GiveManaPerSecond()
    {
        if (manaPlayer < maxManaPlayer)
        {
            manaPlayer += manaPerSecond;
            nextManaRegenTime += 3.0f;
        }
    }
}
