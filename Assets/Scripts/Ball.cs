using TMPro;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Ball : MonoBehaviour {
  public GameObject uiStart;
  public GameObject uiGameOver;
  public GameObject expoPrefab;
  public TextMeshProUGUI txtScoreLeft;
  public TextMeshProUGUI txtScoreRight;

  private int scoreLeft;
  private int scoreRight;

  public float speed = 4;
  public Vector2 dir;
  private Vector2 origPos;
  private AudioSource audioSrc;

  public Game Game;

  private void Awake() {
    uiStart.SetActive(true);
    uiGameOver.SetActive(false);
    Game = new Game();
  }

  void Start() {
    scoreLeft = 0;
    scoreRight = 0;
    txtScoreLeft.text = "0";
    txtScoreRight.text = "0";
    origPos = transform.position;
    audioSrc = GetComponent<AudioSource>();
    audioSrc.clip = Resources.Load<AudioClip>("Ping");
    Game.isPlaying = false;
  }

  public void Play() {
    transform.position = origPos;
    Game.isPlaying = true;
    uiStart.SetActive(false);
    float result = Random.Range(0f, 1f);
    if (result < 0.5) {
      dir = Vector2.left;
    }
    else {
      dir = Vector2.right;
    }
    result = Random.Range(0f, 1f);
    if (result < 0.5) {
      dir.y = 1;
    }
    else {
      dir.y = -1;
    }
  }

  public void Restart() {
    scoreLeft = scoreRight = 0;
    txtScoreLeft.text = "0";
    txtScoreRight.text = "0";
    uiGameOver.SetActive(false);
    Play();
  }

  void Update() {
    if (Game.isPlaying) {
      transform.Translate(dir * speed * Time.deltaTime);
    }
  }

  void OnCollisionEnter2D(Collision2D c) {
    if (c.gameObject.transform.tag.StartsWith("Paddle")) {
      var expo = Instantiate(expoPrefab, c.contacts[0].point, Quaternion.identity);
      Destroy(expo, 1.0f);
      audioSrc.Play();
      dir.x *= -1;
    }
    else if (c.gameObject.CompareTag("TopBottom Boundary")) {
      dir.y *= -1;
    }
    else if (c.gameObject.CompareTag("Left Boundary")) {
      scoreRight++;
      txtScoreRight.text = scoreRight.ToString();
      if (scoreRight >= 2) {
        uiGameOver.SetActive(true);
        Game.isPlaying = false;
      }
      else {
        transform.position = origPos;
      }
    }
    else if (c.gameObject.CompareTag("Right Boundary")) {
      scoreLeft++;
      txtScoreLeft.text = scoreLeft.ToString();

      if (scoreLeft >= 2) {
        Game.isPlaying = false;
        uiGameOver.SetActive(true);
      }
      else {
        transform.position = origPos;
      }
    }
  }
}
