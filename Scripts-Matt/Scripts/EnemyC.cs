using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyC : MonoBehaviour
{
    private Rigidbody eRb;
    public string[] scenes;

    // Start is called before the first frame update
    void Start()
    {
        eRb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player")){
            other.gameObject.SetActive(false);
            StartCoroutine(PlayerDeath());
        }
    }

    IEnumerator PlayerDeath()
    {
        yield return new WaitForSeconds(5);
        SceneManager.LoadScene(scenes[0]);
    }
}
