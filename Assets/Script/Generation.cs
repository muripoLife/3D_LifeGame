using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Generation : MonoBehaviour
{

	public Text generationText;         // Text用変数
	private int generation = 0;         //世代計算用変数

	void Start ()
	{
		generationText.text = "第0世代"; // 初期世代を代入して画面に表示
	}
	
	/// <summary>
	/// ライフゲームの世代更新
	/// <summary>
	public string GenerationNum()
	{
        // 世代を更新して表示
        return generationText.text = "第" + generation++ + "世代";
    }
}
