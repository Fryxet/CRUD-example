using System;
using System.Linq;
using System.Windows;
using Microsoft.EntityFrameworkCore;

namespace CompanyApp
{
    public partial class MainWindow : Window
    {
        private CompanyContext _context = new CompanyContext();

        public MainWindow()
        {
            InitializeComponent();
            LoadData();
        }

        private void LoadData()
        {
            EmployeesGrid.ItemsSource = _context.Employees.ToList();
        }

        private void EmployeesGrid_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            var selectedEmployee = EmployeesGrid.SelectedItem as Employee;
            if (selectedEmployee != null)
            {
                NameBox.Text = selectedEmployee.Name;
                PositionBox.Text = selectedEmployee.Position;
                DepartmentBox.Text = selectedEmployee.Department;
                HireDateBox.SelectedDate = selectedEmployee.HireDate;
                SalaryBox.Text = selectedEmployee.Salary.ToString();
                EmailBox.Text = selectedEmployee.Email;
            }
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            if (AreFieldsEmpty())
            {
                MessageBox.Show("Заполните все поля перед добавлением сотрудника.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var employee = new Employee
            {
                Name = NameBox.Text,
                Position = PositionBox.Text,
                Department = DepartmentBox.Text,
                HireDate = HireDateBox.SelectedDate ?? DateTime.Now,
                Salary = decimal.Parse(SalaryBox.Text),
                Email = EmailBox.Text
            };

            _context.Employees.Add(employee);
            _context.SaveChanges();
            LoadData();
            ClearFields();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (AreFieldsEmpty())
            {
                MessageBox.Show("Заполните все поля перед сохранением изменений.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var selectedEmployee = EmployeesGrid.SelectedItem as Employee;
            if (selectedEmployee != null)
            {
                selectedEmployee.Name = NameBox.Text;
                selectedEmployee.Position = PositionBox.Text;
                selectedEmployee.Department = DepartmentBox.Text;
                selectedEmployee.HireDate = HireDateBox.SelectedDate ?? DateTime.Now;
                selectedEmployee.Salary = decimal.Parse(SalaryBox.Text);
                selectedEmployee.Email = EmailBox.Text;

                _context.SaveChanges();
                LoadData();
                ClearFields();
            }
            else
            {
                MessageBox.Show("Выберите сотрудника для редактирования.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedEmployee = EmployeesGrid.SelectedItem as Employee;
            if (selectedEmployee != null)
            {
                _context.Employees.Remove(selectedEmployee);
                _context.SaveChanges();
                LoadData();
                ClearFields();
            }
            else
            {
                MessageBox.Show("Выберите сотрудника для удаления.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void ClearFields()
        {
            NameBox.Text = string.Empty;
            PositionBox.Text = string.Empty;
            DepartmentBox.Text = string.Empty;
            HireDateBox.SelectedDate = null;
            SalaryBox.Text = string.Empty;
            EmailBox.Text = string.Empty;
        }

        private bool AreFieldsEmpty()
        {
            return string.IsNullOrWhiteSpace(NameBox.Text) ||
                   string.IsNullOrWhiteSpace(PositionBox.Text) ||
                   string.IsNullOrWhiteSpace(DepartmentBox.Text) ||
                   HireDateBox.SelectedDate == null ||
                   string.IsNullOrWhiteSpace(SalaryBox.Text) ||
                   string.IsNullOrWhiteSpace(EmailBox.Text);
        }
    }
}