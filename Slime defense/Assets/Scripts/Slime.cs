
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using static System.Runtime.CompilerServices.RuntimeHelpers;

public class Slime : MonoBehaviour
{
    Animator slimeAnimator;

    void Start()
    {
        slimeAnimator = GetComponent<Animator>();
    }

    void Update()
    {
        //slimeAnimator.Play("Taunt");
        //OnKeyDown();
    }
}
