Function: InsertEmployee
Datetime: 6/15/2017 2:46:13 PM
Error message: Conversion from string "NCS0951" to type 'Double' is not valid.
Stack trace:    at Microsoft.VisualBasic.CompilerServices.Conversions.ToDouble(String Value, NumberFormatInfo NumberFormat)
   at Microsoft.VisualBasic.CompilerServices.Conversions.ToDouble(String Value)
   at ProfileDAL.ProfileRepository.InsertEmployee(EmployeeDTO objEmp, UserLog log, Decimal& gID, String& _strEmpCode, Byte[] _imageBinary, EmployeeCVDTO objEmpCV, EmployeeEduDTO objEmpEdu, EmployeeHealthDTO objEmpHealth) in D:\HISTAFF\02.Sourcecode\01.SourceCode\ProfileDAL\ProfileRepository.Employee.vb:line 552
Inner error message: Input string was not in a correct format.
Inner stack trace:    at Microsoft.VisualBasic.CompilerServices.Conversions.ParseDouble(String Value, NumberFormatInfo NumberFormat)
   at Microsoft.VisualBasic.CompilerServices.Conversions.ToDouble(String Value, NumberFormatInfo NumberFormat)
Other Text: iProfile

