namespace Davaleba_._2
{
    public partial class Form1 : Form
    {
        private School school;
        public Form1()
        {
            InitializeComponent();
            school = new School();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(textBox1.Text))
            {
                MessageBox.Show("Fill in appropriate boxes", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            school.AddTeacher(textBox1.Text);
            new Form2("Teacher added successfully").ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(textBox1.Text) || String.IsNullOrEmpty(textBox2.Text))
            {
                MessageBox.Show("Fill in appropriate boxes", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            school.AddSubject(textBox2.Text, textBox1.Text);
            new Form2("Subject added successfully").ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(textBox3.Text) || String.IsNullOrEmpty(textBox2.Text))
            {
                MessageBox.Show("Fill in appropriate boxes", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            school.AddPupil(textBox3.Text, textBox2.Text);
            new Form2("Pupil added successfully").ShowDialog();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            richTextBox1.Clear();
            IEnumerable<string> teachers = (school.GetTeachers(textBox3.Text));

            if (teachers == null || textBox3.Text == "")
            {
                return;
            }
            foreach (string teacher in teachers)
            {
                richTextBox1.Text += teacher.ToString() + "\n";
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            richTextBox1.Clear();
            IEnumerable<string> pupils = (school.GetPupils(textBox1.Text));

            if (pupils == null || textBox1.Text == "")
            {
                return;
            }
            foreach (string pupil in pupils)
            {
                richTextBox1.Text += pupil.ToString() + "\n";
            }
        }
    }
}