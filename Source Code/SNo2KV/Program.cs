using System;

namespace SplitListIntoNGroups
{
    class Program
    {
        static void Main(string[] args)
        {
            //Sample of usage:
            //SplitListIntoKGroups( FromNumber, ToNumber, NumberOfGroups, DisplayMode)
            //SplitListIntoKGroups(List<int> SenderGroup, NumberOfGroups, DisplayMode)

            SNo2KV.SplitListIntoKGroups(1, 7, 3, DisplayMode.Normal);

            SNo2KV.SplitListIntoKGroups(1, 10, 5, DisplayMode.Normal);

            //Can also accept a custom list as well as showing the displaymodes:

            SNo2KV.SplitListIntoKGroupsFromList(new System.Collections.Generic.List<int>(new int[] { 1, 1, 1, 1, 1, 1, 1, 1, 1, 8, 8, 8, 8, 8, 8, 9, 20, 20, 20, 20, 20, 21, 21, 21, 21, 23, 70, 70, 70, 71, 71, 71, 71, 71, 71, 100, 200, 1000, 2000 }), 8, DisplayMode.None);

            SNo2KV.SplitListIntoKGroupsFromList(new System.Collections.Generic.List<int>(new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 100, 101, 102, 103,104,105,106,107,108,109,201,202,203,204,205,206,207,208,209,210 }), 12, DisplayMode.None);

            //Sample of exeptions: FromNumber is 0, Groups is 0 and ToNumber is less as FromNumber

            SNo2KV.SplitListIntoKGroups(1, 10, 0, DisplayMode.Normal);

            SNo2KV.SplitListIntoKGroups(0, 10, 3, DisplayMode.Normal);

            SNo2KV.SplitListIntoKGroups(10, 1, 3, DisplayMode.Normal);
        }
    }
}
