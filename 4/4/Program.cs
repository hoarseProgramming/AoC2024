string path = @"C:\Users\hampu\Programmering\Advent_Of_Code\2024\4\Input.txt";

var input = File.ReadAllText(path);

int xAxisLength = input.IndexOf('\r');
int yAxisLength = 1;

foreach (var c in input)
{
    if (c == '\n')

    {
        yAxisLength++;
    }
}

var array = new ValueTuple<char, bool>[yAxisLength, xAxisLength];

string fixedInput = input.Replace("\n", "").Replace("\r", "");

int currentIndexOfInput = 0;

for (int i = 0; i < array.GetLongLength(0); i++)
{
    for (int j = 0; j < array.GetLongLength(1); j++)
    {
        array[i, j] = ValueTuple.Create(fixedInput[currentIndexOfInput], false);
        currentIndexOfInput++;
    }
}

//Console.WriteLine(ChallengeOne(array));

Console.WriteLine(ChallengeTwo(array));

static int ChallengeOne(ValueTuple<char, bool>[,] array)
{
    int foundXMASes = 0;

    for (int i = 0; i < array.GetLongLength(0); i++)
    {
        for (int j = 0; j < array.GetLongLength(1); j++)
        {
            if (i >= 3 && i < array.GetLongLength(0) - 3 && j >= 3 && j < array.GetLongLength(1) - 3)
            {
                foundXMASes += LookForXMAS(array, j, i, 0, -1); // Upp
                foundXMASes += LookForXMAS(array, j, i, 1, -1); // Upp höger
                foundXMASes += LookForXMAS(array, j, i, 1, 0); // Höger
                foundXMASes += LookForXMAS(array, j, i, 1, 1); // Ner höger
                foundXMASes += LookForXMAS(array, j, i, 0, 1); // Ner
                foundXMASes += LookForXMAS(array, j, i, -1, 1); // Ner vänster
                foundXMASes += LookForXMAS(array, j, i, -1, 0); // Vänster
                foundXMASes += LookForXMAS(array, j, i, -1, -1); // Upp Vänster

            }
            else if (i >= 3 && j >= 3 && j < array.GetLongLength(1) - 3)
            {
                //Gör åt alla håll utom neråt & snett neråt
                foundXMASes += LookForXMAS(array, j, i, 0, -1); // Upp
                foundXMASes += LookForXMAS(array, j, i, 1, -1); // Upp höger
                foundXMASes += LookForXMAS(array, j, i, 1, 0); // Höger
                foundXMASes += LookForXMAS(array, j, i, -1, 0); // Vänster
                foundXMASes += LookForXMAS(array, j, i, -1, -1); // Upp Vänster
            }
            else if (i < array.GetLongLength(0) - 3 && j >= 3 && j < array.GetLongLength(1) - 3)
            {
                // Gör åt alla håll utom uppåt och snett uppåt
                foundXMASes += LookForXMAS(array, j, i, 1, 0); // Höger
                foundXMASes += LookForXMAS(array, j, i, 1, 1); // Ner höger
                foundXMASes += LookForXMAS(array, j, i, 0, 1); // Ner
                foundXMASes += LookForXMAS(array, j, i, -1, 1); // Ner vänster
                foundXMASes += LookForXMAS(array, j, i, -1, 0); // Vänster
            }
            else if (i >= 3 && i < array.GetLongLength(0) - 3 && j >= 3)
            {

                // Gör åt alla håll utom åt höger och snett åt höger
                foundXMASes += LookForXMAS(array, j, i, 0, -1); // Upp
                foundXMASes += LookForXMAS(array, j, i, 0, 1); // Ner
                foundXMASes += LookForXMAS(array, j, i, -1, 1); // Ner vänster
                foundXMASes += LookForXMAS(array, j, i, -1, 0); // Vänster
                foundXMASes += LookForXMAS(array, j, i, -1, -1); // Upp Vänster
            }
            else if (i >= 3 && i < array.GetLongLength(0) - 3 && j < array.GetLongLength(1) - 3)
            {
                // Gör åt alla håll utom åt vänster och snett åt vänster
                foundXMASes += LookForXMAS(array, j, i, 0, -1); // Upp
                foundXMASes += LookForXMAS(array, j, i, 1, -1); // Upp höger
                foundXMASes += LookForXMAS(array, j, i, 1, 0); // Höger
                foundXMASes += LookForXMAS(array, j, i, 1, 1); // Ner höger
                foundXMASes += LookForXMAS(array, j, i, 0, 1); // Ner
            }
            else if (i < array.GetLongLength(0) - 3 && j < array.GetLongLength(1) - 3)
            {
                // Gör ner, höger, snett ner åt höger
                foundXMASes += LookForXMAS(array, j, i, 1, 0); // Höger
                foundXMASes += LookForXMAS(array, j, i, 1, 1); // Ner höger
                foundXMASes += LookForXMAS(array, j, i, 0, 1); // Ner
            }
            else if (i < array.GetLongLength(0) - 3 && j >= 3)
            {
                // Gör ner, vänster, snett ner åt vänster
                foundXMASes += LookForXMAS(array, j, i, 0, 1); // Ner
                foundXMASes += LookForXMAS(array, j, i, -1, 1); // Ner vänster
                foundXMASes += LookForXMAS(array, j, i, -1, 0); // Vänster
            }
            else if (i >= 3 && j < array.GetLongLength(1) - 3)
            {
                // Gör upp, höger, snett upp åt höger
                foundXMASes += LookForXMAS(array, j, i, 0, -1); // Upp
                foundXMASes += LookForXMAS(array, j, i, 1, -1); // Upp höger
                foundXMASes += LookForXMAS(array, j, i, 1, 0); // Höger
            }
            else
            {
                // Gör upp, vänster, snett upp åt vänster
                foundXMASes += LookForXMAS(array, j, i, 0, -1); // Upp
                foundXMASes += LookForXMAS(array, j, i, -1, 0); // Vänster
                foundXMASes += LookForXMAS(array, j, i, -1, -1); // Upp Vänster

            }
        }
    }

    return foundXMASes;
    static int LookForXMAS(ValueTuple<char, bool>[,] array, int xIndex, int yIndex, int xVelocity, int yVelocity)
    {
        int currentXIndex = xIndex;
        int currentYIndex = yIndex;

        string potentialXMAS = string.Empty;

        for (int i = 0; i < 4; i++)
        {
            potentialXMAS += array[currentYIndex, currentXIndex].Item1;
            currentXIndex += xVelocity;
            currentYIndex += yVelocity;
        }

        if (potentialXMAS == "XMAS")
        {
            return 1;
        }
        else
        {
            return 0;
        }
    }
}


