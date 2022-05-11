using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainRender : MonoBehaviour
{

	public GameObject dirtPrefab;
	public GameObject grassPrefab;

	public int minX = -100;
	int maxX = 100;
	int minY = -60;

	public float[] groundPosX; //Array to store ground positions to do ground collisions later
	public float[] groundPosY;

	void Start()
	{
		RandomizeArray(perm); // Used for Perlin
		groundPosX = new float[600];
		groundPosY = new float[600];
		Generate();
	}

	private float mountainHeight(int x)
	{
		// Gaussian function to generate a mountain shape
		float y = Mathf.Exp(-x * x / 0.08f * 0.0005f) / (Mathf.Sqrt(2f * 3.1416f) * 0.012f);
		return y;
	}

	private void Generate()
	{

		float width = 1f;
		float height = 1f;
		int index = 0;
		for (int i = minX; i < maxX; i++)
		{
			
			// Get the basic mountain shape
			int columnHeight = (int)Mathf.Round(mountainHeight(i)) + 20;


			// Noise() generates Perlin noise
			columnHeight += Mathf.RoundToInt(3 * Noise((i + 400) / 20f));


			for (int j = minY; j < minY + columnHeight - 3; j++)
			{
				Instantiate(dirtPrefab, new Vector2(i * width, j * height), Quaternion.identity);
			}

			// Use Grass prefab for top 3 levels
			Instantiate(grassPrefab, new Vector2(i * width, (minY + columnHeight - 3) * height), Quaternion.identity);
			Instantiate(grassPrefab, new Vector2(i * width, (minY + columnHeight - 2) * height), Quaternion.identity);
			Instantiate(grassPrefab, new Vector2(i * width, (minY + columnHeight - 1) * height), Quaternion.identity);


			//Store the positions of the top 3 levels of ground to do ground collision
			groundPosX[index] = i * width;
			groundPosX[index+1] = i * width;
			groundPosX[index+2] = i * width;
		
			groundPosY[index] = (minY + columnHeight - 1) * height;
			groundPosY[index+1] = (minY + columnHeight - 2) * height;
			groundPosY[index+2] = (minY + columnHeight - 3) * height;		

			index +=3;
			
		}
	
	}

	////////////////////The code below is for 1D Perlin Noise//////////////////////

	public static float Noise(float x)
	{
		var X = Mathf.FloorToInt(x) & 0xff;
		x -= Mathf.Floor(x);
		var u = Fade(x);
		return Lerp(u, Grad(perm[X], x), Grad(perm[X + 1], x - 1)) * 2;
	}
	static float Fade(float t)
	{
		return t * t * t * (t * (t * 6 - 15) + 10);
	}
	static float Lerp(float t, float a, float b)
	{
		return a + t * (b - a);
	}
	static float Grad(int hash, float x)
	{
		return (hash & 1) == 0 ? x : -x;
	}
	static int[] perm = {
		151,160,137,91,90,15,
		131,13,201,95,96,53,194,233,7,225,140,36,103,30,69,142,8,99,37,240,21,10,23,
		190, 6,148,247,120,234,75,0,26,197,62,94,252,219,203,117,35,11,32,57,177,33,
		88,237,149,56,87,174,20,125,136,171,168, 68,175,74,165,71,134,139,48,27,166,
		77,146,158,231,83,111,229,122,60,211,133,230,220,105,92,41,55,46,245,40,244,
		102,143,54, 65,25,63,161, 1,216,80,73,209,76,132,187,208, 89,18,169,200,196,
		135,130,116,188,159,86,164,100,109,198,173,186, 3,64,52,217,226,250,124,123,
		5,202,38,147,118,126,255,82,85,212,207,206,59,227,47,16,58,17,182,189,28,42,
		223,183,170,213,119,248,152, 2,44,154,163, 70,221,153,101,155,167, 43,172,9,
		129,22,39,253, 19,98,108,110,79,113,224,232,178,185, 112,104,218,246,97,228,
		251,34,242,193,238,210,144,12,191,179,162,241, 81,51,145,235,249,14,239,107,
		49,192,214, 31,181,199,106,157,184, 84,204,176,115,121,50,45,127, 4,150,254,
		138,236,205,93,222,114,67,29,24,72,243,141,128,195,78,66,215,61,156,180,
		151
	};

	static void RandomizeArray(int[] arr)
	{
		for (var i = 255; i > 0; i--)
		{
			var r = Random.Range(0, i);
			var tmp = arr[i];
			arr[i] = arr[r];
			arr[r] = tmp;
		}
	}

}
