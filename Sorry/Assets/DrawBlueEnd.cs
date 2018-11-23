//AFSHIN MAHINI 2013 - 2014

using UnityEngine;
using System.Collections;

public class DrawBlueEnd : MonoBehaviour {

	// Use this for initialization
	
	LineRenderer lr;
	int vertices ;
	float posXTranslation, posYTranslation ;
	
	void Start () {
		
		lr = GetComponent<LineRenderer>();
		vertices = 20;
		
		
		lr.SetColors(Color.blue , Color.blue);
		lr.SetWidth(2.0f, 2.0f);
		lr.SetVertexCount(vertices + 1);
		lr.useWorldSpace = true;
		lr.castShadows = false;
		lr.receiveShadows = false;
		
		
		posXTranslation = -72.5f;
		posYTranslation = -68.43626f;
	
	}
	
	// Update is called once per frame
	void Update () {
		
		DrawCircle ( lr);	
	}
	
	void DrawCircle(LineRenderer lr)
    {
		float radCircle = 12.0f;
		float delTheta = 2.0f * Mathf.PI  / 20.0f;
		float theta = 0.0f;
		
	    
		for(int i = 0; i < vertices + 1; i++)
		{
			Vector3 pos = new Vector3(posXTranslation + (Mathf.Cos(theta) * radCircle), posYTranslation + (Mathf.Sin(theta) * radCircle), 0) ;
			lr.SetPosition(i, pos);
			
			theta += delTheta;
		}
    }
}
