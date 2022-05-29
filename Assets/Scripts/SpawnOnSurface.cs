using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class SpawnOnSurface : MonoBehaviour
{
    public ARRaycastManager RaycastManager;
    public ARPlaneManager planeManager;
    public GameObject objectPrefab;

    private GameObject _spawnedGameObject = null;

    public GameObject reticlePrefab;

    private GameObject reticle;
    private Vector2 screenPoint;

    private void Awake()
    {
        reticle = Instantiate(reticlePrefab, transform);
    }

    // Update is called once per frame
    void Update()
    {
        // screen point 
        screenPoint = Camera.main.ViewportToScreenPoint(new Vector2(0.5f, 0.5f));

        UpdateCursor();
        TryPlaceObject();
        ShowFrontPlanes();
    }

    private void UpdateCursor()
    {
        if (_spawnedGameObject == null)
        {
            // set reticle position
            List<ARRaycastHit> hits = new List<ARRaycastHit>();
            RaycastManager.Raycast(screenPoint, hits, TrackableType.Planes);
            
            if (hits.Count > 0)
            {
                reticle.gameObject.SetActive(true);
                reticle.transform.position = hits[0].pose.position;
                reticle.transform.rotation = hits[0].pose.rotation; 
            }
            else
            {
                reticle.gameObject.SetActive(false);
            }
        }
    }

    private void TryPlaceObject()
    {
        if (_spawnedGameObject == null)
        {
            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
            {
                List<ARRaycastHit> hits = new List<ARRaycastHit>();

                RaycastManager.Raycast(Input.GetTouch(0).position, hits, TrackableType.Planes);

                if (hits.Count == 1)
                {
                    _spawnedGameObject = Instantiate(objectPrefab, hits[0].pose.position, hits[0].pose.rotation);
                    Debug.Log("Object Created");
                    reticle.SetActive(false);
                }
            }
        }
        else if (_spawnedGameObject != null)
        {
            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
            {
                List<ARRaycastHit> hits = new List<ARRaycastHit>();

                RaycastManager.Raycast(Input.GetTouch(0).position, hits, TrackableType.Planes);

                if (hits.Count == 1)
                {
                    _spawnedGameObject.transform.position = hits[0].pose.position;
                }
            }
        }
    }

    private void ShowFrontPlanes()
    {
        if (planeManager.trackables.count < 0)
        {
            Debug.Log("Total Planes are : " + planeManager.trackables.count);
            return;
        }
        
        Debug.DrawRay(screenPoint, Camera.main.transform.forward, Color.green);
        
        List<ARRaycastHit> hits = new List<ARRaycastHit>();
        RaycastManager.Raycast(screenPoint, hits, TrackableType.Planes);
        
        foreach (var arPlane in planeManager.trackables)
        {
            if (arPlane.trackableId == hits[0].trackableId)
            {
                arPlane.gameObject.SetActive(true);
                // arPlane.GetComponent<MeshRenderer>().enabled = true;
                // arPlane.GetComponent<LineRenderer>().enabled = true;
            }
            else
            {
                arPlane.gameObject.SetActive(false);
                // arPlane.GetComponent<MeshRenderer>().enabled = false;
                // arPlane.GetComponent<LineRenderer>().enabled = false;
            }
        }
        
    }
    
}
