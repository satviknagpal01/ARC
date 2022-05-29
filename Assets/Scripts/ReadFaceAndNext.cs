using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.SceneManagement;

public class ReadFaceAndNext : MonoBehaviour
{
    public ARFaceManager faceManager;
    void Start()
    {
        faceManager = GetComponent<ARFaceManager>();
        faceManager.facesChanged += OnFaceChanged;
    }


    void Update()
    {

    }

    void OnFaceChanged(ARFacesChangedEventArgs args)
    {
        if (args.added.Count == 1)
        {
            SceneManager.LoadScene(2);
        }
        if (args.removed.Count == 1)
        {
            OnFaceNotVisible();
        }
    }
    public void OnFaceVisible()
    {
        SceneManager.LoadScene(2);
    }

    void OnFaceNotVisible()
    {

    }
}
