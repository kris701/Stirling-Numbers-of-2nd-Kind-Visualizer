using System;
using System.Collections.Generic;

namespace SplitListIntoNGroups
{
    //Simple Class to split a set of numbers into a set of lists.

    //Sample of usage:
    //SplitListIntoKGroups( FromNumber, ToNumber, NumberOfGroups, DisplayMode)
    //SplitListIntoKGroups(List<int> SenderGroup, NumberOfGroups, DisplayMode)

    enum DisplayMode { Normal, Every10, Every100, Every1000, Every10000, Every100000, None };

    class SNo2KV
    {
        //Main Call void
        static public void SplitListIntoKGroups(int FromNumber, int ToNumber, int NumberOfGroups, DisplayMode _DisplayMode)
        {
            //Simple catchers to make sure no error values gets in and crashes the program
            if (FromNumber <= 0)
            {
                Console.WriteLine("FromNumber must be larger than 0!");
                PrintEndMessage();
                return;
            }

            if (ToNumber < FromNumber)
            {
                Console.WriteLine("FromNumber must be lower than ToNumber");
                PrintEndMessage();
                return;
            }

            if (NumberOfGroups <= 2)
            {
                Console.WriteLine("NumberOfGroups must be larger than 2!");
                PrintEndMessage();
                return;
            }

            //Initializer for the groups
            //Taking the numbers FromInt to ToInt into the groups, and the last group gets filled with the rest of numbers
            //Sample:
            // Numbers 1-7 into 3 groups will give the initial groups:
            // [ 1 ] [ 2 ] [ 3, 4, 5, 6, 7]
            List<List<int>> NewGroups = new List<List<int>>();
            int Count = FromNumber;
            for (int i = 0; i < NumberOfGroups; i++)
            {
                List<int> InnerList = new List<int>(new int[ToNumber + 1 - NumberOfGroups]);
                if (i + 1 == NumberOfGroups)
                {
                    int InnerCount = 0;
                    for (int j = Count; j <= ToNumber; j++)
                    {
                        InnerList[InnerCount] = j;
                        InnerCount++;
                    }
                }
                else
                {
                    InnerList[0] = Count;
                    Count++;
                }
                InnerList.Sort((a, b) => a - b);
                NewGroups.Add(InnerList);
            }

            Console.WriteLine("Splitting the numbers " + FromNumber + " to " + ToNumber + " into all possible " + NumberOfGroups + " groups.");
            Console.WriteLine("");

            InnerSplitListIntoKGroups(NewGroups, NumberOfGroups, ToNumber + 1 - NumberOfGroups - 1, _DisplayMode);
        }

        static public void SplitListIntoKGroupsFromList(List<int> SenderGroup, int NumberOfGroups, DisplayMode _DisplayMode)
        {
            //Simple catchers to make sure no error values gets in and crashes the program
            if (NumberOfGroups <= 2)
            {
                Console.WriteLine("NumberOfGroups must be larger than 2!");
                PrintEndMessage();
                return;
            }

            for (int i = 0; i < SenderGroup.Count; i++)
            {
                if (SenderGroup[i] <= 0)
                {
                    Console.WriteLine("Sendergroup must not contain numbers under 1!");
                    PrintEndMessage();
                    return;
                }
            }

            //Sorts the sender group
            SenderGroup.Sort((a, b) => a - b);

            //Initializing the groups, much like the upper function
            List<List<int>> NewGroups = new List<List<int>>();
            int CurrentIndex = 0;
            for (int i = 0; i < NumberOfGroups; i++)
            {
                List<int> InnerNewGroup = new List<int>(new int[SenderGroup.Count + 1 - NumberOfGroups]);
                if (i == NumberOfGroups - 1)
                {
                    for (int j = 0; j < SenderGroup.Count + 1 - NumberOfGroups; j++)
                    {
                        InnerNewGroup[j] = SenderGroup[CurrentIndex];
                        CurrentIndex++;
                    }
                }
                else
                {
                    InnerNewGroup[0] = SenderGroup[CurrentIndex];
                    CurrentIndex++;
                }
                NewGroups.Add(InnerNewGroup);
            }

            Console.Write("Splitting the numbers [ ");
            for (int i = 0; i < SenderGroup.Count; i++)
                Console.Write(SenderGroup[i] + ", ");
            Console.WriteLine("] into all possible " + NumberOfGroups + " groups.");
            Console.WriteLine("");

            InnerSplitListIntoKGroups(NewGroups, NumberOfGroups, SenderGroup.Count + 1 - NumberOfGroups - 1, _DisplayMode);
        }

