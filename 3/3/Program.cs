string path = "C:\\Users\\hampu\\Programmering\\Advent_Of_Code\\2024\\3\\Input.txt";

ChallengeTwo(path);

static void ChallengeOne(string path)
{

    string input = File.ReadAllText(path);

    List<int> interestingIndexes = new();

    for (int i = 0; i < input.Length; i++)
    {
        if (input[i] == 'm' || input[i] == ')')
        {
            interestingIndexes.Add(i);
        }
    }

    List<string> potentialInstructions = new();

    for (int i = 0; i < interestingIndexes.Count - 1; i++)
    {
        if (input[interestingIndexes[i]] == 'm' && input[interestingIndexes[i + 1]] == ')')
        {
            int lengthOfSubstring = interestingIndexes[i + 1] - interestingIndexes[i] + 1;

            potentialInstructions.Add(input.Substring(interestingIndexes[i], lengthOfSubstring));

            i++;
        }
    }

    int sumOfInstructions = 0;
    List<string> test = new();

    foreach (var potentialInstruction in potentialInstructions)
    {
        string start = potentialInstruction.Substring(0, 3);

        if (potentialInstruction.Substring(0, 3) == "mul")
        {
            string ending = potentialInstruction.Remove(0, 3);

            if (ending.IndexOf('(') == 0)
            {
                string potentialMultiplication = ending.Substring(1, ending.Length - 2);

                string[] potentialNumbersForMultiplication = potentialMultiplication.Split(',');

                if (potentialNumbersForMultiplication.Length == 2)
                {
                    int numberOne;
                    int numberTwo;

                    bool firstValueIsNumber = Int32.TryParse(potentialNumbersForMultiplication[0], out numberOne);
                    bool SecondValueIsNumber = Int32.TryParse(potentialNumbersForMultiplication[1], out numberTwo);

                    if (firstValueIsNumber && numberOne < 1000 && SecondValueIsNumber && numberTwo < 1000)
                    {
                        sumOfInstructions += numberOne * numberTwo;
                    }
                }
            }
        }
    }

    Console.WriteLine(sumOfInstructions);
}
static void ChallengeTwo(string path)
{

    string input = File.ReadAllText(path);

    string[] dosAndDonts = new string[] { "do()", "don't()" };

    var separatedParts = input.Split(dosAndDonts, StringSplitOptions.None);

    List<string> interestingParts = new();

    interestingParts.Add(separatedParts[0]);

    for (int i = 1; i < separatedParts.Length; i++)
    {
        int indexOfCurrentInterestingPart = input.IndexOf(separatedParts[i]);
        int indexOfPreviousInterestingPart = input.IndexOf(separatedParts[i - 1]);

        string stringToSearchForDosAndDonts = input.Substring(indexOfPreviousInterestingPart, indexOfCurrentInterestingPart - indexOfPreviousInterestingPart);

        if (!(stringToSearchForDosAndDonts.LastIndexOf(dosAndDonts[1]) > stringToSearchForDosAndDonts.LastIndexOf(dosAndDonts[0])))
        {
            interestingParts.Add(separatedParts[i]);
        }
    }

    List<ValueTuple<int, int>> interestingIndexes = new();

    for (int i = 0; i < interestingParts.Count; i++)
    {
        for (int j = 0; j < interestingParts[i].Length; j++)
        {
            if (interestingParts[i][j] == 'm' || interestingParts[i][j] == ')')
            {
                interestingIndexes.Add(ValueTuple.Create(i, j));
            }
        }
    }

    List<string> potentialInstructions = new();

    for (int i = 0; i < interestingParts.Count; i++)
    {
        List<ValueTuple<int, int>> relevantIndexes = new();
        foreach (var index in interestingIndexes)
        {
            if (index.Item1 == i)
            {
                relevantIndexes.Add(index);
            }
            else if (index.Item1 > i)
            {
                continue;
            }
        }

        for (int j = 0; j < relevantIndexes.Count - 1; j++)
        {
            if (interestingParts[i][relevantIndexes[j].Item2] == 'm' && interestingParts[i][relevantIndexes[j + 1].Item2] == ')')
            {
                int lengthOfSubstring = relevantIndexes[j + 1].Item2 - relevantIndexes[j].Item2 + 1;

                potentialInstructions.Add(interestingParts[i].Substring(relevantIndexes[j].Item2, lengthOfSubstring));

                j++;
            }
        }
    }

    int sumOfInstructions = 0;
    List<string> test = new();

    foreach (var potentialInstruction in potentialInstructions)
    {
        string start = potentialInstruction.Substring(0, 3);

        if (potentialInstruction.Substring(0, 3) == "mul")
        {
            string ending = potentialInstruction.Remove(0, 3);

            if (ending.IndexOf('(') == 0)
            {
                string potentialMultiplication = ending.Substring(1, ending.Length - 2);

                string[] potentialNumbersForMultiplication = potentialMultiplication.Split(',');

                if (potentialNumbersForMultiplication.Length == 2)
                {
                    int numberOne;
                    int numberTwo;

                    bool firstValueIsNumber = Int32.TryParse(potentialNumbersForMultiplication[0], out numberOne);
                    bool SecondValueIsNumber = Int32.TryParse(potentialNumbersForMultiplication[1], out numberTwo);

                    if (firstValueIsNumber && numberOne < 1000 && SecondValueIsNumber && numberTwo < 1000)
                    {
                        sumOfInstructions += numberOne * numberTwo;
                    }
                }
            }
        }
    }
    Console.WriteLine(sumOfInstructions);
}