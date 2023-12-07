using DiffingApiTask.Models;

namespace DiffingApiTask.Classes
{
    public class DiffDataComparison
    {
        public static DiffResult DiffTwoStringsAndGetTheResult(string left, string right)
        {
            DiffResult diffResult = new DiffResult()
            {
                DiffResultType = "",
                Diffs = new List<Diff>()
            };

            if (left.Length != right.Length) diffResult.DiffResultType = "SizeDoNotMatch";

            else if (left == right) diffResult.DiffResultType = "Equals";

            else // Diff algorithm
            {
                diffResult.DiffResultType = "ContentDoNotMatch";
                var leftArray = left.ToCharArray(); // ToCharArray() is more performant than ToArray();
                var rightArray = right.ToCharArray();
                int offsetLocation = 0, offsetLength = 0;

                for (int i = 0; i < leftArray.Length; i++)
                {
                    if (leftArray[i] != rightArray[i])
                    {
                        if (leftArray[i - 1] == rightArray[i - 1]) offsetLocation = i;

                        offsetLength++;

                        if (leftArray[i + 1] == rightArray[i + 1])
                        {
                            diffResult.Diffs.Add(new Diff { Offset = offsetLocation, Length = offsetLength });
                            offsetLocation = offsetLength = 0;
                        }
                    }
                }
            }

            return diffResult;
        }
    }
} 
