using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Slime : MonoBehaviour
{
    [SerializeField] Sprite[] idleSprites;
    [SerializeField] float timeBetweenFrames = 0.1f;
    [Space]
    [SerializeField] string SceneToLoad;

    SpriteRenderer sr;
    float animationTimer = 0f;
    int animationIndex = 0;

    void Awake() => sr = GetComponent<SpriteRenderer>();

    void Update()
    {
        // Animate the slime sprite
        if (idleSprites.Length > 0)
        {
            if (animationTimer >= timeBetweenFrames)
            {
                sr.sprite = idleSprites[++animationIndex % idleSprites.Length];
                animationTimer = 0f;
            }

            animationTimer += Time.deltaTime;
        }
    }

    public void LoadScene()
    {
        switch (SceneToLoad.Replace("Opponent ", ""))
        {
            case "1":
                SharedState.EnemyName = "BAT";
                break;
            case "2":
                SharedState.EnemyName = "GHOST";
                break;
            case "3":
                SharedState.EnemyName = "SKELETON";
                break;
            default:
                SharedState.EnemyName = "404 ENEMY NAME NOT FOUND. THIS IS A BUG :((((((";
                break;
        }
        SceneManager.LoadScene(SceneToLoad);
    }
}
