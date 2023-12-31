// This file was auto-generated by ML.NET Model Builder. 

using System;
using QuizAppML.Model;

namespace QuizAppML.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            // Create single instance of sample data from first line of dataset for model input
            ModelInput sampleData = new ModelInput()
            {
                Precipitation = 0F,
                Temp_max = 12.8F,
                Temp_min = 5F,
                Wind = 4.7F,
            };

            // Make a single prediction on the sample data and print results
            var predictionResult = ConsumeModel.Predict(sampleData);

            Console.WriteLine("Using model to make single prediction -- Comparing actual Weather with predicted Weather from sample data...\n\n");
            Console.WriteLine($"Precipitation: {sampleData.Precipitation}");
            Console.WriteLine($"Temp_max: {sampleData.Temp_max}");
            Console.WriteLine($"Temp_min: {sampleData.Temp_min}");
            Console.WriteLine($"Wind: {sampleData.Wind}");
            Console.WriteLine($"\n\nPredicted Weather value {predictionResult.Prediction} \nPredicted Weather scores: [{String.Join(",", predictionResult.Score)}]\n\n");
            Console.WriteLine("=============== End of process, hit any key to finish ===============");
            Console.ReadKey();
        }
    }
}
