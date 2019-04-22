using System;
using System.Collections.Generic;

namespace SplitListIntoNGroups
{
    //Simple Class to split a set of numbers into a set of lists.

    //Sample of usage:
    //SplitListIntoKGroups( FromNumber, ToNumber, NumberOfGroups)
    //SplitListIntoKGroups(List<int> SenderGroup, NumberOfGroups)

    class SNo2KV
    {
        //Main Call void
        static public void SplitListIntoKGroups(int FromNumber, int ToNumber, int NumberOfGroups)
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
                NewGroups.Add(SortList(InnerList));
            }

            Console.WriteLine("Splitting the numbers " + FromNumber + " to " + ToNumber + " into all possible " + NumberOfGroups + " groups.");
            Console.WriteLine("");

            InnerSplitListIntoKGroups(NewGroups, NumberOfGroups, ToNumber + 1 - NumberOfGroups - 1);
        }

        static public void SplitListIntoKGroupsFromList(List<int> SenderGroup, int NumberOfGroups)
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
            List<int> SortedSenderGroup = SortList(SenderGroup);

            //Initializing the groups, much like the upper function
            List<List<int>> NewGroups = new List<List<int>>();
            int CurrentIndex = 0;
            for (int i = 0; i < NumberOfGroups; i++)
            {
                List<int> InnerNewGroup = new List<int>(new int[SortedSenderGroup.Count + 1 - NumberOfGroups]);
                if (i == NumberOfGroups - 1)
                {
                    for (int j = 0; j < SortedSenderGroup.Count + 1 - NumberOfGroups; j++)
                    {
                        InnerNewGroup[j] = SortedSenderGroup[CurrentIndex];
                        CurrentIndex++;
                    }
                }
                else
                {
                    InnerNewGroup[0] = SortedSenderGroup[CurrentIndex];
                    CurrentIndex++;
                }
                NewGroups.Add(InnerNewGroup);
            }

            Console.Write("Splitting the numbers [ ");
            for (int i = 0; i < SortedSenderGroup.Count; i++)
                Console.Write(SortedSenderGroup[i] + ", ");
            Console.WriteLine("] into all possible " + NumberOfGroups + " groups.");
            Console.WriteLine("");

            InnerSplitListIntoKGroups(NewGroups, NumberOfGroups, SortedSenderGroup.Count + 1 - NumberOfGroups - 1);
        }

        static private void InnerSplitListIntoKGroups(List<List<int>> NewGroups, int NumberOfGroups, int MaxIndexLength)
        {
            // Stopwatch to get the execution time
            var watch = System.Diagnostics.Stopwatch.StartNew();
            int NumberOfCombinations = 1;

            //Prints the initial groups:
            SortAllList(NewGroups);
            PrintLists(NewGroups, NumberOfCombinations);

            //Starting index will always be the last group:
            int Index = NumberOfGroups - 1;
            while (NewGroups[0][0] == 0)
            {
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
                    SortAllList(NewGroups);

                    for (int i = Index; i <= NumberOfGroups - 1; i++)
                    {
                        while (NewGroups[i - 1][MaxIndexLength - 1] != 0)
                        {
                            MoveNumberToOtherList(NewGroups[i], NewGroups[i - 1], true);
                            SortAllList(NewGroups);
                        }
                    }
                    Index--;
                    NumberOfCombinations++;
                    PrintLists(NewGroups, NumberOfCombinations);
                    continue;
                }

                //Moves numbers from the current group over to the other group
                while (NewGroups[Index][MaxIndexLength - 1] != 0)
                {
                    MoveNumberToOtherList(NewGroups[Index - 1], NewGroups[Index], false);
                    SortAllList(NewGroups);
                    NumberOfCombinations++;
                    PrintLists(NewGroups, NumberOfCombinations);
                }

                //When that cant be done anymore, move one number even further back, and sort the groups.
                MoveNumberToOtherList(NewGroups[Index - 2], NewGroups[Index - 1], false);
                SortAllList(NewGroups);

                //Revert what was just done, and put all the numbers moved in the start back to the first list
                for (int i = Index; i <= NumberOfGroups - 1; i++)
                {
                    while (NewGroups[i - 1][MaxIndexLength - 1] != 0)
                    {
                        MoveNumberToOtherList(NewGroups[i], NewGroups[i - 1], true);
                        SortAllList(NewGroups);
                    }
                }

                NumberOfCombinations++;
                PrintLists(NewGroups, NumberOfCombinations);
            }

            //Stops the stopwatch
            watch.Stop();

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
                InputList[i] = SortList(InputList[i]);
        }

        //Sorts just one list (sorting can be changed if desired)
        static private List<int> SortList(List<int> InputList)
        {
            List<int> NewList = new List<int>(InputList);

            NewList.Sort((a, b) => a - b);

            return NewList;
        }

        //Prints all lists in a structured way
        static private void PrintLists(List<List<int>> Groups, int Itteration)
        {
            string AddSpaces = "    : ";
            if (Itteration >= 10)
                AddSpaces = "   : ";
            if (Itteration >= 100)
                AddSpaces = "  : ";
            if (Itteration >= 1000)
                AddSpaces = " : ";
            if (Itteration >= 10000)
                AddSpaces = ": ";
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
