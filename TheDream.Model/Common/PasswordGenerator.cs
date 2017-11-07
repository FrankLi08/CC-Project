using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheDream.Model.Common
{
   public class PasswordGenerator
    {

        /// <summary>
        /// Generate random password with length in the given range, and meet the desired Regular Expression
        /// </summary>
        /// <param name="minLength"></param>
        /// <param name="maxLength"></param>
        /// <param name="minDigitNum"></param>
        /// <param name="minLowerCaseNum"></param>
        /// <param name="minUpperCaseNum"></param>
        /// <param name="minSpecialCharNum"></param>
        /// <returns>password</returns>
        public string GeneratePassword(int minLength, int maxLength, int minDigitNum, int minLowerCaseNum, int minUpperCaseNum, int minSpecialCharNum)
        {
            string digit = "0123456789";
            string lower = "abcdefghijklmnopqrstuvwxyz";
            string upper = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            string specialChar = "@#$%^&+=";
            string fullDict = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789@#$%^&+=";
            Random random = new Random();
            StringBuilder password = new StringBuilder();

            int passwordLength = random.Next(minLength, maxLength + 1);

            for (int i = 0; i < passwordLength - minDigitNum - minLowerCaseNum - minUpperCaseNum - minSpecialCharNum; i++)
            {
                password.Append(fullDict[random.Next(fullDict.Length)]);
            }
            for (int i = 0; i < minDigitNum; i++)
            {
                password.Insert(random.Next(password.Length), digit[random.Next(digit.Length)]);
            }
            for (int i = 0; i < minLowerCaseNum; i++)
            {
                password.Insert(random.Next(password.Length), lower[random.Next(lower.Length)]);
            }
            for (int i = 0; i < minUpperCaseNum; i++)
            {
                password.Insert(random.Next(password.Length), upper[random.Next(upper.Length)]);
            }
            for (int i = 0; i < minSpecialCharNum; i++)
            {
                password.Insert(random.Next(password.Length), specialChar[random.Next(specialChar.Length)]);
            }
            return password.ToString();
        }
    }
}
