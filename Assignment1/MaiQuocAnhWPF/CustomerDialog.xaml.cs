using MaiQuocAnhWPF.Data.models;
using MaiQuocAnhWPF.Business;
using System.Linq;
using System.Windows;

namespace MaiQuocAnhWPF
{
    public partial class CustomerDialog : Window
    {
        private readonly bool _isEditMode;
        public Customer Customer { get; set; }

        public CustomerDialog(Customer? customer = null)
        {
            InitializeComponent();
            _isEditMode = customer != null;

            if (_isEditMode && customer != null)
            {
                Customer = customer;
                LoadCustomerData();
                Title = "Edit Customer";
            }
            else
            {
                Customer = new Customer();
                GenerateNewId();
                Title = "Add Customer";
            }
        }

        private void GenerateNewId()
        {
            var service = new CustomerService();
            var customers = service.GetAllCustomers().ToList();
            var maxId = customers.Count > 0 ? customers.Max(c => c.CustomerID) : 0;
            Customer.CustomerID = maxId + 1;
            CustomerIdTextBox.Text = Customer.CustomerID.ToString();
        }

        private void LoadCustomerData()
        {
            CustomerIdTextBox.Text = Customer.CustomerID.ToString();
            CompanyNameTextBox.Text = Customer.CompanyName;
            ContactNameTextBox.Text = Customer.ContactName;
            ContactTitleTextBox.Text = Customer.ContactTitle;
            AddressTextBox.Text = Customer.Address;
            PhoneTextBox.Text = Customer.Phone;
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            if (!ValidateInput()) return;

            Customer.CompanyName = CompanyNameTextBox.Text.Trim();
            Customer.ContactName = ContactNameTextBox.Text.Trim();
            Customer.ContactTitle = ContactTitleTextBox.Text.Trim();
            Customer.Address = AddressTextBox.Text.Trim();
            Customer.Phone = PhoneTextBox.Text.Trim();

            DialogResult = true;
            Close();
        }

        private bool ValidateInput()
        {
            if (string.IsNullOrWhiteSpace(CompanyNameTextBox.Text))
            {
                MessageBox.Show("Company Name is required.", "Validation Error",
                               MessageBoxButton.OK, MessageBoxImage.Warning);
                CompanyNameTextBox.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(ContactNameTextBox.Text))
            {
                MessageBox.Show("Contact Name is required.", "Validation Error",
                               MessageBoxButton.OK, MessageBoxImage.Warning);
                ContactNameTextBox.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(PhoneTextBox.Text))
            {
                MessageBox.Show("Phone is required.", "Validation Error",
                               MessageBoxButton.OK, MessageBoxImage.Warning);
                PhoneTextBox.Focus();
                return false;
            }

            return true;
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}