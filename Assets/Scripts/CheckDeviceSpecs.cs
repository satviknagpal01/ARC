using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.ARFoundation;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CheckDeviceSpecs : MonoBehaviour
{
    [SerializeField] ARSession m_Session;

    public GameObject canDo;
    public GameObject cantDo;
    public GameObject sceneStart;

    bool needUpdate=true;

    private void Start()
    {
        canDo.SetActive(false);
        sceneStart.SetActive(false);
        cantDo.SetActive(false);
        
    }

    private void Update()
    {
        if(needUpdate)
            StartCoroutine(Check());
    }

    IEnumerator Check()
    {
        if((ARSession.state == ARSessionState.None) || (ARSession.state == ARSessionState.CheckingAvailability))
        {
            yield return ARSession.CheckAvailability();
        }
        if(ARSession.state == ARSessionState.Unsupported)
        {
            cantDo.SetActive(true);
        }
        else if(ARSession.state == ARSessionState.NeedsInstall)
        {
            Application.OpenURL("https://play.google.com/store/apps/details?id=com.google.ar.core&hl=en_IN&gl=US");
        }
        else
        {
            canDo.SetActive(true);
            sceneStart.SetActive(true);
            needUpdate = false;
        }
    }
    public void sceneStartGet()
    {
        SceneManager.LoadScene(1);
    }
}
