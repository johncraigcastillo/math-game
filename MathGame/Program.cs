using Spectre.Console;

var myGame = new Game();
myGame.SetUserName();
myGame.SelectGameOption();


class Game
{
    public int[]? History { get; set; }

    private string? PlayerName { get; set; }

    private string? GameOption { get; set; }

    public void SetUserName()
    {
        PlayerName = AnsiConsole.Ask<string>("What's your [bold maroon]name[/]?");
        AnsiConsole.MarkupInterpolated($"Hi [bold maroon]{PlayerName}[/], This is a math game.\n");
    }

    public void SelectGameOption()
    {
        GameOption = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("[bold maroon]Select an option:[/]")
                .PageSize(5)
                .HighlightStyle("bold maroon")
                .AddChoices(new[]
                {
                    "Addition", "Subtraction", "Multiplication", "Division", "Quit"
                }));
        NavigateToOption();
    }

    private void NavigateToOption()
    {
        switch (GameOption)
        {
            case "Addition":
                AdditionGame();
                break;
            case "Subtraction":
                // Todo: Implement SubtractionGame
                // SubtractionGame();
                break;
            case "Multiplication":
                // Todo: Implement MultiplicationGame
                // MultiplicationGame();
                break;
            case "Division":
                // Todo: Implement DivisionGame
                // DivisionGame();
                break;
            case "Quit":
                Quit();
                break;
        }
    }

    private static void Quit()
    {
        AnsiConsole.MarkupLine("[bold maroon]Goodbye[/]");
        Environment.Exit(1);
    }

    private static void AdditionGame()
    {
        Console.WriteLine("Addition Selected!");
        var num1 = new Random().Next(0, 100);
        var num2 = new Random().Next(0, 100);
        var userAnswer = AnsiConsole.Ask<int>($"What is {num1} + {num2}?:");
        var answer = num1 + num2;
        EvaluateAnswer(userAnswer: userAnswer, answer: answer);
    }

    private static void EvaluateAnswer(int userAnswer, int answer)
    {
        if (userAnswer == answer)
        {
            AnsiConsole.MarkupLine("[bold green]Correct![/]");
        }
        else
        {
            AnsiConsole.MarkupLine("[bold red]Incorrect![/]");
            AnsiConsole.MarkupLine($"The correct answer is [bold maroon]{answer}[/]");
        }
    }
}