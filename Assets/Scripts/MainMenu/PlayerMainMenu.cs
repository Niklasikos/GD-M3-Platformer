using System.Collections;
using UnityEngine;

public class PlayerMainMenu : MonoBehaviour
{
    private Animator animator;
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        animator.Play("Player_Idle");
    }
}