        static private void InnerSplitListIntoKGroups(List<List<int>> NewGroups, int NumberOfGroups, int MaxIndexLength, DisplayMode _DisplayMode)
        {
            // Stopwatch to get the execution time
            var watch = System.Diagnostics.Stopwatch.StartNew();
            var RPSwatch = System.Diagnostics.Stopwatch.StartNew();
            int NumberOfCombinations = 1;
            int RPSNumberOfCombinations = 1;

            //Prints the initial groups:
            SortAllList(NewGroups);
            PrintLists(NewGroups, NumberOfCombinations, _DisplayMode);

            //Starting index will always be the last group:
            int Index = NumberOfGroups - 1;
            while (NewGroups[0][0] == 0)
            {
                if (RPSwatch.ElapsedMilliseconds >= 1000)
                {
                    Console.Title = "Renderings pr Second: " + (NumberOfCombinations - RPSNumberOfCombinations);
                    RPSNumberOfCombinations = NumberOfCombinations;
                    RPSwatch.Restart();
                }
                Index = NumberOfGroups - 1;
                //Checks if the current group is empty, if it is, go to the next group
                while (NewGroups[Index][MaxIndexLength - 1] == 0)
                {
                    Index--;
                }
                if (Index != NumberOfGroups - 1)
                {
                    //pushes all remaining numbers back to start
                    Index++;
                    if (Index > 1)
                        MoveNumberToOtherList(NewGroups[Index - 2], NewGroups[Index - 1], false);
                    SortList(NewGroups[Index - 2]);

                    for (int i = Index; i <= NumberOfGroups - 1; i++)
                    {
                        while (NewGroups[i - 1][MaxIndexLength - 1] != 0)
                        {
                            MoveNumberToOtherList(NewGroups[i], NewGroups[i - 1], true);
                            SortList(NewGroups[i]);
                            SortList(NewGroups[i - 1]);
                        }
                    }
                    Index--;
                    NumberOfCombinations++;
                    PrintLists(NewGroups, NumberOfCombinations, _DisplayMode);
                    continue;
                }

                //Moves numbers from the current group over to the other group
                while (NewGroups[Index][MaxIndexLength - 1] != 0)
                {
                    MoveNumberToOtherList(NewGroups[Index - 1], NewGroups[Index], false);
                    SortList(NewGroups[Index - 1]);
                    NumberOfCombinations++;
                    PrintLists(NewGroups, NumberOfCombinations, _DisplayMode);
                }

                //When that cant be done anymore, move one number even further back, and sort the groups.
                MoveNumberToOtherList(NewGroups[Index - 2], NewGroups[Index - 1], false);
                SortList(NewGroups[Index - 2]);

                //Revert what was just done, and put all the numbers moved in the start back to the first list
                for (int i = Index; i <= NumberOfGroups - 1; i++)
                {
                    while (NewGroups[i - 1][MaxIndexLength - 1] != 0)
                    {
                        MoveNumberToOtherList(NewGroups[i], NewGroups[i - 1], true);
                        SortList(NewGroups[i]);
                        SortList(NewGroups[i - 1]);
                    }
                }

                NumberOfCombinations++;
                PrintLists(NewGroups, NumberOfCombinations, _DisplayMode);
            }

            //Stops the stopwatch
            watch.Stop();
            RPSwatch.Stop();

            Console.Title = "Done";
            Console.WriteLine("");
            Console.WriteLine("Done! A total of " + NumberOfCombinations + " combinations took " + watch.ElapsedMilliseconds + " ms");

            PrintEndMessage();
        }

