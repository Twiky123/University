using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace UniversityApp
{
    public partial class MainForm : Form
    {
        private Dictionary<string, List<University>> universitiesByCity;

        public object dataGridView1 { get; private set; }
        public object comboBoxCities { get; private set; }

        public MainForm()
        {
            InitializeComponent();
            LoadData();
            PopulateCitiesComboBox();
        }

        private void InitializeComponent()
        {
            throw new NotImplementedException();
        }

        private void LoadData()
        {
            universitiesByCity = new Dictionary<string, List<University>>();

            string[] lines = File.ReadAllLines("universities.txt");
            foreach (string line in lines)
            {
                string[] parts = line.Split(',');
                string city = parts[2].Trim();

                if (!universitiesByCity.ContainsKey(city))
                {
                    universitiesByCity[city] = new List<University>();
                }

                universitiesByCity[city].Add(new University
                {
                    Id = int.Parse(parts[0]),
                    Name = parts[1],
                    City = city,
                    ImagePath = parts[3].Trim()
                });
            }
        }

        private void PopulateCitiesComboBox()
        {
            comboBoxCities.Items.AddRange(universitiesByCity.Keys.ToArray());
        }

        private void comboBoxCities_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedCity = comboBoxCities.SelectedItem.ToString();
            dataGridView1.DataSource = universitiesByCity[selectedCity];
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                University selectedUniversity = (University)row.DataBoundItem;

                Form2 detailsForm = new Form2(selectedUniversity);
                detailsForm.ShowDialog();
            }
        }
    }

    public class University
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        public string ImagePath { get; set; }
    }

    public partial class Form2 : Form
    {
        private University university;

        public Form2(University university)
        {
            InitializeComponent();
            this.university = university;
            DisplayUniversityDetails();
        }

        private void DisplayUniversityDetails()
        {
            Text = university.Name;
            textBoxCity.Text = university.City;
            pictureBoxUniversity.ImageLocation = university.ImagePath;
        }
    }
}