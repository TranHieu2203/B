from django.shortcuts import render
from django.http import HttpResponse
# Create your views here.

def index(request):
    #return HttpResponse('ccc')
    title ={'name': 'Dashboard view'}
    return render(request,'Dashboard/index.html',title)