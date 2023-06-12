from django.shortcuts import render
from django.http import HttpResponse
from django.shortcuts import render 
#import urllib.request as urlrq
#import certifi
#import ssl
import requests
# Create your views here.
def index(request):
    #dataJson
        response = requests.get('https://localhost:5001/api/Process/72FC1F59C5CCB34F7E29A71AF111E170',verify=False)
        dataJson = response.json()
        return render(request,'Dashboard/index.html',{
            'EMPLOYEE_ID' :dataJson[0]['EMPLOYEE_ID'],
            'PE_PERIOD_ID':dataJson[0]['PE_PERIOD_ID'],
            'EMPLOYEE_APPROVED':dataJson[0]['EMPLOYEE_APPROVED'],
            'APP_DATE':dataJson[0]['APP_DATE'],
            'APP_LEVEL':dataJson[0]['APP_LEVEL'],
            'APP_STATUS':dataJson[0]['APP_STATUS'],
            'CREATED_BY':dataJson[0]['CREATED_BY'],
            'CREATED_DATE':dataJson[0]['CREATED_DATE'],
            'ID_REGGROUP':dataJson[0]['ID_REGGROUP'],
            'TEMPLATE_ID':dataJson[0]['TEMPLATE_ID'],
            'PROCESS_TYPE':dataJson[0]['PROCESS_TYPE'],
            'APP_NOTES': dataJson[0]['APP_NOTES']
        })


def status_process_approve(request) :
        emp= requests.post(['hidEMPLOYEE_ID'])
        period= requests.post(['hidPE_PERIOD_ID'])
        print(emp)
        return render(request,'Dashboard/process_approve.html',{'resulst':period})

        #      contentProces={
        #      'EMPLOYEE_ID':dataJson[0]['EMPLOYEE_ID'],
        #      'PE_PERIOD_ID':dataJson[0]['PE_PERIOD_ID'],
        #      'EMPLOYEE_APPROVED':dataJson[0]['EMPLOYEE_APPROVED'],
        #      'APP_LEVEL':dataJson[0]['APP_LEVEL'],
        #      'PROCESS_TYPE':dataJson[0]['PROCESS_TYPE'],
        #      'APP_NOTES': dataJson[0]['APP_NOTES']
        #  }
        #  rep= requests.post('https://localhost:5001/api/Process/',json=contentProces)
        #  print(rep.text)
