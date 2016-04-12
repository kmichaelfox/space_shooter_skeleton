using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

	public GameObject[] hazards;
	public Vector3 spawnValue;
	public int hazardCount;
	public float spawnWait;
	public float startWait;
	public float waveWait;

	public GUIText scoreText;
	public GUIText restartText;
	public GUIText gameoverText;

	private bool gameover;
	private bool restart;
	private int score;

	void Start () {
		gameover = false;
		restart = false;
		restartText.text = "";
		gameoverText.text = "";
		score = 0;
		UpdateScore ();
		StartCoroutine (SpawnWaves ());
	}

	void Update () {
		if (restart) {
			if (Input.GetKeyDown (KeyCode.R)) {
				Application.LoadLevel (Application.loadedLevel);
			}
		}
	}

	IEnumerator SpawnWaves () {
		yield return new WaitForSeconds (startWait);
		while (!gameover) {
			for (int i = 0; i < hazardCount; i++) {
				GameObject hazard = hazards[Random.Range (0, hazards.Length)];
				Vector3 spawnPosition = new Vector3 (
					Random.Range (-spawnValue.x, spawnValue.x), 
					spawnValue.y, 
					spawnValue.z
				);
				Quaternion spawnRotation = Quaternion.identity;
				Instantiate (hazard, spawnPosition, spawnRotation);
				yield return new WaitForSeconds (spawnWait);
			}
			yield return new WaitForSeconds (waveWait);

			if (gameover) {
				restartText.text = "Press 'R' for Restart";
				restart = true;
				break;
			}
		} 
	}

	public void AddScore (int newScoreValue) {
		score += newScoreValue;
		UpdateScore ();
	}

	void UpdateScore () {
		scoreText.text = "Score: " + score;
	}

	public void GameOver () {
		gameoverText.text = "GameOver";
		gameover = true;
	}
}
