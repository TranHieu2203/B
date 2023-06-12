from django.urls import path
from . import views
urlpatterns = [
    path('', views.index,name='index'),
    path('', views.status_process_approve,name='status_process_approve'),
]
