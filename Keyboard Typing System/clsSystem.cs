using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace Keyboard_Typing_System
{
    public class clsSystem

    {
        static public int Failed {  get; set; }
        static public int Success { get; set; }

        static public string[] Status { get; } = { "Lazy", "Bad", "Normal", "Good", "Fast", "Special" };

        static public int All { get; set; }

        public enum enTextType { eTraining = 1 , eTest = 2 };
        static public int CurrentTime { get; set; }


        static string GetTextFromFile(string FileName)
        {
            StreamReader Reader = new StreamReader(FileName);

            return Reader.ReadToEnd();
        }

        static public string GetText(enTextType Type)
        {
            switch (Type)
            {
                case enTextType.eTraining:
                    {
                        return GetTextFromFile("Training.txt");
                    }
                case enTextType.eTest:
                    {
                        return GetTextFromFile("Test.txt");
                    }

                    default:
                    {
                        return string.Empty;
                    }
            }
        }

        
        static public void TestEnds(string WordsWritten)
        {
            

            List<string> LinesWritten = WordsWritten.Split(' ').ToList();
            List<string> Lines = GetText(enTextType.eTest).Trim().Split(' ').ToList();

            for (int i = 0; i < LinesWritten.Count; i++)
            {
                if (LinesWritten[i] == Lines[i])
                    Success++;
                else
                    Failed++;
            }

            All = LinesWritten.Count;

        }


        static public float GetAccuracy()
        {
            if (Success == 0)
                return 0;

            return ((float)Success * 100) / (float)All;
        }

        static public float GetWrong()
        {
            if (Failed == 0)
                return 0;

            return ((float)Failed * 100) / (float)All;
        }


        static public void Reset()
        {
            Success = 0; Failed = 0; All = 0; CurrentTime = 60;
        }


    }
}
