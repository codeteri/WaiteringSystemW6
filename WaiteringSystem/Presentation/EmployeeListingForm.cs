using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WaiteringSystem.Business;

namespace WaiteringSystem.Presentation
{


    public partial class EmployeeListingForm : Form
    {

        #region Variables
        public bool listFormClosed;//= true;
        private Collection<Employee> employees;
        private Role.RoleType roleValue;
        private EmployeeController employeeController;
        private FormStates state;
        #endregion

        #region property methods

        public Role.RoleType RoleValue
        {

            set { roleValue = value; }

        }

        #endregion

        #region Constructor
        public EmployeeListingForm()
        {
            InitializeComponent();
            this.Load += EmployeeListingForm_Load;
            this.Activated += EmployeeListingForm_Activated;
            state = FormStates.View;
        }



        public EmployeeListingForm(EmployeeController empController)
        {

            InitializeComponent();
            employeeController = empController;
            this.Load += EmployeeListingForm_Load;
            this.Activated += EmployeeListingForm_Activated;

        }
        #endregion

        #region Events
        private void EmployeeListingForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            listFormClosed = true;
        }
        
        private void EmployeeListingForm_Load(object sender, EventArgs e)
        {
            employeeListView.View = View.Details;
        }

        private void EmployeeListingForm_Activated(object sender, EventArgs e)
        {
            employeeListView.View = View.Details;
        }
        #endregion

        #region ListView set up
        public void setUpEmployeeListView()
        {
            ListViewItem employeeDetails;
            HeadWaiter headW;
            Waiter waiter;
            Runner runner;
            employees = null;
            employeeListView.Clear();

            employeeListView.Columns.Insert(0, "ID", 120, HorizontalAlignment.Left);
            employeeListView.Columns.Insert(1, "EMPID", 120, HorizontalAlignment.Left);
            employeeListView.Columns.Insert(2, "Name", 120, HorizontalAlignment.Left);
            employeeListView.Columns.Insert(3, "Phone", 120, HorizontalAlignment.Left);


            switch (roleValue)
            {
                case Role.RoleType.NoRole:
                    employees = employeeController.AllEmployees; listLabel.Text = "Listing of all employees";
                    employeeListView.Columns.Insert(4, "Payment", 100, HorizontalAlignment.Center);
                    break;
                case Role.RoleType.Headwaiter:
                    //Add a FindByRole method to the EmployeeController
                    employees = employeeController.FindByRole(employeeController.AllEmployees, Role.RoleType.Headwaiter);
                    listLabel.Text = "Listing of all Headwaiters";
                    //Set Up Columns of List View
                    employeeListView.Columns.Insert(4, "Salary", 100, HorizontalAlignment.Center);
                    break;
                case Role.RoleType.Waiter:
                    //Add a FindByRole method to the EmployeeController
                    employees = employeeController.FindByRole(employeeController.AllEmployees, Role.RoleType.Waiter);
                    listLabel.Text = "Listing of all Waiters";
                    //Set Up Columns of List View
                    employeeListView.Columns.Insert(4, "DayRate", 100, HorizontalAlignment.Center);
                    employeeListView.Columns.Insert(5, "NoOfShifts", 100, HorizontalAlignment.Center);
                    employeeListView.Columns.Insert(6, "Tips", 100, HorizontalAlignment.Center);
                    break;

                case Role.RoleType.Runner:
                    //Add a FindByRole method to the EmployeeController
                    employees = employeeController.FindByRole(employeeController.AllEmployees, Role.RoleType.Runner);
                    listLabel.Text = "Listing of all Runners";
                    //Set Up Columns of List View
                    employeeListView.Columns.Insert(4, "DayRate", 100, HorizontalAlignment.Center);
                    employeeListView.Columns.Insert(5, "NoOfShifts", 100, HorizontalAlignment.Center);
                    employeeListView.Columns.Insert(6, "Tips", 100, HorizontalAlignment.Center);
                    break;
            }




            foreach (Employee employee in employees)
            {
                employeeDetails = new ListViewItem();
                employeeDetails.Text = employee.ID.ToString();
                employeeDetails.SubItems.Add(employee.EmployeeID.ToString());
                employeeDetails.SubItems.Add(employee.Name.ToString());
                employeeDetails.SubItems.Add(employee.Telephone.ToString());
                // Do the same for EmpID, Name and Phone
                switch (employee.role.getRoleValue)
                {
                    case Role.RoleType.Headwaiter:
                        headW = (HeadWaiter)employee.role;
                        employeeDetails.SubItems.Add(headW.SalaryAmount.ToString());
                        break;
                    case Role.RoleType.Waiter:
                        waiter = (Waiter)employee.role;
                        employeeDetails.SubItems.Add(waiter.getRate.ToString());
                        employeeDetails.SubItems.Add(waiter.getShifts.ToString());
                        employeeDetails.SubItems.Add(waiter.getTips.ToString());
                        break;
                    case Role.RoleType.Runner:
                        runner = (Runner)employee.role;
                        employeeDetails.SubItems.Add(runner.getRate.ToString());
                        employeeDetails.SubItems.Add(runner.getShifts.ToString());
                        employeeDetails.SubItems.Add(runner.getTips.ToString());
                        break;

                }

                employeeListView.Items.Add(employeeDetails);


            }

            employeeListView.Refresh();
            employeeListView.GridLines = true;
        }
        #endregion

        private void listLabel_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

    }
}
