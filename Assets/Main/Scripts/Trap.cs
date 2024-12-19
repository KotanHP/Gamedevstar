using System.Collections;
using UnityEngine;

public class Trap : MonoBehaviour
{
    [SerializeField] float attackDamage = 1f;
    [SerializeField] float attackPeriod = 0.1f;

    private bool ableToAttack = false;

    private void OnTriggerEnter(Collider other)
    {
        print("able to attack");
        if(other.TryGetComponent<PlayerMortality>(out PlayerMortality playerMortality))
        {
            ableToAttack = true;
            StartCoroutine(Attack(playerMortality));
        }
        print(other.gameObject);
    }

    IEnumerator Attack(PlayerMortality playerMortality)
    {
        while (ableToAttack)
        {
            playerMortality.ChangeHealth(-attackDamage);
            print(playerMortality.GetCurrentHealth());
            yield return new WaitForSeconds(attackPeriod);
        }
    }

    //применимо если на карте предполагается один игрок с PlayerMortality. Если их будет несколько, стоит перепишите с использованием List<Mortality>
    private void OnTriggerExit(Collider other)
    {
        if(other.GetComponent<PlayerMortality>())
        {
            ableToAttack = false;
        }
    }
}