static int ChallengeTwo(ValueTuple<char, bool>[,] array)
{
    int foundMAS = 0;

    for (int y = 0; y < array.GetLongLength(0); y++)
    {
        for (int x = 0; x < array.GetLongLength(1); x++)
        {
            if (y <= array.GetLongLength(0) - 3 && x <= array.GetLongLength(1) - 3)
            {
                int currentXIndex = x;
                int currentYIndex = y;
                string charactersToAdd = string.Empty;

                for (int y1 = 0; y1 < 3; y1++)
                {
                    for (int x1 = 0; x1 < 3; x1++)
                    {
                        charactersToAdd += array[currentYIndex, currentXIndex].Item1;
                        currentXIndex++;
                    }
                    currentXIndex = x;
                    currentYIndex++;
                }

                int currentIndex = 0;

                var threeByThreeArray = new ValueTuple<char, bool>[3, 3];

                for (int y1 = 0; y1 < 3; y1++)
                {
                    for (int x1 = 0; x1 < 3; x1++)
                    {
                        threeByThreeArray[y1, x1] = ValueTuple.Create(charactersToAdd[currentIndex], false);
                        currentIndex++;
                    }
                }

                foundMAS += LookForXmasThreeByThree(threeByThreeArray);

            }
        }
    }

    return foundMAS;

    static int LookForXmasThreeByThree(ValueTuple<char, bool>[,] array)
    {

        int foundMASes = 0;

        foundMASes += LookForXMAS(array, 0, 0, 1, 1); // Ner höger
        foundMASes += LookForXMAS(array, 0, 2, 1, -1); // Upp höger

        if (foundMASes == 2)
        {
            return 1;
        }
        else
        {
            return 0;
        }
    }

    static int LookForXMAS(ValueTuple<char, bool>[,] array, int xIndex, int yIndex, int xVelocity, int yVelocity)
    {
        int currentXIndex = xIndex;
        int currentYIndex = yIndex;

        string potentialXMAS = string.Empty;

        for (int i = 0; i < 3; i++)
        {
            potentialXMAS += array[currentYIndex, currentXIndex].Item1;
            currentXIndex += xVelocity;
            currentYIndex += yVelocity;
        }

        if (potentialXMAS == "MAS" || potentialXMAS == "SAM")
        {
            return 1;
        }
        else
        {
            return 0;
        }
    }
}
