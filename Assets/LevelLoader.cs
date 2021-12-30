using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour {

    private Animator animator;

    private void Awake() {
        animator = GetComponent<Animator>();
    }

    void Update() {

    }

    void restartScene(string sceneName) {
        LoadLevel(sceneName);
    }

    IEnumerator LoadLevel(string sceneName) {
        animator.SetTrigger("start");
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(sceneName);
    }
}
