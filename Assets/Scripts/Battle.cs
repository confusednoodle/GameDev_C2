using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;


public class Battle : MonoBehaviour
{
    bool charged = false;
    bool defend = false;
    int playerHP = 100;
    int enemyHP = 100;

    [SerializeField] TMP_Text battleText;
    [SerializeField] TMP_Text hitpointsText;
    [SerializeField] Image healthBar;

    [SerializeField] AudioSource hitSound;
    [SerializeField] AudioSource healSound;
    [SerializeField] AudioSource victorySound;
    [SerializeField] AudioSource deathSound;
    [SerializeField] AudioSource chargeSound;
    [SerializeField] AudioSource specialSound;
    [SerializeField] AudioSource escapeSound;

    [SerializeField] Button attackButton;
    [SerializeField] Button healButton;
    [SerializeField] Button defendButton;
    [SerializeField] Button runButton;

    [SerializeField] Animator animator;



    public void Attack()
    {
        defend = false;

        attackButton.interactable = false;
        healButton.interactable = false;
        defendButton.interactable = false;
        runButton.interactable = false;

        int number = Random.Range(1, 101);
        if (number < 11) //missing attack
        {
            hitSound.Play();
            battleText.text = "YOU MISS! THE ENEMY TAKES 0 DAMAGE";
            StartCoroutine(WaitForEnemy());
        }

        else if (number > 10 & number < 41) //light attack
        {
            hitSound.Play();
            int damage = Random.Range(5, 11);
            battleText.text = "YOU LAND A LIGHT HIT! THE ENEMY TAKES " + damage.ToString() + " DAMAGE";
            animator.Play("hit");
            enemyHP -= damage;
            StartCoroutine(WaitForEnemy());
        }

        else if (number > 40 & number < 91) //moderate attack
        {
            hitSound.Play();
            int damage = Random.Range(20, 31);
            battleText.text = "YOU LAND A MODERATE HIT! THE ENEMY TAKES " + damage.ToString() + " DAMAGE";
            animator.Play("hit");
            enemyHP -= damage;
            StartCoroutine(WaitForEnemy());
        }

        else if (number > 90 & number < 101) //critical attack
        {
            hitSound.Play();
            int damage = Random.Range(40, 51);
            battleText.text = "A CRITICAL HIT! THE ENEMY TAKES " + damage.ToString() + " DAMAGE";
            animator.Play("hit");
            enemyHP -= damage;
            StartCoroutine(WaitForEnemy());
        }

        if (enemyHP <= 0)
        {
            victorySound.Play();
            battleText.text = "ENEMY DEFEATED!";
            StartCoroutine(WaitForScene());
        }
    }

    public void Heal()
    {
        attackButton.interactable = false;
        healButton.interactable = false;
        defendButton.interactable = false;
        runButton.interactable = false;

        healSound.Play();
        int number = Random.Range(20, 70);
        playerHP += number;
        if (playerHP >= 100)
        {
            playerHP = 100;
            hitpointsText.text = playerHP.ToString() + " HP";
            battleText.text = "YOUR HP ARE FULLY RESTORED!";
            healthBar.fillAmount = 1.0f;
        }
        else
        {
            battleText.text = "YOU RESTORE " + number.ToString() + " HP!";
            hitpointsText.text = playerHP.ToString() + " HP";
            healthBar.fillAmount += number * 0.01f;
        }


        StartCoroutine(WaitForEnemy());
    }

    public void Defend()
    {
        attackButton.interactable = false;
        healButton.interactable = false;
        defendButton.interactable = false;
        runButton.interactable = false;

        defend = true;
        battleText.text = "YOU PREPARE YOURSELF TO BLOCK!";

        StartCoroutine(WaitForEnemy());
    }

    public void Run()
    {
        attackButton.interactable = false;
        healButton.interactable = false;
        defendButton.interactable = false;
        runButton.interactable = false;

        escapeSound.Play();
        battleText.text = "YOU ESCAPE!";
        StartCoroutine(WaitForEscape());
    }

