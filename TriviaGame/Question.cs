using UnityEngine;
using System.Collections;

public class Question : MonoBehaviour
{
	public string question;
	public string[] answers;
	public Transform videoScreen;
	public int correctAnswerIndex;	

	private bool movieQuestion;

	// Use this for initialization
	void Start ()
	{
		if (gameObject.GetComponent<MeshRenderer>().materials.Length == 1)
		{
			movieQuestion = true;
		}
		else
			movieQuestion = false;
	}
	
	// Update is called once per frame
	void Update ()
	{

	}

	public bool isMovieQuestion()
	{
		return movieQuestion;
	}

	public string GetQuestion()
	{
		return question;
	}

	public string[] GetAnswers()
	{
		return answers;
	}
}
