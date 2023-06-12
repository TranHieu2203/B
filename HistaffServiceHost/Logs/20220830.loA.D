Date,Time,TimeProcess,Class,Method,leve-message,exception,InnerException,StackTrace
30/08/2022,13:35:05,170,AttendanceRepository.Business.vb,ReadCheckInOutData,Info-Info connect and query
select
t.UserEnrollNumber As EMPLOYEE_CODE
,Source As MACHINE_TYPE
,MAchineNo As Org_Id
,WorkCode As account_shortname
,CONVERT(VARCHAR(10), t.TimeDate, 111) As WORKINGDAY
,CONVERT(VARCHAR(5), t.TimeStr, 8) As VALTIME
from CheckInOut t
Where CONVERT(VARCHAR(8), t.TimeDate, 112) >='20220829'
And CONVERT(VARCHAR(8), t.TimeDate, 112) <='20220830'  Provider=SQLOLEDB;Data Source=HSVSGNEDB03;Initial Catalog=WiseEyeOn39;User ID=HRIS;Password=wiseeye@hsv@2021;,,,
30/08/2022,13:35:24,188,AttendanceRepository.Business.vb,ReadCheckInOutData,Info-iTime.ReadCheckInOutData -99,,,
