namespace QuizApp
{
    public partial class Form1 : Form
    {
        QuizApiClient? _client;

        public Form1()
        {
            InitializeComponent();
        }

        override protected void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            _client = QuizApiClient.Create();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            var questions = _client?.GetQuizQuestionsAsync();
            foreach(var question in await questions!)
            {
                listBox1.Items.Add(question.Question);
                listBox1.Items.Add("-- " + question.CorrectAnswer);
                foreach(var incorrect in question.IncorrectAnswers)
                {
                    listBox1.Items.Add("---- " + incorrect);
                }
            }
        }
    }
}
