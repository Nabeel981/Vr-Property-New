namespace Mapbox.Examples
{
	using System.Collections;
	using System.Collections.Generic;
	using UnityEngine;

	using Mapbox.Unity.Map;
	using Mapbox.Utils;
	

	public class POIPlacementScriptExample : MonoBehaviour
	{
		public AbstractMap map;
		public static POIPlacementScriptExample instance;
       
      
        //prefab to spawn
        public GameObject prefab;
		//cache of spawned gameobjects
		private List<GameObject> _prefabInstances;
		private void OnEnable()
		{
			if (instance == null)
			{
                instance = this;
            }
           
        }
		// Use this for initialization
		void Start()
		{
		
			//add layers before initializing the map

			//	map.VectorData.SpawnPrefabByCategory(prefab, LocationPrefabCategories.ArtsAndEntertainment, 10, HandlePrefabSpawned, true, "SpawnFromScriptLayer");
			//map.Initialize(new Vector2d(31.469911575317383, 74.27169799804688), 16);

			//map.VectorData.SpawnPrefabAtGeoLocation(prefab, new Vector2d(31.469921112060547, 74.27168273925781));
			
        }

		//handle callbacks
		/// <summary>
		/// makes  a POI in the map
		/// </summary>
		//public void HandlePrefabSpawned()
		//{
  //       //   map.VectorData.SpawnPrefabAtGeoLocation(prefab, new Vector2d(31.469921112060547, 74.27168273925781 ), null, false);
  //          foreach (WayspotInformation wayspotInformation in PlayFabPlayerManager.instance.fromPlayfabWayspotsInfo.WayspotInformationList)
  //          {

                


  //            //  map.VectorData.SpawnPrefabAtGeoLocation(prefab, new Vector2d(wayspotInformation.latitude, wayspotInformation.longitude) ,null, true );


		//		map.VectorData.SpawnPrefabAtGeoLocation(prefab, new Vector2d(wayspotInformation.latitude, wayspotInformation.longitude) ,null ,  false);


				
		//	}

		//}

	}
}
