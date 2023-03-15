using TMPro;
using UnityEngine;

public class Paddle : MonoBehaviour {
  [SerializeField]
  private float speed = 0.01f;
  public Game Game = new Game();

  // Start is called before the first frame update
  void Start() {
    if (transform.CompareTag("PaddleRight")) {

    }
    else {
      GetComponent<SpriteRenderer>().color = new Color(1, 0, 1);
    }
  }

  // Update is called once per frame
  void Update() {
    if (!Game.isPlaying) {
      return;
    }
    if (transform.CompareTag("PaddleRight")) {
      GetComponent<SpriteRenderer>().color = new Color(0, 0, Mathf.Abs(Mathf.Sin(Time.time)));
      if (Input.GetKey(KeyCode.UpArrow)) {
        transform.Translate(Vector3.up * speed * Time.deltaTime);
      }
      if (Input.GetKey(KeyCode.DownArrow)) {
        transform.Translate(-Vector3.up * speed * Time.deltaTime);
      }
    }
    else if (transform.CompareTag("PaddleLeft")) {
      GetComponent<SpriteRenderer>().color = new Color(Mathf.Abs(Mathf.Sin(Time.time)), 0, Mathf.Abs(Mathf.Sin(Time.time)));
      if (Input.GetKey(KeyCode.W)) {
        transform.Translate(Vector3.up * speed * Time.deltaTime);
      }
      if (Input.GetKey(KeyCode.S)) {
        transform.Translate(-Vector3.up * speed * Time.deltaTime);
      }
    }
  }
}
