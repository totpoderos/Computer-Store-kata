using System;
using System.Collections.Generic;
using System.Linq;

namespace ComputerStore.Services
{
    public static class ImageService
    {
        private const int CharSizey = 8;

        private static Dictionary<char, string[]> chars = new Dictionary<char, string[]>
        {
            {' ', new[]
            {
                "        ",
                "        ",
                "        ",
                "        ",
                "        ",
                "        ",
                "        ",
                "        "
            }},
            {'i', new[]
            {
                "        ",
                "        ",
                "   *    ",
                "  **    ",
                "   *    ",
                "   *    ",
                "   *    ",
                "  ***   "
            }},
            {'M', new[]
            {
                "        ",
                " *    * ",
                " **  ** ",
                " * ** * ",
                " *    * ",
                " *    * ",
                " *    * ",
                " *    * "
            }},
            {'a', new[]
            {
                "        ",
                "        ",
                " *****  ",
                "      * ",
                " ****** ",
                " *    * ",
                " *    * ",
                " ***** *"
            }},
            {'c', new[]
            {
                "        ",
                "        ",
                "  ****  ",
                " *    * ",
                " *      ", 
                " *      ",
                " *    * ",
                "  ****  "
            }},
            {'r', new[]
            {
                "        ",
                "        ",
                " *** ** ",
                "  ** *** ",
                "  *** **", 
                "  **    ",
                "  **    ",
                " ****   "
            }},
            {'B', new[]
            {
                "        ",
                " ****** ",
                " *     *",
                " *     *",
                " * **** ", 
                " *     *",
                " *     *",
                " ****** "
            }},
            {'P', new[]
            {
                "        ",
                " ****** ",
                " *     *",
                " *     *",
                " * **** ", 
                " *      ",
                " *      ",
                " *      "
            }},
            {'k', new[]
            {
                "        ",
                "        ",
                " *     *",
                " *    * ",
                " * **   ", 
                " *   *  ",
                " *    * ",
                " *     * "
            }},
            {'o', new[]
            {
                "        ",
                "        ",
                " ****** ",
                " *    * ",
                " *    * ", 
                " *    * ",
                " *    * ",
                " ****** "
            }},
            {'1', new[]
            {
                "        ",
                "     *  ",
                "    **  ",
                "  ****  ",
                "    **  ", 
                "    **  ",
                "    **  ",
                "  ******"
            }},
            {'2', new[]
            {
                "        ",
                " ****** ",
                "**    **",
                "*   **  ", 
                "   **   ",
                "  **    ",
                " **   **",
                " *******"
            }},
            {'3', new[]
            {
                "        ",
                " ****** ",
                "**    **",
                "      **",
                "    **  ",
                "      **", 
                "**    **",
                " ****** "
            }},
            {'5', new[]
            {
                "         ",
                " ******* ",
                " **      ",
                " **      ",
                " ******  ",
                "      **  ",
                " **   ** ",
                "  *****  ", 
            }},
            {'7', new[]
            {
                "         ",
                " ********",
                " **    **",
                "       **",
                "      ** ",
                "     **  ",
                "    **   ",
                "    **   ", 
            }},
            
        };
        public static string[] TransformCharToDotMatrix(char character)
        {
            if (!chars.ContainsKey(character)) return null;
            return chars[character].ToArray();
        }

        public static List<string> TransformTextoToAsciiMatrix(string computerImageFilename)
        {
            List<string[]> charList = new List<string[]>();
            computerImageFilename.ToCharArray().ToList().ForEach(c =>  charList.Add(TransformCharToDotMatrix(c)));
            List<string> asciiList = new List<string>();
            for (int i = 0; i < CharSizey; i++)
            {
                string line = String.Empty;
                for (int j = 0; j < charList.Count; j++)
                {
                    line += (charList[j])[i];
                }
                asciiList.Add(line);
            }
            return asciiList;
        }
    }
}