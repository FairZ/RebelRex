using UnityEngine;
using UnityEngine.SceneManagement;

//simple script to allow switching between scenes via buttons
public class MenuController : MonoBehaviour
{
	private void Start()
	{
		SceneManager.UnloadScene ("Game");
	}

	public void OnControlsClicked()
	{	
		SceneManager.LoadScene("Options_Credits");
	}

  public void OnStartClicked()
  {
    SceneManager.LoadScene("Game");
  }
		
  public void OnBackClicked()
  {
		SceneManager.LoadScene("Menu");
  }

	public void OnQuitClicked()
	{
		Application.Quit ();
	}
}
