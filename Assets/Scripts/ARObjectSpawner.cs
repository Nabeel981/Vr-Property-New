using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using Mapbox.Map;
using Mapbox.Utils;
using Mapbox.Unity.Location;
using Mapbox.Unity.Map;
using Mapbox.CheapRulerCs;
using Mapbox.Unity.Utilities;
public class ARObjectSpawner : MonoBehaviour
{
    public static ARObjectSpawner instance;
    public ARRaycastManager aRRaycastManager;
    public DeviceLocationProvider deviceLocationProvider;
    public List<ARRaycastHit> m_hits = new List<ARRaycastHit>();
    public GameObject spawnableObject;
    public Camera arCamera;
    public ARPlaneManager planeManager;


    GameObject spawnedObject;
    private void OnEnable()
    {
        instance = this;
    }
    private void OnDisable()
    {
        Destroy(this);
    }

    void Start()
    {
        spawnedObject = null;
    }

    // Update is called once per frame
    void Update()
    {
        CheckInput();
    }

    public void CheckInput()
    {

        if (Input.touchCount !=0 )
        {


            Vector2d vectorToSpawnat = Conversions.GeoToWorldPosition(new Vector2d(NearbyPOintsDetection.instance.nearbyLocations[0].x, NearbyPOintsDetection.instance.nearbyLocations[0].x), new Vector2d(deviceLocationProvider.CurrentLocation.LatitudeLongitude.x, deviceLocationProvider.CurrentLocation.LatitudeLongitude.y), 1);
          spawnedObject =   GameObject.Instantiate(spawnableObject, new Vector3((float)vectorToSpawnat.x, (float)vectorToSpawnat.y, 1f), Quaternion.identity);
            Debug.Log(spawnedObject.transform.position + " the posiotion of the object") ;
            Debug.Log(arCamera.transform.position + " my position in unity") ;
        }

    }
    public void SpawnPrefab()
    {
        
        aRRaycastManager.Raycast(arCamera.transform.position , List<ARRaycastHit> hitresults){

        }


        foreach(var plane in planeManager.trackables)
        {
         
        }
    }



}
