using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
 * 
 * Collisions damage
 * 
 */
public class PlayerHealth : MonoBehaviour
{
    public float regenPerSecond = 30f;
    public float collisionDamage = 0.7f;
    public Slider healthSlider;
    private float playerHp = 100f;
    private bool playerDead = false;
    private bool regenActive = true;

    void Start()
    {
        healthSlider.value = playerHp;
    }

    private void OnCollisionStay()
    {
        // TODO: smoke etc from airplane

        DamagePlayer(collisionDamage);

    }

    // Positive values for health loss, negative values for health regen
    public void DamagePlayer(float dmgTaken)
    {
        regenActive = false;

        playerHp -= dmgTaken;
        if (playerHp < 0) playerHp = 0;

        healthSlider.value = playerHp;
        if (playerHp == 0 && !playerDead)
        {
            PlayerDeath();
        }
    }

    public void HealPlayer(float dmgHealed)
    {
        playerHp += dmgHealed;
        if (playerHp > 100f) playerHp = 100f;

        healthSlider.value = playerHp;
    }

    private IEnumerator HealthRegeneration(float regenPerSecond)
    {
        if (!regenActive)
        {
            regenActive = true;
            yield return new WaitForSeconds(3f);
            while (playerHp < 100f && regenActive)
            {
                HealPlayer(regenPerSecond / 100);
                yield return new WaitForSeconds(0.01f);
            }
        }
    }
    private void OnCollisionExit()
    {
        StartCoroutine(HealthRegeneration(regenPerSecond));
    }

    private void PlayerDeath()
    {
        playerDead = true;
        // camera stop?
        // explosion trigger?
        // after animation go to game over scene 
    }
}