        //Moves the lowest number from one list to another, can be inversed
        static private void MoveNumberToOtherList(List<int> ToList, List<int> FromList, bool Inv)
        {
            int CurrentIndex = ToList.IndexOf(0);
            int CurrentPreIndex = FromList.LastIndexOf(0) + 1;
            if (Inv)
            {
                CurrentPreIndex = FromList.LastIndexOf(0) - 1;
                if (FromList[ToList.Count - 1] != 0)
                    CurrentPreIndex = ToList.Count - 1;
            }
            if (CurrentPreIndex <= -1)
                CurrentPreIndex = 0;
            if (CurrentPreIndex > ToList.Count - 1)
                CurrentPreIndex = ToList.Count - 1;

            if (FromList[CurrentPreIndex] != 0)
            {
                ToList[CurrentIndex] = FromList[CurrentPreIndex];
                FromList[CurrentPreIndex] = 0;
            }
        }

        //Sorts all lists in a list
        static private void SortAllList(List<List<int>> InputList)
        {
            for (int i = 0; i < InputList.Count; i++)
            {
                //InputList[i].Sort((a, b) => a - b);
                SortList(InputList[i]);
            }
        }

        static private void SortList(List<int> InputList)
        {
            //Insertion Sort
            for (int i = 1; i < InputList.Count; i++)
            {
                if (InputList[i] < InputList[i - 1])
                {
                    int temp = InputList[i];
                    int j;
                    for (j = i; j > 0 && InputList[j - 1] > temp; j--)
                        InputList[j] = InputList[j - 1];
                    InputList[j] = temp;
                }
            }
        }

        //Prints all lists in a structured way
        static private void PrintLists(List<List<int>> Groups, int Itteration, DisplayMode _DisplayMode)
        {
            if (_DisplayMode == DisplayMode.Every10)
                if (Itteration % 10 == 0)
                    InnerPrintLists(Groups, Itteration);

            if (_DisplayMode == DisplayMode.Every100)
                if (Itteration % 100 == 0)
                    InnerPrintLists(Groups, Itteration);

            if (_DisplayMode == DisplayMode.Every1000)
                if (Itteration % 1000 == 0)
                    InnerPrintLists(Groups, Itteration);

            if (_DisplayMode == DisplayMode.Every10000)
                if (Itteration % 10000 == 0)
                    InnerPrintLists(Groups, Itteration);

            if (_DisplayMode == DisplayMode.Every100000)
                if (Itteration % 100000 == 0)
                    InnerPrintLists(Groups, Itteration);

            if (_DisplayMode == DisplayMode.Normal)
                InnerPrintLists(Groups, Itteration);
        }

        static private void InnerPrintLists(List<List<int>> Groups, int Itteration)
        {
            string AddSpaces = "         : ";
            if (Itteration >= 10)
                AddSpaces = "        : ";
            if (Itteration >= 100)
                AddSpaces = "       : ";
            if (Itteration >= 1000)
                AddSpaces = "      : ";
            if (Itteration >= 10000)
                AddSpaces = "     : ";
            if (Itteration >= 100000)
                AddSpaces = "    : ";
            if (Itteration >= 1000000)
                AddSpaces = "   : ";
            if (Itteration >= 10000000)
                AddSpaces = "  : ";
            if (Itteration >= 100000000)
                AddSpaces = " : ";
            Console.Write(Itteration + AddSpaces);

            foreach (List<int> InnerList in Groups)
            {
                Console.Write("[ ");
                foreach (int i in InnerList)
                {
                    if (i != 0)
                        Console.Write(i + ", ");
                }
                Console.Write("] ");
            }
            Console.WriteLine("");
        }

        //Centralized end message void
        static private void PrintEndMessage()
        {
            Console.WriteLine("");
            Console.WriteLine("Press any key to exit");
            Console.ReadKey();
        }
    }
}
