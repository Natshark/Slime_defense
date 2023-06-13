using UnityEngine;


public class AtackHero : MonoBehaviour
{
    public GameObject Camera;
    public GameObject Sword;

    public float CoolDownHit = 0f;

    void Update()
    {
        if (CoolDownHit <= 0 && GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).fullPathHash.ToString() != "-86853306")
        {
            Attack();
        }

        CoolDownHit -= Time.deltaTime;
    }

    void Attack()
    {
        if (Input.GetMouseButtonDown(0) && Time.timeScale != 0f)
        {
            if (Camera.transform.parent != null)
            {
                GetComponent<Animator>().speed = 1;
                GetComponent<Animator>().Play("Attack01_SwordAndShiled");
                CoolDownHit = 0f;
                Sword.GetComponent<Sword>().LastHittedSlime = null;
            }
        } 
    }
}
