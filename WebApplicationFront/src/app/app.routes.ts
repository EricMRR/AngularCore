
import { NgModule, Injectable } from '@angular/core';
import { RouterModule, Routes, DefaultUrlSerializer, UrlSerializer, UrlTree, TitleStrategy } from '@angular/router';

import { DispositivoComponent } from './components/dispositivo/dispositivo.component';

export const routes: Routes = [
{ path: 'dispositivo', component: DispositivoComponent, title: 'Dispositivo' },

];
