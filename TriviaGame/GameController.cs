using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
	public int numberOfPlayers;
	public int numberOfRounds;
	public Transform questions;
	public GameObject videoScreen;
	public GameObject UICanvas;
	public Text questionText;
	public Text[] answersText;

	private int currentRound;
	private Question currentQuestion;

	private MeshRenderer screenRenderer;

	private bool questionReady;
	private bool movieStarted;
	private bool questionFinished;
	private bool gameFinished;
	private bool firstFrameDelay;

	private MovieTexture mtex;
	private AudioSource mAS;

	// Use this for initialization
	void Start ()
	{
		currentRound = 0;
		questionReady = false;
		questionFinished = false;
		gameFinished = false;
		// questions = transform.Find("Questions");
		int randomNum = Random.Range(0, questions.childCount);
		currentQuestion = questions.GetChild(randomNum).gameObject.GetComponent<Question>();
		// videoScreen = transform.Find("VideoScreen");

		videoScreen.SetActive(false);
		UICanvas.SetActive(false);

		firstFrameDelay = true;
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (!gameFinished)
		{
			// Question Has Been Finished
			if (questionFinished)
			{
				currentRound += 1;
				if (currentRound > numberOfRounds)
					gameFinished = true;
			}

			// Question Has Not Been Finished
			else
			{
				// Load a movie Question
				if (currentQuestion.isMovieQuestion() && !questionReady)
				{
					LoadMovieQuestion();

					questionReady = true;
					movieStarted = false;
				}

				// Question is Ready
				if (questionReady)
				{
					if (currentQuestion.isMovieQuestion())
					{
						// Start the video once
						if (!movieStarted && firstFrameDelay)
						{
							videoScreen.SetActive(true);
							mtex.Stop();
							mAS.Stop();
							mtex.Play();
							mAS.Play();
							mtex.Pause();
							mAS.Pause();
							firstFrameDelay = false;
						}
						else if (!movieStarted && !firstFrameDelay)
						{
							UICanvas.SetActive(false);
							videoScreen.SetActive(true);
							mtex.Stop();
							mAS.Stop();
							mtex.Play();
							mAS.Play();

							movieStarted = true;
						}

						// When the video has finished playing
						if (!UICanvas.activeInHierarchy && !mtex.isPlaying)
						{
							videoScreen.SetActive(false);
							UICanvas.SetActive(true);
							firstFrameDelay = true;
						}
					}
				}

			}
		}
		else
		{
			// Game is Over
		}
	}

	void LoadMovieQuestion()
	{
		screenRenderer = videoScreen.GetComponent<MeshRenderer>();
		screenRenderer.material = currentQuestion.GetComponent<Renderer>().material;
		videoScreen.GetComponent<AudioSource>().clip = currentQuestion.GetComponent<AudioSource>().clip;
		videoScreen.GetComponent<AudioSource>().pitch = currentQuestion.GetComponent<AudioSource>().pitch;
		
		mtex = (MovieTexture)screenRenderer.material.mainTexture;
		mAS = videoScreen.GetComponent<AudioSource>();
		mtex.Stop();
		mAS.Stop();
		
		questionText.text = currentQuestion.GetQuestion();
		
		for (int i = 0; i < currentQuestion.GetAnswers().Length; i++)
			answersText[i].text = currentQuestion.GetAnswers()[i];
	}
}
