using System.Collections;
using UnityEngine;


public class CharacterAnimationEvent : MonoBehaviour
{
    private Character character;
   
    private void Awake()
    {
        character = transform.root.GetComponent<Character>();
    }

    public void OnAttack()
    {
        character.Shoot();
    }

    public void EndAttack()
    {
        
        if (character.isRunning)
        {
            character.ChangeState(BehaviourState.Run);
        }
        else
        {
            character.ChangeState(BehaviourState.Idle);
        }
    }
}
