using QuizApp.Models;

namespace QuizApp
{
    public partial class Form1 : Form
    {
        int _score = 0;
        int wrongAnswers = 0;

        QuizApiClient? _client;
        List<QuizQuestion> quizQuestions = [];
        int _currentQuestion = 0;


        public Form1()
        {
            InitializeComponent();
        }

        override protected async void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            _client = QuizApiClient.Create();
            await GetQuestions();
            SetNextQuestion();
        }

        async Task GetQuestions()
        {
            quizQuestions = await _client?.GetQuizQuestionsAsync();
        }

        void SetNextQuestion()
        {
            var question = quizQuestions[_currentQuestion];

            label1.Text = question.Question;

            List<string> answers = new List<string>();
            answers.Add(question.CorrectAnswer);
            answers.AddRange(question.IncorrectAnswers);

            Random rnd = new Random();
            for (int i = answers.Count - 1; i > 0; i--)
            {
                int j = rnd.Next(i + 1);
                (answers[i], answers[j]) = (answers[j], answers[i]);
            }

            button1.Text = answers[0];
            button2.Text = answers[1];
            button3.Text = answers[2];
            button4.Text = answers[3];
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Button? current_button = sender as Button;

            var correctAnswer = quizQuestions[_currentQuestion].CorrectAnswer;

            if (current_button.Text == correctAnswer)
            {
                _score++;
                MessageBox.Show("Correct answer!");
            }
            else
            {
                wrongAnswers++;
                MessageBox.Show("Wrong answer!");
            }

            UpdateScoreLabel();


            _currentQuestion++;

            if (_currentQuestion < quizQuestions.Count)
            {
                SetNextQuestion();
                UpdateScoreLabel();
            }
            else
            {
                MessageBox.Show($"Quiz finished!");
            }
        }

        void UpdateScoreLabel()
        {
            int total = quizQuestions.Count;
            labelScore.Text = $"{_score} av {total} svar rätt";
        }
    }
}

