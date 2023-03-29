using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using Mapbox.Map;
using Mapbox.Utils;
using Mapbox.Unity.Location;
using Mapbox.Unity.Map;
using Mapbox.CheapRulerCs;
using TMPro;
using Mapbox.Unity.Utilities;
using UnityEngine.SceneManagement;
using static Mapbox.Unity.Constants;
using UnityEngine.XR.ARFoundation;

public class NearbyPOintsDetection : MonoBehaviour
{
    public static NearbyPOintsDetection instance;
    public Dictionary<Vector2d, double> pointsAndDistance = new Dictionary<Vector2d, double>();
    public Vector2d[] nearbyLocations;
    public AbstractMap Map;
    //  public AbstractLocationProvider locationInfo;
    public DeviceLocationProvider locationInfo;
    public float distance;
    public List<int> pointsInRange = new List<int>();
    public TextMeshProUGUI output;
    public Button checkButton;
    public Vector2d fakeLocation;
    public Vector2d closestPointKey;
    private void Awake()
    {
        DontDestroyOnLoad(this);
    }



    public void CheckLocationsInGivenDisatance()
    {
        pointsInRange.Clear();

        CheapRuler cheapRuler = new CheapRuler(locationInfo.CurrentLocation.LatitudeLongitude.x, CheapRulerUnits.Meters);
        foreach (KeyValuePair<Vector2d, double> pair in pointsAndDistance)
        {


            Vector2d p = locationInfo.CurrentLocation.LatitudeLongitude;
            double distance = cheapRuler.Distance(
                new double[] { p.y, p.x },
                new double[] { pair.Key.y, pair.Key.x }
            );

            //if (distance < 10.0)
            //{
            //    pointsInRange.Add(i);
            //}

            double Bearing = cheapRuler.Bearing(
                     new double[] { p.y, p.x },
                     new double[] { pair.Key.y, pair.Key.x }
                 );

            pointsAndDistance[pair.Key] = distance;

        }

        //if (pointsInRange.Count == 0)
        //{
        //    output.text = "there are no points within 10 meters of you";
        //}
        //else
        //{
        //    output.text = ("There are " + pointsInRange.Count + " points within 10 meters of you");
        //}
    }

    public void GetClosestPoint()
    {
        CheckLocationsInGivenDisatance();
        Vector2d min = pointsAndDistance.Aggregate((l, r) => l.Value < r.Value ? l : r).Key;

        if (pointsAndDistance[min] < 4)
        {
            
        }
        else
        {
            Debug.Log("not close enough");
        }
    }



    [ContextMenu("checkonEditor")]
    public void CheckLocationsInGivenDisatanceInEditor()
    {
        pointsInRange.Clear();

        CheapRuler cheapRuler = new CheapRuler(fakeLocation.x, CheapRulerUnits.Meters);
        for (int i = 0; i < nearbyLocations.Length; i++)
        {


            Vector2d p = fakeLocation;
            double distance = cheapRuler.Distance(
                new double[] { p.y, p.x },
                new double[] { nearbyLocations[i].y, nearbyLocations[i].x }
            );
            if (distance < 10.0)
            {
                pointsInRange.Add(i);
            }

        }


        if (pointsInRange.Count == 0)
        {
            output.text = "there are no points within 10 meters of you";
        }
        else
        {
            output.text = ("There are" + pointsInRange.Count + " points within 10 meters of you");
        }
    }




    public double ToRad(double degrees)
    {
        return degrees * (Mathf.PI / 180);
    }

    public double ToDegrees(double radians)
    {
        return radians * 180 / Mathf.PI;
    }

    public double ToBearing(double radians)
    {
        // convert radians to degrees (as bearing: 0...360)
        return (ToDegrees(radians) + 360) % 360;
    }

    //public double CalculateDistanceBetweenLonLat()
    //{
    //    double deviceLat = LocationProviderFactory.Instance.DefaultLocationProvider.CurrentLocation.LatitudeLongitude.x;
    //    double deviceLon = LocationProviderFactory.Instance.DefaultLocationProvider.CurrentLocation.LatitudeLongitude.y;
    //    double rlat1 = deviceLat * Math.PI / 180.0f;//base lat
    //    double rlat2 = data.loc_lat * Math.PI / 180.0f;//target lat
    //    var theta = data.loc_long - deviceLon;
    //    double rtheta = theta * Math.PI / 180.0f;
    //    double rlattheta = (data.loc_lat - deviceLat) * Math.PI / 180.0f;
    //    double dist =
    //    Math.Sin(rlattheta / 2.0f) * Math.Sin(rlattheta / 2.0f) + Math.Cos(rlat1) *
    //    Math.Cos(rlat2) * Math.Sin(rtheta / 2.0f) * Math.Sin(rtheta / 2.0f);
    //    dist = 2.0f * Math.Atan2(Math.Sqrt(dist), Math.Sqrt(1 - dist));
    //    dist = dist * AppConstantsScript.MeterConversionVariable;
    //    dist = dist * AppConstantsScript.KiloMeterConversionVariable;
    //    return dist;
    //}



}
