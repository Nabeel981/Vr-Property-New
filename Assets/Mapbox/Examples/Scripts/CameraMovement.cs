namespace Mapbox.Examples
{
	using UnityEngine;
	using UnityEngine.EventSystems;
	using Mapbox.Unity.Map;
	using System.IO;
	

	public class CameraMovement : MonoBehaviour
	{
		[SerializeField]
		AbstractMap _map;
		Touch touchZero;
		Touch touchOne;
		[SerializeField]
		float _panSpeed = 20f;
		public float zoomspeed;

		[SerializeField]
		float _zoomSpeed = 50f;

		[SerializeField]
		Camera _referenceCamera;

		Quaternion _originalRotation;
		Vector3 _origin;
		Vector3 _delta;
		bool _shouldDrag;
		float oldDistance;
		float newDistance;

		void HandleTouch()
		{
			float zoomFactor = 0.0f;
			//pinch to zoom. 
			switch (Input.touchCount)
			{
				case 1:
					{
						HandleMouseAndKeyBoard();
					}
					break;
				case 2:
					{

                        touchOne = Input.GetTouch(1);

                        newDistance = Vector2.Distance(touchZero.position, touchOne.position);
						ZoomMapUsingTouchOrMouse();
						// Store both touches.
						touchZero = Input.GetTouch(0);
						
						oldDistance = Vector2.Distance(touchZero.position, touchOne.position);
						// Find the position in the previous frame of each touch.
						Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
						Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

						// Find the magnitude of the vector (the distance) between the touches in each frame.
						float prevTouchDeltaMag = (touchZeroPrevPos - touchOnePrevPos).magnitude;
						float touchDeltaMag = (touchZero.position - touchOne.position).magnitude;

						// Find the difference in the distances between each frame.
						zoomFactor = 0.05f * (touchDeltaMag - prevTouchDeltaMag);

					}
					//ZoomMapUsingTouchOrMouse1(zoomFactor);
					break;
				default:
					break;
			}
		}
		public void SetZoomConstraints()
		{
            float x = _map.Zoom;
            //  float x = _map.Zoom;
            if (x > 19)
            {
                _map.UpdateMap(18);
            }
            if (x < 1)
            {
                _map.UpdateMap(2);
            }

        }
		void ZoomMapUsingTouchOrMouse()
		{

			float x = _map.Zoom;
          //  float x = _map.Zoom;
          
            if (oldDistance < newDistance)
			{if(Mathf.Abs(oldDistance - newDistance) > 1.1)
				if (x < 21.5)
				{



					_map.UpdateMap(x + 0.1f);
				}
            }

			if (oldDistance > newDistance)
			{
				if (Mathf.Abs(oldDistance - newDistance) > 1.1)
				{
                    if (x > 2)
                    {
                        _map.UpdateMap(x - 0.1f);
                    }
                }
            }

		}
		void ZoomMapUsingTouchOrMouse1212()
		{
			float x = _map.Zoom;

			if (Input.GetMouseButton(0))
			{
				_map.UpdateMap(x + 0.1f); 
			}
			if (Input.GetMouseButton(1))
			{
				_map.UpdateMap(x - 0.1f);
			}

		}


		void HandleMouseAndKeyBoard()
			{
				if (Input.GetMouseButton(0) && !EventSystem.current.IsPointerOverGameObject())
				{
					var mousePosition = Input.mousePosition;
					mousePosition.z = _referenceCamera.transform.localPosition.y;
					_delta = _referenceCamera.ScreenToWorldPoint(mousePosition) - _referenceCamera.transform.localPosition;
					_delta.y = 0f;
					if (_shouldDrag == false)
					{
						_shouldDrag = true;
						_origin = _referenceCamera.ScreenToWorldPoint(mousePosition);
					}
				}
				else
				{
					_shouldDrag = false;
				}

				if (_shouldDrag == true)
				{
					var offset = _origin - _delta;
					offset.y = transform.localPosition.y;
					transform.localPosition = offset;
				}
				else
				{
					if (EventSystem.current.IsPointerOverGameObject())
					{
						return;
					}

					var x = Input.GetAxis("Horizontal");
					var z = Input.GetAxis("Vertical");
					var y = Input.GetAxis("Mouse ScrollWheel") * _zoomSpeed;
					if (!(Mathf.Approximately(x, 0) && Mathf.Approximately(y, 0) && Mathf.Approximately(z, 0)))
					{
						transform.localPosition += transform.forward * y + (_originalRotation * new Vector3(x * _panSpeed, 0, z * _panSpeed));
						_map.UpdateMap();
					}
				}


			}

			void Awake()
			{
				_originalRotation = Quaternion.Euler(0, transform.eulerAngles.y, 0);

				if (_referenceCamera == null)
				{
					_referenceCamera = GetComponent<Camera>();
					if (_referenceCamera == null)
					{
						throw new System.Exception("You must have a reference camera assigned!");
					}
				}

				if (_map == null)
				{
					_map = FindObjectOfType<AbstractMap>();
					if (_map == null)
					{
						throw new System.Exception("You must have a reference map assigned!");
					}
				}
			}

			void LateUpdate()
			{
			//ZoomMapUsingTouchOrMouse1212();

		//	SetZoomConstraints();

          //      float x = _map.Zoom;

            //if (Input.GetMouseButton(1))
            //{
            //    Debug.Log("right click");
            //    _map.UpdateMap(x - 0.1f);
            //}
            //if (Input.GetMouseButton(0))
            //{
            //    Debug.Log("left click");
            //    _map.UpdateMap(x + 0.1f);
            //}
           
;                //ZoomMapUsingTouchOrMouse1212();

            //if (Input.touchSupported && Input.touchCount > 0)
            //{
                    HandleTouch();
				//}
				//else
				//{
				//	HandleMouseAndKeyBoard();
				//}
			}
		
	}
}