    void EnemyAttack()
    {

        int random = Random.Range(1, 4);
        if (enemyHP < 50 & random == 2 & charged == false) // Heal
        {
            Debug.Log("Heal");
            healSound.Play();
            enemyHP += 30;
            battleText.text = "THE ENEMY RESTORES SOME HP!";
            attackButton.interactable = true;
            healButton.interactable = true;
            defendButton.interactable = true;
            runButton.interactable = true;
        }

        else
        {
            int number = Random.Range(1, 101);
            Debug.Log(number.ToString());
            if ((number < 11 & charged == false) || defend) //missing attack
            {
                Debug.Log("missing attack");
                hitSound.Play();
                charged = false;

                battleText.text = "THE ENEMY MISSES!";

                attackButton.interactable = true;
                healButton.interactable = true;
                defendButton.interactable = true;
                runButton.interactable = true;
            }

            else if (number > 10 & number < 41 & charged == false) //light attack
            {
                Debug.Log("light attack");
                hitSound.Play();
                int damage = Random.Range(5, 11);
                playerHP -= damage;
                battleText.text = "THE ENEMY LANDS A LIGHT HIT! YOU LOSE " + damage.ToString() + " HP";

                if (playerHP <= 0)
                {
                    hitpointsText.text = "0 LP";
                    healthBar.fillAmount = 0.0f;
                    StartCoroutine(WaitForDeath());
                    StartCoroutine(WaitForLeave());
                }
                else
                {
                    hitpointsText.text = playerHP.ToString() + " HP";
                    healthBar.fillAmount -= damage * 0.01f;
                }

                attackButton.interactable = true;
                healButton.interactable = true;
                defendButton.interactable = true;
                runButton.interactable = true;

            }

            else if (number > 40 & number < 81 & charged == false) //moderate attack
            {
                Debug.Log("moderate attack");
                hitSound.Play();
                int damage = Random.Range(15, 31);
                playerHP -= damage;
                battleText.text = "THE ENEMY LANDS A MODERATE HIT! YOU LOSE " + damage.ToString() + " HP";

                if (playerHP <= 0)
                {
                    hitpointsText.text = "0 LP";
                    healthBar.fillAmount = 0.0f;
                    StartCoroutine(WaitForDeath());
                    StartCoroutine(WaitForLeave());
                }
                else
                {
                    hitpointsText.text = playerHP.ToString() + " HP";
                    healthBar.fillAmount -= damage * 0.01f;
                }

                attackButton.interactable = true;
                healButton.interactable = true;
                defendButton.interactable = true;
                runButton.interactable = true;
            }

            else if (number > 80 & number < 101 & charged == false) //charge
            {
                Debug.Log("charge");
                chargeSound.Play();
                charged = true;
                battleText.text = "THE ENEMY CHARGES FOR A SPECIAL ATTACK! WATCH OUT!";

                attackButton.interactable = true;
                healButton.interactable = true;
                defendButton.interactable = true;
                runButton.interactable = true;
            }

            else if (charged = true & number < 80) //critical attack
            {
                Debug.Log("critical attack");
                specialSound.Play();
                int damage = Random.Range(30, 41);
                playerHP -= damage;
                battleText.text = "A CRITICAL HIT! YOU LOSE " + damage.ToString() + " HP";

                if (playerHP <= 0)
                {
                    hitpointsText.text = "0 LP";
                    healthBar.fillAmount = 0.0f;
                    StartCoroutine(WaitForDeath());
                    StartCoroutine(WaitForLeave());
                }
                else
                {
                    hitpointsText.text = playerHP.ToString() + " HP";
                    healthBar.fillAmount -= damage * 0.01f;
                }
                charged = false;


                attackButton.interactable = true;
                healButton.interactable = true;
                defendButton.interactable = true;
                runButton.interactable = true;
            }

        }


    }

    private IEnumerator WaitForScene()
    {
        yield return new WaitForSeconds(1.9f);
        SceneManager.LoadScene("SampleScene");
    }

    private IEnumerator WaitForLeave()
    {
        yield return new WaitForSeconds(7.5f);
        SceneManager.LoadScene("SampleScene");
    }

    private IEnumerator WaitForEscape()
    {
        SharedState.PlayerEscaped = true;
        yield return new WaitForSeconds(1.3f);
        SceneManager.LoadScene("SampleScene");
    }

    private IEnumerator WaitForEnemy()
    {
        yield return new WaitForSeconds(2.0f);
        battleText.text = "THE ENEMY PREPARES TO MAKE A MOVE!";
        yield return new WaitForSeconds(2.0f);
        EnemyAttack();
    }

    private IEnumerator WaitForDeath()
    {
        yield return new WaitForSeconds(3.0f);
        deathSound.Play();
        transform.GetChild(2).gameObject.SetActive(false);
        transform.GetChild(3).gameObject.SetActive(false);
        transform.GetChild(4).gameObject.SetActive(false);
        transform.GetChild(5).gameObject.SetActive(false);
        transform.GetChild(1).gameObject.SetActive(true);
    }

    public void Start()
    {
        hitpointsText.text = playerHP.ToString() + " HP";
    }

}
