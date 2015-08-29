using UnityEngine;
using System.Collections;

public class Cell : MonoBehaviour {
	public bool Living { get; private set; } // このセルが生存状態か

	private GameObject death_cell; // 死滅時
	private GameObject living_cell; // 生存時

	void Awake()
	{
		death_cell  = transform.FindChild("DeathCell").gameObject;
		living_cell = transform.FindChild("LivingCell").gameObject;

		death_cell.SetActive (true);
		living_cell.SetActive (false);
		Living = false;
	}
	
	void Update () {
		Living = living_cell.activeSelf;
	}

	/// <summary>
	/// 誕生
	/// </summary>
	public void Birth()
	{
		death_cell.SetActive (false);
		living_cell.SetActive (true);
	}

	/// <summary>
	/// 死滅
	/// </summary>
	public void Die()
	{
		death_cell.SetActive (true);
		living_cell.SetActive (false);
	}

}
