using System;
using System.Collections.Generic;
using System.Media;
using System.Windows.Forms;

namespace CyberSecurityChatbotGUI
{
    public partial class Form1 : Form
    {
        Random random = new Random();

        Dictionary<string, List<string>> keywordResponses =
            new Dictionary<string, List<string>>();

        string userName = "";
        string favouriteTopic = "";

        public Form1()
        {
            InitializeComponent();

            SetupResponses();
            StartChatbot();
        }

        // =========================
        // CHATBOT STARTUP
        // =========================
        private void StartChatbot()
        {
            PlayGreeting();

            AppendBotMessage("=========================================");
            AppendBotMessage(" CYBERSECURITY AWARENESS BOT ");
            AppendBotMessage("=========================================");

            AppendBotMessage("Hello and welcome.");
            AppendBotMessage("Please enter your name to begin.");
        }

        // =========================
        // KEYWORD RESPONSES
        // =========================
        private void SetupResponses()
        {
            keywordResponses.Add("password", new List<string>()
            {
                "Strong passwords should include letters, numbers, and symbols.",
                "Avoid using personal information in your passwords.",
                "A password manager can help keep your passwords secure."
            });

            keywordResponses.Add("phishing", new List<string>()
            {
                "Phishing scams try to steal your information through fake emails or websites.",
                "Always verify suspicious links before clicking them.",
                "Be careful of urgent messages asking for personal information."
            });

            keywordResponses.Add("privacy", new List<string>()
            {
                "Never share sensitive information on unsafe websites.",
                "Check your privacy settings on social media regularly.",
                "Think carefully before posting personal information online."
            });
        }

        // =========================
        // SEND BUTTON
        // =========================
        private void btnSend_Click(object sender, EventArgs e)
        {
            string input = txtUserInput.Text.Trim();

            // INPUT VALIDATION
            if (string.IsNullOrWhiteSpace(input))
            {
                AppendBotMessage("Please enter a message first.");
                return;
            }

            AppendUserMessage(input);

            txtUserInput.Clear();

            // STORE USER NAME
            if (string.IsNullOrEmpty(userName))
            {
                userName = input;

                AppendBotMessage($"Nice to meet you, {userName}.");
                AppendBotMessage("How can I help you today?");

                return;
            }

            // SENTIMENT DETECTION
            DetectSentiment(input);

            string lowerInput = input.ToLower();

            // MEMORY FEATURE
            if (lowerInput.Contains("my favourite topic is"))
            {
                favouriteTopic = input.Replace("my favourite topic is", "").Trim();

                AppendBotMessage($"I will remember that your favourite topic is {favouriteTopic}.");

                return;
            }

            // RECALL MEMORY
            if (lowerInput.Contains("what is my favourite topic"))
            {
                if (!string.IsNullOrEmpty(favouriteTopic))
                {
                    AppendBotMessage($"Earlier you mentioned that your favourite topic is {favouriteTopic}.");
                }
                else
                {
                    AppendBotMessage("You have not told me your favourite topic yet.");
                }

                return;
            }

            bool keywordFound = false;

            // KEYWORD RECOGNITION
            foreach (var keyword in keywordResponses.Keys)
            {
                if (lowerInput.Contains(keyword))
                {
                    keywordFound = true;

                    List<string> responses = keywordResponses[keyword];

                    int randomIndex = random.Next(responses.Count);

                    AppendBotMessage(responses[randomIndex]);

                    break;
                }
            }

            // DEFAULT RESPONSE
            if (!keywordFound)
            {
                AppendBotMessage("I am not sure how to respond to that.");
                AppendBotMessage("Try asking about passwords, phishing, or privacy.");
            }
        }

        // =========================
        // SENTIMENT DETECTION
        // =========================
        private void DetectSentiment(string input)
        {
            string lowerInput = input.ToLower();

            if (lowerInput.Contains("worried") || lowerInput.Contains("scared"))
            {
                AppendBotMessage("It is understandable to feel worried about online threats.");
                AppendBotMessage("Learning cybersecurity safety tips can help protect you.");
            }
            else if (lowerInput.Contains("frustrated") || lowerInput.Contains("angry"))
            {
                AppendBotMessage("Cybersecurity issues can definitely be frustrating.");
            }
            else if (lowerInput.Contains("curious") || lowerInput.Contains("interested"))
            {
                AppendBotMessage("That is great. Learning about cybersecurity is very important.");
            }
        }

        // =========================
        // BOT MESSAGE
        // =========================
        private void AppendBotMessage(string message)
        {
            rtbChat.SelectionColor = System.Drawing.Color.LightGreen;

            rtbChat.AppendText("Bot: " + message + Environment.NewLine + Environment.NewLine);
        }

        // =========================
        // USER MESSAGE
        // =========================
        private void AppendUserMessage(string message)
        {
            rtbChat.SelectionColor = System.Drawing.Color.White;

            rtbChat.AppendText("You: " + message + Environment.NewLine + Environment.NewLine);
        }

        // =========================
        // CLEAR BUTTON
        // =========================
        private void btnClear_Click(object sender, EventArgs e)
        {
            rtbChat.Clear();
        }

        // =========================
        // VOICE GREETING
        // =========================
        private void PlayGreeting()
        {
            try
            {
                SoundPlayer player = new SoundPlayer("greeting.wav");

                player.PlaySync();
            }
            catch
            {
                AppendBotMessage("Voice greeting could not be loaded.");
            }
        }
    }
}
