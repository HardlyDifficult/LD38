using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetChopper : MonoBehaviour {

	// Use this for initialization
	void Start () {
    var pixelResource = Resources.Load<GameObject>("Pixel");

    var sprite = GetComponent<SpriteRenderer>().sprite;

    for(int x = 0; x < sprite.texture.width; x++)
    {
      for(int y = 0; y < sprite.texture.height; y++)
      {

        var pixel = sprite.texture.GetPixel(x, y);
        if(pixel.a == 0)
        {
          continue;
        }

        var newPixel = Instantiate(pixelResource, new Vector3(0, y, x) / (100 / 10), Quaternion.identity);
        //var texture = new Texture2D(1, 1);
        //texture.SetPixel(0, 0, pixel);
        newPixel.transform.rotation = transform.rotation;

        newPixel.GetComponent<SpriteRenderer>().color = pixel;
          
          //.sprite =
        //  Sprite.Create(texture, new Rect(0, 0, 1, 1), Vector2.zero);
      }
    }
    


    Destroy(gameObject);
	}
	
}
