using UnityEngine;
//using UnityEngine.Transform;
using System.Collections;
using UnityEngine.UI;


public class CameraMove : MonoBehaviour
{
    private int rotfragNum = 0;
//    public Object leftButton;
//    public Object rightButton;

    void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		// カメラワークの回転
        RotationView();
	}

	/// <summary>
	/// 視点回転
	/// <summary>
	/// <returns>RotationView.</returns>
	private void RotationView()
	{
        // ←ボタン押下
		if(Input.GetKeyDown(KeyCode.RightArrow))
		{
			rotfragNum++;
			transform.Rotate(new Vector3(0f, 90f, 0f));
		}

		if(Input.GetKeyDown(KeyCode.LeftArrow))
		{
			rotfragNum--;
			transform.Rotate(new Vector3(0f, -90f, 0f));
		}

/////////////////////////////////////////////////////////
/*
        // ←ボタン押下
        if(Input.GetKeyDown(KeyCode.RightArrow))
        {
            rotfragNum++;
            transform.Rotate(new Vector3(0f, 90f, 0f));
        }

        if(Input.GetKeyDown(KeyCode.LeftArrow))
        {
            rotfragNum--;
            transform.Rotate(new Vector3(0f, -90f, 0f));
        }
*/
/////////////////////////////////////////////////////////

        if(rotfragNum <= -2)
        {
            rotfragNum = Mathf.Abs(rotfragNum);
        }

		if(rotfragNum >= 3)
		{
			rotfragNum = rotfragNum - 4;
		}

		switch (rotfragNum)
		{
			case 0:
				transform.localPosition = new Vector3(0f, 25f, -100f);
				break;
			case 1:
				transform.localPosition = new Vector3(-100f, 25f, 0f);
				break;
			case 2:
				transform.localPosition = new Vector3(0f, 25f, 100f);
				break;
			case -1:
				transform.localPosition = new Vector3(100f, 25f, 0f);
				break;
			default:
				Debug.Log("Default case");
				break;
		}
	}

}