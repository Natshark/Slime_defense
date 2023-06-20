using UnityEngine;
using UnityEngine.UI;

public class AtackHero : MonoBehaviour
{
    public GameObject Sword;

    public float CoolDownHit = 0.5f;
    public float timer = 0;

    RaycastHit hittedObject;
    Ray ray;

    public GameObject prefabCircle;
    public GameObject createdCircle;
    public GameObject flameStrike;

    public Text manaText;

    public bool is_R_pressed = false;

    public float manaPlayer = 100.0f;
    public float maxManaPlayer = 100.0f;
    public float sunstrikePrice = 40.0f;

    public float manaPerSecond = 1.0f;
    public float nextManaRegenTime = 0.0f;

    void Start()
    {
        InvokeRepeating("GiveManaPerSecond", 0.0f, 3F);
    }

    void Update()
    {
        manaText.text = manaPlayer.ToString();

        if (timer < 0)
        {
            if (GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).fullPathHash.ToString() != "-86853306")
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

    void SwordAttack()
    {
        if (Input.GetMouseButtonDown(0) && Time.timeScale != 0f && !is_R_pressed)
        {
            if (Camera.main.transform.parent != null)
            {
                GetComponent<Animator>().speed = 1;
                GetComponent<Animator>().Play("Attack01_SwordAndShiled");

                Sword.GetComponent<Sword>().LastHittedSlime = null;
                timer = CoolDownHit;
            }
        } 
    }

    void Defend()
    {
        if (Input.GetMouseButtonDown(1) && Time.timeScale != 0f && !is_R_pressed)
        {
            if (Camera.main.transform.parent != null)
            {
                GetComponent<Animator>().speed = 1;
                GetComponent<Animator>().Play("Defend_SwordAndShield");
            }
        }
    }

    void Sunstrike()
    {
        if (Camera.main.transform.parent != null && Time.timeScale != 0f)
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
                        Instantiate(flameStrike, createdCircle.transform.position, Quaternion.identity);
                        Destroy(createdCircle);
                        is_R_pressed = false;
                        timer = CoolDownHit;
                    }
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
