using UnityEngine;
using TMPro; // Import TextMeshPro namespace
using UnityEngine.UI;

public class mcqmanager : MonoBehaviour
{
    // UI Elements
    public TextMeshProUGUI questionText; // TextMeshPro for question
    public Button[] optionButtons; // Four buttons for options
    public TextMeshProUGUI feedbackText; // TextMeshPro for feedback
    public Button nextButton;
    public Button backButton;

    // Questions and Answers
    private string[] questions = new string[]
    {
        "1. Which layer of the OSI model handles routing?",
        "2. The physical layer is responsible for?",
        "3. Which OSI layer ensures reliable data transfer?",
        "4. Encryption is performed at which layer?",
        "5. Which layer provides end-to-end communication?"
    };

    private string[,] options = new string[5, 4]
    {
        { "Application Layer", "Transport Layer", "Network Layer", "Data Link Layer" },
        { "Routing", "Error Detection", "Data Encoding", "Logical Addressing" },
        { "Physical Layer", "Data Link Layer", "Network Layer", "Transport Layer" },
        { "Transport Layer", "Session Layer", "Presentation Layer", "Network Layer" },
        { "Physical Layer", "Application Layer", "Network Layer", "Transport Layer" }
    };

    private int[] correctAnswers = new int[] { 2, 2, 3, 2, 3 }; // Correct option indices (0-based)

    private int currentQuestionIndex = 0;

    void Start()
    {
        // Initialize the UI with the first question
        ShowQuestion();
        feedbackText.text = "";

        // Set navigation buttons
        nextButton.onClick.AddListener(NextQuestion);
        backButton.onClick.AddListener(PreviousQuestion);

        // Set option buttons
        for (int i = 0; i < optionButtons.Length; i++)
        {
            int index = i; // Capture loop variable
            optionButtons[i].onClick.AddListener(() => CheckAnswer(index));
        }
    }

    public void ShowQuestion()
    {
        // Display the current question and options
        questionText.text = questions[currentQuestionIndex];
        for (int i = 0; i < optionButtons.Length; i++)
        {
            // Use TextMeshPro for option buttons
            optionButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = options[currentQuestionIndex, i];
        }

        // Clear feedback
        feedbackText.text = "";
    }

    public void CheckAnswer(int selectedOption)
    {
        if (selectedOption == correctAnswers[currentQuestionIndex])
        {
            feedbackText.text = "Correct Answer!";
            feedbackText.color = Color.green;
        }
        else
        {
            feedbackText.text = "Try Again!";
            feedbackText.color = Color.red;
        }
    }

    public void NextQuestion()
    {
        if (currentQuestionIndex < questions.Length - 1)
        {
            currentQuestionIndex++;
            ShowQuestion();
        }
    }

    public void PreviousQuestion()
    {
        if (currentQuestionIndex > 0)
        {
            currentQuestionIndex--;
            ShowQuestion();
        }
    }
}
