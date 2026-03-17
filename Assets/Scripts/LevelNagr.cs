using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelNagr : MonoBehaviour
{
    public string SceneName;
    public void Loadscene() 
    { 
     SceneManager.LoadScene(SceneName);
    }
     
    private void OnTriggerEnter(Collider other)
    {
        
        if (other.gameObject.CompareTag("Player")) 
        {

            SceneManager.LoadScene(SceneName);
        
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
