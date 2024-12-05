string path = @"C:\Users\hampu\Programmering\Advent_Of_Code\2024\2\Input.txt";

string input = File.ReadAllText(path);

//string path = @"C:\Users\hampu\Programmering\Advent_Of_Code\2024\2\Test.txt";

//string input = File.ReadAllText(path);

string removedRs = input.Replace("\r", "");

var listOfReports = removedRs.Split('\n');

List<List<int>> intedReports = new();

foreach (string report in listOfReports)
{
    List<int> intedReport = new();

    string[] separatedValues = report.Split(" ");

    for (int i = 0; i < separatedValues.Length; i++)
    {
        intedReport.Add(Int32.Parse(separatedValues[i]));
    }
    intedReports.Add(intedReport);
}

int safeReports = 0;

foreach (var report in intedReports)
{
    bool isSafe = true;
    bool isAscending = false;
    bool isDescending = false;

    for (int i = 1; i < report.Count; i++)
    {
        if (i == 1)
        {
            if (report[i] > report[i - 1] && Math.Abs(report[i] - report[i - 1]) < 4)
            {
                isAscending = true;
            }
            else if (report[i] < report[i - 1] && Math.Abs(report[i] - report[i - 1]) < 4)
            {
                isDescending = true;
            }
            else
            {
                isSafe = false;
                break;
            }
        }
        else
        {
            if (isAscending)
            {
                if (!(report[i] > report[i - 1] && Math.Abs(report[i] - report[i - 1]) < 4))
                {
                    isSafe = false;
                    break;
                }
            }
            else
            {
                if (!(report[i] < report[i - 1] && Math.Abs(report[i] - report[i - 1]) < 4))
                {
                    isSafe = false;
                    break;
                }
            }
        }
    }
    if (isSafe) safeReports++;
}

Console.WriteLine(safeReports);


Console.WriteLine("*** 2 ***");
safeReports = 0;

foreach (var report in intedReports)
{
    if (CheckIfMoreThanOneDoubleDigit(report))
    {
        continue;
    }
    else if (CheckIfMoreThanOneOrderDisturbingValue(report))
    {
        continue;
    }
    else if (CheckReportIsSafe(report, GetShouldAscend(report)))
    {
        safeReports++;
    }
    else
    {
        int indexOfOrderDisturbingValue = GetIndexOfOrderDisturbingValue(report, GetShouldAscend(report));

        if (CheckReportIsSafeWithProblemDampener(report, GetShouldAscend(report), indexOfOrderDisturbingValue))
        {
            safeReports++;
        }
        else
        {
            if (CheckReportIsSafeWithProblemDampener(report, GetShouldAscend(report), indexOfOrderDisturbingValue - 1))
            {
                safeReports++;
            }
        }
    }
}
Console.WriteLine(safeReports);

static bool CheckReportIsSafe(List<int> report, bool shouldAscend)
{
    bool isSafe = true;

    for (int i = 0; i < report.Count - 1; i++)
    {
        if (shouldAscend)
        {
            if (!CheckAscendingValue(report, i))
            {
                isSafe = false;
                break;
            }
        }
        else
        {
            if (!CheckDescendingValue(report, i))
            {
                isSafe = false;
                break;
            }
        }
    }

    return isSafe;
}
static bool CheckReportIsSafeWithProblemDampener(List<int> report, bool shouldAscend, int index)
{
    List<int> reportCopy = new();

    foreach (int value in report)
    {
        reportCopy.Add(value);
    }

    reportCopy.RemoveAt(index);

    bool isSafe = true;

    for (int i = 0; i < reportCopy.Count - 1; i++)
    {
        if (shouldAscend)
        {
            if (!CheckAscendingValue(reportCopy, i))
            {
                isSafe = false;
                break;
            }
        }
        else
        {
            if (!CheckDescendingValue(reportCopy, i))
            {
                isSafe = false;
                break;
            }
        }
    }

    return isSafe;
}

static bool CheckIfMoreThanOneDoubleDigit(List<int> report)
{
    bool isMoreThanOne = false;
    int sumOfDoubleDigits = 0;
    for (int i = 0; i < report.Count; i++)
    {
        for (int j = 0; j < report.Count; j++)
        {
            if (!(i == j))
            {
                if (report[i] == report[j])
                {
                    sumOfDoubleDigits++;
                }
            }
        }
    }
    if (sumOfDoubleDigits > 2)
    {
        isMoreThanOne = true;
    }
    return isMoreThanOne;
}

static int GetIndexOfOrderDisturbingValue(List<int> report, bool shouldAscend)
{
    int indexOfDisturbingValue = 0;

    if (shouldAscend)
    {
        for (int i = 0; i < report.Count - 1; i++)
        {
            if (!CheckAscendingValue(report, i))
            {
                indexOfDisturbingValue = i + 1;
                break;
            }
        }
    }
    else
    {
        for (int i = 0; i < report.Count - 1; i++)
        {
            if (!CheckDescendingValue(report, i))
            {
                indexOfDisturbingValue = i + 1;
                break;
            }
        }
    }

    return indexOfDisturbingValue;

    //bool isMoreThanOneDisturbingValue =
    //    indexOfAscendingDisturbingValue != indexOfDescendingDisturbingValue
    //    && indexOfAscendingDisturbingValue != report.Count - 1
    //    && indexOfDescendingDisturbingValue != 0;

    //return isMoreThanOneDisturbingValue;
}

static bool CheckIfMoreThanOneOrderDisturbingValue(List<int> report)
{
    int sumOfAscendingPairs = 0;
    int sumOfDescendingPairs = 0;
    int sumOfDoubles = 0;

    for (int i = 0; i < report.Count - 1; i++)
    {
        if (report[i] < report[i + 1])
        {
            sumOfAscendingPairs++;
        }
        else if (report[i] > report[i + 1])
        {
            sumOfDescendingPairs++;
        }
        else
        {
            sumOfDoubles++;
        }
    }

    bool isMoreThanOneOrderDisturbingValue =
        sumOfAscendingPairs > 1 && sumOfDescendingPairs > 1
        || sumOfAscendingPairs > 0 && sumOfDescendingPairs > 0 && sumOfDoubles > 0;

    return isMoreThanOneOrderDisturbingValue;
}

static bool GetShouldAscend(List<int> report)
{
    int sumOfAscendingPairs = 0;
    int sumOfDescendingPairs = 0;

    for (int i = 0; i < report.Count - 1; i++)
    {
        if (report[i] < report[i + 1])
        {
            sumOfAscendingPairs++;
        }
        else if (report[i] > report[i + 1])
        {
            sumOfDescendingPairs++;
        }
    }

    bool shouldAscend = sumOfAscendingPairs > sumOfDescendingPairs;

    return shouldAscend;
}

static bool CheckAscendingValue(List<int> report, int i)
{
    if (report[i] < report[i + 1] && Math.Abs(report[i] - report[i + 1]) < 4)
    {
        return true;
    }
    else
    {
        return false;
    }
}

static bool CheckDescendingValue(List<int> report, int i)
{
    if (report[i] > report[i + 1] && Math.Abs(report[i] - report[i + 1]) < 4)
    {
        return true;
    }
    else
    {
        return false;
    }
}

