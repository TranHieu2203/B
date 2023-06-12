Datetime: 11:35:35 PM Thursday, July 7, 2022
Time Process: 1
Path: AttendanceRepository.Business.vb | ReadCheckInOutData
Info : Info connect and query
select
t.UserEnrollNumber As EMPLOYEE_CODE
,Source As MACHINE_TYPE
,MAchineNo As Org_Id
,WorkCode As account_shortname
,CONVERT(VARCHAR(10), t.TimeDate, 111) As WORKINGDAY
,CONVERT(VARCHAR(5), t.TimeStr, 8) As VALTIME
from CheckInOut t
Where CONVERT(VARCHAR(8), t.TimeDate, 112) >='20220706'
And CONVERT(VARCHAR(8), t.TimeDate, 112) <='20220707'  Provider=SQLOLEDB;Data Source=HSVSGNEDB03;Initial Catalog=WiseEyeOn39;User ID=HRIS;Password=wiseeye@hsv@2021;
Datetime: 11:35:53 PM Thursday, July 7, 2022
Time Process: 20
Path: AttendanceRepository.Business.vb | ReadCheckInOutData
Info : iTime.ReadCheckInOutData -99
