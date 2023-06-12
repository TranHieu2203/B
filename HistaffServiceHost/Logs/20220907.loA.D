Date,Time,TimeProcess,Class,Method,leve-message,exception,InnerException,StackTrace
07/09/2022,12:30:00,0,AttendanceRepository.Business.vb,ReadCheckInOutData,Info-Info connect and query
select
t.UserEnrollNumber As EMPLOYEE_CODE
,Source As MACHINE_TYPE
,MAchineNo As Org_Id
,WorkCode As account_shortname
,CONVERT(VARCHAR(10), t.TimeDate, 111) As WORKINGDAY
,CONVERT(VARCHAR(5), t.TimeStr, 8) As VALTIME
from CheckInOut t
Where CONVERT(VARCHAR(8), t.TimeDate, 112) >='20220906'
And CONVERT(VARCHAR(8), t.TimeDate, 112) <='20220907'  Provider=SQLOLEDB;Data Source=HSVSGNEDB03;Initial Catalog=WiseEyeOn39;User ID=HRIS;Password=wiseeye@hsv@2021;,,,
07/09/2022,12:30:18,19,AttendanceRepository.Business.vb,ReadCheckInOutData,Info-iTime.ReadCheckInOutData -99,,,
