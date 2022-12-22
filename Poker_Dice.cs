// Practice of a Poker Dice

int dice1 = 0, dice2 = 0, dice3 = 0, dice4 = 0, dice5 = 0;
bool fixed1 = false, fixed2 = true, fixed3 = false, fixed4 = false, fixed5 = false;

string input;
string hand = "";
int pointsPlayer1 = 0;
int pointsPlayer2 = 0;

void RollDice()
{
    if (!(fixed1)) { dice1 = Random.Shared.Next(1, 7); }
    if (!(fixed2)) { dice2 = Random.Shared.Next(1, 7); }
    if (!(fixed3)) { dice3 = Random.Shared.Next(1, 7); }
    if (!(fixed4)) { dice4 = Random.Shared.Next(1, 7); }
    if (!(fixed5)) { dice5 = Random.Shared.Next(1, 7); }
}

void PrintDice(int count)
{
    System.Console.WriteLine("");
    System.Console.WriteLine($"Dice roll #{count} (out of three): {dice1}, {dice2}, {dice3}, {dice4}, {dice5}");
}

void FixDice()
{
    do
    {
        System.Console.WriteLine();
        System.Console.Write("Which dice do you want to fix/unfix? (1-5, or 'r' to roll the dice): ");
        input = Console.ReadLine()!;

        switch (input)
        {
            case "1": fixed1 = !fixed1; break;
            case "2": fixed2 = !fixed2; break;
            case "3": fixed3 = !fixed3; break;
            case "4": fixed4 = !fixed4; break;
            case "5": fixed5 = !fixed5; break;
            case "r": break;
            default: System.Console.WriteLine("WHAT?"); break;
        }
        if (input != "r")
        {
            System.Console.Write("Fixed: ");
            if (fixed1) { System.Console.Write("1 "); }
            if (fixed2) { System.Console.Write("2 "); }
            if (fixed3) { System.Console.Write("3 "); }
            if (fixed4) { System.Console.Write("4 "); }
            if (fixed5) { System.Console.Write("5 "); }
        }
    }
    while (input != "r");
}

void SortDice()
{
    bool somethingMoved = true;
    do
    {
        somethingMoved = false;
        if (dice1 > dice2)
        {
            (dice1, dice2) = (dice2, dice1);
            somethingMoved = true;
        }
        if (dice2 > dice3)
        {
            (dice2, dice3) = (dice3, dice2);
            somethingMoved = true;
        }
        if (dice3 > dice4)
        {
            (dice3, dice4) = (dice4, dice3);
            somethingMoved = true;
        }
        if (dice4 > dice5)
        {
            (dice4, dice5) = (dice5, dice4);
            somethingMoved = true;
        }
    }
    while (somethingMoved);
}

int AnalyzeResult()
{
    if (dice1 == dice5)
    {
        hand = "Five of a kind";
        return 8;
    }
    else if (dice1 == dice4 || dice2 == dice5)
    {
        hand = "Four of a kind";
        return 7;
    }

    else if (dice1 == dice3 || dice2 == dice4 || dice3 == dice5)
    {
        if (dice1 == dice3)
        {
            if (dice4 == dice5)
            {
                hand = "Full House";
                return 6;
            }
            else
            {
                hand = "Three of a kind";
                return 3;
            }
        }
        else if (dice3 == dice5)
        {
            if (dice1 == dice2)
            {
                hand = "Full House";
                return 6;
            }
            else
            {
                hand = "Three of a kind";
                return 3;
            }
        }
        else
        {
            hand = "Three of a kind";
            return 3;
        }
    }
    else if (dice1 == 1 && dice2 == 2 && dice3 == 3 && dice4 == 4 && dice5 == 5)
    {
        hand = "Small Straight";
        return 4;
    }
    else if (dice1 == 2 && dice2 == 3 && dice3 == 4 && dice4 == 5 && dice5 == 6)
    {
        hand = "Big Straight";
        return 5;
    }

    else if (dice1 == dice2 || dice2 == dice3 || dice3 == dice4 || dice4 == dice5)
    {
        if (dice1 == dice2)
        {
            if (dice3 == dice4 || dice4 == dice5)
            {
                hand = "Two pairs";
                return 2;
            }
        }
        else if (dice2 == dice3)
        {
            if (dice4 == dice5)
            {
                hand = "Two pairs";
                return 2;
            }
        }
        else if (dice3 == dice4)
        {
            if (dice1 == dice2)
            {
                hand = "Two pairs";
                return 2;
            }
        }
        else if (dice4 == dice5)
        {
            if (dice1 == dice2 || dice2 == dice3)
            {
                hand = "Two pairs";
                return 2;
            }
        }
        if (hand != "Two pairs")
        {
            hand = "One pair";
            return 1;
        }
    }
    hand = "Bust";
    return 0;
}

void PlayGame(int player)
{
    System.Console.WriteLine();
    System.Console.WriteLine($"Player {player}");
    System.Console.WriteLine("==========");

    fixed1 = fixed2 = fixed3 = fixed4 = fixed5 = false;

    for (int i = 1; i <= 3 && !(fixed1 && fixed2 && fixed3 && fixed4 && fixed5); i++)
    {
        RollDice();
        PrintDice(i);
        if (i < 3) { FixDice(); }
    }

    System.Console.WriteLine($"Game over! Player {player}");
    SortDice();
    System.Console.WriteLine($"Your finale rolled Dices were: {dice1}, {dice2}, {dice3}, {dice4}, {dice5}");
    AnalyzeResult();
    System.Console.WriteLine($"You rolled: {hand}");
}

string DefiniteWinner()
{
    if (pointsPlayer1 > pointsPlayer2)
    {
        return "Player 1 wins!";
    }
    else if (pointsPlayer2 > pointsPlayer1)
    {
        return "Player 2 wins!";
    }
    else
    {
        return "No winner!";
    }
}

Console.Clear();
System.Console.WriteLine("Hello and welcome to the Poker Game!");
PlayGame(1);
pointsPlayer1 = AnalyzeResult();
PlayGame(2);
pointsPlayer2 = AnalyzeResult();
System.Console.WriteLine(DefiniteWinner());