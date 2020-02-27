using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StringCalculatorProj
{
    public class StringCalcualtor
    {
        public static int AddCalledCount = 0;
        private const string comma = ",";
        private const string newLine = "\n";
        private const string customDelimIndictorStart = "//";
        private const int maxNumValue = 1000;
        private const int minNumValue = 0;
        private const int maxAllowedNumbers = 2;

        public event Action<string, int> AddOccured;
        public int Add(string input)
        {

            AddCalledCount++;
            int r = 0;
            AddOccured?.Invoke(input, r);

            if (input.StartsWith(customDelimIndictorStart))
            {// Possibility of processing custom delimiter

                if (input.IndexOf(newLine) > 2)
                {
                    string customDelim = input.Substring(customDelimIndictorStart.Length, input.IndexOf(newLine) - customDelimIndictorStart.Length);

                    // customDelimeter extracted and cannot be a number
                    int parsedInt;
                    if (!int.TryParse(customDelim, out parsedInt))
                    {
                        input = input.Replace(newLine, comma);
                        input = input.Substring(input.IndexOf(comma) + comma.Length, input.Length - (input.IndexOf(comma) + comma.Length));
                        return r = ProcessInput(input, customDelim);
                    }
                    else
                    {
                        throw new InvalidOperationException($"cannot use numbers as custom delimiters - {input}");
                    }
                }
            }
            else
            {
                if (input.Contains(comma) || input.Contains(newLine))
                {

                    if (input.Contains(newLine))
                        input = input.Replace(newLine, comma);
                    return r = ProcessInput(input, comma);
                }
                // Empty String
                if (input == "")
                    return 0;
                // Single number without delimiters
                else if (int.TryParse(input, out r))
                {
                    if (r > maxNumValue)// Greater than 1000 ignore, in this case returns 0
                        return 0;
                    else if (r <= maxNumValue && r > minNumValue)// num >0 and <=1000, rturns num
                        return r;
                    else if (r < minNumValue)
                        throw new InvalidOperationException($"negatives not allowed - {r}");
                }
                else
                {// Nan in parameters
                    throw new InvalidOperationException($"NANs not allowed - {input}");
                }
            }
            return r;
        }

        private int ProcessInput(string input, string detectedDelimeter)
        {
            int r = 0;
            int LegitimateProcessedNumbers = 0;

            string NegativeExceptionMessageHelper = null;
            string NanExceptionMessageHelper = null;
            string AttemptToAddMoreThan3messageHelper = "processing ignored - Attempt to add more than 3 numbers";
            string[] nums;
            #region This is to handle multi characters custom delimiters
            if (detectedDelimeter.Length > 1)
            {
                string newDelimeter = null;
                if (!input.Contains(comma))
                {
                    newDelimeter = comma;
                }

                else
                {
                    newDelimeter = "*";
                }
                input = input.Replace(detectedDelimeter, newDelimeter);
                nums = input.Split(newDelimeter.ToCharArray());
            }
            #endregion
            else
                nums = input.Split(detectedDelimeter.ToCharArray());
            if (nums.Length <= maxAllowedNumbers)
            {
                foreach (string num in nums)
                {
                    int parsedInt;
                    if (int.TryParse(num, out parsedInt))
                    {// It is a number

                        if (parsedInt <= maxNumValue && parsedInt >= minNumValue)
                        {//Number in range inclusive, then process
                            r += parsedInt;
                            LegitimateProcessedNumbers++;
                        }
                        else if (parsedInt > maxNumValue)
                        {// Number > 1000, then process as zero(ignore)
                            LegitimateProcessedNumbers++;
                        }
                        else
                        {// Must be negative number, then just prepare related exception statement
                            if (NegativeExceptionMessageHelper == null)
                                NegativeExceptionMessageHelper += parsedInt;
                            else
                                NegativeExceptionMessageHelper += "," + parsedInt;

                        }
                    }
                    else
                    {// It is a NAN, then just prepare related exception statement
                        if (NanExceptionMessageHelper == null)
                            NanExceptionMessageHelper += num;
                        else
                            NanExceptionMessageHelper += "," + num;
                    }

                }
                if (LegitimateProcessedNumbers == nums.Length)
                {
                    return r;
                }
                if (NegativeExceptionMessageHelper != null)
                    throw new InvalidOperationException($"negatives not allowed - {NegativeExceptionMessageHelper}");
                if (NanExceptionMessageHelper != null)
                    throw new InvalidOperationException($"NANs not allowed - {NanExceptionMessageHelper}");

            }
            else//*****ILLEGAL PER THE REQUIREMENTS - No need to check unless discussed with stake holders

            {// ILLEGAL: More than 2 parameters are passed
             //note: Whether within range, outside of range or NaNs 
             // (This violates the requirements of non, 1, or 2 parameters), 
             // as the input string parameter  might be faulty, corrupted, or tampered with

                throw new InvalidOperationException($"operation not allowed - {AttemptToAddMoreThan3messageHelper}");
            }
            return r;
        }

        public int GetCalledCount()
        {
            return AddCalledCount;
        }
    }
}
