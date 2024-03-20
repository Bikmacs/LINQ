using System.Xml.Linq;

namespace AbobaChanelLinq
{
    public partial class Form1 : Form
    {
        List<Person> peopleList = new List<Person>();
        List<Department> department = new List<Department>()
        {
            new Department { Name = "Отдел закупок", Reg ="Германия" },
            new Department { Name = "Отдел продаж", Reg ="Испания" },
            new Department { Name = "Отдел маркетинга", Reg ="Испания" }
        };

        List<Employ> employes = new List<Employ>()
        {
            new Employ {Name="Иванов", Department =" Отдел закупок "},
            new Employ {Name="Петров", Department =" Отдел закупок "},
            new Employ {Name="Сидоров", Department =" Отдел продаж "},
            new Employ {Name="Лямин", Department =" Отдел продаж "},
            new Employ {Name="Сидоренко", Department =" Отдел маркетинга "},
            new Employ {Name="Кривоносов", Department =" Отдел продаж "}
        };
        public Form1()
        {
            InitializeComponent();
            if (File.Exists("1.txt"))
            {

                // Read people from file
                string[] lines = File.ReadAllLines("1.txt");
                foreach (string line in lines)
                {
                    string[] parts = line.Split(' ');
                    Person person = new Person()
                    {
                        Name = parts[1],
                        Surname = parts[0],
                        Surname2 = parts[2],
                        Age = int.Parse(parts[3]),
                        Ves = double.Parse(parts[4])
                    };
                    peopleList.Add(person);
                }
            }
            else
            {
                MessageBox.Show("File not found!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            var result = from p in peopleList
                         where p.Age < 40
                         select p;

            foreach (Person person in result)
            {
                listBox1.Items.Add($"{person.Surname} {person.Name} {person.Surname2} {person.Age} {person.Ves}");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            var resultA = from d in department
                          join emp in employes on d.Name equals emp.Department
                          group emp by d.Reg into g
                          select new { Region = g.Key, Employees = g };

            foreach (var group in resultA)
            {
                listBox2.Items.Add($"Region: {group.Region}");
                foreach (var emp in group.Employees)
                {
                    listBox2.Items.Add($"{emp.Name} - {emp.Department}");
                }
            }
            var resultB = from d in department
                          join emp in employes on d.Name equals emp.Department
                          where d.Reg.StartsWith("и")
                          select emp;
            foreach (var emp in resultB)
            {
                listBox3.Items.Add($"{emp.Name} - {emp.Department}");
            }
        }
    }
}