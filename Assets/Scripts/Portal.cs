using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D other)
    {
        // If the player has collided with portal
        if (other.gameObject.tag == "Player")
        {
            // Load the next scene
            // The buildIndex is from the Build Settings
            int currentScene = SceneManager.GetActiveScene().buildIndex;
            int nextScene = currentScene + 1;
            SceneManager.LoadScene(nextScene, LoadSceneMode.Single);
        }
    }
}
