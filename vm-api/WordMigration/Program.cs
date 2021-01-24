using CSharpVerbalExpressions;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace WordMigration
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Model> modelList = new List<Model>();
            List<string> brouhgtText = new List<string>();
            string line;
            System.IO.StreamReader file =
                new System.IO.StreamReader(@"C:\Repository\wordmeister\vm-api\WordMigration\Documents\3000words.txt");
            while ((line = file.ReadLine()) != null)
            {
                if(string.IsNullOrEmpty(line) || string.IsNullOrWhiteSpace(line))
                {
                    continue;
                }

                line = Regex.Replace(line, @"\d[)]|\d[.]", "-");

                var splittedTab = line.Replace("\t", "").Split("-");

                try
                {
                    var model = new Model();

                    string text = splittedTab[1];
                    string translate = splittedTab[2];

                    var splittedType = text.Split(" ");
                    string type = splittedType[splittedType.Length - 1];

                    text = text.Replace(type, "");

                    model.Text = text;
                    model.Type = type;
                    model.TranslatedText = translate;
                    modelList.Add(model);
                }
                catch (Exception)
                {
                    brouhgtText.Add(line);
                }

            }

            file.Close();
        }
    }

    public class Model
    {
        public string Text { get; set; }
        public string Type { get; set; }
        public string TranslatedText { get; set; }
    }
}
