using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    [Header("People")]
    [SerializeField] private GameObject player1;
    [SerializeField] private GameObject player2;
    [SerializeField] private GameObject enemy;
    [SerializeField] private GameObject enemyAura;

    [Header("Health Bar")]
    [SerializeField] private Slider playerHealth;
    [SerializeField] private Slider enemyHealth;

    [Header("Character Selection")]
    [SerializeField] private GameObject player1Selection;
    [SerializeField] private GameObject player2Selection;
    [SerializeField] private GameObject enemySelection;

    [Header("End Screens")]
    [SerializeField] private GameObject enemyDefeated;
    [SerializeField] private GameObject enemyConverted;
    [SerializeField] private GameObject enemyWon;

    [Header("Player Actions")]
    [SerializeField] private float player1AttackValue;
    [SerializeField] private float player1EmpathyValue;
    [SerializeField] private float player2AttackValue;
    [SerializeField] private float player2EmpathyValue;

    [Header("Action Buttons")]
    [SerializeField] private Button attackButton;
    [SerializeField] private Button empathyButton;
    [SerializeField] private Button strongerAttackButton;
    [SerializeField] private Button strongerEmpathyButton;

    [Header("Enemy Stats")]
    [SerializeField] private float enemyAttackValue = 0.25f;
    [SerializeField] private float enemyHealthValue = 1.0f;

    private List<GameObject> turnOrder;
    private int currentTurnIndex;

    void Start()
    {
        InitializeGame();
    }

    private void InitializeGame()
    {
        turnOrder = new List<GameObject> { player1, player2, enemy };
        currentTurnIndex = 0;
        enemyHealth.maxValue = enemyHealthValue;
        enemyHealth.value = enemyHealthValue;
        UpdateTurn();
    }

    private void UpdateTurn()
    {
        DeactivateSelections();
        DisableActionButtons();

        if (turnOrder[currentTurnIndex] == player1)
        {
            ActivatePlayer1Turn();
        }
        else if (turnOrder[currentTurnIndex] == player2)
        {
            ActivatePlayer2Turn();
        }
        else if (turnOrder[currentTurnIndex] == enemy)
        {
            ActivateEnemyTurn();
        }
    }

    private void DeactivateSelections()
    {
        player1Selection.SetActive(false);
        player2Selection.SetActive(false);
        enemySelection.SetActive(false);
    }

    private void DisableActionButtons()
    {
        attackButton.interactable = false;
        empathyButton.interactable = false;
        strongerEmpathyButton.interactable = false;
    }

    private void ActivatePlayer1Turn()
    {
        player1Selection.SetActive(true);
        EnableActionButtons();
    }

    private void ActivatePlayer2Turn()
    {
        player2Selection.SetActive(true);
        EnableActionButtons();
    }

    private void ActivateEnemyTurn()
    {
        enemySelection.SetActive(true);
        StartCoroutine(EnemyAttackRoutine());
    }

    private void EnableActionButtons()
    {
        attackButton.interactable = true;
        empathyButton.interactable = true;
        strongerEmpathyButton.interactable = true;
    }

    private IEnumerator EnemyAttackRoutine()
    {
        yield return new WaitForSeconds(1.0f);
        playerHealth.value -= playerHealth.maxValue * enemyAttackValue;

        if (playerHealth.value <= 0)
        {
            enemyWon.SetActive(true);
        }
        else
        {
            yield return new WaitForSeconds(1.0f);
            NextTurn();
        }
    }

    public void EmpathyButton()
    {
        AdjustEnemyAura();

        if (enemyAura.GetComponent<Image>().color.a == 0)
        {
            enemyConverted.SetActive(true);
        }
        else
        {
            NextTurn();
        }
    }

    //continuar depois do play test
    public void StrongerEmpathyButton()
    {
        AdjustEnemyAura();

        if (enemyAura.GetComponent<Image>().color.a == 0)
        {
            enemyConverted.SetActive(true);
        }
        else
        {
            NextTurn();
        }
    }


    private void AdjustEnemyAura()
    {
        Color auraColor = enemyAura.GetComponent<Image>().color;

        string[] characters = { "Jimmy", "Alice", "Jake", "Vanessa", "Gabirel" };
        if (turnOrder[currentTurnIndex] == player1)
            foreach (string character in characters)
            {
                if (player1.CompareTag(character))
                {
                    player1EmpathyValue = GetCharacterEmpathy(character);
                    auraColor.a = Mathf.Max(0, auraColor.a - player1EmpathyValue);
                    enemyAura.GetComponent<Image>().color = auraColor;
                }

            }
        if (turnOrder[currentTurnIndex] == player2)
            foreach (string character in characters)
            {
                if (player2.CompareTag(character))
                {
                    player2EmpathyValue = GetCharacterEmpathy(character);
                    auraColor.a = Mathf.Max(0, auraColor.a - player2EmpathyValue);
                    enemyAura.GetComponent<Image>().color = auraColor;
                }

            }
    }

    private float GetCharacterEmpathy(string character)
    {

        switch (character)
        {

            case "Jimmy":
                return 0.25f;

            case "Alice":
                return 0.25f;

            case "Jake":
                return 0.15f;

            case "Vanessa":
                return 0.45f;

            case "Gabriel":
                return 0.35f;

        }
        return 0f;

    }

    public void AttackButton()
    {
        AdjustEnemyHealth();

        if (enemyHealth.value == 0)
        {
            enemyDefeated.SetActive(true);
        }
        else
        {
            NextTurn();
        }
    }
    
    //continuar depois do play test
    public void StrongerAttackButton()
    {
        AdjustEnemyHealth();

        if (enemyAura.GetComponent<Image>().color.a == 0)
        {
            enemyConverted.SetActive(true);
        }
        else
        {
            NextTurn();
        }
    }

    private void AdjustEnemyHealth()
    {
        string[] characters = {"Jimmy", "Alice", "Jake", "Vanessa", "Gabirel"};
        if (turnOrder[currentTurnIndex] == player1)
            foreach (string character in characters)
            {
                if (player1.CompareTag(character)) 
                {
                    player1AttackValue = GetCharacterDamage(character);
                    enemyHealth.value -= enemyHealth.maxValue * player1AttackValue;
                }

            }
        if (turnOrder[currentTurnIndex] == player2)
            foreach (string character in characters)
            {
                if (player2.CompareTag(character))
                {
                    player2AttackValue = GetCharacterDamage(character);
                    enemyHealth.value -= enemyHealth.maxValue * player2AttackValue;
                }

            }
    }

    private float GetCharacterDamage(string character) 
    {

        switch (character)
        {

            case "Jimmy":
                return 0.25f;

            case "Alice":
                return 0.25f;

            case "Jake":
                return 0.5f;

            case "Vanessa":
                return 0.10f;

            case "Gabriel":
                return 0.15f;

        }
        return 0f;

    }

    private void NextTurn()
    {
        currentTurnIndex = (currentTurnIndex + 1) % turnOrder.Count;
        UpdateTurn();
    }

    public void ResetSceneButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
