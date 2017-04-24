using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MiniMapController : MonoBehaviour {

	public GameObject miniMap;
	public GameObject blipPrefab;
	private List<GameObject> blips = new List<GameObject>();
  
	// Update is called once per frame
	void Update () {
		updateBlipPositions ();
	}

	public void updateBlipPositions() {
		//Remove existing blips
		foreach (GameObject obj in blips) {
			Destroy(obj);
		}
		blips = new List<GameObject>();

		foreach(TeamPlayer player in TurnController.GetAllPlayers()) {
      if(player == null)
      {
        continue;
      }

			//Calculate latitude and longitude of player
			//Latidude based on height and angle from center of planet
			//Longitude based on rotation around y-axis
			Vector3 playerPosition = player.transform.position;
			float latHypotenuse = Mathf.Abs(playerPosition.magnitude);
			float latOppSide = Mathf.Abs(playerPosition.y);
			float lat = Mathf.Rad2Deg * Mathf.Asin(latOppSide / latHypotenuse);

			if(playerPosition.y < 0) {
				lat = -lat;
			}

			Vector2 flatPlane = new Vector2(playerPosition.x, playerPosition.z);
			float lonHypotenuse = Mathf.Abs (flatPlane.magnitude);
			float lonOppSide = Mathf.Abs (flatPlane.y);
			float lon = Mathf.Rad2Deg * Mathf.Asin (lonOppSide / lonHypotenuse);

			if (flatPlane.x < 0) {
				if (flatPlane.y < 0) {
					//Use calculated result
				} else {
					//Value is negative of calculated result
					lon = -lon;
				}
			} else if (flatPlane.x > 0) {
				if (flatPlane.y < 0) {
					//Add 90 to calculated result
					lon = 180 - lon;
				} else {
					//Subtract 90 from the negative of the result
					lon = -180 + lon;
				}
			}

			//print (lon + ", " + lat);

			//GameObject blip = (GameObject)Instantiate(blipPrefab); //Causes problems with positioning
			GameObject blip = (GameObject)Instantiate(blipPrefab, blipPrefab.transform.position, blipPrefab.transform.rotation);
			blips.Add (blip);

      //blip.transform.SetParent (miniMap.transform, false); //doesn't work for some reason...
      blip.transform.parent = miniMap.transform;
      //blip.transform.SetParent(miniMap.transform, true);


      //print("Blip: " + lat + ", " + lon);

      //Create new blips for each player object, and place them in the proper position
      RawImage blipImg = blip.GetComponent<RawImage>();
      blipImg.canvasRenderer.SetColor(player.playerInfo.team.TeamColor); // Might be a better way idk
			RectTransform blipTransform = blipImg.GetComponent<RectTransform> ();
      blipTransform.parent = miniMap.transform;
      //blipTransform.SetParent(miniMap.transform, true);

			RectTransform miniMapTransform = miniMap.GetComponent<RectTransform> ();

			float latRadians = lat * Mathf.Deg2Rad;
			float lonRadians = lon * Mathf.Deg2Rad;

			//Find x and y based on projection formula
			float a = Mathf.Sqrt((1f/3f)-(Mathf.Pow((latRadians/Mathf.PI), 2)));
			float b = (3f * lonRadians) / 2;
			float x = a * b;
			float y = latRadians;

			//55 is the scale factor used when creating the original map image. The image is displayed on screen at 80% its original size.
			float xD = x * 55 * .8f;
			float yD = y * 55 * .8f;

      //Adjust position of blip.
      //blipTransform.anchoredPosition = miniMapTransform.anchoredPosition + new Vector2(xD, yD);
      blipTransform.anchoredPosition = new Vector2(xD, yD);
    }
	}
}
