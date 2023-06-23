using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public Animator animator;

    public float hp;
    public float shieldBlock;
    public bool isDead = false;

    public GameObject triggerredSlime = null;
    public GameObject closestSlime = null;
    public GameObject[] slimes;
    public float mindist;
    public float curdist;

    public float deathCoolDown;
    public float timer;

    public Text playerHpText;
    public float hpPerSecond = 1.0f;
    public float maxHpPlayer;

    void Start()
    {
        shieldBlock = 0.75f;
        maxHpPlayer = 100;
        hp = maxHpPlayer;
        deathCoolDown = 15;
        animator = GetComponent<Animator>();
        InvokeRepeating("hpRegeneration", 0, 1);
    }

    void Update()
    {
        playerHpText.text = hp.ToString();

        if (!isDead)
        {
            if (hp <= 0)
            {
                Camera.main.GetComponent<ToggleCamera>().thirdFaceView();
                triggerredSlime = null;

                animator.speed = 0;
                animator.Play("Die01_SwordAndShield");
                animator.speed = 1;

                isDead = true;
                timer = deathCoolDown;
            }
            else
            {
                slimes = GameObject.FindGameObjectsWithTag("RedSlime"); // поиск ближайшего слайма и его триггерринг
                mindist = Mathf.Infinity;
                findSlime();
                slimes = GameObject.FindGameObjectsWithTag("BlueSlime");
                findSlime();
                slimes = GameObject.FindGameObjectsWithTag("RedSlimeBoss");
                findSlime();
                slimes = GameObject.FindGameObjectsWithTag("BlueSlimeBoss");
                findSlime();

                if (closestSlime && Vector3.Distance(transform.position, closestSlime.transform.position) <= 7.5f)
                {
                    closestSlime.GetComponent<Slime>().target = gameObject;
                    triggerredSlime = closestSlime;
                }
                else
                {
                    triggerredSlime = null;
                }
            }
        }
        else if (timer > 0)
        {
            timer -= Time.deltaTime;
        }
        else
        {
            hp = maxHpPlayer;
            isDead = false;

            animator.speed = 0;
            animator.Play("GetUp_SwordAndShield");
            animator.speed = 1;
        }
    }

    public void hpRegeneration()
    {
        if (hp < maxHpPlayer && !isDead)
        {
            hp += hpPerSecond;
        }
    }

    public void GetDamage(float damage)
    {
        if (hp > 0)
        {
            hp -= damage;
            if (hp < 0)
            {
                hp = 0;
            }
        }
    }

    void findSlime()
    {
        foreach (GameObject slime in slimes)
        {
            curdist = Vector3.Distance(transform.position, slime.transform.position);
            if (curdist < mindist)
            {
                mindist = curdist;
                closestSlime = slime;
            }
        }
    }
}
