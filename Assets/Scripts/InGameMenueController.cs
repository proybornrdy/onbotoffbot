using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InGameMenueController : MonoBehaviour {

	public Vector2 top_left;
	public Vector2 top_right;

	public Vector2 bottom_left;
	public Vector2 bottom_right;

	public GameObject left_pointer;
	public GameObject right_pointer;

    public Button btnToMenu;
    public Button reset;

    private void Start()
    {
        btnToMenu.onClick.AddListener(delegate () { SceneManager.LoadScene("MainMenu"); });
        reset.onClick.AddListener(delegate () {
            LevelController.ResetScene();
            LevelController.InMenue = false;
            });
        }

    // Update is called once per frame
        void Update () {
		if (Input.GetAxisRaw(PlayerInputTranslator.GetVerticalAxis(Player.ON)) > .5||
			Input.GetAxisRaw(PlayerInputTranslator.GetVerticalAxis(Player.OFF)) > .5)
        {
            reset.Select();
            //left_pointer.transform.localPosition = new Vector3(top_left.x, top_left.y, left_pointer.transform.localPosition.z);
            //right_pointer.transform.localPosition = new Vector3(top_right.x, top_right.y, left_pointer.transform.localPosition.z); ;
        }
		if (Input.GetAxisRaw(PlayerInputTranslator.GetVerticalAxis(Player.ON)) < -.5 ||
			Input.GetAxisRaw(PlayerInputTranslator.GetVerticalAxis(Player.OFF)) < -.5)
        {
            btnToMenu.Select();
            //left_pointer.transform.localPosition = new Vector3(bottom_left.x, bottom_left.y, left_pointer.transform.localPosition.z);
			//right_pointer.transform.localPosition = new Vector3(bottom_right.x, bottom_right.y, left_pointer.transform.localPosition.z);
		}



		//if (Input.GetButton(PlayerInputTranslator.GetJump(Player.ON)) ||
		//	Input.GetButton(PlayerInputTranslator.GetJump(Player.OFF)))
		//{
		//	if (left_pointer.transform.localPosition.y == top_left.y)
		//	{
		//		SceneManager.LoadScene("MainMenue");
		//	}
		//	else
		//	{
		//		LevelController.ResetScene();
		//		LevelController.InMenue = false;
		//	}
		//}
	}
}
