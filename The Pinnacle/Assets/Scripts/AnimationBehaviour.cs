using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationStates : MonoBehaviour
{
    private Animator animator;
    int IsMovingHash; 
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>(); // Get the animator component
        // IsMovingHash = Animator.StringToHash("IsMoving"); // Get the hash of the parameter "isWalking"
    }

    // Update is called once per frame
    void Update()
    {   
        bool isWalking = animator.GetBool(IsMovingHash); // Get the current value of the parameter "isWalking"
        if (Input.GetAxis("Vertical") != 0 && !isWalking)
        {
            animator.SetBool(IsMovingHash, true); // Set the value of the parameter "isWalking" to true
            Debug.Log("walking");
        }

        if (Input.GetAxis("Vertical") == 0 && isWalking)
        {
            animator.SetBool(IsMovingHash, false); // Set the value of the parameter "isWalking" to true
            Debug.Log("not walking");
        }
    }
}

