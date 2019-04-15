using System;
using System.Collections.Generic;

namespace SplitListIntoNGroups
{
    //Simple Class to split a set of numbers into a set of lists.

    //Sample of usage:
    //SplitListIntoKGroups( FromNumber, ToNumber, NumberOfGroups)
    //SplitListIntoKGroups( 1, n, k)

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

            if (NumberOfGroups <= 0)
            {
                Console.WriteLine("NumberOfGroups must be larger than 0!");
                PrintEndMessage();
                return;
            }

            // Stopwatch to get the execution time
            var watch = System.Diagnostics.Stopwatch.StartNew();

            Console.WriteLine("Splitting the numbers " + FromNumber + " to " + ToNumber + " into all possible " + NumberOfGroups + " groups.");
            Console.WriteLine("");

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

            //Prints the initial groups:
            PrintLists(NewGroups);

            //Continue moving numbers arround until the first group gets all the numbers it can get
            int Index = NumberOfGroups - 1;
            while (NewGroups[0][ToNumber + 1 - NumberOfGroups - 1] == 0)
            {
                //Move numbers from the current targeted group to the next, sorting it and displaying all the groups:
                while (NewGroups[Index][1] != 0)
                {
                    MoveNumberToOtherList(NewGroups[Index - 1], NewGroups[Index], false);
                    SortAllList(NewGroups);

                    PrintLists(NewGroups);
                }

                //Check if we are on the last group
                if (Index - 2 >= 0)
                {
                    //Move one number from the group next to the current indexed, to the group before that, as well as sorting the groups again
                    MoveNumberToOtherList(NewGroups[Index - 2], NewGroups[Index - 1], false);
                    SortAllList(NewGroups);

                    //Moves all the numbers, exept the lowest one, from the group next to the current index, back into the index.
                    while (NewGroups[Index - 1][1] != 0)
                    {
                        MoveNumberToOtherList(NewGroups[Index], NewGroups[Index - 1], true);
                        SortAllList(NewGroups);
                    }

                    //Then if after returning the numbers, that non was acturally returned, decresse the index, and continue.
                    if (NewGroups[Index][1] == 0)
                        Index--;

                    //And print the groups again.
                    PrintLists(NewGroups);
                }
            }

            //Stops the stopwatch
            watch.Stop();

            Console.WriteLine("");
            Console.WriteLine("Done! Took " + watch.ElapsedMilliseconds + " ms");

            PrintEndMessage();
        }

        //Moves the lowest number from one list to another, can be inversed
        static private void MoveNumberToOtherList(List<int> ToList, List<int> FromList, bool Inv)
        {
            int CurrentIndex = ToList.IndexOf(0);
            int CurrentPreIndex = FromList.IndexOf(0) - 1;
            if (CurrentPreIndex <= -1)
                CurrentPreIndex = ToList.Capacity - 1;
            if (Inv)
                CurrentPreIndex = 0;

            if (FromList[CurrentPreIndex] != 0)
            {
                ToList[CurrentIndex] = FromList[CurrentPreIndex];
                FromList[CurrentPreIndex] = 0;
            }
        }

        //Sorts all lists in a list
        static private void SortAllList(List<List<int>> InputList)
        {
            for (int i = 0; i < InputList.Count - 1; i++)
                InputList[i] = SortList(InputList[i]);
        }

        //Sorts just one list (sorting can be changed if desired)
        static private List<int> SortList(List<int> InputList)
        {
            List<int> NewList = new List<int>(InputList);

            NewList.Sort((a, b) => b - a);

            return NewList;
        }

        //Prints all lists in a structured way
        static private void PrintLists(List<List<int>> Groups)
        {
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
