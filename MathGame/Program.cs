using MyGameSpace;
using Spectre.Console;


var myGame = new Game();
myGame.SetUserName();

while (true)
{
    myGame.SelectGameOption();
}

namespace MyGameSpace
{
    public class GameData
    {
        public string? Question { get; init; }
        public bool IsCorrect { get; init; }
        public int UserAnswer { get; set; }
        public int CorrectAnswer { get; set; }
    }


    public class Game
    {
        private string? PlayerName { get; set; }
        private string? _gameOption;
        private int _num1;
        private int _num2;
        private int _answer;
        private int _userAnswer;
        private const int QuestionCount = 5;
        private int _score;
        private readonly List<GameData>? _history = new List<GameData>();


        private void ShowHistory()
        {
            if (_history == null)
            {
                AnsiConsole.MarkupLine("[blue bold]No history found![/]");
                return;
            }

            var table = new Table();
            table.Border(TableBorder.Rounded);

            table.AddColumn("Question");
            table.AddColumn("Your Answer");
            table.AddColumn("Correct Answer");
            table.AddColumn("Result");

            foreach (var q in _history)
            {
                if (q.Question != null)
                    table.AddRow(q.Question, Convert.ToString(q.UserAnswer), Convert.ToString(q.CorrectAnswer),
                        q.IsCorrect ? "[green]Correct[/]" : "[red]Incorrect[/]");
            }

            AnsiConsole.Write(table);
            ShowScore();
        }

        private void SetRandomNums(bool isDivision = false)
        {
            var rand = new Random();
            if (isDivision)
            {
                _num1 = rand.Next(1, 100);
                _num2 = rand.Next(1, 100);
                while (_num1 % _num2 != 0)
                {
                    _num1 = rand.Next(1, 100);
                    _num2 = rand.Next(1, 100);
                }
            }
            else
            {
                _num1 = rand.Next(1, 100);
                _num2 = rand.Next(1, 100);
            }
        }

        public void SetUserName()
        {
            PlayerName = AnsiConsole.Ask<string>("What's your [bold blue]name[/]?\n");
            AnsiConsole.MarkupInterpolated($"Hi [bold blue]{PlayerName}[/], This is a math game.\n");
        }

        public void SelectGameOption()
        {
            _gameOption = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("[bold blue]What would you like to do?[/]")
                    .PageSize(6)
                    .HighlightStyle("bold blue")
                    .AddChoices(new[]
                    {
                        "Addition", "Subtraction", "Multiplication", "Division", "Show History", "Quit"
                    }));
            NavigateToOption();
        }

        private void NavigateToOption()
        {
            switch (_gameOption)
            {
                case "Addition":
                    AdditionGame();
                    break;
                case "Subtraction":
                    SubtractionGame();
                    break;
                case "Multiplication":
                    MultiplicationGame();
                    break;
                case "Division":
                    DivisionGame();
                    break;
                case "Show History":
                    ShowHistory();
                    break;
                case "Quit":
                    Quit();
                    break;
            }
        }


        private void AdditionGame()
        {
            AnsiConsole.MarkupLine("[bold blue]Addition Selected![/]");
            for (var i = 0; i < QuestionCount; i++)
            {
                SetRandomNums();
                var q = $"What is {_num1} + {_num2}?";
                _userAnswer = AnsiConsole.Ask<int>($"What is [blue bold]{_num1} + {_num2}[/]?:");
                _answer = _num1 + _num2;
                var result = EvaluateAnswer(userAnswer: _userAnswer, answer: _answer);
                _history?.Add(new GameData
                    { Question = q, UserAnswer = _userAnswer, CorrectAnswer = _answer, IsCorrect = result });
            }
        }

        private void SubtractionGame()
        {
            AnsiConsole.MarkupLine("[bold blue]Subtraction Selected![/]");
            for (var i = 0; i < QuestionCount; i++)
            {
                SetRandomNums();
                var q = $"What is {_num1} - {_num2}?";
                _userAnswer = AnsiConsole.Ask<int>($"What is [blue bold]{_num1} - {_num2}[/]?:");
                _answer = _num1 - _num2;
                var result = EvaluateAnswer(userAnswer: _userAnswer, answer: _answer);
                _history?.Add(new GameData
                    { Question = q, UserAnswer = _userAnswer, CorrectAnswer = _answer, IsCorrect = result });
            }
        }

        private void MultiplicationGame()
        {
            AnsiConsole.MarkupLine("[bold blue]Multiplication Selected![/]");
            for (var i = 0; i < QuestionCount; i++)
            {
                SetRandomNums();
                var q = $"What is {_num1} * {_num2}?";
                _userAnswer = AnsiConsole.Ask<int>($"What is [bold blue]{_num1} * {_num2}[/]?:");
                _answer = _num1 * _num2;
                var result = EvaluateAnswer(userAnswer: _userAnswer, answer: _answer);
                _history?.Add(new GameData
                    { Question = q, UserAnswer = _userAnswer, CorrectAnswer = _answer, IsCorrect = result });
            }
        }

        private void DivisionGame()
        {
            Console.WriteLine("Division Selected!");
            for (var i = 0; i < QuestionCount; i++)
            {
                SetRandomNums(isDivision: true);
                var q = $"What is {_num1} / {_num2}?";
                _userAnswer = AnsiConsole.Ask<int>($"What is [bold blue]{_num1} / {_num2}[/]?:");
                _answer = _num1 / _num2;
                var result = EvaluateAnswer(userAnswer: _userAnswer, answer: _answer);
                _history?.Add(new GameData
                    { Question = q, UserAnswer = _userAnswer, CorrectAnswer = _answer, IsCorrect = result });
            }
        }

        private static void Quit()
        {
            AnsiConsole.MarkupLine("[bold blue]Goodbye![/]");
            Environment.Exit(1);
        }


        private bool EvaluateAnswer(int userAnswer, int answer)
        {
            if (userAnswer == answer)
            {
                AnsiConsole.MarkupLine("[bold green]Correct![/]");
                _score++;
                return true;
            }
            else
            {
                AnsiConsole.MarkupLine("[bold red]Incorrect![/]");
                AnsiConsole.MarkupLine($"[bold blue]The correct answer is: {answer}[/]");
                return false;
            }
        }

        public void ShowScore()
        {
            AnsiConsole.MarkupLine($"[bold blue]Your current score is: {_score}[/]\n");
        }
    }
}