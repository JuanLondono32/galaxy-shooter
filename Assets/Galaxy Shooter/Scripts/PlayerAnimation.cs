using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator _anim;
	// Use this for initialization
	void Start ()
    {
        _anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame

    public void IsMovingLeft()
    {
        _anim.SetBool("TurnLeft", true);
        _anim.SetBool("TurnRight", false);
    }

    public void IsMovingRight()
    {
        _anim.SetBool("TurnRight", true);
        _anim.SetBool("TurnLeft", false);
    }

    public void IsIdle()
    {
        _anim.SetBool("TurnLeft", false);
        _anim.SetBool("TurnRight", false);
    }
}
