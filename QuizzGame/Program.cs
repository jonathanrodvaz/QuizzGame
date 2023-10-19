using QuizzGame;
using static System.Net.Mime.MediaTypeNames;

Console.WriteLine("Quiz Game...");


var questions = new List<Question>();
var answers = new List<Answer>();
var scores = new Dictionary<string, int>();

SeedQuestionsAndOptions();

StartGame();

void StartGame()
{
    Console.WriteLine("Bienvenido! Como te llamas...?");
    Console.WriteLine("Introduce tu nombre: ");
    var userName = Console.ReadLine();
    Console.WriteLine($"Bienvenido {userName}! Let's go!");
    foreach (var item in questions)
    {
        Console.WriteLine(item.QuestionText);
        Console.WriteLine("Please, enter 1, 2, 3, or 4");
        
        foreach (var option in item.Options)
        {
            Console.WriteLine($"{option.Id}. {option.Text}");
        }

        var answer = GetSelectedAnswer();
        AddAnswerToList(answer, item);
    }
    int score = GetScore();
    Console.WriteLine($"Nice try {userName}! You answered well {score} questions...");
    UpdateScore(userName, score);
    ShowScores();

    answers = new List<Answer>();
    Console.WriteLine("Do you want to play again?");
    Console.WriteLine("Enter Y to play again or any other key to stop...");

    var playAgain = Console.ReadLine();
    if (playAgain?.ToLower().Trim() == "Y")
        StartGame();
    else
        Console.WriteLine("See you soon!");
   
}

string GetSelectedAnswer()
{
    var answer = Console.ReadLine();
    if (answer != null && (answer == "1") || (answer == "2") || (answer == "3") || (answer == "4"))
        return answer;
    else
    {
        Console.WriteLine("That is not a valid option, please try again...");
        answer = GetSelectedAnswer();
    }
    return answer;


}

void AddAnswerToList(string answer, Question question)
{
    answers.Add(new Answer
    {
        QuestionId = question.Id,
        SelectedOption = GetSelectedOption(answer, question)
    }); 
}

Option GetSelectedOption(string answer, Question question)
{
    var selectedOption = new Option();
    foreach (var item in question.Options)
    {

        if (item.Id == int.Parse(answer))
            selectedOption = item;
        
    }
    return selectedOption;
}
void SeedQuestionsAndOptions() {

    questions.Add(new Question
    {
        Id = 1,
        QuestionText = "What is the biggest country on earth?",
        Options = new List<Option>()
        {
            new Option  { Id = 1, Text= "Australia" },
            new Option  { Id = 2, Text= "China" },
            new Option  { Id = 3, Text= "Russian", IsValid = true },
            new Option  { Id = 4, Text= "Canada" }
        }
    });
    questions.Add(new Question
    {
        Id = 2,
        QuestionText = "What is the country with the greatest population?",
        Options = new List<Option>()
        { 
            new Option { Id = 1, Text= "India" },
            new Option { Id = 2, Text= "China", IsValid = true },
            new Option { Id = 3, Text= "USA" },
            new Option { Id = 4, Text= "Indonesia" },
        }
    });
    questions.Add(new Question
    {
        Id = 3,
        QuestionText = "What was the less corrupt country in the world in 2021",
        Options = new List<Option>()
        {
            new Option { Id = 1, Text= "Finland" },
            new Option { Id = 2, Text= "New Zealand"},
            new Option { Id = 3, Text= "Denmark", IsValid = true },
            new Option { Id = 4, Text= "Norway" },
        }
    });
    questions.Add(new Question
    {
        Id = 4,
        QuestionText = "What was the best country for quality of life in 2021?",
        Options = new List<Option>()
        {
            new Option { Id = 1, Text= "Norway", IsValid = true },
            new Option { Id = 2, Text= "Belgium" },
            new Option { Id = 3, Text= "Sweden" },
            new Option { Id = 4, Text= "Switzerland" },
        }
    });
}

int GetScore()
{
    int score = 0;
    foreach (var item in answers)
    {
        if (item.SelectedOption.IsValid)
            score++;
    }
    return score;
}

void UpdateScore(string userName, int score)
{
    bool updated = false; 

    foreach (var item in scores)
    {
        
        if(item.Key == userName)
        {
            scores[item.Key] = score;
            updated = true;
        }

        if (!updated)
            scores.Add(userName, score);
    }

}

void ShowScores()
{
    Console.WriteLine("Scores: ");
    foreach ( var item in scores)
    {
        Console.WriteLine($"{item.Key}. score: {item.Value}");
    }
}