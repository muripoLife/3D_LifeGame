using UnityEngine;
using System.Collections;

public class LifeGame : MonoBehaviour
{
	private const float CELL_SIZE = 1f; // セルのサイズ
	public int gridSize = 100;          // グリッドの一辺のセル数
	public GameObject cellPrefab;       // セルのプレハブ
	private Cell[,] cells;              // グリッド状のセル

	void Start ()
	{
		// グリッド状にセルを作成
		cells = new Cell[gridSize, gridSize];
		for(int x = 0; x < gridSize; x++)
		{
			for(int z = 0; z < gridSize; z++)
			{
				// セルを作成
				GameObject obj = Instantiate (cellPrefab) as GameObject;
				obj.transform.SetParent(transform);

				// 位置を設定
				float xPos = (x - gridSize * 0.5f) * CELL_SIZE;
				float zPos = (z - gridSize * 0.5f) * CELL_SIZE;
				obj.transform.localPosition = new Vector3 (xPos, 0f, zPos);

				// Cellをセット
				cells[x,z]=obj.GetComponent<Cell>();
			}
		}
	}
	
	void Update ()
	{
		// クリックしたセルの生存/死滅を切り替える
		if(Input.GetMouseButton(0))
		{
			ClickLifeOrDead ();
		}

		// 「S」キーで開始
		if(Input.GetKeyDown(KeyCode.S))
		{
			StartCoroutine (LifeGameCoroutine ());
		}

		// 「E」キーで開始
		if(Input.GetKeyDown(KeyCode.E))
		{
			StopAllCoroutines ();
		}

		// ランダムに点を打つ
		if(Input.GetKeyDown(KeyCode.R))
		{
			StartCoroutine (RandomCell ());
		}

	}

	/// <summary>
	/// ライフゲームの更新コルーチン
	/// <summary>
	/// <returns>The game coroutine.</returns>
	private IEnumerator LifeGameCoroutine()
	{
		while(true)
		{
			// 全セルを更新
			for(int x = 0; x < gridSize; x++)
			{
				for(int z = 0; z < gridSize; z++)
				{
					UpdateCell(x,z);
				}
			}

			// ちょっと待つ
			yield return new WaitForSeconds(0.1f);

		}
	}

	/// <summary>
	/// クリックしたセルの生存/死滅を切り替える
	/// </summary>
	public void ClickLifeOrDead ()
	{
		// クリックしたセルの生存/死滅を切り替える
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit = new RaycastHit();

		if(Physics.Raycast(ray, out hit))
		{
			Cell cell = hit.collider.gameObject.transform.parent.GetComponent<Cell>();
			if(cell.Living)
			{
				cell.Die();
			}else{
				cell.Birth();
			}
		}
	}


	/// <summary>
	/// セルの状態を更新
	/// </summary>
	/// <param name="x">The x coordinate.</param>
	/// <param name="z">The z coordinate.</param>
	private void UpdateCell(int cellX, int cellZ)
	{
		// 周囲の生存セル数
		int count = 0;
		for (int x = cellX - 1; x <= cellX + 1; x++) {
			for (int z = cellZ - 1; z <= cellZ + 1; z++) {
				if (x == cellX && z == cellZ) {
					continue;
				}

				if (cells [(x + gridSize) % gridSize, (z + gridSize) % gridSize].Living) {
					count++;
				}
			}
		}

		// 誕生/死滅
		Cell cell = cells [cellX, cellZ];
		if (cell.Living) {
			// 周囲の生存セルが１以下、または４以上のとき死滅
			if (count <= 1 || count >= 4) {
				cell.Die ();
			}
		} else {
			// 周囲の生存セルが３つのとき誕生
			if (count == 3) {
				cell.Birth ();
			}
		}
	}

	/// <summary>
	/// ランダムにセルを配置する
	/// </summary>
	/// <param name="x">The x coordinate.</param>
	/// <param name="z">The z coordinate.</param>
	private IEnumerator RandomCell()
	{
		// セルをランダムに配置
		for(int i = 0; i < Random.Range(500, 800); i++)
		{
			int xRam = Random.Range(0, 100);
			int zRam = Random.Range(0, 100);
			Cell cell = cells [xRam, zRam];
			if(cell.Living)
			{
				cell.Die();
			}else{
				cell.Birth();
			}
			// ちょっと待つ
			yield return new WaitForSeconds(0.0001f);
		}
	}

}
