from django.shortcuts import render
from django.http import HttpResponse
# Create your views here.

def index(request):
    #return HttpResponse('ccc')
    title ={'name': 'Chart view'}
    return render(request,'Chart/index.html',title)
