using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SlideShow : MonoBehaviour
{
    private int currentIndex = 0;
    [SerializeField]
    public float[] times;
    // Start is called before the first frame update

    private List<Transform> children = new List<Transform>();

    public float nextActionTime = 0;
    void Start()
    {
        nextActionTime = Time.time + times[0];
        foreach (Transform child in transform)
        {
            children.Add(child);
            child.gameObject.SetActive(false);
        }
        children[0].gameObject.SetActive(true);
    }

    void Update()
    {
        if (Time.time > nextActionTime)
        {
           nextSlide();
        }
    }

    public void nextSlide()
    {
        if (currentIndex + 1 < children.Count)
         {
             children[currentIndex].gameObject.SetActive(false);
             children[++currentIndex].gameObject.SetActive(true);
             nextActionTime += times[currentIndex];
         }
         else if (SceneManager.sceneCountInBuildSettings == SceneManager.GetActiveScene().buildIndex + 1)
         {
            SceneManager.LoadScene(0);
        }
        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
