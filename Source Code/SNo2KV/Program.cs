namespace SplitListIntoNGroups
{
    class Program
    {
        static void Main(string[] args)
        {
            //Sample of usage:
            //SplitListIntoKGroups( FromNumber, ToNumber, NumberOfGroups)
            //SplitListIntoKGroups(List<int> SenderGroup, NumberOfGroups)

            SNo2KV.SplitListIntoKGroups(1, 7, 3);

            SNo2KV.SplitListIntoKGroups(1, 10, 5);

            //Can also accept a custom list:

            SNo2KV.SplitListIntoKGroupsFromList(new System.Collections.Generic.List<int>(new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 100, 101, 102, 103,104,105,106,107,108,109,201,202,203,204,205,206,207,208,209,210 }), 4);

            //Sample of exeptions: FromNumber is 0, Groups is 0 and ToNumber is less as FromNumber

            SNo2KV.SplitListIntoKGroups(1, 10, 0);

            SNo2KV.SplitListIntoKGroups(0, 10, 3);

            SNo2KV.SplitListIntoKGroups(10, 1, 3);
        }
    }
}
