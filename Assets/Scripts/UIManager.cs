using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class UIManager : MonoBehaviour
{
    public GameObject damageTextPF;
    public GameObject healthTextPF;
    public Canvas gameCanvas;

    private void Awake() 
    {
        gameCanvas = FindObjectOfType<Canvas>();
    }

    private void OnEnable() 
    {
        PlayerEvents.playerTookDamage += playerDamaged;
        PlayerEvents.playerRestoredHealth += playerHealed;
    }

    private void OnDisable() 
    {
        PlayerEvents.playerTookDamage -= playerDamaged;
        PlayerEvents.playerRestoredHealth -= playerHealed;
    }
    
    public void playerDamaged(GameObject character, int dmgReceived)
    {
        //Creates the text on character hit.
        Vector3 spawnPos = Camera.main.WorldToScreenPoint(character.transform.position);

        TMP_Text tmpText = Instantiate(damageTextPF, spawnPos, Quaternion.identity, gameCanvas.transform)
            .GetComponent<TMP_Text>();
        
        tmpText.text = dmgReceived.ToString();
    }   

    public void playerHealed(GameObject character, int healthRestored)
    {
        //Creates the text on character heal.
        Vector3 spawnPos = Camera.main.WorldToScreenPoint(character.transform.position);

        TMP_Text tmpText = Instantiate(healthTextPF, spawnPos, Quaternion.identity, gameCanvas.transform)
            .GetComponent<TMP_Text>();
        
        tmpText.text = healthRestored.ToString();
    }

    public void OnExitGame(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            #if (UNITY_EDITOR || DEVELOPMENT_BUILD)
                Debug.Log(this.name + " : " + this.GetType() + " : " + System.Reflection.MethodBase.GetCurrentMethod().Name);
            #endif

            #if (UNITY_EDITOR)
                UnityEditor.EditorApplication.isPlaying = false;
            #elif (UNITY_STANDALONE)
                Application.Quit();
            #elif (UNITY_WEBGL)
                SceneManager.LoadScene("QuitScene");
            #endif

        }
    }
}
