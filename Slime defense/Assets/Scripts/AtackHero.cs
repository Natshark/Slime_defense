using UnityEngine;

public class AtackHero : MonoBehaviour
{
    public GameObject Sword;

    public float CoolDownHit = 0f;

    RaycastHit hittedObject;
    Ray ray;

    public GameObject prefabCircle;
    public GameObject createdCircle;
    public GameObject flameStrike;

    public bool is_R_pressed = false;
    void Update()
    {
        if (CoolDownHit <= 0 && GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).fullPathHash.ToString() != "-86853306")
        {
            SwordAttack();
        }

        Sunstrike();
        CoolDownHit -= Time.deltaTime;
    }

    void SwordAttack()
    {
        if (Input.GetMouseButtonDown(0) && Time.timeScale != 0f && !is_R_pressed)
        {
            if (Camera.main.transform.parent != null)
            {
                GetComponent<Animator>().speed = 1;
                GetComponent<Animator>().Play("Attack01_SwordAndShiled");
                CoolDownHit = 0f;
                Sword.GetComponent<Sword>().LastHittedSlime = null;
            }
        } 
    }

    void Sunstrike()
    {
        if (Camera.main.transform.parent != null && Time.timeScale != 0f)
        {
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Physics.Raycast(ray, out hittedObject, Mathf.Infinity, 1 << 8);

            if (Input.GetKeyDown(KeyCode.R))
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
                        Instantiate(flameStrike, createdCircle.transform.position, Quaternion.identity);
                        Destroy(createdCircle);
                        is_R_pressed = false;
                    }
                }
            }

            
        }
    }
}
