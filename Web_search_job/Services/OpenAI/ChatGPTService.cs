using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;
using OpenAI_API;
using OpenAI_API.Completions;


public class ChatGPTService
{
    private static readonly HttpClient client = new HttpClient();

    /*public async Task<string> GetStructuredDataFromChatGPT(string text)
    {
        var openAiApiKey = "sk-proj-OF5Vgb0HOk3JriK4GhtCT3BlbkFJgGshQCqr0RwR2HEXdwu0"; // Replace with your OpenAI API key

        APIAuthentication aPIAuthentication = new APIAuthentication(openAiApiKey);
        OpenAIAPI openAiApi = new OpenAIAPI(aPIAuthentication);


        var resumeData = new
        {
            firstName = (string)null,
            lastName = (string)null,
            location = (string)null,
            age = (int?)null,
            phone = (string)null,
            skills = new string[] { },
            languages = new[]
                {
                    new { language = (string)null, level = (string)null }
                },
            links = new[]
                {
                    new { link_type = (string)null, link_url = (string)null }
                },
            wantedJob = (string)null,
            wantedSalary = new
            {
                number = (int?)null,
                currency = (string)null,
                time = (string)null
            },
            aboutMe = (string)null,
            workHistory = new[]
                {
                    new
                    {
                        jobName = (string)null,
                        employerName = (string)null,
                        data_start = (string)null,
                        data_end = (string)null,
                        duty = (string)null
                    }
                },
            education = new[]
                {
                    new
                    {
                        education_centre_name = (string)null,
                        nameProffesion = (string)null,
                        industry = (string)null,
                        locationEducation = (string)null,
                        yearEducation = (string)null
                    }
                },
            portfolio = new[]
                {
                    new
                    {
                        portfolioLink = (string)null,
                        portfolioName = (string)null
                    }
                }
        };

        try
        {
            string prompt = "Ти парсер резюме. Ти повинен дати мені відповідь в форматі даних JSON:" +
                "{\r\n  \"firstName\": null,              // Прізвище (string)\r\n  \"lastName\": null,               // Ім'я (string)\r\n  \"location\": null,               // Місто розташування (string)\r\n  \"age\": null,                    // Вік (number)\r\n  \"phone\": null,                  // Номер телефону (string)\r\n  \"skills\": [],                   // Навички (масив стрінгів)\r\n  \"languages\": [                  // Мови (масив об'єктів)\r\n    {\r\n      \"language\": null,           // Мова (string)\r\n      \"level\": null               // Рівень володіння (string: Початковий, Середній, Високий, Рідний)\r\n    }\r\n  ],\r\n  \"links\": [                      // Посилання (масив об'єктів)\r\n    {\r\n      \"link_type\": null,          // Тип посилання (string: linkedIn, behance, facebook, dribbble)\r\n      \"link_url\": null            // URL посилання (string)\r\n    }\r\n  ],\r\n  \"wantedJob\": null,              // Бажана посада (string)\r\n  \"wantedSalary\": {               // Бажана заробітня плата (об'єкт)\r\n    \"number\": null,               // Число (number)\r\n    \"currency\": null,             // Валюта (string: UAH або USD)\r\n    \"time\": null                  // Час (string: година, місяць, рік)\r\n  },\r\n  \"aboutMe\": null,                // Опис про себе (string)\r\n  \"workHistory\": [                // Історія роботи (масив об'єктів)\r\n    {\r\n      \"jobName\": null,            // Назва професії (string)\r\n      \"employerName\": null,       // Назва компанії (string)\r\n      \"data_start\": null,         // Дата початку (string)\r\n      \"data_end\": null,           // Дата закінчення (string)\r\n      \"duty\": null                // Основні обов'язки (string)\r\n    }\r\n  ],\r\n  \"education\": [                  // Освіта (масив об'єктів)\r\n    {\r\n      \"education_centre_name\": null,   // Назва навчального закладу (string)\r\n      \"nameProffesion\": null,          // Назва професії навчання (string)\r\n      \"industry\": null,                 // Індустрія (string)\r\n      \"locationEducation\": null,        // Місто навчання (string)\r\n      \"yearEducation\": null             // Рік навчання (string)\r\n    }\r\n  ],\r\n  \"portfolio\": [                  // Портфоліо (масив об'єктів)\r\n    {\r\n      \"portfolioLink\": null,      // Посилання на роботу (string)\r\n      \"portfolioName\": null       // Назва роботи (string)\r\n    }\r\n  ]\r\n}";
            string model = "gpt-3.5-turbo";
            int maxTokens = 50;

            var completionRequest = new CompletionRequest
            {
                Prompt = prompt,
                Model = model,
                MaxTokens = maxTokens
            };

            var completionResult = await openAiApi.Completions.CreateCompletionAsync(completionRequest);
            var generatedText = completionResult.Completions[0].Text; //completionResult.Choices[0].Text.Trim();

            Console.WriteLine("Generated text:");
            Console.WriteLine(generatedText);

            return generatedText;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
            return null;
        }

    }
*/



public async Task<string> GetStructuredDataFromChatGPT(string text)
    {
        var apiKey = "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";
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);


        var resumeData = new
        {
            firstName = (string)null,
            lastName = (string)null,
            location = (string)null,
            age = (int?)null,
            phone = (string)null,
            skills = new string[] { },
            languages = new[]
            {
                new { language = (string)null, level = (string)null }
            },
            links = new[]
            {
                new { link_type = (string)null, link_url = (string)null }
            },
            wantedJob = (string)null,
            wantedSalary = new
            {
                number = (int?)null,
                currency = (string)null,
                time = (string)null
            },
            aboutMe = (string)null,
            workHistory = new[]
            {
                new
                {
                    jobName = (string)null,
                    employerName = (string)null,
                    data_start = (string)null,
                    data_end = (string)null,
                    duty = (string)null
                }
            },
            education = new[]
            {
                new
                {
                    education_centre_name = (string)null,
                    nameProffesion = (string)null,
                    industry = (string)null,
                    locationEducation = (string)null,
                    yearEducation = (string)null
                }
            },
            portfolio = new[]
            {
                new
                {
                    portfolioLink = (string)null,
                    portfolioName = (string)null
                }
            }
        };

        var requestBody = new
        {
            model = "gpt-3.5-turbo",
            messages = new[]
            {
                new { role = "system", content = $"You are a resume parser for ukrainian language with english language elements." },
                new { role = "user", content = JsonConvert.SerializeObject(resumeData) }
            }
        };

        var content = new StringContent(JsonConvert.SerializeObject(requestBody), System.Text.Encoding.UTF8, "application/json");

        var response = await client.PostAsync("https://api.openai.com/v1/chat/completions", content);

        response.EnsureSuccessStatusCode();
        var responseBody = await response.Content.ReadAsStringAsync();
        var parsedResponse = JsonConvert.DeserializeObject<ChatGPTResponse>(responseBody);

        return parsedResponse.choices[0].message.content;
    }
}

public class ChatGPTResponse
{
    public Choice[] choices { get; set; }
}

public class Choice
{
    public Message message { get; set; }
}

public class Message
{
    public string content { get; set; }
}