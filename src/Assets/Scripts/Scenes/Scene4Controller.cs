using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class Scene4Controller : MonoBehaviour
{
    [SerializeField] private GameObject[] _images;

    [YarnCommand("choose_scene4_image")]
    public void ChooseScene4Image(int index)
    {
        HideAllImages();
        _images[index].SetActive(true);
    }

    private void HideAllImages()
    {
        foreach (var image in _images)
        {
            image.SetActive(false);
        }
    }
}
