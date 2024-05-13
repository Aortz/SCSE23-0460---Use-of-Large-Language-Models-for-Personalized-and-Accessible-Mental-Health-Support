using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

namespace HelperFunction
{
    public class JsonTextParser : MonoBehaviour
    {

        // Example method to call with your JSON response
        public string[] ParseJsonResponse(string jsonResponse)
        {
            // Step 1: Parse the JSON response
            string cleanedText = RemoveFormatting(jsonResponse);

            // Step 3: Split text into sentences
            return SplitIntoSentences(cleanedText);
        }

        private string RemoveFormatting(string text)
        {
            // Remove bullet points, asterisks, and numerical list prefixes
            string withoutBullets = Regex.Replace(text, @"[\u2022\u2023\u25E6\u2043\u2219\*]", ""); // Common bullet characters and asterisks
            string withoutNumbering = Regex.Replace(withoutBullets, @"\d+\.\s*", ""); // Matches numerical prefixes like "1.", "2.", etc.
            string cleanedText = Regex.Replace(withoutNumbering, @"\s+", " "); // Replace multiple whitespaces with a single space
            return cleanedText.Trim();
        }

        private string[] SplitIntoSentences(string text)
        {
            // Split the text into sentences based on common sentence-ending punctuation
            string[] sentences = Regex.Split(text, @"(?<=[\.!\?])\s+");
            return sentences;
        }
    }
}