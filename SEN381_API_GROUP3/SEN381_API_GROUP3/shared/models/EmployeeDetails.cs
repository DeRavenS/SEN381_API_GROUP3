namespace SEN381_API_GROUP3.shared.models
{
    public class EmployeeDetails
    {
        string employeeName;
        string employeeEmail;
        string employeePassword;

        public EmployeeDetails(string employeeName, string employeeEmail, string employeePassword)
        {
            this.EmployeeName = employeeName;
            this.EmployeeEmail = employeeEmail;
            this.EmployeePassword = employeePassword;
        }

        public string EmployeeName { get => employeeName; set => employeeName = value; }
        public string EmployeeEmail { get => employeeEmail; set => employeeEmail = value; }
        public string EmployeePassword { get => employeePassword; set => employeePassword = value; }

        public override bool Equals(object? obj)
        {
            return obj is EmployeeDetails details &&
                   employeeName == details.employeeName &&
                   employeeEmail == details.employeeEmail &&
                   employeePassword == details.employeePassword;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(employeeName, employeeEmail, employeePassword);
        }
    }
}
