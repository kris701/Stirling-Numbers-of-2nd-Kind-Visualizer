namespace SplitListIntoNGroups
{
    class Program
    {
        static void Main(string[] args)
        {
            //Sample of usage:
            //SplitListIntoKGroups( FromNumber, ToNumber, NumberOfGroups)
            //SplitListIntoKGroups( 1, n, k)

            SNo2KV.SplitListIntoKGroups(1, 7, 3);

            SNo2KV.SplitListIntoKGroups(1, 7, 2);

            SNo2KV.SplitListIntoKGroups(1, 10, 6);

            //Sample of exeptions: FromNumber is 0, Groups is 0 and ToNumber is less as FromNumber

            SNo2KV.SplitListIntoKGroups(1, 10, 0);

            SNo2KV.SplitListIntoKGroups(0, 10, 3);

            SNo2KV.SplitListIntoKGroups(10, 1, 3);
        }
    }
}
